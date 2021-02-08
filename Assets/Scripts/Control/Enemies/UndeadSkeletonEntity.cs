using UnityEngine;

public class UndeadSkeletonEntity : EnemyController
{
    [SerializeField] private SkeletonSetup currentSetup;
    public SkeletonStats Stats
	{
		get
		{
            if(stats == null)
			{
                stats = new SkeletonStats(currentSetup);
            }
            return stats as SkeletonStats;
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