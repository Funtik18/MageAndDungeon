using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Entity ent;


    Rigidbody rb;
    Transform target; 

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        target = FindObjectOfType<Mag>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = target.position - transform.position;
        rb.velocity = new Vector3(destination.x * speed * Time.deltaTime, rb.velocity.y, destination.z * speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ent.Death();
        }
    }
}
