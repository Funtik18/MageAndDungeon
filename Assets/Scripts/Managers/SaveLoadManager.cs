using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager instance;
    public static SaveLoadManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<SaveLoadManager>();
				if(instance == null)
				{
					instance = new GameObject("_SaveLoadManager").AddComponent<SaveLoadManager>();
					DontDestroyOnLoad(instance);
				}
			}
			return instance;
		}
	}

	private const string currentGoldKeyName = "CurrentGold";
	private const string currentDiamondKeyName = "CurrentDiamond";
	private const string currentLevelKeyName = "CurrentLevel";


	private int currentGold = -1;
	public int CurrentGold
	{
		set
		{
			if(currentGold != value)
			{
				currentGold = value;
				PlayerPrefs.SetInt(currentGoldKeyName, currentGold);
			}
		}
		get
		{
			if(currentGold == -1)
			{
				currentGold = PlayerPrefs.GetInt(currentGoldKeyName, -1);
				if(currentGold == -1)//first time
				{
					CurrentGold = 0;
				}
			}
			return currentGold;
		}
	}


	private int currentDiamond = -1;
	public int CurrentDiamond
	{
		set
		{
			if(currentDiamond != value)
			{
				currentDiamond = value;
				PlayerPrefs.SetInt(currentDiamondKeyName, currentDiamond);
			}
		}
		get
		{
			if(currentDiamond == -1)
			{
				currentDiamond = PlayerPrefs.GetInt(currentDiamondKeyName, -1);
				if(currentDiamond == -1)//first time
				{
					CurrentDiamond = 0;
				}
			}
			return currentDiamond;

		}
	}


	private int currentLevel = -1;
	public int CurrentLevel
	{
		set
		{
			if(currentLevel != value)
			{
				currentLevel = value;
				PlayerPrefs.SetInt(currentLevelKeyName, currentLevel);
			}
		}
		get
		{
			if(currentLevel == -1)
			{
				currentLevel = PlayerPrefs.GetInt(currentLevelKeyName, -1);
				if(currentLevel == -1)//first time
				{
					CurrentLevel = 0;
				}
			}
			return currentLevel;
		}
	}


	private void DisposeAll()
	{
		PlayerPrefs.DeleteAll();
	}
}