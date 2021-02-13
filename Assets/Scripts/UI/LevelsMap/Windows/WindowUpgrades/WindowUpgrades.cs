using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WindowUpgrades : MonoBehaviour
{
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

    public TMPro.TMP_FontAsset fontAsset;
    [Space]
    public BookPages book;

    public void ResetState()
	{
        book.pageSpells.UnChoseSpells();
        book.pageSpellInformation.CloseWindow();
    }


    [ContextMenu("ChangeBookFont")]
    private void ChangeBookFont()
    {
        TMPro.TextMeshProUGUI[] txts = GetComponentsInChildren<TMPro.TextMeshProUGUI>();

		for(int i = 0; i < txts.Length; i++)
		{
            txts[i].font = fontAsset;
		}

#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }

    [ContextMenu("OpenWindow")]
    public void OpenWindow()
    {
        Fader.CanvasGroup.alpha = 1;
        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
    [ContextMenu("CloseWindow")]
    public void CloseWindow()
    {
        Fader.CanvasGroup.alpha = 0;
        Fader.CanvasGroup.interactable = false;
        Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
}