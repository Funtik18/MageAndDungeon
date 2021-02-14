using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Stats/StatMaxPassiveIncome", fileName = "Stat")]
[System.Serializable]
public class StatMaxPassiveIncomeData : StatData
{
    public int income;
}