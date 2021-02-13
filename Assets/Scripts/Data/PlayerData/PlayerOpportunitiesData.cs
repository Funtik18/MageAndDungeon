using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/PlayerOpportunitiesData", fileName = "PlayerOpportunitiesData")]
[System.Serializable]
public class PlayerOpportunitiesData : ScriptableObject
{
    [Header("Stats")]
    public List<StatMaxHPData> maxHPDatas = new List<StatMaxHPData>();
    public List<StatMaxRadiusData> maxRadiusDatas = new List<StatMaxRadiusData>();
    [Header("Spells")]
    public List<SpellHellishFrostData> hellishFrostDatas = new List<SpellHellishFrostData>();
    public List<SpellPunishingFistData> punishingFistDatas = new List<SpellPunishingFistData>();
    public List<SpellThunderStormData> thunderStormDatas = new List<SpellThunderStormData>();
}