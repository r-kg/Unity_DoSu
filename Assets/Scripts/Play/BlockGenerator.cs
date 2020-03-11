using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] GameObject block;
    private int obstacle = 0;
    private readonly float obstacleProb = 10f;
    private float prob = 10f;

    void Awake()
    {
        //block.transform.localScale = new Vector3(Constants.blockSize, Constants.blockSize, 1);
    }

    /// <summary>
    /// 게임 시작시 블럭 뿌려주는 함수
    /// </summary>
    /// <returns></returns>
    public bool GenerateBlockSet()
    {
        Main.blockList = null;
        Main.blockList = new List<GameObject>();
        Main.blockAnimatorList = new List<Animator>();

        int pivotX = (int)typeof(Constants).GetField("x" + Constants.size).GetValue(typeof(Constants));
        int pivotY = (int)typeof(Constants).GetField("y" + Constants.size).GetValue(typeof(Constants));
        float bSize = (float)typeof(Constants).GetField("blockSize" + Constants.size).GetValue(typeof(Constants));
        Constants.gap = (int)typeof(Constants).GetField("gap" + Constants.size).GetValue(typeof(Constants));
        block.transform.localScale = new Vector3(bSize, bSize, 1);

        for (int i = 0; i < Mathf.Pow(Constants.size, 2); i++)
        {
            GameObject blockObject = Instantiate(block, new Vector3(0, 0, 0), Quaternion.identity);
            Block blockScript = blockObject.GetComponent<Block>();
            blockScript.Coord.X = i % Constants.size;
            blockScript.Coord.Y = i / Constants.size;
            blockScript.Color = Constants.BlockType.Random;

            blockObject.name = "Block " + blockScript.Coord.GetIndex();
            blockObject.transform.position = new Vector3(pivotX + (Constants.gap * blockScript.Coord.X), pivotY - (Constants.gap * blockScript.Coord.Y), 0);
            Main.blockList.Add(blockObject);
            Main.blockAnimatorList.Add(blockScript.Animator);
            blockScript.Animator.SetTrigger("BlockIn");
        }

        Main.blockTarget.SetTargets(Constants.targetPool);
        return true;
    }

    /// <summary>
    /// Click 등으로 비활성화된 블럭들 활성화
    /// </summary>
    public bool RestoreBlockSet(bool flag)
    {
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

    public void DelayGenerate(float delay, bool obstacle)
    {
        StartCoroutine(DelayGenerator(delay,obstacle));
    }

    private IEnumerator DelayGenerator(float delay, bool obstacle)
    {
        yield return new WaitForSeconds(delay);
        RestoreBlockSet(obstacle);
    }
}
