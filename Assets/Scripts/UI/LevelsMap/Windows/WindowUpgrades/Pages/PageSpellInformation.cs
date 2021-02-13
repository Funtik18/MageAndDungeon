using UnityEngine;
using UnityEngine.UI;

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
    [Space]
    [SerializeField] private Button acceptButton;

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

	private SpellUI currentSpell;
	public void ShowSpellInformation(SpellUI spellUI)
	{
		currentSpell = spellUI;

		UpdatePage();

		if(currentSpell.data.level >= 4)
		{
			acceptButton.GetComponent<Image>().enabled = false;
		}
		else
		{
			acceptButton.GetComponent<Image>().enabled = true;

			acceptButton.onClick.RemoveAllListeners();

			if(currentSpell.data.price <= SaveData.Instance.currentGold)
			{
				acceptButton.GetComponent<Image>().color = Color.green;

				acceptButton.onClick.AddListener(AcceptBuy);
			}
			else
			{
				acceptButton.GetComponent<Image>().color = Color.red;
			}
		}

		OpenWindow();
	}

	private void AcceptBuy()
	{
		SaveData.Instance.currentGold -= currentSpell.data.price;

		SaveData.Instance.spellsLevels[currentSpell.statIndex]++;

		SaveLoadManager.Save(SaveLoadManager.mainStatisticPath, SaveData.Instance);

		//upd
		LevelsMapManager.Instance.UpdateUI();
	}

	private void UpdatePage()
	{
		tittle.text = currentSpell.data.russianInfo.name;
		level.text = currentSpell.data.level.ToString();
		description.text = currentSpell.data.russianInfo.description;
		cost.text = currentSpell.data.price.ToString();
		additionalInfo.text = currentSpell.data.russianInfo.additional;
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