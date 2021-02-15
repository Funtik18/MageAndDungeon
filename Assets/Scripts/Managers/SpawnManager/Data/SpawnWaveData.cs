using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SceneData/WaveOrder", fileName = "Wave")]
[System.Serializable]
public class SpawnWaveData : ScriptableObject
{
	public List<SpawnOrderData> spawnOrdersData = new List<SpawnOrderData>();

	public Time time;

	[System.Serializable]
	public struct Time
	{
		[Tooltip("Дополнительное время для завершения волны (Время на подготовку к следующей волне).")]
		public float addTime;
	}
}