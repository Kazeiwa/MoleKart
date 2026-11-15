using UnityEngine;

public class SimulateKart : btRaycastVehicle
{
	private bool m_bLockMovement;

	public SimulateKart(VehicleData phyData, Transform bodyTransform, Transform lFrontWheel, Transform rFrontWheel, Transform lRearWheel, Transform rRearWheel, EnmCollisionLayer clLayer, CollisionCallback callback)
		: base(phyData, bodyTransform, lFrontWheel, rFrontWheel, lRearWheel, rRearWheel)
	{
		wheels[0] = lFrontWheel;
		wheels[1] = rFrontWheel;
		wheels[3] = lRearWheel;
		wheels[2] = rRearWheel;
		InitChassis(wheels, fFrontTireRadius, fRearTireRadius, fSpring, fsuspensionDistance, bodyTransform.gameObject);
		m_vGravity = m_kRigidBody.mass * Physics.gravity;
		m_kRigidBody.useGravity = false;
		m_kRigidBody.isKinematic = true;
		m_kCompositeCollider = new BodyComponent(bodyTransform.gameObject, this, EnmCollisionLayer.CL_PLAYER);
		m_kBounceSimulator = new BounceSimulator(this);
		m_kCarCollisionCenter = new SimpleCarCollisionDetection();
		m_kCarCollisionCenter.Init(m_kCompositeCollider, callback, this);
		Transform transform = bodyTransform.gameObject.transform.FindChild("collision/bodyhead");
		if (transform != null)
		{
			transform.collider.material = m_kCompositeCollider[0].GetCollider().material;
			transform.gameObject.layer = 19;
		}
		transform = bodyTransform.gameObject.transform.FindChild("collision/bodytail");
		if (transform != null)
		{
			transform.collider.material = m_kCompositeCollider[0].GetCollider().material;
			transform.gameObject.layer = 19;
		}
	}

	public override void Uninit()
	{
		m_kCarCollisionCenter.Unint();
		m_kCompositeCollider.Uninit();
		base.Uninit();
	}

	public override void Hide()
	{
		m_bVisible = false;
		MeshRenderer[] componentsInChildren = m_CenterofMassTransform.FindChild("car/root").GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		m_CenterofMassTransform.FindChild("collision").gameObject.SetActiveRecursively(state: false);
	}

	public override void LockMovement(bool bLock)
	{
		m_bLockMovement = bLock;
	}

	public override bool GetLockMovement()
	{
		return m_bLockMovement;
	}

	public override void EnableKinematic()
	{
		m_kRigidBody.isKinematic = true;
	}

	public override void DisableKinematic()
	{
		m_kRigidBody.isKinematic = true;
	}

	public override bool IsKinematic()
	{
		return m_kRigidBody.isKinematic;
	}

	public override void SetKeepLiveVehicle(btVehicleBase kOther)
	{
		m_kCarCollisionCenter.AddKeepLivePair(kOther);
		kOther.m_kCarCollisionCenter.AddKeepLivePair(this);
	}

	public override void StartBoost(float fTime)
	{
	}

	public override void StartBoost(float fTime, float fSpeed)
	{
	}

	public override void StopBoost()
	{
	}

	public override bool GetBoostMode()
	{
		return false;
	}

	public override bool GetShiftMode()
	{
		return false;
	}

	public override Vector3 GetCurrentMotorForce()
	{
		return Vector3.zero;
	}

	public override bool MoveToGround(Vector3 vPos)
	{
		return true;
	}

	public override void AISteeringNew(bool bAccelation, bool bBrake, bool bBackward, float fSteering)
	{
	}

	public override void Steering(bool bAccelation, bool bBrake, float fSteering)
	{
	}

	public override bool CheckCanDriveNow()
	{
		return false;
	}

	public override void Update()
	{
		m_vPreVelGlobal = m_vVecGlobal;
	}

	public override void UpdateRunningRegion()
	{
	}

	public override void UpdateVehicle()
	{
	}

	public override void ChangeVehicleRunningMode(RUNNING_MDOE eMode, object data)
	{
	}

	public override void OnGUI()
	{
	}
}
