using Misc;
using RaceTrack;
using UnityEngine;

public class Prop : MonoBehaviour, IAgent, IProp
{
	private const float REFRESH_TIME = 5f;

	protected const float DISTANCE_TO_HIDE_2 = 10000f;

	private bool m_IsPicked;

	private float m_SleepTime;

	public bool m_Flying = true;

	protected float m_CreatedTime;

	protected bool m_HitTarget;

	protected bool m_Defended;

	protected float DELTA_TIME = 0.02f;

	protected Renderer[] m_Render;

	protected AudioSource m_Audio;

	protected Vector3 m_InitRight;

	protected float m_DeltaRight;

	private Transform m_Got;

	private Transform m_Pub25;

	protected Camera m_refMainCamera;

	protected bool m_bIsDistanceShow = true;

	protected bool m_bShow = true;

	protected int m_bitUpdateFlag = Random.Range(0, 15);

	private int vehicleLockCount;

	private static float m_LastPlaySound;

	public bool IsPicked => m_IsPicked;

	public int ID { get; set; }

	public Vector3 Position => base.gameObject.transform.position;

	public Vector2 Pos => Vector3Ex.To2D(base.gameObject.transform.position);

	public int VehicleLockCount => vehicleLockCount;

	public virtual bool IsPickable
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public virtual bool IsDangerous
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public virtual bool IsMoving
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public void IncVehicleLockCount()
	{
		vehicleLockCount++;
	}

	public void DecVehicleLockCount()
	{
		if (vehicleLockCount == 0)
		{
			Debug.LogError("vehicleLockCount: the count is 0.");
		}
		vehicleLockCount--;
	}

	private void Start()
	{
		base.gameObject.GetComponentInChildren<Collider>().isTrigger = true;
		base.gameObject.GetComponentInChildren<Renderer>().castShadows = false;
		base.gameObject.GetComponentInChildren<Renderer>().receiveShadows = false;
		m_IsPicked = false;
		m_SleepTime = 0f;
		ID = 0;
		m_Render = base.transform.GetComponentsInChildren<Renderer>();
		m_Got = base.transform.FindChild("got_01");
		if (null != m_Got)
		{
			m_Got.gameObject.SetActiveRecursively(state: false);
		}
		InitDistanceToCamera();
	}

	protected void InitDistanceToCamera()
	{
		if (m_Render == null)
		{
			m_Render = base.transform.GetComponentsInChildren<Renderer>();
		}
		GameObject gameObject = GameObject.Find("MainCamera");
		if ((bool)gameObject)
		{
			m_refMainCamera = gameObject.GetComponent<Camera>();
		}
	}

	protected void UpdateDistanceToCamera()
	{
		if (!(null != m_refMainCamera))
		{
			return;
		}
		if (m_bitUpdateFlag >= 16)
		{
			Vector3 position = base.transform.position;
			if ((position - m_refMainCamera.transform.position).sqrMagnitude > 10000f)
			{
				m_bIsDistanceShow = false;
			}
			else
			{
				m_bIsDistanceShow = true;
			}
			m_bitUpdateFlag = 0;
		}
		else
		{
			m_bitUpdateFlag++;
		}
	}

	protected void UpdateVisible()
	{
		bool bIsDistanceShow = m_bIsDistanceShow;
		if (bIsDistanceShow && !m_bShow)
		{
			SetVisible(isActive: true);
			m_bShow = true;
		}
		else if (!bIsDistanceShow && m_bShow)
		{
			SetVisible(isActive: false);
			m_bShow = false;
		}
	}

	private void Update()
	{
		UpdateDistanceToCamera();
		if (m_IsPicked)
		{
			m_SleepTime += Time.deltaTime;
			if (m_SleepTime >= 5f)
			{
				Show();
				m_IsPicked = false;
				m_SleepTime = 0f;
			}
		}
		UpdateVisible();
	}

