using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WayPoint
{
	public static ArrayList waypoints = null;

	private int currentWaypoint;

	private int currentNavigateWaypoint;

	private static WayPoint instance = null;

	private static Dictionary<int, ArrayList> m_WayPoints = new Dictionary<int, ArrayList>();

	public static WayPoint Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new WayPoint();
			}
			return instance;
		}
	}

	public int CurrentWayPointIndex
	{
		get
		{
			return currentWaypoint;
		}
		set
		{
			currentWaypoint = value;
		}
	}

	public int TotalWayPointNums
	{
		get
		{
			if (waypoints == null)
			{
				return 0;
			}
			return waypoints.Count;
		}
	}

	public Transform GetCurrentWayPoint
	{
		get
		{
			if (waypoints != null && currentWaypoint < waypoints.Count)
			{
				return (Transform)waypoints[currentWaypoint];
			}
			return null;
		}
	}

	public int CurrentNavigateWayPointIndex
	{
		get
		{
			return currentNavigateWaypoint;
		}
		set
		{
			currentNavigateWaypoint = value;
		}
	}

	public void Init(string name)
	{
		GetWaypoints(name);
	}

	public void Destroy()
	{
		if (waypoints != null)
		{
			waypoints.Clear();
		}
		m_WayPoints.Clear();
	}

	private void GetWaypoints(string name)
	{
		m_WayPoints.Clear();
		TextAsset textAsset = Resources.Load(name) as TextAsset;
		if (null == textAsset)
		{
			return;
		}
		Stream stream = new MemoryStream(textAsset.bytes);
		StreamReader streamReader = new StreamReader(stream);
		if (streamReader == null)
		{
			return;
		}
		ArrayList arrayList = null;
		string text;
		while ((text = streamReader.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			if (text.Equals("MainWay"))
			{
				arrayList = new ArrayList();
				m_WayPoints.Add(0, arrayList);
			}
			else if (text.Contains("Branch"))
			{
				arrayList = new ArrayList();
				for (int num = 50; num > 0; num--)
				{
					if (text.Contains(num.ToString()))
					{
						Debug.Log(text);
						m_WayPoints.Add(num, arrayList);
						break;
					}
				}
			}
			else
			{
				char[] separator = new char[1] { '\t' };
				string[] array = text.Split(separator);
				if (array != null)
				{
					Vector3 vector = new Vector3(Convert.ToSingle(array[0].Replace(" ", string.Empty)), Convert.ToSingle(array[1].Replace(" ", string.Empty)), Convert.ToSingle(array[2].Replace(" ", string.Empty)));
					arrayList.Add(vector);
				}
			}
		}
	}

	public int GetBranchNum(Vector3 pos)
	{
		float num = float.MaxValue;
		int result = -1;
		for (int i = 0; i < m_WayPoints.Count; i++)
		{
			foreach (Vector3 item in m_WayPoints[i])
			{
				if (num > (pos - item).magnitude)
				{
					num = (pos - item).magnitude;
					result = i;
				}
			}
		}
		return result;
	}

	public ArrayList GetBranchWayPoints(int branch)
	{
		if (m_WayPoints.ContainsKey(branch))
		{
			return m_WayPoints[branch];
		}
		return null;
	}

	public int GetNearWaypointNum(int branch, Vector3 pos)
	{
		ArrayList branchWayPoints = GetBranchWayPoints(branch);
		if (branchWayPoints == null)
		{
			return 0;
		}
		float num = float.MaxValue;
		int result = 0;
		for (int i = 0; i < branchWayPoints.Count; i++)
		{
			if (num > (pos - (Vector3)branchWayPoints[i]).magnitude)
			{
				num = (pos - (Vector3)branchWayPoints[i]).magnitude;
				result = i;
			}
		}
		return result;
	}

	public Transform GetWayPoint(int index)
	{
		if (waypoints == null)
		{
			return null;
		}
		return (Transform)waypoints[index];
	}

	public Vector3 GetPoint(int branch, int index)
	{
		return (Vector3)m_WayPoints[branch][index];
	}
}
