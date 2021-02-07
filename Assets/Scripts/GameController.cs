using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
	{
		get
		{
            if(instance == null)
            {
                instance = FindObjectOfType<GameController>();
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
                playerTarget = FindObjectOfType<Mag>().transform;
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
        UIController.Instance.moneyChange(moneyCount);
    }

    public void moneyDepleated(int count)
    {
        moneyCount -= count;
        UIController.Instance.moneyChange(moneyCount);
    }

    public void hpIncrese(int count)
    {
        hpCount += count;
        UIController.Instance.hpChange(hpCount);
    }

    public void hpDecrease(int count)
    {
        hpCount -= count;
        UIController.Instance.hpChange(hpCount);
    }

    void passiveMoneyIncrease()
    {
        moneyIncrease(passiveErning);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        UIController.Instance.moneyChange(moneyCount);
        UIController.Instance.hpChange(hpCount);
        //InvokeRepeating("SpawnEnemy", 0, 1);
        InvokeRepeating("passiveMoneyIncrease", 0, 3);
    }


    void SpawnEnemy()
    {
        Instantiate(enemyPref, spawns[Random.Range(0, spawns.Length - 1)].transform);
    }
}
