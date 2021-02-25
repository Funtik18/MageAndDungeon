using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobInterstitial : MonoBehaviour
{
	private InterstitialAd interstitial;

#if UNITY_ANDROID
	//test ca-app-pub-3940256099942544/1033173712
	//real ca-app-pub-7697745999229541/6081282293
	private const string interstitialId = "ca-app-pub-7697745999229541/6081282293";
#elif UNITY_IPHONE
	private const string interstitialId = "";
#else
	string interstitialId = "unexpected_platform";
#endif
	private void OnEnable()
	{
		RequestInterstitial();
	}

	private void RequestInterstitial()
	{
		interstitial = new InterstitialAd(interstitialId);
		AdRequest adRequest = new AdRequest.Builder().Build();
		interstitial.LoadAd(adRequest);

		interstitial.OnAdLoaded += InterstitialLoaded;
		interstitial.OnAdOpening += InterstitialOpened;
		interstitial.OnAdClosed += InterstitialClosed;
	}
	public void DestroyInterstitial()
	{
		interstitial.OnAdLoaded -= InterstitialLoaded;
		interstitial.OnAdOpening -= InterstitialOpened;
		interstitial.OnAdClosed -= InterstitialClosed;

		interstitial.Destroy();
	}

	public void ShowInterstitial()
	{
		if(interstitial.IsLoaded())
		{
			interstitial.Show();
		}
	}

	private void InterstitialLoaded(object sender, EventArgs e)
	{

	}
	private void InterstitialOpened(object sender, EventArgs e)
	{

	}
	private void InterstitialClosed(object sender, EventArgs e)
	{
		interstitial.OnAdLoaded -= InterstitialLoaded;
		interstitial.OnAdOpening -= InterstitialOpened;
		interstitial.OnAdClosed -= InterstitialClosed;

		RequestInterstitial();
	}


	private void OnDestroy()
	{
		DestroyInterstitial();
	}
}