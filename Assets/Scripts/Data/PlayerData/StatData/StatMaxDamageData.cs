using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxDamage")]
[System.Serializable]
public class StatMaxDamageData : StatData
{
    public int damage;
}
