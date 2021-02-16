using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wizard : MonoBehaviour
{
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

    [Header("SecondChanceSpel")]
    public SecondChance secondChance;

    [Header("Periodic mage casting")]
    public GameObject selfDefenceSpell;
    public float timeToCast;
    float time=0;

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

        StartLine();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (!SpawnManager.Instance.instruction.isPause.value && time>timeToCast)
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
    }

    public void ReBorn(int countHealthPoint)
	{
        Stats.CurrentHealthPoints = countHealthPoint;

        if(Stats.CurrentHealthPoints > 0)
        {
            isDead = false;
            isReborn = true;
        }
            
	}
    public void Died()
	{
        if(Stats.CurrentHealthPoints < 1)
        {
            isDead = true;
            onDeath?.Invoke();
        }
    }

    public void AddMoney(int money)
	{
        Stats.CurrentMoney += money;
     
        UIManager.Instance.UpdateStatistics();
    }

    public void SelfDefence()
    {
        StartCoroutine(periodicDamage());
    }

    IEnumerator periodicDamage()
    {
        yield return new WaitForSeconds(timeToCast);
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