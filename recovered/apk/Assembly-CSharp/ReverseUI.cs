using RaceTrack;
using UnityEngine;

public class ReverseUI : ISceneUI
{
	private GameObject m_ReverseUI;

	private bool m_ShowUI = true;

	public void InitUI(Transform transform)
	{
		if (!(null == transform))
		{
			Transform transform2 = transform.FindChild("Reverse/plane");
			if (null != transform2)
			{
				m_ReverseUI = transform2.gameObject;
			}
		}
	}

	public void UpdateUI()
	{
		Car heroCar = TSingleton<CarManager>.Instance.GetHeroCar();
		if (!(null != heroCar) || !(null != m_ReverseUI))
		{
			return;
		}
		bool reverse = false;
		if (RaceWorld.Instance.RacingLine.IsGoingReversely(heroCar.Pos, heroCar.Heading))
		{
			if (!m_ReverseUI.active && m_ShowUI && !heroCar.MissleState)
			{
				m_ReverseUI.active = true;
			}
			if (!heroCar.MissleState)
			{
				reverse = true;
			}
		}
		else if (m_ReverseUI.active)
		{
			m_ReverseUI.active = false;
		}
		heroCar.SetReverse(reverse);
	}

	public void HideReverseUI()
	{
		if (null != m_ReverseUI)
		{
			m_ReverseUI.active = false;
		}
		m_ShowUI = false;
	}

	public void ShowReverseUI()
	{
		m_ShowUI = true;
	}
}
