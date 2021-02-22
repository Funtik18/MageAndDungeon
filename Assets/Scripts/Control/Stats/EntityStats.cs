using UnityEngine;

[System.Serializable]
public class EntityStats { }

public class PlayerStats : EntityStats
{
	#region Stats
	#region HealthPoints
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
	#endregion

	#region Damage
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


	#endregion

	#region Speed
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
	#endregion

	#region Radius
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
	#endregion

	#region Income
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

	protected VariableFloat mobScalarProfit;
	public float MobScalarProfit
	{
		set
		{
			mobScalarProfit.value = Mathf.Max(1, value);
		}
		get
		{
			return mobScalarProfit.value;
		}
	}
	#endregion

	#region Money
	protected VariableInt currentMoney;
	public int CurrentMoney
	{
		set
		{
			currentMoney.value = Mathf.Max(0, value);
		}
		get
		{
			return currentMoney.value;
		}
	}
    #endregion

    #region SelfDefence
    protected VariableInt damageOverTime;
    public int DamageOverTime
    {
        set
        {
            damageOverTime.value = Mathf.Max(0, value);
        }
        get
        {
            return damageOverTime.value;
        }
    }

    protected VariableInt arrowsCount;
    public int ArrowsCount
    {
        set
        {
            arrowsCount.value = Mathf.Max(1, value);
        }
        get
        {
            return arrowsCount.value;
        }
    }

    protected VariableInt frequency;
    public int Frequency
    {
        set
        {
            frequency.value = Mathf.Max(0, value);
        }
        get
        {
            return frequency.value;
        }
    }

    #endregion

    #endregion

    #region Spells
    //protected Spells[] spell

    protected SpellHellishFrostData spellHellishFrost;
	public SpellHellishFrostData SpellHellishFrost
	{
		get
		{
			return spellHellishFrost;
		}
	}

	protected SpellPunishingFistData spellPunishingFist;
	public SpellPunishingFistData SpellPunishingFist
	{
		get
		{
			return spellPunishingFist;
		}
	}

	protected SpellThunderStormData spellThunderStorm;
	public SpellThunderStormData SpellThunderStorm
	{
		get
		{
			return spellThunderStorm;
		}
	}
	#endregion
	public PlayerStats(PlayerOpportunitiesData playerOpportunities, SaveData data)
	{
		//stats
		int hp = playerOpportunities.maxHps[data.statsLevels[0]].maxHP;
		maxHealthPoints = new VariableInt(hp);
		currentHealthPoints = new VariableInt(hp);

		damage = new VariableInt(playerOpportunities.maxDamages[data.statsLevels[1]].damage);
		damageOverTime = new VariableInt(playerOpportunities.maxDamageOverTimes[data.statsLevels[2]].damage);
        arrowsCount = new VariableInt(playerOpportunities.maxDamageOverTimes[data.statsLevels[2]].arrowsCount);
        frequency = new VariableInt(playerOpportunities.maxDamageOverTimes[data.statsLevels[2]].frequency);


        speed = new VariableFloat(playerOpportunities.maxSpeeds[data.statsLevels[3]].speed);

		radius = new VariableFloat(playerOpportunities.maxRadiuses[data.statsLevels[4]].radius);

		incomeAmount = new VariableInt(playerOpportunities.maxPassiveIncomes[data.statsLevels[5]].income);

		mobScalarProfit = new VariableFloat(playerOpportunities.maxMobScalarProfits[data.statsLevels[6]].scalar);

		currentMoney = new VariableInt(0);

		//spells
		spellHellishFrost = playerOpportunities.hellishFrosts[data.spellsLevels[0]];
		spellPunishingFist = playerOpportunities.punishingFists[data.spellsLevels[1]];
		spellThunderStorm = playerOpportunities.thunderStorms[data.spellsLevels[2]];
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
	public SkeletonStats(EntityData setup)
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
	public ZombieStats(EntityData setup) : base(setup) { }
}