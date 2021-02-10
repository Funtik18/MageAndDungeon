using UnityEngine;

public class UndeadZombieEntity : EnemyController
{
	[SerializeField] private ZombieSetup currentSetup;
	public ZombieStats Stats
	{
		get
		{
			if(stats == null)
			{
				stats = new ZombieStats(currentSetup);
			}
			return stats as ZombieStats;
		}
	}  


	protected override void Movement()
	{
		Vector3 velocity = CurrentTransform.forward * Stats.Speed * Time.deltaTime;
		rb.velocity = velocity;
	}
	protected override void AttackTarget()
	{
		target.TakeDamage(Stats.Damage);
	}
	public override void TakeDamage(int damage)
	{
		Stats.HealthPoints -= damage;
		if(Stats.HealthPoints == 0)
		{
			target.AddMoney(Stats.Price);
		
			isAlive = false;
		}
	}
	public override int GetPrice()
	{
		return Stats.Price;
	}
	protected override float AttackAnimationLength()
	{
		return Stats.AnimationLength;
	}
}