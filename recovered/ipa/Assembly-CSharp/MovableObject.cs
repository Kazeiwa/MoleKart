using System;
using System.Collections;
using UnityEngine;

public class MovableObject
{
	protected RUNNING_MDOE m_eRunnigMode;

	public Vector3 m_vVecGlobal;

	public Rigidbody m_kRigidBody;

	public Transform m_CenterofMassTransform;

	public RUNNINGSTATES m_state;

	protected float m_fYaw;

	protected float m_fPitch;

	protected float m_fBank;

	protected float m_fAngularVelocity;

	protected Vector3 m_vWecGlobal;

	protected ENMLOCKMOTION m_eLockMotion;

	protected ArrayList arrForcePool;

	protected ArrayList arrTwistPool;

	protected Vector3 m_vTotalForce;

	protected Vector3 m_vTotalTwist;

	public Vector3 m_DrawForce;

	public Vector3 m_DrawFriction;

	public Vector3 m_DrawMotor;

	private int[] aIndicesOrg;

	public RUNNING_MDOE RunningMode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ENMLOCKMOTION LockMotion
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vTotalForce
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vTotalTwist
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void SetPosition(Vector3 pos)
	{
	}

	public Vector3 GetPosition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetRotation(Quaternion rotation)
	{
	}

	public Quaternion GetRotation()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetVecGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetVecGlobal(Vector3 vec)
	{
	}

	public void SetAngularVec(Vector3 avec)
	{
	}

	public float GetSpeed()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetVecXGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetVecXGlobal(float value)
	{
	}

	public float GetVecYGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetVecYGlobal(float value)
	{
	}

	public float GetVecZGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetVecZGlobal(float value)
	{
	}

	public Vector3 GetVecLocal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetEulerAngle()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool SetEulerAngle(Vector3 vEuler)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetEulerAngleX(float fAngle)
	{
	}

	public void SetEulerAngleY(float fAngle)
	{
	}

	public float GetEulerAngleX()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetEulerAngleY()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetEulerAngleZ()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetWecGlobal(Vector3 vWec)
	{
	}

	public Vector3 GetWecGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetWecLocal(Vector3 vWec)
	{
	}

	public Vector3 GetWecLocal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void LockRotation(bool bLockX, bool bLockY, bool bLockZ)
	{
	}

	public void AddForceGlobal(Vector3 vForce, ENMFORCETYPE eType, float fTime)
	{
	}

	public void AddForceAtPointGlobal(Vector3 vForce, Vector3 vPosition, float fTime)
	{
	}

	public void TriggerForceTwistPool(float fTicktime)
	{
	}

	public void SortDec<T>(T[] aV, int[] aIndices, int num) where T : IComparable<T>
	{
	}
}
