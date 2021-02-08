using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/New Player Setup", fileName = "New player")]

public class PlayerSetup : ScriptableObject
{
    public string playerName;
    public int hpAmount;
    public float speed;
    public float radius;
    public int incomeAmount;
    public int damage;
    public int startMoney;
}
