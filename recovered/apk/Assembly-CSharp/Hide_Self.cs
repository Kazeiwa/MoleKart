using UnityEngine;

public class Hide_Self : MonoBehaviour
{
	private MeshRenderer m_Mesh;

	public float m_AreaHeight = 650f;

	public float m_GameObjectHeight = 20f;

	public bool m_Show = true;

	private void Start()
	{
		m_Mesh = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		if (m_Show && null != base.transform.parent && null != base.transform.parent.parent && null != base.transform.parent.parent.parent)
		{
			if (base.transform.position.y > base.transform.parent.parent.parent.transform.position.y + (m_AreaHeight - m_GameObjectHeight) / 2f)
			{
				m_Mesh.enabled = false;
			}
			else if (base.transform.position.y < base.transform.parent.parent.parent.transform.position.y - (m_AreaHeight - m_GameObjectHeight) / 2f)
			{
				m_Mesh.enabled = false;
			}
			else
			{
				m_Mesh.enabled = true;
			}
		}
	}
}
