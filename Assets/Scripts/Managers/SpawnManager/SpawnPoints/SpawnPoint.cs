using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPoint : MonoBehaviour
{
	[Header("UI")]
	public SpawnTimer timer;

	[Header("SpawnMachine")]
	[HideInInspector] public List<Entity> spawnedEntities = new List<Entity>();

	public List<WaveOrder> waves = new List<WaveOrder>();

	private Coroutine spawnCoroutine = null;
	public bool IsSpawnProcess => spawnCoroutine != null;

	public void StartSpawn()
	{
		if(!IsSpawnProcess && waves.Count > 0)
		{
			spawnCoroutine = StartCoroutine(Spawner());
		}
	}
	private IEnumerator Spawner()
	{
		for(int i = 0; i < waves.Count; i++)
		{
			WaveOrder wave = waves[i];
			wave.StartSpawnWave(this);

			while(wave.IsSpawnWaveProcess)
			{
				yield return null;
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

	/// <summary>
	/// Заспавнить существо.
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public Entity SpawnEntity(Entity entity)
	{
		Entity temp = Instantiate(entity, transform);
		temp.transform.SetParent(null);

		temp.onDied = SpawnManager.Instance.RemoveEntity;//action
		spawnedEntities.Add(temp);
		SpawnManager.Instance.AddEntity(temp);
		return temp;
	}

	public void ShowTimer(float secs, UnityAction action)
	{
		timer.StartTimer(secs, action);
	}

	/// <summary>
	/// Сколько всего сущностей на микро-волнах
	/// </summary>
	public int GetTotalEntitiesWaves()
	{
		int count = 0;
		for(int i = 0; i < waves.Count; i++)
		{
			count += waves[i].GetTotalEntittiesWave();
		}
		return count;
	}

	/// <summary>
	/// Сколько продлятся микро-волны
	/// </summary>
	public float GetTotalTimeWaves()
	{
		float time = 0f;
		for(int i = 0; i < waves.Count; i++)
		{
			time += waves[i].GetTotalTimeWave();
		}
		return time;
	}
}

[System.Serializable]
public class WaveOrder
{
	public List<SpawnOrder> spawnOrders = new List<SpawnOrder>();
	
	[SerializeField] private TimeInfo timeInfo;

	private SpawnPoint spawnOwner;

	#region Wave
	private Coroutine spawnWaveCoroutine = null;
	public bool IsSpawnWaveProcess => spawnWaveCoroutine != null;

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
		for(int i = 0; i < spawnOrders.Count; i++)
		{
			spawnOrders[i].StartSpawnOrder(spawnOwner);
		}

		timeInfo.StartTime();
		timeInfo.totalWaveTime = GetTotalTimeWave();
		timeInfo.UpdateCurrentTme();

		while(timeInfo.currentTime < timeInfo.totalWaveTime)
		{
			timeInfo.UpdateCurrentTme();
			yield return null;
		}

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
	#endregion

	/// <summary>
	/// Сколько должно заспавнится сущностей на волне.
	/// </summary>
	public int GetTotalEntittiesWave()
	{
		int count = 0;
		for(int i = 0; i < spawnOrders.Count; i++)
		{
			count += spawnOrders[i].GetTotalEntities();
		}
		return count;
	}

	/// <summary>
	///	Сколько продлится волна.
	/// </summary>
	public float GetTotalTimeWave()
	{
		float time = 0f;
		for(int i = 0; i < spawnOrders.Count; i++)
		{
			time = Mathf.Max(time, spawnOrders[i].GetTotalTimeSpawn());
		}
		return time + timeInfo.addTime;
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

		[SerializeField] private TimeInfo timeInfo;

		private SpawnPoint spawnOwner;

		#region SpawnOrder
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
			int count = 0;
			while(count < countTuples)
			{
				for(int j = 0; j < countEntitiesInTuples; j++)//tuple
				{
					spawnOwner.SpawnEntity(entity);

					if(j != countEntitiesInTuples - 1)
					{
						yield return new WaitForSecondsRealtime(timeInfo.delayInTuple);
					}
				}

				count++;

				if(count != countTuples)
				{
					yield return new WaitForSecondsRealtime(timeInfo.frequency);
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
		#endregion

		public int GetTotalEntities()
		{
			return countTuples * countEntitiesInTuples;
		}
		public float GetTotalTimeSpawn()
		{
			return (countEntitiesInTuples - 1) * timeInfo.delayInTuple + (countTuples - 1) * timeInfo.frequency;
		}

		[System.Serializable]
		private struct TimeInfo
		{
			[Tooltip("Задержа спавна в пачке.")]
			public float delayInTuple;
			[Tooltip("Как часто происходит спавн пачек? Раз в N секунд.")]
			public float frequency;
		}
	}

	[System.Serializable]
	private struct TimeInfo
	{
		[Tooltip("Дополнительное время для завершения волны (Время на подготовку к следующей волне).")]
		public float addTime;
		[Space]
		[Space]
		[Tooltip("Сколько должна продлиться волна.")]
		public float totalWaveTime;
		public float currentTime;
		public float startTime;
		public float LeftTime
		{
			get
			{
				return totalWaveTime - currentTime;
			}
		}


		public void StartTime()
		{
			startTime = Time.time;
		}
		public void UpdateCurrentTme()
		{
			currentTime = Time.time - startTime;
		}
	}
}