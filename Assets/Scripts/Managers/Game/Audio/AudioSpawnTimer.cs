using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpawnTimer : Audio
{

	[SerializeField] private AudioClip clipOpen;
	[SerializeField] private AudioClip clipClose;


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
	public void SetAudioClipOnOpen()
	{
		Source.clip = clipOpen;
	}
	public void SetAudioClipOnClose()
	{
		Source.clip = clipClose;
	}
}