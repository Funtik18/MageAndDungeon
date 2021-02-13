using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpellUI : MonoBehaviour, IPointerDownHandler
{
	public UnityAction<SpellUI> onTap;

	[HideInInspector] public SpellData data;
	[HideInInspector] public int statIndex = 0;
	[Space]
	public PanelInteraction interaction;
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
	
	public void OnPointerDown(PointerEventData eventData)
	{
		onTap?.Invoke(this);
	}

	public void UpdateSpell()
	{
		icon.sprite = data.icon;
		spellName.text = data.russianInfo.name;
	}
}