using UnityEngine;

[MessageClass(typeof(PlayerCar))]
internal class PlayerCar : Car
{
	private Vector3 m_DestPosition;

	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
	}

	public override bool Init(int id, GameObject carEffectRoot)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void Update()
	{
	}

	[MessageFun(typeof(PlayerCar))]
	public new void processMsg(int msg, ref ABMessageData data)
	{
	}

	public override void SetPosition(Vector3 pos)
	{
	}

	public override void UpdatePhysics()
	{
	}
}
