using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class BlockTarget : MonoBehaviour
{
    private BlockTargetFormer blockTargetFormer;
    public Constants.BlockType TargetColor { get; set; }

    [SerializeField] Sprite[] phaseTextImage;

    public int targetform;
    public Image targetColor, targetForm, targetPhaseImage;
    private Animator targetColorAnim, targetFormAnim, targetPhaseAnim, scoreTextAnim;
    public int Score {get; set;}
    private Text scoreText, comboText;
    private int combo = 0;
    private GameObject comboImage;
    private Animator comboAnim;

    /// <summary>
    /// Constructor
    /// </summary>
    void Awake()
    {
        //coordinateForm = gameObject.AddComponent<CoordinateForm>();
        blockTargetFormer = gameObject.AddComponent<BlockTargetFormer>();
        targetColor = GameObject.Find("Canvas").transform.Find("TargetBalloon").transform.Find("TargetColor").gameObject.GetComponent<Image>();
        targetColorAnim = GameObject.Find("Canvas").transform.Find("TargetBalloon").transform.Find("TargetColor").gameObject.GetComponent<Animator>();
        targetForm = GameObject.Find("Canvas").transform.Find("TargetBalloon").transform.Find("TargetForm").gameObject.GetComponent<Image>();
        targetPhaseImage = GameObject.Find("Canvas").transform.Find("TargetBalloon").transform.Find("TargetPhase").gameObject.GetComponent<Image>();
        targetPhaseAnim = GameObject.Find("Canvas").transform.Find("TargetBalloon").transform.Find("TargetPhase").gameObject.GetComponent<Animator>();
        scoreText = GameObject.Find("CanvasUI").transform.Find("ScoreText").gameObject.GetComponent<Text>();
        scoreTextAnim = scoreText.GetComponent<Animator>();
        comboImage = GameObject.Find("CanvasUI").transform.Find("ComboImage").gameObject;
        comboText = comboImage.transform.Find("ComboText").gameObject.GetComponent<Text>();
        comboAnim = comboImage.GetComponent<Animator>();
        Score = 0;
    }

    /// <summary>
    /// Set target color with random pool size of parameter n 
    /// Set target form randomly
    /// </summary>
    /// <param name="n">Block type 후보군 크기</param>
    public void SetTargets(int n)
    {
        Dictionary<Constants.BlockType, int> dict = new Dictionary<Constants.BlockType, int>();
        List<Constants.BlockType> sortedList;

        for(int i = 0; i < Constants.range; i++)
        {
            dict.Add((Constants.BlockType)i, 0);
        }

        foreach(GameObject item in Main.blockList)
        {
            Block block = item.GetComponent<Block>();
            if((int)block.Color < Constants.range)
            {
                dict[block.Color]++;
            }
        }

        var sortedDict = from pair in dict
                    orderby pair.Value descending
                    select pair;

        sortedList = sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value).Keys.ToList();

        if (n > Constants.range) n = Constants.range;
        TargetColor = sortedList[Random.Range(0, n)];
        targetColor.sprite = Main.blockColors[(int)TargetColor];
        targetColorAnim.SetTrigger("colorChange");

        blockTargetFormer.SetTargetFormNumSprite(targetForm);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selectedBlock"></param>
    /// <param name="touchWorldPos"></param>
    /// <returns></returns>
    public int CalculateTargets(GameObject selectedBlock, Vector2 touchWorldPos)
    {
        List<Coordinate> targetCoords = new List<Coordinate>();
        int hitCount = 0, exceedCount = 0, missCount = 0;

        blockTargetFormer.Pivot = selectedBlock.GetComponent<Block>().Coord;
        this.SendMessage("Phase"+Constants.blockPhase+"Form" + blockTargetFormer.targetFormNumber, targetCoords);

        for (int i = 0; i < Mathf.Pow(Constants.size,2); i++)
        {
            Block tempClass = Main.blockList[i].GetComponent<Block>();

            for (int j = 0; j < targetCoords.Count; j++)
            {
                if (tempClass.Coord.CompareCoord(targetCoords[j]))
                {
                    //Tile on target coords 
                    if (tempClass.Color == TargetColor || tempClass.Color == Constants.BlockType.LOCK)
                    {
                        //Tile has color match
                        //or destroy obstacle
                        if (tempClass.Color == Constants.BlockType.LOCK)
                        {
                            break;
                        }

                        tempClass.Destroy("Pop",0.41f);
                        hitCount++;
                        
                        if (targetCoords[j].Exceed)
                        {
                            //Tile has color match with exceed coord
                            exceedCount++;
                        }//if
                    }
                    else
                    {
                        //Tile has no color match
                        missCount++;
                        tempClass.Destroy("Destroy",0.5f);
                    }

                    break;

                }//if
            }//for
        }

        SetScore(hitCount, missCount);
        SetCombo(hitCount, missCount, touchWorldPos);
        //SoundManager.Instance.Click();

        return hitCount - missCount;
    }

    public void HintTarget(GameObject selectedBlock, bool flag)
    {
        if(!selectedBlock) return;
        if(!selectedBlock.tag.Equals("Block")) return;
        
        List<Coordinate> targetCoords = new List<Coordinate>();
        //coordinateForm.Pivot = selectedBlock.GetComponent<Block>().Coord;
        blockTargetFormer.Pivot = selectedBlock.GetComponent<Block>().Coord;
        this.SendMessage("Phase"+Constants.blockPhase+"Form" + targetform, targetCoords);

        for (int i = 0; i < Mathf.Pow(Constants.size,2); i++)
         {
            Block tempClass = Main.blockList[i].GetComponent<Block>();

            for (int j = 0; j < targetCoords.Count; j++)
            {
                if (tempClass.Coord.CompareCoord(targetCoords[j]))
                {
                    tempClass.Animator.SetBool("Pressed",flag);
                }
            }
         }
    }
   

    /// <summary>
    /// Set Score
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="miss"></param>
    private void SetScore(int hit, int miss)
    {
        if(hit > miss)
        {
            Score += (int)(Mathf.Pow(hit, 2) * 50);
            scoreTextAnim.SetTrigger("ScorePlus");
            Main.timer.timeBarAnim.SetTrigger("TimePlus");
        }
        else
        {
            Main.timer.timeBarAnim.SetTrigger("TimeMinus");
        }

        SetDifficulty();
        scoreText.text = string.Format("{0:#,###0}", Score);
        Main.timer.SetTimeDifficulty(Score);


    }

    /// <summary>
    /// Set combo
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="miss"></param>
    /// <param name="pos"></param>
    private void SetCombo(int hit, int miss, Vector2 pos)
    {
        comboImage.transform.position = pos;

        if(hit / (float)(hit + miss) >= 0.6f)
        {
            SoundManager.Instance.Click();
            combo++;
            Main.blockItem.itemDo.Animator.SetTrigger("DoSmile");
            Main.blockItem.itemSoo.Animator.SetTrigger("SooSmile");
            Main.blockItem.itemDo.TryGenerate();
            Main.blockItem.itemSoo.TryGenerate();

            if (combo >= 2)
            {
                comboAnim.SetTrigger("ShowCombo");
            }
        }
        else
        {
            combo = 0;
            Main.blockItem.itemDo.Animator.SetTrigger("DoSad");
            Main.blockItem.itemSoo.Animator.SetTrigger("SooSad");
            comboAnim.SetTrigger("ShowComboFailed");
            SoundManager.Instance.Scratch();
        }
        comboText.text = combo + "";
    }
    
    private void SetDifficulty()
    {
        if(Score >= 60000)
        {
            Constants.obsRange = 2;
            Constants.blockPhase  = Random.Range(5,8);
        }
        else if(Score >= 50000)
        {
            Constants.range = 5;
            Constants.blockPhase  = Random.Range(5,8);
        }
        else if(Score >= 40000)
        {
            Constants.range = 4;
            Constants.blockPhase  = Random.Range(5,8);
        }
        else if(Score >= 35000)
        {
            Constants.obsRange = 1;
            Constants.blockPhase = 7;
        }
        else if(Score >= 30000)
        {
            Constants.range = 5;
            Constants.blockPhase = 6;
        }
        else if(Score >= 25000)
        {   
            if(Constants.blockPhase == 4){
                targetPhaseImage.sprite = phaseTextImage[2];
                targetPhaseAnim.SetTrigger("PhaseChange");
            } 
            Constants.range = 4;
            Constants.blockPhase = 5;
        }
        else if(Score >= 20000)
        {
            Constants.range = 5;
            Constants.blockPhase = 4;
        }
        else if(Score >= 15000)
        {
            Constants.range = 4;
            Constants.obsRange = 1;
            Constants.blockPhase = 3;
        }
        else if(Score >= 10000)
        {
            Constants.range = 5;
        }
        else if(Score >= 4500)
        {
            if(Constants.blockPhase == 1)
            {
                targetPhaseImage.sprite = phaseTextImage[1];
                targetPhaseAnim.SetTrigger("PhaseChange");
            }
            Constants.blockPhase = 2;
            Constants.targetPool = 2;
        }
        else if(Score >= 0)
        {
            Constants.blockPhase = 1;
            Constants.targetPool = 3;
        }
        
    }
}
