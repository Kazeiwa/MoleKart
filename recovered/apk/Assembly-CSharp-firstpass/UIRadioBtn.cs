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

	public bool useParentForGrouping = true;

	public int radioGroup;

	protected RadioBtnGroup group;

	public bool defaultValue;

	protected bool stateChangeWhileDeactivated;

	[HideInInspector]
	public TextureAnim[] states = new TextureAnim[3]
	{
		new TextureAnim("True"),
		new TextureAnim("False"),
		new TextureAnim("Disabled")
	};

	[HideInInspector]
	public string[] stateLabels = new string[3] { "[\"]", "[\"]", "[\"]" };

	[HideInInspector]
	public EZTransitionList[] transitions = new EZTransitionList[3]
	{
		new EZTransitionList(new EZTransition[2]
		{
			new EZTransition("From False"),
			new EZTransition("From Disabled")
		}),
		new EZTransitionList(new EZTransition[2]
		{
			new EZTransition("From True"),
			new EZTransition("From Disabled")
		}),
		new EZTransitionList(new EZTransition[2]
		{
			new EZTransition("From True"),
			new EZTransition("From False")
		})
	};

	private EZTransition prevTransition;

	public SpriteRoot[] layers = new SpriteRoot[0];

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvoke;

	public POINTER_INFO.INPUT_EVENT whenToInvoke = POINTER_INFO.INPUT_EVENT.TAP;

	public float delay;

	public AudioSource soundToPlay;

	protected int[,] stateIndices;

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
				DisableMe();
			}
			else
			{
				SetButtonState();
			}
		}
	}

	public bool Value
	{
		get
		{
			return btnValue;
		}
		set
		{
			bool flag = btnValue;
			btnValue = value;
			if (btnValue)
			{
				PopOtherButtonsInGroup();
			}
			SetButtonState();
			if (flag != btnValue && changeDelegate != null)
			{
				changeDelegate(this);
			}
		}
	}

	public override TextureAnim[] States
	{
		get
		{
			return states;
		}
		set
		{
			states = value;
		}
	}

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

	public override CSpriteFrame DefaultFrame
	{
		get
		{
			int num = ((!btnValue) ? 1 : 0);
			if (States[num].spriteFrames.Length != 0)
			{
				return States[num].spriteFrames[0];
			}
			return null;
		}
	}

	public override TextureAnim DefaultState
	{
		get
		{
			int num = ((!btnValue) ? 1 : 0);
			return States[num];
		}
	}

	public override string GetStateLabel(int index)
	{
		return stateLabels[index];
	}

	public override void SetStateLabel(int index, string label)
	{
		stateLabels[index] = label;
	}

	public override EZTransitionList GetTransitions(int index)
	{
		if (index >= transitions.Length)
		{
			return null;
		}
		return transitions[index];
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
		if (deleted)
		{
			return;
		}
		if (!m_controlIsEnabled || IsHidden())
		{
			base.OnInput(ref ptr);
			return;
		}
		if (inputDelegate != null)
		{
			inputDelegate(ref ptr);
		}
		if (!m_controlIsEnabled || IsHidden())
		{
			base.OnInput(ref ptr);
			return;
		}
		if (!m_controlIsEnabled || IsHidden())
		{
			base.OnInput(ref ptr);
			return;
		}
		if (ptr.evt == whenToInvoke)
		{
			Value = true;
			if (soundToPlay != null)
			{
				soundToPlay.PlayOneShot(soundToPlay.clip);
			}
			if (scriptWithMethodToInvoke != null)
			{
				scriptWithMethodToInvoke.Invoke(methodToInvoke, delay);
			}
		}
		base.OnInput(ref ptr);
	}

	public override void Copy(SpriteRoot s)
	{
		Copy(s, ControlCopyFlags.All);
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
		base.Copy(s, flags);
		if (!(s is UIRadioBtn))
		{
			return;
		}
		UIRadioBtn uIRadioBtn = (UIRadioBtn)s;
		if ((flags & ControlCopyFlags.State) == ControlCopyFlags.State)
		{
			state = uIRadioBtn.state;
			prevTransition = uIRadioBtn.prevTransition;
			if (Application.isPlaying)
			{
				Value = uIRadioBtn.Value;
			}
		}
		if ((flags & ControlCopyFlags.Settings) == ControlCopyFlags.Settings)
		{
			group = uIRadioBtn.group;
			defaultValue = uIRadioBtn.defaultValue;
		}
		if ((flags & ControlCopyFlags.Invocation) == ControlCopyFlags.Invocation)
		{
			scriptWithMethodToInvoke = uIRadioBtn.scriptWithMethodToInvoke;
			methodToInvoke = uIRadioBtn.methodToInvoke;
			whenToInvoke = uIRadioBtn.whenToInvoke;
			delay = uIRadioBtn.delay;
		}
		if ((flags & ControlCopyFlags.Sound) == ControlCopyFlags.Sound)
		{
			soundToPlay = uIRadioBtn.soundToPlay;
		}
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		if (group != null)
		{
			group.buttons.Remove(this);
			group = null;
		}
	}

	public void SetGroup(GameObject parent)
	{
		SetGroup(parent.transform.GetHashCode());
	}

	public void SetGroup(Transform parent)
	{
		SetGroup(parent.GetHashCode());
	}

	public void SetGroup(int groupID)
	{
		if (group != null)
		{
			group.buttons.Remove(this);
			group = null;
		}
		radioGroup = groupID;
		group = RadioBtnGroup.GetGroup(groupID);
		group.buttons.Add(this);
		if (btnValue)
		{
			PopOtherButtonsInGroup();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		btnValue = defaultValue;
	}

	public override void Start()
	{
		if (m_started)
		{
			return;
		}
		base.Start();
		aggregateLayers = new SpriteRoot[1][];
		aggregateLayers[0] = layers;
		state = ((!controlIsEnabled) ? CONTROL_STATE.Disabled : ((!btnValue) ? CONTROL_STATE.False : CONTROL_STATE.True));
		if (Application.isPlaying)
		{
			transitions[0].list[0].MainSubject = base.gameObject;
			transitions[1].list[0].MainSubject = base.gameObject;
			transitions[2].list[0].MainSubject = base.gameObject;
			transitions[2].list[1].MainSubject = base.gameObject;
			stateIndices = new int[layers.Length, 3];
			int num = ((!btnValue) ? 1 : 0);
			num = ((!m_controlIsEnabled) ? 2 : num);
			for (int i = 0; i < layers.Length; i++)
			{
				if (layers[i] == null)
				{
					Debug.LogError("A null layer sprite was encountered on control \"" + base.name + "\". Please fill in the layer reference, or remove the empty element.");
					continue;
				}
				stateIndices[i, 0] = layers[i].GetStateIndex("true");
				stateIndices[i, 1] = layers[i].GetStateIndex("false");
				stateIndices[i, 2] = layers[i].GetStateIndex("disabled");
				if (stateIndices[i, 0] != -1)
				{
					transitions[0].list[0].AddSubSubject(layers[i].gameObject);
				}
				if (stateIndices[i, 1] != -1)
				{
					transitions[1].list[0].AddSubSubject(layers[i].gameObject);
				}
				if (stateIndices[i, 2] != -1)
				{
					transitions[2].list[0].AddSubSubject(layers[i].gameObject);
					transitions[2].list[1].AddSubSubject(layers[i].gameObject);
				}
				if (stateIndices[i, num] != -1)
				{
					layers[i].SetState(stateIndices[i, num]);
				}
				else
				{
					layers[i].Hide(tf: true);
				}
			}
			if (base.collider == null && !bIngoreRaycastTest)
			{
				AddCollider();
			}
			Value = btnValue;
			if (useParentForGrouping && base.transform.parent != null)
			{
				SetGroup(base.transform.parent.GetHashCode());
			}
			else
			{
				SetGroup(radioGroup);
			}
		}
		if (managed && m_hidden)
		{
			Hide(tf: true);
		}
	}

	protected void PopOtherButtonsInGroup()
	{
		if (group == null)
		{
			return;
		}
		for (int i = 0; i < group.buttons.Count; i++)
		{
			if ((UIRadioBtn)group.buttons[i] != this)
			{
				((UIRadioBtn)group.buttons[i]).Value = false;
			}
		}
	}

	protected void SetButtonState()
	{
		if (base.spriteMesh == null || !m_started)
		{
			return;
		}
		int prevState = (int)state;
		state = ((!controlIsEnabled) ? CONTROL_STATE.Disabled : ((!btnValue) ? CONTROL_STATE.False : CONTROL_STATE.True));
		int num = (int)state;
		if (!base.gameObject.active)
		{
			stateChangeWhileDeactivated = true;
			return;
		}
		SetState(num);
		UseStateLabel(num);
		UpdateCollider();
		for (int i = 0; i < layers.Length; i++)
		{
			if (stateIndices[i, num] != -1)
			{
				layers[i].Hide(tf: false);
				layers[i].SetState(stateIndices[i, num]);
			}
			else
			{
				layers[i].Hide(tf: true);
			}
		}
		if (prevTransition != null)
		{
			prevTransition.StopSafe();
		}
		StartTransition(num, prevState);
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
		}
		transitions[newState].list[num].Start();
		prevTransition = transitions[newState].list[num];
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (stateChangeWhileDeactivated)
		{
			SetButtonState();
			stateChangeWhileDeactivated = false;
		}
	}

	protected void DisableMe()
	{
		SetState(states.Length - 1);
		for (int i = 0; i < layers.Length; i++)
		{
			if (stateIndices[i, states.Length - 1] != -1)
			{
				layers[i].SetState(stateIndices[i, states.Length - 1]);
			}
		}
		if (prevTransition != null)
		{
			prevTransition.StopSafe();
		}
		StartTransition(2, (int)state);
		state = CONTROL_STATE.Disabled;
	}

	public override void InitUVs()
	{
		int num = (m_controlIsEnabled ? ((!defaultValue) ? 1 : 0) : (states.Length - 1));
		if (states[num].spriteFrames.Length != 0)
		{
			frameInfo.Copy(states[num].spriteFrames[0]);
		}
		base.InitUVs();
	}

	public override void DrawPreTransitionUI(int selState, IGUIScriptSelector gui)
	{
		scriptWithMethodToInvoke = gui.DrawScriptSelection(scriptWithMethodToInvoke, ref methodToInvoke);
	}

	public static UIRadioBtn Create(string name, Vector3 pos)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		return (UIRadioBtn)gameObject.AddComponent(typeof(UIRadioBtn));
	}

	public static UIRadioBtn Create(string name, Vector3 pos, Quaternion rotation)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		gameObject.transform.rotation = rotation;
		return (UIRadioBtn)gameObject.AddComponent(typeof(UIRadioBtn));
	}

	virtual string IRadioButton.get_name()
	{
		return base.name;
	}

	virtual void IRadioButton.set_name(string value)
	{
		base.name = value;
	}
}
