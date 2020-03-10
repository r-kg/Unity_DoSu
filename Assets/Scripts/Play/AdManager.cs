using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoSingleton<AdManager>
{
    private string APP_ID = "ca-app-pub-2751306043506296~4464715852";

    public bool acitve;
    private BannerView bannerAd;
    private InterstitialAd interstitialAd;

    private RewardBasedVideoAd rewardBasedVideoAd;


    void Awake()
    {
        //when you publis your app
        //RequestInterstitial();
        MobileAds.Initialize(APP_ID);
        
        this.rewardBasedVideoAd = RewardBasedVideoAd.Instance;
        rewardBasedVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;


        RequestBanner();
        RequestInterstitial();
        RequestRewardBasedVideoAd();
    }


    void RequestBanner()
    {
        string bannerId = "ca-app-pub-3940256099942544/6300978111";
        //bannerAd = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Bottom);
        bannerAd = new BannerView(bannerId, AdSize.Banner,AdPosition.Bottom);

        //for publish
        //AdRequest adRequest = new AdRequest.Builder().Build();
        
        //for testing
        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        bannerAd.LoadAd(adRequest);
        bannerAd.Hide();
    }

    void RequestInterstitial()
    {
        string interstitialId = "ca-app-pub-3940256099942544/1033173712";
        interstitialAd = new InterstitialAd(interstitialId);

        //for publish
        //AdRequest adRequest = new AdRequest.Builder().Build();
        
        //for testing
        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        interstitialAd.LoadAd(adRequest);
        
    }

    void RequestRewardBasedVideoAd()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";

        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        this.rewardBasedVideoAd.LoadAd(adRequest, adUnitId);
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideoAd();
    }


    public void DisplayBanner()
    {
        bannerAd.Show();
    }
    public void HideBanner()
    {
        bannerAd.Hide();
    }
    public void DisplayInterstitial()
    {
        interstitialAd.Show();
    }
    public void DisplayRewardBasedVideo()
    {
        if(rewardBasedVideoAd.IsLoaded())
        {
            rewardBasedVideoAd.Show();
        }
    }
    
}
