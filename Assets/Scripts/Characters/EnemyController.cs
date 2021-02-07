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

    private Rigidbody rb;
    public Rigidbody Rigidbody
    {
        get
        {
            if(rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            return rb;
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

    private Coroutine attackCoroutine = null;
    public bool IsAttackProcess => attackCoroutine != null;

    private bool isAlive = true;
    private bool isTargetNear = false;

    private Transform target;

    void Awake()
    {
        target = GameController.Instance.PlayerTarget;

        StartLife();
    }

	#region Life
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
			if(!isTargetNear)
			{
                Vector3 destination = target.position - transform.position;

                Movement(destination);

                isTargetNear = destination.magnitude <= 1f ? true : false;
				if(isTargetNear)
				{
                    Rigidbody.velocity = Vector3.zero;
                }
			}
			else
			{
                Animator.SetTrigger("Attack");
                StartAttack();
            }
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

    #region Attack
    private void AttackMag()
    {
        target.GetComponent<Mag>().TakeDamage();
    }


    private bool canAttack = true;
    private void StartAttack()
	{
		if(!IsAttackProcess)
		{
            attackCoroutine = StartCoroutine(Attack());
		}
	}
    private IEnumerator Attack()
	{
        yield return null;

        canAttack = true;

        AnimatorStateInfo animatorStateInfo = Animator.GetCurrentAnimatorStateInfo(0);

        float startTime = Time.time;
        float currentTime = Time.time - startTime;
        while(currentTime < animatorStateInfo.length)
		{
            currentTime = Time.time - startTime;

            if(currentTime >= AttackAnimationLength())
			{
				if(canAttack)
				{
                    AttackMag();
                    canAttack = false;
                }
            }
            yield return null;
        }

		if(IsAttackProcess)
		{
            StopCoroutine(attackCoroutine);
           attackCoroutine = null;
        }
    }
	#endregion
	#endregion

	private void Movement(Vector3 destination)
	{
        Rigidbody.velocity = new Vector3(destination.x * speed * Time.deltaTime, Rigidbody.velocity.y, destination.z * speed * Time.deltaTime);
    }

    protected virtual float AttackAnimationLength()
	{
        return 1.17f;
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
