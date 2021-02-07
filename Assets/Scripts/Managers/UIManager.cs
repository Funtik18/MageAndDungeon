using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
	{
		get
		{
            if(instance == null)
			{
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
	}

    public TMPro.TextMeshProUGUI hpCount;
    public TMPro.TextMeshProUGUI moneyCount;


    public void moneyChange(int count)
    {
        moneyCount.text= count.ToString();
    }

    public void hpChange(int count)
    {
        hpCount.text= count.ToString();
    }
}