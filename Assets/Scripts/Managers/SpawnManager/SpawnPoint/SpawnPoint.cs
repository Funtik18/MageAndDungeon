using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<Entity> entities = new List<Entity>();

    private Coroutine spawnCoroutine = null;
    public bool IsSpawnProcess => spawnCoroutine != null;

	public void StartSpawn()
	{
		if(!IsSpawnProcess && entities.Count > 0)
		{
			spawnCoroutine = StartCoroutine(Spawner());
		}
	}
    protected virtual IEnumerator Spawner()
	{
		yield return new WaitForSeconds(Random.Range(0f, 2.5f));

		while(true)
		{
			for(int i = 0; i < entities.Count; i++)
			{
				Instantiate(entities[i],transform);
				yield return new WaitForSeconds(3f);
			}
		}

		StopSpawn();
	}
    public void StopSpawn()
	{
		if(IsSpawnProcess)
		{
			StopCoroutine(spawnCoroutine);
			spawnCoroutine = null;
		}
	}
}