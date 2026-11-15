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
		}
	}

	public delegate void PointerPollerDelegate();

	public delegate void PointerInfoDelegate(POINTER_INFO ptr);

	private static UIManager s_Instance;

	public POINTER_TYPE pointerType;

	public float dragThreshold;

	public float rayDragThreshold;

	public float rayDepth;

	public LayerMask rayMask;

	public bool focusWithRay;

	public string actionAxis;

	public OUTSIDE_VIEWPORT inputOutsideViewport;

	public bool warnOnNonUiHits;

	protected Transform raycastingTransform;

	public EZCameraSettings[] uiCameras;

	public Camera rayCamera;

	public bool blockInput;

	public TextAsset defaultFont;

	public Material defaultFontMaterial;

	public bool autoRotateKeyboardPortrait;

	public bool autoRotateKeyboardPortraitUpsideDown;

	public bool autoRotateKeyboardLandscapeLeft;

	public bool autoRotateKeyboardLandscapeRight;

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

	private KEYBOARD_INFO kbInfo;

	protected int inputLockCount;

	private int lastUpdateFrame;

	private int curActionID;

	private int numTouches;

	protected RaycastHit hit;

	protected Vector3 tempVec;

	private bool down;

	private IUIObject tempObj;

	private POINTER_INFO tempPtr;

	private StringBuilder sb;

	private iPhoneKeyboard iKeyboard;

	public static UIManager instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RAY_ACTIVE_STATE RayActive
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public IUIObject FocusObject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public int InsertionPoint
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static bool Exists()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	protected void SetupPointers()
	{
	}

	public void SetNonUIHitDelegate(PointerInfoDelegate del)
	{
	}

	public void AddNonUIHitDelegate(PointerInfoDelegate del)
	{
	}

	public void RemoveNonUIHitDelegate(PointerInfoDelegate del)
	{
	}

	public void AddMouseTouchPtrListener(PointerInfoDelegate del)
	{
	}

	public void AddRayPtrListener(PointerInfoDelegate del)
	{
	}

	public void RemoveMouseTouchPtrListener(PointerInfoDelegate del)
	{
	}

	public void RemoveRayPtrListener(PointerInfoDelegate del)
	{
	}

	protected void AddNonUIHit(int ptrIndex, int camIndex)
	{
	}

	protected void CallNonUIHitDelegate()
	{
	}

	public bool DidPointerHitUI(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AddCamera(Camera cam, LayerMask mask, float depth, int index)
	{
	}

	public void RemoveCamera(int index)
	{
	}

	public void ReplaceCamera(int index, Camera cam)
	{
	}

	public void OnLevelWasLoaded(int level)
	{
	}

	public virtual void Update()
	{
	}

	protected void DispatchInput()
	{
	}

	protected void DispatchHelper(ref POINTER_INFO curPtr, int camIndex)
	{
	}

	protected void PollMouse()
	{
	}

	protected void PollMouseAndTouchpad()
	{
	}

	protected void PollMouse(ref POINTER_INFO curPtr)
	{
	}

	protected void PollTouchpad()
	{
	}

	protected void PollRay()
	{
	}

	protected void PollMouseRay()
	{
	}

	protected void PollTouchpadRay()
	{
	}

	protected void PollKeyboard()
	{
	}

	protected void ProcessKeyboard()
	{
	}

	public void Detarget(IUIObject obj)
	{
	}

	public void LockInput()
	{
	}

	public void UnlockInput()
	{
	}

	protected void LogNonUIObjErr(GameObject obj)
	{
	}
}
