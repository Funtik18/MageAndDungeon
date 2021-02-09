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

	private const string currentLevelKeyName = "CurrentLevel";
	private int currentLevel = -1;
	public int CurrentLevel
	{
		set
		{
			if(currentLevel != value)
			{
				currentLevel = value;
				SaveCurrentLevel();
			}
		}
		get
		{
			if(currentLevel == -1)
			{
				LoadCurrentLevel();
				if(currentLevel == -1)//first time
				{
					CurrentLevel = 0;
				}
			}
			return currentLevel;
		}
	}

	private void SaveCurrentLevel()
	{
		PlayerPrefs.SetInt(currentLevelKeyName, currentLevel);
	}
	private void LoadCurrentLevel()
	{
		currentLevel = PlayerPrefs.GetInt(currentLevelKeyName, -1);
	}


	private void DisposeAll()
	{
		PlayerPrefs.DeleteAll();
	}
}