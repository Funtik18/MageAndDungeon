using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelGoal : MonoBehaviour
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

    [SerializeField] private Image filler;

    public float FillAmount
	{
		set
		{
            filler.fillAmount = value;
		}
		get
		{
            return filler.fillAmount;
		}
	}

	private void Awake()
	{
        ChangeAmount(0);

        SpawnManager.Instance.statistics.onStatisticsLevelGoalChanged += ChangeAmount;

    }

    private void ChangeAmount(float a)
	{
        FillAmount = a;
    }

	public void StartOpenLevelGoal()
    {
        Fader.FadeIn();
        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
    }


    [ContextMenu("OpenWindow")]
    private void OpenButton()
    {
        Fader.CanvasGroup.alpha = 1;
        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
    [ContextMenu("CloseWindow")]
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