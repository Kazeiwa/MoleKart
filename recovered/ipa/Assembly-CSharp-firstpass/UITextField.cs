using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Text Field")]
public class UITextField : AutoSpriteControlBase, IKeyFocusable
{
	public delegate void FocusDelegate(UITextField field);

	[HideInInspector]
	public TextureAnim[] states;

	[HideInInspector]
	public EZTransitionList[] transitions;

	public Vector2 margins;

	protected Rect3D clientClippingRect;

	protected Vector2 marginTopLeft;

	protected Vector2 marginBottomRight;

	public int maxLength;

	public bool multiline;

	public bool password;

	public string maskingCharacter;

	public Vector2 caretSize;

	public ANCHOR_METHOD caretAnchor;

	public Vector3 caretOffset;

	public bool showCaretOnMobile;

	public iPhoneKeyboardType type;

	public bool autoCorrect;

	public bool secure;

	public bool alert;

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvoke;

	protected EZKeyboardCommitDelegate commitDelegate;

	public AudioSource typingSoundEffect;

	public AudioSource fieldFullSound;

	public bool customKeyboard;

	public POINTER_INFO.INPUT_EVENT customFocusEvent;

	protected AutoSprite caret;

	protected FocusDelegate focusDelegate;

	protected FocusDelegate lostfocusDelegate;

	protected int insert;

	protected Vector3 cachedPos;

	protected Quaternion cachedRot;

	protected Vector3 cachedScale;

	protected bool hasFocus;

	protected Vector3 origTextPos;

	protected int[,] stateIndices;

	public override bool controlIsEnabled
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override TextureAnim[] States
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override EZTransitionList[] Transitions
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public string Content
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override IUIContainer Container
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override string Text
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override Rect3D ClippingRect
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override bool Clipped
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override EZTransitionList GetTransitions(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	public override bool GotFocus()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public string GetInputText(ref KEYBOARD_INFO info)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public string SetInputText(string inputText, ref int insertPt)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void LostFocus()
	{
	}

	public void Commit()
	{
	}

	protected void ShowCaret()
	{
	}

	public override void Hide(bool tf)
	{
	}

	protected void HideCaret()
	{
	}

	protected void PositionCaret()
	{
	}

	protected void PositionCaret(bool recur)
	{
	}

	protected void PositionInsertionPoint(Vector3 pt)
	{
	}

	public void SetCommitDelegate(EZKeyboardCommitDelegate del)
	{
	}

	public void AddCommitDelegate(EZKeyboardCommitDelegate del)
	{
	}

	public void RemoveCommitDelegate(EZKeyboardCommitDelegate del)
	{
	}

	public void SetFocusDelegate(FocusDelegate del)
	{
	}

	public void AddFocusDelegate(FocusDelegate del)
	{
	}

	public void RemoveFocusDelegate(FocusDelegate del)
	{
	}

	public void SetLostFocusDelegate(FocusDelegate del)
	{
	}

	public void AddLostFocusDelegate(FocusDelegate del)
	{
	}

	public void RemoveLostFocusDelegate(FocusDelegate del)
	{
	}

	protected override void Awake()
	{
	}

	public override void Start()
	{
	}

	protected void CreateCaret()
	{
	}

	public void CalcClippingRect()
	{
	}

	public void SetMargins(Vector2 marg)
	{
	}

	public override void InitUVs()
	{
	}

	public static UITextField Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UITextField Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Unclip()
	{
	}

	public override void DrawPreTransitionUI(int selState, IGUIScriptSelector gui)
	{
	}

	public override void OnDrawGizmosSelected()
	{
	}

	public override void DoMirror()
	{
	}
}
