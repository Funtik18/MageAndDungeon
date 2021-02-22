using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WindowFailGame : MonoBehaviour
{
    public UnityAction onRewarded;

    public PanelInteraction interaction;
    public Button btnReward;
    public TMPro.TextMeshProUGUI txt;

    private Fader fader;
    public Fader Fader
    {
        get
        {
            if(fader == null)
            {
                fader = GetComponent<Fader>();
            }
            return fader;
        }
    }

    private Coroutine windowCoroutine = null;
    public bool IsWindowProcess => windowCoroutine != null;

	private void Awake()
	{
        interaction.onTap.AddListener(delegate { SceneLoaderManager.Instance.AllowLoadScene(); });
    }

    public void StartOpenWindow()
    {
        if(!IsWindowProcess)
        {

#if UNITY_EDITOR
            btnReward.onClick.AddListener(onRewarded);
#else
            txt.text = "Continue?";
            btnReward.onClick.AddListener(RequestReward);
#endif
            windowCoroutine = StartCoroutine(Open());
        }
    }
    private IEnumerator Open()
    {
        btnReward.interactable = true;

        SceneLoaderManager.Instance.LoadLevelsMap();

        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
        Fader.FadeIn();

        while(Fader.IsFadeProcess)
        {
            yield return null;
        }

        StopOpenWindow();
    }
    public void StopOpenWindow()
    {
        if(IsWindowProcess)
        {
            StopCoroutine(windowCoroutine);
            windowCoroutine = null;
        }
    }

    private void RequestReward()
	{
        AdMobManager.Instance.adMobRewarded.RequestRewardVideo();

        AdMobManager.Instance.adMobRewarded.onClossed = LeftAds;
        AdMobManager.Instance.adMobRewarded.onLeft = LeftAds;
        AdMobManager.Instance.adMobRewarded.onRewarded = onRewarded;
        AdMobManager.Instance.adMobRewarded.onLoaded = ShowReward;
        btnReward.interactable = false;
        txt.text = "Loading...";

        btnReward.onClick.RemoveAllListeners();
    }

    private void ShowReward()
	{
        Debug.Log("Show");
        btnReward.interactable = true;

        Time.timeScale = 0;

        AdMobManager.Instance.adMobRewarded.ShowRewardVideo();
    }

    private void LeftAds()
	{
        txt.text = "Continue?";
        btnReward.interactable = true;
        btnReward.onClick.AddListener(RequestReward);
    }

    [ContextMenu("OpenWindow")]
    private void OpenWindow()
    {
        Fader.CanvasGroup.alpha = 1;
        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
    [ContextMenu("CloseWindow")]
    public void CloseWindow()
    {
        Fader.CanvasGroup.alpha = 0;
        Fader.CanvasGroup.interactable = false;
        Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
}
