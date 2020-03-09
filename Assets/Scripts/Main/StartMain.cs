using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMain : MonoBehaviour
{
    [SerializeField] AudioClip[] bgm;
    [SerializeField] AudioClip[] catAudio;
    [SerializeField] AudioClip[] soundEffects;
    [SerializeField] Camera mainCamera;

    private int count;
    private float exit_time;
    
    void Update()
    {
        //Quit();
    }
    void Start()
    {
        //mainCamera.orthographicSize = (Screen.height / (Screen.width / 16.0f)) / 9.0f; 
        Screen.SetResolution(Screen.height * 9 / 16, Screen.height, true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //Audio Initailize
        LoadAudioClips();
        SoundManager.Instance.playBGM(SoundManager.Instance.bgmMain);
        SoundManager.Instance.bgmPlayer.volume = 0.4f;



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
    }

    private void Quit()
    {
        if(Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            count++;
        }
        if(count==2)
        {
            Application.Quit();
        }

        if(exit_time>1)
        {
            count = 0; 
            exit_time = 0;
        }
        else
        {
            exit_time += Time.deltaTime;
        }
    }
}
