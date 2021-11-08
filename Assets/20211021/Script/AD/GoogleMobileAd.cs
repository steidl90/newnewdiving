using System;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections.Generic;

public class GoogleMobileAd : Singleton<GoogleMobileAd>
{
    private InterstitialAd nextInterstitial;
    private InterstitialAd reStartInterstitial;

    public static readonly string interstitial1Id1 = "ca-app-pub-1195551850458243/1125818831";
    public static readonly string interstitial1Id2 = "ca-app-pub-1195551850458243/2949804889";

    public void Init()
    {
        //List<string> deviceIds = new List<string>();
        //deviceIds.Add("D3335E0DDB922E49F5B55B5B63E67854");
        //deviceIds.Add("6355DBB3179CEFA7D2305B1D67D59210");
        //RequestConfiguration requestConfiguration = new RequestConfiguration
        //    .Builder()
        //    .SetTestDeviceIds(deviceIds)
        //    .build();
        //MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => {
            ReStartRequestInterstitial();
            NextRequestInterstitial();
        });
    }

    public void NextRequestInterstitial()
    {
        if (nextInterstitial != null)
        {
            nextInterstitial.Destroy();
        }
        nextInterstitial = new InterstitialAd(interstitial1Id1);
        //nextInterstitial.OnAdLoaded += HandleOnAdLoaded;
        //nextInterstitial.OnAdOpening += HandleOnAdOpened;
        nextInterstitial.OnAdClosed += HandleOnAdClosedNext;

        AdRequest request = new AdRequest.Builder().Build();
        nextInterstitial.LoadAd(request);
    }
    public void ReStartRequestInterstitial()
    {
        if (reStartInterstitial != null)
        {
            reStartInterstitial.Destroy();
        }
        reStartInterstitial = new InterstitialAd(interstitial1Id2);
        //reStartInterstitial.OnAdLoaded += HandleOnAdLoaded;
        //reStartInterstitial.OnAdOpening += HandleOnAdOpened;
        reStartInterstitial.OnAdClosed += HandleOnAdClosedRe;

        AdRequest request2 = new AdRequest.Builder().Build();
        reStartInterstitial.LoadAd(request2);
    }

    public void NextStageAd()
    {
        Debug.Log(nextInterstitial.IsLoaded());
        if (nextInterstitial.IsLoaded())
        {
            nextInterstitial.Show();
        }
        else
        { 
            GameManager.gameManager.NextStage();
        }
    }

    public void ReStartAd()
    {
        Debug.Log(reStartInterstitial.IsLoaded());

        if (reStartInterstitial.IsLoaded())
        {
            reStartInterstitial.Show();
        }
        else
        {
            GameManager.gameManager.ReStart();
        }
    }

    //public void HandleOnAdLoaded(object sender, EventArgs args)
    //{
    //}

    //public void HandleOnAdOpened(object sender, EventArgs args)
    //{
    //}

    public void HandleOnAdClosedNext(object sender, EventArgs args)
    {
        NextRequestInterstitial();
        GameManager.gameManager.NextStage();
    }

    public void HandleOnAdClosedRe(object sender, EventArgs args)
    {
        ReStartRequestInterstitial();
        GameManager.gameManager.ReStart();
    }


}
