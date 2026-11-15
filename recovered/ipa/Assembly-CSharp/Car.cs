using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RaceTrack;
using UnityEngine;

[MessageClass(typeof(Car))]
public class Car : ABCmdBehaviour, IAgent, IAutoCar, ICollisionCallback
{
	public enum CarType
	{
		NONE = -1,
		HeroCar,
		NPCCAR,
		PLAYERCAR
	}

	public const float FLY_BURST_ENABLE_TIME = 0.5f;

	private const float HIT_FENCE_CD = 2f;

	private const float HIT_CAR_EFFECT_CD = 2f;

	protected const float REVERSE_DEAD_TIME = 5f;

	protected const float NO_MOVEMENT_TIME = 1f;

	protected const float NO_MOVEMENT_DISTANCE = 2f;

	protected const float NO_MOVEMENT_NUM = 5f;

	public Tortoise mAttachTortoise;

	public Transform[] frontWheels;

	public Transform[] rearWheels;

	public CarModeType m_eCarModeType;

	public bool isHero;

	public btVehicleBase vehicle;

	public GameObject m_gameObjectFXSpeedy;

	public CarParticles carParticles;

	public CarActor carActor;

	public uint carState;

	protected GameObject m_objTrackDeath;

	protected bool m_bAlreadySendDeath;

	public RUNNING_MDOE m_RunMode;

	public CarType m_CarType;

	public string[] carWheelsList;

	public bool m_SpeedUpFlag;

	public Vector3 m_RecoverPos;

	public Vector3 m_RecoverVel;

	public Quaternion m_RecoverQuat;

	public Car m_CollideCar;

	public int m_CarDeadMode;

	public bool isHitThunderCloud;

	private Effect m_EftHitStar;

	private Effect m_EftHitByMissile;

	private Effect m_EftHitInvincibleCar;

	private Effect m_EftHitByBanana;

	private Effect m_EftScaleBig;

	private Effect m_EftLightning;

	private Effect m_EftHitByLeaf;

	private Effect m_EftHitByMushroom;

	private Effect m_EftInvincible;

	private Effect m_EftEarthQuake;

	private Effect m_EftQuakRot;

	public GameObject m_AnimCar;

	private GameObject m_EftCar;

	private GameObject m_EftCamera;

	protected FXTrail m_FXTrail;

	protected FXDrift m_FXDrift;

	private FXSpeedy m_FXS;

	private Dictionary<int, AudioSource> m_Audios;

	public float DISTANCE_TO_HIDE_2;

	public Vector2 m_ForwardHitByRobb;

	public float m_CarRadius;

	public float m_FollowDis;

	public float m_FollowHeight;

	public bool m_bFlyingBoostTrigger;

	public bool m_bDoCancelFlyBoost;

	public float _SpeedDown;

	public float _RotateAng0;

	public float _RotateAng1;

	public float _RotateAng2;

	public float _RotateAcc0;

	public float _RotateAcc1;

	public float _RotateAcc2;

	public float _P1Speed;

	public float _P1DownAcc;

	public float _P1UpAcc;

	public float _XShiftTotalTime;

	public float _XShiftRecoverTime;

	public Transform currentTouchSpeedUp;

	public float lastTimeTouchSpeedUp;

	protected CarRenderHelper carRenderHelper;

	private Camera cacheCamera;

	public ThunderCloud m_Thunder;

	public static float m_fLastHitFenceTime;

	public static float m_fLastHitAnotherCarTime;

	protected bool checkReverseDrive;

	protected bool checkNoMovement;

	protected bool inReverse;

	protected float inReverseTime;

	protected Vector3 lastPos;

	protected float noMoveTime;

	protected int numNoMove;

	protected float normalSpeed;

	public float FastSpeed;

	public float SlowSpeed;

	private Transform cHead;

	private Transform cTail;

	private Transform cBody;

	private PlayerState m_PlayerState;

	public float m_AccelerateTime;

	public float m_AccelerateSpeed;

	public float m_HorizonSpeed;

	public float m_VeticalSpeed;

	public float m_HorizonDampingCoefficient;

	public float m_VeticalDampingCoefficient;

	public float m_VeticalDampingCoefficient_Down;

