using System;
using System.Collections;
using UnityEngine;

public class FXFlower2 : MonoBehaviour
{
	public struct Petal
	{
		public float age;

		public float life;

		public Color clr;

		public Vector3 pos;

		public Vector3 rot;

		public Vector3 spd;

		public Vector3 spd2;
	}

	public FXMgrShadow _MgrShadow;

	public GameObject _PetalObj;

	public float _Pps = 20f;

	public float _Radius = 0.25f;

	public float _minSpeed;

	public float _maxSpeed;

	public float _minLife = 0.25f;

	public float _maxLife = 0.5f;

	public float _midLife = 0.5f;

	public Vector3 _Gravity = new Vector3(0f, -10f, 0f);

	public Vector3 _RotSpeed = Vector3.zero;

	public Vector4 _rectShadow = new Vector4(0f, 0f, 256f, 256f);

	private Mesh _Mesh;

	private float _TimeLastCreate;

	private bool _Enabled = true;

	private Vector3[] _vbSrc;

	private Vector2[] _uvSrc;

	private int[] _ibSrc;

	private ArrayList _Petals;

	private void Awake()
	{
		Mesh sharedMesh = _PetalObj.GetComponent<MeshFilter>().sharedMesh;
		_vbSrc = new Vector3[sharedMesh.vertices.Length];
		sharedMesh.vertices.CopyTo(_vbSrc, 0);
		_uvSrc = new Vector2[sharedMesh.uv.Length];
		sharedMesh.uv.CopyTo(_uvSrc, 0);
		_ibSrc = new int[sharedMesh.triangles.Length];
		sharedMesh.triangles.CopyTo(_ibSrc, 0);
		MeshFilter meshFilter = base.gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		_Mesh = meshFilter.mesh;
		MeshRenderer meshRenderer = base.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		meshRenderer.material = _PetalObj.renderer.sharedMaterial;
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = false;
		_Petals = new ArrayList();
	}

	private void CreatePetals(int num)
	{
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		Petal petal = default(Petal);
		for (int i = 0; i < num; i++)
		{
			petal.age = 0f;
			petal.life = Mathf.Lerp(_minLife, _maxLife, UnityEngine.Random.value);
			petal.clr = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1f);
			float f = UnityEngine.Random.value * (float)Math.PI * 2f;
			float f2 = Mathf.Lerp((float)Math.PI / 2f, (float)Math.PI, UnityEngine.Random.value);
			float x = Mathf.Cos(f) * Mathf.Sin(f2);
			float z = Mathf.Sin(f) * Mathf.Sin(f2);
			float y = Mathf.Cos(f2);
			petal.spd = new Vector3(x, y, z);
			petal.pos = localToWorldMatrix.MultiplyPoint(petal.spd * _Radius);
			petal.spd = localToWorldMatrix.MultiplyVector(petal.spd);
			petal.spd *= Mathf.Lerp(_minSpeed, _maxSpeed, UnityEngine.Random.value);
			petal.spd2 = Vector3.zero;
			petal.rot = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			petal.rot *= 360f;
			_Petals.Add(petal);
		}
	}

	private void UpdatePetals(float timeDelta)
	{
		ArrayList arrayList = new ArrayList();
		Color black = Color.black;
		foreach (Petal petal3 in _Petals)
		{
			if (!(petal3.age + timeDelta < petal3.life))
			{
				continue;
			}
			Petal petal2 = petal3;
			petal2.age += timeDelta;
			petal2.spd2 += _Gravity * timeDelta;
			petal2.pos += (petal2.spd + 0.5f * petal2.spd2) * timeDelta;
			petal2.rot += _RotSpeed * timeDelta;
			float num = _midLife * petal2.life;
			if (petal2.age < _midLife * petal2.life)
			{
				float t = petal2.age / num;
				petal2.clr.a = Mathf.Lerp(0f, 1f, t);
			}
			else
			{
				float t2 = (petal2.age - num) / (petal2.life - num);
				petal2.clr.a = Mathf.Lerp(1f, 0f, t2);
			}
			arrayList.Add(petal2);
			if (null != _MgrShadow)
			{
				Vector3 pos = petal2.pos;
				pos.y = 0.025f;
				black.a = 0.35f;
				float num2 = petal2.life * 0.9f;
				if (petal2.age > num2)
				{
					black.a *= 1f - (petal2.age - num2) / (petal2.life - num2);
				}
				_MgrShadow.AddShadow(pos, Vector3.forward, Vector3.right, 0.1f, 0.1f, _rectShadow, black);
			}
		}
		_Petals = arrayList;
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		_TimeLastCreate += deltaTime;
		if (_Enabled)
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
			Matrix4x4 matrix4x = worldToLocalMatrix * Matrix4x4.TRS(petal.pos, Quaternion.Euler(petal.rot.x, petal.rot.y, petal.rot.z), Vector3.one) * Matrix4x4.Scale(new Vector3(0.5f, 0.5f, 0.5f));
			int num = _vbSrc.Length;
			int num2 = i * num;
			for (int j = 0; j < num; j++)
			{
				ref Vector3 reference = ref array[num2 + j];
				reference = matrix4x.MultiplyPoint(_vbSrc[j]);
				ref Vector2 reference2 = ref array2[num2 + j];
				reference2 = _uvSrc[j];
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
