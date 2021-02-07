using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance
	{
		get
		{
            if(instance == null)
			{
                instance = FindObjectOfType<UIController>();
            }
            return instance;
        }
	}

    public Text hpCount;
    public Text moneyCount;


    public void moneyChange(int count)
    {
        moneyCount.text= count.ToString();
    }

    public void hpChange(int count)
    {
        hpCount.text= count.ToString();
    }
}
