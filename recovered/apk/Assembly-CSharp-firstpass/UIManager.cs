using System;
using System.Text;
using UnityEngine;

[AddComponentMenu("EZ GUI/Management/UI Manager")]
public class UIManager : MonoBehaviour
{
	public enum POINTER_TYPE
	{
		MOUSE,
		TOUCHPAD,
		AUTO_TOUCHPAD,
		RAY,
		MOUSE_AND_RAY,
		TOUCHPAD_AND_RAY
	}

	public enum RAY_ACTIVE_STATE
	{
		Inactive,
		Momentary,
		Constant
	}

	public enum OUTSIDE_VIEWPORT
	{
		Process_All,
		Ignore,
		Move_Off
	}

	public struct NonUIHitInfo
	{
		public int ptrIndex;

		public int camIndex;

		public NonUIHitInfo(int pIndex, int cIndex)
		{
			ptrIndex = pIndex;
			camIndex = cIndex;
		}
	}

	public delegate void PointerPollerDelegate();

	public delegate void PointerInfoDelegate(POINTER_INFO ptr);

	private static UIManager s_Instance;

	public POINTER_TYPE pointerType;

	public float dragThreshold = 20f;

	public float rayDragThreshold = 2f;

	public float rayDepth = float.PositiveInfinity;

	public LayerMask rayMask = -1;

	public bool focusWithRay;

	public string actionAxis = "Fire1";

	public OUTSIDE_VIEWPORT inputOutsideViewport = OUTSIDE_VIEWPORT.Move_Off;

	public bool warnOnNonUiHits = true;

	protected Transform raycastingTransform;

	public EZCameraSettings[] uiCameras = new EZCameraSettings[1];

	public Camera rayCamera;

	public bool blockInput;

	public TextAsset defaultFont;

	public Material defaultFontMaterial;

	public bool autoRotateKeyboardPortrait = true;

	public bool autoRotateKeyboardPortraitUpsideDown = true;

	public bool autoRotateKeyboardLandscapeLeft = true;

	public bool autoRotateKeyboardLandscapeRight = true;

	protected bool rayActive;

	protected RAY_ACTIVE_STATE rayState;

	protected POINTER_INFO[,] pointers;

	protected NonUIHitInfo[] nonUIHits;

	protected bool[] usedPointers;

	protected bool[] usedNonUIHits;

	protected bool rayIsNonUIHit;

	protected int numPointers;

	protected int numTouchPointers;

	protected int[] activePointers;

	protected int numActivePointers;

	protected int numNonUIHits;

	protected POINTER_INFO rayPtr;

	protected PointerPollerDelegate pointerPoller;

	protected PointerInfoDelegate informNonUIHit;

	protected PointerInfoDelegate mouseTouchListeners;

	protected PointerInfoDelegate rayListeners;

	protected IUIObject focusObj;

	protected string controlText;

	protected int insert;

	private KEYBOARD_INFO kbInfo = default(KEYBOARD_INFO);

	protected int inputLockCount;

	private int lastUpdateFrame;

	private int curActionID;

	private int numTouches;

	protected RaycastHit hit;

	protected Vector3 tempVec;

	private bool down;

	private IUIObject tempObj;

	private POINTER_INFO tempPtr;

	private StringBuilder sb = new StringBuilder();

	private iPhoneKeyboard iKeyboard;

