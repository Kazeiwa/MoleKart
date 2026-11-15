using System.Collections.Generic;
using UnityEngine;

public abstract class btRaycastVehicle : btVehicleBase
{
	private const float DRAG = 5f;

	private const float RESISTANCE = 50f;

	private const float CA_R = -5.2f;

	private const float CA_F = -5f;

	private const float MAX_GRIP = 2f;

	protected float m_tau;

	protected float m_damping;

	protected btVehicleRaycaster m_vehicleRaycaster;

	protected float m_pitchControl;

	protected float m_steeringValue;

	protected int m_indexRightAxis;

	protected int m_indexUpAxis;

	protected int m_indexForwardAxis;

	protected float m_fSteeringAngle;

	protected List<btWheelInfo> m_wheelInfo;

	public CONTACTSTATE m_eContactState;

	protected GameObject BodyObject;

	public float m_fFrontDistance;

	public float m_fBackDistacne;

	protected Vector2 m_vTraction;

	protected Vector2 m_vResistance;

	private Vector2 velocity_wc;

	protected float motortorque;

	protected float braketorque;

	private float m_track_connect_accel;

	private int iCurIngore;

	private Vector3 m_vGroundNormal;

	public Vector3[] m_arrGroundNormal;

	public Vector3[] m_arrGroundPoint;

	public btRaycastVehicle(VehicleData phydata, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel, Car car)
	{
	}

	private bool Init(btVehicleTuning tuning, Rigidbody chassis, btVehicleRaycaster raycaster)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void defaultInit(btVehicleTuning tuning)
	{
	}

	public void InitChassis(Transform[] wheels, float fFrontTireRadius, float fRearTireRadius, float fSpring, float fsuspensionDistance, GameObject body)
	{
	}

	private void CaculateMassCenter(GameObject body)
	{
	}

	private btWheelInfo addWheel(Vector3 connectionPointCS, Vector3 wheelDirectionCS0, Vector3 wheelAxleCS, float suspensionRestLength, float wheelRadius, btVehicleTuning tuning, bool isFrontWheel)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected int getNumWheels()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected btWheelInfo Wheels(int idx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Transform getChassisWorldTransform()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void updateWheelTransformsWS(btWheelInfo wheel, bool interpolatedTransform)
	{
	}

	private void updateWheelTransform(int wheelIndex, bool interpolatedTransform)
	{
	}

	protected bool projectVehicleToSurface(Vector3 ray, bool btranslate_vehicle)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float rayCast(btWheelInfo wheel)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float rayCast(btWheelInfo wheel, Vector3 ray)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetwheelsPosition(int idx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void DoWheelSteering(float fTickTime)
	{
	}

	public void doSteering(float fTickTime)
	{
	}

	public override void SimulateFlying(float fTime, ENMMOTIONDIR VelocityDir)
	{
	}

	public void SynchronizeWheel(float fAngle, float fRpm, float fTime, ENMMOTIONDIR VelocityDir)
	{
	}

	public override Vector3 GetGroundNormal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void UpdateContaceState()
	{
	}

	public override Vector3 GetUpGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetForwardGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override Vector3 GetRightGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void RotateCarLocal(float fAngleX, float fAngleY, float fAngleZ)
	{
	}

	public override void RotateAroundRearWheelsWorldUp(float fAngle, bool bRotateVelocity)
	{
	}

	public override void RotateAroundPoint(Vector3 vOrigo, float fAngle, bool bRotateVelocity)
	{
	}

	public override void TranslateCarLocal(float OffsetX, float OffsetY, float OffsetZ)
	{
	}

	public override void TranslateCarWorld(Vector3 vOffset)
	{
	}

	public override void OnDrawGizmos()
	{
	}
}
