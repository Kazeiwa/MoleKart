using System.Collections;
using UnityEngine;

public class Missile : Prop
{
	private Car m_PlayerCar;

	private int m_CurrentWaypoint;

	private int m_Branch;

	private float m_FlyingTime;

	private static GameObject m_MainCamera;

	public float MISSILE_LIFE_TIME;

	private float m_FlySpeed;

	protected ArrayList m_WayPoints;

	private bool m_PlayDisappearEft;

	private ArrayList pts;

	private float m_PathLength;

	private bool m_bFlySoundPlayed;

	private void Start()
	{
	}

	public override void Reset()
	{
	}

	private void FixedUpdate()
	{
	}

	private void ReCoverCarConfig()
	{
	}

	private void HideMissile()
	{
	}

	private void ShowMissile(bool enable)
	{
	}

	private Vector3 Interp(ArrayList pts, float t, float pathLength)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void FollowWaypoint()
	{
	}

	protected override void OnTriggerEnter(Collider other)
	{
	}

	public void SetCar(Car car, GameObject mainCamera)
	{
	}

	private bool CreatePath()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected ArrayList GetBranchWaypoints()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private int FindNearByWaypoint(bool step)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
