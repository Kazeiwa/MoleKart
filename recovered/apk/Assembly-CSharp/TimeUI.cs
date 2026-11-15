using System.Collections.Generic;
using UnityEngine;

public class TimeUI : ISceneUI
{
	private Dictionary<int, GameObject> m_MinuteMap1 = new Dictionary<int, GameObject>();

	private Dictionary<int, GameObject> m_MinuteMap2 = new Dictionary<int, GameObject>();

	private Dictionary<int, GameObject> m_SecondMap1 = new Dictionary<int, GameObject>();

	private Dictionary<int, GameObject> m_SecondMap2 = new Dictionary<int, GameObject>();

	private Dictionary<int, GameObject> m_MilsecondMap1 = new Dictionary<int, GameObject>();

	private Dictionary<int, GameObject> m_MilsecondMap2 = new Dictionary<int, GameObject>();

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
		if (!(null == transform))
		{
			m_Transform = transform;
			for (int i = 0; i < 10; i++)
			{
				GameObject gameObject = transform.FindChild("Times/Minute_1/" + i).gameObject;
				m_MinuteMap1.Add(i, gameObject);
				gameObject = transform.FindChild("Times/Minute_2/" + i).gameObject;
				m_MinuteMap2.Add(i, gameObject);
				gameObject = transform.FindChild("Times/Second_1/" + i).gameObject;
				m_SecondMap1.Add(i, gameObject);
				gameObject = transform.FindChild("Times/Second_2/" + i).gameObject;
				m_SecondMap2.Add(i, gameObject);
				gameObject = transform.FindChild("Times/Milsecond_1/" + i).gameObject;
				m_MilsecondMap1.Add(i, gameObject);
				gameObject = transform.FindChild("Times/Milsecond_2/" + i).gameObject;
				m_MilsecondMap2.Add(i, gameObject);
			}
			m_Time_interval_1 = transform.FindChild("Times/Interval_1").gameObject;
			if (null != m_Time_interval_1)
			{
				m_Time_interval_1.active = true;
			}
			m_Time_interval_2 = transform.FindChild("Times/Interval_2").gameObject;
			if (null != m_Time_interval_2)
			{
				m_Time_interval_2.active = true;
			}
			m_Time_tittle = transform.FindChild("Times/Title").gameObject;
			if (null != m_Time_tittle)
			{
				m_Time_tittle.active = true;
			}
			m_MilsecondMap1[0].active = true;
			m_MilsecondMap2[0].active = true;
			m_SecondMap1[0].active = true;
			m_SecondMap2[0].active = true;
			m_MinuteMap1[0].active = true;
			m_MinuteMap2[0].active = true;
		}
	}

	public void UpdateUI()
	{
	}

	public void ShowTime(float fTime)
	{
		if (m_HideTime)
		{
			if (null != m_Time_interval_1 && !m_Time_interval_1.active)
			{
				m_Time_interval_1.active = true;
			}
			if (null != m_Time_interval_2 && !m_Time_interval_2.active)
			{
				m_Time_interval_2.active = true;
			}
			if (null != m_Time_tittle && !m_Time_tittle.active)
			{
				m_Time_tittle.active = true;
			}
			if (m_MinuteMap1.ContainsKey(m_PreMinute1) && null != m_MinuteMap1[m_PreMinute1])
			{
				m_MinuteMap1[m_PreMinute1].active = true;
			}
			if (m_MinuteMap2.ContainsKey(m_PreMinute2) && null != m_MinuteMap2[m_PreMinute2])
			{
				m_MinuteMap2[m_PreMinute2].active = true;
			}
			if (m_SecondMap1.ContainsKey(m_PreSecond1) && null != m_SecondMap1[m_PreSecond1])
			{
				m_SecondMap1[m_PreSecond1].active = true;
			}
			if (m_SecondMap2.ContainsKey(m_PreSecond2) && null != m_SecondMap2[m_PreSecond2])
			{
				m_SecondMap2[m_PreSecond2].active = true;
			}
			if (m_MilsecondMap1.ContainsKey(m_PreMilsecond1) && null != m_MilsecondMap1[m_PreMilsecond1])
			{
				m_MilsecondMap1[m_PreMilsecond1].active = true;
			}
			if (m_MilsecondMap2.ContainsKey(m_PreMilsecond2) && null != m_MilsecondMap2[m_PreMilsecond2])
			{
				m_MilsecondMap2[m_PreMilsecond2].active = true;
			}
			m_HideTime = false;
		}
		int num = (int)(fTime * 100f);
		int num2 = num / 6000;
		int num3 = num2 / 10;
		int num4 = num2 % 10;
		int num5 = num / 100;
		int num6 = num5 % 60 / 10;
		int num7 = num5 % 60 % 10;
		int num8 = num % 10;
		int num9 = num / 10 % 10;
		if (m_PreMilsecond2 != num8)
		{
			if (m_MilsecondMap2.ContainsKey(m_PreMilsecond2) && null != m_MilsecondMap2[m_PreMilsecond2])
			{
				m_MilsecondMap2[m_PreMilsecond2].active = false;
			}
			if (m_MilsecondMap2.ContainsKey(num8) && null != m_MilsecondMap2[num8])
			{
				m_MilsecondMap2[num8].active = true;
			}
			m_PreMilsecond2 = num8;
		}
		if (m_PreMilsecond1 != num9)
		{
			if (m_MilsecondMap1.ContainsKey(m_PreMilsecond1) && null != m_MilsecondMap1[m_PreMilsecond1])
			{
				m_MilsecondMap1[m_PreMilsecond1].active = false;
			}
			if (m_MilsecondMap1.ContainsKey(num9) && null != m_MilsecondMap1[num9])
			{
				m_MilsecondMap1[num9].active = true;
			}
			m_PreMilsecond1 = num9;
		}
		if (m_PreSecond2 != num7)
		{
			if (m_SecondMap2.ContainsKey(m_PreSecond2) && null != m_SecondMap2[m_PreSecond2])
			{
				m_SecondMap2[m_PreSecond2].active = false;
			}
			if (m_SecondMap2.ContainsKey(num7) && null != m_SecondMap2[num7])
			{
				m_SecondMap2[num7].active = true;
			}
			m_PreSecond2 = num7;
		}
		if (m_PreSecond1 != num6)
		{
			if (m_SecondMap1.ContainsKey(m_PreSecond1) && null != m_SecondMap1[m_PreSecond1])
			{
				m_SecondMap1[m_PreSecond1].active = false;
			}
			if (m_SecondMap1.ContainsKey(num6) && null != m_SecondMap1[num6])
			{
				m_SecondMap1[num6].active = true;
			}
			m_PreSecond1 = num6;
		}
		if (m_PreMinute2 != num4)
		{
			if (m_MinuteMap2.ContainsKey(m_PreMinute2) && null != m_MinuteMap2[m_PreMinute2])
			{
				m_MinuteMap2[m_PreMinute2].active = false;
			}
			if (m_MinuteMap2.ContainsKey(num4) && null != m_MinuteMap2[num4])
			{
				m_MinuteMap2[num4].active = true;
			}
			m_PreMinute2 = num4;
		}
		if (m_PreMinute1 != num3)
		{
			if (m_MinuteMap1.ContainsKey(m_PreMinute1) && null != m_MinuteMap1[m_PreMinute1])
			{
				m_MinuteMap1[m_PreMinute1].active = false;
			}
			if (m_MinuteMap1.ContainsKey(num3) && null != m_MinuteMap1[num3])
			{
				m_MinuteMap1[num3].active = true;
			}
			m_PreMinute1 = num3;
		}
	}

	public void HideTime()
	{
		if (!(m_Transform == null))
		{
			GameObject gameObject = m_Transform.FindChild("Times").gameObject;
			if (null != gameObject)
			{
				gameObject.SetActiveRecursively(state: false);
				m_HideTime = true;
			}
		}
	}
}
