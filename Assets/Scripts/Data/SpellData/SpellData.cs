using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/New Spell Setup", fileName = "New Spell")]
public class SpellData : ScriptableObject
{
    public Sprite icon;

    public string spellName;
    public string spellDiscription;
    public string spellAdditionalInfo;

    public List<SpellDataCast> nextLevel = new List<SpellDataCast>();
}
