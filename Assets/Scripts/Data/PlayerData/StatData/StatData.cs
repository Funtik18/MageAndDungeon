﻿using UnityEngine;

[System.Serializable]
public class StatData : ScriptableObject
{
	public InfoData englishInfo;
	public InfoData russianInfo;

	public int level;
	public int price;

	public virtual string GetDiffrence(StatData data)
	{
		return "";
	}
}