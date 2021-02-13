using UnityEngine;

[CreateAssetMenu(fileName = "InfoData", menuName = "Scriptable Object/InfoData")]
[System.Serializable]
public class InfoData : ScriptableObject
{
	public string name;
	public string description;
	public string additional;
}
