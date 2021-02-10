using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	[Header("UI")]
	public SpawnTimer timer;

	[Header("SpawnMachine")]
	public List<Entity> spawnedEntities = new List<Entity>();

	public List<WaveOrder> waves = new List<WaveOrder>();

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

	public void StartSpawn()
	{
		if(!IsSpawnProcess)
		{
			spawnCoroutine = StartCoroutine(Spawner());
		}
	}
	private IEnumerator Spawner()
	{
		for(int i = 0; i < waves.Count; i++)
		{
			float startWaveTime = Time.time;

			WaveOrder wave = waves[i];
			wave.StartSpawnWave(this);

			bool startTimerOnce = true;
			bool breakSkip = false;
			while(wave.IsSpawnWaveProcess)
			{
				if(i != waves.Count - 1)
				{
					if(startTimerOnce)
						if(wave.currentTime >= wave.totalWaveTime * 0.25f)//если прошло 1/4 волны
						{
							timer.onTap = delegate
							{
								breakSkip = true;
							};
							timer.StartTimer(wave.totalWaveTime - wave.currentTime);
							startTimerOnce = false;
						}
					if(breakSkip)
						break;
				}

				yield return null;
			}
		}

		Debug.LogError("Волны завершились, нужна проверка на мёртвых");

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

[System.Serializable]
public class SpawnOrder 
{
	[Tooltip("Сущность которую спавним.")]
	public Entity entity;

	[Range(1, 1000)]
	[Tooltip("Сколько 'пачек' сущностей НУЖНО заспавнить.")]
	public int countTuples = 10;
	
	[Range(1, 100)]
	[Tooltip("Пачка - Колличество сущностей за один спавн.")]
	public int countEntitiesInTuples = 1;

	[Tooltip("Страртовая задеражка")]
	public float delayStart = 0f;
	[Tooltip("Задержа спавна в пачке.")]
	public float delayInTuple = 0f;
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
		while(count < countTuples)
		{
			for(int j = 0; j < countEntitiesInTuples; j++)//tuple
			{
				spawnOwner.SpawnEntity(entity);

				if(j != countEntitiesInTuples - 1)
				{
					SpawnManager.Instance.CurrentTime += delayInTuple;
					yield return new WaitForSecondsRealtime(delayInTuple);
				}
			}
			
			count++;
			
			if(count != countTuples)
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
		return countTuples * countEntitiesInTuples;
	}
	public float GetTotalTime()
	{
		return delayStart + (countEntitiesInTuples - 1) * delayInTuple + (countTuples - 1) * frequency;
	}
}

[System.Serializable]
public class WaveOrder
{
	[Tooltip("Сколько должна продлиться волна.")]
	public float totalWaveTime = 0f;
	public float currentTime;

	[Space]
	[Tooltip("Время, с течением которого заспавнется вся волна.")]
	public float totalSpawnWaveTime = 0f;
	[Tooltip("Дополнительное время для завершения волны (Время на подготовку к следующей волне).")]
	public float totalSpawnWaveAddTime = 1f;


	public List<SpawnOrder> spawnOrders = new List<SpawnOrder>();

	private Coroutine spawnWaveCoroutine = null;
	public bool IsSpawnWaveProcess => spawnWaveCoroutine != null;

	private SpawnPoint spawnOwner;

	public void StartSpawnWave(SpawnPoint owner)
	{
		if(!IsSpawnWaveProcess)
		{
			spawnOwner = owner;
			spawnWaveCoroutine = spawnOwner.StartCoroutine(SpawnWave());
		}
	}
	protected virtual IEnumerator SpawnWave()
	{
		float startTime = Time.time;

		for(int i = 0; i < spawnOrders.Count; i++)
		{
			totalSpawnWaveTime = Mathf.Max(totalSpawnWaveTime, spawnOrders[i].GetTotalTime());
			SpawnManager.Instance.totalWaveTime = Mathf.Max(SpawnManager.Instance.totalWaveTime, totalSpawnWaveTime);
			SpawnManager.Instance.TotalEntitiesAmount += spawnOrders[i].GetTotalEntitiesAmount();

			spawnOrders[i].StartSpawnOrder(spawnOwner);
		}

		totalWaveTime = totalSpawnWaveTime + totalSpawnWaveAddTime;

		currentTime = Time.time - startTime;
		while(currentTime < totalWaveTime)
		{
			currentTime = Time.time - startTime;
			yield return null;
		}

		//Debug.LogError("Волна завершилась");

		StopSpawnWave();
	}
	public void StopSpawnWave()
	{
		if(IsSpawnWaveProcess)
		{
			spawnOwner.StopCoroutine(spawnWaveCoroutine);
			spawnWaveCoroutine = null;
		}
	}
}