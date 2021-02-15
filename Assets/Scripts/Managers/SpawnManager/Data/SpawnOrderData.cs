using UnityEngine;

[CreateAssetMenu(menuName = "SceneData/SpawnOrder", fileName = "SpawnOrder")]
[System.Serializable]
public class SpawnOrderData : ScriptableObject
{
	[Tooltip("Сущность которую спавним.")]
	public Entity entity;

	[Range(1, 1000)]
	[Tooltip("Сколько 'пачек' сущностей НУЖНО заспавнить.")]
	public int countTuples = 10;

	[Range(1, 100)]
	[Tooltip("Пачка - Колличество сущностей за один спавн.")]
	public int countEntitiesInTuples = 1;

	public Time time;

	public int GetTotalEntities()
	{
		return countTuples * countEntitiesInTuples;
	}

	public float GetTotalTimeSpawn()
	{
		return (countEntitiesInTuples - 1) * time.delayInTuple + (countTuples - 1) * time.frequency;
	}


	[System.Serializable]
	public struct Time
	{
		[Tooltip("Задержа спавна в пачке.")]
		public float delayInTuple;
		[Tooltip("Как часто происходит спавн пачек? Раз в N секунд.")]
		public float frequency;
	}
}