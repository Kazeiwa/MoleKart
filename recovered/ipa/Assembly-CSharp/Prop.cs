using System.Runtime.CompilerServices;
using RaceTrack;
using UnityEngine;

public class Prop : MonoBehaviour, IAgent, IProp
{
	private const float REFRESH_TIME = 5f;

	protected const float DISTANCE_TO_HIDE_2 = 10000f;

	private bool m_IsPicked;

	private float m_SleepTime;

	public bool m_Flying;

	public float m_CreatedTime;

	protected bool m_HitTarget;

	protected bool m_Defended;

	protected float DELTA_TIME;

	protected Renderer[] m_Render;

	protected AudioSource m_Audio;

	protected Vector3 m_InitRight;

	protected float m_DeltaRight;

	private Transform m_Got;

	private Transform m_Pub25;

	protected Camera m_refMainCamera;

	protected bool m_bIsDistanceShow;

	protected bool m_bShow;

	protected int m_bitUpdateFlag;

	protected ePropType m_Type;

	private int vehicleLockCount;

	private static float m_LastPlaySound;

	public bool IsPicked
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

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

	public ePropType Type
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

	public Vector2 Pos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int VehicleLockCount
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual bool IsPickable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual bool IsDangerous
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual bool IsMoving
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public void IncVehicleLockCount()
	{
	}

	public void DecVehicleLockCount()
	{
	}

	private void Start()
	{
	}

	public virtual void Reset()
	{
	}

	protected void InitDistanceToCamera()
	{
	}

	protected void UpdateDistanceToCamera()
	{
	}

	protected void UpdateVisible()
	{
	}

	private void Update()
	{
	}

	protected bool PlaySound(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
	}

	protected bool CheckCollidWithMonster(float radius)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected bool CheckCollideWithOtherProps(float radius)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected bool CheckCollide(Vector3 pos, Vector3 dir, float radius, out Vector3 rebounceDir)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void IncrementUuid()
	{
	}

	public void SetVisible(bool isActive)
	{
	}

	private void Show()
	{
	}

	private void Hide()
	{
	}

	public void SetHitTarget(bool bHit)
	{
	}

	public void SetProtected(bool protect)
	{
	}
}
