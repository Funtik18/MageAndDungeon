using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxRadiusData")]
[System.Serializable]
public class StatMaxRadiusData : StatData
{
	public float radius;

	public override string GetDiffrence(StatData data)
	{
		if(data is StatMaxRadiusData stat)
		{
			return "Radius : " + "<color=yellow>" + radius + "</color>" + " > " + "<color=green>" + stat.radius + "</color>";
		}
		return "";
	}
}