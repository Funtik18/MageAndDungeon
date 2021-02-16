﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePeriodicDamage : MonoBehaviour
{
    public float damage;
    public float speed;

    private List<Entity> entities = new List<Entity>();
    Transform target;

    private void Awake()
    {
        entities = SpawnManager.Instance.spawnedEntities;
        target = entities[Random.Range(0, entities.Count)].transform;
        while (!target.GetComponent<EnemyController>().isAlive)
        {
            target = entities[Random.Range(0, entities.Count)].transform;
        }

    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,target.position,speed*Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target.position)<0.5f)
        {
            target.GetComponent<EnemyController>().isAlive = false;
            Destroy(this.gameObject);
        }
    }
}
