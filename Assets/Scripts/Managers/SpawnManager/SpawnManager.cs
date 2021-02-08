using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SpawnManager>();
            }
            return instance;
        }
    }

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    [Header("Debug")]
    public bool isDebug = false;


    public void StartSpawn()
	{
		for(int i = 0; i < spawnPoints.Count; i++)
		{
            spawnPoints[i].StartSpawn();
        }
	}



    [ContextMenu("GetAllSpawnPoints")]
    private void GetAllSpawnPoints()
	{
        spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
	}


	private void OnDrawGizmos()
	{
        if(!isDebug) return;
        
		for(int i = 0; i < spawnPoints.Count; i++)
		{
            if(spawnPoints[i].entities.Count > 0) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.DrawSphere(spawnPoints[i].transform.position, 0.2f);
		}
	}
}