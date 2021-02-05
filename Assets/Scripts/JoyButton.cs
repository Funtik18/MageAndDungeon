using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class JoyButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    [HideInInspector]
    protected bool Pressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
