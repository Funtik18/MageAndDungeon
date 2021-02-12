using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class JoyButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public float spelDuration;

    private bool isBlock = false;
    public bool IsBlock
	{
		set
		{
            isBlock = value;
            Fader.CanvasGroup.interactable = !isBlock;
            Fader.CanvasGroup.blocksRaycasts = !isBlock;
        }
		get
		{
            return isBlock;
		}
	}

    private Fader fader;
    public Fader Fader
	{
		get
		{
            if(fader == null)
			{
                fader = GetComponent<Fader>();
			}
            return fader;
		}
	}

    public GameObject spel;
    private GameObject currentSpell;

    private bool Pressed;

    public void StartOpenButton()
    {
        Fader.FadeIn();
        IsBlock = isBlock;
    }

    private IEnumerator SpellCasting(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        foreach (var item in currentSpell.GetComponentsInChildren<ParticleSystem>())
        {
            item.Stop();
        }
        yield return new WaitForSecondsRealtime(sec);
        Destroy(currentSpell);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isBlock) return;

        Pressed = true;
        currentSpell = Instantiate(spel) as GameObject;
        StartCoroutine(SpellCasting(spelDuration));
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(isBlock) return;

        Pressed = false;
    }


    [ContextMenu("OpenButton")]
    private void OpenButton()
    {
        Fader.CanvasGroup.alpha = 1;
        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
    [ContextMenu("CloseButton")]
    private void CloseButton()
    {
        Fader.CanvasGroup.alpha = 0;
        Fader.CanvasGroup.interactable = false;
        Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
}