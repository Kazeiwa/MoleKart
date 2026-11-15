using UnityEngine;

public class DriveManager
{
	private static DriveManager instance;

	private bool usingAcceleration;

	private bool usingGUIControl;

	private bool usingDrift;

	private Vector2[] slideTouchVec = new Vector2[10];

	private Vector2[] slideTouchOriVec = new Vector2[10];

	private bool isSliding;

	private CMD_TYPE btnType = CMD_TYPE.NONE;

	private Rect breakRect;

	private Rect shiftRect;

	private int sliderFingerID = -1;

	private bool canDrive = true;

	public static DriveManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new DriveManager();
				instance.SetRect();
			}
			return instance;
		}
	}

	public void Reset()
	{
		for (int i = 0; i < slideTouchVec.Length; i++)
		{
			ref Vector2 reference = ref slideTouchVec[i];
			reference = Vector2.zero;
			ref Vector2 reference2 = ref slideTouchOriVec[i];
			reference2 = Vector2.zero;
		}
		isSliding = false;
		btnType = CMD_TYPE.NONE;
		sliderFingerID = -1;
		canDrive = false;
		InputManager.Instance.CurrentSliding = 0f;
		InputManager.Instance.ResetBoostCheck();
		InputManager.Instance.CurrentSteering = 0f;
	}

	private void SetRect()
	{
		string currentPlatform = AppMain.Instance.GetCurrentPlatform();
		if (currentPlatform == "iphone")
		{
			breakRect = new Rect(847f, 531f, 85f, 85f);
			shiftRect = new Rect(847f, 419f, 86f, 86f);
		}
		else
		{
			breakRect = new Rect(903f, 647f, 85f, 85f);
			shiftRect = new Rect(903f, 519f, 86f, 86f);
		}
	}

	public void InitBreakRectangle(Rect bRect, Rect sRect)
	{
		breakRect = bRect;
		shiftRect = sRect;
	}

	public CMD_TYPE BtnType()
	{
		return btnType;
	}

	public void Init()
	{
		UpdateControlSetting();
	}

	private bool CanUseSlider()
	{
		usingGUIControl = InputManager.Instance.ControlType == CONTROL_TYPE.CONTROLTYPE_DAXIA || InputManager.Instance.ControlType == CONTROL_TYPE.CONTROLTYPE_MASTER;
		return usingGUIControl;
	}

	private bool CanUseAcceleration()
	{
		usingAcceleration = InputManager.Instance.ControlType == CONTROL_TYPE.CONTROLTYPE_BABY || InputManager.Instance.ControlType == CONTROL_TYPE.CONTROLTYPE_CHILD;
		return usingAcceleration;
	}

	private bool CanDrift()
	{
		usingDrift = InputManager.Instance.ControlType == CONTROL_TYPE.CONTROLTYPE_CHILD || InputManager.Instance.ControlType == CONTROL_TYPE.CONTROLTYPE_MASTER;
		return usingDrift;
	}

	public void UpdateControlSetting()
	{
		CanUseSlider();
		CanUseAcceleration();
		CanDrift();
	}

	public int GetSlidingFingerID()
	{
		return sliderFingerID;
	}

	private void CheckButtonClick()
	{
		InputManager.Instance.bTouchMode_Break = false;
		InputManager.Instance.bTouchMode_Shift = false;
		btnType = CMD_TYPE.NONE;
		Touch[] touches = Input.touches;
		Vector2[] array = new Vector2[0];
		if (touches != null)
		{
			array = new Vector2[touches.Length];
			for (int i = 0; i < touches.Length; i++)
			{
				ref Vector2 reference = ref array[i];
				reference = touches[i].position;
				array[i].y = (float)Screen.height - array[i].y;
			}
		}
		if (usingAcceleration)
		{
			InputManager.Instance.CurrentSteering = InputManager.Instance.GetAccelerationInput();
		}
		if (usingGUIControl && array.Length == 0)
		{
			InputManager.Instance.CurrentSteering = 0f;
			InputManager.Instance.CurrentSliding = 0f;
			slideTouchVec = new Vector2[10];
			slideTouchOriVec = new Vector2[10];
		}
		Rect rect = new Rect(0f, (float)Screen.height / 2f, (float)Screen.width / 2f, (float)Screen.height / 2f);
		for (int j = 0; j < array.Length; j++)
		{
			if (breakRect.Contains(array[j]))
			{
				btnType = CMD_TYPE.HANDBREAK;
				InputManager.Instance.bTouchMode_Break = true;
			}
			else if (shiftRect.Contains(array[j]) & usingDrift)
			{
				btnType = CMD_TYPE.SHIFT_LEFT;
				InputManager.Instance.bTouchMode_Shift = true;
			}
			else if (!rect.Contains(array[j]))
			{
				Touch touch = touches[j];
				if (touch.fingerId == sliderFingerID)
				{
					switch (touch.phase)
					{
					case TouchPhase.Moved:
					case TouchPhase.Ended:
					case TouchPhase.Canceled:
						sliderFingerID = -1;
						isSliding = false;
						InputManager.Instance.CurrentSteering = 0f;
						InputManager.Instance.CurrentSliding = 0f;
						break;
					}
				}
			}
			else if ((rect.Contains(array[j]) || isSliding) && usingGUIControl)
			{
				Touch touch2 = touches[j];
				if (sliderFingerID == -1)
				{
					sliderFingerID = touch2.fingerId;
				}
				if (sliderFingerID == -1 || sliderFingerID != touch2.fingerId)
				{
					continue;
				}
				ref Vector2 reference2 = ref slideTouchVec[j];
				reference2 = touch2.position;
				switch (touch2.phase)
				{
				case TouchPhase.Began:
				{
					ref Vector2 reference11 = ref slideTouchVec[j];
					reference11 = new Vector2(array[j].x, array[j].y);
					ref Vector2 reference12 = ref slideTouchOriVec[j];
					reference12 = slideTouchVec[j];
					isSliding = true;
					break;
				}
				case TouchPhase.Moved:
					if (!isSliding)
					{
						ref Vector2 reference7 = ref slideTouchVec[j];
						reference7 = new Vector2(array[j].x, array[j].y);
						ref Vector2 reference8 = ref slideTouchOriVec[j];
						reference8 = slideTouchVec[j];
						isSliding = true;
					}
					else if (Mathf.Abs(touch2.position.x - slideTouchOriVec[j].x) >= 45f)
					{
						if (touch2.position.x - slideTouchOriVec[j].x > 0f)
						{
							ref Vector2 reference9 = ref slideTouchOriVec[j];
							reference9 = new Vector2(touch2.position.x - 45f, touch2.position.y);
						}
						else
						{
							ref Vector2 reference10 = ref slideTouchOriVec[j];
							reference10 = new Vector2(touch2.position.x + 45f, touch2.position.y);
						}
					}
					break;
				case TouchPhase.Ended:
				{
					ref Vector2 reference5 = ref slideTouchVec[j];
					reference5 = array[j];
					ref Vector2 reference6 = ref slideTouchOriVec[j];
					reference6 = array[j];
					isSliding = false;
					sliderFingerID = -1;
					break;
				}
				case TouchPhase.Canceled:
				{
					ref Vector2 reference3 = ref slideTouchVec[j];
					reference3 = array[j];
					ref Vector2 reference4 = ref slideTouchOriVec[j];
					reference4 = array[j];
					isSliding = false;
					sliderFingerID = -1;
					break;
				}
				}
				btnType = (CMD_TYPE)3;
				float num = array[j].x - slideTouchOriVec[j].x;
				InputManager.Instance.CurrentSteering = Mathf.Clamp(num / 45f, -1f, 1f);
				InputManager.Instance.CurrentSliding = Mathf.Clamp(num / 45f, -1f, 1f);
			}
			else if (!breakRect.Contains(array[j]) && usingGUIControl)
			{
				InputManager.Instance.CurrentSteering = 0f;
				InputManager.Instance.CurrentSliding = 0f;
				for (int k = 0; k < slideTouchVec.Length; k++)
				{
					ref Vector2 reference13 = ref slideTouchVec[k];
					reference13 = Vector2.zero;
					ref Vector2 reference14 = ref slideTouchOriVec[k];
					reference14 = Vector2.zero;
				}
				sliderFingerID = -1;
			}
		}
	}

	private void CheckKeyBoard()
	{
		btnType = CMD_TYPE.NONE;
		if (InputManager.Instance.CheckHandbrake())
		{
			btnType = CMD_TYPE.HANDBREAK;
		}
	}

	public void Drive()
	{
		if (canDrive)
		{
			UpdateControlSetting();
			CheckButtonClick();
			InputManager.Instance.CheckSwap();
		}
	}

	public void SetCanDrive(bool drive)
	{
		canDrive = drive;
		InputManager.Instance.m_Accelerate = drive;
	}
}
