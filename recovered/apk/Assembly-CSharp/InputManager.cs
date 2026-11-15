using UnityEngine;

public class InputManager
{
	public float throttle;

	public Vector3 dragMultiplier = new Vector3(2f, 5f, 1f);

	public float steer;

	public int iCount;

	public float swapDistance;

	public Vector2 deltaPos;

	public bool swapForward;

	public bool swapBackward;

	public float maxAngle = 20f;

	public bool lockTouch = true;

	private Touch swapTouch;

	private Rect breakRect;

	private Rect driftRect;

	private Rect sliderRect = new Rect(0f, (float)Screen.height / 2f, (float)Screen.width / 2f, (float)Screen.height / 2f);

	private CONTROL_TYPE controlType;

	public GestureChecker gestureChecker = new GestureChecker();

	private ScreenOrientation oritation;

	public bool bTouchMode_Break;

	public bool bTouchMode_TurnLeft;

	public bool bTouchMode_TurnRight;

	public bool bTouchMode_UseProp;

	public bool bTouchMode_Shift;

	private static InputManager instance;

	private Vector3 firstBoostVec = Vector3.zero;

	private bool firstBoostSet;

	private float m_fSteering;

	public bool m_Accelerate = true;

	public CONTROL_TYPE ControlType => controlType;

