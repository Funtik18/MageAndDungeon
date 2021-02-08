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
				DontDestroyOnLoad(instance);
				MobileAds.Initialize(init => { });
			}
			return instance;
		}
	}

	private void Awake()
	{
		//if(Instance) { }
	}
}