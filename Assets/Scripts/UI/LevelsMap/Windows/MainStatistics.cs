using UnityEngine;

public class MainStatistics : MonoBehaviour
{
	private static MainStatistics instance;
	public static MainStatistics Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<MainStatistics>();
			}
			return instance;
		}
	}

    [SerializeField] private TMPro.TextMeshProUGUI gold;
    [SerializeField] private TMPro.TextMeshProUGUI diamonds;

	public void UpdateUI()
	{
		gold.text = SaveData.Instance.currentGold.ToString();
		diamonds.text = SaveData.Instance.currentDiamonds.ToString();
	}
}