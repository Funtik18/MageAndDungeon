using UnityEditor;

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Entities/Entity", fileName = "Entity")]
public class EntityData: ScriptableObject 
{
	[Header("Entity")]
	public Entity entity;

    [Header("Stats")]
    public int hpAmount;
    public float speed;
    public int damage;
    public int price;
    public float animationLength;
}