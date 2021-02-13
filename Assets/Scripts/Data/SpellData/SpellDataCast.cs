using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/New Spell Cast", fileName = "New Spell Cast")]
public class SpellDataCast : ScriptableObject
{
    public int newPrice;
    public float newDurability;
    public float newDamage;
    public float newCooldown;
}