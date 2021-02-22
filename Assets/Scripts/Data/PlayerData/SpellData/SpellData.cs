using UnityEngine;

[System.Serializable]
public class SpellData : ScriptableObject
{
    public Sprite icon;
    public Sprite disable;

    public InfoData englishInfo;
    public InfoData russianInfo;

    public int level;
    public int price;

    public virtual string GetDiffrence(SpellData data)
	{
        return null;
	}
}