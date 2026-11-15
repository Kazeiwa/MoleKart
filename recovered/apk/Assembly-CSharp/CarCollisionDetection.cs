using System;
using UnityEngine;

public class CarCollisionDetection
{
	protected CollisionCallback callback;

	protected BodyComponent m_body;

	protected CollisionLayer kLayer;

	protected btVehicleBase m_car;

	protected ShiftHelper m_kShiftHelper;

	protected bool m_bEnable = true;

	protected btVehicleBase m_kKeepPair;

	private Vector3 vReflect = Vector3.zero;

	private Vector3 vWallNormal = Vector3.zero;

	private Vector3 vDir = Vector3.zero;

	private Vector3 vCollisionPos = Vector3.zero;

	private Vector3 vHitForce = Vector3.zero;

	public bool bColliable
	{
		get
		{
			return m_bEnable;
		}
		set
		{
			m_bEnable = value;
		}
	}

	public virtual void Init(BodyComponent body, CollisionCallback callback, btVehicleBase car)
	{
		m_body = body;
		m_car = car;
		kLayer = CollisionLayer.Instance;
		this.callback = callback;
		if (null == m_kShiftHelper)
		{
			m_kShiftHelper = m_car.GetNode().gameObject.AddComponent<ShiftHelper>();
			m_kShiftHelper.SetTarget(m_car, this);
		}
		CollisionCenter.Instance.Add(this);
	}

	public virtual void Unint()
	{
		CollisionCenter.Instance.Remove(this);
	}

	private float ClampEulerAngle(float fAngle)
	{
		if (fAngle >= 360f)
		{
			fAngle -= 360f;
		}
		if (fAngle < 0f)
		{
			fAngle += 360f;
		}
		return fAngle;
	}

	public virtual void UpdatePhysx(float fTicktime)
	{
		m_car.CollisionSide = VehicleCollisionSide.CS_NONE;
		if (m_car.Flag_ProccessCarToTreeAndBox || m_car.IsKinematic())
		{
			ProccessCarToTreeAndBox();
		}
		if (m_car.Flag_ProccessCarToFenceEx || m_car.IsKinematic())
		{
			if (m_car is Kart)
			{
				ProccessCarToFenceEx();
			}
			else if (m_car is Vehicle)
			{
				ProccessVehicleToFenceEx();
			}
		}
		if (m_car.Flag_ProccessCarToCar)
		{
			ProccessCarToCar();
		}
	}

	public void AddKeepLivePair(btVehicleBase kOther)
	{
		m_kKeepPair = kOther;
	}

	protected bool CheckKeepLivePair(btVehicleBase kPair)
	{
		if (m_kKeepPair != null && m_kKeepPair == kPair)
		{
			if (CheckCollision(m_car, m_kKeepPair) != VehicleCollisionSide.CS_NONE)
			{
				return true;
			}
			m_kKeepPair = null;
		}
		return false;
	}

