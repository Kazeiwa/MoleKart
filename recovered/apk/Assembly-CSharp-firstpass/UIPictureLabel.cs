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
	public TextureAnim[] states = new TextureAnim[2]
	{
		new TextureAnim("Normal"),
		new TextureAnim("Disabled")
	};

	[HideInInspector]
	public string[] stateLabels = new string[2] { "[\"]", "[\"]" };

	[HideInInspector]
	public EZTransitionList[] transitions = new EZTransitionList[2]
	{
		new EZTransitionList(new EZTransition[1]
		{
			new EZTransition("From Disabled")
		}),
		new EZTransitionList(new EZTransition[1]
		{
			new EZTransition("From Normal")
		})
	};

	private EZTransition prevTransition;

	public SpriteRoot[] layers = new SpriteRoot[0];

	public bool alwaysFinishActiveTransition;

	protected bool transitionQueued;

	protected EZTransition nextTransition;

	protected int[,] stateIndices;

	public CONTROL_STATE controlState => m_ctrlState;

	public override bool controlIsEnabled
	{
		get
		{
			return m_controlIsEnabled;
		}
		set
		{
			bool flag = m_controlIsEnabled;
			m_controlIsEnabled = value;
			if (!value)
			{
				SetControlState(CONTROL_STATE.DISABLED);
			}
			else if (!flag)
			{
				SetControlState(CONTROL_STATE.NORMAL);
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

	public override string GetStateLabel(int index)
	{
		return stateLabels[index];
	}

	public override EZTransitionList GetTransitions(int index)
	{
		if (index >= transitions.Length)
		{
			return null;
		}
		return transitions[index];
	}

	public override void SetStateLabel(int index, string label)
	{
		stateLabels[index] = label;
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
		if (!deleted)
		{
			base.OnInput(ref ptr);
		}
	}

	public override void Start()
	{
		if (m_started)
		{
			return;
		}
		base.Start();
		if (Application.isPlaying)
		{
			aggregateLayers = new SpriteRoot[1][];
			aggregateLayers[0] = layers;
			transitions[0].list[0].MainSubject = base.gameObject;
			transitions[1].list[0].MainSubject = base.gameObject;
			stateIndices = new int[layers.Length, 2];
			for (int i = 0; i < layers.Length; i++)
			{
				if (layers[i] == null)
				{
					Debug.LogError("A null layer sprite was encountered on control \"" + base.name + "\". Please fill in the layer reference, or remove the empty element.");
					continue;
				}
				stateIndices[i, 0] = layers[i].GetStateIndex("normal");
				stateIndices[i, 1] = layers[i].GetStateIndex("disabled");
				if (stateIndices[i, 0] != -1)
				{
					transitions[0].list[0].AddSubSubject(layers[i].gameObject);
					transitions[0].list[1].AddSubSubject(layers[i].gameObject);
					transitions[0].list[2].AddSubSubject(layers[i].gameObject);
				}
				if (stateIndices[i, 1] != -1)
				{
					transitions[1].list[0].AddSubSubject(layers[i].gameObject);
					transitions[1].list[1].AddSubSubject(layers[i].gameObject);
				}
				if (stateIndices[i, (int)m_ctrlState] != -1)
				{
					layers[i].SetState(stateIndices[i, (int)m_ctrlState]);
				}
				else
				{
					layers[i].Hide(tf: true);
				}
			}
			SetState((int)m_ctrlState);
		}
		if (managed && m_hidden)
		{
			Hide(tf: true);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (transitionQueued)
		{
			nextTransition.RemoveTransitionEndDelegate(RunFollowupTrans);
			transitionQueued = false;
		}
		if (EZAnimator.Exists() && !deleted)
		{
			bool flag = alwaysFinishActiveTransition;
			alwaysFinishActiveTransition = false;
			if (controlState == CONTROL_STATE.DISABLED)
			{
				SetControlState(CONTROL_STATE.DISABLED);
			}
			else
			{
				SetControlState(CONTROL_STATE.NORMAL);
			}
			if (prevTransition != null && prevTransition.IsRunning())
			{
				prevTransition.End();
			}
			alwaysFinishActiveTransition = flag;
		}
		prevTransition = null;
	}

	public override void Copy(SpriteRoot s)
	{
		Copy(s, ControlCopyFlags.All);
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
		base.Copy(s, flags);
		if (s is UIPictureLabel)
		{
		}
	}

	public void SetControlState(CONTROL_STATE s)
	{
		if (m_ctrlState == s)
		{
			return;
		}
		int ctrlState = (int)m_ctrlState;
		m_ctrlState = s;
		if (animations[(int)s].GetFrameCount() > 0)
		{
			SetState((int)s);
		}
		UseStateLabel((int)s);
		if (s == CONTROL_STATE.DISABLED)
		{
			m_controlIsEnabled = false;
		}
		else
		{
			m_controlIsEnabled = true;
		}
		for (int i = 0; i < layers.Length; i++)
		{
			if (stateIndices[i, (int)s] != -1)
			{
				layers[i].Hide(tf: false);
				layers[i].SetState(stateIndices[i, (int)s]);
			}
			else
			{
				layers[i].Hide(tf: true);
			}
		}
	}

	public void ForceNormal()
	{
		CONTROL_STATE cONTROL_STATE = CONTROL_STATE.NORMAL;
		int ctrlState = (int)m_ctrlState;
		m_ctrlState = cONTROL_STATE;
		if (animations[(int)cONTROL_STATE].GetFrameCount() > 0)
		{
			SetState((int)cONTROL_STATE);
		}
		UseStateLabel((int)cONTROL_STATE);
		if (cONTROL_STATE == CONTROL_STATE.DISABLED)
		{
			m_controlIsEnabled = false;
		}
		else
		{
			m_controlIsEnabled = true;
		}
		for (int i = 0; i < layers.Length; i++)
		{
			if (stateIndices[i, (int)cONTROL_STATE] != -1)
			{
				layers[i].Hide(tf: false);
				layers[i].SetState(stateIndices[i, (int)cONTROL_STATE]);
			}
			else
			{
				layers[i].Hide(tf: true);
			}
		}
	}

	protected int DetermineNextTransition(int newState, int prevState)
	{
		int result = 0;
		switch (newState)
		{
		case 0:
			if (prevState == 1)
			{
				result = 1;
			}
			break;
		case 1:
			if (prevState == 0)
			{
				result = 0;
			}
			break;
		}
		return result;
	}

	protected void StartTransition(int newState, int prevState)
	{
		int num = DetermineNextTransition(newState, prevState);
		prevTransition = transitions[newState].list[num];
		prevTransition.Start();
	}

	protected void QueueTransition(int newState, int prevState)
	{
		if (!deleted)
		{
			nextTransition = transitions[newState].list[DetermineNextTransition(newState, prevState)];
			if (!transitionQueued)
			{
				prevTransition.AddTransitionEndDelegate(RunFollowupTrans);
			}
			transitionQueued = true;
		}
	}

	protected void RunFollowupTrans(EZTransition trans)
	{
		if (deleted)
		{
			trans.RemoveTransitionEndDelegate(RunFollowupTrans);
			return;
		}
		prevTransition = nextTransition;
		nextTransition = null;
		trans.RemoveTransitionEndDelegate(RunFollowupTrans);
		transitionQueued = false;
		if (prevTransition != null)
		{
			prevTransition.Start();
		}
	}

	public override void InitUVs()
	{
		if (states[0].spriteFrames.Length != 0)
		{
			frameInfo.Copy(states[0].spriteFrames[0]);
		}
		base.InitUVs();
	}

	public static UIPictureLabel Create(string name, Vector3 pos)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		return (UIPictureLabel)gameObject.AddComponent(typeof(UIPictureLabel));
	}

	public static UIPictureLabel Create(string name, Vector3 pos, Quaternion rotation)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		gameObject.transform.rotation = rotation;
		return (UIPictureLabel)gameObject.AddComponent(typeof(UIPictureLabel));
	}
}
