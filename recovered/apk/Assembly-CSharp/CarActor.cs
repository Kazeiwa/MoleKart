using System;
using RaceTrack;
using UnityEngine;

public class CarActor
{
	private Car m_PlayerCar;

	private PlayerData m_PlayerData;

	private FStateMachine machine;

	private PlayerState m_PlayerState;

	private Role m_Role;

	private string m_OtherClipName;

	private float m_AniTime;

	private bool m_bLock;

	public PlayerAIProfile AIProfile { get; private set; }

	public CarActor(Car car)
	{
		m_PlayerCar = car;
		InitPlayerData(car);
		AIProfile = null;
		InitStateMachine();
	}

	public void AttachAIProfile(PlayerAIProfile profile)
	{
		AIProfile = profile;
	}

	private void InitStateMachine()
	{
		machine = new FStateMachine();
		NormalState normalState = new NormalState(this);
		machine.AddState(EventType.ToNormal, normalState);
		RotateState state = new RotateState(this);
		machine.AddState(EventType.ToRotate, state);
		UpBoundState state2 = new UpBoundState(this);
		machine.AddState(EventType.ToUpBound, state2);
		LeftBoundState state3 = new LeftBoundState(this);
		machine.AddState(EventType.ToLeftBound, state3);
		RightBoundState state4 = new RightBoundState(this);
		machine.AddState(EventType.ToRightBound, state4);
		MissileState state5 = new MissileState(this);
		machine.AddState(EventType.ToMissile, state5);
		AccelerateState state6 = new AccelerateState(this);
		machine.AddState(EventType.ToAccelerate, state6);
		InvincibleState state7 = new InvincibleState(this);
		machine.AddState(EventType.ToInvincible, state7);
		SclaeSmallState state8 = new SclaeSmallState(this);
		machine.AddState(EventType.ToScaleSmaller, state8);
		ScaleBigState state9 = new ScaleBigState(this);
		machine.AddState(EventType.ToScaleBigger, state9);
		QuakeCarState state10 = new QuakeCarState(this);
		machine.AddState(EventType.ToQuake, state10);
		ThunderCloudState state11 = new ThunderCloudState(this);
		machine.AddState(EventType.ToThunderCloud, state11);
		ScaleFlatState state12 = new ScaleFlatState(this);
		machine.AddState(EventType.ToScaleFlat, state12);
		BigBoundState state13 = new BigBoundState(this);
		machine.AddState(EventType.ToBigBound, state13);
		LeafShelterState state14 = new LeafShelterState(this);
		machine.AddState(EventType.ToLeafShelter, state14);
		CarDeathState state15 = new CarDeathState(this);
		machine.AddState(EventType.ToDead, state15);
		BackFlipState state16 = new BackFlipState(this);
		machine.AddState(EventType.ToBackFlip, state16);
		BounceState state17 = new BounceState(this);
		machine.AddState(EventType.ToBounce, state17);
		BounceOverState state18 = new BounceOverState(this);
		machine.AddState(EventType.ToBounceOver, state18);
		machine.SetInitialState(normalState);
		machine.Start();
	}

	private void InitPlayerData(Car car)
	{
		m_PlayerData = new PlayerData();
		m_PlayerData.selProp = ePropType.NONE;
		m_PlayerData.altProp = ePropType.NONE;
		m_PlayerData.CurLaps = 0;
		m_PlayerData.IsFinish = false;
		m_PlayerData.NormalFinish = false;
		m_PlayerData.PreBlockNum = 0;
		m_PlayerData.Ranking = 0;
		m_PlayerData.ShowLaps = 0;
		m_PlayerData.SpendTime = 0f;
		m_PlayerData.IsMaster = car.isHero;
		RaceSceneData.SetPlayerData(Convert.ToUInt64(car.ID), m_PlayerData);
	}

	public void Uninit()
	{
		machine.Stop();
		RaceSceneData.RemovePlayerData(Convert.ToUInt64(m_PlayerCar.ID));
	}

	public void Update()
	{
		machine.Update();
		PlayRoleAnim();
	}

	public void UpdatePhysics()
	{
		machine.UpdatePhysics();
	}

	public Car GetPlayerCar()
	{
		return m_PlayerCar;
	}

	public bool CanDrive()
	{
		if (m_bLock)
		{
			return false;
		}
		return machine.CanDrive();
	}

	public void LockCarActor()
	{
		m_bLock = true;
	}

	public void UnLockCarActor()
	{
		m_bLock = false;
	}

	public PlayerState GetPlayerState()
	{
		return m_PlayerState;
	}

	public void SetPlayerState(PlayerState state)
	{
		m_PlayerState = state;
	}

	public void ResetPlayerState()
	{
		m_PlayerState = PlayerState.STATE_IDEL;
	}

	public void PushRole(Role role)
	{
		m_Role = role;
	}

