using UnityEngine;

public class Entity : MonoBehaviour
{
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


	[ContextMenu("Reborn")]
	public void ReBorn()
	{
		Ragdoll.EnableRagdoll(false);
		Animator.enabled = true;
	}

	[ContextMenu("Death")]
	public void Death()
	{
		Animator.enabled = false;
		Ragdoll.EnableRagdoll(true);
	}
}
