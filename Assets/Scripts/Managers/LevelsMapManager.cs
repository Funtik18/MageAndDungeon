using UnityEngine;

public class LevelsMapManager : MonoBehaviour
{
	private static LevelsMapManager instance;
	public static LevelsMapManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<LevelsMapManager>();
			}
			return instance;
		}
	}

	public PlayerOpportunitiesData opportunitiesData;

	public Animator levelLoader;

	private void Awake()
	{
		levelLoader.SetTrigger("TransitionOut");

		//Debug.LogError(Application.persistentDataPath + SaveLoadManager.mainStatisticPath);
		SaveData data = (SaveData)SaveLoadManager.Load(Application.persistentDataPath + SaveLoadManager.mainStatisticPath);
		if(data == null)//first time
		{
			data = SaveData.Instance.StartValues();
			SaveLoadManager.Save(SaveLoadManager.mainStatisticPath, data);

			SaveData.Instance.RefreshData(data);
		}
		else
		{
			SaveData.Instance.RefreshData(data);
		}

		UpdateUI();
	}

	public void UpdateUI()
	{
		MainStatistics.Instance.UpdateUI();
		WindowUpgrades.Instance.UpdateUI(opportunitiesData);
	}
}