using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PageSpells : MonoBehaviour
{
	public SpellUI spellHellishFrost;
	public SpellUI spellPunishingFist;
	public SpellUI spellThunderStorm;

	private List<SpellUI> spellsUI = new List<SpellUI>();

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

	[SerializeField] private PageSpellInformation pageInformation;

	public void Click(SpellUI spellUI)
	{
		UnChoseSpells();

		spellUI.IsChosen = true;

		pageInformation.ShowSpellInformation(spellUI);
	}

	public void UnChoseSpells()
	{
		for(int i = 0; i < spellsUI.Count; i++)
		{
			spellsUI[i].IsChosen = false;
		}
	}

	public void UpdateSpells(PlayerOpportunitiesData data)
	{
		spellsUI.Clear();

		spellsUI.Add(spellHellishFrost);
		spellsUI.Add(spellPunishingFist);
		spellsUI.Add(spellThunderStorm);

		UpdateSpell(spellHellishFrost, data.hellishFrostDatas[SaveData.Instance.spellsLevels[0]], 0);
		UpdateSpell(spellPunishingFist, data.punishingFistDatas[SaveData.Instance.spellsLevels[1]], 1);
		UpdateSpell(spellThunderStorm, data.thunderStormDatas[SaveData.Instance.spellsLevels[2]], 2);

		for(int i = 0; i < spellsUI.Count; i++)
		{
			if(spellsUI[i].IsChosen)
			{
				pageInformation.ShowSpellInformation(spellsUI[i]);
				break;
			}
		}
	}
	private void UpdateSpell(SpellUI spellUI, SpellData data, int spellIndex)
	{
		spellUI.data = data;
		spellUI.statIndex = spellIndex;

		spellUI.onTap += Click;

		spellUI.UpdateSpell();
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