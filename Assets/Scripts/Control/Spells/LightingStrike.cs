using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingStrike : MonoBehaviour
{
    public GameObject spel;

    private Wizard wizard;
    private List<Entity> entities = new List<Entity>();
    private List<Entity> temp = new List<Entity>();

    public float timeToNextStrike;
    public float maxStrikes;
    float currentTime;
    private void Awake()
    {
        entities = SpawnManager.Instance.spawnedEntities;
        wizard = GameManager.Instance.WizardTarget;
        currentTime = timeToNextStrike;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        for (int i = 0; i < entities.Count; i++)
        {
            Entity refEntity = entities[i];
            if (refEntity.isAlive)
            {
                if (!temp.Contains(refEntity))
                    temp.Add(refEntity);
            }
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
        refEntity.isAlive = false;
        wizard.AddMoney(refEntity.GetPrice());
        Destroy(strike, strike.GetComponent<ParticleSystem>().main.duration);
    }
}
