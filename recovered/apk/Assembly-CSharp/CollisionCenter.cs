using System.Collections.Generic;
using UnityEngine;

public class CollisionCenter
{
	private static CollisionCenter instance;

	private List<CarCollisionDetection> m_lst;

	public static CollisionCenter Instance
	{
		get
		{
			if (instance == null)
			{
				new CollisionCenter();
			}
			return instance;
		}
	}

	private CollisionCenter()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public bool Init()
	{
		m_lst = new List<CarCollisionDetection>();
		return true;
	}

	public void Shutdown()
	{
		m_lst.Clear();
	}

	public bool Add(CarCollisionDetection it)
	{
		if (!m_lst.Contains(it))
		{
			m_lst.Add(it);
			return true;
		}
		return false;
	}

	public void Remove(CarCollisionDetection it)
	{
		if (m_lst.Contains(it))
		{
			m_lst.Remove(it);
		}
	}

	public void UpdatePhysx()
	{
		foreach (CarCollisionDetection item in m_lst)
		{
			item.bColliable = true;
		}
		float fixedDeltaTime = Time.fixedDeltaTime;
		foreach (CarCollisionDetection item2 in m_lst)
		{
			item2.UpdatePhysx(fixedDeltaTime);
		}
	}
}
