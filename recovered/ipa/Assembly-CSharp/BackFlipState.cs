using UnityEngine;

public class BackFlipState : FState
{
	private float m_FlipTime;

	private Transform m_CarColliderRoot;

	private Vector3 m_vLocalPos;

	private float m_CarSpeed;

	private Vector3 m_CarBackForward;

	private Vector3 m_CarTempPos;

	private bool bStopMove;

	public BackFlipState(CarActor actor)
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
