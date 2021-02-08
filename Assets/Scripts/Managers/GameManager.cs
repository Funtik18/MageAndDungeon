﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerSetup playerSetup;

    private static GameManager instance;
    public static GameManager Instance
	{
		get
		{
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
	}

    private Wizard wizardTarget;
    public Wizard WizardTarget
	{
		get
		{
            if(wizardTarget == null)
			{
                wizardTarget = FindObjectOfType<Wizard>();
            }
            return wizardTarget;
		}
	}

    private int moneyCount;
    private int hpCount;
    private int passiveErning;

	private void Awake()
	{
        SetMyStats();
        UIManager.Instance.statistics.moneyChange(moneyCount);
        UIManager.Instance.statistics.hpChange(hpCount);
    }

    void SetMyStats()
    {
        moneyCount = playerSetup.startMoney;
        hpCount = playerSetup.hpAmount;
        passiveErning = playerSetup.incomeAmount;
    }

    public void StartProcess()
	{
        InvokeRepeating("passiveMoneyIncrease", 0, 3);
    }


    public void moneyIncrease(int count)
    {
        moneyCount += count;
        UIManager.Instance.statistics.moneyChange(moneyCount);
    }

    public void moneyDepleated(int count)
    {
        moneyCount -= count;
        UIManager.Instance.statistics.moneyChange(moneyCount);
    }

    public void hpIncrese(int count)
    {
        hpCount += count;
        UIManager.Instance.statistics.hpChange(hpCount);
    }

    public void hpDecrease(int count)
    {
        hpCount -= count;
        UIManager.Instance.statistics.hpChange(hpCount);
    }

    private void passiveMoneyIncrease()
    {
        moneyIncrease(passiveErning);
    }
}