﻿using UnityEngine;
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

	private Animator animator;
	public Animator Animator
	{
		get
		{
			if(animator == null)
			{
				animator = GetComponent<Animator>();
			}
			return animator;
		}
	}

	private AudioSpawnTimer audioTimer;
	public AudioSpawnTimer AudioTimer
	{
		get
		{
			if(audioTimer == null)
			{
				audioTimer = GetComponent<AudioSpawnTimer>();
			}
			return audioTimer;
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


	private bool isTap = false;

	private float secs = 1f;
	private Coroutine timerCoroutine = null;
	public bool IsTimerProcess => timerCoroutine != null;

	private void Awake()
	{
		Animator.enabled = false;
		interaction.onTap.AddListener(delegate
		{
			isTap = true;

			onTap?.Invoke();

			AudioTimer.SetAudioClipOnClose();
			AudioTimer.PlayAudio();
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
		isTap = false;

		AudioTimer.SetAudioClipOnOpen();
		AudioTimer.PlayAudio();


		Animator.enabled = true;
		Animator.SetTrigger("Open");

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
		Animator.SetTrigger("Close");
		StopTimer();
	}
	private void StopTimer()
	{
		if(IsTimerProcess)
		{
			if(isTap == false)
			{
				AudioTimer.SetAudioClipOnClose();
				AudioTimer.PlayAudio();
			}

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