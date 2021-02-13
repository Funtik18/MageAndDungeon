using UnityEngine;

public class MainStatistics : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI gold;
    [SerializeField] private TMPro.TextMeshProUGUI diamonds;

	private void Awake()
	{
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
		gold.text = SaveData.Instance.currentGold.ToString();
		diamonds.text = SaveData.Instance.currentDiamonds.ToString();
	}
}