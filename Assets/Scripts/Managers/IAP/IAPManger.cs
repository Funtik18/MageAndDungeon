using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManger : MonoBehaviour
{
    private static IAPManger instance;
    public static IAPManger Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<IAPManger>();
			}
			return instance;
		}
	}

	private string removeADS = "";
}