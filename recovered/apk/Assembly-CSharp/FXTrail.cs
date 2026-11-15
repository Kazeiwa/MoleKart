using System.Collections;
using UnityEngine;

public class FXTrail : MonoBehaviour
{
	public struct TrailNode
	{
		public float age;

		public float dis;

		public Vector3 leftL;

		public Vector3 rightL;

		public Vector3 leftR;

		public Vector3 rightR;

		public Vector3 center;
	}

	private static float NODE_AGE = 1f;

	public float _TrailWidth = 0.1f;

	public GameObject _TyreLeft;

	public GameObject _TyreRight;

	private bool _Enabled;

	private ArrayList _Trail;

	private Mesh _Mesh;

	private float _Distance;

	public void EnableTrail(bool bEnabled)
	{
		if (_Enabled != bEnabled)
		{
			_Enabled = bEnabled;
		}
	}

	public bool IsTrailEnabled()
	{
		return _Enabled;
	}

	public void Init(Material mtl, GameObject root)
	{
		if (_Trail == null)
		{
			_Trail = new ArrayList();
			GameObject gameObject = new GameObject();
			gameObject.name = "TrailMesh";
			if (null != root)
			{
				gameObject.transform.parent = root.transform;
			}
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			MeshFilter meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
			_Mesh = meshFilter.mesh;
			MeshRenderer meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
			meshRenderer.material = mtl;
			meshRenderer.castShadows = false;
			meshRenderer.receiveShadows = false;
		}
	}

	private bool GetProjected(Vector3 pt, out Vector3 ptHit)
	{
		if (Physics.Raycast(pt, -Vector3.up, out var hitInfo, 10000f, 256))
		{
			ptHit = hitInfo.point;
			return true;
		}
		ptHit = Vector3.zero;
		return false;
	}

	private void RecordTrail()
	{
		bool flag = false;
		Transform transform = _TyreLeft.transform;
		Transform transform2 = _TyreRight.transform;
		Vector3 vector = Vector3.zero;
		if (_Trail.Count <= 0)
		{
			flag = true;
		}
		else
		{
			TrailNode trailNode = (TrailNode)_Trail[_Trail.Count - 1];
			vector = (transform.position + transform2.position) * 0.5f;
			float magnitude = (vector - trailNode.center).magnitude;
			if (magnitude > 0.1f)
			{
				flag = true;
			}
			_Distance += magnitude;
		}
		if (!flag)
		{
			return;
		}
		Vector3 pt = transform.position - transform.right * _TrailWidth;
		TrailNode trailNode2 = default(TrailNode);
		if (!GetProjected(pt, out trailNode2.leftL))
		{
			return;
		}
		pt = transform.position + transform.right * _TrailWidth;
		if (!GetProjected(pt, out trailNode2.rightL))
		{
			return;
		}
		pt = transform2.position - transform2.right * _TrailWidth;
		if (!GetProjected(pt, out trailNode2.leftR))
		{
			return;
		}
		pt = transform2.position + transform2.right * _TrailWidth;
		if (GetProjected(pt, out trailNode2.rightR))
		{
			trailNode2.leftL += Vector3.up * 0.06f;
			trailNode2.rightL += Vector3.up * 0.06f;
			trailNode2.leftR += Vector3.up * 0.06f;
			trailNode2.rightR += Vector3.up * 0.06f;
			if (flag)
			{
				trailNode2.age = NODE_AGE;
				trailNode2.dis = _Distance;
				trailNode2.center = vector;
				_Trail.Add(trailNode2);
			}
		}
	}

	private void UpdateTrail(float timeDelta)
	{
		if (_Trail == null || _Trail.Count <= 0)
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		foreach (TrailNode item in _Trail)
		{
			if (!(item.age - timeDelta < 0f))
			{
				TrailNode trailNode2 = item;
				trailNode2.age -= timeDelta;
				arrayList.Add(trailNode2);
			}
		}
		_Trail = arrayList;
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (_Enabled)
		{
			RecordTrail();
		}
		UpdateTrail(deltaTime);
	}

	private Color getColor(float age)
	{
		Color white = Color.white;
		if (age < NODE_AGE)
		{
			white.a = Mathf.Lerp(0f, 1f, age / NODE_AGE);
		}
		white.a *= 0.35f;
		return white;
	}

