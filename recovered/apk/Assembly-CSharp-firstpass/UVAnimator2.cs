using UnityEngine;

public class UVAnimator2 : MonoBehaviour
{
	public int _uNum = 1;

	public int _vNum = 1;

	public float _FPS;

	public float _uSpeed;

	public float _vSpeed;

	public bool _Loop = true;

	private float _Time;

	private int _Index;

	private float _uOffset;

	private float _vOffset;

	private float _u;

	private float _v;

	private float _uScl = 1f;

	private float _vScl = 1f;

	private bool m_unloopEnd;

	private bool UpdateTexMatrix(int idx)
	{
		Texture2D texture2D = base.renderer.material.GetTexture("_MainTex") as Texture2D;
		if (null != texture2D)
		{
			_uScl = 1f / (float)_uNum;
			_vScl = 1f / (float)_vNum;
			int num = texture2D.width / _uNum;
			int num2 = texture2D.height / _vNum;
			int num3 = idx / _uNum;
			int num4 = idx - num2 * _uNum;
			float num5 = 1f / (float)texture2D.width;
			float num6 = 1f / (float)texture2D.height;
			_u = (float)(num * num4) * num5;
			_v = num2 * num3;
			_v = (float)texture2D.height - _v - (float)num2;
			_v *= num6;
			if (_uSpeed == 0f && _vSpeed == 0f)
			{
				Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(_u, _v, 0f), Quaternion.identity, new Vector3(_uScl, _vScl, 1f));
				base.renderer.material.SetMatrix("_TexMatrix", matrix);
				return true;
			}
			return false;
		}
		return false;
	}

	private void Start()
	{
		UpdateTexMatrix(_Index);
	}

	private void Update()
	{
		if (_FPS == 0f || m_unloopEnd)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		_Time += deltaTime;
		_uOffset += deltaTime * _uSpeed;
		_vOffset += deltaTime * _vSpeed;
		float num = 1f / _FPS;
		int num2 = (int)(_Time / num);
		if (!_Loop && num2 >= _uNum * _vNum)
		{
			num2 = _uNum * _vNum - 1;
			m_unloopEnd = true;
		}
		num2 %= _uNum * _vNum;
		if (num2 != _Index)
		{
			_Index = num2;
			if (num2 < 0)
			{
				num2 = -num2;
				num2 = _uNum * _vNum - num2;
			}
			if (UpdateTexMatrix(num2))
			{
				return;
			}
		}
		float x = _u + _uOffset;
		float y = _v + _vOffset;
		Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(x, y, 0f), Quaternion.identity, new Vector3(_uScl, _vScl, 1f));
		base.renderer.material.SetMatrix("_TexMatrix", matrix);
	}
}
