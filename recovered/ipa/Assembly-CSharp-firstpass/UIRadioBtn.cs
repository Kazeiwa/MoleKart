using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Radio Button")]
public class UIRadioBtn : AutoSpriteControlBase, IRadioButton
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

	protected bool stateChangeWhileDeactivated;

	[HideInInspector]
	public TextureAnim[] states;

	[HideInInspector]
	public string[] stateLabels;

	[HideInInspector]
	public EZTransitionList[] transitions;

	private EZTransition prevTransition;

	public SpriteRoot[] layers;

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvoke;

	public POINTER_INFO.INPUT_EVENT whenToInvoke;

	public float delay;

	public AudioSource soundToPlay;

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

	public override CSpriteFrame DefaultFrame
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override TextureAnim DefaultState
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override string GetStateLabel(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void SetStateLabel(int index, string label)
	{
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

	public override void OnDestroy()
	{
	}

	public void SetGroup(GameObject parent)
	{
	}

	public void SetGroup(Transform parent)
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

	protected void PopOtherButtonsInGroup()
	{
	}

	protected void SetButtonState()
	{
	}

	protected void StartTransition(int newState, int prevState)
	{
	}

	protected override void OnEnable()
	{
	}

	protected void DisableMe()
	{
	}

	public override void InitUVs()
	{
	}

	public override void DrawPreTransitionUI(int selState, IGUIScriptSelector gui)
	{
	}

	public static UIRadioBtn Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIRadioBtn Create(string name, Vector3 pos, Quaternion rotation)
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
