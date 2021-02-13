using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/New Spell", fileName = "Spell")]
[System.Serializable]
public class SpellData : ScriptableObject
{
    public Sprite icon;

    public InfoData englishInfo;
    public InfoData russianInfo;

    public int level;
    public int price;
}