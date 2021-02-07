using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;

	#region Properties
	private Transform currentTransform;
    public Transform CurrentTransform
    {
        get
        {
            if(currentTransform == null)
            {
                currentTransform = transform;
            }
            return currentTransform;
        }
    }

    private Rigidbody rigidbody;
    public Rigidbody Rigidbody
    {
        get
        {
            if(rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }
            return rigidbody;
        }
    }

    private Animator animator;
    public Animator Animator
    {
        get
        {
            if(animator == null)
            {
                animator = GetComponent<Animator>();
            }
            return animator;
        }
    }

    private RagdollController ragdoll;
    public RagdollController Ragdoll
    {
        get
        {
            if(ragdoll == null)
            {
                ragdoll = GetComponent<RagdollController>();
            }
            return ragdoll;
        }
    }
	#endregion

	private Coroutine lifeCoroutine = null;
    public bool IsLifeCycle => lifeCoroutine != null;

    private bool isAlive = true;


    private Transform target; 
    
    void Awake()
    {
        StartLife();
    }

    private void StartLife()
	{
		if(!IsLifeCycle)
		{
            lifeCoroutine = StartCoroutine(Life());
		}
	}
    private IEnumerator Life()
	{
        Animator.SetTrigger("Walk");

        while(isAlive)
		{
            Vector3 destination = target.position - transform.position;

            //Rigidbody.AddForce(destination * speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            Rigidbody.velocity = new Vector3(destination.x * speed * Time.deltaTime, Rigidbody.velocity.y, destination.z * speed * Time.deltaTime);



            yield return null;

        }
        StopLife();
    }
    public void StopLife()
	{
		if(IsLifeCycle)
		{
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
		}

        Death();
    }

    [ContextMenu("Reborn")]
    private void ReBorn()
    {
        Ragdoll.EnableRagdoll(false);
        Animator.enabled = true;
    }

    [ContextMenu("Death")]
    private void Death()
    {
        Animator.enabled = false;
        Ragdoll.EnableRagdoll(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isAlive = false;
        }
    }
}
