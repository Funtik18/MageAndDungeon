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

    public List<Transform> spawnPoints = new List<Transform>();

    [Header("Debug")]
    public bool isDebug = false;

    [ContextMenu("GetAllSpawnPoints")]
    private void GetAllSpawnPoints()
	{
        spawnPoints = GetComponentsInChildren<Transform>().ToList();
        spawnPoints.RemoveAt(0);
	}


	private void OnDrawGizmos()
	{
        if(!isDebug) return;

        
		for(int i = 0; i < spawnPoints.Count; i++)
		{
            if(spawnPoints[i].GetComponent<SpawnPoint>().entities.Count > 0) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.DrawSphere(spawnPoints[i].position, 0.2f);
		}
	}
}