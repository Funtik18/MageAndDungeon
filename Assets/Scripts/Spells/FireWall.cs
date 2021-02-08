using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public Collider[] hitColliders;
    public float radius;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        hitColliders = Physics.OverlapSphere(transform.position, radius,1<<9);
        Debug.DrawLine(transform.position, transform.forward * radius);
        foreach (var item in hitColliders)
        {
            if (item.tag=="Enemy")
            {
                item.GetComponent<EnemyController>().isAlive=false;
            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }

}
