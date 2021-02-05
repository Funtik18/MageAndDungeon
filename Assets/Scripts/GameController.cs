using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public GameObject enemyPref;
    GameObject[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        spawns= GameObject.FindGameObjectsWithTag("Spawn");
        InvokeRepeating("SpawnEnemy", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnEnemy()
    {
        Instantiate(enemyPref, spawns[Random.Range(0, spawns.Length-1)].transform);
    }
}
