using UnityEngine;

public class Entity : MonoBehaviour
{
	public bool isAlive = true;
	public virtual void TakeDamage(int damage) { isAlive = false; }
	protected virtual void Awake() { }
}