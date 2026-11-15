using UnityEngine;

public class BounceState : FState
{
	private Vector3 m_ThrowDir = Vector3.one;

	private float DELTA_TIME = 0.02f;

	private float m_Delta = 0.5f;

	public BounceState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToBounce;
		m_LowPriority.Add(EventType.ToAccelerate);
		m_LowPriority.Add(EventType.ToBounceOver);
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToMissile);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_PlayerCar.CarState |= 131072u;
			m_PlayerCar.EnableKinematic();
			m_PlayerCar.SetVecGlobal(Vector3.zero);
			m_PlayerCar.PlayAni("stun01");
			m_ThrowDir = m_PlayerCar.m_HorizonSpeed * m_PlayerCar.transform.forward;
			m_ThrowDir.y = m_PlayerCar.m_VeticalSpeed;
			m_Delta = 0.6f;
			m_PlayerCar.EnableJet(enable: true);
			m_PlayerCar.LockSpeed(lockspeed: true);
			CheckAndBreakShift();
		}
	}

	public override void Update()
	{
		if (m_PlayerCar.transform.position.y < HeightMap.m_InvalideHeight)
		{
			m_PlayerCar.m_CarDeadMode = 0;
			FEvent evt = new TransToCarDeadEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
			return;
		}
		if (CollisionLayer.Instance.RayCastHitToGuardbar(m_PlayerCar.transform.position, m_PlayerCar.transform.forward, 2f, out var _, out var vNormal))
		{
			m_ThrowDir = vNormal * m_ThrowDir.magnitude * 0.5f;
		}
		float num = 0f;
		while (num < Time.deltaTime)
		{
			if (m_ThrowDir.y > 0f && m_Delta > 0f)
			{
				float num2 = 0f;
				num2 = ((m_PlayerCar.m_CarType == Car.CarType.HeroCar) ? InputManager.Instance.CurrentSteering : ((m_PlayerCar.m_CarType != Car.CarType.NPCCAR) ? 0f : (m_PlayerCar as NPCCar).Steer.Rate));
				float angle = m_Delta * num2;
				m_Delta -= 0.002f;
				m_ThrowDir = Quaternion.AngleAxis(angle, Vector3.up) * m_ThrowDir;
				Vector3 vector = Quaternion.AngleAxis(angle, Vector3.up) * m_PlayerCar.transform.forward;
				vector.y = 0f;
				m_PlayerCar.transform.forward = vector.normalized;
			}
			if (m_ThrowDir.y < 0f)
			{
				Vector3 vPos2 = default(Vector3);
				Vector3 vNormal2 = default(Vector3);
				Vector3 vWorldPos = m_PlayerCar.transform.position + new Vector3(0f, 2f, 0f);
				float num3 = HeightMapManager.Instance.Height(m_PlayerCar.transform.position.x, m_PlayerCar.transform.position.z) + 0.5f;
				if (CollisionLayer.Instance.RayCastHitToMushroom(vWorldPos, Vector3.down, 3.3f, out vPos2, out vNormal2))
				{
					if (vPos2.y >= m_PlayerCar.transform.position.y)
					{
						m_PlayerCar.m_CarDeadMode = 0;
						FEvent evt2 = new TransToCarDeadEvt(this);
						m_Machine.PostEvent(evt2, FStateMachine.EventPriority.HighPriority);
						m_StateFlag = StateFlag.OVER;
					}
					else
					{
						FEvent evt3 = new TransToBounceOverEvt(this);
						m_Machine.PostEvent(evt3, FStateMachine.EventPriority.HighPriority);
						m_StateFlag = StateFlag.OVER;
					}
					break;
				}
				if (m_PlayerCar.transform.position.y < num3)
				{
					FEvent evt4 = new TransToBounceOverEvt(this);
					m_Machine.PostEvent(evt4, FStateMachine.EventPriority.HighPriority);
					m_StateFlag = StateFlag.OVER;
					break;
				}
			}
			float num4 = ((!(DELTA_TIME < Time.deltaTime - num)) ? (Time.deltaTime - num) : DELTA_TIME);
			num += num4;
			m_ThrowDir.x -= m_ThrowDir.x * num4 * m_PlayerCar.m_HorizonDampingCoefficient;
			m_ThrowDir.z -= m_ThrowDir.z * num4 * m_PlayerCar.m_HorizonDampingCoefficient;
			if (m_ThrowDir.y > 0f)
			{
				m_ThrowDir.y += -9.8f * num4 * m_PlayerCar.m_VeticalDampingCoefficient;
			}
			else
			{
				m_ThrowDir.y += -9.8f * num4 * m_PlayerCar.m_VeticalDampingCoefficient_Down;
			}
			m_PlayerCar.transform.position += m_ThrowDir * num4;
			m_PlayerCar.SetVecGlobal(m_ThrowDir);
		}
	}

	public override void Exited()
	{
		m_PlayerCar.DisableKinematic();
		m_PlayerCar.CarState &= 4294836223u;
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.LockSpeed(lockspeed: false);
		m_PlayerCar.EnableJet(enable: false);
	}
}
