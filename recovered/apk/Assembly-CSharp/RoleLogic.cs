using UnityEngine;

public class RoleLogic : MonoBehaviour
{
	private Transform m_Parent;

	private Transform m_Slef;

	public Transform Parent
	{
		set
		{
			m_Parent = value;
			if (null != m_Parent)
			{
				base.transform.parent = m_Parent;
				base.transform.localPosition = Vector3.zero;
				base.transform.rotation = m_Parent.rotation * Quaternion.Euler(90f, 0f, 0f);
			}
		}
	}

	public void Start()
	{
		if (null != m_Parent)
		{
			base.transform.localPosition = Vector3.zero;
			base.transform.rotation = m_Parent.rotation * Quaternion.Euler(90f, 0f, 0f);
		}
		m_Slef = base.transform.FindChild("Bip root");
		if (!(m_Slef != null))
		{
			return;
		}
		MeshRenderer[] componentsInChildren = m_Slef.GetComponentsInChildren<MeshRenderer>();
		if (componentsInChildren != null)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = false;
			}
		}
	}

	public void Update()
	{
	}
}
