using Misc;
using RaceTrack;
using RaceTrack.SteeringBehavior;
using UnityEngine;

[MessageClass(typeof(NPCCar))]
public class NPCCar : Car, INpcCar
{
	public static int steerFlag;

	public int m_steerFlag;

	protected AdvancedSteeringBehavior steeringBehavior;

	private SteeringArgument steering;

	private CrudeElapsedTimer equipeThrowingTimer;

	private CrudeElapsedTimer propThrowingTimer;

	public AdvancedSteeringBehavior SteeringBehavior
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public SteeringArgument Steer
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override bool Init(int id, GameObject carEffectRoot)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
	}

	public override void UpdateCar()
	{
	}

	protected override bool IsSmallMovement()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void DrawGroundEffect()
	{
	}

	[MessageFun(typeof(NPCCar))]
	public new void processMsg(int msg, ref ABMessageData data)
	{
	}

	public override void UpdatePhysics()
	{
	}

	public override void UpdateAISteering()
	{
	}

	public override void InitSteeringBehavior(RacingLine racingLine)
	{
	}

	public new void OnDrawGizmos()
	{
	}

	protected new void OnDrawGizmosSelected()
	{
	}

	public override void Uninit()
	{
	}

	private static float RandomEquipeThrowingInterval()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static float RandomPropThrowingInterval()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void UpdatePropControl()
	{
	}

	virtual float INpcCar.get_MaxTurnScale()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual void INpcCar.set_MaxTurnScale(float value)
	{
	}

	virtual float INpcCar.get_MotorForceScale()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual void INpcCar.set_MotorForceScale(float value)
	{
	}
}
