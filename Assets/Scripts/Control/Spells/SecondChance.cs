using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChance : MonoBehaviour
{
    public float radius = 8;
    public float explosionForce = 10f;
    public float explosionRadius = 8f;

    private void Awake()
    {
        GetComponent<AudioSound>().PlayAudio();
    }

    GameObject exp;
    public void SecondChanceCast()
    {
        exp = Instantiate(gameObject, GameManager.Instance.WizardTarget.transform);

        List<Entity> entities = SpawnManager.Instance.spawnedEntities;

        for (int i = 0; i < entities.Count; i++)
        {
            Entity refEntity = entities[i];

            if (Vector3.Distance(GameManager.Instance.WizardTarget.transform.position, refEntity.CurrentTransform.position) <= radius)
            {
                refEntity.rb.AddExplosionForce(explosionForce, GameManager.Instance.WizardTarget.transform.position, explosionRadius, 1, ForceMode.Impulse);
                refEntity.TakeDamage(1000);
            }
        }

        Destroy(exp, gameObject.GetComponent<ParticleSystem>().main.duration);
    }
}