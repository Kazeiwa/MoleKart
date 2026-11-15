using System.Collections.Generic;
using Misc;
using RaceTrack;
using UnityEngine;

[MessageClass(typeof(Car))]
public class Car : ABCmdBehaviour, IAgent, IAutoCar, CollisionCallback
{
	public enum CarType
	{
		NONE = -1,
		HeroCar,
		NPCCAR,
		PLAYERCAR
	}

	private const float HIT_FENCE_CD = 1f;

	private const float HIT_CAR_EFFECT_CD = 2f;

	protected const float REVERSE_DEAD_TIME = 5f;

	protected const float NO_MOVEMENT_TIME = 1f;

	protected const float NO_MOVEMENT_DISTANCE = 2f;

	protected const float NO_MOVEMENT_NUM = 5f;

	public Transform[] frontWheels;

	public Transform[] rearWheels;

	public CarModeType m_eCarModeType = CarModeType.CAR_UNKNOWN;

	public bool isHero;

	public btVehicleBase vehicle;

	public GameObject m_gameObjectFXSpeedy;

	public CarParticles carParticles;

	public CarActor carActor;

	public uint carState;

	public RUNNING_MDOE m_RunMode = RUNNING_MDOE.RM_GROUNDRUNING;

	public CarType m_CarType = CarType.NONE;

	public string[] carWheelsList = new string[4] { "leftback", "leftfront", "rightback", "rightfront" };

	public bool m_SpeedUpFlag;

	public Vector3 m_RecoverPos;

	public Vector3 m_RecoverVel;

	public Quaternion m_RecoverQuat;

	public Car m_CollideCar;

	public int m_CarDeadMode = -1;

	public Effect m_LightningEft;

	public GameObject m_AnimCar;

	private GameObject m_EftCar;

	private GameObject m_EftCamera;

	protected FXTrail m_FXTrail;

	protected FXDrift m_FXDrift;

	private FXSpeedy m_FXS;

	private Dictionary<int, AudioSource> m_Audios = new Dictionary<int, AudioSource>();

	public float DISTANCE_TO_HIDE_2 = 10000f;

	public Vector2 m_ForwardHitByRobb = Vector2.zero;

	public float m_CarRadius = 1.9f;

	public float m_FollowDis = 1.5f;

	public float m_FollowHeight;

	public float _SpeedDown = 0.75f;

	public float _RotateAng0 = 20f;

	public float _RotateAng1 = 40f;

	public float _RotateAng2 = 60f;

	public float _RotateAcc0 = 200f;

	public float _RotateAcc1 = 400f;

	public float _RotateAcc2 = -200f;

	public float _P1Speed = 26f;

	public float _P1DownAcc = 8f;

	public float _P1UpAcc = 8f;

	public float _XShiftTotalTime;

	public float _XShiftRecoverTime;

	public Transform currentTouchSpeedUp;

	public float lastTimeTouchSpeedUp;

	protected CarRenderHelper carRenderHelper = new CarRenderHelper();

	private Effect m_EftHitStar;

	private Effect m_EftHitByMissile;

	private Effect m_EftHitInvincibleCar;

	private Effect m_EftHitByBanana;

	private Effect m_EftScaleBig;

	private Effect m_EftHitByLeaf;

	private Effect m_EftHitByMushroom;

	private Effect m_EftInvincible;

	private Effect m_EftEarthQuake;

	private Effect m_EftQuakRot;

	public ThunderCloud m_Thunder;

	public static float m_fLastHitFenceTime = -1f;

	public static float m_fLastHitAnotherCarTime = -1f;

	protected bool checkReverseDrive;

	protected bool checkNoMovement;

	protected bool inReverse;

	protected float inReverseTime;

	protected Vector3 lastPos = Vector3.zero;

	protected float noMoveTime;

	protected int numNoMove;

	protected float normalSpeed;

	public float FastSpeed = 35f;

	public float SlowSpeed = 15f;

	private Transform cHead;

	private Transform cTail;

	private Transform cBody;

	private PlayerState m_PlayerState = PlayerState.STATE_NONE;

	public float m_AccelerateTime;

	public float m_HorizonSpeed;

	public float m_VeticalSpeed;

	public float m_HorizonDampingCoefficient = 2f;

	public float m_VeticalDampingCoefficient = 2f;

	public float m_VeticalDampingCoefficient_Down = 2f;

	public int ID { get; set; }

	public float MaxTurnScale
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

	public float MotorForceScale
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

	public float GetConfigMaxSpeed => vehicle.GetPhyData().fMaxSpeed;

	public uint CarState
	{
		get
		{
			return carState;
		}
		set
		{
			carState = value;
		}
	}

	public bool IsDead => InState(CarLogicState.CarDeath);

	public float MaxVelocity
	{
		get
		{
			if (vehicle != null)
			{
				return vehicle.MaxVelocity;
			}
			return 0f;
		}
		set
		{
			if (vehicle != null)
			{
				vehicle.MaxVelocity = value;
			}
		}
	}

	public Vector3 PreviousVelocity => GetPreVelGlobal();

	public RUNNING_MDOE RunningMode
	{
		get
		{
			if (vehicle != null)
			{
				return vehicle.RunningMode;
			}
			return RUNNING_MDOE.RM_BOUNCE;
		}
	}

	public ENMTERRAIN_TYPE GroundedType
	{
		get
		{
			if (vehicle != null)
			{
				return vehicle.GroundedType;
			}
			return ENMTERRAIN_TYPE.ETT_NONE;
		}
	}

	public Vector2 Pos => vehicle.Pos;

	public Vector3 Position => vehicle.Position;

	public float MaxSpeed
	{
		get
		{
			if (normalSpeed == 0f)
			{
				normalSpeed = vehicle.GetPhyData().fMaxSpeed;
			}
			return normalSpeed;
		}
		set
		{
			normalSpeed = value;
		}
	}

	public float MaxForce => 10f;

	public Vector2 Vel => vehicle.Velocity;

	public Vector2 Heading => vehicle.Heading;

	public Vector2 Right => vehicle.Right;

	public float Radius => vehicle.GetBody().BoundRadius;

	public float CurrentMinimalCurveRadius => vehicle.CurrentMinimalCurveRadius;

	public PlayerAIProfile AIProfile => carActor.AIProfile;

