using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxDamageOverTime")]
[System.Serializable]
public class StatMaxDamageOverTimeData : StatData
{
    public int damage;
    public int arrowsCount;
    public int frequency;
}