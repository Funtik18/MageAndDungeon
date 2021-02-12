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

	public SpellData data;
	[Space]
	public PanelInteraction interaction;
	[SerializeField] private Image icon;
	[SerializeField] private Image frame;

	private bool isChosen = false;
	public bool IsChosen
	{
		set
		{
			isChosen = value;
		}
		get
		{
			return isChosen;
		}
	}
	[SerializeField] private Image chosenImage;

	[Space]
	public TMPro.TextMeshProUGUI spellName;
	
	public void OnPointerDown(PointerEventData eventData)
	{
		onTap?.Invoke(this);
	}

	public void ChoseSpell()
	{
		chosenImage.enabled = true;
	}
	public void UnChoseSpell()
	{
		chosenImage.enabled = false;
	}

	[ContextMenu("UpdateSpellInfo")]
	private void UpdateInformation()
	{
		icon.sprite = data.icon;

		spellName.text = data.spellName;

#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
}