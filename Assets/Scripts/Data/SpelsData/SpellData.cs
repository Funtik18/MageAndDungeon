using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/New Spell Setup", fileName = "New Spell")]

public class SpellData : ScriptableObject
{
    public string spellName;
    public string spellDiscription;
    public string spellAdditionalInfo;

    public List<SpellData1> nextLevel = new List<SpellData1>();
}
