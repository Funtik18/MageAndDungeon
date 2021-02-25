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

		int[] stats = SaveData.Instance.statsLevels;
        //Debug.Log(data.maxHps[0].englishInfo.);
		UpdateStat(statMaxHP, data.maxHps[stats[0]], stats[0] + 1 <= data.maxHps.Count - 1 ? data.maxHps[stats[0] + 1] : null , 0);
		UpdateStat(statMaxDamage, data.maxDamages[stats[1]], stats[1] + 1 <= data.maxDamages.Count-1 ? data.maxDamages[stats[1] + 1] : null, 1);
		UpdateStat(statMaxDamageOverTime, data.maxDamageOverTimes[stats[2]], stats[2] + 1 <= data.maxDamageOverTimes.Count - 1 ? data.maxDamageOverTimes[stats[2] + 1] : null, 2);
		UpdateStat(statSpeed, data.maxSpeeds[stats[3]], stats[3] + 1 <= data.maxSpeeds.Count - 1 ? data.maxSpeeds[stats[3] + 1] : null, 3);
		UpdateStat(statRadius, data.maxRadiuses[stats[4]], stats[4] + 1 <= data.maxRadiuses.Count - 1 ? data.maxRadiuses[stats[4] + 1] : null, 4);
		UpdateStat(statPassiveIncome, data.maxPassiveIncomes[stats[5]], stats[5] + 1 <= data.maxPassiveIncomes.Count - 1 ? data.maxPassiveIncomes[stats[5] + 1] : null, 5);
		UpdateStat(statMobScalarProfit, data.maxMobScalarProfits[stats[6]], stats[6] + 1 <= data.maxMobScalarProfits.Count - 1 ? data.maxMobScalarProfits[stats[6] + 1] : null, 6);

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
	private void UpdateStat(StatUI statUI, StatData data, StatData nextData, int statIndex)
	{
		statUI.data = data;
		statUI.statIndex = statIndex;

		if(nextData == null)
		{
			statUI.diff = "";
			statUI.price = 0;
		}
		else
		{
			statUI.price = nextData.price;
			statUI.diff = data.GetDiffrence(nextData);
		}

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
