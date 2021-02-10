﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WindowFailGame : MonoBehaviour
{
	public PanelInteraction interaction;
	public Button btnReward;

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
		interaction.onTap = delegate
			{
				SceneLoaderManager.Instance.AllowLoadScene();
			};
		btnReward.onClick.AddListener(delegate {
			StopOpenWindow();
			AdMobManager.Instance.adMobInterstitial.ShowInterstitial();
		});
	}

	public void StartOpenWindow()
	{
		if(!IsWindowProcess)
		{
			windowCoroutine = StartCoroutine(Open());
		}
	}
	private IEnumerator Open()
	{
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
	private void StopOpenWindow()
	{
		if(IsWindowProcess)
		{
			StopCoroutine(windowCoroutine);
			windowCoroutine = null;
		}
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
	private void CloseWindow()
	{
		Fader.CanvasGroup.alpha = 0;
		Fader.CanvasGroup.interactable = false;
		Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
}
