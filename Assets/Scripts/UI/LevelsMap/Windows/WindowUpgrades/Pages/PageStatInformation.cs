using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PageStatInformation : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI tittle;
	[SerializeField] private TMPro.TextMeshProUGUI level;
	[SerializeField] private TMPro.TextMeshProUGUI description;
	[SerializeField] private TMPro.TextMeshProUGUI cost;
	[SerializeField] private TMPro.TextMeshProUGUI additionalInfo;
	[Space]
	[SerializeField] private Button acceptButton;

	private Fader fader;
	public Fader Fader
	{
		get
		{
			if(fader == null)
			{
				fader = GetComponent<Fader>();
			}
			return fader;
		}
	}

	private StatUI currentStat;

	public void ShowStatInformation(StatUI stat)
	{
		currentStat = stat;

		UpdatePage();

		if(currentStat.data.level >= 4)
		{
			acceptButton.GetComponent<Image>().enabled = false;
		}
		else
		{
			acceptButton.GetComponent<Image>().enabled = true;

			acceptButton.onClick.RemoveAllListeners();

			if(currentStat.data.price <= SaveData.Instance.currentGold)
			{
				acceptButton.GetComponent<Image>().color = Color.green;

				acceptButton.onClick.AddListener(AcceptBuy);
			}
			else
			{
				acceptButton.GetComponent<Image>().color = Color.red;
			}
		}

		OpenWindow();
	}
	private void UpdatePage()
	{
		tittle.text = currentStat.data.russianInfo.name;
		level.text = currentStat.data.level.ToString();
		description.text = currentStat.data.russianInfo.description;
		cost.text = currentStat.data.price.ToString();
		additionalInfo.text = currentStat.data.russianInfo.additional;
	}

	private void AcceptBuy()
	{
		SaveData.Instance.currentGold -= currentStat.data.price;

		SaveData.Instance.statsLevels[currentStat.statIndex]++;

		SaveLoadManager.Save(SaveLoadManager.mainStatisticPath, SaveData.Instance);

		//upd
		LevelsMapManager.Instance.UpdateUI();
	}


	[ContextMenu("OpenWindow")]
	public void OpenWindow()
	{
		Fader.CanvasGroup.alpha = 1;
		Fader.CanvasGroup.interactable = true;
		Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
	[ContextMenu("CloseWindow")]
	public void CloseWindow()
	{
		Fader.CanvasGroup.alpha = 0;
		Fader.CanvasGroup.interactable = false;
		Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
}
