using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingsAudio : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSound>().PlayAudio();
    }

}
