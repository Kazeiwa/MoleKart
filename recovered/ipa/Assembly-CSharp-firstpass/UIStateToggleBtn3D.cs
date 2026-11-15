using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/3D Toggle Button")]
public class UIStateToggleBtn3D : ControlBase
{
	protected int curStateIndex;

	public int defaultState;

	[HideInInspector]
	public string[] states;

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

	public static UIStateToggleBtn3D Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIStateToggleBtn3D Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
