using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wizard : MonoBehaviour
{
    public PlayerController player;

    public UnityAction onDeath;

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

	public void TakeDamage()
	{
        player.Stats.HealthPoints--;

        UIManager.Instance.UpdateStatistics();
        CheckDeath();
    }

    private void CheckDeath()
	{
        if(player.Stats.HealthPoints < 1)
        {
            onDeath?.Invoke();
        }
    }

    public void StartIncome()
    {
        InvokeRepeating("passiveMoneyIncrease", 0, 3);
    }

    private void passiveMoneyIncrease()
    {
        UIManager.Instance.UpdateStatistics();

        player.Stats.Money += player.Stats.IncomeAmount;
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
        Vector3 oldPos = transform.position + (Vector3.right * player.Stats.Radius);
        for(int i = 0; i <= vertexCount; i++)
        {
            Vector3 pos = new Vector3(player.Stats.Radius * Mathf.Cos(theta), posY, player.Stats.Radius * Mathf.Sin(theta));
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