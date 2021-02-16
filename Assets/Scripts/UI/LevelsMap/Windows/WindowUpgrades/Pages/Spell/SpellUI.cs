using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpellUI : MonoBehaviour, IPointerDownHandler
{
	public UnityAction<SpellUI> onTap;

	[HideInInspector] public SpellData data;
	[HideInInspector] public int statIndex = 0;
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
		icon.sprite = data.icon;

		spellName.text = data.russianInfo.name;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		onTap?.Invoke(this);
	}
}