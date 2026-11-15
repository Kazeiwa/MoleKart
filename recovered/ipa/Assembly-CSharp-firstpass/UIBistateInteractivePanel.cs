using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("EZ GUI/Panels/Bi-State Interactive Panel")]
public class UIBistateInteractivePanel : UIPanelBase
{
	public enum STATE
	{
		SHOWING,
		HIDDEN
	}

	protected STATE m_panelState;

	[HideInInspector]
	public EZTransitionList transitions;

	public bool requireTap;

	public bool alwaysShowOnClick;

	public bool dismissOnOutsideClick;

	public bool dismissOnPeerClick;

	public bool dismissOnChildClick;

	public bool dismissOnMoveOff;

	public bool showOnChildClick;

	public STATE initialState;

	protected int lastActionID;

	protected POINTER_INFO.POINTER_TYPE lastPtrType;

	protected POINTER_INFO.POINTER_TYPE lastListenerType;

	public STATE State
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override EZTransitionList Transitions
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override void OnInput(POINTER_INFO ptr)
	{
	}

	protected void PanelClicked(POINTER_INFO ptr)
	{
	}

	public void Awake()
	{
	}

	public void ToggleState()
	{
	}

	protected void SetPanelState(STATE s)
	{
	}

	public override void StartTransition(UIPanelManager.SHOW_MODE mode)
	{
	}

	protected void ClickListener(POINTER_INFO ptr)
	{
	}

	public void Hide()
	{
	}

	public void Reveal()
	{
	}

	public static UIBistateInteractivePanel Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIBistateInteractivePanel Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
