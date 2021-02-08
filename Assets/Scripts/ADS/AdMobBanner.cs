using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobBanner : MonoBehaviour
{
    private BannerView banner;

#if UNITY_ANDROID
	//test ca-app-pub-3940256099942544/6300978111
	//real ca-app-pub-7697745999229541/4168315269
	private const string bannerId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
	private const string bannerId = "";
#else
	string adUnitId = "unexpected_platform";
#endif
	private void OnEnable()
	{
		//AdSize size = new AdSize(AdSize.FullWidth, AdSize.Banner.Height);
		banner = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);
		AdRequest adRequest = new AdRequest.Builder().Build();
		banner.LoadAd(adRequest);
		StartCoroutine(ShowBanner());
	}

	private IEnumerator ShowBanner()
	{
		yield return new WaitForSecondsRealtime(0);
		banner.Show();
	}

	public void DestroyBanner()
	{
		banner.Destroy();
	}

	private void OnDestroy()
	{
		DestroyBanner();
	}
}