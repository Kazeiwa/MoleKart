using System;
using System.Collections;
using UnityEngine;

public class MeshEmitter : MonoBehaviour
{
	public struct Petal
	{
		public float age;

		public float life;

		public Vector3 sclBorn;

		public Vector3 scl;

		public Color clr;

		public Vector3 pos;

		public Vector3 rot;

		public Vector3 spd;

		public Vector3 spd2;

		public Vector3 rotSpd;

		public Vector2 uvPos;
	}

	public GameObject _MeshObj;

	public Material _Mat;

	public bool _localSpace;

	public float _Pps = 20f;

	public float _minLife = 0.25f;

	public float _maxLife = 0.5f;

	public float _midLife = 0.5f;

	public float _minRadius;

	public float _maxRadius;

	public float _minTheta;

	public float _maxTheta = 360f;

	public float _minPhi;

	public float _maxPhi = 180f;

	public float _spdMin;

	public float _spdMax;

	public Vector3 _minSclBorn = Vector3.one;

	public Vector3 _maxSclBorn = Vector3.one;

	public Vector3 _sclStart = Vector3.one;

	public Vector3 _sclMiddle = Vector3.one;

	public Vector3 _sclEnd = Vector3.one;

	public Vector3 _minRot = Vector3.zero;

	public Vector3 _maxRot = Vector3.zero;

	public Vector3 _minRotSpeed = Vector3.zero;

	public Vector3 _maxRotSpeed = Vector3.zero;

	public Color _clrStart = Color.white;

	public Color _clrMiddle = Color.white;

	public Color _clrEnd = Color.white;

	public Vector3 _Gravity = new Vector3(0f, -10f, 0f);

	public float _uSpeed;

	public float _vSpeed;

	public float _uRepeat = 1f;

	public float _vRepeat = 1f;

	private Mesh _Mesh;

	private float _TimeLastCreate;

	private bool _Emit = true;

	private Vector3[] _vbSrc;

	private Vector2[] _uvSrc;

	private int[] _ibSrc;

	private ArrayList _Petals;

	public bool emit
	{
		get
		{
			return _Emit;
		}
		set
		{
			if (value != _Emit)
			{
				_Emit = value;
				if (_Emit)
				{
					_TimeLastCreate = 0f;
				}
			}
		}
	}

	private void Awake()
	{
		if (null != _MeshObj)
		{
			_minTheta *= (float)Math.PI / 180f;
			_maxTheta *= (float)Math.PI / 180f;
			_minPhi *= (float)Math.PI / 180f;
			_maxPhi *= (float)Math.PI / 180f;
			Mesh sharedMesh = _MeshObj.GetComponent<MeshFilter>().sharedMesh;
			_vbSrc = new Vector3[sharedMesh.vertices.Length];
			sharedMesh.vertices.CopyTo(_vbSrc, 0);
			_uvSrc = new Vector2[sharedMesh.uv.Length];
			sharedMesh.uv.CopyTo(_uvSrc, 0);
			_ibSrc = new int[sharedMesh.triangles.Length];
			sharedMesh.triangles.CopyTo(_ibSrc, 0);
			MeshFilter meshFilter = base.gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
			_Mesh = meshFilter.mesh;
			MeshRenderer meshRenderer = base.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
			if ((bool)_Mat)
			{
				meshRenderer.material = _Mat;
			}
			else
			{
				meshRenderer.material = _MeshObj.renderer.sharedMaterial;
			}
			meshRenderer.castShadows = false;
			meshRenderer.receiveShadows = false;
			_Petals = new ArrayList();
		}
	}

	private Color lerp(ref Color c1, ref Color c2, float fac)
	{
		return new Color
		{
			r = Mathf.Lerp(c1.r, c2.r, fac),
			g = Mathf.Lerp(c1.g, c2.g, fac),
			b = Mathf.Lerp(c1.b, c2.b, fac),
			a = Mathf.Lerp(c1.a, c2.a, fac)
		};
	}

	private Vector3 lerp(ref Vector3 v1, ref Vector3 v2, float fac)
	{
		return v1 + (v2 - v1) * fac;
	}

	private Vector3 mult(ref Vector3 v1, ref Vector3 v2)
	{
		return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
	}

