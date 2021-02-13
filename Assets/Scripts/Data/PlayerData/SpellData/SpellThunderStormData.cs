using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/SpellThunderStormData", fileName = "SpellThunderStorm")]
[System.Serializable]
public class SpellThunderStormData : SpellData
{
	public float durability;
	public float damage;
	public float cooldown;
}