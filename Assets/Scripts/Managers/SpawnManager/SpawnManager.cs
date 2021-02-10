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

    [Header("Time")]
    public float totalWaveTime = 0f;
    [SerializeField] private float currentTime = 0f;
    public float CurrentTime
	{
		set
		{
            currentTime = value;
            totalLeftTime = totalWaveTime - CurrentTime;
		}
		get
		{
            return currentTime;
		}
	}
    public float totalLeftTime = 0f;

    [Header("Entities")]
    [Space]
    [SerializeField] private int totalEntitiesAmount = 0;
    public int TotalEntitiesAmount
	{
		set
		{
            totalEntitiesAmount = value;
            totalLeftToKilled = TotalEntitiesAmount - TotalEntitiesDied;
        }
        get
		{
            return totalEntitiesAmount;
        }
	}

    [Space]
    [SerializeField] private int totalEntitiesSpawned = 0;
    public int TotalEntitiesSpawned 
    {
		set
		{
            totalEntitiesSpawned = value;
            totalLeftToKilled = TotalEntitiesAmount - TotalEntitiesDied;
        }
        get
		{
            return totalEntitiesSpawned;
        }
    }

    [SerializeField] private int totalEntitiesDied = 0;
    public int TotalEntitiesDied
    {
		set
		{
            totalEntitiesDied = value;
            totalLeftToKilled = TotalEntitiesAmount - TotalEntitiesDied;
        }
        get
		{
            return totalEntitiesDied;
		}
	}

    [Space]
    public int totalLeftToKilled = 0;


    
    [Header("Spawns")]
    [Space]
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    public List<Entity> spawnedEntities = new List<Entity>();

    [Header("Debug")]
    public bool isDebug = false;

    public void StartSpawn()
	{
		for(int i = 0; i < spawnPoints.Count; i++)
		{
            spawnPoints[i].StartSpawn();
        }
	}
    public void StopSpawn()
	{
		for(int i = 0; i < spawnPoints.Count; i++)
		{
            spawnPoints[i].StopSpawn();
		}
	}

    public void AddEntity(Entity entity)
	{
		if(!spawnedEntities.Contains(entity))
		{
            spawnedEntities.Add(entity);
            TotalEntitiesSpawned++;
        }
    }
    public void RemoveEntity(Entity entity)
	{
		if(spawnedEntities.Contains(entity))
		{
            spawnedEntities.Remove(entity);
            TotalEntitiesDied++;
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
            //if(spawnPoints[i].spawnOrders.Count > 0) Gizmos.color = Color.green;
            //else Gizmos.color = Color.red;

            Gizmos.DrawSphere(spawnPoints[i].transform.position, 0.2f);
		}
	}
}