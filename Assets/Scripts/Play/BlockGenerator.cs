using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] GameObject block, blockHandler;
    private int obstacle = 0;

    private int pivotX, pivotY, blockCount;
    private readonly float obstacleProb =7.5f;
    private float prob = 10f;

    public int listCount =0;

    public bool isGenerating = true;

    void Awake()
    {
        //block.transform.localScale = new Vector3(Constants.blockSize, Constants.blockSize, 1);
    }

    /// <summary>
    /// 게임 시작시 블럭 뿌려주는 함수
    /// </summary>
    /// <returns></returns>
    public bool GenerateBlockSet(bool setTarget)
    {
        blockCount = 0;
        Main.resetCheck = false;
        Main.blockList = null;
        Main.blockList = new List<GameObject>();
        Main.blockAnimatorList = new List<Animator>();

        pivotX = (int)typeof(Constants).GetField("x" + Constants.size).GetValue(typeof(Constants));
        pivotY = (int)typeof(Constants).GetField("y" + Constants.size).GetValue(typeof(Constants));
        float bSize = (float)typeof(Constants).GetField("blockSize" + Constants.size).GetValue(typeof(Constants));
        Constants.gap = (int)typeof(Constants).GetField("gap" + Constants.size).GetValue(typeof(Constants));
        block.transform.localScale = new Vector3(bSize, bSize, 1);

        /*
        for (int i = 0; i < Mathf.Pow(Constants.size, 2); i++)
        {
            GameObject blockObject = Instantiate(block, new Vector3(0, 0, 0), Quaternion.identity);
            Block blockScript = blockObject.GetComponent<Block>();
            blockScript.Coord.X = i % Constants.size;
            blockScript.Coord.Y = i / Constants.size;
            blockScript.Color = Constants.BlockType.Random;

            blockObject.name = "Block " + blockScript.Coord.GetIndex();
            blockObject.transform.parent = blockHandler.transform;
            blockObject.transform.position = new Vector3(pivotX + (Constants.gap * blockScript.Coord.X), pivotY - (Constants.gap * blockScript.Coord.Y), 0);
            Main.blockList.Add(blockObject);
            Main.blockAnimatorList.Add(blockScript.Animator);
            blockScript.Animator.SetTrigger("BlockIn");
            
        }
        */

        if(setTarget) Main.blockTarget.SetTargets(Constants.targetPool);
        StartCoroutine(GenerateBlock());

        return true;
    }

    IEnumerator GenerateBlock()
    {
        while(true)
        {
            if(blockCount == Mathf.Pow(Constants.size, 2))
            {
                Main.timer.isPause = false;
                isGenerating = false;
                Main.resetCheck = true;
                yield break;
            }
            GameObject blockObject = Instantiate(block, new Vector3(0, 0, 0), Quaternion.identity);
            Block blockScript = blockObject.GetComponent<Block>();
            blockScript.Coord.X = blockCount % Constants.size;
            blockScript.Coord.Y = blockCount / Constants.size;
            blockScript.Color = Constants.BlockType.Random;
            blockCount++;

            blockObject.name = "Block " + blockScript.Coord.GetIndex();
            blockObject.transform.parent = blockHandler.transform;
            blockObject.transform.position = new Vector3(pivotX + (Constants.gap * blockScript.Coord.X), pivotY - (Constants.gap * blockScript.Coord.Y), 0);
            Main.blockList.Add(blockObject);
            Main.blockAnimatorList.Add(blockScript.Animator);
            blockScript.Animator.SetTrigger("BlockIn");

            yield return new WaitForSeconds(0.08f);
        }
    }

    /// <summary>
    /// Click 등으로 비활성화된 블럭들 활성화
    /// </summary>
    public bool RestoreBlockSet(bool flag)
    {
        if(Main.blockList.Count == 0) return true;

        for (int i = 0; i < Mathf.Pow(Constants.size, 2); i++)
        {
            if (Main.blockList[i].activeSelf == false)
            {
                Block blockScript = Main.blockList[i].GetComponent<Block>();

                if (blockScript.Color == Constants.BlockType.LOCK) obstacle--;

                blockScript.Color = Constants.BlockType.Random;
                
                GenerateObstacle(blockScript, flag);
                Main.blockList[i].SetActive(true);
                blockScript.Animator.SetTrigger("BlockIn");
            }
        }

        Main.blockTarget.SetDifficulty();
        Main.blockTarget.SetTargets(Constants.targetPool);
        Main.resetCheck = true;
        return true;
    }

    /// <summary>
    /// 장애물 랜덤 생성
    /// </summary>
    private void GenerateObstacle(Block blockScript, bool flag)
    {
        if(!flag) return;

        if(obstacle < Constants.obsRange)
        {
            if(Constants.Prob(ref prob, 0f))
            {
                blockScript.Color = Constants.BlockType.LOCK;
                prob = obstacleProb;
                obstacle++;
            }
        }
    }

    public void DestroyBlockSet()
    {
        /*
        Main.blockAnimatorList.Clear();
        foreach(GameObject blockObject in Main.blockList)
        {
            Destroy(blockObject);
        }
        Main.blockList.Clear();
        */
        //Main.blockTarget.SetTargets(Constants.targetPool);
        
        Main.timer.isPause= true;
        isGenerating = true;
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        while(true){
            if(Main.blockList.Count == 0)
            {
                Main.blockAnimatorList.Clear();
                Main.blockList.Clear();
                Main.blockGenerator.DelayGenerate(1.5f,false,false);
                //Main.blockGenerator.GenerateBlockSet(true);

                yield break;
            }

            Main.blockList[0].GetComponent<Animator>().SetTrigger("Destroy");
            Destroy(Main.blockList[0],0.9f);
            Main.blockList.RemoveAt(0);
            

            yield return new WaitForSeconds(0.09f);
        }
    }

    
    public void DelayGenerate(float delay, bool obstacle, bool restore)
    {
        StartCoroutine(DelayGenerator(delay,obstacle,restore));
    }

    private IEnumerator DelayGenerator(float delay, bool obstacle,bool restore)
    {
        yield return new WaitForSeconds(delay);
        if(restore)
        {
            RestoreBlockSet(obstacle);
        }
        else
        {
            Main.resetCheck =  GenerateBlockSet(true);
        }
    }

}
