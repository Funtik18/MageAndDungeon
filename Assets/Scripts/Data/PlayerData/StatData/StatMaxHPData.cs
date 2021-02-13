using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/StatMaxHPData")]
[System.Serializable]
public class StatMaxHPData : StatData
{
	public int maxHP;
}