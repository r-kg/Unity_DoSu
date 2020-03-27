using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AchievementTracker : MonoBehaviour
{
    public void CheckAchievement()
    {
        if(Social.localUser.authenticated == false) return;

        CheckScoreAchievement();
        CheckItemAchievement();
        CheckPerfectAchievement();
    }  

    private void CheckScoreAchievement()
    {
        if(Main.blockTarget.Score >= 1000000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_1000000);
        }
        if(Main.blockTarget.Score >= 100000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_100000);
        }
        if(Main.blockTarget.Score >= 50000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_50000);
        }
        if(Main.blockTarget.Score >= 25000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_25000);
        }
        if(Main.blockTarget.Score >= 10000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_10000);
        }
        if(Main.blockTarget.Score >= 5000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_score_5000);
        }
        if(Main.blockTarget.Score == 0)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_im_more_of_a_dog_person);
        }

        GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_hello_dosu);
    }

    private void CheckItemAchievement()
    {
        if(Main.itemDoCount + Main.itemSuCount >= 30)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_please_stop);
        }
        if(Main.itemDoCount + Main.itemSuCount >= 20)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_meowmeowmeowmeow);
        }
        if(Main.itemDoCount + Main.itemSuCount >= 10)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_meowmeow);
        }
        if(Main.itemDoCount + Main.itemSuCount >= 5)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_meow);
        }


        //아이템 사용 x 업적
        if(Main.itemDoCount + Main.itemSuCount == 0 && Main.blockTarget.Score >= 65000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_are_they_sleeping);
        }
        if(Main.itemDoCount + Main.itemSuCount == 0 && Main.blockTarget.Score >= 40000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_the_silence_of_the_cats);
        }

        //한쪽 아이템만 사용 업적
        if(Main.itemDoCount >= 1 && Main.itemSuCount == 0 && Main.blockTarget.Score >= 30000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_lets_go_do);
        }
        else if(Main.itemSuCount >= 1 && Main.itemDoCount == 0 && Main.blockTarget.Score >= 30000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_lets_go_su);
        }
    }
    
    private void CheckPerfectAchievement()
    {
        if(Main.totalMiss == 0 && Main.blockTarget.Score >= 50000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_perfect_game_iii);
        }
        if(Main.totalMiss == 0 && Main.blockTarget.Score >= 40000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_perfect_game_ii);
        }
        if(Main.totalMiss == 0 && Main.blockTarget.Score >= 30000)
        {
            GoogleManager.Instance.ReportAchievements(GPGSIds.achievement_perfect__game_i);
        }
    }
}
