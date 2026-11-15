using System.Collections.Generic;
using UnityEngine;

public class LapUI : ISceneUI
{
	private Transform m_Transform;

	private Dictionary<int, GameObject> m_CurLapMap = new Dictionary<int, GameObject>();

	private Dictionary<int, GameObject> m_TotalLapMap = new Dictionary<int, GameObject>();

	private int m_PreLap;

	private bool m_HideLap;

	private GameObject m_Lap_Bg;

	private GameObject m_Lap_Slash;

	public void InitUI(Transform transform)
	{
		if (null == transform)
		{
			return;
		}
		m_Transform = transform;
		for (int i = 0; i < 10; i++)
		{
			GameObject gameObject = transform.FindChild("Laps/CurLap/" + i).gameObject;
			if (null != gameObject && i == 0)
			{
				gameObject.active = true;
			}
			m_CurLapMap.Add(i, gameObject);
			gameObject = transform.FindChild("Laps/TotalLap/" + i).gameObject;
			m_TotalLapMap.Add(i, gameObject);
		}
		m_Lap_Bg = transform.FindChild("Laps/Bg").gameObject;
		if (null != m_Lap_Bg)
		{
			m_Lap_Bg.active = true;
		}
		m_Lap_Slash = transform.FindChild("Laps/Slash").gameObject;
		if (null != m_Lap_Slash)
		{
			m_Lap_Slash.active = true;
		}
		if (m_TotalLapMap.ContainsKey(RaceModeFactory.ScenePointMap[SceneManager.Instance.CurGameSceneId].Total_Laps) && null != m_TotalLapMap[RaceModeFactory.ScenePointMap[SceneManager.Instance.CurGameSceneId].Total_Laps])
		{
			m_TotalLapMap[RaceModeFactory.ScenePointMap[SceneManager.Instance.CurGameSceneId].Total_Laps].active = true;
		}
	}

	public void ShowLap(int lap)
	{
		if (m_HideLap)
		{
			if (null != m_Lap_Bg && !m_Lap_Bg.active)
			{
				m_Lap_Bg.active = true;
			}
			if (null != m_Lap_Slash && !m_Lap_Slash.active)
			{
				m_Lap_Slash.active = true;
			}
			if (m_TotalLapMap.ContainsKey(RaceModeFactory.ScenePointMap[SceneManager.Instance.CurGameSceneId].Total_Laps) && null != m_TotalLapMap[RaceModeFactory.ScenePointMap[SceneManager.Instance.CurGameSceneId].Total_Laps] && !m_TotalLapMap[RaceModeFactory.ScenePointMap[SceneManager.Instance.CurGameSceneId].Total_Laps].active)
			{
				m_TotalLapMap[RaceModeFactory.ScenePointMap[SceneManager.Instance.CurGameSceneId].Total_Laps].active = true;
			}
			m_CurLapMap[m_PreLap].active = true;
			m_HideLap = false;
		}
		if (m_PreLap != lap)
		{
			m_CurLapMap[m_PreLap].active = false;
			m_PreLap = lap;
			m_CurLapMap[m_PreLap].active = true;
		}
	}

	public void HideLap()
	{
		GameObject gameObject = m_Transform.FindChild("Laps").gameObject;
		if (null != gameObject)
		{
			gameObject.SetActiveRecursively(state: false);
			m_HideLap = true;
		}
	}

	public void UpdateUI()
	{
	}
}
