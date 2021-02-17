using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxHPData")]
[System.Serializable]
public class StatMaxHPData : StatData
{
	public int maxHP;

	public override string GetDiffrence(StatData data)
	{
		if(data is StatMaxHPData stat)
		{
			return "MaxHp : " + "<color=yellow>" + maxHP + "</color>" + " > " + "<color=green>" + stat.maxHP + "</color>";
		}
		return "";
	}
}