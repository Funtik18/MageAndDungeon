using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/SpellHellishFrostData", fileName = "SpellHellishFrost")]
[System.Serializable]
public class SpellHellishFrostData : SpellData
{
	public float durability;
	public float cooldown;

	public override string GetDiffrence(SpellData data)
	{
		if(data is SpellHellishFrostData data1)
		{
			return "Dur : " + "<color=yellow>" + durability + "</color>" + " > " + "<color=green>" + data1.durability + "</color>" + "\n"
				+ "CoolDown : " + "<color=yellow>" + cooldown + "</color>" + " > " + "<color=green>" + data1.cooldown + "</color>";
		}
		return "";
	}
}