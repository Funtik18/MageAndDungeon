using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobManager : MonoBehaviour
{
	private static AdMobManager instance;
	public static AdMobManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<AdMobManager>();
			}
			return instance;
		}
	}

	public AdMobBanner adMobBanner;
	public AdMobInterstitial adMobInterstitial;
	public AdMobRewarded adMobRewarded;

	private void Awake()
	{
		MobileAds.Initialize(init => { });
	}
}