using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatUI : MonoBehaviour, IPointerDownHandler
{
	public UnityEvent onTaps;
	public UnityAction<StatUI> onTap;

	[HideInInspector] public StatData data;
	[HideInInspector] public int statIndex = 0;
	[HideInInspector] public int price;
	[HideInInspector] public string diff;

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
		onTaps?.Invoke();
		onTap?.Invoke(this);
	}
}