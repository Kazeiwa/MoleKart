using UnityEngine;

public class CameraMouseOrbit : MonoBehaviour
{
	public Transform target;

	public float distance = 10f;

	public float xSpeed;

	public float ySpeed;

	public float yMinLimit = 45f;

	public float yMaxLimit = 45f;

	private float x;

	private float y;

	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		x = eulerAngles.y;
		y = eulerAngles.x;
		if ((bool)base.rigidbody)
		{
			base.rigidbody.freezeRotation = true;
		}
	}

	private void LateUpdate()
	{
		if ((bool)target)
		{
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			Quaternion quaternion = Quaternion.Euler(y, x, 0f);
			Vector3 position = quaternion * new Vector3(0f, 0f, 0f - distance) + target.position;
			base.transform.rotation = quaternion;
			base.transform.position = position;
		}
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
