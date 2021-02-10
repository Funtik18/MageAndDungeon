using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PanelInteraction : MonoBehaviour, IPointerDownHandler
{
    public UnityAction<PointerEventData> onTap;

	public void OnPointerDown(PointerEventData eventData)
	{
		onTap?.Invoke(eventData);
	}
}