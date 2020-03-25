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

    public void PopupTutorial()
    {
        tutorial.SetActive(true);
        tutorialReview = true;
        tutorialPageNum = 0;
        tutorial.GetComponent<Image>().sprite = tutorialPages[tutorialPageNum];
        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_ui);
        AdManager.Instance.DisplayBanner();
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
        if (gameReady && tutorial.activeSelf == false)
        {
            if(PlayerPrefs.HasKey("Tutorial") == false)
            {
                tutorial.SetActive(true);
                tutorialPageNum = 0;
                tutorial.GetComponent<Image>().sprite = tutorialPages[tutorialPageNum];
            }
            else
            {
                AdManager.Instance.HideBanner();
                Initiate.Fade("Play",Color.white,1.0f);
                SoundManager.Instance.PlayBGM(SoundManager.Instance.bgm_Play);
                //SoundManager.Instance.DoMeow();
            }
        }
    }


    public void TutorialNext()
    {
        tutorialPageNum++;
        if(tutorialPageNum >= tutorialPages.Length)
        {
            tutorial.SetActive(false);
            AdManager.Instance.HideBanner();
            PlayerPrefs.SetInt("Tutorial",1);
            PlayerPrefs.Save();

            if(tutorialReview == false)
            {
                gameReady = true;
                LoadMain();
                return;
            }
            else
            {
                tutorialReview = false;
                return;
            }
        }

        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_swipe);
        tutorial.GetComponent<Image>().sprite = tutorialPages[tutorialPageNum];
    }

    public void TutorialBack()
    {
        tutorialPageNum--;
        if(tutorialPageNum < 0)
        {
            tutorialPageNum = 0;
        }

        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_swipe);
        tutorial.GetComponent<Image>().sprite = tutorialPages[tutorialPageNum];
    }

    public void TutorialSkip()
    {
        tutorial.SetActive(false);
        PlayerPrefs.SetInt("Tutorial",1);
        PlayerPrefs.Save();
        SoundManager.Instance.PlayEffect(SoundManager.Instance.effect_ui);
        AdManager.Instance.HideBanner();
        
        if(tutorialReview == false)
        {
            LoadMain();
            return;
        }
        else
        {
            tutorialReview = false;
            return;
        }
    }

}