	public int ID
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public float MaxTurnScale
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float MotorForceScale
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float GetConfigMaxSpeed
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public uint CarState
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool IsDead
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float MaxVelocity
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Vector3 PreviousVelocity
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RUNNING_MDOE RunningMode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ENMTERRAIN_TYPE GroundedType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ENM_SUB_TERRAIN_TYPE SlowEffectType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Pos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 Position
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float MaxSpeed
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float MaxForce
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Vel
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Heading
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 Right
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float Radius
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float CurrentMinimalCurveRadius
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public PlayerAIProfile AIProfile
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsInPowerStatus
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float Mass
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public string Name
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool MissleState
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarIncicible
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarScaleSmall
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarScaleFlat
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarScaleBig
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarMushroomBounce
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarBounceOver
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarDeath
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarAccelerate
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarThunderCloud
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CarCanNotByHit
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool CanControl
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual void SetCarState(uint state)
	{
	}

	public void SpeedUp()
	{
	}

	public void Play(string name)
	{
	}

	public void StopCarEft(string name)
	{
	}

	public void CheckHitTrackDeath()
	{
	}

	public void StopScreenEft(string name)
	{
	}

	private bool CheckEffectDistanceToCamera()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void PlayHitCarEffect(VehicleCollisionSide eSide)
	{
	}

	private void InitEffects()
	{
	}

	public void PlayCollisionEffect()
	{
	}

	public void PlayHitByMissile()
	{
	}

	public void PlayHitInvincibleCar()
	{
	}

	public void PlayHitByBananaEffect()
	{
	}

	public void PlayScaleBigCarEft()
	{
	}

	public void PlayHitByLightningEffect()
	{
	}

	public void PlayHitByLeafEffect()
	{
	}

	public void PlayHitMushroom()
	{
	}

	public void PlayInvincibleEffect()
	{
	}

	public void PlayEarthQuakeEffect()
	{
	}

	public void PlayQuakRotEft()
	{
	}

	public void RestAllParticle()
	{
	}

	public void DisableAllCarParticles()
	{
	}

