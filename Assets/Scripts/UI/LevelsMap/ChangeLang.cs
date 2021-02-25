using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLang : MonoBehaviour
{
    public TextMeshProUGUI[] Texts;
    public string[] TextRus;
    public string[] TextEng;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("language") == 0)
            setText(TextRus);
        else
            setText(TextEng);
    }

    public void changeLang()
    {
        if (PlayerPrefs.GetInt("language") == 0)
            setText(TextRus);
        else
            setText(TextEng);
    }

    void setText(string[] Text)
    {
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].text = Text[i];
        }
    }
}
