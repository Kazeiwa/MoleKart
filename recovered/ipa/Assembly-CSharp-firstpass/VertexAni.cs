using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class VertexAni : MonoBehaviour
{
	public Mesh[] m_Meshes;

	public bool m_AnimateAutomatically;

	public float m_OneLoopLength;

	public WrapMode m_WrapMode;

	protected float m_AutomaticTime;

	protected int m_SrcMesh;

	protected int m_DstMesh;

	protected float m_Weight;

	protected Mesh m_Mesh;

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	private void SetComplexMorph(int srcIndex, int dstIndex, float t)
	{
	}

	private void SetMorph(float t)
	{
	}
}
