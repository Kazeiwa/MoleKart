using UnityEngine;

public abstract class ControlBase : MonoBehaviour, IControl, IUIObject
{
	protected ControlBaseMirror mirror;

	public string text;

	public SpriteText spriteText;

	public float textOffsetZ;

	public bool includeTextInAutoCollider;

	protected SpriteText.Anchor_Pos defaultTextAnchor;

	protected SpriteText.Alignment_Type defaultTextAlignment;

	protected bool deleted;

	public bool detargetOnDisable;

	protected bool customCollider;

	[HideInInspector]
	public object data;

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

	public abstract string[] States { get; }

	public abstract EZTransitionList[] Transitions { get; set; }

	protected virtual void Awake()
	{
	}

	public virtual void Start()
	{
	}

	protected virtual void AddCollider()
	{
	}

	public virtual void UpdateCollider()
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

	public virtual void OnDisable()
	{
	}

	public virtual void OnDestroy()
	{
	}

	public virtual void Copy(IControl ctl)
	{
	}

	public virtual void Copy(IControl ctl, ControlCopyFlags flags)
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

	public abstract EZTransitionList GetTransitions(int index);

	public virtual void DoMirror()
	{
	}

	public virtual void OnDrawGizmos()
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
