using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public UnityAction onStart;

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

    public LevelGoal levelGoal;

    [Header("Windows")]
    public WindowStartGame windowStart;
    public WindowFailGame windowFail;
    public WindowWinGame windowWin;

    [Header("Control")]
    public FixedJoystick joystick;
    public JoyButton fireWall;
    public JoyButton hellishFrost;
    public JoyButton thunderStorm;

    [HideInInspector] public bool isEducation = false;


    private void Awake()
    {
		if(!isEducation)
		{
            if(!SaveData.Instance.isHaveFist)
			{
                fireWall.isBlockedSure = true;
            }
            if(!SaveData.Instance.isHaveFrost)
            {
                hellishFrost.isBlockedSure = true;
            }
            if(!SaveData.Instance.isHaveStorm)
            {
                thunderStorm.isBlockedSure = true;
            }
        }


        windowStart.onClosed = WindowStartClossed;

        windowFail.onRewarded = PlayerReborn;


        GameManager.Instance.WizardTarget.onDeath = WizardDeath;
    }
    private void Start()
    {
        windowStart.OpenWindow();
    }

    private void PlayerReborn()
	{
            Time.timeScale = 1;

            windowFail.CloseWindow();

            GameManager.Instance.WizardTarget.secondChance.SecondChanceCast();
            GameManager.Instance.WizardTarget.ReBorn(Stats.MaxHealthPoints / 2);

            SpawnManager.Instance.ResumeWaves();

            joystick.isBlock = false;
            hellishFrost.IsBlock = false;
            fireWall.IsBlock = false;
            thunderStorm.IsBlock = false;
    }


    public void WindowStartClossed()//подготовка ui после нажатия на windowStart
    {
        joystick.StartOpenJoystick();
        fireWall.StartOpenButton();
        hellishFrost.StartOpenButton();
        thunderStorm.StartOpenButton();

        levelGoal.StartOpenLevelGoal();

        statistics.StartOpenStatistics();

        if(!isEducation)
            SpawnManager.Instance.StartWaves();

        onStart?.Invoke();
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



    public void ShowEducation0()
	{

	}
}