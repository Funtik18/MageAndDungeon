using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxSpeed")]
[System.Serializable]
public class StatMaxSpeedData : StatData
{
    public float speed;

	public override string GetDiffrence(StatData data)
	{
		if(data is StatMaxSpeedData stat)
		{
			return "Speed : " + "<color=yellow>" + speed + "</color>" + " > " + "<color=green>" + stat.speed + "</color>";
		}
		return "";
	}
}
