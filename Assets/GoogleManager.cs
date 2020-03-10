using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GoogleManager : MonoSingleton<GoogleManager>
{

    public bool active;


    // Start is called before the first frame update
    void Awake()
    {   
        PlayGamesPlatform.InitializeInstance(new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
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
                
                }
                else
                {
                    Debug.Log("LogIn Failed");
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
            Social.ReportScore(score, GPGSIds.leaderboard_dosu_ranking,(bool success) =>
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
            Social.LoadScores(GPGSIds.leaderboard_dosu_ranking, (scores) =>{});
            Social.ShowLeaderboardUI();
        }
    }

    public void OnShowAchivementUI()
    {
        if(Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

}
