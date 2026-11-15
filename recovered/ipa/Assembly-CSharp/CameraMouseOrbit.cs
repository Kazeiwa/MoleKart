using UnityEngine;

public class CameraMouseOrbit : MonoBehaviour
{
	public Transform target;

	public float distance;

	public float xSpeed;

	public float ySpeed;

	public float yMinLimit;

	public float yMaxLimit;

	private float x;

	private float y;

	private void Start()
	{
	}

	private void LateUpdate()
	{
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
