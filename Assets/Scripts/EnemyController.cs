using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public List<Sprite> sprites;

    Rigidbody rb;
    Transform target; 
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
        target = FindObjectOfType<Mag>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = target.position - transform.position;

        //Rigidbody.AddForce(destination * speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        rb.velocity = new Vector3(destination.x * speed * Time.deltaTime, destination.y * speed * Time.deltaTime, rb.velocity.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
