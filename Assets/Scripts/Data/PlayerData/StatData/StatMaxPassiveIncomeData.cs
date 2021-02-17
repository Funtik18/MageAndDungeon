using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Stats/StatMaxPassiveIncome", fileName = "Stat")]
[System.Serializable]
public class StatMaxPassiveIncomeData : StatData
{
    public int income;

	public override string GetDiffrence(StatData data)
	{
		if(data is StatMaxPassiveIncomeData stat)
		{
			return "Income : " + "<color=yellow>" + income + "</color>" + " > " + "<color=green>" + stat.income + "</color>";
		}
		return "";
	}
}