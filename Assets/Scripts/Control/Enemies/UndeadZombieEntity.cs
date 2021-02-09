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
	public override void TakeDamage(int damage)
	{
		Stats.HealthPoints -= damage;
		if(Stats.HealthPoints == 0)
		{
			isAlive = false;
		}
	}
	protected override float AttackAnimationLength()
	{
		return Stats.AnimationLength;
	}

	//protected override void OnTriggerEnter(Collider other)
	//{
	//	if(other.tag == "Player")
	//	{
	//		isAlive = false;
	//		PlayerStats stats = other.GetComponent<PlayerController>().Stats;
	//		stats.Money += Stats.Price;
	//		UIManager.Instance.UpdateStatistics();
	//	}
	//}
}