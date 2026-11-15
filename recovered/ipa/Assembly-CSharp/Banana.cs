using UnityEngine;

public class Banana : Prop
{
	private bool m_Throwing;

	private Vector3 m_ThrowDir;

	public Car m_FollowCar;

	public float m_FollowDis;

	public float BANANA_LIFE_TIME;

	private float m_ThrowSpeed;

	private float m_AnimateTime;

	private bool m_AnimPlayed;

	private Transform m_Fx_Banana;

	private Transform m_Pro01;

	public int FollowOrder;

	private Transform child;

	private Vector3 orgPosition;

	private Vector3 orgRotation;

	private Vector3 orgScale;

	public override bool IsPickable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override bool IsDangerous
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override bool IsMoving
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	private void Start()
	{
	}

	public override void Reset()
	{
	}

	protected override void OnTriggerEnter(Collider other)
	{
	}

	public void InitFollowCarPos()
	{
	}

	private void Update()
	{
	}

	protected void PrepareDeleteObject()
	{
	}

	private bool CheckTouchGround()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ThrowFromPos(Vector3 vPos, Vector3 vDir, Vector3 right, float fRelationspeed, bool forward)
	{
	}

	public void DropAtPos(Vector3 pos, Vector3 vDir, float speed)
	{
	}
}
