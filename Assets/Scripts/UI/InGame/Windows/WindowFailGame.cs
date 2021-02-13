using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WindowFailGame : MonoBehaviour
{
    public PanelInteraction interaction;
    public Button btnReward;
    
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

    private Coroutine windowCoroutine = null;
    public bool IsWindowProcess => windowCoroutine != null;

    public void StartOpenWindow()
    {
        if (!IsWindowProcess)
        {
            interaction.onTap.AddListener(delegate { SceneLoaderManager.Instance.AllowLoadScene(); });
            windowCoroutine = StartCoroutine(Open());
        }
    }
    private IEnumerator Open()
    {
        SceneLoaderManager.Instance.LoadLevelsMap();

        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
        Fader.FadeIn();

        while (Fader.IsFadeProcess)
        {
            yield return null;
        }

        StopOpenWindow();
    }
    public void StopOpenWindow()
    {
        if (IsWindowProcess)
        {
            StopCoroutine(windowCoroutine);
            windowCoroutine = null;
        }
    }

    [ContextMenu("OpenWindow")]
    private void OpenWindow()
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