	protected bool PlaySound(int id, int priority)
	{
		if (!m_bIsDistanceShow)
		{
			return true;
		}
		if (null == m_Audio)
		{
			m_Audio = SoundManager.Instance.AddAudio(base.gameObject, id);
		}
		if (null != m_Audio)
		{
			SoundManager.Instance.PlayAudio(base.gameObject, m_Audio, priority);
		}
		return true;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 8 || other.gameObject.transform.GetComponent<Tortoise>() != null || m_IsPicked)
		{
			return;
		}
		if (m_bIsDistanceShow)
		{
			if (null != m_Got)
			{
				m_Got.gameObject.SetActiveRecursively(state: true);
			}
			EmitController[] componentsInChildren = base.gameObject.transform.GetComponentsInChildren<EmitController>();
			if (componentsInChildren != null)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].doReset();
				}
			}
		}
		GameObject gameObject = other.gameObject.transform.root.gameObject;
		Car component = gameObject.GetComponent<Car>();
		if (component != null)
		{
			if (Time.time - m_LastPlaySound >= 1f || component.isHero || iPhoneSettings.generation >= iPhoneGeneration.iPad2Gen)
			{
				PlaySound(50001, 1);
				m_LastPlaySound = Time.time;
			}
			Hide();
			m_IsPicked = true;
			PickPropManager.Instance.PickProp(component);
		}
		else if (other.gameObject.transform.GetComponent<Missile>() != null)
		{
			PlaySound(50001, 1);
			Hide();
			m_IsPicked = true;
		}
	}

	protected bool CheckCollidWithMonster(float radius)
	{
		float num = radius * radius;
		foreach (IMonster monster in RaceWorld.Instance.Monsters)
		{
			if ((base.transform.position - monster.Position).sqrMagnitude < num)
			{
				monster.ForceKnockedOut = true;
				m_HitTarget = true;
				return true;
			}
		}
		return false;
	}

	protected bool CheckCollideWithOtherProps(float radius)
	{
		for (int i = 0; i < TSingleton<PropManager>.Instance.Porps.Count; i++)
		{
			Prop prop = TSingleton<PropManager>.Instance.Porps[i] as Prop;
			if (!(null == prop) && !(this == prop) && !prop.m_HitTarget && (prop.transform.position - base.transform.position).magnitude <= radius)
			{
				m_HitTarget = true;
				prop.SetHitTarget(bHit: true);
				return true;
			}
		}
		return false;
	}

	protected bool CheckCollide(Vector3 pos, Vector3 dir, float radius, out Vector3 rebounceDir)
	{
		bool result = false;
		rebounceDir = Vector3.one;
		if (CollisionLayer.Instance.RayCastHitToGuardbar(pos, dir, radius, out var vPos, out var vNormal))
		{
			rebounceDir = vNormal;
			result = true;
		}
		else if (CollisionLayer.Instance.RayCastHitToTree(pos, dir, radius, out vPos, out vNormal))
		{
			rebounceDir = vNormal;
			result = true;
		}
		return result;
	}

	public void IncrementUuid()
	{
	}

	public void SetVisible(bool isActive)
	{
		if (m_Render != null)
		{
			for (int i = 0; i < m_Render.Length; i++)
			{
				m_Render[i].enabled = isActive;
			}
		}
	}

	private void Show()
	{
		if (null == m_Pub25)
		{
			m_Pub25 = base.transform.FindChild("puc_25");
		}
		if (null != m_Pub25)
		{
			m_Pub25.gameObject.SetActiveRecursively(state: true);
		}
	}

	private void Hide()
	{
		if (null == m_Pub25)
		{
			m_Pub25 = base.transform.FindChild("puc_25");
		}
		if (null != m_Pub25)
		{
			m_Pub25.gameObject.SetActiveRecursively(state: false);
		}
	}

	public void SetHitTarget(bool bHit)
	{
		m_HitTarget = bHit;
	}

	public void SetProtected(bool protect)
	{
		m_Defended = protect;
	}
}
