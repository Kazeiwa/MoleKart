using System;
using Misc;
using RaceTrack;
using RaceTrack.SteeringBehavior;
using UnityEngine;

[MessageClass(typeof(NPCCar))]
public class NPCCar : Car, INpcCar
{
	public static int steerFlag;

	public int m_steerFlag = -1;

	protected AdvancedSteeringBehavior steeringBehavior = new AdvancedSteeringBehavior();

	private SteeringArgument steering = SteeringArgument.Default;

	private PlayerData playerData = new PlayerData();

	private CrudeElapsedTimer equipeThrowingTimer = new CrudeElapsedTimer(2f);

	private CrudeElapsedTimer propThrowingTimer = new CrudeElapsedTimer(3f);

	private StuckDetector stuckDetector = new StuckDetector();

	private bool dontUseMissle = true;

	public new float MaxTurnScale
	{
		get
		{
			return vehicle.MaxTurnScale;
		}
		set
		{
			vehicle.MaxTurnScale = value;
		}
	}

	public new float MotorForceScale
	{
		get
		{
			return vehicle.MotorForceScale;
		}
		set
		{
			vehicle.MotorForceScale = value;
		}
	}

	public AdvancedSteeringBehavior SteeringBehavior
	{
		get
		{
			return steeringBehavior;
		}
		set
		{
			steeringBehavior = value;
		}
	}

	public SteeringArgument Steer => steering;

	public NPCCar()
	{
		MessageFun<Car>(typeof(NPCCar), processMsg);
	}

