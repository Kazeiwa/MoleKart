using UnityEngine;

public class UpBoundState : FState
{
	private float m_BoundTime = 1.5f;

	private Transform m_CarColliderRoot;

	private Vector3 m_vLocalPos;

	private float m_CarSpeed;

	private Vector3 m_CarForward;

	private Vector3 m_CarTempPos;

	public UpBoundState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToUpBound;
		m_LowPriority.Add(EventType.ToRotate);
		m_LowPriority.Add(EventType.ToNormal);
		m_HighPriority.Add(EventType.ToDead);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_CarSpeed = m_PlayerCar.GetVelocity();
			m_CarForward = m_PlayerCar.transform.forward;
			m_PlayerCar.PlaySound(40002, 1);
			m_PlayerCar.DropProp(drop: false);
			m_PlayerCar.StopBoost();
			m_PlayerCar.EnableKinematic();
			if (m_PlayerCar.CarScaleSmall)
			{
				m_PlayerCar.Play("small_flip01");
			}
			else if (m_PlayerCar.CarScaleFlat)
			{
				m_PlayerCar.Play("flat_flip01");
			}
			else
			{
				m_PlayerCar.Play("flip01");
			}
			if (m_PlayerCar.GetRole(0) != null)
			{
				m_PlayerCar.GetRole(0).PlaySound(Role.SCREAM_LOW);
			}
			m_PlayerCar.PlayAniOnly("land");
			m_CarColliderRoot = m_PlayerCar.transform.FindChild("collision");
			m_vLocalPos = m_CarColliderRoot.localPosition;
			Vector3 position = m_PlayerCar.GetPosition() + new Vector3(0f, 10f, 0f);
			position = m_CarColliderRoot.InverseTransformPoint(position);
			if (!m_PlayerCar.CarMushroomBounce)
			{
				m_CarColliderRoot.localPosition = position;
			}
			m_PlayerCar.CarState |= 16u;
			m_BoundTime = 1.2f;
			SceneUIManager.Instance.RotateCar(m_PlayerCar);
			CheckAndBreakShift();
		}
	}

	public override void Update()
	{
		if (!m_PlayerCar.CarMushroomBounce && !CollisionLayer.Instance.RayCastHitToGuardbar(m_PlayerCar.transform.position, m_CarForward, 4f, out var vPos, out var vNormal) && !CollisionLayer.Instance.RayCastHitToGuardbar(m_PlayerCar.transform.position, m_PlayerCar.transform.right, 3f, out vPos, out vNormal) && !CollisionLayer.Instance.RayCastHitToGuardbar(m_PlayerCar.transform.position, -m_PlayerCar.transform.right, 3f, out vPos, out vNormal) && !m_PlayerCar.CarMushroomBounce && !m_PlayerCar.CarBounceOver)
		{
			if (HeightMapManager.Instance.Height(m_PlayerCar.transform.position.x, m_PlayerCar.transform.position.z) < HeightMap.m_InvalideHeight)
			{
				m_PlayerCar.m_CarDeadMode = 0;
				FEvent evt = new TransToCarDeadEvt(this);
				m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
				m_StateFlag = StateFlag.OVER;
				return;
			}
			m_CarTempPos = m_CarForward * m_CarSpeed * Time.deltaTime;
			if (m_CarTempPos.sqrMagnitude > 6.25f)
			{
				m_CarTempPos = m_CarTempPos.normalized * 2.5f;
			}
			m_CarTempPos = m_PlayerCar.transform.position + m_CarTempPos;
			m_CarTempPos.y = 300f;
			if (Physics.Raycast(m_CarTempPos, Vector3.down, out var hitInfo, 1000f, 256))
			{
				m_CarTempPos.y = hitInfo.point.y;
			}
			else
			{
				m_CarTempPos.y = HeightMapManager.Instance.Height(m_CarTempPos.x, m_CarTempPos.z) + 0.1f;
			}
			m_PlayerCar.transform.position = m_CarTempPos;
		}
		m_BoundTime -= Time.deltaTime;
		m_CarSpeed *= 0.9f;
		if (m_BoundTime <= 0f)
		{
			FEvent evt2 = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt2, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_PlayerCar.StopAniOnly();
		m_CarColliderRoot.localPosition = m_vLocalPos;
		m_PlayerCar.DisableKinematic();
		m_PlayerCar.CarState &= 4294967279u;
		m_StateFlag = StateFlag.STOP;
	}
}
