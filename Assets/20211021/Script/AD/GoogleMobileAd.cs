using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;


public class GoogleMobileAd : MonoBehaviour
{
    private InterstitialAd nextInterstitial;
    private InterstitialAd reStartInterstitial;

    public static readonly string interstitial1Id = "ca-app-pub-1195551850458243/1125818831";

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestInterstitial()
    {
        if (nextInterstitial != null)
        {
            nextInterstitial.Destroy();
        }
        nextInterstitial = new InterstitialAd(interstitial1Id);
        nextInterstitial.OnAdClosed += HandleOnAdClosedNext;

        AdRequest request = new AdRequest.Builder().Build();
        nextInterstitial.LoadAd(request);

        
    }
    public void RequestInterstitialAd()
    {
        if (reStartInterstitial != null)
        {
            reStartInterstitial.Destroy();
        }
        reStartInterstitial = new InterstitialAd(interstitial1Id);
        reStartInterstitial.OnAdClosed += HandleOnAdClosedRe;

        AdRequest request2 = new AdRequest.Builder().Build();
        reStartInterstitial.LoadAd(request2);
    }

    public void NextStageAd()
    {
        if (nextInterstitial.IsLoaded())
        {
            nextInterstitial.Show();
        }
    }

    public void ReStartAd()
    {
        if (reStartInterstitial.IsLoaded())
        {
            reStartInterstitial.Show();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosedNext(object sender, EventArgs args)
    {
        GameManager.gameManager.NextStage();
    }

    public void HandleOnAdClosedRe(object sender, EventArgs args)
    {
        GameManager.gameManager.ReStart();
    }
}
