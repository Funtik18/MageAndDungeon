using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxDamageOverTime")]
[System.Serializable]
public class StatMaxDamageOverTimeData : StatData
{
    public int damage;
    public int arrowsCount;
    public int frequency;

	public override string GetDiffrence(StatData data)
	{
		if(data is StatMaxDamageOverTimeData stat)
		{
			return "Dmg : " + "<color=yellow>" + damage + "</color>" + " > " + "<color=green>" + stat.damage + "</color>" + "\n" 
				+ "Arrows : " + "<color=yellow>" + arrowsCount + "</color>" + " > " + "<color=green>" + stat.arrowsCount + "</color>" + "\n"
				+ "Frq : " + "<color=yellow>" + frequency + "</color>" + " > " + "<color=green>" + stat.frequency + "</color>";
		}
		return "";
	}
}