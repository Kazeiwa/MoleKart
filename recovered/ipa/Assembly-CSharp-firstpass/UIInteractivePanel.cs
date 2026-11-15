using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("EZ GUI/Panels/Interactive Panel")]
public class UIInteractivePanel : UIPanelBase
{
	public enum STATE
	{
		NORMAL,
		OVER,
		DRAGGING
	}

	protected STATE m_panelState;

	[HideInInspector]
	public EZTransitionList transitions;

	public bool draggable;

	public bool constrainDragArea;

	public Vector3 dragBoundaryMin;

	public Vector3 dragBoundaryMax;

	public STATE State
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
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

	protected void SetPanelState(STATE s)
	{
	}

	protected void StartTransition(STATE s, STATE prevState)
	{
	}

	public void Hide()
	{
	}

	public void Reveal()
	{
	}

	public static UIInteractivePanel Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIInteractivePanel Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
