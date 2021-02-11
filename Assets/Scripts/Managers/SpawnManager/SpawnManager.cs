using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public LevelStatistics statistics;

    public List<Entity> spawnedEntities = new List<Entity>();

    [Header("Spawns")]
    public SpawnInstruction instruction;


	private void Awake()
	{
        statistics = new LevelStatistics();
    }

    #region Spawn
    private Coroutine spawnCoroutine = null;
    public bool IsSpawnProcess => spawnCoroutine != null; 
    public void StartWaves()
	{
		if(!IsSpawnProcess)
		{
            spawnCoroutine = StartCoroutine(Spawn());
        }
	}
    private IEnumerator Spawn()
	{
        instruction.StartInstruction(this);

        Debug.LogError(instruction.GetTotalEnteties());
        statistics.TotalEntities = instruction.GetTotalEnteties();

        while(instruction.IsInstructionProcess)
		{
            yield return null;
        }
        Debug.LogError("YOU WIN!!!");
        StopWaves();
    }
    public void PauseWaves()
	{

	}
    public void ResumeWaves()
	{

	}
    public void StopWaves()
	{
        if(IsSpawnProcess)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
	#endregion

	public void AddEntity(Entity entity)
	{
		if(!spawnedEntities.Contains(entity))
		{
            spawnedEntities.Add(entity);

            statistics.TotaSpawnedlEntities++;
        }
    }
    public void RemoveEntity(Entity entity)
	{
		if(spawnedEntities.Contains(entity))
		{
            spawnedEntities.Remove(entity);

            statistics.TotalDiedEntities++;
        }
    }

    [System.Serializable]
    public class LevelStatistics
    {
        [SerializeField] private int totalEntities;
        public int TotalEntities
		{
			set
			{
                totalEntities = value;
            }
			get
			{
                return totalEntities;
            }
		}


        [SerializeField] private int totalSpawnedEntities;
        public int TotaSpawnedlEntities
        {
            set
            {
                totalSpawnedEntities = value;
                totalLeftToKill = TotaSpawnedlEntities - TotalDiedEntities;
            }
            get
            {
                return totalSpawnedEntities;
            }
        }

        [SerializeField] private int totalDiedEntities;
        public int TotalDiedEntities
        {
            set
            {
                totalDiedEntities = value;
                totalLeftToKill = TotaSpawnedlEntities - TotalDiedEntities;
            }
            get
            {
                return totalDiedEntities;
            }
        }

        public int totalLeftToKill;

        [SerializeField] private TimeInfo timeInfo;
    }
    [System.Serializable]
    public struct TimeInfo
	{
        public float totalLevelTime;
        [SerializeField] private float currentTime;
        public float CurrentTime
        {
            set
            {
                currentTime = value;
                totalLeftTime = totalLevelTime - CurrentTime;
            }
            get
            {
                return currentTime;
            }
        }
        [SerializeField] private float totalLeftTime;
    }
}

[System.Serializable]
public class SpawnInstruction
{
    public List<SpawnUnite> spawnUnites = new List<SpawnUnite>();

    private MonoBehaviour processOwner;

    [HideInInspector] public bool isPause = false;

	#region Instruction
	private Coroutine instuctionCoroutine = null;
    public bool IsInstructionProcess => instuctionCoroutine != null;
    public void StartInstruction(MonoBehaviour owner)
	{
		if(!IsInstructionProcess)
		{
            processOwner = owner;
            instuctionCoroutine = processOwner.StartCoroutine(Instruction());
		}
	}
    private IEnumerator Instruction()
	{
        for(int i = 0; i < spawnUnites.Count; i++)
		{
            List<SpawnPoint> points = spawnUnites[i].spawnPoints;

            for(int j = 0; j < points.Count; j++)
            {
                points[j].StartSpawn();
            }

            bool skip = false;

            float startTime = Time.time;
            float currentTime = Time.time - startTime;
            float totalTime = spawnUnites[i].GetLongestWaveTime();
            while(currentTime < totalTime)//ждём окончания самой долгой микро волны
            {
                if(currentTime >= totalTime * 0.25f)
                {
                    if(i != spawnUnites.Count - 1)
                    {
                        spawnUnites[i + 1].ShowSkipers(totalTime - currentTime, delegate { skip = true; });
                    }
                }

                currentTime = Time.time - startTime;

				if(skip)
				{

                    break;
                }
                yield return null;
            }
        }

        StopInstruction();
    }
    public void PauseInstruction()
	{

	}
    public void ResumeInstruction()
	{

	}
    public void StopInstruction()
	{
		if(IsInstructionProcess)
		{
            processOwner.StopCoroutine(instuctionCoroutine);
            instuctionCoroutine = null;
		}
	}
	#endregion

    /// <summary>
    /// Всего сущностей.
    /// </summary>
    public int GetTotalEnteties()
	{
        int count = 0;
		for(int i = 0; i < spawnUnites.Count; i++)
		{
            count += spawnUnites[i].GetTotalEntitiesPoint();
        }
        return count;
	}


	[System.Serializable]
    public class SpawnUnite
	{
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public void ShowSkipers(float secs, UnityAction action)
		{
			for(int i = 0; i < spawnPoints.Count; i++)
			{
                action += spawnPoints[i].timer.TimerFadeOut;
            }

            for(int i = 0; i < spawnPoints.Count; i++)
			{
                spawnPoints[i].ShowTimer(secs, action);
			}
		}

        /// <summary>
        /// Сколько всего сушностей на точке спавна
        /// </summary>
        public int GetTotalEntitiesPoint()
		{
            int count = 0;
			for(int i = 0; i < spawnPoints.Count; i++)
			{
                count += spawnPoints[i].GetTotalEntitiesWaves();
			}
            return count;
		}

        public float GetLongestWaveTime()
		{
            float time = 0f;
			for(int i = 0; i < spawnPoints.Count; i++)
			{
                time = Mathf.Max(time, spawnPoints[i].GetTotalTimeWaves());
			}
            return time;
		}
	}
}