	public static InputManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new InputManager();
				instance.LoadControlType();
			}
			return instance;
		}
	}

	public float CurrentSteering
	{
		get
		{
			return m_fSteering;
		}
		set
		{
			m_fSteering = value;
		}
	}

	public float CurrentSliding { get; set; }

	public CONTROL_TYPE LoadControlType()
	{
		controlType = LocalUserConfig.Instance.ControlType;
		return controlType;
	}

	public void SaveControlType(CONTROL_TYPE type)
	{
		controlType = type;
		LocalUserConfig.Instance.ControlType = type;
		LocalUserConfig.Instance.SaveDataToDisk();
		DriveManager.Instance.UpdateControlSetting();
	}

	public void InitBreakRectangle(Rect bRect, Rect sRect)
	{
		breakRect = new Rect(bRect.xMin, (float)Screen.height - bRect.height - bRect.yMin, bRect.width, bRect.height);
		driftRect = new Rect(sRect.xMin, (float)Screen.height - sRect.height - sRect.yMin, sRect.width, sRect.height);
		if (gestureChecker != null)
		{
			gestureChecker.breakRect = breakRect;
			gestureChecker.driftRect = driftRect;
			gestureChecker.sliderRect = new Rect(0f, 0f, (float)Screen.width / 2f, (float)Screen.height / 2f);
			float num = (float)Screen.width / 1024f;
			gestureChecker.pauseRect = new Rect(0f, (float)Screen.height - 100f * num, 100f * num, 100f * num);
		}
	}

	public bool CheckSwap()
	{
		if (gestureChecker != null)
		{
			switch (gestureChecker.CheckSwap())
			{
			case 0:
				swapForward = false;
				swapBackward = false;
				return false;
			case 1:
				swapForward = true;
				swapBackward = false;
				return true;
			case 2:
				swapForward = false;
				swapBackward = true;
				return true;
			case 3:
				swapForward = true;
				swapBackward = false;
				return true;
			default:
				return false;
			}
		}
		return false;
	}

	public float GetAccelerationInput()
	{
		Vector3 acceleration = Input.acceleration;
		oritation = Screen.orientation;
		if (oritation == ScreenOrientation.LandscapeLeft)
		{
			float num = (0f - acceleration.normalized.y) * (90f / maxAngle);
			if (num >= 1f)
			{
				return 1f;
			}
			if (num <= -1f)
			{
				return -1f;
			}
			return num;
		}
		if (oritation == ScreenOrientation.LandscapeRight)
		{
			float num2 = (0f - acceleration.normalized.y) * (90f / maxAngle);
			if (num2 >= 1f)
			{
				return 1f;
			}
			if (num2 <= -1f)
			{
				return -1f;
			}
			return num2;
		}
		return 0f;
	}

	public bool CheckBoost()
	{
		if (!firstBoostSet)
		{
			firstBoostVec = Input.acceleration;
			firstBoostSet = true;
		}
		if (Input.acceleration.z - firstBoostVec.z > 0.4f)
		{
			return true;
		}
		return false;
	}

	public void ResetBoostCheck()
	{
		firstBoostSet = false;
		firstBoostVec = Vector3.zero;
	}

	public void GetInput()
	{
		throttle = Input.GetAxis("Vertical");
		steer = Input.GetAxis("Horizontal");
		CheckHandbrake();
	}

	public bool CheckHandbrake()
	{
		return bTouchMode_Break;
	}

	public bool CheckUseProp()
	{
		return swapForward;
	}

	public bool CheckEquipProp()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			return true;
		}
		return false;
	}

	public bool CheckShift()
	{
		return Instance.bTouchMode_Shift;
	}

	public bool Check0()
	{
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			return true;
		}
		return false;
	}

	public bool Check1()
	{
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			return true;
		}
		return false;
	}

	public bool Check2()
	{
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			return true;
		}
		return false;
	}

	public bool Check3()
	{
		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			return true;
		}
		return false;
	}

	public bool Check4()
	{
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			return true;
		}
		return false;
	}

	public bool Check5()
	{
		if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			return true;
		}
		return false;
	}

	public bool Check6()
	{
		if (Input.GetKeyDown(KeyCode.Keypad6))
		{
			return true;
		}
		return false;
	}

	public bool Check7()
	{
		if (Input.GetKeyDown(KeyCode.Keypad7))
		{
			return true;
		}
		return false;
	}

	public bool Check8()
	{
		if (Input.GetKeyDown(KeyCode.Keypad8))
		{
			return true;
		}
		return false;
	}

	public bool Check9()
	{
		if (Input.GetKeyDown(KeyCode.Keypad9))
		{
			return true;
		}
		return false;
	}

	public bool CheckF1()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			return true;
		}
		return false;
	}

	public bool CheckF2()
	{
		if (Input.GetKeyDown(KeyCode.F2))
		{
			return true;
		}
		return false;
	}

	public bool CheckF3()
	{
		if (Input.GetKeyDown(KeyCode.F3))
		{
			return true;
		}
		return false;
	}

	public bool CheckF4()
	{
		if (Input.GetKeyDown(KeyCode.F4))
		{
			return true;
		}
		return false;
	}

	public bool CheckF5()
	{
		if (Input.GetKeyDown(KeyCode.F5))
		{
			return true;
		}
		return false;
	}

	public bool CheckF6()
	{
		if (Input.GetKeyDown(KeyCode.F6))
		{
			return true;
		}
		return false;
	}

	public bool CheckF7()
	{
		if (Input.GetKeyDown(KeyCode.F7))
		{
			return true;
		}
		return false;
	}

	public bool CheckF8()
	{
		if (Input.GetKeyDown(KeyCode.F8))
		{
			return true;
		}
		return false;
	}

	public bool CheckI()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			return true;
		}
		return false;
	}

	public bool CheckEsc()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			return true;
		}
		return false;
	}

	public bool CheckBackwardUseProp()
	{
		return swapBackward;
	}

	public bool IsTurnRight()
	{
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			return true;
		}
		return false;
	}

	public bool IsTurnLeft()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			return true;
		}
		return false;
	}

	private void CaculateSteering(float fTicktime, bool bSteerLeft, bool bSteerRight)
	{
		float num = 3f;
		if (bSteerRight && !bSteerLeft)
		{
			m_fSteering += fTicktime * num;
			if (m_fSteering > 1f)
			{
				m_fSteering = 1f;
			}
		}
		else if (bSteerLeft && !bSteerRight)
		{
			m_fSteering -= fTicktime * num;
			if (m_fSteering < -1f)
			{
				m_fSteering = -1f;
			}
		}
		else
		{
			SpringZeroSteering(fTicktime);
		}
	}

	private void SpringZeroSteering(float fTicktime)
	{
		float num = (0f - m_fSteering) * fTicktime * 5f;
		m_fSteering += num;
		if (Mathf.Abs(m_fSteering) < 0.01f)
		{
			m_fSteering = 0f;
		}
	}

	public void Update()
	{
	}

	public bool IsAccelerate()
	{
		return m_Accelerate;
	}

	public Vector2[] GetTouchPoints()
	{
		int touchCount = Input.touchCount;
		Vector2[] array = new Vector2[touchCount];
		for (int i = 0; i < touchCount; i++)
		{
			ref Vector2 reference = ref array[i];
			reference = Input.GetTouch(i).position;
			array[i].y = (float)Screen.height - array[i].y;
		}
		return array;
	}
}
