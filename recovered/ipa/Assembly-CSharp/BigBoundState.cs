using UnityEngine;

public class BigBoundState : FState
{
	private float m_BoundTime;

	private Transform m_CarColliderRoot;

	private Vector3 m_vLocalPos;

	private float m_CarSpeed;

	private Vector3 m_MoveDirection;

	private Vector3 m_CarForward;

	private Vector3 carRightDir;

	private Vector3 carLeftDir;

	private Vector3 m_TempPos;

	private bool bStopMove;

	public BigBoundState(CarActor actor)
	{
	}

	public override void Entered()
	{
	}

	public override void Update()
	{
	}

	public override void Exited()
	{
	}
}
