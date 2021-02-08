using System.Collections;
using System.Collections.Generic;
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
		Rigidbody.velocity = CurrentTransform.forward * Stats.Speed * Time.deltaTime;
	}
	protected override float AttackAnimationLength()
	{
		return Stats.AnimationLength;
	}

	protected override void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			isAlive = false;
			PlayerStats stats = other.GetComponent<PlayerController>().Stats;
			stats.Money += Stats.Price;
			UIManager.Instance.UpdateStatistics();
		}
	}
}