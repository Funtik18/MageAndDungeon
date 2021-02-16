﻿using UnityEngine;
using UnityEngine.Events;

public class AudioBackground : Audio
{
	[SerializeField] private AudioClip clip;

	public UnityEvent onAwake;


	private bool isPlaying => Source.isPlaying;

	private void Awake()
	{
		Source.clip = clip;
		Source.loop = true;
		onAwake?.Invoke();
	}

	public override void PlayAudio()
	{
		Source.Play();
	}

	public override void PauseAudio()
	{
		Source.Pause();
	}
	

	public void CheckMusic()
	{
		if(SaveLoadManager.IsMusic)
		{
			if(!isPlaying)
				PlayAudio();
		}
		else
		{
			if(isPlaying)
				PauseAudio();
		}
	}
}