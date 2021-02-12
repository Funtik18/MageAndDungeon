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

		pageInformation.ShowSpellInformation(spellUI.data);
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
}