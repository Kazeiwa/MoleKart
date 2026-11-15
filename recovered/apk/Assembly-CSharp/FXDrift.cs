using System.Collections;
using UnityEngine;

public class FXDrift : MonoBehaviour
{
	public struct DriftNode
	{
		public float age;

		public Vector3 downL;

		public Vector3 upL;

		public Vector3 downR;

		public Vector3 upR;

		public float distance;

		public Vector3 center;
	}

	public GameObject m_TyreLeft;

	public GameObject m_TyreRight;

	public float m_DriftWidth = 0.1f;

	private static float NODE_AGE = 1f;

	private Mesh m_Mesh;

	private ArrayList m_DriftList;

	private bool m_Enabled;

	private float m_Distance;

	public void EnableDirft(bool bEnabled)
	{
		if (m_Enabled != bEnabled)
		{
			m_Enabled = bEnabled;
		}
	}

	public void Init(Material mtl, GameObject root)
	{
		if (m_DriftList == null)
		{
			m_DriftList = new ArrayList();
			GameObject gameObject = new GameObject();
			gameObject.name = "Drift";
			if (null != root)
			{
				gameObject.transform.parent = root.transform;
			}
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			MeshFilter meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
			m_Mesh = meshFilter.mesh;
			MeshRenderer meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
			meshRenderer.material = mtl;
			meshRenderer.castShadows = false;
			meshRenderer.receiveShadows = false;
		}
	}

	public void Update()
	{
		float deltaTime = Time.deltaTime;
		if (m_Enabled)
		{
			RecordDrift();
		}
		UpdateDrift(deltaTime);
	}

	public void LateUpdate()
	{
		if (m_Mesh == null)
		{
			return;
		}
		m_Mesh.Clear();
		int count = m_DriftList.Count;
		if (count >= 1)
		{
			Vector3[] array = new Vector3[count * 4];
			Vector2[] array2 = new Vector2[count * 4];
			Color[] array3 = new Color[count * 4];
			int[] array4 = new int[(count - 1) * 2 * 3 * 2];
			DriftNode driftNode = (DriftNode)m_DriftList[0];
			ref Vector3 reference = ref array[0];
			reference = driftNode.downL;
			ref Vector3 reference2 = ref array[1];
			reference2 = driftNode.upL;
			ref Vector2 reference3 = ref array2[0];
			reference3 = new Vector2(driftNode.distance, 0f);
			ref Vector2 reference4 = ref array2[1];
			reference4 = new Vector2(driftNode.distance, 1f);
			ref Color reference5 = ref array3[0];
			reference5 = GetColor(driftNode.age);
			ref Color reference6 = ref array3[1];
			reference6 = GetColor(driftNode.age);
			for (int i = 1; i < count; i++)
			{
				driftNode = (DriftNode)m_DriftList[i];
				ref Vector3 reference7 = ref array[i * 2];
				reference7 = driftNode.downL;
				ref Vector3 reference8 = ref array[i * 2 + 1];
				reference8 = driftNode.upL;
				ref Vector2 reference9 = ref array2[i * 2];
				reference9 = new Vector2(driftNode.distance, 0f);
				ref Vector2 reference10 = ref array2[i * 2 + 1];
				reference10 = new Vector2(driftNode.distance, 1f);
				ref Color reference11 = ref array3[i * 2];
				reference11 = GetColor(driftNode.age);
				ref Color reference12 = ref array3[i * 2 + 1];
				reference12 = GetColor(driftNode.age);
				int num = i - 1;
				int num2 = (i - 1) * 6;
				array4[num2] = num * 2;
				array4[num2 + 1] = num * 2 + 1;
				array4[num2 + 2] = num * 2 + 2;
				array4[num2 + 3] = num * 2 + 1;
				array4[num2 + 4] = num * 2 + 2;
				array4[num2 + 5] = num * 2 + 3;
			}
			driftNode = (DriftNode)m_DriftList[0];
			int num3 = count * 2;
			ref Vector3 reference13 = ref array[num3];
			reference13 = driftNode.downR;
			ref Vector3 reference14 = ref array[num3 + 1];
			reference14 = driftNode.upR;
			ref Vector2 reference15 = ref array2[num3];
			reference15 = new Vector2(driftNode.distance, 0f);
			ref Vector2 reference16 = ref array2[num3 + 1];
			reference16 = new Vector2(driftNode.distance, 1f);
			ref Color reference17 = ref array3[num3];
			reference17 = GetColor(driftNode.age);
			ref Color reference18 = ref array3[num3 + 1];
			reference18 = GetColor(driftNode.age);
			int num4 = (count - 1) * 2 * 3;
			for (int j = 1; j < count; j++)
			{
				driftNode = (DriftNode)m_DriftList[j];
				ref Vector3 reference19 = ref array[num3 + j * 2];
				reference19 = driftNode.downR;
				ref Vector3 reference20 = ref array[num3 + j * 2 + 1];
				reference20 = driftNode.upR;
				ref Vector2 reference21 = ref array2[num3 + j * 2];
				reference21 = new Vector2(driftNode.distance, 0f);
				ref Vector2 reference22 = ref array2[num3 + j * 2 + 1];
				reference22 = new Vector2(driftNode.distance, 1f);
				ref Color reference23 = ref array3[num3 + j * 2];
				reference23 = GetColor(driftNode.age);
				ref Color reference24 = ref array3[num3 + j * 2 + 1];
				reference24 = GetColor(driftNode.age);
				int num5 = j - 1;
				int num6 = (j - 1) * 6 + num4;
				array4[num6] = num3 + num5 * 2;
				array4[num6 + 1] = num3 + num5 * 2 + 1;
				array4[num6 + 2] = num3 + num5 * 2 + 2;
				array4[num6 + 3] = num3 + num5 * 2 + 1;
				array4[num6 + 4] = num3 + num5 * 2 + 2;
				array4[num6 + 5] = num3 + num5 * 2 + 3;
			}
			m_Mesh.vertices = array;
			m_Mesh.colors = array3;
			m_Mesh.uv = array2;
			m_Mesh.triangles = array4;
		}
	}

