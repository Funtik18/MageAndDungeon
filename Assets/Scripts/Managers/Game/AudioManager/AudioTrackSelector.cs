using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrackSelector : MonoBehaviour
{
    public AudioClip[] audioClips;
    public bool haveSound=true;

    AudioSource myAudioSource;
    AudioBackground myAudioBackground;

    private void Awake()
    {
        if (!haveSound)
            return;
        int ind = Random.Range(0, audioClips.Length);
        myAudioSource = GetComponent<AudioSource>();
        myAudioBackground = GetComponent<AudioBackground>();
        myAudioBackground.clip = audioClips[ind];
        myAudioSource.clip= audioClips[ind];
        myAudioBackground.PlayAudio();
    }
}
