using UnityEngine;

public class BounceOverState : FState
{
	private bool m_Kenimatic;

	private float m_DownSpeed;

	private float m_DownTime;

	private float m_InitHeight;

	private float m_InitSpeed = 2f;

	private float DELTA_TIME = 0.02f;

	private float m_HitHeight;

	private bool m_BounceOVer;

	public BounceOverState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToBounceOver;
		m_LowPriority.Add(EventType.ToAccelerate);
		m_HighPriority.Add(EventType.ToBounce);
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToMissile);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_HitHeight = HeightMap.m_InvalideHeight;
			m_BounceOVer = false;
			m_StateFlag = StateFlag.UPDATING;
			m_PlayerCar.CarState |= 262144u;
			m_PlayerCar.EnableKinematic();
			m_Kenimatic = true;
			m_DownSpeed = 0f;
			m_DownTime = 0f;
			m_InitHeight = m_PlayerCar.transform.position.y;
			m_PlayerCar.StopBoost();
			m_PlayerCar.LockSpeed(lockspeed: true);
			CheckAndBreakShift();
		}
	}

	public override void Update()
	{
		if (m_BounceOVer)
		{
			return;
		}
		if (m_PlayerCar.transform.position.y < HeightMap.m_InvalideHeight)
		{
			m_PlayerCar.m_CarDeadMode = 0;
			FEvent evt = new TransToCarDeadEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
			return;
		}
		if (m_PlayerCar.transform.position.y < HeightMapManager.Instance.Height(m_PlayerCar.transform.position.x, m_PlayerCar.transform.position.z) + 0.5f)
		{
			FEvent evt2 = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt2, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
			return;
		}
		float num = 0f;
		while (num < Time.deltaTime)
		{
			Vector3 vPos = default(Vector3);
			Vector3 vNormal = default(Vector3);
			Vector3 vWorldPos = m_PlayerCar.transform.position + new Vector3(0f, 2f, 0f);
			if (m_Kenimatic && CollisionLayer.Instance.RayCastHitToMushroom(vWorldPos, Vector3.down, 4f, out vPos, out vNormal))
			{
				if (vPos.y >= m_PlayerCar.transform.position.y)
				{
					m_PlayerCar.m_CarDeadMode = 0;
					FEvent evt3 = new TransToCarDeadEvt(this);
					m_Machine.PostEvent(evt3, FStateMachine.EventPriority.HighPriority);
					m_StateFlag = StateFlag.OVER;
					break;
				}
				m_PlayerCar.DisableKinematic();
				m_Kenimatic = false;
				m_HitHeight = vPos.y;
			}
			float num2 = ((!(DELTA_TIME < Time.deltaTime - num)) ? (Time.deltaTime - num) : DELTA_TIME);
			num += num2;
			m_DownTime += num2;
			m_DownSpeed += 9.8f * num2;
			Vector3 position = m_PlayerCar.transform.position;
			if (m_Kenimatic)
			{
				Vector3 vector = m_PlayerCar.transform.forward * 7f;
				vector.y = 0f;
				position += vector * num2;
				position.y = m_InitHeight - (4.9f * m_DownTime + m_InitSpeed) * m_DownTime;
				m_PlayerCar.transform.position = position;
				continue;
			}
			Vector3 vector2 = m_PlayerCar.transform.forward * 7f;
			vector2.y = 0f;
			position += vector2 * num2;
			if (m_HitHeight < m_InitHeight - (4.9f * m_DownTime + m_InitSpeed) * m_DownTime - 0.15f)
			{
				position.y = m_InitHeight - (4.9f * m_DownTime + m_InitSpeed) * m_DownTime;
			}
			else
			{
				position.y = m_HitHeight + 0.3f;
				m_BounceOVer = true;
			}
			m_PlayerCar.transform.position = position;
		}
	}

	public override void Exited()
	{
		m_PlayerCar.DisableKinematic();
		m_PlayerCar.CarState &= 4294705151u;
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.SetVecGlobal(m_PlayerCar.transform.forward * m_PlayerCar.MaxSpeed);
		m_PlayerCar.LockSpeed(lockspeed: false);
	}
}
