using System.Collections;
using UnityEngine;

public class Tortoise : Prop
{
	protected float m_EftTime;

	protected float m_LifeTime;

	protected bool m_Throwing;

	protected Vector3 m_ThrowDir;

	protected Car m_SurroundCar;

	protected bool m_Forward;

	protected int m_CurrentWaypoint;

	public float TORTOISE_LIFE_TIME;

	protected float m_FlyingSpeed;

	protected float m_ThrowSpeed;

	public float TORTOISE_ROTATE_RADIUS;

	public float TORTOISE_ROTATE_SPEED;

	protected TrailRenderer m_TrailRender;

	protected int m_Branch;

	protected ArrayList m_WayPoints;

	protected Vector3 m_InitForward;

	protected GameObject m_OwnerCar;

	protected bool m_Droped;

	protected ArrayList pts;

	protected float m_PathLength;

	protected GameObject obj_Explode;

	protected Effect eft_Explode;

	public Car SurroundCar
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override bool IsPickable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override bool IsDangerous
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override bool IsMoving
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override void Reset()
	{
	}

	protected void RotateSurroundCar()
	{
	}

	private void RotatePositionAdjust()
	{
	}

	protected void PrepareDeleteObject(float time)
	{
	}

	protected ArrayList GetBranchWaypoints()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Vector3 Interp(ArrayList pts, float t, float pathLength)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected bool CreatePath()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void FollowWaypoint()
	{
	}

	protected int FindNearByWaypoint()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected bool CheckTouchGround()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ThrowFromPos(GameObject ownerCar, Vector3 vPos, Vector3 vDir, Vector3 right, float fRelationspeed, bool forward)
	{
	}

	public void DropAtPos(Vector3 pos, Vector3 dir, float speed)
	{
	}

	protected void PlayExplodeEft(float time, eEffectType type)
	{
	}

	protected void StopSurroundSound()
	{
	}
}
