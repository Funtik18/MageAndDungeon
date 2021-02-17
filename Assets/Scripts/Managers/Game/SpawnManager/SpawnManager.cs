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

    public LevelStatistics statistics = new LevelStatistics();

    public List<Entity> spawnedEntities = new List<Entity>();

    [Header("Spawns")]
    public SpawnInstruction instruction;

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

        statistics.TotalEntities = instruction.GetTotalEnteties();
        statistics.totalGold = instruction.GetTotalGold();
        statistics.totalLevelTime = instruction.GetTotalMaxTime();

        while(instruction.IsInstructionProcess)
		{
            yield return null;
        }

        Debug.LogError("Добей выживших!!!");

        while(statistics.totalLeftToKill != 0)
		{
            yield return null;
		}

        Debug.LogError("WIN!!!");

        UIManager.Instance.WinWindow();

        StopWaves();
    }
    public void PauseWaves()
	{
        instruction.isPause.value = true;
    }
    public void ResumeWaves()
	{
        instruction.isPause.value = false;
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
        public UnityAction onStatisticsChange;
        public UnityAction<float> onStatisticsLevelGoalChanged;

        [Header("LevelGoal")]
        [SerializeField] private float levelGoal;
        public float LevelGoal
		{
			set
			{
                levelGoal = value;
                onStatisticsLevelGoalChanged(levelGoal);
            }
			get
			{
                return levelGoal;
			}
		}


        [Header("Entities")]
        [SerializeField] private int totalEntities;
        public int TotalEntities
		{
			set
			{
                totalEntities = value;

                LevelGoal = (float) totalDiedEntities / totalEntities;
            }
			get
			{
                return totalEntities;
            }
		}
        [Space]
        [SerializeField] private int totalSpawnedEntities;
        public int TotaSpawnedlEntities
        {
            set
            {
                totalSpawnedEntities = value;
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
                totalLeftToKill = TotalEntities - TotalDiedEntities;

                LevelGoal = (float) totalDiedEntities / totalEntities;
            }
            get
            {
                return totalDiedEntities;
            }
        }

        public int totalLeftToKill;

        [Header("Gold")]
        public int totalGold;

        [Header("Time")]
        public float totalLevelTime;
        public float storedTime;
    }
}

[System.Serializable]
public class SpawnInstruction
{
    public VariableBoolean isPause = new VariableBoolean(false);
    public VariableBoolean isLoop = new VariableBoolean(false);

    public List<SpawnUnite> spawnUnites = new List<SpawnUnite>();

    private MonoBehaviour processOwner;

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
        //pause setup
        for(int i = 0; i < spawnUnites.Count; i++)
        {
            List<SpawnPoint> points = spawnUnites[i].spawnPoints;

            for(int j = 0; j < points.Count; j++)
            {
                points[j].SetPauseRef(isPause);
            }
        }


		while(true)
		{
            for(int i = 0; i < spawnUnites.Count; i++)
            {
                List<SpawnPoint> points = spawnUnites[i].spawnPoints;

                for(int j = 0; j < points.Count; j++)
                {
                    points[j].StartSpawn();
                }

                bool skip = false;
                bool showTimersOnce = false;
                float startTime = Time.time;
                float currentTime = Time.time - startTime;
                float totalTime = spawnUnites[i].GetLongestWaveTime();
                while(currentTime < totalTime)//ждём окончания самой долгой микро волны
                {
                    if(showTimersOnce == false)
                        if(i != spawnUnites.Count - 1)
                        {
                            SpawnUnite unite = spawnUnites[i + 1];
                            if(currentTime >= totalTime * unite.skipWaiter)
                            {
                                unite.ShowSkipers(totalTime - currentTime, delegate { skip = true; });
                                showTimersOnce = true;
                            }
                        }

                    currentTime = Time.time - startTime;

                    while(isPause.value)
                    {
                        startTime += Time.deltaTime;
                        yield return null;
                    }

                    if(skip)
                    {
                        //сколько выйграл времeни
                        SpawnManager.Instance.statistics.storedTime += (totalTime - currentTime);
                        break;
                    }
                    yield return null;
                }
            }

            Debug.Log("Circle");
            if(isLoop.value == false) break;

            yield return null;
        }

       

        StopInstruction();
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

