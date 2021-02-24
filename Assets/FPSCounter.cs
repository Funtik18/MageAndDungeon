using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public int avgFrameRate;

    // Update is called once per frame
    void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
    }

    void OnGUI()
    {
            GUI.Label(new Rect(20, 40, 80, 20), avgFrameRate.ToString());  
    }
}
