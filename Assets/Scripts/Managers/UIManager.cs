using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
	{
		get
		{
            if(instance == null)
			{
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
	}

    public StatisticsInGame statistics;

    [Header("Windows")]
    public WindowStartGame windowStart;
    public WindowFailGame windowFail;
    public WindowWinGame windowWin;

    [Header("Joystick")]
    public FixedJoystick joystick;

	private void Awake()
	{
        windowStart.onClosed = StartPrepare;

        GameManager.Instance.WizardTarget.onDeath = EndPrepare;
    }
	private void Start()
	{
        windowStart.OpenWindow();
    }

    public void StartPrepare()
	{
        joystick.StartOpenJoystick();
        statistics.StartOpenStatistics();

        SpawnManager.Instance.StartSpawn();
        GameManager.Instance.StartProcess();
    }

    public void EndPrepare()
	{
        windowFail.StartOpenWindow();
        joystick.isBlock = true;
    }
}