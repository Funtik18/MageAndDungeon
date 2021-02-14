using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
	{
		get
		{
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
	}

	public PlayerOpportunitiesData playerOpportunities;


	private SaveData data;
	private SaveData Data
	{
		get
		{
			if(data == null)
			{
				//data = (SaveData)SaveLoadManager.Load(Application.persistentDataPath + SaveLoadManager.mainStatisticPath);
				//data = SaveData.Instance.RefreshData(data);
				//Debug.LogError(Application.persistentDataPath + SaveLoadManager.mainStatisticPath);
				data = (SaveData)SaveLoadManager.Load(Application.persistentDataPath + SaveLoadManager.mainStatisticPath);
				if(data == null)//first time
				{
					data = SaveData.Instance.StartValues();
					SaveLoadManager.Save(SaveLoadManager.mainStatisticPath, data);

					data = SaveData.Instance.RefreshData(data);
				}
				else
				{
					data = SaveData.Instance.RefreshData(data);
				}
			}
			return data;
		}
	}


	private Wizard wizardTarget;
    public Wizard WizardTarget
	{
		get
		{
            if(wizardTarget == null)
			{
                wizardTarget = FindObjectOfType<Wizard>();
            }
            return wizardTarget;
		}
	}


	[SerializeField]private PlayerStats stats;
	public PlayerStats Stats
	{
		get
		{
			if(stats == null)
			{
				stats = new PlayerStats(playerOpportunities, Data);
			}
			return stats;
		}
	}


	private void Awake()
	{
		if(Data == null) { }
	}
}