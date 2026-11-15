using System;
using System.Collections;
using UnityEngine;

public class MeshEmitterNew : MonoBehaviour
{
	public class Petal
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

	private int nNumPetals;

	private ArrayList _Petals = new ArrayList();

	private Vector3[] vb;

	private Vector2[] uvs;

	private Color[] clrs;

	private int[] ib;

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

	public void Awake()
	{
		if (_Pps <= 0f)
		{
			_Pps = 5f;
		}
		nNumPetals = (int)_Pps;
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
			vb = new Vector3[nNumPetals * _vbSrc.Length];
			uvs = new Vector2[nNumPetals * _uvSrc.Length];
			clrs = new Color[nNumPetals * _vbSrc.Length];
			ib = new int[nNumPetals * _ibSrc.Length];
			_Mesh.vertices = vb;
			_Mesh.uv = uvs;
			_Mesh.colors = clrs;
			_Mesh.triangles = ib;
			CreatePetals(nNumPetals);
			UpdateMesh();
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

	private void InitOnePetal(Petal pt)
	{
		pt.age = 0f;
		pt.life = Mathf.Lerp(_minLife, _maxLife, UnityEngine.Random.value);
		pt.clr = _clrStart;
		pt.sclBorn = lerp(ref _minSclBorn, ref _maxSclBorn, UnityEngine.Random.value);
		pt.scl = mult(ref pt.sclBorn, ref _sclStart);
		float f = Mathf.Lerp(_minTheta, _maxTheta, UnityEngine.Random.value);
		float f2 = Mathf.Lerp(_minPhi, _maxPhi, UnityEngine.Random.value);
		float x = Mathf.Cos(f) * Mathf.Sin(f2);
		float y = Mathf.Sin(f) * Mathf.Sin(f2);
		float z = Mathf.Cos(f2);
		pt.spd = new Vector3(x, y, z);
		float num = Mathf.Lerp(_minRadius, _maxRadius, UnityEngine.Random.value);
		pt.pos = pt.spd * num;
		if (!_localSpace)
		{
			pt.pos = base.transform.localToWorldMatrix.MultiplyPoint(pt.pos);
		}
		if (!_localSpace)
		{
			pt.spd = base.transform.localToWorldMatrix.MultiplyVector(pt.spd);
		}
		pt.spd *= Mathf.Lerp(_spdMin, _spdMax, UnityEngine.Random.value);
		pt.spd2 = Vector3.zero;
		pt.rot.x = Mathf.Lerp(_minRot.x, _maxRot.x, UnityEngine.Random.value);
		pt.rot.y = Mathf.Lerp(_minRot.y, _maxRot.y, UnityEngine.Random.value);
		pt.rot.z = Mathf.Lerp(_minRot.z, _maxRot.z, UnityEngine.Random.value);
		pt.rotSpd.x = Mathf.Lerp(_minRotSpeed.x, _maxRotSpeed.x, UnityEngine.Random.value);
		pt.rotSpd.y = Mathf.Lerp(_minRotSpeed.y, _maxRotSpeed.y, UnityEngine.Random.value);
		pt.rotSpd.z = Mathf.Lerp(_minRotSpeed.z, _maxRotSpeed.z, UnityEngine.Random.value);
		pt.uvPos = Vector2.zero;
	}

	private void CreatePetals(int num)
	{
		for (int i = 0; i < num; i++)
		{
			Petal petal = new Petal();
			InitOnePetal(petal);
			_Petals.Add(petal);
		}
	}

	private void UpdatePetals(float timeDelta)
	{
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
			}
			else
			{
				InitOnePetal(petal3);
			}
		}
	}

	public void Update()
	{
		if (_Emit)
		{
			UpdatePetals(Time.deltaTime);
			UpdateMesh();
		}
		else if (_Mesh != null)
		{
			_Mesh.Clear();
		}
	}

	private void UpdateMesh()
	{
		if (null == _Mesh)
		{
			return;
		}
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		_Mesh.Clear();
		for (int i = 0; i < nNumPetals; i++)
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
				ref Vector3 reference = ref vb[num2 + j];
				reference = matrix4x.MultiplyPoint(_vbSrc[j]);
				ref Vector2 reference2 = ref uvs[num2 + j];
				reference2 = matrix4x2.MultiplyPoint(_uvSrc[j]);
				ref Color reference3 = ref clrs[num2 + j];
				reference3 = petal.clr;
			}
			int num3 = _ibSrc.Length;
			int num4 = i * num3;
			for (int k = 0; k < num3; k++)
			{
				ib[num4 + k] = num2 + _ibSrc[k];
			}
		}
		_Mesh.vertices = vb;
		_Mesh.uv = uvs;
		_Mesh.colors = clrs;
		_Mesh.triangles = ib;
	}
}
