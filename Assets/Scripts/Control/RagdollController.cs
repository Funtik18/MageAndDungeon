using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RagdollController : MonoBehaviour
{
	[SerializeField] private List<Transform> children = new List<Transform>();

	[SerializeField] private List<Rigidbody> rigidbodies = new List<Rigidbody>();

	[SerializeField] private List<Vector3> childrenPositions = new List<Vector3>();
	[SerializeField] private List<Quaternion> childrenRotations = new List<Quaternion>();

	[SerializeField] private List<Collider> colliders = new List<Collider>();



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

	public void EnableColliders(bool trigger)
	{
		for(int i = 0; i < colliders.Count; i++)
		{
			colliders[i].enabled = trigger;
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
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
	[ContextMenu("2) UpdateLists pos and rot")]
	private void UpdateListsPositionAndRotation()
	{
		GetTPose();
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
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
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}

	[ContextMenu("UpdateListColliders")]
	private void UpdateListColliders()
	{
		colliders = GetComponentsInChildren<Collider>().ToList();
		colliders.RemoveAt(0);
#if UNITY_EDITOR
		EditorUtility.SetDirty(gameObject);
#endif
	}
}