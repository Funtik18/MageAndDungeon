using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PanelInteraction : MonoBehaviour, IPointerClickHandler
{
    public UnityAction<PointerEventData> onTap;

	public void OnPointerClick(PointerEventData eventData)
	{
		onTap?.Invoke(eventData);
	}
}
