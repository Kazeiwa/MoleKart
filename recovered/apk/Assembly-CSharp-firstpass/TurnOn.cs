using UnityEngine;

public class TurnOn : MonoBehaviour
{
	private Transform[] m_Children;

	private void Start()
	{
		m_Children = base.gameObject.transform.GetComponentsInChildren<Transform>();
		if (m_Children == null)
		{
			return;
		}
		for (int i = 0; i < m_Children.Length; i++)
		{
			if (m_Children[i] == base.gameObject.transform)
			{
			}
		}
	}

	private void Update()
	{
	}

	public void TurnOnEffect()
	{
		if (m_Children == null)
		{
			return;
		}
		for (int i = 0; i < m_Children.Length; i++)
		{
			if (!(m_Children[i] == base.transform))
			{
				m_Children[i].GetComponent<EmitController>().doReset();
			}
		}
	}
}