	public bool PlaySound(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool PlaySound(int id, float valume)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PlayLowEngineSound(ushort carID, float volume)
	{
	}

	public void PlayHighEngineSound(ushort carID, float volume)
	{
	}

	public void StopLowEngineSound(ushort carID)
	{
	}

	public void StopHighEngineSound(ushort carID)
	{
	}

	public bool PlayOneSound(int id, float volume)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool StopSound(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PlayRoleHappySound()
	{
	}

	protected virtual void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
	}

	public virtual bool Init(int id, GameObject carEffectRoot)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void CameraDistanceHide(bool isNear)
	{
	}

	public bool IsFarFromCamera()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ResetComputeRender()
	{
	}

	public virtual void CreateTrailAndDriftVisualEffect(GameObject carEffectRoot)
	{
	}

	protected GameObject InitWheel(Transform transform, string strDummy)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void Uninit()
	{
	}

	public void Hide()
	{
	}

	public void Show()
	{
	}

	public void HideShadow()
	{
	}

	public void ShowShadow()
	{
	}

	public void DisableAllJetState()
	{
	}

	public void ThunderTransfer(Car destCar)
	{
	}

	public virtual void UpdateCar()
	{
	}

	public virtual void UpdatePhysics()
	{
	}

	public void UpdateCarActor()
	{
	}

	public void UpdateFXSpeedy()
	{
	}

	public float GetCarSpeed()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void LockSpeed(bool lockspeed)
	{
	}

	public void MoveToGround()
	{
	}

	public void OnDrawGizmos()
	{
	}

	protected void OnDrawGizmosSelected()
	{
	}

	public void EnableGroundFriction(bool bEnable)
	{
	}

	public void SetDynamicScaleFactor(float fScale)
	{
	}

	public void SetMaxColliderMass(bool bMax)
	{
	}

	public void EnableKinematic()
	{
	}

	public void DisableKinematic()
	{
	}

	public void BreakVehicle(float fAutoBreakTime)
	{
	}

	public Vector3 GetPreVelGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void LimitSpeed(float speed)
	{
	}

	public void SetVecGlobal(Vector3 vec)
	{
	}

	public void SetAccelate(Vector3 a)
	{
	}

	public Vector3 GetAccelate()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetPosition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetUpGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetForwardGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool MoveToGround(Vector3 vPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void SetPosition(Vector3 pos)
	{
	}

	public virtual void SetRotation(Quaternion rotation)
	{
	}

	public void SetCarTorqueScale(int flag)
	{
	}

	public void StartBoost(float fTime)
	{
	}

	public void StartBoost(float fTime, float fSpeed)
	{
	}

	public void StopBoost()
	{
	}

	public void SpeedPulse(float fTime, Vector3 vDir, float fV)
	{
	}

	public void EnableJet(bool enable)
	{
	}

	public virtual void EnableFlyBurst(bool enable)
	{
	}

	public virtual void DoFlyBurst()
	{
	}

	public float GetMass()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetMass(float fValue)
	{
	}

	public BoxComponent GetBox(int i)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetVecGlobal()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetVelocity()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetAheadSpeed()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetCurrentWaterPoint()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Quaternion GetRotation()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Steering(bool bAccelation, bool bBrake, float fSteering)
	{
	}

	public bool CheckCanDriveNow()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AISteeringNew(bool bAccelation, bool bBrake, bool bBackward, float fSteering)
	{
	}

	public void OnHitFence(Collider colfrom, Collider colto)
	{
	}

	public bool CanTranferThuderCloud()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool OnHitAnotherCar(btVehicleBase otherCar, VehicleCollisionSide eSide)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[MessageFun(typeof(Car))]
	public new void processMsg(int msg, ref ABMessageData data)
	{
	}

	protected virtual void CheckBadCondition()
	{
	}

	protected virtual void CheckReverseDrive()
	{
	}

	protected virtual void CheckNoMovement()
	{
	}

	public void SetReverse(bool reverse)
	{
	}

	protected virtual bool IsSmallMovement()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RestoreNormalSpeed()
	{
	}

	public float GetMaxSteeringAngle(float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetMinimalCurveRadius(float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void UpdateAISteering()
	{
	}

	public virtual void InitSteeringBehavior(RacingLine racing_line)
	{
	}

	public virtual void ResetPathFollowing()
	{
	}

	public void IgnoreNPC(bool bIgnore)
	{
	}

	public bool MatchState(uint flag)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void DropProp(bool drop)
	{
	}

	public Role GetRole(int idx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PlayAniOnly(string name)
	{
	}

	public void StopAniOnly()
	{
	}

	public void SetPlayerState(PlayerState state)
	{
	}

	public void ResetPlayerState()
	{
	}

	public PlayerState GetPlayerState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PickProp(ePropType type)
	{
	}

	public void PushRole(Role role)
	{
	}

	public void LockCarActor()
	{
	}

	public void AttachAIProfile(PlayerAIProfile profile)
	{
	}

	public void UnLockCarActor()
	{
	}

	public void HitByTortoise()
	{
	}

	public void HitByLuckyStar(bool right)
	{
	}

	public void UseThunderCloud()
	{
	}

	public void DisappearThunderCloud()
	{
	}

	public void AccelerateCar(float time)
	{
	}

	public void ShiftAccelerateCar(float time, float speed)
	{
	}

	public void InvincibleCar()
	{
	}

	public void PlayAni(string name)
	{
	}

	public void RecoverCarFromMissile()
	{
	}

	public void HitByMissile(bool right)
	{
	}

	public void BecomeMissile()
	{
	}

	public void HitByLightning()
	{
	}

	public void QuakeCar()
	{
	}

	public void HitByCuttleFish()
	{
	}

	public void HitByCheatBox()
	{
	}

	public void HitByBomb(bool bSendMessagetToOthers)
	{
	}

	public void PressByBigCar()
	{
	}

	public void PressByTrap()
	{
	}

	public void UseBigMushroom()
	{
	}

	public void HitByBanana()
	{
	}

	public bool CanDrive()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void HitByRobb(Vector2 direction)
	{
	}

	public void BackFlip()
	{
	}

	public void BackFlipDeputy()
	{
	}

	public void HitSimpleBlocker()
	{
	}

	public void CarDead(int mode)
	{
	}

	public bool CheckSpeedUpCondition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CheckFlyBoostCondition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void BounceCar(float horizeSpeed, float horizonCampingCoefficient, float verticalSpeed, float verticalCampingCoefficient, float verticalCampingCoefficient_down)
	{
	}

	public virtual bool IsInShiftMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual float GetShiftPower()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual int GetBoostTriggerState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
