using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
[AddComponentMenu("EZ GUI/Management/Panel Manager")]
public class UIPanelManager : MonoBehaviour, IUIContainer, IUIObject
{
	public enum SHOW_MODE
	{
		BringInForward,
		BringInBack,
		DismissForward,
		DismissBack
	}

	public enum MENU_DIRECTION
	{
		Forwards,
		Backwards,
		Auto
	}

	protected static UIPanelManager m_instance;

	protected List<UIPanelBase> panels;

	public UIPanelBase initialPanel;

	public bool deactivateAllButInitialAtStart;

	public bool linearNavigation;

	public bool circular;

	public bool advancePastEnd;

	protected UIPanelBase curPanel;

	protected bool m_started;

	protected List<UIPanelBase> breadcrumbs;

	protected bool m_controlIsEnabled;

	protected IUIContainer container;

	protected EZInputDelegate inputDelegate;

	protected EZValueChangedDelegate changeDelegate;

	public static UIPanelManager instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public UIPanelBase CurrentPanel
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual bool controlIsEnabled
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual bool DetargetOnDisable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual IUIContainer Container
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public void AddChild(GameObject go)
	{
	}

	public void RemoveChild(GameObject go)
	{
	}

	public void AddSubject(GameObject go)
	{
	}

	public void RemoveSubject(GameObject go)
	{
	}

	public void MakeChild(GameObject go)
	{
	}

	private void Awake()
	{
	}

	[DebuggerHidden]
	private IEnumerator Start()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected virtual void OnEnable()
	{
	}

	public void ScanChildren()
	{
	}

	public bool MoveForward()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool MoveBack()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void BringIn(UIPanelBase panel, MENU_DIRECTION dir)
	{
	}

	public void BringInImmediate(UIPanelBase panel, MENU_DIRECTION dir)
	{
	}

	public void BringIn(string panelName, MENU_DIRECTION dir)
	{
	}

	public void BringIn(UIPanelBase panel)
	{
	}

	public void BringIn(string panelName)
	{
	}

	public void BringIn(int panelIndex)
	{
	}

	public void BringIn(int panelIndex, MENU_DIRECTION dir)
	{
	}

	public void BringInImmediate(string panelName, MENU_DIRECTION dir)
	{
	}

	public void BringInImmediate(UIPanelBase panel)
	{
	}

	public void BringInImmediate(string panelName)
	{
	}

	public void BringInImmediate(int panelIndex)
	{
	}

	public void BringInImmediate(int panelIndex, MENU_DIRECTION dir)
	{
	}

	public void Dismiss(MENU_DIRECTION dir)
	{
	}

	public void Dismiss()
	{
	}

	public void DismissImmediate(MENU_DIRECTION dir)
	{
	}

	public void DismissImmediate()
	{
	}

	public static void SetLayerRecursively(GameObject go, int layer)
	{
	}

	public bool RequestContainership(IUIContainer cont)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GotFocus()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void SetInputDelegate(EZInputDelegate del)
	{
	}

	public virtual void AddInputDelegate(EZInputDelegate del)
	{
	}

	public virtual void RemoveInputDelegate(EZInputDelegate del)
	{
	}

	public virtual void SetValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	public virtual void AddValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	public virtual void RemoveValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	public virtual void OnInput(POINTER_INFO ptr)
	{
	}

	public static UIPanelManager Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual GameObject IUIObject.get_gameObject()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual Transform IUIObject.get_transform()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
