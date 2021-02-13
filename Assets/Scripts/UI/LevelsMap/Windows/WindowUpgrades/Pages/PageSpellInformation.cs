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

	private PageSpells.SpellUIData currentData;
	public void ShowSpellInformation(PageSpells.SpellUIData data)
	{
		currentData = data;

		UpdatePage();

		if(currentData.level >= 4)
		{
			acceptButton.GetComponent<Image>().enabled = false;
		}
		else
		{
			acceptButton.GetComponent<Image>().enabled = true;

			acceptButton.onClick.RemoveAllListeners();

			if(currentData.price <= SaveData.Instance.currentGold)
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
		SaveData.Instance.currentGold -= currentData.price;
		SaveData.Instance.currentLevelSpells[currentData.spellIndex]++;

		SaveLoadManager.Save(SaveLoadManager.mainStatisticPath, SaveData.Instance);

		//upd
		MainStatistics.Instance.UpdateUI();

		currentData.UpdateData();
		ShowSpellInformation(currentData);
	}

	private void UpdatePage()
	{
		tittle.text = currentData.name;
		level.text = currentData.level.ToString();
		description.text = currentData.description;
		cost.text = currentData.price.ToString();
		additionalInfo.text = currentData.additionalInfo;
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