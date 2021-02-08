﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wizard : MonoBehaviour
{
    public int lifeCount=3;

    public UnityAction onDeath;

    [Header("Line")]
    public LineRenderer line;
    public float lineUpdateTime = 0.1f;
    
    [Header("Circle")]
    public float posY = 1f;
    public float radius;

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
        radius = GameManager.Instance.playerSetup.radius;
        StartLine();
    }

	public void TakeDamage()
	{
        if(lifeCount == 0)
		{
            onDeath?.Invoke();
		}
		else
		{
            lifeCount--;
            GameManager.Instance.hpDecrease(1);
        }
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
        Vector3 oldPos = transform.position + (Vector3.right * radius);
        for(int i = 0; i <= vertexCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), posY, radius * Mathf.Sin(theta));
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