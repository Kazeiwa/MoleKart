using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	public enum RotationAxes
	{
		MouseXAndY,
		MouseX,
		MouseY
	}

	public RotationAxes axes;

	public float sensitivityX;

	public float sensitivityY;

	public float minimumX;

	public float maximumX;

	public float minimumY;

	public float maximumY;

	private float rotationX;

	private float rotationY;

	private Quaternion originalRotation;

	private void Update()
	{
	}

	private void Start()
	{
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
