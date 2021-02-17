using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerPunch : Audio
{
	public List<AudioClip> clips = new List<AudioClip>();
	public override void PauseAudio()
	{
		throw new System.NotImplementedException();
	}

	public override void PlayAudio()
	{
		if(SaveLoadManager.IsSound)
		{
			Source.clip = clips[Random.Range(0, clips.Count - 1)];

			Source.Play();
		}
	}
}