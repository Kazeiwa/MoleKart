using UnityEngine;

public class FXWindmill : MonoBehaviour
{
	public float _RotSpeed = 30f;

	private float _Ang;

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		base.transform.localRotation = Quaternion.Euler(90f, _Ang, 0f);
		_Ang += deltaTime * _RotSpeed;
	}
}
