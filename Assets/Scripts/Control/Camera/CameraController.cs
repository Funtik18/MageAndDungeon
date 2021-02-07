using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
	public float speedRotate = 10f;
	public float radius = 5f;
	public float alphaDeg = 30f;
	public float bethaDeg = 30f;

	public Transform cameraTransform = null;
	public Transform followTransform = null;

	private Vector3 newPosition;
	private Quaternion newQuaternion;
	private Vector3 newQuaternionPostion;


	private void Start()
	{
		newPosition = followTransform.position;
		newQuaternion = followTransform.rotation;
		newQuaternionPostion = cameraTransform.localPosition;
	}

	private void Update()
	{
		//rotate
		RotateAround();

		cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newQuaternionPostion, Time.deltaTime * speedRotate);
		cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, newQuaternion, Time.deltaTime * speedRotate);
	}

	/// <summary>
	/// Поворот вокруг объекта.
	/// </summary>
	private void RotateAround()
	{
		float x = radius * Mathf.Sin(alphaDeg * Mathf.Deg2Rad);
		float z = radius * Mathf.Cos(alphaDeg * Mathf.Deg2Rad);

		float x2 = radius * Mathf.Cos(bethaDeg * Mathf.Deg2Rad);
		float y = radius * Mathf.Sin(bethaDeg * Mathf.Deg2Rad);

		Vector3 dir = new Vector3(x, 0, z);
		Vector3 p = dir.normalized * x2;

		p.y = y;

		cameraTransform.localPosition = p;
		cameraTransform.LookAt(followTransform);

		newQuaternionPostion = p;
		newQuaternion = Quaternion.LookRotation(followTransform.position - followTransform.TransformPoint(p));
	}
}
