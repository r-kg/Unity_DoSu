using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainClickEvent : MonoBehaviour
{
    private bool gameReady = true;
    public Animator settingAnim, CreditAnim;
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
        settingAnim.SetTrigger("OpenSetting");
        UIHider.SetActive(false);
        effectSlider.value = SoundManager.Instance.effectPlayer.volume * 10;
        bgmSlider.value = SoundManager.Instance.bgmPlayer.volume * 10;
        SoundManager.Instance.UI();
        if(GoogleManager.Instance.isLogged())
        {
            gpButton.GetComponent<Image>().sprite  = logged;
            loggedUserName.text = "ID : " + GoogleManager.Instance.GetUserName();
        }
    }

    
    public void CloseSetting()
    {
        gameReady = true;
        settingAnim.SetTrigger("CloseSetting");
        UIHider.SetActive(true);
        AdManager.Instance.HideBanner();
        SoundManager.Instance.UI();
    }
    public void PopupCredit()
    {
        CreditAnim.SetTrigger("OpenSetting");
        SoundManager.Instance.UI();
        GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_thanks_for_playing);
    }
    
    public void CloseCredit()
    {
        CreditAnim.SetTrigger("CloseSetting");
        SoundManager.Instance.UI();
    }


    public void ChangeBGMVolume()
    {
        SoundManager.Instance.bgmPlayer.volume = bgmSlider.value / 10;
    }
    
    public void ChangeEffectVolume()
    {
        SoundManager.Instance.effectPlayer.volume = effectSlider.value / 10;
        SoundManager.Instance.effectSubPlayer.volume = effectSlider.value / 25;
        SoundManager.Instance.Click();
    }

    public void PopupTutorial()
    {
        tutorial.SetActive(true);
        tutorialReview = true;
        tutorialPageNum = 0;
        tutorial.GetComponent<Image>().sprite = tutorialPages[tutorialPageNum];
        SoundManager.Instance.UI();
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
                    loggedUserName.text = "ID : " + GoogleManager.Instance.GetUserName();
                
                }
                else
                {
                    Debug.Log("LogIn Failed");
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
                SoundManager.Instance.playBGM(SoundManager.Instance.bgmPlay);
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

        SoundManager.Instance.Swipe();
        tutorial.GetComponent<Image>().sprite = tutorialPages[tutorialPageNum];
    }

    public void TutorialBack()
    {
        tutorialPageNum--;
        if(tutorialPageNum < 0)
        {
            tutorialPageNum = 0;
        }

        SoundManager.Instance.Swipe();
        tutorial.GetComponent<Image>().sprite = tutorialPages[tutorialPageNum];
    }

    public void TutorialSkip()
    {
        tutorial.SetActive(false);
        PlayerPrefs.SetInt("Tutorial",1);
        PlayerPrefs.Save();
        SoundManager.Instance.UI();
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
