using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
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
                stats = GameManager.Instance.Stats;
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
    public JoyButton fireWall;
    public JoyButton hellishFrost;
    public JoyButton thunderStorm;

    private void Awake()
    {
        windowStart.onClosed = WindowStartClossed;

        windowFail.btnReward.onClick.AddListener(delegate
        {
            windowFail.StopOpenWindow();
            GameManager.Instance.WizardTarget.secondChance.SecondChanceCast();

            AdMobManager.Instance.adMobRewarded.RequestRewardVideo();

            GameManager.Instance.WizardTarget.ReBorn(Stats.MaxHealthPoints / 2);

            windowFail.CloseWindow();

            SpawnManager.Instance.ResumeWaves();

            joystick.isBlock = false;
            hellishFrost.IsBlock = false;
            fireWall.IsBlock = false;
            thunderStorm.IsBlock = false;
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
        fireWall.StartOpenButton();
        hellishFrost.StartOpenButton();
        thunderStorm.StartOpenButton();

        statistics.StartOpenStatistics();

        SpawnManager.Instance.StartWaves();
    }

    public void WizardDeath()//подготовка ui после смерти гг
    {
        SpawnManager.Instance.PauseWaves();
        windowFail.StartOpenWindow();

        joystick.isBlock = true;
        fireWall.IsBlock = true;
        hellishFrost.IsBlock = true;
        thunderStorm.IsBlock = true;
    }

    public void WinWindow()
    {
        joystick.isBlock = true;
        fireWall.IsBlock = true;
        hellishFrost.IsBlock = true;
        thunderStorm.IsBlock = true;

        windowWin.StartOpenWindow();
    }

    public void UpdateStatistics()
    {
        if (!GameManager.Instance.WizardTarget.isReborn)
            statistics.healthCircle.FillAmount = (float)Stats.CurrentHealthPoints / Stats.MaxHealthPoints;
        else
        {
            statistics.healthCircle.Reborned((float)Stats.CurrentHealthPoints / Stats.MaxHealthPoints);
        }

        statistics.moneyCount.text = Stats.CurrentMoney.ToString();
    }
}