using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public float radius;

    private PlayerStats refStats;
    private List<Entity> entities = new List<Entity>();

	private void Awake()
	{
        entities = SpawnManager.Instance.spawnedEntities;
        refStats = GameManager.Instance.WizardTarget.player.Stats;
    }

	void Update()
    {
		for(int i = 0; i < entities.Count; i++)
		{
            Entity refEntity = entities[i];
			if(refEntity.isAlive)
			{
                if(Vector3.Distance(transform.position, refEntity.transform.position) <= radius)
                {
                    refEntity.isAlive = false;
                    refStats.Money += 5;
                    UIManager.Instance.UpdateStatistics();
                }
            }
        }
    }
}