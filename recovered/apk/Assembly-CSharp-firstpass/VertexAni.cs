using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class VertexAni : MonoBehaviour
{
	public Mesh[] m_Meshes;

	public bool m_AnimateAutomatically = true;

	public float m_OneLoopLength = 1f;

	public WrapMode m_WrapMode = WrapMode.Loop;

	protected float m_AutomaticTime;

	protected int m_SrcMesh = -1;

	protected int m_DstMesh = -1;

	protected float m_Weight = -1f;

	protected Mesh m_Mesh;

	public virtual void Start()
	{
		base.enabled = m_AnimateAutomatically;
		MeshFilter component = GetComponent<MeshFilter>();
		for (int i = 0; i < m_Meshes.Length; i++)
		{
			if (m_Meshes[i] == null)
			{
				m_AnimateAutomatically = false;
				return;
			}
		}
		if (m_Meshes.Length < 2)
		{
			m_AnimateAutomatically = false;
			return;
		}
		component.sharedMesh = m_Meshes[0];
		m_Mesh = component.mesh;
		int vertexCount = m_Mesh.vertexCount;
		for (int j = 0; j < m_Meshes.Length; j++)
		{
			if (m_Meshes[j].vertexCount != vertexCount)
			{
				m_AnimateAutomatically = false;
				break;
			}
		}
	}

	public virtual void Update()
	{
	}

	private void SetComplexMorph(int srcIndex, int dstIndex, float t)
	{
		if (m_SrcMesh != srcIndex || m_DstMesh != dstIndex || !Mathf.Approximately(m_Weight, t))
		{
			Vector3[] vertices = m_Meshes[srcIndex].vertices;
			Vector3[] vertices2 = m_Meshes[dstIndex].vertices;
			Vector3[] array = new Vector3[m_Mesh.vertexCount];
			for (int i = 0; i < array.Length; i++)
			{
				ref Vector3 reference = ref array[i];
				reference = Vector3.Lerp(vertices[i], vertices2[i], t);
			}
			m_Mesh.vertices = array;
			m_Mesh.RecalculateBounds();
		}
	}

	private void SetMorph(float t)
	{
		int value = (int)t;
		value = Mathf.Clamp(value, 0, m_Meshes.Length - 2);
		float num = t - (float)value;
		num = Mathf.Clamp(t - (float)value, 0f, 1f);
		SetComplexMorph(value, value + 1, num);
	}
}
