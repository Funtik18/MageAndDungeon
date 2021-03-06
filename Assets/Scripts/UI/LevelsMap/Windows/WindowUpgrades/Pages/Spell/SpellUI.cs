﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpellUI : MonoBehaviour, IPointerDownHandler
{
	public UnityEvent onTaps;
	public UnityAction<SpellUI> onTap;

	[HideInInspector] public int price;
	[HideInInspector] public SpellData data;
	[HideInInspector] public int spellIndex = 0;
	[HideInInspector] public string diff;
	[Space]
	[SerializeField] private Image icon;
	[SerializeField] private Image frame;

	public bool IsChosen
	{
		set
		{
			chosenImage.enabled = value;
		}
		get
		{
			return chosenImage.enabled;
		}
	}
	[SerializeField] private Image chosenImage;

	[Space]
	public TMPro.TextMeshProUGUI spellName;
	
	public void UpdateSpell()
	{
		if(spellIndex == 0 && !SaveData.Instance.isHaveFrost)
		{
			icon.sprite = data.disable;
		}
		else if(spellIndex == 1 && !SaveData.Instance.isHaveFist)
		{
			icon.sprite = data.disable;
		}
		else if(spellIndex == 2 && !SaveData.Instance.isHaveStorm)
		{
			icon.sprite = data.disable;
		}
		else
		{
			icon.sprite = data.icon;
		}

		spellName.text = data.russianInfo.name;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		onTaps?.Invoke();
		onTap?.Invoke(this);
	}
}