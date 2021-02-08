using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Entity
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

    private Collider coll;
    public Collider Coll
	{
		get
		{
            if(coll == null)
			{
                coll = GetComponent<Collider>();
			}
            return coll;
		}
	}

	#endregion

	private Coroutine lifeCoroutine = null;
    public bool IsLifeCycle => lifeCoroutine != null;

    private Coroutine attackCoroutine = null;
    public bool IsAttackProcess => attackCoroutine != null;

    private bool isAlive = true;
    private bool isTargetNear = false;

    private Wizard target;
    private Transform targetTransform;
    public Transform TargetTransform
	{
		get
		{
            if(targetTransform == null)
			{
                if(target == null)
				{
                    target = GameManager.Instance.WizardTarget;
                }
                targetTransform = target.transform;
			}
            return targetTransform;
		}
	}


    void Awake()
    {
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
                Vector3 destination = TargetTransform.position - transform.position;

                transform.LookAt(TargetTransform);

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
            yield return new WaitForFixedUpdate();
        }
        StopAttack();
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
        target.TakeDamage();
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
        StopAttack();
    }
    private void StopAttack()
	{
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
        //Rigidbody.velocity = new Vector3(destination.x * speed * Time.deltaTime, Rigidbody.velocity.y, destination.z * speed * Time.deltaTime);

        Rigidbody.velocity = CurrentTransform.forward * speed * Time.deltaTime;
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
        Coll.enabled = false;

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