	private void CreatePetals(int num)
	{
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		Petal petal = default(Petal);
		for (int i = 0; i < num; i++)
		{
			petal.age = 0f;
			petal.life = Mathf.Lerp(_minLife, _maxLife, UnityEngine.Random.value);
			petal.clr = _clrStart;
			petal.sclBorn = lerp(ref _minSclBorn, ref _maxSclBorn, UnityEngine.Random.value);
			petal.scl = mult(ref petal.sclBorn, ref _sclStart);
			float f = Mathf.Lerp(_minTheta, _maxTheta, UnityEngine.Random.value);
			float f2 = Mathf.Lerp(_minPhi, _maxPhi, UnityEngine.Random.value);
			float x = Mathf.Cos(f) * Mathf.Sin(f2);
			float y = Mathf.Sin(f) * Mathf.Sin(f2);
			float z = Mathf.Cos(f2);
			petal.spd = new Vector3(x, y, z);
			float num2 = Mathf.Lerp(_minRadius, _maxRadius, UnityEngine.Random.value);
			petal.pos = petal.spd * num2;
			if (!_localSpace)
			{
				petal.pos = localToWorldMatrix.MultiplyPoint(petal.pos);
			}
			if (!_localSpace)
			{
				petal.spd = localToWorldMatrix.MultiplyVector(petal.spd);
			}
			petal.spd *= Mathf.Lerp(_spdMin, _spdMax, UnityEngine.Random.value);
			petal.spd2 = Vector3.zero;
			petal.rot.x = Mathf.Lerp(_minRot.x, _maxRot.x, UnityEngine.Random.value);
			petal.rot.y = Mathf.Lerp(_minRot.y, _maxRot.y, UnityEngine.Random.value);
			petal.rot.z = Mathf.Lerp(_minRot.z, _maxRot.z, UnityEngine.Random.value);
			petal.rotSpd.x = Mathf.Lerp(_minRotSpeed.x, _maxRotSpeed.x, UnityEngine.Random.value);
			petal.rotSpd.y = Mathf.Lerp(_minRotSpeed.y, _maxRotSpeed.y, UnityEngine.Random.value);
			petal.rotSpd.z = Mathf.Lerp(_minRotSpeed.z, _maxRotSpeed.z, UnityEngine.Random.value);
			petal.uvPos = Vector2.zero;
			_Petals.Add(petal);
		}
	}

	private void UpdatePetals(float timeDelta)
	{
		ArrayList arrayList = new ArrayList();
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		foreach (Petal petal3 in _Petals)
		{
			if (petal3.age + timeDelta < petal3.life)
			{
				Petal petal2 = petal3;
				petal2.age += timeDelta;
				if (!_localSpace)
				{
					petal2.spd2 += _Gravity * timeDelta;
				}
				else
				{
					petal2.spd2 += worldToLocalMatrix.MultiplyVector(_Gravity) * timeDelta;
				}
				petal2.pos += (petal2.spd + 0.5f * petal2.spd2) * timeDelta;
				petal2.rot += petal3.rotSpd * timeDelta;
				petal2.uvPos.x += _uSpeed * timeDelta;
				petal2.uvPos.y += _vSpeed * timeDelta;
				float num = _midLife * petal2.life;
				if (petal2.age < _midLife * petal2.life)
				{
					float fac = petal2.age / num;
					petal2.clr = lerp(ref _clrStart, ref _clrMiddle, fac);
					petal2.scl = lerp(ref _sclStart, ref _sclMiddle, fac);
				}
				else
				{
					float fac2 = (petal2.age - num) / (petal2.life - num);
					petal2.clr = lerp(ref _clrMiddle, ref _clrEnd, fac2);
					petal2.scl = lerp(ref _sclMiddle, ref _sclEnd, fac2);
				}
				petal2.scl = mult(ref petal2.scl, ref petal2.sclBorn);
				arrayList.Add(petal2);
			}
		}
		_Petals = arrayList;
	}

	private void Update()
	{
		if (null == _Mesh)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		_TimeLastCreate += deltaTime;
		if (_Emit)
		{
			int num = (int)(_TimeLastCreate * _Pps);
			if (num > 0)
			{
				CreatePetals(num);
				_TimeLastCreate -= (float)num / _Pps;
			}
		}
		if (_Petals.Count > 0)
		{
			UpdatePetals(deltaTime);
		}
	}

	private void LateUpdate()
	{
		if (null == _Mesh)
		{
			return;
		}
		_Mesh.Clear();
		int count = _Petals.Count;
		if (count <= 0)
		{
			return;
		}
		Vector3[] array = new Vector3[count * _vbSrc.Length];
		Vector2[] array2 = new Vector2[count * _uvSrc.Length];
		Color[] array3 = new Color[count * _vbSrc.Length];
		int[] array4 = new int[count * _ibSrc.Length];
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		for (int i = 0; i < count; i++)
		{
			Petal petal = (Petal)_Petals[i];
			Matrix4x4 matrix4x = Matrix4x4.TRS(petal.pos, Quaternion.Euler(petal.rot.x, petal.rot.y, petal.rot.z), petal.scl);
			if (!_localSpace)
			{
				matrix4x = worldToLocalMatrix * matrix4x;
			}
			int num = _vbSrc.Length;
			int num2 = i * num;
			Matrix4x4 matrix4x2 = Matrix4x4.TRS(new Vector3(petal.uvPos.x, petal.uvPos.y, 0f), Quaternion.identity, new Vector3(_uRepeat, _vRepeat, 1f));
			for (int j = 0; j < num; j++)
			{
				ref Vector3 reference = ref array[num2 + j];
				reference = matrix4x.MultiplyPoint(_vbSrc[j]);
				ref Vector2 reference2 = ref array2[num2 + j];
				reference2 = matrix4x2.MultiplyPoint(_uvSrc[j]);
				ref Color reference3 = ref array3[num2 + j];
				reference3 = petal.clr;
			}
			int num3 = _ibSrc.Length;
			int num4 = i * num3;
			for (int k = 0; k < num3; k++)
			{
				array4[num4 + k] = num2 + _ibSrc[k];
			}
		}
		_Mesh.vertices = array;
		_Mesh.uv = array2;
		_Mesh.colors = array3;
		_Mesh.triangles = array4;
	}
}