	public void PopRole(Role role)
	{
		m_Role = null;
	}

	public Role GetRole(int idx)
	{
		return m_Role;
	}

	public void PickProp(ePropType type)
	{
		RaceSceneData.GetPlayerData(Convert.ToUInt64(m_PlayerCar.ID), ref m_PlayerData);
		if (m_PlayerData.selProp == ePropType.NONE)
		{
			m_PlayerData.selProp = type;
			RaceSceneData.SetPlayerData(Convert.ToUInt64(m_PlayerCar.ID), m_PlayerData);
		}
	}

	public void HitByBanana()
	{
		if (!Defected())
		{
			m_PlayerCar.PlayHitByBananaEffect();
			FEvent evt = new TransToRotateEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void HitByTortoise()
	{
		if (!Defected())
		{
			FEvent evt = new TransToUpBoundEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void HitByMissile(bool right)
	{
		if (!m_PlayerCar.CarIncicible && !m_PlayerCar.MissleState && !m_PlayerCar.IsDead)
		{
			m_PlayerCar.PlayHitByMissile();
			m_PlayerCar.PlaySound(40002, 1);
			FEvent fEvent = null;
			fEvent = ((!right) ? ((FEvent)new TransToLeftBoundEvt(null)) : ((FEvent)new TransToRightBoundEvt(null)));
			machine.PostEvent(fEvent, FStateMachine.EventPriority.HighPriority);
			DropProp(drop: false);
		}
	}

	public void BecomeMissile()
	{
		FEvent evt = new TransToMissileEvt(null);
		machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
	}

	public void RecoverCarFromMissile()
	{
		PlayerData data = new PlayerData();
		RaceSceneData.GetPlayerData(Convert.ToUInt64(m_PlayerCar.ID), ref data);
		data.altProp = ePropType.NONE;
		RaceSceneData.SetPlayerData(Convert.ToUInt64(m_PlayerCar.ID), data);
		if (m_PlayerCar.isHero)
		{
			SceneUIManager.Instance.ShowProp(data.altProp);
		}
		FEvent evt = new TransToNormalEvt(null);
		machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
	}

	public void AccelerateCar()
	{
		FEvent evt = new TransToAcclerateEvt(null);
		machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
	}

	public void InvincibleCar()
	{
		FEvent evt = new TransToInvincibleEvt(null);
		machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
	}

	public void HitByBomb()
	{
		if (!Defected())
		{
			FEvent evt = new TransToBigBoundEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void HitByLightning()
	{
		if (!Defected())
		{
			m_PlayerCar.PlaySound(52301, 1);
			FEvent evt = new TransToScaleSmallerEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void HitByCuttleFish()
	{
		if (!m_PlayerCar.CarIncicible && !m_PlayerCar.MissleState && !m_PlayerCar.IsDead && m_PlayerCar.isHero)
		{
			m_PlayerCar.PlayHitByLeafEffect();
			FEvent evt = new TransToLeafShelter(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void HitByCheatBox()
	{
		if (!Defected())
		{
			FEvent evt = new TransToUpBoundEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void HitByLuckyStar(bool right)
	{
		if (!m_PlayerCar.CarIncicible && !m_PlayerCar.MissleState && !m_PlayerCar.IsDead)
		{
			m_PlayerCar.PlaySound(40002, 1);
			m_PlayerCar.PlayHitInvincibleCar();
			FEvent fEvent = null;
			fEvent = ((!right) ? ((FEvent)new TransToLeftBoundEvt(null)) : ((FEvent)new TransToRightBoundEvt(null)));
			machine.PostEvent(fEvent, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void UseBigMushroom()
	{
		FEvent evt = new TransToScaleBigEvt(null);
		machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
	}

	public void QuakeCar()
	{
		if (!Defected())
		{
			FEvent evt = new TransToQuakeEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void UseThunderCloud()
	{
		if (!Defected())
		{
			FEvent evt = new TransToThunderCloudEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void DisappearThunderCloud()
	{
		PlayerData data = new PlayerData();
		RaceSceneData.GetPlayerData(Convert.ToUInt64(m_PlayerCar.ID), ref data);
		data.altProp = ePropType.NONE;
		RaceSceneData.SetPlayerData(Convert.ToUInt64(m_PlayerCar.ID), data);
		if (m_PlayerCar.isHero)
		{
			SceneUIManager.Instance.ShowProp(data.altProp);
		}
		FEvent evt = new TransToNormalEvt(null);
		machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
	}

	public void PressByBigCar()
	{
		if (!m_PlayerCar.MissleState && !m_PlayerCar.CarIncicible && !m_PlayerCar.IsDead)
		{
			FEvent evt = new TransToScaleFlatEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void HitByRobb()
	{
		if (!Defected())
		{
			FEvent evt = new TransToBigBoundEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void BackFlip()
	{
		if (!Defected())
		{
			FEvent evt = new TransToBackFlipEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	public void CarDead(int deadMode)
	{
		if (!m_PlayerCar.MissleState)
		{
			m_PlayerCar.m_CarDeadMode = deadMode;
			FEvent evt = new TransToCarDeadEvt(null);
			machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
		}
	}

	private bool Defected()
	{
		if (m_PlayerCar.CarIncicible || m_PlayerCar.MissleState || m_PlayerCar.CarScaleBig || m_PlayerCar.CarDeath)
		{
			return true;
		}
		return false;
	}

	public void BounceCar()
	{
		FEvent evt = new TransToBounceEvt(null);
		machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
	}

	public void DropProp(bool drop)
	{
		TSingleton<PropManager>.Instance.LoseBanana(m_PlayerCar, m_PlayerCar.gameObject.name);
		TSingleton<PropManager>.Instance.LoseTortoise(m_PlayerCar, m_PlayerCar.gameObject.name);
		PlayerData data = new PlayerData();
		RaceSceneData.GetPlayerData(Convert.ToUInt64(m_PlayerCar.ID), ref data);
		ePropType selProp = data.selProp;
		data.selProp = ePropType.NONE;
		if (!m_PlayerCar.CarThunderCloud)
		{
			data.altProp = ePropType.NONE;
		}
		RaceSceneData.SetPlayerData(Convert.ToUInt64(m_PlayerCar.ID), data);
		if (m_PlayerCar.isHero)
		{
			SceneUIManager.Instance.ShowProp(data.altProp);
		}
		if (drop)
		{
			SkillManager.Instance.DropProp(m_PlayerCar, selProp);
		}
	}

	public void PlayOther(string name)
	{
		if (m_Role != null)
		{
			m_Role.PlayOther(name);
		}
	}

	public void PlayAni(string name)
	{
		if (GetPlayerState() != PlayerState.STATE_ONLYPLAY && m_Role != null)
		{
			m_Role.PlayAni(name, flag: true);
		}
	}

	public void PlayAniOnly(string name)
	{
		SetPlayerState(PlayerState.STATE_ONLYPLAY);
		if (m_Role != null)
		{
			m_Role.PlayAni(name, flag: true);
		}
	}

	public void StopAniOnly()
	{
		SetPlayerState(PlayerState.STATE_IDEL);
		if (m_Role != null)
		{
			m_Role.StopAni();
			m_Role.PlayNext(RoleAnim3D.GetClipName(RoleAnim3D.RoleAnimClipState.stan));
		}
	}

	public void PlayRoleAnim()
	{
		if (!m_PlayerCar.isHero)
		{
			return;
		}
		PlayerState playerState = GetPlayerState();
		string text = null;
		switch (playerState)
		{
		case PlayerState.STATE_IDEL:
			text = RoleAnim3D.GetClipName(RoleAnim3D.RoleAnimClipState.stan);
			break;
		case PlayerState.STATE_LEFT_LEAN:
			text = RoleAnim3D.GetClipName(RoleAnim3D.RoleAnimClipState.turl01);
			break;
		case PlayerState.STATE_RIGHT_LEAN:
			text = RoleAnim3D.GetClipName(RoleAnim3D.RoleAnimClipState.turr01);
			break;
		case PlayerState.STATE_TURN_DOWN:
			text = RoleAnim3D.GetClipName(RoleAnim3D.RoleAnimClipState.reve01);
			break;
		case PlayerState.STATE_ACC:
			text = RoleAnim3D.GetClipName(RoleAnim3D.RoleAnimClipState.lean01);
			break;
		}
		if (m_Role != null && text != null && m_OtherClipName == null)
		{
			m_Role.Play(text);
		}
		if (playerState == PlayerState.STATE_ONLYPLAY)
		{
			if (m_Role != null)
			{
				m_Role.PlayOnly(m_Role.m_CurrentName);
			}
			return;
		}
		if (m_Role != null && m_Role.m_CurrentName != null && m_Role.m_CurrentFlag && m_Role.animation != null)
		{
			m_OtherClipName = m_Role.m_CurrentName;
			m_Role.PlayOther(m_OtherClipName);
			if ((bool)m_Role.animation.GetClip(m_OtherClipName))
			{
				m_AniTime = m_Role.animation.GetClip(m_OtherClipName).length;
				m_Role.m_CurrentFlag = false;
			}
		}
		if (m_Role != null && m_OtherClipName != null)
		{
			m_AniTime -= Time.deltaTime;
			if (m_Role != null && m_AniTime <= 0f)
			{
				m_Role.SetPreAniName(m_OtherClipName);
				m_OtherClipName = null;
				m_Role.m_CurrentName = null;
			}
		}
	}
}
