using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class UIPanelBase : MonoBehaviour, IUIContainer, IUIObject
{
	public delegate void TransitionCompleteDelegate(UIPanelBase panel, EZTransition transition);

	protected EZLinkedList<EZLinkedListNode<IUIObject>> uiObjs;

	protected EZLinkedList<EZLinkedListNode<UIPanelBase>> childPanels;

	[HideInInspector]
	public bool[] blockInput;

	protected EZTransition prevTransition;

	protected int prevTransIndex;

	protected bool m_started;

	public int index;

	public bool deactivateAllOnDismiss;

	public bool detargetOnDisable;

	[NonSerialized]
	protected Dictionary<int, GameObject> subjects;

	protected TransitionCompleteDelegate tempTransCompleteDel;

	protected bool m_controlIsEnabled;

	protected IUIContainer container;

	protected EZInputDelegate inputDelegate;

	protected EZValueChangedDelegate changeDelegate;

	public abstract EZTransitionList Transitions { get; }

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

	protected virtual void OnDisable()
	{
	}

	public virtual void Start()
	{
	}

	public void ScanChildren()
	{
	}

	protected virtual void SetupTransitionSubjects()
	{
	}

	public void AddChild(GameObject go)
	{
	}

	public void RemoveChild(GameObject go)
	{
	}

	public void MakeChild(GameObject go)
	{
	}

	public void AddSubject(GameObject go)
	{
	}

	public void RemoveSubject(GameObject go)
	{
	}

	public string[] GetTransitionNames()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public EZTransition GetTransition(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public EZTransition GetTransition(UIPanelManager.SHOW_MODE transition)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public EZTransition GetTransition(string transName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void StartTransition(UIPanelManager.SHOW_MODE mode)
	{
	}

	public virtual void StartTransition(string transName)
	{
	}

	public void TransitionCompleted(EZTransition transition)
	{
	}

	public virtual void BringIn()
	{
	}

	public virtual void Dismiss()
	{
	}

	public static int CompareIndices(UIPanelBase a, UIPanelBase b)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AddTempTransitionDelegate(TransitionCompleteDelegate del)
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

	virtual GameObject IUIObject.get_gameObject()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual Transform IUIObject.get_transform()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
