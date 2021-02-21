using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellishFrost : MonoBehaviour
{
    public float radius;
    public float spelDuration;
    private List<Entity> entities = new List<Entity>();

    private void Awake()
    {
        entities = SpawnManager.Instance.spawnedEntities;
    }

    void Update()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            Entity refEntity = entities[i];

            if (Vector3.Distance(transform.position, refEntity.CurrentTransform.position) <= radius)
            {
                refEntity.GetFrozen(spelDuration);
            }

        }
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
    }
}