	private Color GetColor(float age)
	{
		Color white = Color.white;
		if (age < NODE_AGE)
		{
			white.a = Mathf.Lerp(0f, 1f, age / NODE_AGE);
		}
		white.a *= 0.35f;
		return white;
	}

	private bool GetProjected(Vector3 pt, float offset, out Vector3 recordpt)
	{
		if (Physics.Raycast(pt, -Vector3.up, out var hitInfo, 10000f, 256))
		{
			recordpt = hitInfo.point;
			recordpt.y += offset;
			return true;
		}
		recordpt = Vector3.zero;
		return false;
	}

	private void RecordDrift()
	{
		bool flag = false;
		Transform transform = m_TyreLeft.transform;
		Transform transform2 = m_TyreRight.transform;
		Vector3 vector = Vector3.zero;
		if (m_DriftList.Count <= 0)
		{
			flag = true;
		}
		else
		{
			DriftNode driftNode = (DriftNode)m_DriftList[m_DriftList.Count - 1];
			vector = (transform.position + transform2.position) * 0.5f;
			float magnitude = (vector - driftNode.center).magnitude;
			if (magnitude > 0.1f)
			{
				flag = true;
			}
			m_Distance += magnitude;
		}
		if (!flag)
		{
			return;
		}
		Vector3 pt = transform.position - transform.up * m_DriftWidth;
		DriftNode driftNode2 = default(DriftNode);
		if (!GetProjected(pt, 0f, out driftNode2.downL))
		{
			return;
		}
		pt = transform.position + transform.up * m_DriftWidth;
		if (!GetProjected(pt, 0.3f, out driftNode2.upL))
		{
			return;
		}
		pt = transform2.position - transform2.up * m_DriftWidth;
		if (GetProjected(pt, 0f, out driftNode2.downR))
		{
			pt = transform2.position + transform2.up * m_DriftWidth;
			if (GetProjected(pt, 0.3f, out driftNode2.upR) && flag)
			{
				driftNode2.age = NODE_AGE;
				driftNode2.distance = m_Distance;
				driftNode2.center = vector;
				m_DriftList.Add(driftNode2);
			}
		}
	}

	private void UpdateDrift(float timeDelta)
	{
		if (m_DriftList == null || m_DriftList.Count <= 0)
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		foreach (DriftNode drift in m_DriftList)
		{
			if (!(drift.age - timeDelta < 0f))
			{
				DriftNode driftNode2 = drift;
				driftNode2.age -= timeDelta;
				arrayList.Add(driftNode2);
			}
		}
		m_DriftList = arrayList;
	}
}
