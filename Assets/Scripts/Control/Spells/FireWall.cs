using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public float radius;

    private Wizard wizard;
    private List<Entity> entities = new List<Entity>();

    private void Awake()
    {
        entities = SpawnManager.Instance.spawnedEntities;
        wizard = GameManager.Instance.WizardTarget;
    }

    void Update()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            Entity refEntity = entities[i];

            if (Vector3.Distance(transform.position, refEntity.transform.position) <= radius)
            {
                refEntity.isAlive = false;
                wizard.AddMoney(refEntity.GetPrice());
            }

        }
    }
}