using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainClickEvent : MonoBehaviour
{
    private bool gameReady = true;
    public Animator settingAnim;
    public Animator CreditAnim;
    public GameObject UIHider;
    public Slider bgmSlider;
    public Slider effectSlider;
    [SerializeField] GameObject tutorial;

    [SerializeField] Sprite[] tutorialPages;

    private int tutorialPageNum = 0;

    private bool tutorialReview = false;

    // Start is called before the first frame update
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }

    public void PopupSetting()
    {
        AdManager.Instance.DisplayBanner();
        gameReady = false;
        settingAnim.SetTrigger("OpenSetting");
        UIHider.SetActive(false);
        effectSlider.value = SoundManager.Instance.effectPlayer.volume * 10;
        bgmSlider.value = SoundManager.Instance.bgmPlayer.volume * 10;
        SoundManager.Instance.UI();
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
        gameReady = false;
        UIHider.SetActive(false);
        AdManager.Instance.DisplayBanner();
        SoundManager.Instance.UI();
    }
    
    public void CloseCredit()
    {
        CreditAnim.SetTrigger("CloseSetting");
        gameReady = true;
        UIHider.SetActive(true);
        AdManager.Instance.HideBanner();
        SoundManager.Instance.UI();
    }


    public void ChangeBGMVolume()
    {
        SoundManager.Instance.bgmPlayer.volume = bgmSlider.value / 10;
    }
    
    public void ChangeEffectVolume()
    {
        SoundManager.Instance.effectPlayer.volume = effectSlider.value / 10;
        SoundManager.Instance.Click();
    }

    public void PopupTutorial()
    {
        tutorial.SetActive(true);
        tutorialReview = true;
        tutorialPageNum = 0;
        tutorial.GetComponent<Image>().sprite = tutorialPages[tutorialPageNum];
        SoundManager.Instance.UI();
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
