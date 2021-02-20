using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WindowLoading : MonoBehaviour
{
    private static WindowLoading instance;
    public static WindowLoading Instance
	{
		get
		{
            if(instance == null)
			{
                instance = FindObjectOfType<WindowLoading>();
			}
            return instance;
		}
	}

    public UnityEvent onOpen;


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


    [SerializeField] private Image loading;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    
    public float FillAmount
	{
		set
		{
            loading.fillAmount = value;
        }
		get
		{
            return loading.fillAmount;
		}
	}

	private void Awake()
	{
        FillAmount = 0;
	}


	public void Open()
	{
        onOpen?.Invoke();

    }
    public void Close()
	{

	}

    public void SetText(string txt)
	{
        text.text = txt;
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
