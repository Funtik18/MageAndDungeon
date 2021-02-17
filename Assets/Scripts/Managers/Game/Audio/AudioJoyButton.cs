using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioJoyButton : Audio
{
	[SerializeField] private AudioClip buttonClick;
	[SerializeField] private AudioClip amountReady;

	public override void PauseAudio()
	{
	}

	public override void PlayAudio()
	{
		if(SaveLoadManager.IsSound)
		{
			Source.Play();
		}
	}
	public void SetAudioOnClick()
	{
		Source.clip = buttonClick;
	}
	public void SetAudioOnAmountReady()
	{
		Source.clip = amountReady;
	}
}