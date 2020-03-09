using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GoogleManager : MonoSingleton<GoogleManager>
{
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    public void ActivateGPGS()
    {
        PlayGamesPlatform.InitializeInstance(new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
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
            Social.ReportScore(score, GPGSIds.leaderboard_dosu_score_leaderboard,(bool success) =>
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

    public long LoadLeaderboardScore()
    {
        long bestScore = 0;
        ILeaderboard ib = PlayGamesPlatform.Instance.CreateLeaderboard();
        ib.id = GPGSIds.leaderboard_dosu_score_leaderboard;

        ib.LoadScores(scores =>
        {
            bestScore = ib.localUserScore.value;
        });

        return bestScore;
    }

    public void OnShowLeaderboard()
    {
        if(Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void OnShowAchivementUI()
    {
        Social.ShowAchievementsUI();
    }

}
