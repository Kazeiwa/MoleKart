using RaceTrack;
using UnityEngine;

public class CarDeathState : FState
{
	private float m_DeathTime;

	private bool m_FadedOut;

	private bool m_Recoverd;

	private bool m_Played;

	private Vector3 vPreVelocity;

	private Vector3 vCarDeadPos;

	public CarDeathState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToDead;
		m_LowPriority.Add(EventType.ToInvincible);
		m_LowPriority.Add(EventType.ToLeafShelter);
		m_LowPriority.Add(EventType.ToLeftBound);
		m_LowPriority.Add(EventType.ToNormal);
		m_LowPriority.Add(EventType.ToQuake);
		m_LowPriority.Add(EventType.ToRightBound);
		m_LowPriority.Add(EventType.ToRotate);
		m_LowPriority.Add(EventType.ToScaleBigger);
		m_LowPriority.Add(EventType.ToScaleFlat);
		m_LowPriority.Add(EventType.ToScaleSmaller);
		m_LowPriority.Add(EventType.ToThunderCloud);
		m_LowPriority.Add(EventType.ToUpBound);
		m_LowPriority.Add(EventType.ToBackFlip);
		m_LowPriority.Add(EventType.ToBounceOver);
		m_LowPriority.Add(EventType.ToBounce);
	}

	public override void Entered()
	{
		if (!m_CanRepeat && m_StateFlag == StateFlag.UPDATING)
		{
			return;
		}
		PlayerData data = new PlayerData();
		RaceSceneData.GetPlayerData((ulong)m_PlayerCar.ID, ref data);
		data.selProp = ePropType.NONE;
		data.altProp = ePropType.NONE;
		RaceSceneData.SetPlayerData((ulong)m_PlayerCar.ID, data);
		if (m_PlayerCar.isHero)
		{
			SceneUIManager.Instance.ShowProp(ePropType.NONE);
		}
		if (m_PlayerCar is NPCCar)
		{
			NPCCar nPCCar = m_PlayerCar as NPCCar;
			nPCCar.SteeringBehavior.ResetStickingMonitor();
		}
		m_StateFlag = StateFlag.UPDATING;
		m_PlayerCar.CarState |= 32768u;
		m_DeathTime = 0f;
		m_PlayerCar.DropProp(drop: false);
		m_FadedOut = false;
		m_Recoverd = false;
		m_Played = false;
		m_PlayerCar.PlayAniOnly("land");
		m_PlayerCar.GetRole(0).PlaySound(Role.SCREAM_HIGHT);
		if (m_PlayerCar.m_CarDeadMode == 0)
		{
			vPreVelocity = m_PlayerCar.GetVecGlobal();
			if (vPreVelocity.sqrMagnitude > 100f)
			{
				vPreVelocity = vPreVelocity.normalized * 10f;
			}
			m_PlayerCar.EnableKinematic();
		}
		CameraManager.instance.DeathReset();
		CheckAndBreakShift();
		m_PlayerCar.StopBoost();
		m_PlayerCar.vehicle.ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
		if (m_PlayerCar.isHero)
		{
			InputManager.Instance.m_Accelerate = false;
		}
		vCarDeadPos = m_PlayerCar.transform.position;
	}

	public override void Update()
	{
		m_DeathTime += Time.deltaTime;
		if (m_PlayerCar.isHero && !m_FadedOut && m_DeathTime >= 0.2f)
		{
			m_FadedOut = true;
			CameraManager.instance.FadeInAndOut();
		}
		if (m_DeathTime < 1f && m_PlayerCar.m_CarDeadMode == 0)
		{
			Vector3 position = m_PlayerCar.GetPosition();
			vPreVelocity += Vector3.down * Time.deltaTime * 10f;
			position += vPreVelocity * Time.deltaTime;
			m_PlayerCar.SetPosition(position);
		}
		if (!m_Recoverd && m_DeathTime >= 1f)
		{
			if (m_PlayerCar.isHero)
			{
				CameraManager.instance.ForceCameraNormal();
			}
			m_Recoverd = true;
			if (RecoverPointManager.Instance != null)
			{
				m_PlayerCar.transform.position = RecoverPointManager.Instance.GetRecoverPoint(vCarDeadPos);
			}
			Vector2 pos = new Vector2(m_PlayerCar.transform.position.x, m_PlayerCar.transform.position.z);
			if (RaceWorld.Instance.RacingLine != null)
			{
				RaceNode raceNode = RaceWorld.Instance.RacingLine.QueryClosestNode(pos);
				if (raceNode != null)
				{
					int index = raceNode.Index;
					Vector2 pos2 = RaceWorld.Instance.RacingLine.Node(index).Pos;
					Vector3 vector = new Vector3(pos2.x, HeightMapManager.Instance.Height(pos2.x, pos2.y), pos2.y);
					int to = RaceWorld.Instance.RacingLine.QueryEdgesByFromNode(index)[0].To;
					Vector2 pos3 = RaceWorld.Instance.RacingLine.Node(to).Pos;
					Vector3 vector2 = new Vector3(pos3.x, HeightMapManager.Instance.Height(pos3.x, pos3.y), pos3.y);
					Vector3 up = HeightMapManager.Instance.Normal(pos2.x, 0f, pos2.y);
					m_PlayerCar.transform.up = up;
					m_PlayerCar.transform.forward = (vector2 - vector).normalized;
					m_PlayerCar.SetVecGlobal(Vector3.zero);
				}
			}
			m_PlayerCar.StopAniOnly();
		}
		if (!m_Played && m_DeathTime >= 1.2f)
		{
			m_Played = true;
			m_PlayerCar.Play("stickdown");
		}
		if (m_DeathTime >= 2f)
		{
			m_PlayerCar.DisableKinematic();
		}
		if (m_DeathTime >= 3f)
		{
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_PlayerCar.CarState &= 4294934527u;
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.SetVecGlobal(Vector3.zero);
		m_PlayerCar.m_CarDeadMode = -1;
		if (m_PlayerCar.isHero)
		{
			InputManager.Instance.m_Accelerate = true;
		}
	}
}
