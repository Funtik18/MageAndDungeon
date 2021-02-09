using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<Entity> entities = new List<Entity>();

	public List<Entity> spawnedEntities = new List<Entity>();

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
				Entity entity = Instantiate(entities[i], transform).GetComponent<Entity>();

				entity.transform.SetParent(null);

				spawnedEntities.Add(entity);
				SpawnManager.Instance.AddEntity(entity);
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