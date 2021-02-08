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

    private PlayerStats stats;
    private PlayerStats Stats
	{
		get
		{
            if(stats == null)
			{
                stats = GameManager.Instance.WizardTarget.player.Stats;
			}
            return stats;
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
        GameManager.Instance.WizardTarget.StartIncome();
    }

    public void EndPrepare()
	{
        SpawnManager.Instance.StopSpawn();
        windowFail.StartOpenWindow();
        joystick.isBlock = true;
    }


	public void UpdateStatistics()
	{
        statistics.hpCount.text = Stats.HealthPoints.ToString();
        statistics.moneyCount.text = Stats.Money.ToString();
    }
}