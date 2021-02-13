using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PageSpellInformation : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI tittle;
    [SerializeField] private TMPro.TextMeshProUGUI level;
    [SerializeField] private TMPro.TextMeshProUGUI description;
    [SerializeField] private TMPro.TextMeshProUGUI cost;
    [SerializeField] private TMPro.TextMeshProUGUI additionalInfo;

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

    public void ShowSpellInformation(SpellData data)
	{
        tittle.text = data.spellName;
        level.text = "0";
        description.text = data.spellDiscription;
        cost.text = data.nextLevel[0].newPrice.ToString();
        additionalInfo.text = data.spellAdditionalInfo;

        OpenWindow();
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