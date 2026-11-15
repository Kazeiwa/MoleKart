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

	protected string[] states = new string[4] { "Normal", "Over", "Active", "Disabled" };

	[HideInInspector]
	public EZTransitionList[] transitions = new EZTransitionList[4]
	{
		new EZTransitionList(new EZTransition[3]
		{
			new EZTransition("From Over"),
			new EZTransition("From Active"),
			new EZTransition("From Disabled")
		}),
		new EZTransitionList(new EZTransition[2]
		{
			new EZTransition("From Normal"),
			new EZTransition("From Active")
		}),
		new EZTransitionList(new EZTransition[2]
		{
			new EZTransition("From Normal"),
			new EZTransition("From Over")
		}),
		new EZTransitionList(new EZTransition[3]
		{
			new EZTransition("From Normal"),
			new EZTransition("From Over"),
			new EZTransition("From Active")
		})
	};

	private EZTransition prevTransition;

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvoke;

	public POINTER_INFO.INPUT_EVENT whenToInvoke = POINTER_INFO.INPUT_EVENT.TAP;

	public float delay;

	public AudioSource soundOnOver;

	public AudioSource soundOnClick;

	public bool repeat;

	public CONTROL_STATE controlState => m_ctrlState;

	public override bool controlIsEnabled
	{
		get
		{
			return m_controlIsEnabled;
		}
		set
		{
			m_controlIsEnabled = value;
			if (!value)
			{
				SetControlState(CONTROL_STATE.DISABLED);
			}
			else
			{
				SetControlState(CONTROL_STATE.NORMAL);
			}
		}
	}

	public override string[] States => states;

	public override EZTransitionList[] Transitions
	{
		get
		{
			return transitions;
		}
		set
		{
			transitions = value;
		}
	}

	public override EZTransitionList GetTransitions(int index)
	{
		if (index >= transitions.Length)
		{
			return null;
		}
		return transitions[index];
	}

	public override void OnInput(POINTER_INFO ptr)
	{
		if (deleted)
		{
			return;
		}
		if (!m_controlIsEnabled)
		{
			base.OnInput(ptr);
			return;
		}
		if (inputDelegate != null)
		{
			inputDelegate(ref ptr);
		}
		if (!m_controlIsEnabled)
		{
			base.OnInput(ptr);
			return;
		}
		switch (ptr.evt)
		{
		case POINTER_INFO.INPUT_EVENT.MOVE:
			if (m_ctrlState != CONTROL_STATE.OVER)
			{
				SetControlState(CONTROL_STATE.OVER);
				if (soundOnOver != null)
				{
					soundOnOver.PlayOneShot(soundOnOver.clip);
				}
			}
			break;
		case POINTER_INFO.INPUT_EVENT.PRESS:
		case POINTER_INFO.INPUT_EVENT.DRAG:
			SetControlState(CONTROL_STATE.ACTIVE);
			break;
		case POINTER_INFO.INPUT_EVENT.RELEASE:
		case POINTER_INFO.INPUT_EVENT.TAP:
			if (ptr.type != POINTER_INFO.POINTER_TYPE.TOUCHPAD && ptr.hitInfo.collider == base.collider)
			{
				SetControlState(CONTROL_STATE.OVER);
			}
			else
			{
				SetControlState(CONTROL_STATE.NORMAL);
			}
			break;
		case POINTER_INFO.INPUT_EVENT.MOVE_OFF:
		case POINTER_INFO.INPUT_EVENT.RELEASE_OFF:
			SetControlState(CONTROL_STATE.NORMAL);
			break;
		}
		base.OnInput(ptr);
		if (repeat)
		{
			if (m_ctrlState != CONTROL_STATE.ACTIVE)
			{
				return;
			}
		}
		else if (ptr.evt != whenToInvoke)
		{
			return;
		}
		if (ptr.evt == whenToInvoke && soundOnClick != null)
		{
			soundOnClick.PlayOneShot(soundOnClick.clip);
		}
		if (scriptWithMethodToInvoke != null)
		{
			scriptWithMethodToInvoke.Invoke(methodToInvoke, delay);
		}
		if (changeDelegate != null)
		{
			changeDelegate(this);
		}
	}

	public override void Start()
	{
		if (Application.isPlaying)
		{
			transitions[0].list[0].MainSubject = base.gameObject;
			transitions[0].list[1].MainSubject = base.gameObject;
			transitions[0].list[2].MainSubject = base.gameObject;
			transitions[1].list[0].MainSubject = base.gameObject;
			transitions[1].list[1].MainSubject = base.gameObject;
			transitions[2].list[0].MainSubject = base.gameObject;
			transitions[2].list[1].MainSubject = base.gameObject;
			transitions[3].list[0].MainSubject = base.gameObject;
			transitions[3].list[1].MainSubject = base.gameObject;
			transitions[3].list[2].MainSubject = base.gameObject;
			if (base.collider == null)
			{
				AddCollider();
			}
		}
	}

	public override void Copy(IControl c)
	{
		Copy(c, ControlCopyFlags.All);
	}

	public override void Copy(IControl c, ControlCopyFlags flags)
	{
		base.Copy(c, flags);
		if (!(c is UIButton3D))
		{
			return;
		}
		UIButton3D uIButton3D = (UIButton3D)c;
		if ((flags & ControlCopyFlags.State) == ControlCopyFlags.State)
		{
			prevTransition = uIButton3D.prevTransition;
			if (Application.isPlaying)
			{
				SetControlState(uIButton3D.controlState);
			}
		}
		if ((flags & ControlCopyFlags.Invocation) == ControlCopyFlags.Invocation)
		{
			scriptWithMethodToInvoke = uIButton3D.scriptWithMethodToInvoke;
			methodToInvoke = uIButton3D.methodToInvoke;
			whenToInvoke = uIButton3D.whenToInvoke;
			delay = uIButton3D.delay;
		}
		if ((flags & ControlCopyFlags.Sound) == ControlCopyFlags.Sound)
		{
			soundOnOver = uIButton3D.soundOnOver;
			soundOnClick = uIButton3D.soundOnClick;
		}
		if ((flags & ControlCopyFlags.Settings) == ControlCopyFlags.Settings)
		{
			repeat = uIButton3D.repeat;
		}
	}

	protected void SetControlState(CONTROL_STATE s)
	{
		if (m_ctrlState != s)
		{
			int ctrlState = (int)m_ctrlState;
			m_ctrlState = s;
			if (prevTransition != null)
			{
				prevTransition.StopSafe();
			}
			StartTransition((int)s, ctrlState);
		}
	}

	protected void StartTransition(int newState, int prevState)
	{
		int num = 0;
		switch (newState)
		{
		case 0:
			switch (prevState)
			{
			case 1:
				num = 0;
				break;
			case 2:
				num = 1;
				break;
			case 3:
				num = 2;
				break;
			}
			break;
		case 1:
			switch (prevState)
			{
			case 0:
				num = 0;
				break;
			case 2:
				num = 1;
				break;
			}
			break;
		case 2:
			switch (prevState)
			{
			case 0:
				num = 0;
				break;
			case 1:
				num = 1;
				break;
			}
			break;
		case 3:
			switch (prevState)
			{
			case 0:
				num = 0;
				break;
			case 1:
				num = 1;
				break;
			case 2:
				num = 2;
				break;
			}
			break;
		}
		prevTransition = transitions[newState].list[num];
		prevTransition.Start();
	}

	public override void DrawPreTransitionUI(int selState, IGUIScriptSelector gui)
	{
		scriptWithMethodToInvoke = gui.DrawScriptSelection(scriptWithMethodToInvoke, ref methodToInvoke);
	}

	public static UIButton3D Create(string name, Vector3 pos)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		return (UIButton3D)gameObject.AddComponent(typeof(UIButton3D));
	}

	public static UIButton3D Create(string name, Vector3 pos, Quaternion rotation)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		gameObject.transform.rotation = rotation;
		return (UIButton3D)gameObject.AddComponent(typeof(UIButton3D));
	}
}
