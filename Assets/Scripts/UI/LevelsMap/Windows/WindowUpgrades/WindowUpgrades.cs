using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WindowUpgrades : MonoBehaviour
{
    private static WindowUpgrades instance;
    public static WindowUpgrades Instance 
    {
		get
		{
            if(instance == null)
			{
                instance = FindObjectOfType<WindowUpgrades>();
			}
            return instance;
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

    public TMPro.TMP_FontAsset fontAsset;
    [Space]
    
    public BookPages book;
    
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private int indexPages = 0;

    public void UpdateUI(PlayerOpportunitiesData data)
	{
        book.pageStats.UpdateStats(data);
        book.pageSpells.UpdateSpells(data);
    }

    public void ResetState()
	{
        book.pageStats.UnChoseSpells();
        book.pageStatInformation.CloseWindow();

        book.pageSpells.UnChoseSpells();
        book.pageSpellInformation.CloseWindow();
    }

    private void UpdatePages()
	{
        if(indexPages == 0)
		{
            book.pageStats.OpenWindow();
            book.pageStatInformation.CloseWindow();

            book.pageSpells.CloseWindow();
            book.pageSpellInformation.CloseWindow();
            book.pageSpells.UnChoseSpells();

            leftButton.GetComponent<Image>().enabled = false;
            rightButton.GetComponent<Image>().enabled = true;

        }
        else if(indexPages == 1)
		{
            book.pageStats.CloseWindow();
            book.pageStatInformation.CloseWindow();
            book.pageStats.UnChoseSpells();

            book.pageSpells.OpenWindow();
            book.pageSpellInformation.CloseWindow();

            leftButton.GetComponent<Image>().enabled = true;
            rightButton.GetComponent<Image>().enabled = false;
        }
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


    [ContextMenu("FlipLeft")]
    public void FlipLeft()
    {
        indexPages = Mathf.Max(0, indexPages - 1);

        UpdatePages();

#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
    [ContextMenu("FlipRight")]
    public void FlipRight()
    {
        indexPages = Mathf.Min(1, indexPages + 1);

        UpdatePages();

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