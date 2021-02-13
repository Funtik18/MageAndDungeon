using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatData : ScriptableObject
{
    public Sprite icon;

    public string statName;
    public string statDescription;
    public string statAdditionalInfo;

    public List<StatDataCast> nextLevel = new List<StatDataCast>();
}
