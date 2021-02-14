using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/PlayerOpportunitiesData", fileName = "PlayerOpportunitiesData")]
[System.Serializable]
public class PlayerOpportunitiesData : ScriptableObject
{
    [Header("Stats")]
    public List<StatMaxHPData> maxHps = new List<StatMaxHPData>();
    
    public List<StatMaxDamageData> maxDamages = new List<StatMaxDamageData>();
    public List<StatMaxDamageOverTimeData> maxDamageOverTimes = new List<StatMaxDamageOverTimeData>();
    public List<StatMaxSpeedData> maxSpeeds = new List<StatMaxSpeedData>();
    public List<StatMaxRadiusData> maxRadiuses = new List<StatMaxRadiusData>();

    public List<StatMaxPassiveIncomeData> maxPassiveIncomes = new List<StatMaxPassiveIncomeData>();
    public List<StatMaxMobScalarProfitData> maxMobScalarProfits = new List<StatMaxMobScalarProfitData>();

    [Header("Spells")]
    public List<SpellHellishFrostData> hellishFrosts = new List<SpellHellishFrostData>();
    public List<SpellPunishingFistData> punishingFists = new List<SpellPunishingFistData>();
    public List<SpellThunderStormData> thunderStorms = new List<SpellThunderStormData>();
}