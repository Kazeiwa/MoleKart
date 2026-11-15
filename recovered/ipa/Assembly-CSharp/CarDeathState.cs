using UnityEngine;

public class CarDeathState : FState
{
	private float m_DeathTime;

	private bool m_FadedOut;

	private bool m_Recoverd;

	private bool m_Played;

	private Vector3 vPreVelocity;

	private Vector3 vCarDeadPos;

	public CarDeathState(CarActor actor)
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
