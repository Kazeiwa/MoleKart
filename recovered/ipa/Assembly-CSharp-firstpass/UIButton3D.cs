using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/3D Button")]
public class UIButton3D : ControlBase
{
	public enum CONTROL_STATE
	{
		NORMAL,
		OVER,
		ACTIVE,
		DISABLED
	}

	protected CONTROL_STATE m_ctrlState;

	protected string[] states;

	[HideInInspector]
	public EZTransitionList[] transitions;

	private EZTransition prevTransition;

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvoke;

	public POINTER_INFO.INPUT_EVENT whenToInvoke;

	public float delay;

	public AudioSource soundOnOver;

	public AudioSource soundOnClick;

	public bool repeat;

	public CONTROL_STATE controlState
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

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

	public override void Start()
	{
	}

	public override void Copy(IControl c)
	{
	}

	public override void Copy(IControl c, ControlCopyFlags flags)
	{
	}

	protected void SetControlState(CONTROL_STATE s)
	{
	}

	protected void StartTransition(int newState, int prevState)
	{
	}

	public override void DrawPreTransitionUI(int selState, IGUIScriptSelector gui)
	{
	}

	public static UIButton3D Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIButton3D Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
