using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PageStats : MonoBehaviour
{
	public StatUI statMaxHP;
	
	public StatUI statMaxDamage;
	public StatUI statMaxDamageOverTime;

	public StatUI statSpeed;

	public StatUI statRadius;

	public StatUI statPassiveIncome;

	public StatUI statMobScalarProfit;

	private List<StatUI> statsUI = new List<StatUI>();

	[SerializeField] private PageStatInformation pageInformation;

	public ScrollRect rect;


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
		rect.verticalScrollbar.value = 1f;

		UpdateStat(statMaxHP, data.maxHps[SaveData.Instance.statsLevels[0]], 0);
		UpdateStat(statMaxDamage, data.maxDamages[SaveData.Instance.statsLevels[1]], 1);
		UpdateStat(statMaxDamageOverTime, data.maxDamageOverTimes[SaveData.Instance.statsLevels[2]], 2);
		UpdateStat(statSpeed, data.maxSpeeds[SaveData.Instance.statsLevels[3]], 3);
		UpdateStat(statRadius, data.maxRadiuses[SaveData.Instance.statsLevels[4]], 4);
		UpdateStat(statPassiveIncome, data.maxPassiveIncomes[SaveData.Instance.statsLevels[5]], 5);
		UpdateStat(statMobScalarProfit, data.maxMobScalarProfits[SaveData.Instance.statsLevels[6]], 6);

		statsUI.Clear();

		statsUI.Add(statMaxHP);
		statsUI.Add(statMaxDamage);
		statsUI.Add(statMaxDamageOverTime);
		statsUI.Add(statSpeed);
		statsUI.Add(statRadius);
		statsUI.Add(statPassiveIncome);
		statsUI.Add(statMobScalarProfit);

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
