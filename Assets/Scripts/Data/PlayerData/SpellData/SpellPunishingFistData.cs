using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/SpellPunishingFistData", fileName = "SpellPunishingFist")]
[System.Serializable]
public class SpellPunishingFistData : SpellData
{
	public float durability;
	public float damage;
	public float cooldown;

	public override string GetDiffrence(SpellData data)
	{
		if(data is SpellPunishingFistData data1)
		{
			return "Dmg : " + "<color=yellow>" + damage + "</color>" + " > " + "<color=green>" + data1.damage + "</color>" + "\n"
				+ "Dur : " + "<color=yellow>" + durability + "</color>" + " > " + "<color=green>" + data1.durability + "</color>" + "\n"
				+ "CoolDown : " + "<color=yellow>" + cooldown + "</color>" + " > " + "<color=green>" + data1.cooldown + "</color>";
		}
		return "";
	}
}