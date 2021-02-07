using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public int lifeCount=3;

    [Header("Line")]
    public LineRenderer line;
    public float lineUpdateTime = 0.1f;
    
    [Header("Circle")]
    public float radius;
    public float posY = 1f;

    private int vertexCount = 40;
    private float ThetaDelta { get { return (2f * Mathf.PI) / vertexCount; } }
    private float theta = 0f;

    private List<Vector3> linePoints = new List<Vector3>();

    private Coroutine lineCircleCoroutine = null;
    public bool IsLineCircleProcess => lineCircleCoroutine != null;

	private void Awake()
	{
        StartLine();
    }

	public void TakeDamage()
	{
        if(lifeCount == 0)
            Time.timeScale = 0;
        lifeCount--;
        GameManager.Instance.hpDecrease(1);
        Debug.Log("Жизней осталось " + lifeCount);
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
        Gizmos.color = Color.green;

        UpdateLinePoints();

		for(int i = 0; i < linePoints.Count-1; i++)
		{
            Gizmos.DrawLine(linePoints[i], linePoints[i+1]);
        }
    }
}