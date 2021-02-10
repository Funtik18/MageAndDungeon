using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
	protected EntityStats stats;

	public UnityAction<Entity> onDied;


	public bool isAlive = true;

	protected virtual void AttackTarget() { }
	public virtual void TakeDamage(int damage) { isAlive = false; }
	public virtual int GetPrice() { return 0; }

	protected virtual float AttackAnimationLength() { return 0; }
	protected virtual void Awake() { }
}