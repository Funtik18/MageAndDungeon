using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobManager : MonoBehaviour
{
	private void Awake()
	{
		MobileAds.Initialize(init => { });
	}
}