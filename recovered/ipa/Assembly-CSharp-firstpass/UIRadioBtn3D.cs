using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/3D Radio Button")]
public class UIRadioBtn3D : ControlBase, IRadioButton
{
	protected enum CONTROL_STATE
	{
		True,
		False,
		Disabled
	}

	private CONTROL_STATE state;

	protected bool btnValue;

	public bool useParentForGrouping;

	public int radioGroup;

	protected RadioBtnGroup group;

	public bool defaultValue;

	protected string[] states;

	[HideInInspector]
	public EZTransitionList[] transitions;

	private EZTransition prevTransition;

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvoke;

	public POINTER_INFO.INPUT_EVENT whenToInvoke;

	public float delay;

	public AudioSource soundToPlay;

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

	public bool Value
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override string[] States
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
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

	public override EZTransitionList GetTransitions(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void OnInput(POINTER_INFO ptr)
	{
	}

	public override void OnDestroy()
	{
	}

	public void SetGroup(GameObject parent)
	{
	}

	public void SetGroup(int groupID)
	{
	}

	protected override void Awake()
	{
	}

	public override void Start()
	{
	}

	public override void Copy(IControl c)
	{
	}

	public override void Copy(IControl c, ControlCopyFlags flags)
	{
	}

	protected void PopOtherButtonsInGroup()
	{
	}

	protected void SetButtonState()
	{
	}

	protected void StartTransition(int newState, int prevState)
	{
	}

	protected void DisableMe()
	{
	}

	public override void DrawPreTransitionUI(int selState, IGUIScriptSelector gui)
	{
	}

	public static UIRadioBtn3D Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIRadioBtn3D Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual string IRadioButton.get_name()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual void IRadioButton.set_name(string value)
	{
	}
}