	public bool IsInPowerStatus
	{
		get
		{
			CarLogicState carLogicState = (CarLogicState)CarState;
			if (carLogicState == CarLogicState.CarBecomeMissile || carLogicState == CarLogicState.CarInvincible || carLogicState == CarLogicState.CarScaleBig)
			{
				return true;
			}
			return false;
		}
	}

	public float Mass => GetMass();

	public string Name => base.name;

	public bool MissleState
	{
		get
		{
			if ((carState & 0x20) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarIncicible
	{
		get
		{
			if ((carState & 0x40) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarScaleSmall
	{
		get
		{
			if ((carState & 0x800) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarScaleFlat
	{
		get
		{
			if ((carState & 0x4000) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarScaleBig
	{
		get
		{
			if ((carState & 0x80) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarMushroomBounce
	{
		get
		{
			if ((carState & 0x20000) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarBounceOver
	{
		get
		{
			if ((carState & 0x40000) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarDeath
	{
		get
		{
			if ((carState & 0x8000) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarThunderCloud
	{
		get
		{
			if ((carState & 0x100) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CarCanNotByHit
	{
		get
		{
			if ((carState & 0x10000) != 0)
			{
				return true;
			}
			if ((carState & 0x200) != 0)
			{
				return true;
			}
			if ((carState & 0x10) != 0)
			{
				return true;
			}
			if ((carState & 0x2000) != 0)
			{
				return true;
			}
			if ((carState & 0x1000) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CanControl
	{
		get
		{
			if ((carState & 0x10000) != 0)
			{
				return false;
			}
			if ((carState & 0x200) != 0)
			{
				return false;
			}
			if ((carState & 0x10) != 0)
			{
				return false;
			}
			if ((carState & 0x2000) != 0)
			{
				return false;
			}
			if ((carState & 0x1000) != 0)
			{
				return false;
			}
			return true;
		}
	}

	public Car()
	{
		MessageFun<ABCmdBehaviour>(typeof(Car), processMsg);
	}

	public virtual void SetCarState(uint state)
	{
		CarState = state;
	}

	private bool InState(CarLogicState flag)
	{
		return 0 != (carState & (uint)flag);
	}

	public void SpeedUp()
	{
		m_SpeedUpFlag = true;
	}

	public void Play(string name)
	{
		if (null != m_AnimCar && null != m_AnimCar.animation && null != m_AnimCar.animation.GetClip(name))
		{
			m_AnimCar.animation.Play(name);
		}
	}

	public void StopCarEft(string name)
	{
		if (!(null == m_EftCar))
		{
			Transform transform = m_EftCar.transform.FindChild(name);
			if (null != transform && null != transform.GetComponent<Effect>())
			{
				transform.GetComponent<Effect>().Stop();
			}
		}
	}

	public void StopScreenEft(string name)
	{
		if (!(null == m_EftCamera))
		{
			Transform transform = m_EftCamera.transform.FindChild(name);
			if (null != transform && null != transform.GetComponent<Effect>())
			{
				transform.GetComponent<Effect>().Stop();
			}
		}
	}

	private bool CheckEffectDistanceToCamera()
	{
		bool result = true;
		GameObject gameObject = GameObject.Find("MainCamera");
		if ((bool)gameObject)
		{
			Camera component = gameObject.GetComponent<Camera>();
			if (component != null)
			{
				Vector3 position = vehicle.Position;
				if ((position - component.transform.position).sqrMagnitude > DISTANCE_TO_HIDE_2)
				{
					result = false;
				}
			}
		}
		return result;
	}

	private void PlayHitCarEffect(VehicleCollisionSide eSide)
	{
		if (vehicle.GetVelocity() < 5f)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject != null && vehicle != null)
		{
			Vector3 position = vehicle.Position;
			if ((position - gameObject.transform.position).sqrMagnitude > 22500f)
			{
				return;
			}
		}
		Vector3 zero = Vector3.zero;
		if ((eSide & VehicleCollisionSide.CS_LEFT) == VehicleCollisionSide.CS_LEFT)
		{
			zero.x = (0f - vehicle.GetBody().BoundRadius) * 0.5f;
		}
		else if ((eSide & VehicleCollisionSide.CS_RIGHT) == VehicleCollisionSide.CS_RIGHT)
		{
			zero.x = vehicle.GetBody().BoundRadius * 0.5f;
		}
		else if ((eSide & VehicleCollisionSide.CS_FRONT) == VehicleCollisionSide.CS_FRONT)
		{
			zero.z = vehicle.GetBody().BoundRadius;
		}
		if (Random.Range(0, 1) == 0)
		{
			PlaySound(40008, 0.5f, 1);
		}
		else
		{
			PlaySound(40009, 0.5f, 1);
		}
		if (m_EftHitStar != null && CheckEffectDistanceToCamera())
		{
			m_EftHitStar.AttachTo(base.gameObject.transform.FindChild("effect"), zero, new Vector3(0f, 0f, 0f));
			m_EftHitStar.Enable();
		}
	}

	private void Start()
	{
		Transform parentTrans = base.gameObject.transform.FindChild("effect");
		GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.HIT_STAR, isPause: true, isOneShot: false);
		m_EftHitStar = gameObject.GetComponent<Effect>();
		m_EftHitStar.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_EftHitStar.SetLifeTime(0.5f);
		m_EftHitStar.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.HIT_BY_MISSILE, isPause: true, isOneShot: false);
		m_EftHitByMissile = gameObject.GetComponent<Effect>();
		m_EftHitByMissile.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_EftHitByMissile.SetLifeTime(0.5f);
		m_EftHitByMissile.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.HIT_BY_MISSILE, isPause: true, isOneShot: false);
		m_EftHitInvincibleCar = gameObject.GetComponent<Effect>();
		m_EftHitInvincibleCar.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_EftHitInvincibleCar.SetLifeTime(0.5f);
		m_EftHitInvincibleCar.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.HIT_BY_BANANA, isPause: true, isOneShot: false);
		m_EftHitByBanana = gameObject.GetComponent<Effect>();
		m_EftHitByBanana.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_EftHitByBanana.SetLifeTime(0.5f);
		m_EftHitByBanana.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.SCALE_BIG, isPause: true, isOneShot: false);
		m_EftScaleBig = gameObject.GetComponent<Effect>();
		m_EftScaleBig.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_EftScaleBig.SetLifeTime(3f);
		m_EftScaleBig.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.HIT_BY_LEAF, isPause: true, isOneShot: false);
		m_EftHitByLeaf = gameObject.GetComponent<Effect>();
		m_EftHitByLeaf.AttachTo(GameObject.Find("MainCamera").transform, new Vector3(0f, 0.02759956f, 4f), new Vector3(0f, 0f, 0f));
		m_EftHitByLeaf.SetLifeTime(10f);
		m_EftHitByLeaf.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.HITMUSHROOM, isPause: true, isOneShot: false);
		m_EftHitByMushroom = gameObject.GetComponent<Effect>();
		m_EftHitByMushroom.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_EftHitByMushroom.SetLifeTime(0.5f);
		m_EftHitByMushroom.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.INVINCIBLE_EFFECT, isPause: true, isOneShot: false);
		m_EftInvincible = gameObject.GetComponent<Effect>();
		m_EftInvincible.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_EftInvincible.SetLifeTime(InvincibleState.TIME);
		m_EftInvincible.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.EARTH_QUAKE, isPause: true, isOneShot: false);
		m_EftEarthQuake = gameObject.GetComponent<Effect>();
		m_EftEarthQuake.AttachTo(GameObject.Find("MainCamera").transform, new Vector3(0f, 2.351988f, 8.573843f), new Vector3(0f, 0f, 0f));
		m_EftEarthQuake.SetLifeTime(QuakeCarState.QUAKE_TIME);
		m_EftEarthQuake.Disable();
		gameObject = EffectManager.Instance.CreateEffect(eEffectType.QUAK_ROT, isPause: true, isOneShot: false);
		m_EftQuakRot = gameObject.GetComponent<Effect>();
		m_EftQuakRot.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_EftQuakRot.SetLifeTime(3.5f);
		m_EftQuakRot.Disable();
	}

	public void PlayCollisionEffect()
	{
		if (m_EftHitStar != null && CheckEffectDistanceToCamera())
		{
			m_EftHitStar.Enable();
		}
	}

	public void PlayHitByMissile()
	{
		if (m_EftHitByMissile != null && CheckEffectDistanceToCamera())
		{
			m_EftHitByMissile.Enable();
		}
	}

	public void PlayHitInvincibleCar()
	{
		if (m_EftHitInvincibleCar != null && CheckEffectDistanceToCamera())
		{
			m_EftHitInvincibleCar.Enable();
		}
	}

	public void PlayHitByBananaEffect()
	{
		if (m_EftHitByBanana != null && CheckEffectDistanceToCamera())
		{
			m_EftHitByBanana.Enable();
		}
	}

	public void PlayScaleBigCarEft()
	{
		if (m_EftScaleBig != null && CheckEffectDistanceToCamera())
		{
			m_EftScaleBig.Enable();
		}
	}

	public void PlayHitByLightningEffect()
	{
		if (m_LightningEft != null && CheckEffectDistanceToCamera())
		{
			m_LightningEft.Enable();
		}
	}

	public void PlayHitByLeafEffect()
	{
		if (m_EftHitByLeaf != null && CheckEffectDistanceToCamera())
		{
			m_EftHitByLeaf.Enable();
		}
	}

	public void PlayHitMushroom()
	{
		if (m_EftHitByMushroom != null && CheckEffectDistanceToCamera())
		{
			m_EftHitByMushroom.Enable();
		}
	}

	public void PlayInvincibleEffect()
	{
		if (m_EftInvincible != null && CheckEffectDistanceToCamera())
		{
			m_EftInvincible.name = "PlayInvincibleEffect";
			m_EftInvincible.Enable();
		}
	}

	public void PlayEarthQuakeEffect()
	{
		if (m_EftEarthQuake != null && CheckEffectDistanceToCamera())
		{
			m_EftEarthQuake.Enable();
		}
	}

	public void PlayQuakRotEft()
	{
		if (m_EftQuakRot != null && CheckEffectDistanceToCamera())
		{
			m_EftQuakRot.Enable();
		}
	}

	public void RestAllParticle()
	{
		m_LightningEft.Disable();
		if (carParticles != null)
		{
			carParticles.EnableJet(bBoost: true);
		}
	}

	public void DisableAllCarParticles()
	{
		if (carParticles != null)
		{
			carParticles.DisableAllJetState(bBoost: true);
		}
	}

	public bool PlaySound(int id, int priority)
	{
		if (!m_Audios.ContainsKey(id))
		{
			AudioSource audioSource = SoundManager.Instance.AddAudio(base.gameObject, id);
			if (null != audioSource)
			{
				m_Audios.Add(id, audioSource);
			}
		}
		if (m_Audios.ContainsKey(id) && null != m_Audios[id])
		{
			SoundManager.Instance.PlayAudio(base.gameObject, m_Audios[id], priority);
			return true;
		}
		return false;
	}

	public bool PlaySound(int id, float valume, int priority)
	{
		if (!m_Audios.ContainsKey(id))
		{
			AudioSource audioSource = SoundManager.Instance.AddAudio(base.gameObject, id);
			if (null != audioSource)
			{
				m_Audios.Add(id, audioSource);
			}
		}
		if (m_Audios.ContainsKey(id) && null != m_Audios[id])
		{
			m_Audios[id].volume = valume;
			SoundManager.Instance.PlayAudio(base.gameObject, m_Audios[id], priority);
			return true;
		}
		return false;
	}

	public void PlayLowEngineSound(ushort carID, float volume)
	{
		switch (carID)
		{
		case 0:
			PlayOneSound(40101, volume);
			break;
		case 1:
			PlayOneSound(40201, volume);
			break;
		default:
			PlayOneSound(40101, volume);
			break;
		}
	}

	public void PlayHighEngineSound(ushort carID, float volume)
	{
		switch (carID)
		{
		case 0:
			PlayOneSound(40102, volume);
			break;
		case 1:
			PlayOneSound(40202, volume);
			break;
		default:
			PlayOneSound(40102, volume);
			break;
		}
	}

	public void StopLowEngineSound(ushort carID)
	{
		switch (carID)
		{
		case 0:
			StopSound(40101);
			break;
		case 1:
			StopSound(40201);
			break;
		default:
			StopSound(40101);
			break;
		}
	}

	public void StopHighEngineSound(ushort carID)
	{
		switch (carID)
		{
		case 0:
			StopSound(40102);
			break;
		case 1:
			StopSound(40202);
			break;
		default:
			StopSound(40102);
			break;
		}
	}

	public bool PlayOneSound(int id, float volume)
	{
		if (!m_Audios.ContainsKey(id))
		{
			AudioSource audioSource = SoundManager.Instance.AddAudio(base.gameObject, id);
			if (null != audioSource)
			{
				m_Audios.Add(id, audioSource);
			}
		}
		if (m_Audios.ContainsKey(id) && null != m_Audios[id] && !m_Audios[id].isPlaying)
		{
			m_Audios[id].volume = volume;
			SoundManager.Instance.PlayAudio(base.gameObject, m_Audios[id], 0);
			return true;
		}
		return false;
	}

	public bool StopSound(int id)
	{
		if (m_Audios.ContainsKey(id) && null != m_Audios[id])
		{
			SoundManager.Instance.StopAudio(m_Audios[id]);
			return true;
		}
		return false;
	}

	protected virtual void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
		VehicleData vehicleData = TSingleton<CarManager>.Instance.GetVehicleData(m_eCarModeType);
		vehicle = new Vehicle(vehicleData, base.gameObject.transform, lfp, rfp, lbp, rbp, EnmCollisionLayer.CL_PLAYER, this);
		carParticles = new CarParticles(this, eEffectType.JET, eEffectType.JET_BOOST, eEffectType.JETBACK);
		vehicle.m_kUserData = this;
	}

	public virtual bool Init(int id, GameObject carEffectRoot)
	{
		ID = id;
		m_AnimCar = base.transform.Find("car").gameObject;
		m_EftCar = base.transform.FindChild("effect").gameObject;
		m_EftCamera = GameObject.Find("MainCamera");
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		GameObject gameObject3 = null;
		GameObject gameObject4 = null;
		gameObject2 = InitWheel(base.transform, carWheelsList[0]);
		gameObject = InitWheel(base.transform, carWheelsList[1]);
		gameObject4 = InitWheel(base.transform, carWheelsList[2]);
		gameObject3 = InitWheel(base.transform, carWheelsList[3]);
		frontWheels = new Transform[2];
		rearWheels = new Transform[2];
		frontWheels[0] = ((!(gameObject == null)) ? gameObject.transform : null);
		frontWheels[1] = ((!(gameObject3 == null)) ? gameObject3.transform : null);
		rearWheels[0] = ((!(gameObject2 == null)) ? gameObject2.transform : null);
		rearWheels[1] = ((!(gameObject4 == null)) ? gameObject4.transform : null);
		carActor = new CarActor(this);
		carState = 0u;
		Transform lfp = ((!(gameObject == null)) ? gameObject.transform.parent : null);
		Transform rfp = ((!(gameObject3 == null)) ? gameObject3.transform.parent : null);
		Transform lbp = ((!(gameObject2 == null)) ? gameObject2.transform.parent : null);
		Transform rbp = ((!(gameObject4 == null)) ? gameObject4.transform.parent : null);
		CreateVehicle(lfp, rfp, lbp, rbp);
		if (vehicle is Vehicle)
		{
			CreateTrailAndDriftVisualEffect(carEffectRoot);
		}
		m_gameObjectFXSpeedy = GameObject.Find("FX_Root/FX_Speedy");
		GameObject gameObject5 = EffectManager.Instance.CreateEffect(eEffectType.HIT_BY_LIGHTNING, isPause: true, isOneShot: false);
		m_LightningEft = gameObject5.GetComponent<Effect>();
		m_LightningEft.AttachTo(base.gameObject.transform.FindChild("effect"), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_LightningEft.Disable();
		if (null != base.transform.FindChild("car").gameObject.animation)
		{
			base.transform.FindChild("car").gameObject.animation.Stop();
		}
		return true;
	}

	public void CameraDistanceHide(bool isNear)
	{
		if (isNear)
		{
			carRenderHelper.SetLodLevel(1);
		}
		else
		{
			carRenderHelper.SetLodLevel(0);
		}
	}

	public void ResetComputeRender()
	{
		carRenderHelper.ResetMesh();
	}

	public virtual void CreateTrailAndDriftVisualEffect(GameObject carEffectRoot)
	{
		if (null == base.gameObject.GetComponent<FXTrail>() && frontWheels[0] != null && frontWheels[1] != null)
		{
			FXTrail fXTrail = base.gameObject.AddComponent<FXTrail>();
			if ((bool)fXTrail)
			{
				fXTrail._TyreLeft = rearWheels[0].gameObject;
				fXTrail._TyreRight = rearWheels[1].gameObject;
				Material material = new Material(Shader.Find("Particles/Alpha Blended"));
				Texture2D texture = Resources.Load("Effects/texture/trail") as Texture2D;
				material.SetTexture("_MainTex", texture);
				material.SetColor("_TintColor", new Color(0f, 0f, 0f, 1f));
				fXTrail.Init(material, carEffectRoot);
				fXTrail.EnableTrail(bEnabled: false);
			}
		}
		if (null == base.gameObject.GetComponent<FXDrift>() && frontWheels[0] != null && frontWheels[1] != null)
		{
			FXDrift fXDrift = base.gameObject.AddComponent<FXDrift>();
			if ((bool)fXDrift)
			{
				fXDrift.m_TyreLeft = rearWheels[0].gameObject;
				fXDrift.m_TyreRight = rearWheels[1].gameObject;
				Material material2 = new Material(Shader.Find("Particles/Alpha Blended"));
				Texture2D texture2 = Resources.Load("Effects/texture/Drift") as Texture2D;
				material2.SetTexture("_MainTex", texture2);
				material2.SetColor("_TintColor", new Color(255f, 255f, 255f, 1f));
				fXDrift.Init(material2, carEffectRoot);
				fXDrift.EnableDirft(bEnabled: false);
			}
		}
		m_gameObjectFXSpeedy = GameObject.Find("FX_Root/FX_Speedy");
		GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.HIT_BY_LIGHTNING, isPause: true, isOneShot: false);
		m_LightningEft = gameObject.GetComponent<Effect>();
		m_LightningEft.AttachTo(base.gameObject.transform.FindChild("effect"), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
		m_LightningEft.Disable();
		if (null != base.gameObject.animation)
		{
			base.animation.Stop();
		}
	}

	protected GameObject InitWheel(Transform transform, string strDummy)
	{
		Transform transform2 = null;
		transform2 = Utility.FindChildTranform(transform, strDummy);
		if (transform2 == null)
		{
			return null;
		}
		transform2 = Utility.FindFirstChildTranform(transform2);
		if (transform2 == null)
		{
			return null;
		}
		return transform2.gameObject;
	}

	public virtual void Uninit()
	{
		Debug.Log("Car:Uninit()");
		if (carActor != null)
		{
			carActor.Uninit();
		}
		if (vehicle != null)
		{
			vehicle.Uninit();
		}
	}

	public void Hide()
	{
		carRenderHelper.SetVisibleAll(enable: false);
		Transform transform = base.transform.FindChild("effect");
		if (null != transform)
		{
			Effect[] componentsInChildren = transform.GetComponentsInChildren<Effect>();
			if (componentsInChildren != null)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (null != componentsInChildren[i])
					{
						componentsInChildren[i].Disable();
					}
				}
			}
		}
		transform = base.transform.FindChild("car/root/leftback");
		if (null != transform)
		{
			Effect[] componentsInChildren2 = transform.GetComponentsInChildren<Effect>();
			if (componentsInChildren2 != null)
			{
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					if (null != componentsInChildren2[j])
					{
						componentsInChildren2[j].Disable();
					}
				}
			}
		}
		transform = base.transform.FindChild("car/root/rightback");
		if (null != transform)
		{
			Effect[] componentsInChildren3 = transform.GetComponentsInChildren<Effect>();
			if (componentsInChildren3 != null)
			{
				for (int k = 0; k < componentsInChildren3.Length; k++)
				{
					if (null != componentsInChildren3[k])
					{
						componentsInChildren3[k].Disable();
					}
				}
			}
		}
		if (carParticles != null)
		{
			carParticles.DisableAllJetState(bBoost: true);
		}
	}

	public void Show()
	{
		carRenderHelper.SetVisibleAll(enable: true);
		if (carParticles != null)
		{
			carParticles.DisableAllJetState(bBoost: false);
		}
	}

	public void HideShadow()
	{
		carRenderHelper.SetShadow(enable: false);
	}

	public void ShowShadow()
	{
		carRenderHelper.SetShadow(enable: true);
	}

	public void DisableAllJetState()
	{
		if (carParticles != null)
		{
			carParticles.DisableAllJetState(bBoost: true);
		}
	}

	public void ThunderTransfer(Car destCar)
	{
		if (null != m_Thunder)
		{
			m_Thunder.Transfer(destCar);
		}
	}

	public virtual void UpdateCar()
	{
	}

	public virtual void UpdatePhysics()
	{
		if (carActor != null)
		{
			carActor.UpdatePhysics();
		}
		if (vehicle != null)
		{
			vehicle.UpdateVehicle();
		}
	}

	public void UpdateCarActor()
	{
		if (vehicle != null)
		{
			vehicle.Update();
		}
		if (carActor != null)
		{
			carActor.Update();
		}
	}

	public void UpdateFXSpeedy()
	{
		if (m_gameObjectFXSpeedy != null)
		{
			if (null == m_FXS)
			{
				m_FXS = m_gameObjectFXSpeedy.GetComponent<FXSpeedy>();
			}
			if (m_FXS != null)
			{
				if (vehicle.GetBoostMode())
				{
					m_FXS.EnableEffect(bEnabled: true);
				}
				else if (((carState & 0x40) != 0 || (carState & 0x80) != 0 || (carState & 0x100) != 0) && vehicle.GetVelocity() >= vehicle.MaxVelocity * 0.7f)
				{
					m_FXS.EnableEffect(bEnabled: true);
				}
				else if ((carState & 0x20000) != 0)
				{
					m_FXS.EnableEffect(bEnabled: true);
				}
				else
				{
					m_FXS.EnableEffect(bEnabled: false);
				}
			}
		}
		else
		{
			m_gameObjectFXSpeedy = GameObject.Find("FX_Root/FX_Speedy");
		}
	}

	public float GetCarSpeed()
	{
		if (vehicle != null)
		{
			return vehicle.GetVecGlobal().magnitude;
		}
		return 0f;
	}

	public void LockSpeed(bool lockspeed)
	{
		if (vehicle != null)
		{
			vehicle.m_LockSpeed = lockspeed;
		}
	}

	public void MoveToGround()
	{
		if (vehicle != null)
		{
			vehicle.MoveToGround(GetPosition());
		}
	}

	public void OnDrawGizmos()
	{
		if (vehicle != null)
		{
			vehicle.OnDrawGizmos();
			if (SystemConfig.Instance.GizmosVehicleName)
			{
				GizmosEx.DrawText(Position, base.name);
			}
		}
	}

	protected void OnDrawGizmosSelected()
	{
		if (SystemConfig.Instance.GizmosWorldPartition && vehicle != null)
		{
			vehicle.OnDrawGizmosSelected();
		}
	}

	public void EnableGroundFriction(bool bEnable)
	{
		if (vehicle != null)
		{
			vehicle.EnableGroundFriction(bEnable);
		}
	}

	public void SetDynamicScaleFactor(float fScale)
	{
		if (vehicle != null)
		{
			vehicle.SetDynamicScaleFactor(fScale);
		}
	}

	public void SetMaxColliderMass(bool bMax)
	{
		if (vehicle != null)
		{
			vehicle.SetMaxColliderMass(bMax);
		}
	}

	public void EnableKinematic()
	{
		if (vehicle != null)
		{
			vehicle.EnableKinematic();
		}
	}

	public void DisableKinematic()
	{
		if (vehicle != null)
		{
			vehicle.DisableKinematic();
		}
	}

	public void BreakVehicle(float fAutoBreakTime)
	{
		if (vehicle != null)
		{
			vehicle.BreakVehicle(fAutoBreakTime);
		}
	}

	public Vector3 GetPreVelGlobal()
	{
		if (vehicle != null)
		{
			return vehicle.m_vPreVelGlobal;
		}
		return Vector3.zero;
	}

	public void LimitSpeed(float speed)
	{
		if (vehicle != null)
		{
			vehicle.fLimitSpeed = speed;
		}
	}

	public void SetVecGlobal(Vector3 vec)
	{
		if (vehicle != null)
		{
			vehicle.SetVecGlobal(vec);
		}
	}

	public void SetAccelate(Vector3 a)
	{
		if (vehicle != null)
		{
			vehicle.SetAccelate(a);
		}
	}

	public Vector3 GetAccelate()
	{
		if (vehicle != null)
		{
			vehicle.GetAccelate();
		}
		return Vector3.zero;
	}

	public Vector3 GetPosition()
	{
		if (vehicle != null)
		{
			return vehicle.GetPosition();
		}
		return Vector3.zero;
	}

	public Vector3 GetUpGlobal()
	{
		if (vehicle != null)
		{
			return vehicle.GetUpGlobal();
		}
		return Vector3.up;
	}

	public Vector3 GetForwardGlobal()
	{
		if (vehicle != null)
		{
			return vehicle.GetForwardGlobal();
		}
		return Vector3.forward;
	}

	public bool MoveToGround(Vector3 vPos)
	{
		if (vehicle != null)
		{
			return vehicle.MoveToGround(vPos);
		}
		return false;
	}

	public virtual void SetPosition(Vector3 pos)
	{
		if (vehicle != null)
		{
			vehicle.SetPosition(pos);
		}
	}

	public virtual void SetRotation(Quaternion rotation)
	{
		if (vehicle != null)
		{
			vehicle.SetRotation(rotation);
		}
	}

	public void SetCarTorqueScale(int flag)
	{
		if (vehicle != null)
		{
			vehicle.m_CarTorqueFlag = flag;
		}
	}

	public void StartBoost(float fTime)
	{
		if (vehicle != null)
		{
			vehicle.StartBoost(fTime);
		}
		if (carParticles != null)
		{
			carParticles.EnableJet(bBoost: true);
		}
	}

	public void StartBoost(float fTime, float fSpeed)
	{
		if (vehicle != null)
		{
			vehicle.StartBoost(fTime, fSpeed);
		}
		if (carParticles != null)
		{
			carParticles.EnableJet(bBoost: true);
		}
	}

	public void StopBoost()
	{
		if (vehicle != null)
		{
			vehicle.StopBoost();
		}
		if (carParticles != null)
		{
			carParticles.EnableJet(bBoost: false);
		}
	}

	public void SpeedPulse(float fTime, Vector3 vDir, float fV)
	{
		if (vehicle != null)
		{
			vehicle.AddForceGlobal(vDir * fV, ENMFORCETYPE.EFT_VELOCITY_CHANGE, fTime);
		}
	}

	public void EnableJet(bool enable)
	{
		if (carParticles != null)
		{
			carParticles.EnableJet(enable);
		}
	}

	public float GetMass()
	{
		if (vehicle != null)
		{
			return vehicle.GetMass();
		}
		return 0f;
	}

	public void SetMass(float fValue)
	{
		if (vehicle != null)
		{
			vehicle.SetMass(fValue);
		}
	}

	public BoxComponent GetBox(int i)
	{
		if (vehicle != null)
		{
			return vehicle.GetBox(i);
		}
		return null;
	}

	public Vector3 GetVecGlobal()
	{
		if (vehicle != null)
		{
			return vehicle.GetVecGlobal();
		}
		return Vector3.zero;
	}

	public float GetVelocity()
	{
		if (vehicle != null)
		{
			return vehicle.GetVelocity();
		}
		return 0f;
	}

	public float GetAheadSpeed()
	{
		if (vehicle != null)
		{
			return Vector3.Dot(vehicle.m_kRigidBody.velocity, vehicle.GetForwardGlobal());
		}
		return 0f;
	}

	public Vector3 GetCurrentWaterPoint()
	{
		if (vehicle != null)
		{
			return vehicle.GetCurrentWaterPoint();
		}
		return Vector3.zero;
	}

	public Quaternion GetRotation()
	{
		if (vehicle != null)
		{
			return vehicle.GetRotation();
		}
		return Quaternion.identity;
	}

	public void Steering(bool bAccelation, bool bBrake, float fSteering)
	{
		if (vehicle != null)
		{
			vehicle.Steering(bAccelation, bBrake, fSteering);
		}
	}

	public bool CheckCanDriveNow()
	{
		if (vehicle != null)
		{
			return vehicle.CheckCanDriveNow();
		}
		return false;
	}

	public void AISteeringNew(bool bAccelation, bool bBrake, bool bBackward, float fSteering)
	{
		if (vehicle != null)
		{
			vehicle.AISteeringNew(bAccelation, bBrake, bBackward, fSteering);
		}
	}

	public void OnHitFence(Collider colfrom, Collider colto)
	{
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject != null && vehicle != null)
		{
			Vector3 position = vehicle.Position;
			if ((position - gameObject.transform.position).sqrMagnitude > 22500f)
			{
				return;
			}
		}
		if (vehicle != null && vehicle.GetVecLocal().magnitude > 5f)
		{
			string currentPlatform = AppMain.Instance.GetCurrentPlatform();
			if (this is HeroCar && m_fLastHitFenceTime < Time.time - 1f)
			{
				m_fLastHitFenceTime = Time.time;
				PlayCollisionEffect();
				PlaySound(40002, 1);
			}
		}
	}

	public bool OnHitAnotherCar(btVehicleBase otherCar, VehicleCollisionSide eSide)
	{
		Car car = otherCar.m_kUserData as Car;
		if (CarMushroomBounce || CarBounceOver || CarDeath || car.CarMushroomBounce || car.CarBounceOver || car.CarDeath)
		{
			return true;
		}
		car.m_CollideCar = null;
		m_CollideCar = null;
		uint num = car.carState;
		carActor.PlayAni("hitd");
		bool flag = (eSide & VehicleCollisionSide.CS_FRONT) == VehicleCollisionSide.CS_FRONT;
		bool flag2 = (eSide & VehicleCollisionSide.CS_BACK) == VehicleCollisionSide.CS_BACK;
		if (CarIncicible && carState == num)
		{
			return false;
		}
		if (CarScaleBig && carState == num)
		{
			return false;
		}
		if (CarIncicible)
		{
			if (Vector3.Dot(car.transform.position - base.transform.position, base.transform.right) > 0f)
			{
				car.HitByLuckyStar(right: true);
			}
			else
			{
				car.HitByLuckyStar(right: false);
			}
			return true;
		}
		if (car.CarIncicible)
		{
			if (Vector3.Dot(base.transform.position - car.transform.position, car.transform.right) > 0f)
			{
				HitByLuckyStar(right: true);
			}
			else
			{
				HitByLuckyStar(right: false);
			}
			return true;
		}
		if (CarScaleBig)
		{
			if (flag)
			{
				car.PressByBigCar();
				otherCar.SetKeepLiveVehicle(vehicle);
				return true;
			}
			return false;
		}
		if (car.CarScaleBig)
		{
			if (flag2)
			{
				PressByBigCar();
				otherCar.SetKeepLiveVehicle(vehicle);
				return true;
			}
			return false;
		}
		if (CarThunderCloud && !car.CarThunderCloud)
		{
			m_CollideCar = car;
		}
		else if (car.CarThunderCloud && !CarThunderCloud)
		{
			car.m_CollideCar = this;
		}
		else
		{
			string currentPlatform = AppMain.Instance.GetCurrentPlatform();
			if ((this is HeroCar || car is HeroCar) && m_fLastHitAnotherCarTime < Time.time - 2f)
			{
				m_fLastHitAnotherCarTime = Time.time;
				PlayHitCarEffect(eSide);
			}
		}
		return false;
	}

	[MessageFun(typeof(Car))]
	public new void processMsg(int msg, ref ABMessageData data)
	{
		switch (msg)
		{
		case 1001:
		{
			COM_DATA cOM_DATA = (COM_DATA)data;
			if (cOM_DATA != null)
			{
				carActor.CarDead(0);
			}
			break;
		}
		case 1003:
		{
			COM_DATA cOM_DATA = (COM_DATA)data;
			if (cOM_DATA != null)
			{
				carActor.CarDead(1);
			}
			break;
		}
		case 1004:
		{
			COM_DATA cOM_DATA = (COM_DATA)data;
			if (cOM_DATA != null)
			{
				carActor.CarDead(2);
			}
			break;
		}
		case 1002:
			break;
		}
	}

	protected virtual void CheckBadCondition()
	{
		if (SceneUIManager.Instance.RaceIsStarted())
		{
			if (checkReverseDrive)
			{
				CheckReverseDrive();
			}
			if (checkNoMovement)
			{
				CheckNoMovement();
			}
		}
	}

	protected virtual void CheckReverseDrive()
	{
		if (inReverseTime > 5f)
		{
			inReverseTime = 0f;
			SetVecGlobal(Vector3.zero);
			ABMessageData data = new COM_DATA();
			((COM_DATA)data).iValue = 0;
			((COM_DATA)data).vValue = GetPosition();
			CTMessagePipeline.sendMessage(this, 1004, ref data);
		}
		if (inReverse && (CarState & 0x8000) == 0 && (CarState & 0x20) == 0)
		{
			inReverseTime += Time.deltaTime;
		}
		else
		{
			inReverseTime = 0f;
		}
	}

	protected virtual void CheckNoMovement()
	{
		if (IsSmallMovement())
		{
			SetVecGlobal(Vector3.zero);
			ABMessageData data = new COM_DATA();
			((COM_DATA)data).iValue = 0;
			((COM_DATA)data).vValue = GetPosition();
			CTMessagePipeline.sendMessage(this, 1004, ref data);
		}
	}

	public void SetReverse(bool reverse)
	{
		inReverse = reverse;
	}

	protected virtual bool IsSmallMovement()
	{
		noMoveTime += Time.deltaTime;
		Vector3 position = GetPosition();
		if (InputManager.Instance.CheckHandbrake())
		{
			noMoveTime = 0f;
			numNoMove = 0;
			lastPos = position;
		}
		if (!InputManager.Instance.CheckHandbrake() && !InputManager.Instance.IsAccelerate())
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

	public void RestoreNormalSpeed()
	{
		normalSpeed = vehicle.GetPhyData().fMaxSpeed;
	}

	public float GetMaxSteeringAngle(float speed)
	{
		return vehicle.GetMaxSteeringAngle(speed);
	}

	public float GetMinimalCurveRadius(float speed)
	{
		return vehicle.GetMinimalCurveRadius(speed);
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
		if (cHead == null)
		{
			Transform transform = base.gameObject.transform.FindChild("collision/bodyhead");
			if (transform != null)
			{
				cHead = transform;
			}
		}
		if (cTail == null)
		{
			Transform transform2 = base.gameObject.transform.FindChild("collision/bodytail");
			if (transform2 != null)
			{
				cTail = transform2;
			}
		}
		if (cBody == null)
		{
			Transform transform3 = base.gameObject.transform.FindChild("collision/bodybox");
			if (transform3 != null)
			{
				cBody = transform3;
			}
		}
		if (bIgnore)
		{
			cHead.gameObject.layer = 24;
			cTail.gameObject.layer = 24;
			cBody.gameObject.layer = 25;
		}
		else
		{
			cHead.gameObject.layer = 19;
			cTail.gameObject.layer = 19;
			cBody.gameObject.layer = 10;
		}
	}

	public void DropProp(bool drop)
	{
		if (carActor != null)
		{
			carActor.DropProp(drop);
		}
	}

	public Role GetRole(int idx)
	{
		if (carActor != null)
		{
			return carActor.GetRole(idx);
		}
		return null;
	}

	public void PlayAniOnly(string name)
	{
		if (carActor != null)
		{
			carActor.PlayAniOnly(name);
		}
	}

	public void StopAniOnly()
	{
		if (carActor != null)
		{
			carActor.StopAniOnly();
		}
	}

	public void SetPlayerState(PlayerState state)
	{
		if (carActor != null)
		{
			carActor.SetPlayerState(state);
		}
		if (state == PlayerState.STATE_TURN_DOWN && m_PlayerState != state)
		{
			m_PlayerState = state;
			if (carParticles != null)
			{
				carParticles.EnableBackEffect(bBack: true);
			}
		}
	}

	public void ResetPlayerState()
	{
		if (carActor != null)
		{
			carActor.ResetPlayerState();
		}
		if (carParticles != null)
		{
			carParticles.EnableBackEffect(bBack: false);
		}
		m_PlayerState = carActor.GetPlayerState();
	}

	public PlayerState GetPlayerState()
	{
		if (carActor != null)
		{
			return carActor.GetPlayerState();
		}
		return PlayerState.STATE_IDEL;
	}

	public void PickProp(ePropType type)
	{
		if (carActor != null)
		{
			carActor.PickProp(type);
		}
	}

	public void PushRole(Role role)
	{
		if (role != null)
		{
			role.m_Car = this;
		}
		if (carActor != null)
		{
			carActor.PushRole(role);
		}
	}

	public void LockCarActor()
	{
		if (carActor != null)
		{
			carActor.LockCarActor();
		}
	}

	public void AttachAIProfile(PlayerAIProfile profile)
	{
		if (carActor != null)
		{
			carActor.AttachAIProfile(profile);
		}
	}

	public void UnLockCarActor()
	{
		if (carActor != null)
		{
			carActor.UnLockCarActor();
		}
	}

	public void HitByTortoise()
	{
		if (AppMain.Instance.IsOnlineMode && isHero)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToUpBound);
		}
		if (carActor != null)
		{
			carActor.HitByTortoise();
		}
	}

	public void HitByLuckyStar(bool right)
	{
		if (AppMain.Instance.IsOnlineMode && isHero)
		{
			if (right)
			{
				SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToRightBound);
			}
			else
			{
				SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToLeftBound);
			}
		}
		if (carActor != null)
		{
			carActor.HitByLuckyStar(right);
		}
	}

	public void UseThunderCloud()
	{
		if (carActor != null)
		{
			carActor.UseThunderCloud();
		}
	}

	public void DisappearThunderCloud()
	{
		m_Thunder = null;
		if (carActor != null)
		{
			carActor.DisappearThunderCloud();
		}
	}

	public void AccelerateCar(float time)
	{
		m_AccelerateTime = time;
		if (carActor != null)
		{
			carActor.AccelerateCar();
		}
	}

	public void InvincibleCar()
	{
		if (carActor != null)
		{
			carActor.InvincibleCar();
		}
	}

	public void PlayAni(string name)
	{
		if (carActor != null)
		{
			carActor.PlayAni(name);
		}
	}

	public void RecoverCarFromMissile()
	{
		if (carActor != null)
		{
			carActor.RecoverCarFromMissile();
		}
	}

	public void HitByMissile(bool right)
	{
		if (AppMain.Instance.IsOnlineMode && isHero)
		{
			if (right)
			{
				SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToRightBound);
			}
			else
			{
				SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToLeftBound);
			}
		}
		if (carActor != null)
		{
			carActor.HitByMissile(right);
		}
	}

	public void BecomeMissile()
	{
		if (carActor != null)
		{
			carActor.BecomeMissile();
		}
	}

	public void HitByLightning()
	{
		if (AppMain.Instance.IsOnlineMode && isHero)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToScaleSmaller);
		}
		if (carActor != null)
		{
			carActor.HitByLightning();
		}
	}

	public void QuakeCar()
	{
		if (carActor != null)
		{
			carActor.QuakeCar();
		}
	}

	public void HitByCuttleFish()
	{
		if (carActor != null)
		{
			carActor.HitByCuttleFish();
		}
	}

	public void HitByCheatBox()
	{
		if (AppMain.Instance.IsOnlineMode && isHero)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToUpBound);
		}
		if (carActor != null)
		{
			carActor.HitByCheatBox();
		}
	}

	public void HitByBomb()
	{
		if (AppMain.Instance.IsOnlineMode && isHero)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToBigBound);
		}
		if (carActor != null)
		{
			carActor.HitByBomb();
		}
	}

	public void PressByBigCar()
	{
		if (AppMain.Instance.IsOnlineMode)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToScaleFlat);
		}
		if (carActor != null)
		{
			carActor.PressByBigCar();
		}
	}

	public void PressByTrap()
	{
		if (AppMain.Instance.IsOnlineMode)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToScaleFlat);
		}
		if (carActor != null)
		{
			carActor.PressByBigCar();
			SpeedPulse(0.2f, GetForwardGlobal(), 100f);
		}
	}

	public void UseBigMushroom()
	{
		if (carActor != null)
		{
			carActor.UseBigMushroom();
		}
	}

	public void HitByBanana()
	{
		if (AppMain.Instance.IsOnlineMode && isHero)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToRotate);
		}
		if (carActor != null)
		{
			carActor.HitByBanana();
		}
	}

	public bool CanDrive()
	{
		if (carActor != null)
		{
			return carActor.CanDrive();
		}
		return false;
	}

	public void HitByRobb(Vector2 direction)
	{
		if (AppMain.Instance.IsOnlineMode)
		{
			SendMessageHandler.Instance.BroadCastCarHitByRobb(this, direction);
		}
		if (carActor != null)
		{
			m_ForwardHitByRobb = direction;
			carActor.HitByRobb();
		}
	}

	public void BackFlip()
	{
		if (AppMain.Instance.IsOnlineMode)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToBackFlip);
		}
		if (carActor != null)
		{
			carActor.BackFlip();
		}
	}

	public void BackFlipDeputy()
	{
		if (AppMain.Instance.IsOnlineMode && isHero)
		{
			SendMessageHandler.Instance.BroadCastCarState(this, EventType.ToBackFlip);
		}
		if (carActor != null)
		{
			carActor.BackFlip();
		}
	}

	public void HitSimpleBlocker()
	{
		if (CarIncicible || CarScaleBig)
		{
			vehicle.ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
			Vector3 vecGlobal = new Vector3(0f, -20f, 0f);
			vehicle.SetVecGlobal(vecGlobal);
		}
		else
		{
			vehicle.ChangeVehicleRunningMode(RUNNING_MDOE.RM_GROUNDRUNING, null);
			BackFlipDeputy();
		}
	}

	public void CarDead(int mode)
	{
		if (carActor != null)
		{
			carActor.CarDead(mode);
		}
	}

	public bool CheckSpeedUpCondition()
	{
		if ((carState & 0x20) != 0)
		{
			return false;
		}
		if ((carState & 0x8000) != 0)
		{
			return false;
		}
		return true;
	}

	public void BounceCar(float horizeSpeed, float horizonCampingCoefficient, float verticalSpeed, float verticalCampingCoefficient, float verticalCampingCoefficient_down)
	{
		m_VeticalSpeed = verticalSpeed;
		m_HorizonSpeed = horizeSpeed;
		m_HorizonDampingCoefficient = horizonCampingCoefficient;
		m_VeticalDampingCoefficient = verticalCampingCoefficient;
		m_VeticalDampingCoefficient_Down = verticalCampingCoefficient_down;
		if (carActor != null)
		{
			carActor.BounceCar();
		}
	}
}
