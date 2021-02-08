using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/New Enemy Setup", fileName = "New Enemy")]

public class EnemySetup: ScriptableObject
{
    public string enemyName;
    public int hpAmount;
    public float speed;
    public int damage;
    public int price;
    public float animationLength;
}
