using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class QualitySet : MonoBehaviour
{
    public Button lowQual;
    public Button midQual;
    public Button maxQual;

    private void Awake()
    {
        setQuality(IsQuality);
    }

    public void changeQuality()
    {
        setQuality(IsQuality);
    }

    void setQuality(int curQual)
    {
        switch (curQual)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                lowQual.GetComponent<Image>().color = Color.white;
                midQual.GetComponent<Image>().color = Color.gray;
                maxQual.GetComponent<Image>().color = Color.gray;

                break;
            case 1:
                QualitySettings.SetQualityLevel(2);
                lowQual.GetComponent<Image>().color = Color.gray;
                midQual.GetComponent<Image>().color = Color.white;
                maxQual.GetComponent<Image>().color = Color.gray;
                break;
            case 2:
                QualitySettings.SetQualityLevel(5);
                lowQual.GetComponent<Image>().color = Color.gray;
                midQual.GetComponent<Image>().color = Color.gray;
                maxQual.GetComponent<Image>().color = Color.white;
                break;

        }
        IsQuality = curQual;
    }

    public void setCurQual(int ind)
    {
        IsQuality = ind;
    }

    private static int isQuality = -1;
    private static int IsQuality
    {
        set
        {
            isQuality = value;
            Debug.Log(isQuality);
            PlayerPrefs.SetInt("Quality", isQuality);
        }
        get
        {
            if (isQuality == -1)
            {
                Debug.Log(isQuality);
                isQuality = PlayerPrefs.GetInt("Quality", -1);
                if (isQuality == -1)
                {
                    IsQuality = 0;
                }
            }
            return isQuality;
        }
    }

}
