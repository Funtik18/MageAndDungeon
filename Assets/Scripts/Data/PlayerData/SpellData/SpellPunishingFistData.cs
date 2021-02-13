using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/SpellPunishingFistData", fileName = "SpellPunishingFist")]
[System.Serializable]
public class SpellPunishingFistData : SpellData
{
	public float durability;
	public float damage;
	public float cooldown;
}