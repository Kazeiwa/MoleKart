using System;
using UnityEngine;

[Serializable]
public class CameraControl
{
	[SerializeField]
	private TargetEnum target;

	[SerializeField]
	private float stepSize;

	[SerializeField]
	private MouseCodeEnum mouseCode;

	[SerializeField]
	private KeyCode keyCode;

	public TargetEnum Target
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float StepSize
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public MouseCodeEnum MouseCode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public KeyCode KeyCode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool IsPressed
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float Value
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public CameraControl()
	{
	}

	public CameraControl(TargetEnum argTarget, KeyCode argKeyCode, float argStepSize)
	{
	}

	public CameraControl(TargetEnum argTarget, MouseCodeEnum argMouseCode, float argStepSize)
	{
	}

	public CameraControl(TargetEnum argTarget, KeyCode argKeyCode, MouseCodeEnum argMouseCode, float argStepSize)
	{
	}
}
