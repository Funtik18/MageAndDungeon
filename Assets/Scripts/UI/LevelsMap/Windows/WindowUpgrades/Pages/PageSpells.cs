using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PageSpells : MonoBehaviour
{
    public List<SpellUI> spells = new List<SpellUI>();

	[SerializeField] private PageSpellInformation pageInformation;

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

	private void Awake()
	{
		for(int i = 0; i < spells.Count; i++)
		{
			spells[i].onTap = Click;
		}
	}

	public void Click(SpellUI spellUI)
	{
		UnChoseSpells();

		spellUI.ChoseSpell();

		SpellUIData spellUIData = new SpellUIData(spellUI.data, spells.IndexOf(spellUI));

		pageInformation.ShowSpellInformation(spellUIData);
	}

	public void UnChoseSpells()
	{
		for(int i = 0; i < spells.Count; i++)
		{
			spells[i].UnChoseSpell();
		}
	}

	[ContextMenu("GetSpells")]
	private void GetAllSpells()
	{
		spells = GetComponentsInChildren<SpellUI>().ToList();

#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
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


	[System.Serializable]
	public class SpellUIData
	{
		public string name;
		public int level;
		public string description;
		public string additionalInfo;
		public int price;

		public int spellIndex;

		private SpellData data;
		public SpellUIData(SpellData data, int spellIndex)
		{
			this.data = data;

			this.spellIndex = spellIndex;
		
			name = data.spellName;
			description = data.spellDiscription;
			additionalInfo = data.spellAdditionalInfo;

			level = SaveData.Instance.currentLevelSpells[spellIndex];
			price = data.nextLevel[level].newPrice;
		}

		public void UpdateData()
		{
			level = SaveData.Instance.currentLevelSpells[spellIndex];
			price = data.nextLevel[level].newPrice;
		}
	}
}