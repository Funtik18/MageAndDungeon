using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
	[SerializeField] private List<Transform> children = new List<Transform>();

	[SerializeField] private List<Rigidbody> rigidbodies = new List<Rigidbody>();

	[SerializeField] private List<Vector3> childrenPositions = new List<Vector3>();
    [SerializeField] private List<Quaternion> childrenRotations = new List<Quaternion>();


	public void EnableRagdoll(bool trigger)
	{
		for(int i = 0; i < rigidbodies.Count; i++)
		{
			rigidbodies[i].isKinematic = !trigger;
		}
	}

	public void ResetRagdoll()
	{
		for(int i = 0; i < children.Count; i++)
		{
			children[i].localPosition = childrenPositions[i];
			children[i].localRotation = childrenRotations[i];
		}
	}


	private void GetTPose()
	{
		childrenPositions.Clear();
		childrenRotations.Clear();
		for(int i = 0; i < children.Count; i++)
		{
			childrenPositions.Add(children[i].localPosition);
			childrenRotations.Add(children[i].localRotation);
		}
	}


	[ContextMenu("1) UpdateListTransforms")]
	private void UpdateListTransforms()
	{
		children = GetComponentsInChildren<Transform>().ToList();
	}
	[ContextMenu("2) UpdateLists pos and rot")]
	private void UpdateListsPositionAndRotation()
	{
		GetTPose();
	}
	[ContextMenu("3) UpdateListRigidbodies")]
	private void UpdateListRigidbodies()
	{
		rigidbodies.Clear();
		for(int i = 0; i < children.Count; i++)
		{
			Rigidbody rbody = children[i].GetComponent<Rigidbody>();

			if(rbody != null)
				rigidbodies.Add(rbody);
		}
	}
}
