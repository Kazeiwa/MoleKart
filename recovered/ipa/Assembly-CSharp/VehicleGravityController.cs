using UnityEngine;

public class VehicleGravityController
{
	public enum GravityType
	{
		DefaultGravity,
		NormalBasedGravity,
		CustomGravity
	}

	public const float defaultGravityValue = 10f;

	private GravityType curGravityType;

	private btVehicleBase vehicle;

	public Vector3 defaultGravity;

	public Vector3 curGravity;

	public Vector3 cachedCustomGravity;

	public void ResetGravtiyType()
	{
	}

	public void EnterNormalBasedGravity()
	{
	}

	public void LeaveNormalBasedGravity()
	{
	}

	public void EnterCustomGravity()
	{
	}

	public void SetCustomGravity(Vector3 g)
	{
	}

	public void LeaveCustomGravity()
	{
	}

	private void Start()
	{
	}

	public void InitComponent(btVehicleBase v)
	{
	}

	public void Update()
	{
	}

	private void ApplyGravity()
	{
	}

	public Vector3 GetGravity()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
