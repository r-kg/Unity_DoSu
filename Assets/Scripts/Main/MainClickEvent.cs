using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainClickEvent : MonoBehaviour
{
    private bool gameReady = true;
    
    public GameObject settingPopup, creditPopup;
    public GameObject UIHider;
    public Slider bgmSlider, effectSlider;
    [SerializeField] GameObject tutorial;
    [SerializeField] Sprite[] tutorialPages;

    [SerializeField] GameObject gpButton;
    [SerializeField] Sprite logged, login;
    [SerializeField] Text loggedUserName;

    private int tutorialPageNum = 0;
    private bool tutorialReview = false;


    //Tutorial
    [SerializeField] GameObject tutorialObject;
    [SerializeField] Animator tutorialAnimator;

    public void ViewTutorial()
    {
        tutorialObject.SetActive(true);
    }

    public void TutorialNext()
    {
        tutorialAnimator.SetTrigger("next");
        if(tutorialAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tutorial_7"))
        {
            tutorialObject.SetActive(false);
            if(PlayerPrefs.GetInt("fp") == 1)
            {
                PlayerPrefs.SetInt("fp",0);
                PlayerPrefs.Save();
                LoadMain();
            }
        }
    }
    public void TutorialPrev()
    {
        tutorialAnimator.SetTrigger("prev");
    }

    public void PopupSetting()
    {
        AdManager.Instance.DisplayBanner();
        gameReady = false;
        settingPopup.SetActive(true);
        UIHider.SetActive(false);
        effectSlider.value = SoundManager.Instance.effectPlayer.volume * 10;
        bgmSlider.value = SoundManager.Instance.bgmPlayer.volume * 10;
        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_ui);
        if(GoogleManager.Instance.isLogged())
        {
            gpButton.GetComponent<Image>().sprite  = logged;
            loggedUserName.text = GoogleManager.Instance.GetUserName();
        }
    }

    
    public void CloseSetting()
    {
        gameReady = true;
        settingPopup.SetActive(false);
        UIHider.SetActive(true);
        AdManager.Instance.HideBanner();
        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_ui);
    }
    public void PopupCredit()
    {
        creditPopup.SetActive(true);
        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_ui);
        GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_thanks_for_playing);
    }
    
    public void CloseCredit()
    {
        creditPopup.SetActive(false);
        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_ui);
    }


    public void ChangeBGMVolume()
    {
        SoundManager.Instance.bgmPlayer.volume = bgmSlider.value / 10;
    }
    
    public void ChangeEffectVolume()
    {
        SoundManager.Instance.effectPlayer.volume = effectSlider.value / 10;
        SoundManager.Instance.effectSubPlayer.volume = effectSlider.value / 25;
        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_click);
    }

    public void InteractGP()
    {
        if(GoogleManager.Instance.isLogged())
        {
            GoogleManager.Instance.LogOut();
            gpButton.GetComponent<Image>().sprite = login;
            loggedUserName.text = "";
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if(success)
                {   
                    gpButton.GetComponent<Image>().sprite = logged;
                    loggedUserName.text = GoogleManager.Instance.GetUserName();
                    ToastMessage.Instance.showAndroidToast("Play 게임 서비스에 로그인 합니다.");
                
                }
                else
                {
                    Debug.Log("LogIn Failed");
                    ToastMessage.Instance.showAndroidToast("Play 게임 서비스 로그인 실패.");
                }
            });

            
        }
    }

    public void ShowLeaderboard()
    {
        GoogleManager.Instance.OnShowLeaderboard();

    }

    public void ShowAchivement()
    {
        GoogleManager.Instance.OnShowAchivementUI();
    }

    public void ShowExtraAd()
    {
        AdManager.Instance.DisplayRewardBasedVideo();
    }


    public void LoadMain()
    {
        if (gameReady)
        {
            if(PlayerPrefs.GetInt("fp") == 1)
            {
                ViewTutorial();
            }
            else
            {
                Initiate.Fade("Play",Color.white,1.0f);
                SoundManager.Instance.PlayBGM(SoundManager.Instance.bgm_Play);
            }
        }
    }

    void OnEnable()
    {
        UIHider.SetActive(true);
    }
}
