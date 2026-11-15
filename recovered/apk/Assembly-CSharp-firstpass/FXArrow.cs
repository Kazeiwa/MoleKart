using UnityEngine;

public class FXArrow : MonoBehaviour
{
	public float _Speed = 3f;

	public float _SpdBlink = 5f;

	private float _Pos;

	private float _Time;

	private void Awake()
	{
		_Pos = 0f;
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		_Time += deltaTime;
		float num = Mathf.Sin(_Time * _SpdBlink);
		num = ((!(num > -0.35f)) ? 0f : (-0.5f));
		Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(_Pos, num, 0f), Quaternion.identity, new Vector3(1f, 0.5f, 1f));
		base.renderer.material.SetMatrix("_TexMatrix", matrix);
		_Pos += deltaTime * _Speed;
	}
}
