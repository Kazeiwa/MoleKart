using UnityEngine;

public class Bomb : Prop
{
	private bool m_Throwing;

	private float m_ExplodedTime;

	private Vector3 m_ThrowDir;

	private float m_ThrowSpeed;

	private bool m_Explode;

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

	private void Update()
	{
	}

	private void Explode()
	{
	}

	public void ThrowFromPos(Vector3 vPos, Vector3 vDir, Vector3 right, float fRelationspeed, bool forward)
	{
	}

	protected bool CheckTouchGround()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected override void OnTriggerEnter(Collider other)
	{
	}
}
