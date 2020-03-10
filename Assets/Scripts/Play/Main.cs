using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Sprite[] blockColors;
    [SerializeField] Sprite[] serializedBlockColors;
    public static List<GameObject> blockList;
    public static List<Animator> blockAnimatorList;
    public static bool resetCheck;
    public static int itemDoCount = 0, itemSuCount = 0, totalHit = 0, totalMiss = 0;
    private bool isPause;

    //Logic Classes
    public static BlockTarget blockTarget;
    public static BlockGenerator blockGenerator;
    public static BlockSlider blockSlider;
    public static BlockItem blockItem;
    public AchievementTracker achievementTracker;
    public static Timer timer;
    private TouchListener touchListener;

    public float timeLeft = 3.0f;
    public GameObject resultDlg;
    public GameObject CountdownImage;
    public Text resultScore;



    /// <summary>
    /// Constructor
    /// </summary>
    private void Awake()
    {
        //**Loading resources & Mapping objects**//
        blockColors = (Sprite[])serializedBlockColors.Clone();

        //**Generate class instances**//
        blockSlider = gameObject.AddComponent<BlockSlider>();
        blockItem = gameObject.AddComponent<BlockItem>();
        timer = gameObject.AddComponent<Timer>();
        touchListener = gameObject.AddComponent<TouchListener>();

        isPause = true;
        StartCoroutine(Countdown());
        
    }

    private void Start()
    {
        blockTarget = this.gameObject.GetComponent<BlockTarget>();
        blockGenerator = this.gameObject.GetComponent<BlockGenerator>();
        achievementTracker = this.gameObject.GetComponent<AchievementTracker>();
        resetCheck = blockGenerator.GenerateBlockSet();
    }

    void Update()
    {
        if (!isPause)
        {
            //test.GetTouch(resetCheck);
            touchListener.GetTouch();

            if (timer.End)
            {
                resetCheck = false;
                if(resultDlg.activeSelf==false){
                    resultDlg.SetActive(true);
                    resultScore.text = string.Format("{0:#,###0}", blockTarget.Score);
                    achievementTracker.CheckAchievement(itemDoCount, itemSuCount, totalHit, totalMiss);
                    GoogleManager.Instance.ReportLeaderboardScore(Main.blockTarget.Score);
                    AdManager.Instance.DisplayBanner();
                }
            }
        }
    }

    /// <summary>
    /// 게임 시작 3초 카운트 다운 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Countdown()
    {
        while (true)
        {
            SoundManager.Instance.Count();
            yield return new WaitForSeconds(1.0f);

            timeLeft--;
            if (timeLeft == 0)
            {
                SoundManager.Instance.Ding();
                yield return new WaitForSeconds(1.0f);
                CountdownImage.SetActive(false);
                isPause = false;
                timer.StartCoroutine(timer.TimeCoroutine());
                yield break;
            }
        }
    }
    
    /// <summary>
    /// 씬 전환
    /// </summary>
    public void LoadMain()
    {
        AdManager.Instance.HideBanner();
        AdManager.Instance.DisplayInterstitial();
        Initiate.Fade("Main",Color.white,1.2f);
        
    }

    public void Retry()
    {
        AdManager.Instance.HideBanner();
        AdManager.Instance.DisplayInterstitial();
        Initiate.Fade("Play",Color.white,1.2f);
    }

    public void ShowLeaderboard()
    {
        GoogleManager.Instance.OnShowLeaderboard();
    }

}

