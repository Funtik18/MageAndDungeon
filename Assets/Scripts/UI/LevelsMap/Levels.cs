using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Levels : MonoBehaviour
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

    [SerializeField] private List<Level> levels = new List<Level>();

	private void Awake()
	{
        int currentLevel = SaveLoadManager.Instance.CurrentLevel;

        if(currentLevel >= levels.Count) { Debug.LogError("PlayerPrefs error"); return; }

        for(int i = 0; i <= currentLevel; i++)
		{
            levels[i].GetComponent<Image>().color = Color.green;
        }
		for(int i = currentLevel + 1; i < levels.Count; i++)
		{
            levels[i].GetComponent<Button>().interactable = false;
		}
	}
    public void LoadBrawlScene()
	{
        SceneLoaderManager.Instance.LoadLevelBrawl();
        StartCoroutine(SceneWaiter());
    }

    private IEnumerator SceneWaiter()
    {
        while(!SceneLoaderManager.Instance.IsLoadComplited)
        {
            yield return null;
        }
        SceneLoaderManager.Instance.AllowLoadScene();
    }


    [ContextMenu("GetAllLevels")]
    private void GetAllLevels()
    {
        levels = GetComponentsInChildren<Level>().ToList();
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }

    [ContextMenu("OpenLevels")]
    private void OpenButton()
    {
        Fader.CanvasGroup.alpha = 1;
        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
    [ContextMenu("CloseLevels")]
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