using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stats/New Stat")]
[System.Serializable]
public class StatData : ScriptableObject
{
	public InfoData englishInfo;
	public InfoData russianInfo;

	public int level;
	public int price;
}