using UnityEngine;

[CreateAssetMenu(menuName = "SceneData/Level", fileName = "Level_")]
public class LevelData : ScriptableObject
{
    [HideInInspector] public string levelName = "Level_";
    [Header("Scene Information")]
    public int levelN = 0;
}