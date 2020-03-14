using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMain : MonoBehaviour
{
    [SerializeField] AudioClip[] bgm;
    [SerializeField] AudioClip[] catAudio;
    [SerializeField] AudioClip[] soundEffects;

    [SerializeField] AudioClip[] soundSub;
    [SerializeField] Camera mainCamera;

    private int count;
    private int clickCount;
    private float exit_time;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            clickCount++;
            ToastMessage.Instance.showAndroidToast("'뒤로'버튼을 한번 더 누르시면 종료됩니다.");
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.5f);
       
        }
        else if (clickCount == 2)
        {
            CancelInvoke("DoubleClick");
            SoundManager.Instance.bgmPlayer.Pause();
            SoundManager.Instance.effectPlayer.Pause();
            Quit();

        }
    }

    void Start()
    {
        //mainCamera.orthographicSize = (Screen.height / (Screen.width / 16.0f)) / 9.0f; 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //GPGS
        GoogleManager.Instance.active = true;
        AdManager.Instance.acitve = true;

        //Audio Initailize
        LoadAudioClips();
        SoundManager.Instance.playBGM(SoundManager.Instance.bgmMain);
        SoundManager.Instance.bgmPlayer.volume = 0.4f;
        SoundManager.Instance.effectPlayer.volume = 0.5f;
        
        SoundManager.Instance.effectSubPlayer.volume = 0.3f;

        //Score\
        if(PlayerPrefs.HasKey("Score") == false)
        {
            PlayerPrefs.SetInt("Score",0);
            PlayerPrefs.Save();
        }

    }

    private void LoadAudioClips()
    {
        SoundManager.Instance.bgmMain = bgm[0];
        SoundManager.Instance.bgmPlay = bgm[1];

        SoundManager.Instance.doMeow0 = catAudio[0];
        SoundManager.Instance.doMeow1 = catAudio[1];

        SoundManager.Instance.click = soundEffects[0];
        SoundManager.Instance.slide = soundEffects[1];
        SoundManager.Instance.scratch = soundEffects[2];
        SoundManager.Instance.count = soundEffects[3];
        SoundManager.Instance.ding = soundEffects[4];
        SoundManager.Instance.ui = soundEffects[5];
        SoundManager.Instance.swipe = soundEffects[6];

        SoundManager.Instance.genSound = soundSub[0];
    }
    void DoubleClick()
    {
        clickCount = 0;
    }

    public static void Quit()
    {
    AndroidJavaClass ajc = new AndroidJavaClass("com.lancekun.quit_helper.AN_QuitHelper");
    AndroidJavaObject UnityInstance = ajc.CallStatic<AndroidJavaObject>("Instance");
    UnityInstance.Call("AN_Exit");
    }   
}
