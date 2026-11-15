using UnityEngine;

public class MissileState : FState
{
	private float m_Time = 10f;

	public MissileState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToMissile;
		m_LowPriority.Add(EventType.ToNormal);
		m_LowPriority.Add(EventType.ToInvincible);
		m_LowPriority.Add(EventType.ToAccelerate);
		m_LowPriority.Add(EventType.ToScaleSmaller);
		m_LowPriority.Add(EventType.ToScaleFlat);
		m_LowPriority.Add(EventType.ToScaleBigger);
		m_LowPriority.Add(EventType.ToThunderCloud);
		m_LowPriority.Add(EventType.ToQuake);
		m_LowPriority.Add(EventType.ToLeafShelter);
		m_LowPriority.Add(EventType.ToBounce);
		m_LowPriority.Add(EventType.ToBounceOver);
		m_HighPriority.Add(EventType.ToDead);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_Time = 10f;
			Role role = m_PlayerCar.GetRole(0);
			if (role != null)
			{
				role.Enabled = false;
			}
			m_PlayerCar.SetVecGlobal(Vector3.zero);
			m_PlayerCar.EnableKinematic();
			m_PlayerCar.Hide();
			m_PlayerCar.CarState |= 32u;
			if (m_PlayerCar.isHero)
			{
				SceneUIManager.Instance.ShowTutorialUI(show: false);
			}
			CheckAndBreakShift();
		}
	}

	public override void Update()
	{
		m_PlayerCar.DisableAllJetState();
		m_Time -= Time.deltaTime;
		if (m_Time <= 0f)
		{
			FEvent evt = new TransToNormalEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		if (m_PlayerCar.GetRole(0) != null)
		{
			m_PlayerCar.GetRole(0).Enabled = true;
		}
		m_PlayerCar.Show();
		m_PlayerCar.PlaySound(52102, 1);
		m_PlayerCar.DisableKinematic();
		m_PlayerCar.SetPosition(m_PlayerCar.m_RecoverPos);
		m_PlayerCar.SetRotation(m_PlayerCar.m_RecoverQuat);
		m_PlayerCar.SetVecGlobal(m_PlayerCar.m_RecoverVel);
		m_PlayerCar.CarState &= 4294967263u;
		m_StateFlag = StateFlag.STOP;
		PlayerData data = new PlayerData();
		if (RaceSceneData.GetPlayerData((ulong)m_PlayerCar.ID, ref data))
		{
			data.altProp = ePropType.NONE;
			RaceSceneData.SetPlayerData((ulong)m_PlayerCar.ID, data);
		}
		m_PlayerCar.MoveToGround();
	}
}
