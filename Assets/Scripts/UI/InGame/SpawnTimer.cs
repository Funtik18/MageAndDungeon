using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class SpawnTimer : MonoBehaviour
{
	public UnityAction onTap;
	public UnityAction onTimeOut;

    [SerializeField] private Image timeImage;

	[SerializeField] private PanelInteraction interaction;

	private float speedFilling = 1;

	[HideInInspector] public VariableBoolean isPause;

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
	private Coroutine timerCoroutine = null;
	public bool IsTimerProcess => timerCoroutine != null;

	private void Awake()
	{
		interaction.onTap.AddListener(delegate
		{
			onTap?.Invoke();
		});
	}

	public void IncreaseSpeed()
	{
		speedFilling = 50f;
	}
	public void DecreaseSpeed()
	{
		speedFilling = 1f;
	}

	public void TimerFadeIn()
	{
		Fader.CanvasGroup.interactable = true;
		Fader.CanvasGroup.blocksRaycasts = true;
		Fader.FadeIn();
	}
	public void TimerFadeOut()
	{
		DecreaseSpeed();
		Fader.FadeOut();
		Fader.CanvasGroup.interactable = false;
		Fader.CanvasGroup.blocksRaycasts = false;
	}

	public void StartTimer(float secs, UnityAction action)
	{
		if(!IsTimerProcess)
		{
			this.secs = secs;

			onTap = action;

			TimerFadeIn();
			
			timerCoroutine = StartCoroutine(Timer());
		}
	}
	private IEnumerator Timer()
	{
		float currentSecs = secs;
		FillAmount = currentSecs / secs;

		while(FillAmount != 0)
		{
			FillAmount = currentSecs / secs;
			currentSecs -= speedFilling * Time.deltaTime;

			if(Fader.CanvasGroup.alpha == 0)
			{
				break;
			}

			while(isPause.value)
			{
				yield return null;
			}

			yield return null;
		}

		StopTimer();
	}
	private void StopTimer()
	{
		if(IsTimerProcess)
		{
			TimerFadeOut();
			
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