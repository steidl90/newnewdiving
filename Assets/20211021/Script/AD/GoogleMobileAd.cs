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
        List<string> deviceIds = new List<string>();
        deviceIds.Add("D3335E0DDB922E49F5B55B5B63E67854");
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

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
        nextInterstitial.OnAdLoaded += HandleOnAdLoaded;
        nextInterstitial.OnAdOpening += HandleOnAdOpened;
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
        reStartInterstitial.OnAdLoaded += HandleOnAdLoaded;
        reStartInterstitial.OnAdOpening += HandleOnAdOpened;
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
        print("���� ��û �Ϸ�");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        print("���� ���� �Ϸ�");
    }

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
