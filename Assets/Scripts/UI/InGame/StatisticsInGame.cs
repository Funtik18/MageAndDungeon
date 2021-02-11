using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StatisticsInGame : MonoBehaviour
{
	public HealthCircle healthCircle;
	public TMPro.TextMeshProUGUI moneyCount;

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

	public void StartOpenStatistics()
	{
		Fader.FadeIn();
		Fader.CanvasGroup.interactable = true;
		Fader.CanvasGroup.blocksRaycasts = true;
	}

	[ContextMenu("OpenStatistics")]
	private void OpenStatistics()
	{
		Fader.CanvasGroup.alpha = 1;
		Fader.CanvasGroup.interactable = true;
		Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
	[ContextMenu("CloseStatistics")]
	private void CloseStatistics()
	{
		Fader.CanvasGroup.alpha = 0;
		Fader.CanvasGroup.interactable = false;
		Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
}
