using UnityEngine;

public class DriveManager
{
	private const float TURN_SPEED = 2f;

	private const float buttonTurningSpeed = 0.1f;

	private static DriveManager instance;

	private Rect turnLeftRect;

	private Rect turnRightRect;

	private bool usingAcceleration;

	private bool usingGUIControl;

	private bool usingBtnControl;

	private bool usingDrift;

	private Vector2[] slideTouchVec;

	private Vector2[] slideTouchOriVec;

	private bool isSliding;

	private CMD_TYPE btnType;

	private Rect breakRect;

	private Rect shiftRect;

	private int sliderFingerID;

	private bool canDrive;

	private UIButton leftBtn;

	private UIButton rightBtn;

	protected Vector2 resolutionScale;

	private float m_fSteering;

	public static DriveManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Reset()
	{
	}

	private void SetRect()
	{
	}

	public void InitBreakRectangle(Rect bRect, Rect sRect)
	{
	}

	public void InitTurnRect(Rect lRect, Rect rRect)
	{
	}

	public void InitTurnButton(UIButton left, UIButton right)
	{
	}

	public CMD_TYPE BtnType()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Init()
	{
	}

	private bool CanUseSlider()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool CanUseAcceleration()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool CanDrift()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool CanUseBtnControl()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UpdateControlSetting()
	{
	}

	public int GetSlidingFingerID()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void DriveWithSlider()
	{
	}

	private void DriveWithAcceleration()
	{
	}

	private void DriveWithButton()
	{
	}

	private void CalculateSteering(float fTicktime, bool bSteerLeft, bool bSteerRight)
	{
	}

	private void SpringZeroSteering(float fTicktime)
	{
	}

	private bool CheckHandbreak(Vector2 touchPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool CheckDrift(Vector2 touchPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void CheckBreakAndDrift()
	{
	}

	private void CheckButtonClick()
	{
	}

	private void CheckKeyBoard()
	{
	}

	public void Drive()
	{
	}

	public void SetCanDrive(bool drive)
	{
	}
}
