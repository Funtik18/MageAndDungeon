﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wizard : MonoBehaviour
{
    public UnityEvent onTakeDamage;

    private PlayerStats stats;
    private PlayerStats Stats
	{
		get
		{
            if(stats == null)
			{
                stats = GameManager.Instance.Stats;
			}
            return stats;
		}
	}

    private bool isDead = false;
    [HideInInspector]public bool isReborn = false;
    public UnityAction onDeath;
    public UnityAction onReborn;

    int RebornsCount;

    [Header("SecondChanceSpel")]
    public SecondChance secondChance;

    [Header("Periodic mage casting")]
    public GameObject selfDefenceSpell;
    public float damage;
    public float timeToCast;
    public int arrowsCount;
    public bool haveSound;
    float time=0;
    AudioSound mySound;

    [Header("Line")]
    public LineRenderer line;
    public float lineUpdateTime = 0.1f;
    
    [Header("Circle")]
    public float posY = 1f;

    private int vertexCount = 40;
    private float ThetaDelta { get { return (2f * Mathf.PI) / vertexCount; } }
    private float theta = 0f;

    [Header("Debug")]
    public bool isDebug = false;

    private List<Vector3> linePoints = new List<Vector3>();

    private Coroutine lineCircleCoroutine = null;
    public bool IsLineCircleProcess => lineCircleCoroutine != null;

    
    private void Awake()
	{
        UIManager.Instance.UpdateStatistics();
		UpdateLinePoints();
		GetStats();
        StartLine();
        if (haveSound)
            mySound = GetComponent<AudioSound>();
        RebornsCount = 2;
    }

    void GetStats()
    {
        arrowsCount = Stats.ArrowsCount;
        timeToCast = Stats.Frequency;
        damage = Stats.DamageOverTime;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (SpawnManager.Instance.spawnedEntities.Count>0 && time>timeToCast)
        {
            time = 0;
            SelfDefence();
        }
    }

    public void TakeDamage(int damage)
	{
        if(isDead) return;

        Stats.CurrentHealthPoints-= damage;
        
        Died();

        UIManager.Instance.UpdateStatistics();

        onTakeDamage?.Invoke();
    }

    public void ReBorn(int countHealthPoint)
	{
        Stats.CurrentHealthPoints = countHealthPoint;
        UIManager.Instance.UpdateStatistics();

        if(Stats.CurrentHealthPoints > 0)
        {
            isDead = false;
            isReborn = true;
            onReborn?.Invoke();
        }
    }
    public void Died()
	{
        if(Stats.CurrentHealthPoints < 1)
        {
            RebornsCount--;
            isDead = true;
            onDeath?.Invoke();
        }
        if (RebornsCount < 0)
        {
            AdMobManager.Instance.adMobInterstitial.ShowInterstitial();
            SceneLoaderManager.Instance.AllowLoadScene();
            SceneLoaderManager.Instance.LoadLevelsMap();
        }
    }

    public void AddMoney(int money)
	{
        Stats.CurrentMoney += (int)(money * Stats.MobScalarProfit);
     
        UIManager.Instance.UpdateStatistics();
    }

    public void SelfDefence()
    {
        for (int i = 0; i < arrowsCount; i++)
        {
            StartCoroutine(periodicDamage());
        }
        
    }

    IEnumerator periodicDamage()
    {
        yield return new WaitForSeconds(timeToCast);
        if (haveSound)
            mySound.PlayAudio();
        Instantiate(selfDefenceSpell,transform);
    }

    #region Line Circle
    private void StartLine()
    {
        if(!IsLineCircleProcess)
        {
            lineCircleCoroutine = StartCoroutine(LineCircle());
        }
    }
    private IEnumerator LineCircle()
    {
        while(true)
        {
            yield return new WaitForSeconds(lineUpdateTime);
            UpdateLinePoints();
        }

        if(IsLineCircleProcess)
        {
            StopCoroutine(lineCircleCoroutine);
            lineCircleCoroutine = null;
        }
    }

    private void UpdateLinePoints()
	{
        linePoints.Clear();
        Vector3 oldPos = transform.position + (Vector3.right * Stats.Radius);
        for(int i = 0; i <= vertexCount; i++)
        {
            Vector3 pos = new Vector3(Stats.Radius * Mathf.Cos(theta), posY, Stats.Radius * Mathf.Sin(theta));
            Vector3 newPos = transform.position + pos;


            oldPos = newPos;

            linePoints.Add(oldPos);
            theta += ThetaDelta;
        }
        UpdateLine();
    }
    private void UpdateLine()
	{
        line.positionCount = linePoints.Count;
		for(int i = 0; i < linePoints.Count; i++)
		{
            line.SetPosition(i, linePoints[i]);
		}

    }
	#endregion

	private void OnDrawGizmos()
    {
        if(!isDebug) return;

        Gizmos.color = Color.green;
        UpdateLinePoints();

		for(int i = 0; i < linePoints.Count-1; i++)
		{
            Gizmos.DrawLine(linePoints[i], linePoints[i+1]);
        }
    }
}