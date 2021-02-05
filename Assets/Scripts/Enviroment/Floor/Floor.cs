using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	[Header("Grid")]
	public int gridWidth = 1;
	public int gridHeight = 1;

	[Header("Tile")]
	public FloorTile floorPrefab;
	public Vector2 offset;
	public float floorWidth = 5;
	public float floorHeight = 5;

	[Header("Collider")]
	public Vector3 colliderSize;
	private GameObject floorCollider;


	[HideInInspector] public List<FloorTile> floorTiles = new List<FloorTile>();

	[ContextMenu("Floor Initialization")]
	public void Initialization()
	{
		DestroyAllChildren(transform);
		InstantiateFloor();
	}

	private void InstantiateFloor()
	{
		for(int i = 0; i < gridWidth; i++)
		{
			for(int j = 0; j < gridHeight; j++)
			{
				Transform t = Instantiate(floorPrefab, transform).transform;
				t.localPosition = new Vector3(offset.x + floorWidth * i, 0, offset.y - floorHeight * j);

				floorTiles.Add(t.GetComponent<FloorTile>());
			}
		}

		floorCollider = new GameObject("_collider");
		floorCollider.transform.SetParent(transform);
		BoxCollider box = floorCollider.AddComponent<BoxCollider>();
		box.size = colliderSize;
	}

	private void DestroyAllChildren(Transform parent)
	{
		floorCollider = null;
		floorTiles.Clear();
		for(int i = parent.childCount - 1; i >= 0 ; i--)
		{
			DestroyImmediate(parent.GetChild(i).gameObject);
		}
	}
}
