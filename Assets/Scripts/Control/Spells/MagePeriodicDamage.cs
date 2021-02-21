using System.Collections.Generic;
using UnityEngine;

public class MagePeriodicDamage : MonoBehaviour
{
    public float damage;
    public float speed;

    private List<Entity> entities;
    Entity target;

    private void Awake()
    {
        entities = SpawnManager.Instance.spawnedEntities;
        if (entities.Count <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
        target = entities[Random.Range(0, entities.Count)];
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        transform.LookAt(target.CurrentTransform);
        transform.position = Vector3.MoveTowards(transform.position, target.CurrentTransform.position, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target.CurrentTransform.position) < 0.5f)
        {
            target.TakeDamage(1000);
            Destroy(this.gameObject);
        }
    }
}
