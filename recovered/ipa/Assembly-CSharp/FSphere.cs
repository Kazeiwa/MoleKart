using UnityEngine;

public class FSphere : Prop
{
	private bool m_Throwing;

	private float m_ExplodedTime;

	private Vector3 m_ThrowDir;

	private bool m_Explode;

	private GameObject obj_SPHEREHITGROUND;

	private Effect eft_SPHEREHITGROUND;

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

	private void CreateHitEft()
	{
	}

	private void Explode()
	{
	}

	public void ThrowFromPos(Vector3 fromPos, Vector3 destPos, float speed, float time)
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