	private void LateUpdate()
	{
		if (_Mesh == null)
		{
			return;
		}
		_Mesh.Clear();
		int count = _Trail.Count;
		if (count > 1)
		{
			Vector3[] array = new Vector3[count * 4];
			Vector2[] array2 = new Vector2[count * 4];
			Color[] array3 = new Color[count * 4];
			int[] array4 = new int[(count - 1) * 2 * 3 * 2];
			TrailNode trailNode = (TrailNode)_Trail[0];
			ref Vector3 reference = ref array[0];
			reference = trailNode.leftL;
			ref Vector3 reference2 = ref array[1];
			reference2 = trailNode.rightL;
			ref Color reference3 = ref array3[0];
			reference3 = getColor(trailNode.age);
			ref Color reference4 = ref array3[1];
			reference4 = getColor(trailNode.age);
			ref Vector2 reference5 = ref array2[0];
			reference5 = new Vector2(trailNode.dis * 1.25f, 0f);
			ref Vector2 reference6 = ref array2[1];
			reference6 = new Vector2(trailNode.dis * 1.25f, 1f);
			for (int i = 1; i < count; i++)
			{
				trailNode = (TrailNode)_Trail[i];
				ref Vector3 reference7 = ref array[i * 2];
				reference7 = trailNode.leftL;
				ref Vector3 reference8 = ref array[i * 2 + 1];
				reference8 = trailNode.rightL;
				ref Vector2 reference9 = ref array2[i * 2];
				reference9 = new Vector2(trailNode.dis * 1.25f, 103f / 128f);
				ref Vector2 reference10 = ref array2[i * 2 + 1];
				reference10 = new Vector2(trailNode.dis * 1.25f, 0.99609375f);
				ref Color reference11 = ref array3[i * 2];
				reference11 = getColor(trailNode.age);
				ref Color reference12 = ref array3[i * 2 + 1];
				reference12 = getColor(trailNode.age);
				int num = i - 1;
				int num2 = (i - 1) * 6;
				array4[num2] = num * 2;
				array4[num2 + 1] = num * 2 + 1;
				array4[num2 + 2] = i * 2 + 1;
				array4[num2 + 3] = num * 2;
				array4[num2 + 4] = i * 2 + 1;
				array4[num2 + 5] = i * 2;
			}
			trailNode = (TrailNode)_Trail[0];
			int num3 = count * 2;
			ref Vector3 reference13 = ref array[num3];
			reference13 = trailNode.leftR;
			ref Vector3 reference14 = ref array[num3 + 1];
			reference14 = trailNode.rightR;
			ref Vector2 reference15 = ref array2[num3];
			reference15 = new Vector2(trailNode.dis * 1.25f, 103f / 128f);
			ref Vector2 reference16 = ref array2[num3 + 1];
			reference16 = new Vector2(trailNode.dis * 1.25f, 0.99609375f);
			ref Color reference17 = ref array3[num3];
			reference17 = getColor(trailNode.age);
			ref Color reference18 = ref array3[num3 + 1];
			reference18 = getColor(trailNode.age);
			int num4 = (count - 1) * 2 * 3;
			for (int j = 1; j < count; j++)
			{
				trailNode = (TrailNode)_Trail[j];
				ref Vector3 reference19 = ref array[num3 + j * 2];
				reference19 = trailNode.leftR;
				ref Vector3 reference20 = ref array[num3 + j * 2 + 1];
				reference20 = trailNode.rightR;
				ref Vector2 reference21 = ref array2[num3 + j * 2];
				reference21 = new Vector2(trailNode.dis * 1.25f, 103f / 128f);
				ref Vector2 reference22 = ref array2[num3 + j * 2 + 1];
				reference22 = new Vector2(trailNode.dis * 1.25f, 0.99609375f);
				ref Color reference23 = ref array3[num3 + j * 2];
				reference23 = getColor(trailNode.age);
				ref Color reference24 = ref array3[num3 + j * 2 + 1];
				reference24 = getColor(trailNode.age);
				int num5 = j - 1;
				int num6 = num4 + (j - 1) * 6;
				array4[num6] = num3 + num5 * 2;
				array4[num6 + 1] = num3 + num5 * 2 + 1;
				array4[num6 + 2] = num3 + j * 2 + 1;
				array4[num6 + 3] = num3 + num5 * 2;
				array4[num6 + 4] = num3 + j * 2 + 1;
				array4[num6 + 5] = num3 + j * 2;
			}
			_Mesh.vertices = array;
			_Mesh.uv = array2;
			_Mesh.colors = array3;
			_Mesh.triangles = array4;
		}
	}
}
