using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/PictureLabel")]
public class UIPictureLabel : AutoSpriteControlBase
{
	public enum CONTROL_STATE
	{
		NORMAL,
		DISABLED
	}

	protected CONTROL_STATE m_ctrlState;

	[HideInInspector]
	public TextureAnim[] states;

	[HideInInspector]
	public string[] stateLabels;

	[HideInInspector]
	public EZTransitionList[] transitions;

	private EZTransition prevTransition;

	public SpriteRoot[] layers;

	public bool alwaysFinishActiveTransition;

	protected bool transitionQueued;

	protected EZTransition nextTransition;

	protected int[,] stateIndices;

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

	public override string GetStateLabel(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override EZTransitionList GetTransitions(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void SetStateLabel(int index, string label)
	{
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
	}

	public override void Start()
	{
	}

	protected override void OnDisable()
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	public void SetControlState(CONTROL_STATE s)
	{
	}

	public void ForceNormal()
	{
	}

	protected int DetermineNextTransition(int newState, int prevState)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void StartTransition(int newState, int prevState)
	{
	}

	protected void QueueTransition(int newState, int prevState)
	{
	}

	protected void RunFollowupTrans(EZTransition trans)
	{
	}

	public override void InitUVs()
	{
	}

	public static UIPictureLabel Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIPictureLabel Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
