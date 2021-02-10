using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class SpawnTimer : MonoBehaviour
{
	public UnityAction onTap;

    [SerializeField] private Image timeImage;

	[SerializeField] private PanelInteraction interaction;

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

	public float FillAmount
	{
		set
		{
			timeImage.fillAmount = value;
		}
		get
		{
			return timeImage.fillAmount;
		}
	}

	private float secs = 1f;
	private bool tap = false;

	private Coroutine timerCoroutine = null;
	public bool IsTimerProcess => timerCoroutine != null;

	private void Awake()
	{
		interaction.onTap = delegate
		{
			onTap?.Invoke();
			tap = true;
		};
	}


	public void StartTimer(float secs)
	{
		if(!IsTimerProcess)
		{
			this.secs = secs;
			timerCoroutine = StartCoroutine(Timer());
		}
	}
	private IEnumerator Timer()
	{
		Fader.CanvasGroup.interactable = true;
		Fader.CanvasGroup.blocksRaycasts = true;
		Fader.FadeIn();

		bool once = true;
		float currentSecs = secs;
		FillAmount = currentSecs / secs;


		while(FillAmount != 0)
		{
			FillAmount = currentSecs / secs;

			currentSecs -= Time.deltaTime;

			if(once)
				if(tap)
				{
					Fader.FadeOut();
					Fader.CanvasGroup.interactable = false;
					Fader.CanvasGroup.blocksRaycasts = false;

					once = false;
					tap = false;
				}

			if(Fader.CanvasGroup.alpha == 0)
			{
				break;
			}

			yield return null;
		}


		if(Fader.CanvasGroup.alpha != 0)
		{
			Fader.FadeOut();
			Fader.CanvasGroup.interactable = false;
			Fader.CanvasGroup.blocksRaycasts = false;
		}

		StopTimer();
	}
	private void StopTimer()
	{
		if(IsTimerProcess)
		{
			StopCoroutine(timerCoroutine);
			timerCoroutine = null;
		}
	}


	[ContextMenu("OpenTimer")]
	private void OpenTimer()
	{
		Fader.CanvasGroup.alpha = 1;
		Fader.CanvasGroup.interactable = true;
		Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
	[ContextMenu("CloseTimer")]
	private void CloseTimer()
	{
		Fader.CanvasGroup.alpha = 0;
		Fader.CanvasGroup.interactable = false;
		Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
}