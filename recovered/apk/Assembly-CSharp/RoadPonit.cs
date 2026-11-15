using System.Collections.Generic;
using UnityEngine;

public class RoadPonit
{
	public enum ROADTYPE
	{
		RT_UP,
		RT_DOWN,
		RT_FLAT,
		RT_SIDESTEP,
		RT_GAP
	}

	public struct RPoint
	{
		public Vector3 vPosition;

		public Vector3 vNormal;
	}

	private const int MAX_POINTS = 10;

	private List<RPoint> m_lsRoadpoints = new List<RPoint>();

	public void Push(Vector3 vPos, Vector3 vGroundNormal)
	{
		RPoint item = new RPoint
		{
			vPosition = vPos,
			vNormal = vGroundNormal
		};
		m_lsRoadpoints.Add(item);
		if (m_lsRoadpoints.Count > 10)
		{
			m_lsRoadpoints.RemoveAt(0);
		}
	}

	public RPoint Get(int idx)
	{
		return m_lsRoadpoints[idx];
	}

	public ROADTYPE GetCurrentType()
	{
		ROADTYPE result = ROADTYPE.RT_FLAT;
		if (m_lsRoadpoints.Count > 2)
		{
			Vector3 vNormal = m_lsRoadpoints[0].vNormal;
			Vector3 vNormal2 = m_lsRoadpoints[1].vNormal;
			Vector3 vNormal3 = m_lsRoadpoints[2].vNormal;
			float num = Vector3.Dot(Vector3.Cross(vNormal, vNormal2), Vector3.Cross(vNormal2, vNormal3));
			if (num < 0f)
			{
				float num2 = Vector3.Angle(vNormal, vNormal2);
				float num3 = Vector3.Angle(vNormal2, vNormal3);
				if (num2 > 10f || num3 > 10f)
				{
					result = ROADTYPE.RT_SIDESTEP;
				}
			}
		}
		return result;
	}

	public ROADTYPE CaculateNextType(Vector3 vLineVelocity)
	{
		ROADTYPE result = ROADTYPE.RT_FLAT;
		if (m_lsRoadpoints.Count >= 2)
		{
			vLineVelocity.Normalize();
			Vector3 vWorldPos = m_lsRoadpoints[0].vPosition + vLineVelocity * 2f;
			if (CollisionLayer.Instance.VerticalIntersectHitPosition(vWorldPos, out var vGroundNormal, out var _))
			{
				vWorldPos = vGroundNormal;
				Vector3 vNormal = m_lsRoadpoints[0].vNormal;
				Vector3 vNormal2 = m_lsRoadpoints[1].vNormal;
				float num = Vector3.Dot(Vector3.Cross(vWorldPos, vNormal), Vector3.Cross(vNormal, vNormal2));
				if (num < 0f)
				{
					float num2 = Vector3.Angle(vWorldPos, vNormal);
					float num3 = Vector3.Angle(vNormal, vNormal2);
					if (num2 > 5f || num3 > 5f)
					{
						result = ROADTYPE.RT_SIDESTEP;
					}
				}
			}
		}
		return result;
	}
}
