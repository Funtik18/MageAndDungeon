using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPoint : MonoBehaviour
{
	[Header("UI")]
	public SpawnTimer timer;

	[Header("SpawnMachine")]

	public List<WaveOrder> wavesOrders = new List<WaveOrder>();

	private Coroutine spawnCoroutine = null;
	public bool IsSpawnProcess => spawnCoroutine != null;
	
	private VariableBoolean isPause;

	public void SetPauseRef(VariableBoolean pause)
	{
		isPause = pause;
		timer.isPause = pause;
	}

	public void StartSpawn()
	{
		if(!IsSpawnProcess && wavesOrders.Count > 0)
		{
			spawnCoroutine = StartCoroutine(Spawner());
		}
	}
	private IEnumerator Spawner()
	{
		for(int i = 0; i < wavesOrders.Count; i++)
		{
			WaveOrder waveOrder = wavesOrders[i];
			waveOrder.StartSpawnWave(this, isPause);

			while(waveOrder.IsSpawnWaveProcess)
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
	public Entity SpawnEntity(EntityData data)
	{
		Entity temp = Instantiate(data.entity, transform);
		temp.transform.SetParent(null);

		temp.transform.position += new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

		temp.onDied = SpawnManager.Instance.RemoveEntity;//action
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
		for(int i = 0; i < wavesOrders.Count; i++)
		{
			count += wavesOrders[i].GetTotalEntittiesWave();
		}
		return count;
	}

	public int GetTotalGoldWaves()
	{
		int count = 0;
		for(int i = 0; i < wavesOrders.Count; i++)
		{
			count += wavesOrders[i].GetTotalGoldWave();
		}
		return count;
	}

	/// <summary>
	/// Сколько продлятся микро-волны
	/// </summary>
	public float GetTotalTimeWaves()
	{
		float time = 0f;
		for(int i = 0; i < wavesOrders.Count; i++)
		{
			time += wavesOrders[i].GetTotalTimeWave();
		}
		return time;
	}


	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position, 0.2f);
	}
}