	public override bool Init(int id, GameObject carEffectRoot)
	{
		m_CarType = CarType.NPCCAR;
		bool result = base.Init(id, carEffectRoot);
		carRenderHelper.Init(base.transform.gameObject);
		checkReverseDrive = false;
		checkNoMovement = true;
		return result;
	}

	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
		VehicleData vehicleData = TSingleton<CarManager>.Instance.GetVehicleData(m_eCarModeType);
		vehicle = new Kart(vehicleData, base.gameObject.transform, lfp, rfp, lbp, rbp, EnmCollisionLayer.CL_PLAYER, this);
		vehicle.m_kUserData = this;
		m_steerFlag = steerFlag;
		steerFlag += 2;
		if (steerFlag >= 10)
		{
			steerFlag = 0;
		}
	}

	public override void UpdateCar()
	{
		UpdateCarActor();
		if (SystemConfig.Instance.StuckCheat)
		{
			UpdateStuckDetection();
		}
		if (SystemConfig.Instance.FallenCheat)
		{
			UpdateFallenDetection();
		}
		if (CanDrive() && SystemConfig.Instance.AutoUseProps)
		{
			UpdatePropControl();
		}
		CheckBadCondition();
	}

	protected override bool IsSmallMovement()
	{
		noMoveTime += Time.deltaTime;
		Vector3 position = GetPosition();
		bool braking = steering.Braking;
		bool flag = steering.Direction == SteeringArgument.SteeringDirection.Forth;
		if (braking)
		{
			noMoveTime = 0f;
			numNoMove = 0;
			lastPos = position;
		}
		if (!braking && !flag)
		{
			noMoveTime = 0f;
			numNoMove = 0;
			lastPos = position;
		}
		if (noMoveTime >= 1f)
		{
			float num = Mathf.Abs(position.x - lastPos.x) + Mathf.Abs(position.y - lastPos.y) + Mathf.Abs(position.z - lastPos.z);
			lastPos = position;
			if (num < 2f)
			{
				noMoveTime = 0f;
				numNoMove++;
				if ((float)numNoMove >= 5f)
				{
					numNoMove = 0;
					return true;
				}
			}
			else
			{
				noMoveTime = 0f;
				numNoMove = 0;
			}
		}
		return false;
	}

	private void DrawGroundEffect()
	{
	}

	[MessageFun(typeof(NPCCar))]
	public new void processMsg(int msg, ref ABMessageData data)
	{
		if (msg == 0)
		{
		}
	}

	public override void UpdatePhysics()
	{
		if (!CanDrive())
		{
			Steering(bAccelation: false, bBrake: false, 0f);
			base.UpdatePhysics();
		}
		else if (vehicle != null)
		{
			float rate = steering.Rate;
			bool bBackward = steering.Direction == SteeringArgument.SteeringDirection.Back;
			bool bAccelation = steering.Direction == SteeringArgument.SteeringDirection.Forth;
			bool braking = steering.Braking;
			AISteeringNew(bAccelation, braking, bBackward, rate);
			base.UpdatePhysics();
		}
	}

	public override void ResetPathFollowing()
	{
		steeringBehavior.UpdatePathFollowing();
	}

	public override void UpdateAISteering()
	{
		if (steeringBehavior != null && CanDrive())
		{
			m_steerFlag++;
			steeringBehavior.UpdateState(m_steerFlag >= 10);
			if (m_steerFlag >= 10)
			{
				m_steerFlag = 0;
			}
			steering = steeringBehavior.CalculateSteering();
		}
	}

	public override void InitSteeringBehavior(RacingLine racingLine)
	{
		steeringBehavior.AttachRacingLine(racingLine);
		steeringBehavior.AttachVehicle(this);
		steeringBehavior.UpdatePathFollowing();
		steeringBehavior.AttachObstacles(RaceWorld.Instance.Obstacles);
		steeringBehavior.MakeFSM();
	}

	public new void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		steeringBehavior.OnDrawGizmos();
	}

	protected new void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		steeringBehavior.OnDrawGizmosSelected();
	}

	public override void Uninit()
	{
		steeringBehavior = null;
		base.Uninit();
	}

	private static float RandomEquipeThrowingInterval()
	{
		return UnityEngine.Random.Range(2f, 5f);
	}

	private static float RandomPropThrowingInterval()
	{
		return UnityEngine.Random.Range(2f, 6f);
	}

	private void UpdatePropControl()
	{
		bool flag = false;
		bool throwForward = false;
		switch (TSingleton<PropManager>.Instance.Equiped(base.name))
		{
		case ePropType.BANANA:
			if (equipeThrowingTimer.DoAllAfter(RandomEquipeThrowingInterval()) > 0)
			{
				flag = true;
				throwForward = MathfEx.RandomTrue(0.2f);
			}
			break;
		case ePropType.TORTOISE_RED:
		case ePropType.TORTOISE_GREEN:
			if (equipeThrowingTimer.DoAllAfter(RandomEquipeThrowingInterval()) > 0)
			{
				flag = true;
				throwForward = MathfEx.RandomTrue(0.7f);
			}
			break;
		case ePropType.NONE:
			RaceSceneData.GetPlayerData(Convert.ToUInt64(base.ID), ref playerData);
			if (playerData.selProp != ePropType.NONE && propThrowingTimer.DoAllAfter(RandomPropThrowingInterval()) > 0)
			{
				flag = true;
				switch (playerData.selProp)
				{
				case ePropType.BANANA_THREE:
				case ePropType.TORTOISE_RED_THREE:
				case ePropType.TORTOISE_GREEN_THREE:
					flag = MathfEx.RandomTrue(0.8f);
					break;
				case ePropType.BANANA:
				case ePropType.CHEAT_BOX:
					flag = true;
					throwForward = MathfEx.RandomTrue(0.7f);
					break;
				case ePropType.TORTOISE_RED:
				case ePropType.TORTOISE_GREEN:
				case ePropType.TORTOISE_BLUE:
				case ePropType.BOMB:
					flag = true;
					throwForward = MathfEx.RandomTrue(0.7f);
					break;
				case ePropType.MUSHROOM_ONE:
				case ePropType.MUSHROOM_TWO:
				case ePropType.MUSHROOM_THREE:
				case ePropType.MUSHROOM_GOLDERN:
				case ePropType.MUSHROOM_BIG:
					flag = true;
					break;
				case ePropType.MISSILE:
				case ePropType.LUCKY_STAR:
				case ePropType.LIGHTNING:
				case ePropType.CUTTLE_FISH:
				case ePropType.QUAKE:
					flag = true;
					break;
				case ePropType.THUNDER_CLOUD:
					flag = false;
					break;
				default:
					flag = false;
					Debug.LogError("UpdatePropControl: Invalid prop type.");
					break;
				}
			}
			break;
		}
		if (flag)
		{
			SkillManager.Instance.UseProp(this, throwForward);
		}
	}

	private void PlayStuckEffect()
	{
		GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.GeGeLa_Effect, isPause: true, isOneShot: false);
		Effect component = gameObject.GetComponent<Effect>();
		component.AttachTo(base.gameObject.transform, new Vector3(0f, 0.839f, 0f), Vector3.zero);
		component.SetLifeTime(3f);
	}

	private void UpdateStuckDetection()
	{
		if (!CanDrive())
		{
			stuckDetector.Reset();
		}
		else if (stuckDetector.DectectStuck(Pos, Time.deltaTime))
		{
			HelpMe();
		}
	}

	private void UpdateFallenDetection()
	{
		if (Position.y < -20f)
		{
			if (!dontUseMissle)
			{
				HelpMe();
				dontUseMissle = true;
			}
		}
		else
		{
			dontUseMissle = false;
		}
	}

	private void HelpMe()
	{
		SkillManager.Instance.UseProp(this, throwForward: true);
	}
}
