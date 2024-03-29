﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GoogleManager : MonoSingleton<GoogleManager>
{

    // Start is called before the first frame update
    void Awake()
    {   
        PlayGamesPlatform.InitializeInstance(new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        DontDestroyOnLoad(this);
        
    }
    void Start()
    {
        LogIn();
    }
    
    public void LogIn()
    {
        if(!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if(success)
                {   
                    Debug.Log("LogIn Success");
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

    public void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }

    public void ReportLeaderboardScore(long score)
    {
        if(Social.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_ranking,(bool success) =>
            {
                if(success)
                {
                    
                }
                else
                {

                }
            });
        }
    }

    public void ReportAchievements(string achievement_id)
    {
        Social.ReportProgress(achievement_id, 100.0, (bool success)=>{});
    }

    public bool isLogged()
    {
        return  Social.localUser.authenticated;
    }

    public string GetUserName()
    {
        return Social.localUser.userName;
    }

    public void OnShowLeaderboard()
    {
        if(Social.localUser.authenticated)
        {
            Social.LoadScores(GPGSIds.leaderboard_ranking, (scores) =>{});
            Social.ShowLeaderboardUI();
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if(success)
                {   
                    Debug.Log("LogIn Success");
                    ToastMessage.Instance.showAndroidToast("Play 게임 서비스에 로그인 합니다.");
                    Social.LoadScores(GPGSIds.leaderboard_ranking, (scores) =>{});
                    Social.ShowLeaderboardUI();
                }
                else
                {
                    Debug.Log("LogIn Failed");
                    ToastMessage.Instance.showAndroidToast("Play 게임 서비스 로그인 실패.");
                }
            });
        }
    }

    public void OnShowAchivementUI()
    {
        if(Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if(success)
                {   
                    Debug.Log("LogIn Success");
                    ToastMessage.Instance.showAndroidToast("Play 게임 서비스에 로그인 합니다.");
                    Social.ShowAchievementsUI();
                }
                else
                {
                    Debug.Log("LogIn Failed");
                    ToastMessage.Instance.showAndroidToast("Play 게임 서비스 로그인 실패.");
                }
            });
        }
    }
}
