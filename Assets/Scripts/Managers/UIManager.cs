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

    [Header("Control")]
    public FixedJoystick joystick;
    public JoyButton joybutton;

    private void Awake()
	{
        windowStart.onClosed = WindowStartClossed;

        windowFail.btnReward.onClick.AddListener(delegate {
            windowFail.StopOpenWindow();
            
            AdMobManager.Instance.adMobInterstitial.ShowInterstitial();

            GameManager.Instance.WizardTarget.ReBorn(Stats.MaxHealthPoints/2);

            windowFail.CloseWindow();

            SpawnManager.Instance.ResumeWaves();

            joystick.isBlock = false;
            joybutton.IsBlock = false;
        });

        GameManager.Instance.WizardTarget.onDeath = WizardDeath;
    }
	private void Start()
	{
        windowStart.OpenWindow();
    }

    public void WindowStartClossed()//подготовка ui после нажатия на windowStart
    {
        joystick.StartOpenJoystick();
        joybutton.StartOpenButton();

        statistics.StartOpenStatistics();

        SpawnManager.Instance.StartWaves();
        GameManager.Instance.WizardTarget.StartIncome();
    }

    public void WizardDeath()//подготовка ui после смерти гг
	{
        SpawnManager.Instance.PauseWaves();
        windowFail.StartOpenWindow();

        joystick.isBlock = true;
        joybutton.IsBlock = true;
    }

    public void WinWindow()
	{
        joystick.isBlock = true;
        joybutton.IsBlock = true;

        windowWin.StartOpenWindow();

    }

    public void UpdateStatistics()
	{
        statistics.healthCircle.FillAmount = (float) Stats.CurrentHealthPoints / Stats.MaxHealthPoints;
        statistics.moneyCount.text = Stats.Money.ToString();
    }
}