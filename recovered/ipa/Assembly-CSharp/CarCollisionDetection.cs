using UnityEngine;

public class CarCollisionDetection
{
	protected ICollisionCallback callback;

	protected BodyComponent m_body;

	protected CollisionLayer kLayer;

	protected btVehicleBase m_car;

	protected ShiftHelper m_kShiftHelper;

	protected bool m_bEnable;

	protected btVehicleBase m_kKeepPair;

	private Vector3 vReflect;

	private Vector3 vWallNormal;

	private Vector3 vDir;

	private Vector3 vCollisionPos;

	private Vector3 vHitForce;

	public bool bColliable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual void Init(BodyComponent body, ICollisionCallback callback, btVehicleBase car)
	{
	}

	public virtual void Unint()
	{
	}

	private float ClampEulerAngle(float fAngle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void UpdatePhysx(float fTicktime)
	{
	}

	public void AddKeepLivePair(btVehicleBase kOther)
	{
	}

	protected bool CheckKeepLivePair(btVehicleBase kPair)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ProccessCarToTreeAndBox()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected bool ProccessVehicleToFenceEx()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected bool ProccessCarToFenceEx()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected Vector3 GetEscapeDirection(btVehicleBase car, Vector3 vPos, VehicleCollisionSide eside)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ProccessCarToCar()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected VehicleCollisionSide CheckCollision(btVehicleBase from, btVehicleBase to)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private VehicleCollisionSide CheckCollisionPoint(btVehicleBase from, Vector3 vPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float GetTime(float fOffset)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float GetSpeed(float fOffset)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void GetCollisionParameters(btVehicleBase carfrom, btVehicleBase carto, out float fOffset0, out float fOffset1, out float fTime0, out float fTime1, out float fSpeed0, out float fSpeed1)
	{
	}

	public bool IsSimulating()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
