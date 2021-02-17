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

		int[] spl = SaveData.Instance.spellsLevels;

		UpdateSpell(spellHellishFrost, data.hellishFrosts[spl[0]], spl[0] + 1 <= data.hellishFrosts.Count - 1 ? data.hellishFrosts[spl[0] + 1] : null, 0);
		UpdateSpell(spellPunishingFist, data.punishingFists[spl[1]], spl[1] + 1 <= data.punishingFists.Count - 1 ? data.punishingFists[spl[1] + 1] : null, 1);
		UpdateSpell(spellThunderStorm, data.thunderStorms[spl[2]], spl[2] + 1 <= data.thunderStorms.Count - 1 ? data.thunderStorms[spl[2] + 1] : null, 2);

		for(int i = 0; i < spellsUI.Count; i++)
		{
			if(spellsUI[i].IsChosen)
			{
				pageInformation.ShowSpellInformation(spellsUI[i]);
				break;
			}
		}
	}
	private void UpdateSpell(SpellUI spellUI, SpellData data, SpellData nextData, int spellIndex)
	{
		spellUI.data = data;
		spellUI.statIndex = spellIndex;

		if(nextData == null)
		{
			spellUI.diff = "";
		}
		else
		{
			spellUI.diff = nextData.GetDiffrence(nextData);
		}

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