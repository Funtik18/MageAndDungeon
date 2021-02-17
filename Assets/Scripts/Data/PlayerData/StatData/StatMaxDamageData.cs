using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxDamage")]
[System.Serializable]
public class StatMaxDamageData : StatData
{
    public int damage;

	public override string GetDiffrence(StatData data)
	{
		if(data is StatMaxDamageData stat)
		{
			return "Dmg : " + "<color=yellow>" + damage + "</color>" + " > " + "<color=green>" + stat.damage + "</color>";
		}
		return "";
	}
}
