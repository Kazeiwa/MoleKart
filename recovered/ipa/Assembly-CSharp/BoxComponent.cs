using UnityEngine;

public class BoxComponent : CCollisionBody
{
	protected class BoxData
	{
		public Vector3 center;

		public Vector3 size;

		public float scale;

		public Vector3 vFR;

		public Vector3 vFL;

		public Vector3 vBR;

		public Vector3 vBL;

		public Vector3 vForwardDir;

		public Vector3 vUp;
	}

	private Transform m_tsCollider;

	public EnmCollisionSide m_state;

	public Vector3 CollisionPos;

	public Vector3 GroundNormal;

	public float DistanceToGround;

	protected BoxData m_BoxData;

	public Vector3 vFR
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vFL
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vBR
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vBL
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vBoxCenter
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vForwardDir
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 size
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float scale
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 up
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public BoxComponent(GameObject gameobject, EnmCollisionLayer clLayer)
	{
	}

	public BoxComponent(GameObject gameobject, float fOffsetDistance, EnmCollisionLayer clLayer)
	{
	}

	public virtual void SetCurState(EnmCollisionSide curState)
	{
	}

	private void InitBoxData(BoxCollider kBoxCollider)
	{
	}

	public void MoveToGround()
	{
	}
}
