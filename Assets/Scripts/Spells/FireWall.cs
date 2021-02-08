using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public Collider[] hitColliders;
    public float radius;
    // Start is called before the first frame update
    void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, radius, 1 << 9);
        foreach (var item in hitColliders)
        {
            item.GetComponent<EnemyController>().isAlive = false;

        }

    }

}
