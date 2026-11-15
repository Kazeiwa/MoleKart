using UnityEngine;

public abstract class AutoSpriteControlBase : AutoSpriteBase, IControl, IUIObject
{
	public const string DittoString = "[\"]";

	public bool bIngoreRaycastTest;

	protected bool nullCamera;

	public string text;

	public SpriteText spriteText;

	public float textOffsetZ;

	public bool includeTextInAutoCollider;

	protected SpriteText.Anchor_Pos defaultTextAnchor;

	protected SpriteText.Alignment_Type defaultTextAlignment;

	public bool detargetOnDisable;

	protected bool customCollider;

	protected Vector3 savedColliderSize;

	protected Vector2 topLeftEdge;

	protected Vector2 bottomRightEdge;

	[HideInInspector]
	public object data;

	protected SpriteRoot[][] aggregateLayers;

	protected bool m_controlIsEnabled;

	protected IUIContainer container;

	protected EZInputDelegate inputDelegate;

	protected EZValueChangedDelegate changeDelegate;

	public virtual string Text
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public object Data
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual bool IncludeTextInAutoCollider
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

	public Vector2 TopLeftEdge
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 BottomRightEdge
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual bool controlIsEnabled
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual bool DetargetOnDisable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual IUIContainer Container
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public abstract EZTransitionList[] Transitions { get; set; }

	protected override void Init()
	{
	}

	public override void Start()
	{
	}

	public override void TruncateTop(float pct)
	{
	}

	public override void TruncateBottom(float pct)
	{
	}

	public override void TruncateLeft(float pct)
	{
	}

	public override void TruncateRight(float pct)
	{
	}

	public override void Untruncate()
	{
	}

	public override void Unclip()
	{
	}

	public override void SetCamera(Camera c)
	{
	}

	public override void Hide(bool tf)
	{
	}

	public void Copy(IControl c)
	{
	}

	public void Copy(IControl c, ControlCopyFlags flags)
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public virtual void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	protected override void Awake()
	{
	}

	protected override void OnEnable()
	{
	}

	protected override void OnDisable()
	{
	}

	protected void OnResize(float newWidth, float newHeight, SpriteRoot sprite)
	{
	}

	protected virtual void AddCollider()
	{
	}

	public virtual void UpdateCollider()
	{
	}

	public virtual void FindOuterEdges()
	{
	}

	public bool RequestContainership(IUIContainer cont)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual bool GotFocus()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void SetInputDelegate(EZInputDelegate del)
	{
	}

	public virtual void AddInputDelegate(EZInputDelegate del)
	{
	}

	public virtual void RemoveInputDelegate(EZInputDelegate del)
	{
	}

	public virtual void SetValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	public virtual void AddValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	public virtual void RemoveValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	public virtual void OnInput(POINTER_INFO ptr)
	{
	}

	public virtual void OnInput(ref POINTER_INFO ptr)
	{
	}

	public virtual int DrawPreStateSelectGUI(int selState, bool inspector)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual int DrawPostStateSelectGUI(int selState)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void DrawPreTransitionUI(int selState, IGUIScriptSelector gui)
	{
	}

	public virtual string[] EnumStateElements()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual EZTransitionList GetTransitions(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual string GetStateLabel(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void SetStateLabel(int index, string label)
	{
	}

	public virtual ASCSEInfo GetStateElementInfo(int stateNum)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void UseStateLabel(int index)
	{
	}

	public override void DoMirror()
	{
	}

	virtual GameObject IUIObject.get_gameObject()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual Transform IUIObject.get_transform()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
