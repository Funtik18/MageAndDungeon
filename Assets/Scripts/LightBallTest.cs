using UnityEngine;

public class LightBallTest : MonoBehaviour
{
	public Transform target;

	private void Update()
	{
		if(target == null) return;

		if(Input.GetMouseButton(0))
		{
			RaycastHit hit;
			Vector3 pos = Input.mousePosition;
			pos = Camera.main.ScreenToWorldPoint(pos);
			Ray ray = new Ray(pos, Vector3.down);

			if(Physics.Raycast(ray, out hit))
			{
				target.position = hit.point;
			}
			//Debug.LogError(Input.mousePosition);
			//Vector3 pos = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			//pos.y = 3;
			
		}
	}
}
