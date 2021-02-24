using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class QualitySet : MonoBehaviour
{

    [Header("Quality")]
    public Button lowQual;
    public Button midQual;
    public Button maxQual;
    [Header("Localization")]
    public Button rusButton;
    public Button engButton;
    public TextMeshProUGUI[] Texts;
    public MainMenuData[] dataTexts;

    private void Awake()
    {
        setQuality(IsQuality);
        setLanguage(Language);
        setTexts(dataTexts[language]);
    }

    void setTexts(MainMenuData dataTexts)
    {
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].text = dataTexts.mainMenuTexts[i];
        }
    }

    public void changeLang()
    {
        setLanguage(Language);
        setTexts(dataTexts[language]);
    }

    public void setCurLang(int ind)
    {
        Language = ind;
    }

    void setLanguage(int lang)
    {
        switch (lang)
        {
            case 0:
                rusButton.GetComponent<Image>().color = Color.white;
                engButton.GetComponent<Image>().color = Color.gray;
                break;
            case 1:
                rusButton.GetComponent<Image>().color = Color.gray;
                engButton.GetComponent<Image>().color = Color.white;
                break;
        }
        Language = lang;
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
            PlayerPrefs.SetInt("Quality", isQuality);
        }
        get
        {
            if (isQuality == -1)
            {
                isQuality = PlayerPrefs.GetInt("Quality", -1);
                if (isQuality == -1)
                {
                    IsQuality = 0;
                }
            }
            return isQuality;
        }
    }

    private static int language = -1;
    private static int Language
    {
        set
        {
            language = value;
            PlayerPrefs.SetInt("language", language);
        }
        get
        {
            if (language == -1)
            {
                language = PlayerPrefs.GetInt("language", -1);
                if (language == -1)
                {
                    Language = 0;
                }
            }
            return language;
        }
    }

}
