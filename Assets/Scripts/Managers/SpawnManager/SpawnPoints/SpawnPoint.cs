using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	public List<SpawnOrder> spawnOrders = new List<SpawnOrder>();

	public List<Entity> spawnedEntities = new List<Entity>();


    private Coroutine spawnCoroutine = null;
    public bool IsSpawnProcess => spawnCoroutine != null;

	public Entity SpawnEntity(Entity entity)
	{
		Entity temp = Instantiate(entity, transform);
		temp.transform.SetParent(null);

		temp.onDied = SpawnManager.Instance.RemoveEntity;//action
		spawnedEntities.Add(temp);
		SpawnManager.Instance.AddEntity(temp);
		return temp;
	}


	#region Spawn
	public void StartSpawn()
	{
		if(!IsSpawnProcess && spawnOrders.Count > 0)
		{
			spawnCoroutine = StartCoroutine(Spawner());
		}
	}
    protected virtual IEnumerator Spawner()
	{
		for(int i = 0; i < spawnOrders.Count; i++)
		{
			SpawnManager.Instance.totalTime += spawnOrders[i].GetTotalTime();
			SpawnManager.Instance.TotalEntitiesAmount += spawnOrders[i].GetTotalEntitiesAmount();

			spawnOrders[i].StartSpawnOrder(this);
			yield return null;
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
	#endregion
}
[System.Serializable]
public class SpawnOrder 
{
	[Tooltip("Сущность которую спавним.")]
	public Entity entity;

	[Range(1, 100)]
	[Tooltip("Сколько 'пачек' сущностей НУЖНО заспавнить.")]
	public int countEntitiesTuples = 10;
	
	[Range(1, 10)]
	[Tooltip("Пачка - Колличество сущностей за один спавн.")]
	public int countEntities = 1;

	[Tooltip("Страртавая задеражка")]
	public float delayStart = 0f;
	[Tooltip("Задержа спавна в пачке.")]
	public float delayInTuple = 0.1f;
	[Tooltip("Как часто происходит спавн? Раз в N секунд.")]
	public float frequency = 0.5f;

	private SpawnPoint spawnOwner;

	private Coroutine spawnOrderCoroutine = null;
	public bool IsSpawnOrderProcess => spawnOrderCoroutine != null;

	public void StartSpawnOrder(SpawnPoint owner)
	{
		if(!IsSpawnOrderProcess)
		{
			spawnOwner = owner;
			spawnOrderCoroutine = spawnOwner.StartCoroutine(Spawn());
		}
	}
	private IEnumerator Spawn()
	{
		yield return new WaitForSecondsRealtime(delayStart);

		int count = 0;
		while(count < countEntitiesTuples)
		{
			for(int j = 0; j < countEntities; j++)//tuple
			{
				spawnOwner.SpawnEntity(entity);

				if(j != countEntities - 1)
				{
					SpawnManager.Instance.CurrentTime += delayInTuple;
					yield return new WaitForSecondsRealtime(delayInTuple);
				}
			}
			
			count++;
			
			if(count != countEntitiesTuples)
			{
				SpawnManager.Instance.CurrentTime += frequency;

				yield return new WaitForSecondsRealtime(frequency);
			}
		}
		StopSpawnOrder();
	}
	public void StopSpawnOrder()
	{
		if(IsSpawnOrderProcess)
		{
			spawnOwner.StopCoroutine(spawnOrderCoroutine);
			spawnOrderCoroutine = null;
		}
	}

	
	public int GetTotalEntitiesAmount()
	{
		return countEntitiesTuples * countEntities;
	}
	public float GetTotalTime()
	{
		return delayStart + (countEntities - 1) * delayInTuple + (countEntitiesTuples - 1) * frequency;
	}
}