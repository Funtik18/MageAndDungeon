using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/SpellHellishFrostData", fileName = "SpellHellishFrost")]
[System.Serializable]
public class SpellHellishFrostData : SpellData
{
	public float durability;
	public float cooldown;
}