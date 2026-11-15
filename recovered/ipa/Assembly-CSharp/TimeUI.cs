using System.Collections.Generic;
using UnityEngine;

public class TimeUI : ISceneUI
{
	private Dictionary<int, GameObject> m_MinuteMap1;

	private Dictionary<int, GameObject> m_MinuteMap2;

	private Dictionary<int, GameObject> m_SecondMap1;

	private Dictionary<int, GameObject> m_SecondMap2;

	private Dictionary<int, GameObject> m_MilsecondMap1;

	private Dictionary<int, GameObject> m_MilsecondMap2;

	private int m_PreMinute1;

	private int m_PreMinute2;

	private int m_PreSecond1;

	private int m_PreSecond2;

	private int m_PreMilsecond1;

	private int m_PreMilsecond2;

	private bool m_HideTime;

	private GameObject m_Time_interval_1;

	private GameObject m_Time_interval_2;

	private GameObject m_Time_tittle;

	private Transform m_Transform;

	public void InitUI(Transform transform)
	{
	}

	public void UpdateUI()
	{
	}

	public void ShowTime(float fTime)
	{
	}

	public void HideTime()
	{
	}
}
