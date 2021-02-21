using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
	public EntityData data;
	protected EntityStats stats;

	public UnityAction<Entity> onDied;

    public Rigidbody rb;

    protected bool isAlive = true;

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


    protected virtual void AttackTarget() { }
	public virtual void TakeDamage(int damage) { isAlive = false; }

	public virtual void GetFrozen(float time) { }

	public virtual int GetPrice() { return 0; }

	protected virtual float AttackAnimationLength() { return 0; }
	protected virtual void Awake() { }
}