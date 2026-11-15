using UnityEngine;

public class VertexAniSet : VertexAni
{
	private Mesh[] MeshCopy;

	private AniNameFlag m_curflag;

	public override void Start()
	{
		base.Start();
		MeshCopy = new Mesh[m_Meshes.Length];
		for (int i = 0; i < m_Meshes.Length; i++)
		{
			MeshCopy[i] = m_Meshes[i];
		}
	}

	public override void Update()
	{
		base.Update();
	}

	public void PlayAni(string aniName)
	{
		if (aniName == "jingli")
		{
			if (m_curflag == AniNameFlag.jingli)
			{
				return;
			}
			m_Meshes[0] = MeshCopy[0];
			m_Meshes[1] = MeshCopy[1];
			m_Meshes[2] = MeshCopy[2];
			m_AnimateAutomatically = true;
			m_OneLoopLength = 0.5f;
			m_WrapMode = WrapMode.Once;
			m_AutomaticTime = 0f;
			m_curflag = AniNameFlag.jingli;
		}
		else if (aniName == "libi")
		{
			if (m_curflag != AniNameFlag.jingli && m_curflag == AniNameFlag.libi)
			{
				return;
			}
			m_Meshes[0] = MeshCopy[2];
			m_Meshes[1] = MeshCopy[1];
			m_Meshes[2] = MeshCopy[0];
			m_AnimateAutomatically = true;
			m_OneLoopLength = 0.6f;
			m_WrapMode = WrapMode.Once;
			m_AutomaticTime = 0f;
			m_curflag = AniNameFlag.libi;
		}
		MeshFilter component = GetComponent<MeshFilter>();
		component.sharedMesh = m_Meshes[0];
		m_Mesh = component.mesh;
	}
}
