using UnityEngine;

public class LeftBoundState : FState
{
	private float m_BoundTime = 2f;

	private Vector3 m_LeftUpVec;

	private Vector3 m_CarTempPos;

	public LeftBoundState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToLeftBound;
		m_LowPriority.Add(EventType.ToRotate);
		m_LowPriority.Add(EventType.ToNormal);
		m_LowPriority.Add(EventType.ToScaleBigger);
		m_HighPriority.Add(EventType.ToDead);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_LeftUpVec = (-m_PlayerCar.transform.right + 0.2f * m_PlayerCar.transform.up).normalized * 10f;
			m_PlayerCar.SetVecGlobal(new Vector3(0f, 0f, 0f));
			m_PlayerCar.DropProp(drop: false);
			m_PlayerCar.EnableKinematic();
			if (m_PlayerCar.CarScaleSmall)
			{
				m_PlayerCar.Play("small_left_flick");
			}
			else if (m_PlayerCar.CarScaleFlat)
			{
				m_PlayerCar.Play("flat_left_flick");
			}
			else
			{
				m_PlayerCar.Play("left_flick");
			}
			if (m_PlayerCar.GetRole(0) != null)
			{
				m_PlayerCar.GetRole(0).PlaySound(Role.SCREAM_LOW);
			}
			m_PlayerCar.PlayAniOnly("land");
			m_PlayerCar.CarState |= 4096u;
			m_BoundTime = 2f;
			SceneUIManager.Instance.RotateCar(m_PlayerCar);
			CheckAndBreakShift();
		}
	}

	public override void Update()
	{
		m_BoundTime -= Time.deltaTime;
		if (m_BoundTime > 0.2f && !CollisionLayer.Instance.RayCastHitToGuardbar(m_PlayerCar.transform.position, m_LeftUpVec, 3f, out var _, out var _))
		{
			m_CarTempPos = m_LeftUpVec * Time.deltaTime;
			if (m_CarTempPos.sqrMagnitude > 6.25f)
			{
				m_CarTempPos = m_CarTempPos.normalized * 2.5f;
			}
			m_CarTempPos += m_PlayerCar.transform.position;
			if (HeightMapManager.Instance.Height(m_CarTempPos.x, m_CarTempPos.z) < HeightMap.m_InvalideHeight)
			{
				m_PlayerCar.m_CarDeadMode = 0;
				FEvent evt = new TransToCarDeadEvt(this);
				m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
				m_StateFlag = StateFlag.OVER;
				return;
			}
			m_CarTempPos.y = 300f;
			if (Physics.Raycast(m_CarTempPos, Vector3.down, out var hitInfo, 1000f, 256))
			{
				m_CarTempPos.y = hitInfo.point.y;
			}
			else
			{
				m_CarTempPos.y = HeightMapManager.Instance.Height(m_CarTempPos.x, m_CarTempPos.z) + 0.1f;
			}
			m_PlayerCar.SetPosition(m_CarTempPos);
		}
		if (m_BoundTime <= 0f)
		{
			FEvent evt2 = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt2, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_PlayerCar.DisableKinematic();
		m_PlayerCar.StopAniOnly();
		m_PlayerCar.CarState &= 4294963199u;
		m_StateFlag = StateFlag.STOP;
	}
}
