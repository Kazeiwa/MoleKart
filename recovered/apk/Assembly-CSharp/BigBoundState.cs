using UnityEngine;

public class BigBoundState : FState
{
	private float m_BoundTime = 2f;

	private Transform m_CarColliderRoot;

	private Vector3 m_vLocalPos;

	private Vector3 m_MoveDirection = Vector3.zero;

	private Vector3 m_TempPos = Vector3.zero;

	public BigBoundState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToBigBound;
		m_LowPriority.Add(EventType.ToRotate);
		m_LowPriority.Add(EventType.ToNormal);
		m_HighPriority.Add(EventType.ToDead);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_PlayerCar.LimitSpeed(m_PlayerCar.FastSpeed);
			m_PlayerCar.CarState |= 512u;
			m_PlayerCar.DropProp(drop: true);
			m_PlayerCar.EnableKinematic();
			if (m_PlayerCar.CarScaleSmall)
			{
				m_PlayerCar.Play("small_flip02");
			}
			else if (m_PlayerCar.CarScaleFlat)
			{
				m_PlayerCar.Play("flat_flip02");
			}
			else
			{
				m_PlayerCar.Play("flip02");
			}
			if (m_PlayerCar.GetRole(0) != null)
			{
				m_PlayerCar.GetRole(0).PlaySound(Role.SCREAM_LOW);
			}
			m_PlayerCar.PlayAniOnly("land");
			m_BoundTime = 1.7f;
			SceneUIManager.Instance.RotateCar(m_PlayerCar);
			m_CarColliderRoot = m_PlayerCar.transform.FindChild("collision");
			m_CarColliderRoot.gameObject.SetActiveRecursively(state: false);
			CheckAndBreakShift();
			if (m_PlayerCar.m_ForwardHitByRobb != Vector2.zero)
			{
				m_MoveDirection = new Vector3(m_PlayerCar.m_ForwardHitByRobb.x, 0f, m_PlayerCar.m_ForwardHitByRobb.y);
			}
			else
			{
				m_MoveDirection = Vector3.zero;
			}
		}
	}

	public override void Update()
	{
		if (m_MoveDirection != Vector3.zero && !CollisionLayer.Instance.RayCastHitToGuardbar(m_PlayerCar.transform.position, m_MoveDirection, 3f, out var _, out var _) && !m_PlayerCar.CarMushroomBounce && !m_PlayerCar.CarBounceOver)
		{
			m_TempPos = m_MoveDirection * Time.deltaTime * 6f;
			if (m_TempPos.sqrMagnitude > 6.25f)
			{
				m_TempPos = m_TempPos.normalized * 2.5f;
			}
			m_TempPos = m_PlayerCar.transform.position + m_TempPos;
			if (HeightMapManager.Instance.Height(m_TempPos.x, m_TempPos.z) < HeightMap.m_InvalideHeight)
			{
				m_PlayerCar.m_CarDeadMode = 1;
				FEvent evt = new TransToCarDeadEvt(this);
				m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
				m_StateFlag = StateFlag.OVER;
				return;
			}
			m_TempPos.y = 300f;
			if (Physics.Raycast(m_TempPos, Vector3.down, out var hitInfo, 1000f, 256))
			{
				m_TempPos.y = hitInfo.point.y;
			}
			else
			{
				m_TempPos.y = HeightMapManager.Instance.Height(m_TempPos.x, m_TempPos.z);
			}
			m_PlayerCar.transform.position = m_TempPos;
			m_PlayerCar.MoveToGround();
		}
		m_BoundTime -= Time.deltaTime;
		if (m_BoundTime <= 0f)
		{
			FEvent evt2 = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt2, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_PlayerCar.LimitSpeed(m_PlayerCar.MaxSpeed);
		m_PlayerCar.m_ForwardHitByRobb = Vector2.zero;
		m_PlayerCar.StopAniOnly();
		m_CarColliderRoot.gameObject.SetActiveRecursively(state: true);
		m_PlayerCar.DisableKinematic();
		m_PlayerCar.MoveToGround();
		m_PlayerCar.CarState &= 4294966783u;
		m_StateFlag = StateFlag.STOP;
	}
}
