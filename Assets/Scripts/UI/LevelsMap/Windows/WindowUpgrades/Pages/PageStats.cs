using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PageStats : MonoBehaviour
{
	public StatUI statMaxHP;
	public StatUI statMaxRadius;

	private List<StatUI> statsUI = new List<StatUI>();

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

	[SerializeField] private PageStatInformation pageInformation;

	public void Click(StatUI statUI)
	{
		UnChoseSpells();

		statUI.IsChosen = true;

		pageInformation.ShowStatInformation(statUI);
	}

	public void UnChoseSpells()
	{
		for(int i = 0; i < statsUI.Count; i++)
		{
			statsUI[i].IsChosen = false;
		}
	}

	public void UpdateStats(PlayerOpportunitiesData data)
	{
		statsUI.Clear();

		statsUI.Add(statMaxHP);
		statsUI.Add(statMaxRadius);

		UpdateStat(statMaxHP, data.maxHPDatas[SaveData.Instance.statsLevels[0]], 0);
		UpdateStat(statMaxRadius, data.maxRadiusDatas[SaveData.Instance.statsLevels[1]], 1);

		for(int i = 0; i < statsUI.Count; i++)
		{
			if(statsUI[i].IsChosen)
			{
				pageInformation.ShowStatInformation(statsUI[i]);
				break;
			}
		}
	}
	private void UpdateStat(StatUI statUI, StatData data, int statIndex)
	{
		statUI.data = data;
		statUI.statIndex = statIndex;

		statUI.onTap += Click;
		
		statUI.UpdateStat();
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
