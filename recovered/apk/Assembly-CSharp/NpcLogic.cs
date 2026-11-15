using UnityEngine;

public class NpcLogic : MonoBehaviour
{
	private Transform m_Parent;

	public Transform Parent
	{
		set
		{
			m_Parent = value;
			if (null != m_Parent)
			{
				base.transform.parent = m_Parent;
				base.transform.localPosition = Vector3.zero;
				base.transform.rotation = m_Parent.rotation;
			}
		}
	}

	public void Start()
	{
	}

	public void Update()
	{
	}
}
