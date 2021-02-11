using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobRewarded : MonoBehaviour
{
    private RewardedAd rewarded;
    private RewardBasedVideoAd rewardedVideo;

#if UNITY_ANDROID
	//test ca-app-pub-3940256099942544/5224354917
	//real ca-app-pub-7697745999229541/5889710607
	private const string rewardedId = "ca-app-pub-7697745999229541/5889710607";
#elif UNITY_IPHONE
	private const string bannerId = "";
#else
	string adUnitId = "unexpected_platform";
#endif

	private void OnEnable()
	{
		rewardedVideo = RewardBasedVideoAd.Instance;
	}

	public void RequestRewardVideo()
	{
		AdRequest adRequest = new AdRequest.Builder().Build();
		rewardedVideo.LoadAd(adRequest, rewardedId);

		rewardedVideo.OnAdLoaded += RewardedVideoLoaded;
		rewardedVideo.OnAdOpening += RewardedVideoOpening;
		rewardedVideo.OnAdRewarded += RewardedVideoRewarded;
		rewardedVideo.OnAdClosed += RewardedVideoClosed;
	}

	private void ShowRewardVideo()
	{
		if(rewardedVideo.IsLoaded())
		{
			rewardedVideo.Show();
		}
	}

	private void RewardedVideoLoaded(object sender, System.EventArgs e)
	{
		ShowRewardVideo();
	}
	private void RewardedVideoOpening(object sender, System.EventArgs e)
	{
	}
	private void RewardedVideoClosed(object sender, System.EventArgs e)
	{
	}

	private void RewardedVideoRewarded(object sender, Reward e)
	{
	}
}