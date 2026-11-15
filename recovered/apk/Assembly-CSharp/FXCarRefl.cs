using System;
using UnityEngine;

public class FXCarRefl : MonoBehaviour
{
	private GameObject _CartBody;

	private Vector3 _Pos;

	private Vector3 _PosLast;

	private float _RotBorn;

	private Quaternion _texRot;

	private Quaternion _qBorn;

	private float GetRotation()
	{
		Vector3 forward = base.transform.forward;
		forward.y = 0f;
		float value = Vector3.Dot(forward.normalized, Vector3.forward);
		value = Mathf.Clamp(value, -1f, 1f);
		float num = Mathf.Acos(value);
		if (forward.x < 0f)
		{
			num = (float)Math.PI * 2f - num;
		}
		return 57.29578f * num;
	}

	public void Reset()
	{
		_PosLast = base.transform.position;
		_Pos = Vector3.zero;
		_RotBorn = GetRotation();
		_texRot = Quaternion.identity;
		_qBorn = Quaternion.Euler(0f, _RotBorn, 0f);
	}

	public void Init(GameObject CarBody)
	{
		_CartBody = CarBody;
		Reset();
	}

	private void Update()
	{
		float num = _RotBorn - GetRotation();
		if (num < 0f)
		{
			num += 360f;
		}
		_texRot = Quaternion.Euler(0f, 0f, num);
		Vector3 vector = _PosLast - base.transform.position;
		_PosLast = base.transform.position;
		Vector3 vector2 = Quaternion.Inverse(_qBorn) * vector;
		vector2.y = vector2.z;
		_Pos += vector2;
		Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(_Pos.x * 0.05f, _Pos.y * 0.05f, 0f), _texRot, new Vector3(1f, 1f, 1f));
		Matrix4x4 identity = Matrix4x4.identity;
		identity.m03 = -0.5f;
		identity.m13 = -0.5f;
		matrix *= identity;
		_CartBody.renderer.material.SetMatrix("_TexMatrix", matrix);
	}
}
