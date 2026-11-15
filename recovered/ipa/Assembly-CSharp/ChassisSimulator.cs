using System;
using UnityEngine;

public class ChassisSimulator
{
	private struct VibrationSpring
	{
		public float MaxDistance;

		public float x;

		public float v;

		public float k;

		public float time;
	}

	public const float TOUCH_NOTHING = 5000f;

	private const float MIN_FLYHEIGHT = 0.75f;

	public CONTACTSTATE m_eContactState;

	public int m_iContactFlag;

	private Transform[] Wheels;

	private Transform bodyMesh;

	private Transform seatDummy;

	private Vehicle car;

	private VehicleData m_kPhydata;

	public float fFrontTireRadius;

	public float fRearTireRadius;

	public float fsuspensionDistance;

	private VibrationSpring vspring;

	public Vector3[] m_arrPickNormalPre;

	public Vector3[] m_arrPickNormalCur;

	private Transform ChassisTransform;

	private GameObject BodyObject;

	private Vector3 m_vGroundNormal;

	public WheelCollider[] WheelsColliders;

	public NxWheel[] Nxwheels;

	public Vector3 m_vOffsetFL;

	public Vector3 m_vOffsetFR;

	public Vector3 m_vOffsetRR;

	public Vector3 m_vOffsetRL;

	public float m_fFrontDistance;

	public float m_fBackDistacne;

	private Rigidbody kBody;

	private int m_WheelCount;

	protected bool bFallingDown;

	protected Matrix4x4 m_mtChassis;

	protected float[] m_afDistanceToGround;

	public Vector3[] m_arrGroundNormal;

	public Vector3[] m_arrGroundPoint;

	private int[] aIndicesOrg;

	private float m_fChassisAdjustTime;

	public Vector3 vFrontRight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vFrontLeft
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vFrontMid
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vBackLeft
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vBackRight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vLocalMidFrontWheels
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 vLocalMidRearWheels
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

	public ChassisSimulator(Transform[] wheels, float fFrontTireRadius, float fRearTireRadius, float fSpring, float fsuspensionDistance, GameObject body, Vehicle car, VehicleData kData)
	{
	}

	public void Uninit()
	{
	}

	private void CaculateMassCenter(GameObject body)
	{
	}

	public void ResetMassCenter()
	{
	}

	public void OffsetMassCenter(float fOffset)
	{
	}

	public Vector3 GetWorldPosition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetWorldPosition(Vector3 vPos)
	{
	}

	public Matrix4x4 GetMat()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 TransformToWorld(Vector3 vLocal)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public WheelCollider[] GetWheels(out int iWheels)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void EnableWheels()
	{
	}

	public void DisableWheels()
	{
	}

	public Vector3 GetWheelsPosition(int index, out Vector3 vNormal)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void MoveWheels(int idx, Vector3 vPos)
	{
	}

	public void SimpleHarmonicVibration(float fTickTime)
	{
	}

	public void SynchronizeWheel(float fAngle, float fRpm, float fTime, ENMMOTIONDIR VelocityDir)
	{
	}

	public void SimulateFlying(float fTime, ENMMOTIONDIR VelocityDir)
	{
	}

	private float GetMinDistToGround()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetDistToGround(int iWheelIndex)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SortDec<T>(T[] aV, int[] aIndices, int num) where T : IComparable<T>
	{
	}

	private void UpdateDistToGround()
	{
	}

	private void ChassisAdjust()
	{
	}

	private void UpdateContaceState()
	{
	}

	public Vector3 GetUpGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetForwardGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetRightGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Quaternion GetRootRotate()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 LocalToWroldPoint(Vector3 vPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetVelGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetGroundNormal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
