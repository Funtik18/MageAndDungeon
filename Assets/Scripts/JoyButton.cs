using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class JoyButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    [HideInInspector]
    protected bool Pressed;

    public GameObject spel;

    GameObject currentSpell;
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
        currentSpell=Instantiate(spel) as GameObject;
        StartCoroutine(SpellCasting(5));
    }

    IEnumerator SpellCasting(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        foreach (var item in currentSpell.GetComponentsInChildren<ParticleSystem>())
        {
            item.Stop();
        }
        yield return new WaitForSecondsRealtime(sec);
        Destroy(currentSpell);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
