using UnityEngine;

public abstract class GameInput
{
	private const float SteerSlowRegion = 0.2f;

	private const float SteerSlowSpeed = 2f;

	private const float SteerFastSpeed = 3.5f;

	private const float SteerBackSpeed = 8f;

	public bool bTouchMode_Break;

	public bool bTouchMode_TurnLeft;

	public bool bTouchMode_TurnRight;

	public bool bTouchMode_UseProp;

	public bool bTouchMode_Shift;

	public bool isAccelerate;

	protected float steering;

	private float sliding;

	protected GestureChecker gestureChecker;

	protected float targetSteer;

	public float CurrentSteering
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual float CurrentSliding
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public GestureChecker GestureChecker
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float CurrentSteeringTarget
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public abstract bool CheckBoost();

	public abstract bool CheckHandbrake();

	public abstract bool CheckUseProp();

	public abstract bool CheckShift();

	public abstract bool CheckBackwardUseProp();

	public abstract bool IsAccelerate();

	public abstract bool IsTurnRight();

	public abstract bool IsTurnLeft();

	public abstract void ResetBoostCheck();

	public abstract void GetInput();

	public abstract bool CheckEquipProp();

	public abstract bool CheckSwap();

	public abstract float GetAccelerationInput();

	public abstract bool Check0();

	public abstract bool Check1();

	public abstract bool Check2();

	public abstract bool Check3();

	public abstract bool Check4();

	public abstract bool Check5();

	public abstract bool Check6();

	public abstract bool Check7();

	public abstract bool Check8();

	public abstract bool Check9();

	public abstract bool CheckF1();

	public abstract bool CheckF2();

	public abstract bool CheckF3();

	public abstract bool CheckF4();

	public abstract bool CheckF5();

	public abstract bool CheckF6();

	public abstract bool CheckF7();

	public abstract bool CheckF8();

	public abstract bool CheckI();

	public abstract bool CheckEsc();

	public void SetSteeringTo(float newValue)
	{
	}

	public void UpdateSteering(float dt)
	{
	}

	private void BackToTarget(float dt, float target)
	{
	}

	private void FowardToTarget(float dt, float target)
	{
	}

	public abstract void InitBreakRectangle(Rect bRect, Rect sRect);

	public abstract void InitTurnRect(Rect lRect, Rect rRect);

	public abstract void InitPauseBtnRect(Rect theRect);

	public abstract Vector2[] GetTouchPoints();

	public abstract void Update();
}
