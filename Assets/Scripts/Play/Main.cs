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
    public static int itemDoCount, itemSuCount, totalHit, totalMiss;
    private bool isPause;
    [SerializeField] GameObject[] pauseHider;
    [SerializeField] GameObject pausePopup;

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

    private int clickCount;



    /// <summary>
    /// Constructor
    /// </summary>
    private void Awake()
    {
        //**Loading resources & Mapping objects**//
        blockColors = (Sprite[])serializedBlockColors.Clone();

        //**Generate class instances**//
        //blockSlider = gameObject.AddComponent<BlockSlider>();
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
        blockSlider = this.gameObject.GetComponent<BlockSlider>();
        achievementTracker = this.gameObject.GetComponent<AchievementTracker>();
        resetCheck = blockGenerator.GenerateBlockSet(true);
        itemDoCount = itemSuCount = totalHit = totalMiss = 0;
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
                    achievementTracker.itemDoCount = Main.itemDoCount;
                    achievementTracker.itemSuCount = Main.itemSuCount;
                    achievementTracker.totalHit = Main.totalHit;
                    achievementTracker.totalMiss = Main.totalMiss;
                    resultDlg.SetActive(true);
                    resultScore.text = string.Format("{0:#,###0}", blockTarget.Score);
                    GoogleManager.Instance.LogIn();
                    achievementTracker.CheckAchievement();
                    GoogleManager.Instance.ReportLeaderboardScore(Main.blockTarget.Score);
                    AdManager.Instance.DisplayBanner();
                    ResetValues();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(timer.End || timeLeft > 0)
            {
                Initiate.Fade("Main",Color.white,1.2f);
            }
            else
            {
                PauseGame();
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

                //Main.blockGenerator.DestroyBlockSet();


                yield break;
            }
        }
    }
    
    /// <summary>
    /// 씬 전환
    /// </summary>
    public void LoadMain()
    {
        ResetValues();
        AdManager.Instance.HideBanner();
        AdManager.Instance.DisplayInterstitial();
        Initiate.Fade("Main",Color.white,1.2f);
        
    }

    public void Retry()
    {
        ResetValues();
        AdManager.Instance.HideBanner();
        AdManager.Instance.DisplayInterstitial();
        Initiate.Fade("Play",Color.white,1.2f);
    }

    public void ShowLeaderboard()
    {
        GoogleManager.Instance.OnShowLeaderboard();
    }

    public void PauseGame()
    {

        if(Main.timer.isPause)
        { 
            //일시정지 해제
            Main.resetCheck = true;
            Main.timer.isPause = false;
            AdManager.Instance.HideBanner();
            pausePopup.SetActive(false);
            foreach(GameObject hider in pauseHider)
            {
                hider.SetActive(true);
            }
            ToastMessage.Instance.CancelToast();
        }
        else
        {
            //일시정지
            Main.resetCheck = false;
            Main.timer.isPause = true;
            AdManager.Instance.DisplayBanner();
            pausePopup.SetActive(true);
            foreach(GameObject hider in pauseHider)
            {
                hider.SetActive(false);
            }
            ToastMessage.Instance.showAndroidToast("'뒤로'버튼을 한번 더 누르시면 게임을 재개합니다.");
        }
    }

    public static void ResetValues()
    {
        Constants.size = 5;
        Constants.blockPhase = 1;
        Constants.targetPool = 3;
        Constants.obsRange = 0;
        Constants.range = 4;
        itemDoCount = itemSuCount = totalHit = totalMiss = 0;
    }


    void DoubleClick()
    {
        clickCount = 0;
    }

}

