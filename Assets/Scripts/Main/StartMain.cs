using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMain : MonoBehaviour
{
    private int clickCount;

    
    void Update()
    {
        EscapeApp();
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //Score\
        if(PlayerPrefs.HasKey("fp") == false)
        {
            PlayerPrefs.SetInt("fp",1);
            PlayerPrefs.Save();
        }
    }

    void EscapeApp()
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