	public static UIManager instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = UnityEngine.Object.FindObjectOfType(typeof(UIManager)) as UIManager;
				if (s_Instance == null && Application.isEditor)
				{
					Debug.LogError("Could not locate a UIManager object. You have to have exactly one UIManager in the scene.");
				}
			}
			return s_Instance;
		}
	}

	public RAY_ACTIVE_STATE RayActive
	{
		get
		{
			return rayState;
		}
		set
		{
			rayState = value;
		}
	}

	public IUIObject FocusObject
	{
		get
		{
			return focusObj;
		}
		set
		{
			if (focusObj != null && focusObj is IKeyFocusable)
			{
				((IKeyFocusable)focusObj).LostFocus();
			}
			focusObj = value;
			if (focusObj != null)
			{
				controlText = ((IKeyFocusable)focusObj).GetInputText(ref kbInfo);
				if (controlText == null)
				{
					controlText = string.Empty;
				}
				if (!Application.isEditor)
				{
					iKeyboard = iPhoneKeyboard.Open(controlText, kbInfo.type, kbInfo.autoCorrect, kbInfo.multiline, kbInfo.secure, kbInfo.alert, controlText);
					iKeyboard.text = controlText;
				}
				insert = kbInfo.insert;
				if (sb.Length > 0)
				{
					sb.Replace(sb.ToString(), controlText);
				}
				else
				{
					sb.Append(controlText);
				}
			}
			else if (iKeyboard != null)
			{
				iKeyboard.active = false;
				iKeyboard = null;
			}
		}
	}

	public int InsertionPoint
	{
		get
		{
			return insert;
		}
		set
		{
			insert = value;
		}
	}

	public static bool Exists()
	{
		return s_Instance != null;
	}

	private void Awake()
	{
		if (s_Instance != null)
		{
			Debug.LogError("You can only have one instance of this singleton object in existence.");
		}
		else
		{
			s_Instance = this;
		}
		if (pointerType == POINTER_TYPE.AUTO_TOUCHPAD && !Application.isEditor)
		{
			pointerType = POINTER_TYPE.TOUCHPAD;
		}
		if (pointerType == POINTER_TYPE.TOUCHPAD || pointerType == POINTER_TYPE.TOUCHPAD_AND_RAY)
		{
			iPhoneKeyboard.autorotateToPortrait = autoRotateKeyboardPortrait;
			iPhoneKeyboard.autorotateToPortraitUpsideDown = autoRotateKeyboardPortraitUpsideDown;
			iPhoneKeyboard.autorotateToLandscapeLeft = autoRotateKeyboardLandscapeLeft;
			iPhoneKeyboard.autorotateToLandscapeRight = autoRotateKeyboardLandscapeRight;
			if (iPhoneSettings.model == "iPad")
			{
				numTouches = 11;
			}
			else
			{
				numTouches = 5;
			}
		}
		else if (pointerType == POINTER_TYPE.AUTO_TOUCHPAD)
		{
			numTouches = 12;
		}
		else if (pointerType == POINTER_TYPE.MOUSE_AND_RAY)
		{
			numTouches = 1;
		}
		else
		{
			numTouches = 1;
		}
		if (pointerType == POINTER_TYPE.AUTO_TOUCHPAD || pointerType == POINTER_TYPE.MOUSE || pointerType == POINTER_TYPE.MOUSE_AND_RAY)
		{
			numTouchPointers = numTouches - 1;
		}
		else
		{
			numTouchPointers = numTouches;
		}
		if (uiCameras.Length < 1)
		{
			uiCameras = new EZCameraSettings[1];
			uiCameras[0].camera = Camera.main;
		}
		else
		{
			for (int i = 0; i < uiCameras.Length; i++)
			{
				if (uiCameras[i].camera == null)
				{
					uiCameras[i].camera = Camera.main;
				}
			}
		}
		if (rayCamera == null)
		{
			rayCamera = uiCameras[0].camera;
		}
	}

	private void Start()
	{
		numPointers = numTouches;
		activePointers = new int[numTouches];
		usedPointers = new bool[numPointers];
		nonUIHits = new NonUIHitInfo[numTouches];
		usedNonUIHits = new bool[numPointers];
		numNonUIHits = 0;
		SetupPointers();
	}

	protected void SetupPointers()
	{
		pointers = new POINTER_INFO[uiCameras.Length, numTouches];
		raycastingTransform = rayCamera.gameObject.transform;
		switch (pointerType)
		{
		case POINTER_TYPE.MOUSE:
		{
			pointerPoller = PollMouse;
			activePointers[0] = 0;
			numActivePointers = 1;
			for (int n = 0; n < uiCameras.Length; n++)
			{
				pointers[n, 0].id = 0;
				pointers[n, 0].rayDepth = uiCameras[n].rayDepth;
				pointers[n, 0].layerMask = uiCameras[n].mask;
				pointers[n, 0].camera = uiCameras[n].camera;
				pointers[n, 0].type = POINTER_INFO.POINTER_TYPE.MOUSE;
			}
			break;
		}
		case POINTER_TYPE.TOUCHPAD:
		{
			pointerPoller = PollTouchpad;
			for (int num = 0; num < uiCameras.Length; num++)
			{
				for (int num2 = 0; num2 < numPointers; num2++)
				{
					pointers[num, num2].id = num2;
					pointers[num, num2].rayDepth = uiCameras[num].rayDepth;
					pointers[num, num2].layerMask = uiCameras[num].mask;
					pointers[num, num2].camera = uiCameras[num].camera;
					pointers[num, num2].type = POINTER_INFO.POINTER_TYPE.TOUCHPAD;
				}
			}
			break;
		}
		case POINTER_TYPE.AUTO_TOUCHPAD:
		{
			pointerPoller = PollMouseAndTouchpad;
			for (int k = 0; k < uiCameras.Length; k++)
			{
				for (int l = 0; l < numPointers; l++)
				{
					pointers[k, l].id = l;
					pointers[k, l].rayDepth = uiCameras[k].rayDepth;
					pointers[k, l].layerMask = uiCameras[k].mask;
					pointers[k, l].camera = uiCameras[k].camera;
					pointers[k, l].type = POINTER_INFO.POINTER_TYPE.TOUCHPAD;
				}
				pointers[k, numPointers - 1].type = POINTER_INFO.POINTER_TYPE.MOUSE;
			}
			break;
		}
		case POINTER_TYPE.RAY:
			pointerPoller = PollRay;
			numActivePointers = 0;
			rayPtr.type = POINTER_INFO.POINTER_TYPE.RAY;
			rayPtr.id = -1;
			rayPtr.rayDepth = rayDepth;
			rayPtr.layerMask = rayMask;
			rayPtr.camera = rayCamera;
			break;
		case POINTER_TYPE.MOUSE_AND_RAY:
		{
			pointerPoller = PollMouseRay;
			activePointers[0] = 0;
			numActivePointers = 1;
			for (int m = 0; m < uiCameras.Length; m++)
			{
				pointers[m, 0].id = 0;
				pointers[m, 0].rayDepth = uiCameras[m].rayDepth;
				pointers[m, 0].layerMask = uiCameras[m].mask;
				pointers[m, 0].camera = uiCameras[m].camera;
				pointers[m, 0].type = POINTER_INFO.POINTER_TYPE.MOUSE;
			}
			rayPtr.id = -1;
			rayPtr.type = POINTER_INFO.POINTER_TYPE.RAY;
			rayPtr.rayDepth = rayDepth;
			rayPtr.layerMask = rayMask;
			rayPtr.camera = rayCamera;
			break;
		}
		case POINTER_TYPE.TOUCHPAD_AND_RAY:
		{
			pointerPoller = PollTouchpadRay;
			for (int i = 0; i < uiCameras.Length; i++)
			{
				for (int j = 0; j < numPointers; j++)
				{
					pointers[i, j].id = j;
					pointers[i, j].rayDepth = uiCameras[i].rayDepth;
					pointers[i, j].layerMask = uiCameras[i].mask;
					pointers[i, j].camera = uiCameras[i].camera;
					pointers[i, j].type = POINTER_INFO.POINTER_TYPE.TOUCHPAD;
				}
			}
			rayPtr.id = -1;
			rayPtr.type = POINTER_INFO.POINTER_TYPE.RAY;
			rayPtr.rayDepth = rayDepth;
			rayPtr.layerMask = rayMask;
			rayPtr.camera = rayCamera;
			break;
		}
		default:
			Debug.LogError("ERROR: Invalid pointer type selected!");
			break;
		}
	}

	public void SetNonUIHitDelegate(PointerInfoDelegate del)
	{
		informNonUIHit = del;
	}

	public void AddNonUIHitDelegate(PointerInfoDelegate del)
	{
		informNonUIHit = (PointerInfoDelegate)Delegate.Combine(informNonUIHit, del);
	}

	public void RemoveNonUIHitDelegate(PointerInfoDelegate del)
	{
		informNonUIHit = (PointerInfoDelegate)Delegate.Remove(informNonUIHit, del);
	}

	public void AddMouseTouchPtrListener(PointerInfoDelegate del)
	{
		mouseTouchListeners = (PointerInfoDelegate)Delegate.Combine(mouseTouchListeners, del);
	}

	public void AddRayPtrListener(PointerInfoDelegate del)
	{
		rayListeners = (PointerInfoDelegate)Delegate.Combine(rayListeners, del);
	}

	public void RemoveMouseTouchPtrListener(PointerInfoDelegate del)
	{
		mouseTouchListeners = (PointerInfoDelegate)Delegate.Remove(mouseTouchListeners, del);
	}

	public void RemoveRayPtrListener(PointerInfoDelegate del)
	{
		rayListeners = (PointerInfoDelegate)Delegate.Remove(rayListeners, del);
	}

	protected void AddNonUIHit(int ptrIndex, int camIndex)
	{
		if (informNonUIHit != null)
		{
			if (camIndex == -1)
			{
				rayIsNonUIHit = true;
			}
			else if (!usedPointers[ptrIndex] && !usedNonUIHits[ptrIndex])
			{
				usedNonUIHits[ptrIndex] = true;
				ref NonUIHitInfo reference = ref nonUIHits[numNonUIHits];
				reference = new NonUIHitInfo(ptrIndex, camIndex);
				numNonUIHits++;
			}
		}
	}

	protected void CallNonUIHitDelegate()
	{
		if (informNonUIHit == null)
		{
			return;
		}
		for (int i = 0; i < numNonUIHits; i++)
		{
			NonUIHitInfo nonUIHitInfo = nonUIHits[i];
			usedNonUIHits[nonUIHitInfo.ptrIndex] = false;
			if (!usedPointers[nonUIHitInfo.ptrIndex])
			{
				informNonUIHit(pointers[nonUIHitInfo.camIndex, nonUIHitInfo.ptrIndex]);
			}
		}
		if (rayIsNonUIHit)
		{
			informNonUIHit(rayPtr);
		}
	}

	public bool DidPointerHitUI(int id)
	{
		if (lastUpdateFrame != Time.frameCount)
		{
			Update();
		}
		if (id == -1)
		{
			return rayPtr.targetObj != null;
		}
		Mathf.Clamp(id, 0, usedPointers.Length - 1);
		return usedPointers[id];
	}

	public void AddCamera(Camera cam, LayerMask mask, float depth, int index)
	{
		EZCameraSettings[] array = new EZCameraSettings[uiCameras.Length + 1];
		index = Mathf.Clamp(index, 0, uiCameras.Length + 1);
		int num = 0;
		int num2 = 0;
		while (num < array.Length)
		{
			if (num == index)
			{
				array[num] = new EZCameraSettings();
				array[num].camera = cam;
				array[num].mask = mask;
				array[num].rayDepth = depth;
				num2++;
			}
			else
			{
				array[num] = uiCameras[num2];
			}
			num++;
			num2++;
		}
		uiCameras = array;
		SetupPointers();
	}

	public void RemoveCamera(int index)
	{
		EZCameraSettings[] array = new EZCameraSettings[uiCameras.Length - 1];
		index = Mathf.Clamp(index, 0, uiCameras.Length);
		int num = 0;
		int num2 = 0;
		while (num < uiCameras.Length)
		{
			if (num == index)
			{
				num2++;
			}
			else
			{
				array[num] = uiCameras[num2];
			}
			num++;
			num2++;
		}
		uiCameras = array;
		SetupPointers();
	}

	public void ReplaceCamera(int index, Camera cam)
	{
		index = Mathf.Clamp(index, 0, uiCameras.Length);
		uiCameras[index].camera = cam;
		SetupPointers();
	}

	public void OnLevelWasLoaded(int level)
	{
		for (int i = 0; i < uiCameras.Length; i++)
		{
			if (uiCameras[i].camera == null)
			{
				uiCameras[i].camera = Camera.main;
			}
		}
		if (rayCamera == null)
		{
			rayCamera = Camera.main;
		}
		if (focusObj == null)
		{
			FocusObject = null;
		}
		blockInput = false;
		inputLockCount = 0;
	}

	public virtual void Update()
	{
		if (lastUpdateFrame != Time.frameCount)
		{
			lastUpdateFrame = Time.frameCount;
			pointerPoller();
			if (focusObj != null)
			{
				PollKeyboard();
			}
			DispatchInput();
		}
	}

	protected void DispatchInput()
	{
		numNonUIHits = 0;
		rayIsNonUIHit = false;
		for (int i = 0; i < usedPointers.Length; i++)
		{
			usedPointers[i] = false;
		}
		if (mouseTouchListeners != null)
		{
			for (int j = 0; j < numActivePointers; j++)
			{
				for (int k = 0; k < uiCameras.Length; k++)
				{
					if (uiCameras[k].camera.gameObject.active)
					{
						DispatchHelper(ref pointers[k, activePointers[j]], k);
						if (mouseTouchListeners != null)
						{
							mouseTouchListeners(pointers[k, activePointers[j]]);
						}
						if (usedPointers[activePointers[j]])
						{
							break;
						}
					}
				}
			}
		}
		else
		{
			for (int l = 0; l < numActivePointers; l++)
			{
				for (int m = 0; m < uiCameras.Length; m++)
				{
					if (uiCameras[m].camera.gameObject.active)
					{
						DispatchHelper(ref pointers[m, activePointers[l]], m);
						if (usedPointers[activePointers[l]])
						{
							break;
						}
					}
				}
			}
		}
		if (pointerType == POINTER_TYPE.RAY || pointerType == POINTER_TYPE.MOUSE_AND_RAY || pointerType == POINTER_TYPE.TOUCHPAD_AND_RAY)
		{
			DispatchHelper(ref rayPtr, -1);
			if (rayListeners != null)
			{
				rayListeners(rayPtr);
			}
		}
		CallNonUIHitDelegate();
	}

	protected void DispatchHelper(ref POINTER_INFO curPtr, int camIndex)
	{
		switch (curPtr.evt)
		{
		case POINTER_INFO.INPUT_EVENT.RELEASE:
		case POINTER_INFO.INPUT_EVENT.TAP:
		case POINTER_INFO.INPUT_EVENT.DRAG:
			if (curPtr.evt == POINTER_INFO.INPUT_EVENT.RELEASE || curPtr.evt == POINTER_INFO.INPUT_EVENT.TAP)
			{
				tempObj = null;
				if (Physics.Raycast(curPtr.ray, out hit, curPtr.rayDepth, curPtr.layerMask))
				{
					tempObj = (IUIObject)hit.collider.gameObject.GetComponent("IUIObject");
					curPtr.hitInfo = hit;
					if (tempObj == null)
					{
						AddNonUIHit(curPtr.id, camIndex);
					}
				}
				else
				{
					curPtr.hitInfo = default(RaycastHit);
				}
				if (tempObj != curPtr.targetObj)
				{
					if (curPtr.targetObj != null)
					{
						tempPtr.Copy(curPtr);
						if (curPtr.evt == POINTER_INFO.INPUT_EVENT.RELEASE)
						{
							tempPtr.evt = POINTER_INFO.INPUT_EVENT.RELEASE_OFF;
						}
						else
						{
							tempPtr.evt = POINTER_INFO.INPUT_EVENT.TAP;
						}
						curPtr.targetObj.OnInput(tempPtr);
						if (curPtr.id >= 0)
						{
							usedPointers[curPtr.id] = true;
						}
						if (!blockInput)
						{
							curPtr.targetObj = tempObj;
						}
					}
					if (tempObj != null && curPtr.evt != POINTER_INFO.INPUT_EVENT.TAP && !blockInput && curPtr.evt != POINTER_INFO.INPUT_EVENT.RELEASE)
					{
						tempObj.OnInput(curPtr);
					}
				}
				else if (curPtr.targetObj != null)
				{
					curPtr.targetObj.OnInput(curPtr);
					if (curPtr.id >= 0)
					{
						usedPointers[curPtr.id] = true;
					}
				}
				if (curPtr.type == POINTER_INFO.POINTER_TYPE.TOUCHPAD)
				{
					curPtr.targetObj = null;
				}
				break;
			}
			if (Physics.Raycast(curPtr.ray, out hit, curPtr.rayDepth, curPtr.layerMask))
			{
				curPtr.hitInfo = hit;
				if (curPtr.targetObj == null)
				{
					AddNonUIHit(curPtr.id, camIndex);
				}
			}
			else
			{
				curPtr.hitInfo = default(RaycastHit);
			}
			if (curPtr.targetObj != null && !blockInput)
			{
				curPtr.targetObj.OnInput(curPtr);
			}
			break;
		case POINTER_INFO.INPUT_EVENT.NO_CHANGE:
		case POINTER_INFO.INPUT_EVENT.MOVE:
			tempObj = null;
			if (Physics.Raycast(curPtr.ray, out hit, curPtr.rayDepth, curPtr.layerMask))
			{
				tempObj = (IUIObject)hit.collider.gameObject.GetComponent("IUIObject");
				curPtr.hitInfo = hit;
				if (tempObj == null)
				{
					AddNonUIHit(curPtr.id, camIndex);
					if (warnOnNonUiHits)
					{
						LogNonUIObjErr(hit.collider.gameObject);
					}
				}
				if (!curPtr.active)
				{
					if (curPtr.targetObj != tempObj && curPtr.targetObj != null)
					{
						tempPtr.Copy(curPtr);
						tempPtr.evt = POINTER_INFO.INPUT_EVENT.MOVE_OFF;
						if (!blockInput)
						{
							curPtr.targetObj.OnInput(tempPtr);
						}
					}
					if (!blockInput)
					{
						curPtr.targetObj = tempObj;
						if (tempObj != null)
						{
							curPtr.targetObj.OnInput(curPtr);
						}
					}
				}
				else if (curPtr.targetObj != null && !blockInput)
				{
					curPtr.targetObj.OnInput(curPtr);
				}
			}
			else
			{
				curPtr.hitInfo = default(RaycastHit);
				if (curPtr.targetObj != null && !curPtr.active)
				{
					curPtr.evt = POINTER_INFO.INPUT_EVENT.MOVE_OFF;
					curPtr.targetObj.OnInput(curPtr);
				}
				if (!curPtr.active)
				{
					curPtr.targetObj = null;
				}
			}
			break;
		case POINTER_INFO.INPUT_EVENT.PRESS:
			if (Physics.Raycast(curPtr.ray, out hit, curPtr.rayDepth, curPtr.layerMask))
			{
				tempObj = (IUIObject)hit.collider.gameObject.GetComponent("IUIObject");
				if (tempObj == null)
				{
					AddNonUIHit(curPtr.id, camIndex);
					if (warnOnNonUiHits)
					{
						LogNonUIObjErr(hit.collider.gameObject);
					}
				}
				curPtr.hitInfo = hit;
				if (tempObj != curPtr.targetObj && curPtr.targetObj != null)
				{
					tempPtr.Copy(curPtr);
					tempPtr.evt = POINTER_INFO.INPUT_EVENT.MOVE_OFF;
					if (!blockInput)
					{
						curPtr.targetObj.OnInput(tempPtr);
					}
				}
				if (!blockInput)
				{
					curPtr.targetObj = tempObj;
				}
				else
				{
					if (curPtr.targetObj != null)
					{
						tempPtr.Copy(curPtr);
						tempPtr.evt = POINTER_INFO.INPUT_EVENT.RELEASE_OFF;
						curPtr.targetObj.OnInput(tempPtr);
					}
					curPtr.targetObj = null;
				}
				if (curPtr.targetObj != null)
				{
					if (curPtr.targetObj != focusObj && curPtr.type == POINTER_INFO.POINTER_TYPE.RAY == focusWithRay)
					{
						if (curPtr.targetObj.GotFocus())
						{
							FocusObject = curPtr.targetObj;
						}
						else
						{
							FocusObject = null;
						}
					}
					if (!blockInput)
					{
						curPtr.targetObj.OnInput(curPtr);
					}
				}
				else if (curPtr.type == POINTER_INFO.POINTER_TYPE.RAY == focusWithRay)
				{
					FocusObject = null;
				}
			}
			else
			{
				curPtr.hitInfo = default(RaycastHit);
				if (blockInput && curPtr.targetObj != null)
				{
					tempPtr.Copy(curPtr);
					tempPtr.evt = POINTER_INFO.INPUT_EVENT.RELEASE_OFF;
					curPtr.targetObj.OnInput(tempPtr);
				}
				curPtr.targetObj = null;
				if (curPtr.type == POINTER_INFO.POINTER_TYPE.RAY == focusWithRay)
				{
					FocusObject = null;
				}
			}
			break;
		}
		if (curPtr.targetObj != null && curPtr.id >= 0)
		{
			usedPointers[curPtr.id] = true;
		}
	}

	protected void PollMouse()
	{
		PollMouse(ref pointers[0, 0]);
		for (int i = 1; i < uiCameras.Length; i++)
		{
			if (uiCameras[i].camera.gameObject.active)
			{
				pointers[i, 0].Reuse(pointers[0, 0]);
				pointers[i, 0].prevRay = pointers[i, 0].ray;
				pointers[i, 0].ray = uiCameras[i].camera.ScreenPointToRay(pointers[i, 0].devicePos);
			}
		}
	}

	protected void PollMouseAndTouchpad()
	{
		PollTouchpad();
	}

	protected void PollMouse(ref POINTER_INFO curPtr)
	{
		down = Input.GetMouseButton(0);
		if (down && curPtr.active)
		{
			if (Input.mousePosition != curPtr.devicePos)
			{
				curPtr.evt = POINTER_INFO.INPUT_EVENT.DRAG;
				curPtr.inputDelta = Input.mousePosition - curPtr.devicePos;
				curPtr.devicePos = Input.mousePosition;
				if (curPtr.isTap)
				{
					tempVec = curPtr.origPos - curPtr.devicePos;
					if (Mathf.Abs(tempVec.x) > dragThreshold || Mathf.Abs(tempVec.y) > dragThreshold)
					{
						curPtr.isTap = false;
					}
				}
			}
			else
			{
				curPtr.evt = POINTER_INFO.INPUT_EVENT.NO_CHANGE;
				curPtr.inputDelta = Vector3.zero;
			}
		}
		else if (down && !curPtr.active)
		{
			curPtr.Reset(curActionID++);
			curPtr.evt = POINTER_INFO.INPUT_EVENT.PRESS;
			curPtr.active = true;
			curPtr.inputDelta = Input.mousePosition - curPtr.devicePos;
			curPtr.origPos = Input.mousePosition;
			curPtr.isTap = true;
		}
		else if (!down && curPtr.active)
		{
			curPtr.inputDelta = Input.mousePosition - curPtr.devicePos;
			curPtr.devicePos = Input.mousePosition;
			if (curPtr.isTap)
			{
				tempVec = curPtr.origPos - curPtr.devicePos;
				if (Mathf.Abs(tempVec.x) > dragThreshold || Mathf.Abs(tempVec.y) > dragThreshold)
				{
					curPtr.isTap = false;
				}
			}
			if (curPtr.isTap)
			{
				curPtr.evt = POINTER_INFO.INPUT_EVENT.TAP;
			}
			else
			{
				curPtr.evt = POINTER_INFO.INPUT_EVENT.RELEASE;
			}
			curPtr.active = false;
		}
		else if (!down && Input.mousePosition != curPtr.devicePos)
		{
			curPtr.evt = POINTER_INFO.INPUT_EVENT.MOVE;
			curPtr.inputDelta = Input.mousePosition - curPtr.devicePos;
			curPtr.devicePos = Input.mousePosition;
		}
		else
		{
			curPtr.evt = POINTER_INFO.INPUT_EVENT.NO_CHANGE;
			curPtr.inputDelta = Vector3.zero;
		}
		curPtr.devicePos = Input.mousePosition;
		curPtr.prevRay = curPtr.ray;
		curPtr.ray = uiCameras[0].camera.ScreenPointToRay(curPtr.devicePos);
	}

	protected void PollTouchpad()
	{
		numActivePointers = Mathf.Min(numTouches, Input.touchCount);
		for (int i = 0; i < numActivePointers; i++)
		{
			Touch touch = Input.GetTouch(i);
			int num = touch.fingerId;
			if (num >= numTouchPointers)
			{
				num = numTouchPointers - 1;
			}
			activePointers[i] = num;
			switch (touch.phase)
			{
			case TouchPhase.Moved:
				pointers[0, num].evt = POINTER_INFO.INPUT_EVENT.DRAG;
				pointers[0, num].inputDelta = touch.deltaPosition;
				pointers[0, num].devicePos = touch.position;
				if (pointers[0, num].isTap)
				{
					tempVec = pointers[0, num].origPos - pointers[0, num].devicePos;
					if (Mathf.Abs(tempVec.x) > dragThreshold || Mathf.Abs(tempVec.y) > dragThreshold)
					{
						pointers[0, num].isTap = false;
					}
				}
				break;
			case TouchPhase.Began:
				pointers[0, num].Reset(curActionID++);
				pointers[0, num].evt = POINTER_INFO.INPUT_EVENT.PRESS;
				pointers[0, num].active = true;
				pointers[0, num].inputDelta = Vector3.zero;
				pointers[0, num].origPos = touch.position;
				pointers[0, num].isTap = true;
				break;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				if (pointers[0, num].isTap)
				{
					pointers[0, num].evt = POINTER_INFO.INPUT_EVENT.TAP;
				}
				else
				{
					pointers[0, num].evt = POINTER_INFO.INPUT_EVENT.RELEASE;
				}
				pointers[0, num].inputDelta = touch.deltaPosition;
				pointers[0, num].active = false;
				break;
			case TouchPhase.Stationary:
				pointers[0, num].evt = POINTER_INFO.INPUT_EVENT.NO_CHANGE;
				pointers[0, num].inputDelta = Vector3.zero;
				break;
			}
			pointers[0, num].devicePos = touch.position;
			pointers[0, num].prevRay = pointers[0, num].ray;
			pointers[0, num].ray = uiCameras[0].camera.ScreenPointToRay(pointers[0, num].devicePos);
		}
		for (int j = 1; j < uiCameras.Length; j++)
		{
			for (int k = 0; k < numActivePointers; k++)
			{
				int num2 = activePointers[k];
				pointers[j, num2].Reuse(pointers[0, num2]);
				pointers[j, num2].prevRay = pointers[j, num2].ray;
				pointers[j, num2].ray = uiCameras[j].camera.ScreenPointToRay(pointers[j, num2].devicePos);
			}
		}
	}

	protected void PollRay()
	{
		if (actionAxis.Length != 0)
		{
			rayActive = Input.GetButton(actionAxis);
		}
		else
		{
			rayActive = rayState != RAY_ACTIVE_STATE.Inactive;
			if (rayState == RAY_ACTIVE_STATE.Momentary)
			{
				rayState = RAY_ACTIVE_STATE.Inactive;
			}
		}
		if (rayActive && rayPtr.active)
		{
			if (raycastingTransform.forward != rayPtr.ray.direction || raycastingTransform.position != rayPtr.ray.origin)
			{
				rayPtr.evt = POINTER_INFO.INPUT_EVENT.DRAG;
				tempVec = raycastingTransform.position + raycastingTransform.forward * rayDepth;
				rayPtr.inputDelta = tempVec - rayPtr.devicePos;
				rayPtr.devicePos = tempVec;
				if (rayPtr.isTap)
				{
					tempVec = rayPtr.origPos - rayPtr.devicePos;
					if (tempVec.sqrMagnitude > rayDragThreshold * rayDragThreshold)
					{
						rayPtr.isTap = false;
					}
				}
			}
			else
			{
				rayPtr.evt = POINTER_INFO.INPUT_EVENT.NO_CHANGE;
				rayPtr.inputDelta = Vector3.zero;
			}
		}
		else if (rayActive && !rayPtr.active)
		{
			rayPtr.Reset(curActionID++);
			rayPtr.evt = POINTER_INFO.INPUT_EVENT.PRESS;
			rayPtr.active = true;
			rayPtr.origPos = raycastingTransform.position + raycastingTransform.forward * rayDepth;
			rayPtr.inputDelta = rayPtr.origPos - rayPtr.devicePos;
			rayPtr.devicePos = rayPtr.origPos;
			rayPtr.isTap = true;
		}
		else if (!rayActive && rayPtr.active)
		{
			if (rayPtr.isTap)
			{
				rayPtr.evt = POINTER_INFO.INPUT_EVENT.TAP;
			}
			else
			{
				rayPtr.evt = POINTER_INFO.INPUT_EVENT.RELEASE;
			}
			tempVec = raycastingTransform.position + raycastingTransform.forward * rayDepth;
			rayPtr.inputDelta = tempVec - rayPtr.devicePos;
			rayPtr.devicePos = tempVec;
			rayPtr.active = false;
		}
		else if (!rayActive && Input.mousePosition != rayPtr.devicePos)
		{
			rayPtr.evt = POINTER_INFO.INPUT_EVENT.MOVE;
			tempVec = raycastingTransform.position + raycastingTransform.forward * rayDepth;
			rayPtr.inputDelta = tempVec - rayPtr.devicePos;
			rayPtr.devicePos = tempVec;
		}
		else
		{
			rayPtr.evt = POINTER_INFO.INPUT_EVENT.NO_CHANGE;
			rayPtr.inputDelta = Vector3.zero;
		}
		rayPtr.prevRay = rayPtr.ray;
		rayPtr.ray = new Ray(raycastingTransform.position, raycastingTransform.forward);
	}

	protected void PollMouseRay()
	{
		PollMouse();
		PollRay();
	}

	protected void PollTouchpadRay()
	{
		PollTouchpad();
		PollRay();
	}

	protected void PollKeyboard()
	{
		if (!Application.isEditor)
		{
			if (iKeyboard != null)
			{
				if (iKeyboard.done)
				{
					controlText = iKeyboard.text;
					controlText = ((IKeyFocusable)focusObj).SetInputText(controlText, ref insert);
					((IKeyFocusable)focusObj).Commit();
					FocusObject = null;
				}
				else if (!(controlText == iKeyboard.text))
				{
					controlText = iKeyboard.text;
					iKeyboard.text = ((IKeyFocusable)focusObj).SetInputText(controlText, ref insert);
				}
			}
		}
		else
		{
			ProcessKeyboard();
		}
	}

	protected void ProcessKeyboard()
	{
		if (Input.inputString.Length == 0)
		{
			return;
		}
		controlText = ((IKeyFocusable)focusObj).Content;
		insert = Mathf.Clamp(insert, 0, controlText.Length);
		if (sb.Length > 0)
		{
			sb.Replace(sb.ToString(), controlText);
		}
		else
		{
			sb.Append(controlText);
		}
		string inputString = Input.inputString;
		foreach (char c in inputString)
		{
			if (c == '\b')
			{
				insert = Mathf.Max(0, insert - 1);
				if (insert < sb.Length)
				{
					sb.Remove(insert, 1);
				}
			}
			else
			{
				sb.Insert(insert, c);
				insert++;
			}
		}
		controlText = sb.ToString();
		controlText = ((IKeyFocusable)focusObj).SetInputText(controlText, ref insert);
	}

	public void Detarget(IUIObject obj)
	{
		if (uiCameras == null)
		{
			return;
		}
		for (int i = 0; i < numActivePointers; i++)
		{
			for (int j = 0; j < uiCameras.Length; j++)
			{
				if (uiCameras[j].camera != null && uiCameras[j].camera.gameObject.active && pointers[j, activePointers[i]].targetObj != null)
				{
					if (pointers[j, activePointers[i]].targetObj == obj)
					{
						pointers[j, activePointers[i]].targetObj = null;
						return;
					}
					break;
				}
			}
		}
		if (rayPtr.targetObj == obj)
		{
			rayPtr.targetObj = null;
		}
	}

	public void LockInput()
	{
		blockInput = true;
		inputLockCount++;
	}

	public void UnlockInput()
	{
		inputLockCount--;
		if (inputLockCount < 1)
		{
			inputLockCount = 0;
			blockInput = false;
		}
	}

	protected void LogNonUIObjErr(GameObject obj)
	{
		Debug.LogWarning("The UIManager encountered a collider on object \"" + obj.name + "\" that does not not contain a UIObject or derivative component.  Please double-check that this object has the correct layer and components assigned.");
	}
}
