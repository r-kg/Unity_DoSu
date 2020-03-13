using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AchievementTracker : MonoBehaviour
{
    public int itemDoCount, itemSuCount;
    public int totalHit, totalMiss;

    private int finalScore;

    void Start()
    {
        
    }

    public void CheckAchievement()
    {
        if(Social.localUser.authenticated == false) return;

        finalScore = Main.blockTarget.Score;
        CheckScoreAchievement();
        CheckItemAchievement();
        CheckPerfectAchievement();
        CheckElseAchievement();
    }  

    private void CheckScoreAchievement()
    {
        if(finalScore >= 1000000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_1000000);
        }
        if(finalScore >= 100000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_100000);
        }
        if(finalScore >= 50000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_50000);
        }
        if(finalScore >= 25000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_25000);
        }
        if(finalScore >= 10000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_10000);
        }
        if(finalScore >= 5000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_5000);
        }
        if(finalScore == 0)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_im_more_of_a_dog_person);
        }

        GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_hello_dosu);
    }

    private void CheckItemAchievement()
    {
        if(itemDoCount + itemSuCount >= 30)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_please_stop);
        }
        if(itemDoCount + itemSuCount >= 20)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_meowmeowmeowmeow);
        }
        if(itemDoCount + itemSuCount >= 10)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_meowmeow);
        }
        if(itemDoCount + itemSuCount >= 5)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_meow);
        }


        //아이템 사용 x 업적
        if(itemDoCount + itemSuCount == 0 && finalScore >= 50000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_are_they_sleeping);
        }
        if(itemDoCount + itemSuCount == 0 && finalScore >= 30000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_the_silence_of_the_cats);
        }

        //한쪽 아이템만 사용 업적
        if(itemDoCount >= 1 && itemSuCount == 0 && finalScore >= 30000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_lets_go_do);
        }
        else if(itemSuCount >= 1 && itemDoCount == 0 && finalScore >= 30000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_lets_go_su);
        }
    }
    
    private void CheckPerfectAchievement()
    {
        if(totalMiss == 0 && finalScore >= 50000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_perfect_game_iii);
        }
        if(totalMiss == 0 && finalScore >= 40000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_perfect_game_ii);
        }
        if(totalMiss == 0 && finalScore >= 30000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_perfect__game_i);
        }
    }

    private void CheckElseAchievement()
    {
        
    }
}
