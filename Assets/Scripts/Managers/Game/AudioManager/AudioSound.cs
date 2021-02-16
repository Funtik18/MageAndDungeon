using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSound : Audio
{
	[SerializeField] private AudioClip clip;

	private bool isPlaying => Source.isPlaying;

	private void Awake()
	{
		Source.clip = clip;
	}

	public override void PlayAudio()
	{
		if(SaveLoadManager.IsSound)
		{
			Source.Play();
		}
	}

	public override void PauseAudio()
	{
	}
}