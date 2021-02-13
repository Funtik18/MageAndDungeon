using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StatUI : MonoBehaviour, IPointerDownHandler
{
	public UnityAction<StatUI> onTap;

	[HideInInspector] public StatData data;
	[HideInInspector] public int statIndex = 0;

	[Space]
	public PanelInteraction interaction;

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
	public TMPro.TextMeshProUGUI statName;
	public TMPro.TextMeshProUGUI statLevel;

	public void UpdateStat()
	{
		statName.text = data.russianInfo.name;
		statLevel.text = data.level.ToString();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		onTap?.Invoke(this);
	}
}