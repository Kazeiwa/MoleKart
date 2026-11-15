using UnityEngine;

[MessageClass(typeof(HeroCar))]
internal class HeroCar : Car
{
	private bool m_bLockMovement;

	private InputManager inputMg = InputManager.Instance;

	private Transform m_effect_leaf_2;

	private GameObject m_gameObjectFxLeafFortest;

	private Effect m_eft;

	private Effect m_eftWaterbubbleL;

	private Effect m_eftWaterbubbleR;

	private Effect m_eftWaterflower;

	private GameObject m_goWaterflower;

	private Effect m_eftSandL;

	private Effect m_eftSandR;

	private GameObject m_objFallInWater;

	private Effect m_eftFallInWater;

	private Effect m_eftShift1_BlueDotL;

	private Effect m_eftShift1_BlueDotR;

	private Effect m_eftShift2_BlueFireL;

	private Effect m_eftShift2_BlueFireR;

	private Effect m_eftShift3_GoldDotL;

	private Effect m_eftShift3_GoldDotR;

	private Effect m_eftShift4_GoldFireL;

	private Effect m_eftShift4_GoldFireR;

	private bool bInit;

	public int m_iPerformaceLevel = 1;

	public HeroCar()
	{
		MessageFun<Car>(typeof(HeroCar), processMsg);
	}

	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
		VehicleData vehicleData = TSingleton<CarManager>.Instance.GetVehicleData(m_eCarModeType);
		vehicle = new Vehicle(vehicleData, base.gameObject.transform, lfp, rfp, lbp, rbp, EnmCollisionLayer.CL_PLAYER, this);
		(vehicle as Vehicle).StopWheelDistanceVisual();
		carParticles = new CarParticles(this, eEffectType.JET, eEffectType.JET_BOOST, eEffectType.JETBACK);
		vehicle.m_kUserData = this;
		if (iPhoneSettings.generation < iPhoneGeneration.iPad2Gen)
		{
			m_iPerformaceLevel = 0;
		}
	}

	public override bool Init(int id, GameObject carEffectRoot)
	{
		m_CarType = CarType.HeroCar;
		isHero = true;
		base.Init(id, carEffectRoot);
		vehicle.bHeroVehicle = true;
		GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.LEAF, isPause: true, isOneShot: false);
		m_eft = gameObject.GetComponent<Effect>();
		m_eft.AttachTo(base.gameObject.transform.FindChild("effect"), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_eft.Pause();
		m_effect_leaf_2 = base.transform.FindChild("effect/leaf_2");
		m_gameObjectFxLeafFortest = GameObject.Find("EffectProp/LeafEffect");
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		if (null != rearWheels[0])
		{
			if (m_iPerformaceLevel > 0)
			{
				EffectManager.Instance.CreateEffect(eEffectType.WATER_BUBBLE, isPause: true, isOneShot: false, ref m_eftWaterbubbleL);
				m_eftWaterbubbleL.AttachTo(rearWheels[0].parent, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
				m_eftWaterbubbleL.Pause();
			}
			EffectManager.Instance.CreateEffect(eEffectType.DRIF, isPause: true, isOneShot: false, ref m_eftSandL);
			m_eftSandL.AttachTo(rearWheels[0].parent, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
			m_eftSandL.Pause();
			Transform parentTrans = rearWheels[0].parent.FindChild("shift");
			EffectManager.Instance.CreateEffect(eEffectType.SHIFT_BLUEDOT, isPause: true, isOneShot: false, ref m_eftShift1_BlueDotL);
			m_eftShift1_BlueDotL.AttachTo(parentTrans, zero, zero2);
			m_eftShift1_BlueDotL.Pause();
			EffectManager.Instance.CreateEffect(eEffectType.SHIFT_BLUEFIRE, isPause: true, isOneShot: false, ref m_eftShift2_BlueFireL);
			m_eftShift2_BlueFireL.AttachTo(parentTrans, zero, zero2);
			m_eftShift2_BlueFireL.Pause();
			EffectManager.Instance.CreateEffect(eEffectType.SHIFT_GOLDDOT, isPause: true, isOneShot: false, ref m_eftShift3_GoldDotL);
			m_eftShift3_GoldDotL.AttachTo(parentTrans, zero, zero2);
			m_eftShift3_GoldDotL.Pause();
			EffectManager.Instance.CreateEffect(eEffectType.SHIFT_GOLDFIRE, isPause: true, isOneShot: false, ref m_eftShift4_GoldFireL);
			m_eftShift4_GoldFireL.AttachTo(parentTrans, zero, zero2);
			m_eftShift4_GoldFireL.Pause();
		}
		if (null != rearWheels[1])
		{
			if (m_iPerformaceLevel > 0)
			{
				EffectManager.Instance.CreateEffect(eEffectType.WATER_BUBBLE, isPause: true, isOneShot: false, ref m_eftWaterbubbleR);
				m_eftWaterbubbleR.AttachTo(rearWheels[1].parent, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
				m_eftWaterbubbleR.Pause();
			}
			EffectManager.Instance.CreateEffect(eEffectType.DRIF, isPause: true, isOneShot: false, ref m_eftSandR);
			m_eftSandR.AttachTo(rearWheels[1].parent, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
			m_eftSandR.Pause();
			Transform parentTrans2 = rearWheels[1].parent.FindChild("shift");
			EffectManager.Instance.CreateEffect(eEffectType.SHIFT_BLUEDOT, isPause: true, isOneShot: false, ref m_eftShift1_BlueDotR);
			m_eftShift1_BlueDotR.AttachTo(parentTrans2, zero, zero2);
			m_eftShift1_BlueDotR.Pause();
			EffectManager.Instance.CreateEffect(eEffectType.SHIFT_BLUEFIRE, isPause: true, isOneShot: false, ref m_eftShift2_BlueFireR);
			m_eftShift2_BlueFireR.AttachTo(parentTrans2, zero, zero2);
			m_eftShift2_BlueFireR.Pause();
			EffectManager.Instance.CreateEffect(eEffectType.SHIFT_GOLDDOT, isPause: true, isOneShot: false, ref m_eftShift3_GoldDotR);
			m_eftShift3_GoldDotR.AttachTo(parentTrans2, zero, zero2);
			m_eftShift3_GoldDotR.Pause();
			EffectManager.Instance.CreateEffect(eEffectType.SHIFT_GOLDFIRE, isPause: true, isOneShot: false, ref m_eftShift4_GoldFireR);
			m_eftShift4_GoldFireR.AttachTo(parentTrans2, zero, zero2);
			m_eftShift4_GoldFireR.Pause();
		}
		if (m_iPerformaceLevel > 0)
		{
			m_goWaterflower = EffectManager.Instance.CreateEffect(eEffectType.WATER_FLOWER, isPause: true, isOneShot: false, ref m_eftWaterflower);
			m_eftWaterflower.AttachTo(base.gameObject.transform, new Vector3(0f, 0.2f, 0f), new Vector3(0f, 0f, 0f));
			m_eftWaterflower.Pause();
		}
		m_objFallInWater = EffectManager.Instance.CreateEffect(eEffectType.FALL_IN_WATER, isPause: true, isOneShot: false, ref m_eftFallInWater);
		m_eftFallInWater.AttachTo(base.gameObject.transform, new Vector3(0f, 0.2f, 0f), new Vector3(0f, 0f, 0f));
		m_eftFallInWater.Pause();
		carRenderHelper.Init(base.transform.gameObject);
		bool canShowShadow = true;
		carRenderHelper.SetCanShowShadow(canShowShadow);
		checkReverseDrive = true;
		checkNoMovement = true;
		bInit = true;
		return bInit;
	}

	public override void Uninit()
	{
		base.Uninit();
	}

	[MessageFun(typeof(HeroCar))]
	public new void processMsg(int msg, ref ABMessageData data)
	{
		if (msg != 1006)
		{
			return;
		}
		if (((COM_DATA)data).iValue == 0)
		{
			m_RunMode = (RUNNING_MDOE)((COM_DATA)data).iValue;
			carParticles.EnableJet(bBoost: true);
			carActor.PlayAni("stun01");
			return;
		}
		if (m_RunMode == RUNNING_MDOE.RM_FLYING)
		{
			carParticles.EnableJet(bBoost: false);
		}
		m_RunMode = (RUNNING_MDOE)((COM_DATA)data).iValue;
	}

	public override void UpdateCar()
	{
		if (bInit)
		{
			base.UpdateCar();
			UpdateEffect();
			UpdateCarActor();
			DrawGroundEffect();
			UpdateSound();
			CheckBadCondition();
		}
	}

	public override void UpdatePhysics()
	{
		if (!CanDrive())
		{
			InputManager.Instance.CurrentSteering = 0f;
			Steering(bAccelation: false, bBrake: false, 0f);
			base.UpdatePhysics();
			return;
		}
		bool flag = false;
		bool flag2 = false;
		flag2 = ((inputMg.IsAccelerate() && !inputMg.CheckHandbrake()) ? true : false);
		flag = inputMg.CheckHandbrake();
		if (vehicle.GetBoostMode())
		{
			flag = false;
		}
		if (vehicle != null)
		{
			if (!CheckCanDriveNow())
			{
				flag2 = false;
				flag = false;
			}
			Steering(flag2, flag, inputMg.CurrentSteering);
			base.UpdatePhysics();
		}
	}

	private void UpdateSound()
	{
		float magnitude = GetVecGlobal().magnitude;
		ushort carId = LocalUserInfo.Instance.UserInfo.CarId;
		if (!CanDrive())
		{
			return;
		}
		if (magnitude < base.MaxVelocity / 2f)
		{
			StopHighEngineSound(carId);
			if (!inputMg.IsAccelerate() && !inputMg.IsTurnLeft() && !inputMg.IsTurnRight())
			{
				StopLowEngineSound(carId);
			}
			else if (inputMg.IsAccelerate())
			{
				PlayLowEngineSound(carId, 0.4f);
			}
		}
		else if (magnitude > base.MaxVelocity / 2f)
		{
			StopLowEngineSound(carId);
			PlayHighEngineSound(carId, 0.4f);
		}
	}

	private void UpdateEffect()
	{
		UpdateFXSpeedy();
	}

	public void LockMovement(bool bLock)
	{
		m_bLockMovement = bLock;
		vehicle.LockMovement(m_bLockMovement);
	}

	private void DisableAllShiftEffect()
	{
		m_eftShift1_BlueDotL.Pause();
		m_eftShift1_BlueDotR.Pause();
		m_eftShift2_BlueFireL.Pause();
		m_eftShift2_BlueFireR.Pause();
		m_eftShift3_GoldDotL.Pause();
		m_eftShift3_GoldDotR.Pause();
		m_eftShift4_GoldFireL.Pause();
		m_eftShift4_GoldFireR.Pause();
	}

	private void PlayShiftEffect(int lvl)
	{
		switch (lvl)
		{
		case 1:
			if (m_iPerformaceLevel > 0)
			{
				m_eftShift1_BlueDotL.Resume();
				m_eftShift1_BlueDotR.Resume();
			}
			break;
		case 2:
			m_eftShift2_BlueFireL.Resume();
			m_eftShift2_BlueFireR.Resume();
			break;
		case 3:
			m_eftShift2_BlueFireL.Resume();
			m_eftShift2_BlueFireR.Resume();
			if (m_iPerformaceLevel > 0)
			{
				m_eftShift3_GoldDotL.Resume();
				m_eftShift3_GoldDotR.Resume();
			}
			break;
		case 4:
			m_eftShift4_GoldFireL.Resume();
			m_eftShift4_GoldFireR.Resume();
			break;
		}
	}

	private void DrawGroundEffect()
	{
		if (base.vehicle is Vehicle)
		{
			Vehicle vehicle = base.vehicle as Vehicle;
			int shiftLevel = vehicle.m_kShift.GetShiftLevel();
			DisableAllShiftEffect();
			PlayShiftEffect(shiftLevel);
			if (vehicle.m_kShift.IsShiftMode())
			{
				if (null == m_FXDrift)
				{
					m_FXDrift = base.gameObject.GetComponent<FXDrift>();
				}
				if ((bool)m_FXDrift && m_iPerformaceLevel > 0)
				{
					m_FXDrift.EnableDirft(bEnabled: true);
				}
				if (null == m_FXTrail)
				{
					m_FXTrail = base.gameObject.GetComponent<FXTrail>();
				}
				if (m_FXTrail != null)
				{
					m_FXTrail.EnableTrail(bEnabled: true);
				}
			}
			else
			{
				if (null == m_FXDrift)
				{
					m_FXDrift = base.gameObject.GetComponent<FXDrift>();
				}
				if ((bool)m_FXDrift)
				{
					m_FXDrift.EnableDirft(bEnabled: false);
				}
				if (null == m_FXTrail)
				{
					m_FXTrail = base.gameObject.GetComponent<FXTrail>();
				}
				if (m_FXTrail != null)
				{
					m_FXTrail.EnableTrail(bEnabled: false);
				}
			}
		}
		if (base.GroundedType == ENMTERRAIN_TYPE.ETT_GRASS && GetCarSpeed() > 1f)
		{
			m_eft.active = true;
			m_eft.Resume();
		}
		else
		{
			m_eft.Pause();
		}
		if (m_eftSandL != null && m_eftSandR != null)
		{
			if (base.GroundedType == ENMTERRAIN_TYPE.ETT_SAND && GetCarSpeed() > 1f)
			{
				m_eftSandL.active = true;
				m_eftSandL.Resume();
				m_eftSandR.active = true;
				m_eftSandR.Resume();
			}
			else
			{
				m_eftSandL.Pause();
				m_eftSandR.Pause();
			}
		}
		if (m_iPerformaceLevel <= 0)
		{
			return;
		}
		if ((base.GroundedType == ENMTERRAIN_TYPE.ETT_WATER && GetCarSpeed() > 1f) || base.GroundedType == ENMTERRAIN_TYPE.ETT_DEEPWATER)
		{
			Vector3 currentWaterPoint = GetCurrentWaterPoint();
			m_goWaterflower.transform.position = currentWaterPoint;
			m_goWaterflower.transform.rotation = GetRotation();
			m_eftWaterflower.Resume();
			if (m_eftWaterbubbleL != null)
			{
				m_eftWaterbubbleL.active = true;
				m_eftWaterbubbleL.Resume();
			}
			if (m_eftWaterbubbleR != null)
			{
				m_eftWaterbubbleR.active = true;
				m_eftWaterbubbleR.Resume();
			}
		}
		else
		{
			if (m_eftWaterbubbleL != null)
			{
				m_eftWaterbubbleL.Pause();
			}
			if (m_eftWaterbubbleR != null)
			{
				m_eftWaterbubbleR.Pause();
			}
			m_eftWaterflower.Pause();
		}
	}
}
