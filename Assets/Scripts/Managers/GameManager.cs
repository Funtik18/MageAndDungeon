using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
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

    private Transform playerTarget;
    public Transform PlayerTarget
	{
		get
		{
            if(playerTarget == null)
			{
                playerTarget = FindObjectOfType<Wizard>().transform;
            }
            return playerTarget;
		}
	}

    public GameObject enemyPref;
    private GameObject[] spawns;

    [SerializeField] private int moneyCount;
    [SerializeField] private int hpCount;
    [SerializeField] private int passiveErning;

    public void moneyIncrease(int count)
    {
        moneyCount += count;
        UIManager.Instance.moneyChange(moneyCount);
    }

    public void moneyDepleated(int count)
    {
        moneyCount -= count;
        UIManager.Instance.moneyChange(moneyCount);
    }

    public void hpIncrese(int count)
    {
        hpCount += count;
        UIManager.Instance.hpChange(hpCount);
    }

    public void hpDecrease(int count)
    {
        hpCount -= count;
        UIManager.Instance.hpChange(hpCount);
    }

    void passiveMoneyIncrease()
    {
        moneyIncrease(passiveErning);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        UIManager.Instance.moneyChange(moneyCount);
        UIManager.Instance.hpChange(hpCount);
        //InvokeRepeating("SpawnEnemy", 0, 1);
        InvokeRepeating("passiveMoneyIncrease", 0, 3);
    }


    void SpawnEnemy()
    {
        Instantiate(enemyPref, spawns[Random.Range(0, spawns.Length - 1)].transform);
    }
}
