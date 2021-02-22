using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager
{
	public const string mainStatisticPath = "/saves/statistics.save";

	public const string savePath = "/saves";

	
	private static int isCurrentLevel = -1;
	public static int IsCurrentLevel
	{
		set
		{
			isCurrentLevel = value;
			PlayerPrefs.SetInt("OnCurrentLevel", isCurrentLevel);
		}
		get
		{
			if(isCurrentLevel == -1)
			{
				isCurrentLevel = PlayerPrefs.GetInt("OnCurrentLevel", -1);
				if(isCurrentLevel == -1)
				{
					IsCurrentLevel = 0;
				}
			}
			return isCurrentLevel;
		}
	}


	private static int isLastLevels = -1;
	public static int IsLastLevel
	{
		set
		{
			isLastLevels = value;
			PlayerPrefs.SetInt("OnLastLevels", isLastLevels);
		}
		get
		{
			if(isLastLevels == -1)
			{
				isLastLevels = PlayerPrefs.GetInt("OnLastLevels", -1);
				if(isLastLevels == -1)
				{
					IsLastLevel = 0;
				}
			}
			return isLastLevels;
		}
	}


	private static int isAllLevels = -1;
	public static int IsAllLevels
	{
		set
		{
			isAllLevels = value;
			PlayerPrefs.SetInt("OnAllLevels", isAllLevels);
		}
		get
		{
			if(isAllLevels == -1)
			{
				isAllLevels = PlayerPrefs.GetInt("OnAllLevels", -1);
				if(isAllLevels == -1)
				{
					IsAllLevels = 0;
				}
			}
			return isAllLevels;
		}
	}


	private static int isMusic = -1;
	public static bool IsMusic
	{
		set
		{
			isMusic = value ? 1 : 0;
			PlayerPrefs.SetInt("OnMusic", isMusic);
		}
		get
		{
			if(isMusic == -1)
			{
				isMusic = PlayerPrefs.GetInt("OnMusic", -1);
				if(isMusic == -1)
				{
					IsMusic = true;
				}
			}
			return isMusic == 1 ? true : false;
		}
	}


	private static int isSound = -1;
	public static bool IsSound
	{
		set
		{
			isSound = value ? 1 : 0;
			PlayerPrefs.SetInt("OnSound", isSound);
		}
		get
		{
			if(isSound == -1)
			{
				isSound = PlayerPrefs.GetInt("OnSound", -1);
				if(isSound == -1)
				{
					IsSound = true;
				}
			}
			return isSound == 1 ? true : false;
		}
	}


	private static int isVibration = -1;
	public static bool IsVibration
	{
		set
		{
			isVibration = value ? 1 : 0;
			PlayerPrefs.SetInt("OnVibration", isVibration);
		}
		get
		{
			if(isVibration == -1)
			{
				isVibration = PlayerPrefs.GetInt("OnVibration", -1);
				if(isVibration == -1)
				{
					IsVibration = true;
				}
			}
			return isVibration == 1 ? true : false;
		}
	}


	public static bool Save(string saveName, object saveData)
	{
		BinaryFormatter formatter = GetBinaryFormate();

		if(!Directory.Exists(Application.persistentDataPath + savePath))
		{
			Directory.CreateDirectory(Application.persistentDataPath + savePath);
		}

		string path = Application.persistentDataPath + saveName;

		FileStream file = File.Create(path);

		formatter.Serialize(file, saveData);

		file.Close();

		return true;
	}

	public static object Load(string path)
	{
		if(!File.Exists(path))
		{
			return null;
		}

		BinaryFormatter formatter = GetBinaryFormate();

		FileStream file = File.Open(path, FileMode.Open);

		try
		{
			object save = formatter.Deserialize(file);
			file.Close();
			return save;
		}
		catch
		{
			Debug.LogErrorFormat("Failed to load file {0}", path);
			file.Close();
			return null;
		}
	}

	private static BinaryFormatter GetBinaryFormate()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		return formatter;
	}

	////   private static SaveLoadManager instance;
	////   public static SaveLoadManager Instance
	////{
	////	get
	////	{
	////		if(instance == null)
	////		{
	////			instance = FindObjectOfType<SaveLoadManager>();
	////			if(instance == null)
	////			{
	////				instance = new GameObject("_SaveLoadManager").AddComponent<SaveLoadManager>();
	////				DontDestroyOnLoad(instance);
	////			}
	////		}
	////		return instance;
	////	}
	////}

	//private const string currentGoldKeyName = "CurrentGold";
	//private const string currentDiamondKeyName = "CurrentDiamond";
	//private const string currentLevelKeyName = "CurrentLevel";



	//private int currentGold = -1;
	//public int CurrentGold
	//{
	//	set
	//	{
	//		if(currentGold != value)
	//		{
	//			currentGold = value;
	//			PlayerPrefs.SetInt(currentGoldKeyName, currentGold);
	//		}
	//	}
	//	get
	//	{
	//		if(currentGold == -1)
	//		{
	//			currentGold = PlayerPrefs.GetInt(currentGoldKeyName, -1);
	//			if(currentGold == -1)//first time
	//			{
	//				CurrentGold = 0;
	//			}
	//		}
	//		return currentGold;
	//	}
	//}


	//private int currentDiamond = -1;
	//public int CurrentDiamond
	//{
	//	set
	//	{
	//		if(currentDiamond != value)
	//		{
	//			currentDiamond = value;
	//			PlayerPrefs.SetInt(currentDiamondKeyName, currentDiamond);
	//		}
	//	}
	//	get
	//	{
	//		if(currentDiamond == -1)
	//		{
	//			currentDiamond = PlayerPrefs.GetInt(currentDiamondKeyName, -1);
	//			if(currentDiamond == -1)//first time
	//			{
	//				CurrentDiamond = 0;
	//			}
	//		}
	//		return currentDiamond;

	//	}
	//}


	//private int currentLevel = -1;
	//public int CurrentLevel
	//{
	//	set
	//	{
	//		if(currentLevel != value)
	//		{
	//			currentLevel = value;
	//			PlayerPrefs.SetInt(currentLevelKeyName, currentLevel);
	//		}
	//	}
	//	get
	//	{
	//		if(currentLevel == -1)
	//		{
	//			currentLevel = PlayerPrefs.GetInt(currentLevelKeyName, -1);
	//			if(currentLevel == -1)//first time
	//			{
	//				CurrentLevel = 0;
	//			}
	//		}
	//		return currentLevel;
	//	}
	//}


	//private void DisposeAll()
	//{
	//	PlayerPrefs.DeleteAll();
	//}
}