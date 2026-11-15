using UnityEngine;

public class CountDownUI : ISceneUI
{
	private GameObject m_CountDown;

	private Transform m_Transform;

	public void InitUI(Transform transform)
	{
		if (!(null == transform))
		{
			m_Transform = transform;
			m_CountDown = transform.FindChild("CountDown/3").gameObject;
			if (null != m_CountDown)
			{
				m_CountDown.active = true;
			}
		}
	}

	public void DisableCountDown()
	{
		if (!(null == m_Transform))
		{
			GameObject gameObject = m_Transform.FindChild("CountDown").gameObject;
			if (null != gameObject)
			{
				gameObject.SetActiveRecursively(state: false);
			}
		}
	}

	public void CountDown(int index)
	{
		if (!(null == m_Transform) && index >= 0 && index <= 3 && null != m_CountDown && !m_CountDown.name.Equals(index.ToString()))
		{
			m_CountDown.active = false;
			m_CountDown = m_Transform.FindChild("CountDown/" + index).gameObject;
			if (null != m_CountDown)
			{
				m_CountDown.active = true;
			}
		}
	}

	public void UpdateUI()
	{
	}
}