    /// <summary>
    /// Всего золота.
    /// </summary>
    public int GetTotalGold()
	{
        int gold = 0;
        for(int i = 0; i < spawnUnites.Count; i++)
        {
            gold += spawnUnites[i].GetTotalGoldPoint();
        }
        return gold;
	}

    /// <summary>
    /// Максимальная продолжительность уровня +-.
    /// </summary>
    public float GetTotalMaxTime()
    {
        float time = 0f;
        for(int i = 0; i < spawnUnites.Count; i++)
        {
            time += spawnUnites[i].GetLongestWaveTime();
        }
        return time;
    }

    [System.Serializable]
    public class SpawnUnite
    {
        [Tooltip("Возможность пропустить ожидание для появления следующей волны(totalTime * skipWaiter)")]
        [Range(0f, 1f)]
        public float skipWaiter = 1f;
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public void ShowSkipers(float secs, UnityAction action)
        {
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                action += spawnPoints[i].timer.IncreaseSpeed;
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
        public int GetTotalGoldPoint()
		{
            int count = 0;
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                count += spawnPoints[i].GetTotalGoldWaves();
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


[System.Serializable]
public class WaveOrder
{
    public SpawnWaveData waveData;

    private List<SpawnOrder> spawnOrders = new List<SpawnOrder>();

    private SpawnPoint spawnOwner;

    private VariableBoolean isPause;

    #region Wave
    private Coroutine spawnWaveCoroutine = null;
    public bool IsSpawnWaveProcess => spawnWaveCoroutine != null;

    public void StartSpawnWave(SpawnPoint owner, VariableBoolean pauseParam)
    {
        if(!IsSpawnWaveProcess)
        {
            spawnOwner = owner;
            isPause = pauseParam;
            spawnWaveCoroutine = spawnOwner.StartCoroutine(SpawnWave());
        }
    }
    private IEnumerator SpawnWave()
    {
        for(int i = 0; i < waveData.spawnOrdersData.Count; i++)
        {
            SpawnOrder spawnOrder = new SpawnOrder(waveData.spawnOrdersData[i], isPause);
            spawnOrder.StartSpawnOrder(spawnOwner);

            spawnOrders.Add(spawnOrder);
        }

        float startTime = Time.time;
        float totalWaveTime = GetTotalTimeWave();
        float currentTime = Time.time - startTime;

        while(currentTime < totalWaveTime)
        {
            currentTime = Time.time - startTime;

            while(isPause.value)
            {
                startTime += Time.deltaTime;
                yield return null;
            }

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

        for(int i = 0; i < waveData.spawnOrdersData.Count; i++)
        {
            count += waveData.spawnOrdersData[i].GetTotalEntities();
        }
        return count;
    }

    public int GetTotalGoldWave()
	{
        int count = 0;

        for(int i = 0; i < waveData.spawnOrdersData.Count; i++)
        {
            count += waveData.spawnOrdersData[i].GetTotalGold();
        }
        return count;
    }

    /// <summary>
    ///	Сколько продлится волна.
    /// </summary>
    public float GetTotalTimeWave()
    {
        float time = 0f;
        for(int i = 0; i < waveData.spawnOrdersData.Count; i++)
        {
            time = Mathf.Max(time, waveData.spawnOrdersData[i].GetTotalTimeSpawn());
        }
        return time + waveData.time.addTime;
    }
}

[System.Serializable]
public class SpawnOrder
{
    private SpawnOrderData orderData;
    private SpawnPoint spawnOwner;

    private VariableBoolean isPause;

    public SpawnOrder(SpawnOrderData data, VariableBoolean pauseParam)
	{
        orderData = data;
        isPause = pauseParam;
    }

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
        while(count < orderData.countTuples)
        {
            for(int j = 0; j < orderData.countEntitiesInTuples; j++)//tuple
            {
                spawnOwner.SpawnEntity(orderData.data);

                if(j != orderData.countEntitiesInTuples - 1)
                {
                    yield return new WaitForSecondsRealtime(orderData.time.delayInTuple);
                }
            }

            count++;

            if(count != orderData.countTuples)
            {
                yield return new WaitForSecondsRealtime(orderData.time.frequency);
            }


			while(isPause.value)
			{
                yield return null;
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
}