	private bool ProccessCarToTreeAndBox()
	{
		Vector3 position = m_car.GetPosition();
		float boundRadius = m_car.GetBody().BoundRadius;
		int layerMask = 147456;
		if (Physics.CheckSphere(position, boundRadius, layerMask))
		{
			m_car.CollisionSide = VehicleCollisionSide.CS_FRONT;
			vDir = m_car.GetVecGlobal();
			float magnitude = vDir.magnitude;
			vDir.Normalize();
			float num = 0.5f;
			if (magnitude > 20f)
			{
				bool flag = true;
				do
				{
					flag = Physics.CheckSphere(position, boundRadius, layerMask);
					if (flag)
					{
						float num2 = 0.1f;
						position -= vDir * num2;
						m_car.SetPosition(position);
					}
				}
				while (flag);
			}
			if (m_car.RunningMode == RUNNING_MDOE.RM_FLYING)
			{
				m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
				vReflect = -vDir;
				vReflect.y = 0f;
				float num3 = magnitude * num;
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				vReflect *= num3;
				m_car.SetVecGlobal(vReflect);
				m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_FALLINGDOWN, null);
			}
			return true;
		}
		return false;
	}

	protected bool ProccessVehicleToFenceEx()
	{
		bool result = false;
		EnmCollisionSide enmCollisionSide = EnmCollisionSide.NOTTOUCH;
		BoxComponent cComponent = m_body[0];
		bool isHero = m_car is Vehicle;
		enmCollisionSide = kLayer.DetectToGuardBar(cComponent, 0, isHero);
		if (enmCollisionSide != EnmCollisionSide.NOTTOUCH)
		{
			result = true;
			m_car.CollisionSide |= VehicleCollisionSide.CS_FRONT;
			m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
		}
		cComponent = m_body[0];
		EnmCollisionSide enmCollisionSide2 = kLayer.DetectToGuardBar(cComponent, 1, isHero);
		if (enmCollisionSide2 != EnmCollisionSide.NOTTOUCH)
		{
			result = true;
			m_car.CollisionSide |= VehicleCollisionSide.CS_BACK;
			if (m_car.VelocityDir == ENMMOTIONDIR.ECMD_BACK)
			{
				m_car.SetVecGlobal(Vector3.zero);
			}
			enmCollisionSide |= enmCollisionSide2;
		}
		if (enmCollisionSide != EnmCollisionSide.NOTTOUCH && callback != null)
		{
			callback.OnHitFence(cComponent.GetCollider(), null);
		}
		return result;
	}

	protected bool ProccessCarToFenceEx()
	{
		bool result = false;
		EnmCollisionSide enmCollisionSide = EnmCollisionSide.NOTTOUCH;
		BoxComponent boxComponent = m_body[0];
		bool isHero = m_car is Vehicle;
		enmCollisionSide = kLayer.DetectToGuardBar(boxComponent, 0, isHero);
		if (enmCollisionSide != EnmCollisionSide.NOTTOUCH)
		{
			result = true;
			m_car.CollisionSide |= VehicleCollisionSide.CS_FRONT;
			vDir = m_car.GetVecGlobal();
			float magnitude = vDir.magnitude;
			vDir.Normalize();
			Vector3 collisionNormal = boxComponent.CollisionNormal;
			vCollisionPos = boxComponent.CollisionPos;
			collisionNormal.y = 0f;
			collisionNormal.Normalize();
			vWallNormal = collisionNormal;
			if (Vector3.Dot(vDir, collisionNormal) > 0f)
			{
				return false;
			}
			vReflect = vDir - 2f * Vector3.Dot(vDir, vWallNormal) * vWallNormal;
			vReflect.Normalize();
			float num = 0.5f;
			if (magnitude > 5f)
			{
				float num2 = magnitude / 30f;
				float num3 = magnitude * num;
				if (num3 > 10f)
				{
					num3 = 10f;
				}
				if (m_car.RunningMode != RUNNING_MDOE.RM_BOUNCE)
				{
					m_car.TranslateCarLocal(0f, 2f * num2, 0f);
					m_car.TranslateCarWorld(vWallNormal * 1f * num2);
					Vector3 lhs = vDir;
					lhs.y = 0f;
					lhs.Normalize();
					Vector3 rhs = Vector3.Cross(lhs, collisionNormal);
					Vector3 vector = Vector3.Cross(collisionNormal, rhs);
					vector.y = 0f;
					vector.Normalize();
					vReflect = vector;
					if (enmCollisionSide == EnmCollisionSide.TOUCHFRONTLEFT)
					{
						m_car.RotateAroundRearWheelsWorldUp(20f, bRotateVelocity: true);
					}
					else
					{
						m_car.RotateAroundRearWheelsWorldUp(-20f, bRotateVelocity: true);
					}
					VehicleCollisionSide vehicleCollisionSide = CheckCollisionPoint(m_car, vCollisionPos);
					Vector3 rightGlobal = m_car.GetRightGlobal();
					if ((vehicleCollisionSide & VehicleCollisionSide.CS_RIGHT) == VehicleCollisionSide.CS_RIGHT)
					{
						rightGlobal = -rightGlobal;
					}
					if (m_car.VelocityDir == ENMMOTIONDIR.ECMD_FORWARD)
					{
						Vector3 vecGlobal = m_car.GetForwardGlobal() * m_car.GetVelocity() * 0.5f;
						m_car.SetVecGlobal(vecGlobal);
					}
					else
					{
						Vector3 vecGlobal2 = -m_car.GetForwardGlobal() * m_car.GetVelocity() * 0.5f;
						m_car.SetVecGlobal(vecGlobal2);
					}
					m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_BOUNCE, null);
					float fV = Mathf.Abs(Vector3.Dot(collisionNormal, m_car.m_vVecGlobal));
					BounceSimulator bounceSimulator = m_car.GetBounceSimulator();
					bounceSimulator.Begin(fV, collisionNormal, vehicleCollisionSide);
				}
			}
			else
			{
				float num4 = Math.Min(magnitude, 4f);
				m_car.SetVecGlobal(vDir * num4);
			}
		}
		boxComponent = m_body[0];
		EnmCollisionSide enmCollisionSide2 = kLayer.DetectToGuardBar(boxComponent, 1, isHero);
		if (enmCollisionSide2 != EnmCollisionSide.NOTTOUCH)
		{
			result = true;
			m_car.CollisionSide |= VehicleCollisionSide.CS_BACK;
			if (m_car.VelocityDir == ENMMOTIONDIR.ECMD_BACK)
			{
				m_car.SetVecGlobal(Vector3.zero);
			}
			enmCollisionSide |= enmCollisionSide2;
		}
		if (enmCollisionSide != EnmCollisionSide.NOTTOUCH && callback != null)
		{
			callback.OnHitFence(boxComponent.GetCollider(), null);
		}
		return result;
	}

	public void Trace()
	{
		Debug.DrawRay(vCollisionPos, vWallNormal, Color.green);
		Debug.DrawRay(vCollisionPos, vReflect, Color.black);
		Debug.DrawRay(vCollisionPos, vDir, Color.red);
		Debug.DrawRay(vCollisionPos, vHitForce, Color.blue);
	}

	protected Vector3 GetEscapeDirection(btVehicleBase car, Vector3 vPos, VehicleCollisionSide eside)
	{
		Vector3 vector = car.GetRightGlobal();
		if ((eside & VehicleCollisionSide.CS_RIGHT) == VehicleCollisionSide.CS_RIGHT)
		{
			vector = -vector;
		}
		if (Physics.SphereCast(new Ray
		{
			origin = vPos,
			direction = vector
		}, car.GetBody().BoundRadius, car.GetBody().BoundRadius, 16896))
		{
			vector = Vector3.zero;
		}
		return vector;
	}

	private bool ProccessCarToCar()
	{
		if (!bColliable || !m_car.bVisible)
		{
			return false;
		}
		bColliable = false;
		Vector3 position = m_car.GetPosition();
		foreach (btVehicleBase item in btVehicleBase.ms_ListVehicel)
		{
			if (item == m_car || CheckKeepLivePair(item) || !item.bVisible || item.bRemoteCollider)
			{
				continue;
			}
			VehicleCollisionSide vehicleCollisionSide = CheckCollision(m_car, item);
			Vector3 position2 = item.GetPosition();
			if ((vehicleCollisionSide & VehicleCollisionSide.CS_RIGHT) == VehicleCollisionSide.CS_RIGHT)
			{
				item.m_kCarCollisionCenter.bColliable = false;
				if (callback != null && callback.OnHitAnotherCar(item, vehicleCollisionSide))
				{
					continue;
				}
				Vector3 vecGlobal = m_car.GetVecGlobal();
				Vector3 vecGlobal2 = item.GetVecGlobal();
				GetCollisionParameters(m_car, item, out var fOffset, out var fOffset2, out var fTime, out var fTime2, out var fSpeed, out var fSpeed2);
				m_car.SetVecGlobal(vecGlobal * fSpeed);
				item.SetVecGlobal(vecGlobal2 * fSpeed2);
				Vector3 lhs = vecGlobal;
				Vector3 rhs = vecGlobal2;
				lhs.Normalize();
				rhs.Normalize();
				if (Vector3.Dot(lhs, rhs) > 0.5f)
				{
					if (fSpeed < 1f && m_car is Kart)
					{
						m_car.RotateAroundRearWheelsWorldUp(-10f * (1f - fSpeed * 0.5f), bRotateVelocity: true);
					}
					if (fSpeed2 < 1f && item is Kart)
					{
						item.RotateAroundRearWheelsWorldUp(10f * (1f - fSpeed2 * 0.5f), bRotateVelocity: true);
					}
				}
				VehicleCollisionSide eside = CheckCollision(item, m_car);
				Vector3 escapeDirection = GetEscapeDirection(m_car, position, vehicleCollisionSide);
				Vector3 escapeDirection2 = GetEscapeDirection(item, position2, eside);
				fOffset = 0.3f * fOffset * m_car.GetBody().BoundRadius;
				fOffset2 = 0.3f * fOffset2 * item.GetBody().BoundRadius;
				if (fTime != 0f)
				{
					VehicleCollisionSimulator collisionSimulator = m_car.GetCollisionSimulator();
					collisionSimulator.Begin(fOffset, escapeDirection, vehicleCollisionSide, fTime);
					m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_SIDESLIP, null);
				}
				if (fTime2 != 0f)
				{
					VehicleCollisionSimulator collisionSimulator2 = item.GetCollisionSimulator();
					collisionSimulator2.Begin(fOffset2, escapeDirection2, eside, fTime2);
					item.ChangeVehicleRunningMode(RUNNING_MDOE.RM_SIDESLIP, null);
				}
				m_car.CollisionSide |= vehicleCollisionSide;
				return true;
			}
			if ((vehicleCollisionSide & VehicleCollisionSide.CS_LEFT) != VehicleCollisionSide.CS_LEFT)
			{
				continue;
			}
			item.m_kCarCollisionCenter.bColliable = false;
			if (callback != null && callback.OnHitAnotherCar(item, vehicleCollisionSide))
			{
				continue;
			}
			Vector3 vecGlobal3 = m_car.GetVecGlobal();
			Vector3 vecGlobal4 = item.GetVecGlobal();
			GetCollisionParameters(m_car, item, out var fOffset3, out var fOffset4, out var fTime3, out var fTime4, out var fSpeed3, out var fSpeed4);
			m_car.SetVecGlobal(vecGlobal3 * fSpeed3);
			item.SetVecGlobal(vecGlobal4 * fSpeed4);
			Vector3 lhs2 = vecGlobal3;
			Vector3 rhs2 = vecGlobal4;
			lhs2.Normalize();
			rhs2.Normalize();
			if (Vector3.Dot(lhs2, rhs2) > 0.5f)
			{
				if (fSpeed3 < 1f && m_car is Kart)
				{
					m_car.RotateAroundRearWheelsWorldUp(10f * (1f - fSpeed3 * 0.5f), bRotateVelocity: true);
				}
				if (fSpeed4 < 1f && item is Kart)
				{
					item.RotateAroundRearWheelsWorldUp(-10f * (1f - fSpeed4 * 0.5f), bRotateVelocity: true);
				}
			}
			VehicleCollisionSide eside2 = CheckCollision(item, m_car);
			Vector3 escapeDirection3 = GetEscapeDirection(m_car, position, vehicleCollisionSide);
			Vector3 escapeDirection4 = GetEscapeDirection(item, position2, eside2);
			fOffset3 = 0.3f * fOffset3 * m_car.GetBody().BoundRadius;
			fOffset4 = 0.3f * fOffset4 * item.GetBody().BoundRadius;
			if (fTime3 != 0f)
			{
				VehicleCollisionSimulator collisionSimulator3 = m_car.GetCollisionSimulator();
				collisionSimulator3.Begin(fOffset3, escapeDirection3, vehicleCollisionSide, fTime3);
				m_car.ChangeVehicleRunningMode(RUNNING_MDOE.RM_SIDESLIP, null);
			}
			if (fTime4 != 0f)
			{
				VehicleCollisionSimulator collisionSimulator4 = item.GetCollisionSimulator();
				collisionSimulator4.Begin(fOffset4, escapeDirection4, eside2, fTime4);
				item.ChangeVehicleRunningMode(RUNNING_MDOE.RM_SIDESLIP, null);
			}
			m_car.CollisionSide |= vehicleCollisionSide;
			return true;
		}
		return false;
	}

	protected VehicleCollisionSide CheckCollision(btVehicleBase from, btVehicleBase to)
	{
		VehicleCollisionSide vehicleCollisionSide = VehicleCollisionSide.CS_NONE;
		Vector3 position = from.GetPosition();
		Vector3 position2 = to.GetPosition();
		position = position2 - position;
		float magnitude = position.magnitude;
		if (magnitude <= from.GetBody().BoundRadius + to.GetBody().BoundRadius)
		{
			position.Normalize();
			float num = Vector3.Angle(position, from.GetForwardGlobal());
			if (num != 0f && Vector3.Dot(Vector3.up, Vector3.Cross(position, from.GetForwardGlobal())) < 0f)
			{
				num = 0f - num;
			}
			bool flag = false;
			if (Mathf.Abs(num) <= 15f)
			{
				vehicleCollisionSide |= VehicleCollisionSide.CS_FRONT;
				float f = Vector3.Dot(to.GetForwardGlobal(), from.GetForwardGlobal());
				if (Mathf.Abs(f) < 0.05f)
				{
					flag = true;
				}
			}
			else if (Mathf.Abs(num) >= 165f)
			{
				vehicleCollisionSide |= VehicleCollisionSide.CS_BACK;
			}
			if (!flag)
			{
				vehicleCollisionSide = ((!(num > 0f)) ? (vehicleCollisionSide | VehicleCollisionSide.CS_RIGHT) : (vehicleCollisionSide | VehicleCollisionSide.CS_LEFT));
			}
			else
			{
				float num2 = Vector3.Dot(to.GetForwardGlobal(), from.GetRightGlobal());
				vehicleCollisionSide = ((!(num2 > 0f)) ? (vehicleCollisionSide | VehicleCollisionSide.CS_LEFT) : (vehicleCollisionSide | VehicleCollisionSide.CS_RIGHT));
			}
		}
		return vehicleCollisionSide;
	}

	private VehicleCollisionSide CheckCollisionPoint(btVehicleBase from, Vector3 vPos)
	{
		VehicleCollisionSide vehicleCollisionSide = VehicleCollisionSide.CS_NONE;
		Vector3 position = from.GetPosition();
		position = vPos - position;
		position.Normalize();
		float num = Vector3.Angle(position, from.GetForwardGlobal());
		if (num != 0f && Vector3.Dot(from.GetUpGlobal(), Vector3.Cross(position, from.GetForwardGlobal())) < 0f)
		{
			num = 0f - num;
		}
		if (Mathf.Abs(num) <= 15f)
		{
			vehicleCollisionSide |= VehicleCollisionSide.CS_FRONT;
		}
		else if (Mathf.Abs(num) >= 165f)
		{
			vehicleCollisionSide |= VehicleCollisionSide.CS_BACK;
		}
		if (num > 0f)
		{
			return vehicleCollisionSide | VehicleCollisionSide.CS_LEFT;
		}
		return vehicleCollisionSide | VehicleCollisionSide.CS_RIGHT;
	}

	private float GetTime(float fOffset)
	{
		float num = 0.3f;
		if (fOffset == 0f)
		{
			return 0f;
		}
		if (fOffset <= 10f)
		{
			return 0.3f;
		}
		if (fOffset > 10f && fOffset <= 20f)
		{
			return 0.5f;
		}
		if (fOffset > 20f && fOffset <= 30f)
		{
			return 0.8f;
		}
		if (fOffset > 30f && fOffset <= 40f)
		{
			return 1f;
		}
		if (fOffset > 40f && fOffset <= 50f)
		{
			return 1.2f;
		}
		return 1.2f;
	}

	private float GetSpeed(float fOffset)
	{
		float num = 1f;
		if (fOffset <= 0f)
		{
			return 1f;
		}
		if (fOffset <= 10f)
		{
			return 0.9f;
		}
		if (fOffset > 10f && fOffset <= 20f)
		{
			return 0.8f;
		}
		if (fOffset > 20f && fOffset <= 30f)
		{
			return 0.6f;
		}
		if (fOffset > 30f && fOffset <= 40f)
		{
			return 0.5f;
		}
		if (fOffset > 40f && fOffset <= 50f)
		{
			return 0.4f;
		}
		return 0.4f;
	}

	protected void GetCollisionParameters(btVehicleBase carfrom, btVehicleBase carto, out float fOffset0, out float fOffset1, out float fTime0, out float fTime1, out float fSpeed0, out float fSpeed1)
	{
		fOffset0 = 0f;
		fOffset1 = 1f;
		float num = carfrom.GetVelocity();
		float num2 = carto.GetVelocity();
		if (num > 40f)
		{
			num = 40f;
		}
		if (num2 > 40f)
		{
			num2 = 40f;
		}
		float num3 = carfrom.GetMass() * num;
		float num4 = carto.GetMass() * num2;
		int num5 = (int)(num3 / 10000f);
		int num6 = (int)(num4 / 10000f);
		if (carfrom.GetMaxColliderMass())
		{
			num5 = 12;
		}
		if (carto.GetMaxColliderMass())
		{
			num6 = 12;
		}
		fOffset0 = CollisionTable.GetCollisionOffset(num5, num6);
		fOffset1 = CollisionTable.GetCollisionOffset(num6, num5);
		fTime0 = GetTime(fOffset0);
		fTime1 = GetTime(fOffset1);
		fSpeed0 = GetSpeed(fOffset0);
		fSpeed1 = GetSpeed(fOffset1);
		if (num3 > num4)
		{
			fSpeed0 = 1f;
			if (fSpeed1 < 1f)
			{
				fSpeed1 = 1f - (1f - fSpeed1) * 0.5f;
			}
		}
		else
		{
			fSpeed1 = 1f;
			if (fSpeed0 < 1f)
			{
				fSpeed0 = 1f - (1f - fSpeed0) * 0.5f;
			}
		}
	}

	public bool IsSimulating()
	{
		return m_body.IsSimulateMode;
	}
}
