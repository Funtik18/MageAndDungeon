using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WindowStartGame : MonoBehaviour
{
	public PanelInteraction interaction;

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

	public UnityAction onClosed;

	private Coroutine windowCoroutine = null;
	public bool IsWindowProcess => windowCoroutine != null;

	private void Awake()
	{
		interaction.onTap.AddListener(Tap);
	}

	private void Tap()
	{
		StartCloseWindow();
	}

	public void StartCloseWindow()
	{
		if(!IsWindowProcess)
		{
			windowCoroutine = StartCoroutine(Close());
		}
	}
	private IEnumerator Close()
	{
		Fader.FadeOut();
		while(Fader.IsFadeProcess)
		{
			yield return null;
		}

		Fader.CanvasGroup.interactable = false;
		Fader.CanvasGroup.blocksRaycasts = false;

		onClosed.Invoke();

		StopCloseWindow();
	}
	public void StopCloseWindow()
	{
		if(IsWindowProcess)
		{
			StopCoroutine(windowCoroutine);
			windowCoroutine = null;
		}
	}


	[ContextMenu("OpenWindow")]
	public void OpenWindow()
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