using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Toggle Button")]
public class UIStateToggleBtn : AutoSpriteControlBase
{
	protected int curStateIndex;

	protected bool stateChangeWhileDeactivated;

	public int defaultState;

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

	public int StateNum
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public string StateName
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
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

	protected override void Awake()
	{
	}

	public override void Start()
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	public int ToggleState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetToggleState(int s)
	{
	}

	public void SetToggleState(string stateName)
	{
	}

	protected void DisableMe()
	{
	}

	protected override void OnEnable()
	{
	}

	public override void InitUVs()
	{
	}

	public override int DrawPreStateSelectGUI(int selState, bool inspector)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override int DrawPostStateSelectGUI(int selState)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void DrawPreTransitionUI(int selState, IGUIScriptSelector gui)
	{
	}

	public static UIStateToggleBtn Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIStateToggleBtn Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
