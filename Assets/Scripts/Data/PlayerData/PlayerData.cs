using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/PlayerData", fileName = "PlayerData")]
[System.Serializable]
public class PlayerData : ScriptableObject
{
	public Stats Stats;
	public Spells Spells;
}

//stats
[System.Serializable]
public class Stats
{
	public StatMaxHPData maxHP;
	public StatMaxRadiusData maxRadius;
}
///spells
[System.Serializable]
public class Spells
{
	public SpellHellishFrostData hellishFrost;
	public SpellPunishingFistData punishingFist;
	public SpellThunderStormData thunderStorm;
}