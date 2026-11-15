using System.Collections.Generic;
using UnityEngine;

public class RoadPoint
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

	private List<RPoint> m_lsRoadpoints;

	public void Push(Vector3 vPos, Vector3 vGroundNormal)
	{
	}

	public RPoint Get(int idx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public ROADTYPE GetCurrentType()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public ROADTYPE CaculateNextType(Vector3 vLineVelocity)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
