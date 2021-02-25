using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PageStatInformation : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI tittle;
    [SerializeField] private TMPro.TextMeshProUGUI level;
    [SerializeField] private TMPro.TextMeshProUGUI description;
    [SerializeField] private TMPro.TextMeshProUGUI changes;
    [SerializeField] private TMPro.TextMeshProUGUI cost;
    [SerializeField] private TMPro.TextMeshProUGUI costT;
    [SerializeField] private TMPro.TextMeshProUGUI additionalInfo;
    [SerializeField] private TMPro.TextMeshProUGUI[] statsPageTitle;
    [Space]
    [SerializeField] private Button acceptButton;

    private Fader fader;
    public Fader Fader
    {
        get
        {
            if (fader == null)
            {
                fader = GetComponent<Fader>();
            }
            return fader;
        }
    }

    private StatUI currentStat;

    public void ShowStatInformation(StatUI stat)
    {
        currentStat = stat;

        UpdatePage();

        if (currentStat.data.level >= 5)
        {
            acceptButton.GetComponent<Image>().enabled = false;
            cost.text = "";
            if (PlayerPrefs.GetInt("language") == 0)
            {
                costT.text = "<color=green>МАКСИМУМ</color>";
            }
            else
            {
                costT.text = "<color=green>MAX</color>";
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("language") == 0)
            {
                costT.text = "ЦЕНА : ";
            }
            else
            {
                costT.text = "COST : ";
            }

            acceptButton.GetComponent<Image>().enabled = true;

            acceptButton.onClick.RemoveAllListeners();

            if (currentStat.data.price <= SaveData.Instance.currentGold)
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
    private void UpdatePage()
    {
        if (PlayerPrefs.GetInt("language") == 0)
        {
            tittle.text = currentStat.data.russianInfo.name;
            description.text = currentStat.data.russianInfo.description;
            additionalInfo.text = currentStat.data.russianInfo.additional;
        }
        else
        {
            tittle.text = currentStat.data.englishInfo.name;
            description.text = currentStat.data.englishInfo.description;
            additionalInfo.text = currentStat.data.englishInfo.additional;
        }

        level.text = currentStat.data.level.ToString();
        changes.text = currentStat.diff;
        cost.text = currentStat.price.ToString();
    }

    private void AcceptBuy()
    {
        SaveData.Instance.currentGold -= currentStat.price;

        SaveData.Instance.statsLevels[currentStat.statIndex]++;

        SaveLoadManager.Save(SaveLoadManager.mainStatisticPath, SaveData.Instance);

        //upd
        LevelsMapManager.Instance.UpdateUI();
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
