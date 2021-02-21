using System.Collections.Generic;
using UnityEngine;

public class LightingStrike : MonoBehaviour
{
    public GameObject spel;

    private List<Entity> entities;
    private List<Entity> temp = new List<Entity>();

    public float timeToNextStrike;
    public float maxStrikes;
    float currentTime;
    private void Awake()
    {
        entities = SpawnManager.Instance.spawnedEntities;
        currentTime = timeToNextStrike;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        for (int i = 0; i < entities.Count; i++)
        {
            Entity refEntity = entities[i];
                if (!temp.Contains(refEntity))
                    temp.Add(refEntity);            
        }
        if (currentTime >= timeToNextStrike && maxStrikes > 0)
        {
            ThunderStorm();
        }
    }

    void ThunderStorm()
    {
        maxStrikes--;
        currentTime = 0;
        Entity refEntity = temp[Random.Range(0, temp.Count)];
        temp.Clear();
        GameObject strike=Instantiate(spel);
        strike.transform.position=refEntity.transform.position;

        refEntity.TakeDamage(1000);

        Destroy(strike, strike.GetComponent<ParticleSystem>().main.duration);
    }
}