using System.Collections;
using UnityEngine;

public class EnemyController : Entity
{
    public bool isFrozen = false;

    public Rigidbody rb;

    #region Properties
    private Transform currentTransform;
    public Transform CurrentTransform
    {
        get
        {
            if (currentTransform == null)
            {
                currentTransform = transform;
            }
            return currentTransform;
        }
    }

    private Animator animator;
    public Animator Animator
    {
        get
        {
            if (animator == null)
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
            if (ragdoll == null)
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

    private bool isTargetNear = false;

    protected Wizard target;
    private Transform targetTransform;
    public Transform TargetTransform
    {
        get
        {
            if (targetTransform == null)
            {
                if (target == null)
                {
                    target = GameManager.Instance.WizardTarget;
                }
                targetTransform = target.transform;
            }
            return targetTransform;
        }
    }

    protected override void Awake()
    {
        StartLife();
    }

    private void Update()
    {
        if (isFrozen)
            rb.velocity = Vector3.zero;
    }

    #region Life
    private void StartLife()
    {
        if (!IsLifeCycle)
        {
            lifeCoroutine = StartCoroutine(Life());
        }
    }
    private IEnumerator Life()
    {
        Animator.SetTrigger("Walk");

        while (isAlive)
        {
            if (!isTargetNear)
            {
                Vector3 destination = TargetTransform.position - transform.position;

                transform.LookAt(TargetTransform);

                Movement();

                isTargetNear = destination.magnitude <= 1f ? true : false;
                if (isTargetNear)
                {
                    rb.velocity = Vector3.zero;
                }
            }
            else
            {
                StartAttack();
            }
            yield return new WaitForFixedUpdate();
        }
        StopAttack();
        StopLife();
    }
    public void StopLife()
    {
        if (IsLifeCycle)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }

        Death();
    }

    #region Attack



    private bool canAttack = true;
    private void StartAttack()
    {
        if (!IsAttackProcess)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        Animator.SetTrigger("Attack");

        yield return null;

        canAttack = true;

        AnimatorStateInfo animatorStateInfo = Animator.GetCurrentAnimatorStateInfo(0);

        float startTime = Time.time;
        float currentTime = Time.time - startTime;
        while (currentTime < animatorStateInfo.length)
        {
            currentTime = Time.time - startTime;

            if (currentTime >= AttackAnimationLength())
            {
                if (canAttack)
                {
                    AttackTarget();

                    canAttack = false;
                }
            }
            yield return null;
        }

        yield return null;
        StopAttack();
    }
    protected void StopAttack()
    {
        if (IsAttackProcess)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
    #endregion
    #endregion

    public void getFrozen(float time)
    {
        if (!isFrozen)
            StartCoroutine(Frozen(time));
    }

    public IEnumerator Frozen(float time)
    {
        isFrozen = true;
        rb.velocity = Vector3.zero;
        GetComponentInChildren<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(time);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        isFrozen = false;
    }

    protected virtual void Movement() { }

    private void ReBorn()
    {
        Ragdoll.EnableRagdoll(false);
        Animator.enabled = true;
    }
    private void Death()
    {
        Animator.enabled = false;

        Ragdoll.EnableRagdoll(true);

        StartCoroutine(DeathWaiter());

        onDied?.Invoke(this);
    }
    private IEnumerator DeathWaiter()
    {
        yield return new WaitForSeconds(2f);
        //отключение физики
        Ragdoll.EnableRagdoll(false);
        rb.isKinematic = true;
        Ragdoll.EnableColliders(false);
        yield return new WaitForSeconds(5f);

        float startTime = Time.time;
        float currentTime = Time.time - startTime;
        while (currentTime <= 3f)
        {
            transform.position += Vector3.down * 0.5f * Time.deltaTime;
            yield return null;
        }

        DestroyImmediate(gameObject);
    }
}