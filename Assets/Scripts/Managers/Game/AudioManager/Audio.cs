using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Audio : MonoBehaviour
{
    private AudioSource source;
    public AudioSource Source
	{
		get
		{
			if(source == null)
			{
				source = GetComponent<AudioSource>();
			}
			return source;
		}
	}

	public abstract void PlayAudio();
	public abstract void PauseAudio();
}