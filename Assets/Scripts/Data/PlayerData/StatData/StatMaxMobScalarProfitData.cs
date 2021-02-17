using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxMobScalarProfit")]
[System.Serializable]
public class StatMaxMobScalarProfitData : StatData
{
	public float scalar;

	public override string GetDiffrence(StatData data)
	{
		if(data is StatMaxMobScalarProfitData stat)
		{
			return "Scalar : " + "<color=yellow>" + scalar + "</color>" + " > " + "<color=green>" + stat.scalar + "</color>";
		}
		return "";
	}
}