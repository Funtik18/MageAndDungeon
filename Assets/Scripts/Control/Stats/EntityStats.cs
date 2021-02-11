using UnityEngine;

[System.Serializable]
public class EntityStats
{
    protected string enemyName;
}

public class PlayerStats : EntityStats
{
	#region Stats
	protected VariableInt maxHealthPoints;
	public int MaxHealthPoints
	{
		set
		{
			maxHealthPoints.value = Mathf.Max(0, value);
		}
		get
		{
			return maxHealthPoints.value;
		}
	}

	protected VariableInt currentHealthPoints;
	public int CurrentHealthPoints
	{
		set
		{
			currentHealthPoints.value = Mathf.Max(0, value);
		}
		get
		{
			return currentHealthPoints.value;
		}
	}


	protected VariableFloat speed;
	public float Speed
	{
		set
		{
			speed.value = Mathf.Max(0, value);
		}
		get
		{
			return speed.value;
		}
	}

	protected VariableFloat radius;
	public float Radius
	{
		set
		{
			radius.value = Mathf.Max(1f, value);
		}
		get
		{
			return radius.value;
		}
	}

	protected VariableInt incomeAmount;
	public int IncomeAmount
	{
		set
		{
			incomeAmount.value = Mathf.Max(0, value);
		}
		get
		{
			return incomeAmount.value;
		}
	}

	protected VariableInt damage;
	public int Damage
	{
		set
		{
			damage.value = Mathf.Max(0, value);
		}
		get
		{
			return damage.value;
		}
	}

	protected VariableInt money;
	public int Money
	{
		set
		{
			money.value = Mathf.Max(0, value);
		}
		get
		{
			return money.value;
		}
	}
	#endregion
	public PlayerStats(PlayerSetup setup)
	{
		maxHealthPoints = new VariableInt(setup.hpAmount);
		currentHealthPoints = new VariableInt(setup.hpAmount);
		
		speed = new VariableFloat(setup.speed);
		radius = new VariableFloat(setup.radius);
		incomeAmount = new VariableInt(setup.incomeAmount);
		damage = new VariableInt(setup.damage);
		money = new VariableInt(setup.startMoney);
	}
}

public class SkeletonStats : EntityStats
{
	#region Stats
	protected VariableInt healthPoints;
    public int HealthPoints
	{
		set
		{
			healthPoints.value = Mathf.Max(0, value);
		}
		get
		{
            return healthPoints.value;
		}
	}

	protected VariableFloat speed;
	public float Speed
	{
		set
		{
			speed.value = Mathf.Max(0, value);
		}
		get
		{
			return speed.value;
		}
	}

	protected VariableInt damage;
    public int Damage
	{
		set
		{
			damage.value = Mathf.Max(0, value);
		}
		get
		{
            return damage.value;
		}
	}

	protected VariableInt price;
	public int Price
	{
		set
		{
			price.value = Mathf.Max(0, value);
		}
		get
		{
			return price.value;
		}
	}

	protected VariableFloat animationLength;
	public float AnimationLength
	{
		set
		{
			animationLength.value = value;
		}
		get
		{
			return animationLength.value;
		}
	}
	#endregion
	public SkeletonStats(SkeletonSetup setup)
	{
		healthPoints = new VariableInt(setup.hpAmount);
		speed = new VariableFloat(setup.speed);
		damage = new VariableInt(setup.damage);
		price = new VariableInt(setup.price);
		animationLength = new VariableFloat(setup.animationLength);
	}
}
public class ZombieStats : SkeletonStats 
{
	public ZombieStats(ZombieSetup setup) : base(setup) { }
}