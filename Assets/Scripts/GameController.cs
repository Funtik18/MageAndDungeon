using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController instance;

    public GameObject enemyPref;
    GameObject[] spawns;

    [SerializeField] int moneyCount;
    [SerializeField] int hpCount;
    [SerializeField] int passiveErning;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void moneyIncrease(int count)
    {
        moneyCount += count;
        UIController.instance.moneyChange(moneyCount);
    }

    public void moneyDepleated(int count)
    {
        moneyCount -= count;
        UIController.instance.moneyChange(moneyCount);
    }

    public void hpIncrese(int count)
    {
        hpCount += count;
        UIController.instance.hpChange(hpCount);
    }

    public void hpDecrease(int count)
    {
        hpCount -= count;
        UIController.instance.hpChange(hpCount);
    }

    void passiveMoneyIncrease()
    {
        moneyIncrease(passiveErning);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        UIController.instance.moneyChange(moneyCount);
        UIController.instance.hpChange(hpCount);
        //InvokeRepeating("SpawnEnemy", 0, 1);
        InvokeRepeating("passiveMoneyIncrease", 0, 3);
    }


    void SpawnEnemy()
    {
        Instantiate(enemyPref, spawns[Random.Range(0, spawns.Length - 1)].transform);
    }
}
