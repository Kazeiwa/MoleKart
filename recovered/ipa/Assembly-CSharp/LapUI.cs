using System.Collections.Generic;
using UnityEngine;

public class LapUI : ISceneUI
{
	private Transform m_Transform;

	private Dictionary<int, GameObject> m_CurLapMap;

	private Dictionary<int, GameObject> m_TotalLapMap;

	private int m_PreLap;

	private bool m_HideLap;

	private GameObject m_Lap_Bg;

	private GameObject m_Lap_Slash;

	public void InitUI(Transform transform)
	{
	}

	public void ShowLap(int lap)
	{
	}

	public void HideLap()
	{
	}

	public void UpdateUI()
	{
	}
}
