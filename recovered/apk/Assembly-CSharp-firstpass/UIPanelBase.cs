using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class UIPanelBase : MonoBehaviour, IUIContainer, IUIObject
{
	public delegate void TransitionCompleteDelegate(UIPanelBase panel, EZTransition transition);

	protected EZLinkedList<EZLinkedListNode<IUIObject>> uiObjs = new EZLinkedList<EZLinkedListNode<IUIObject>>();

	protected EZLinkedList<EZLinkedListNode<UIPanelBase>> childPanels = new EZLinkedList<EZLinkedListNode<UIPanelBase>>();

	[HideInInspector]
	public bool[] blockInput = new bool[4] { true, true, true, true };

	protected EZTransition prevTransition;

	protected int prevTransIndex;

	protected bool m_started;

	public int index;

	public bool deactivateAllOnDismiss;

	public bool detargetOnDisable;

	[NonSerialized]
	protected Dictionary<int, GameObject> subjects = new Dictionary<int, GameObject>();

	protected TransitionCompleteDelegate tempTransCompleteDel;

	protected bool m_controlIsEnabled = true;

	protected IUIContainer container;

	protected EZInputDelegate inputDelegate;

	protected EZValueChangedDelegate changeDelegate;

	public abstract EZTransitionList Transitions { get; }

	public virtual bool controlIsEnabled
	{
		get
		{
			return m_controlIsEnabled;
		}
		set
		{
			m_controlIsEnabled = value;
		}
	}

	public virtual bool DetargetOnDisable
	{
		get
		{
			return DetargetOnDisable;
		}
		set
		{
			DetargetOnDisable = value;
		}
	}

	public virtual IUIContainer Container
	{
		get
		{
			return container;
		}
		set
		{
			container = value;
		}
	}

	protected virtual void OnDisable()
	{
		if (Application.isPlaying)
		{
			if (EZAnimator.Exists())
			{
				EZAnimator.instance.Stop(base.gameObject);
			}
			if (detargetOnDisable && UIManager.Exists())
			{
				UIManager.instance.Detarget(this);
			}
		}
	}

	public virtual void Start()
	{
		if (!m_started)
		{
			m_started = true;
			ScanChildren();
			for (int i = 0; i < Transitions.list.Length; i++)
			{
				Transitions.list[i].MainSubject = base.gameObject;
			}
			SetupTransitionSubjects();
		}
	}

	public void ScanChildren()
	{
		uiObjs.Clear();
		Component[] componentsInChildren = base.transform.GetComponentsInChildren(typeof(IUIObject), includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (!(componentsInChildren[i] == this))
			{
				if (base.gameObject.layer == UIManager.instance.gameObject.layer)
				{
					UIPanelManager.SetLayerRecursively(componentsInChildren[i].gameObject, base.gameObject.layer);
				}
				IUIObject iUIObject = (IUIObject)componentsInChildren[i];
				uiObjs.Add(new EZLinkedListNode<IUIObject>(iUIObject));
				iUIObject.RequestContainership(this);
			}
		}
		componentsInChildren = base.transform.GetComponentsInChildren(typeof(UIPanelBase), includeInactive: true);
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			if (!(componentsInChildren[j] == this))
			{
				if (base.gameObject.layer == UIManager.instance.gameObject.layer)
				{
					UIPanelManager.SetLayerRecursively(componentsInChildren[j].gameObject, base.gameObject.layer);
				}
				UIPanelBase uIPanelBase = (UIPanelBase)componentsInChildren[j];
				childPanels.Add(new EZLinkedListNode<UIPanelBase>(uIPanelBase));
				uIPanelBase.RequestContainership(this);
			}
		}
	}

	protected virtual void SetupTransitionSubjects()
	{
		for (int i = 0; i < 4; i++)
		{
			Transitions.list[i].AddTransitionEndDelegate(TransitionCompleted);
		}
		if (uiObjs.Rewind())
		{
			do
			{
				GameObject gameObject = ((Component)uiObjs.Current.val).gameObject;
				int hashCode = gameObject.GetHashCode();
				for (int j = 0; j < Transitions.list.Length; j++)
				{
					Transitions.list[j].AddSubSubject(gameObject);
				}
				if (!subjects.ContainsKey(hashCode))
				{
					subjects.Add(hashCode, gameObject);
				}
			}
			while (uiObjs.MoveNext());
		}
		Component[] componentsInChildren = base.transform.GetComponentsInChildren(typeof(SpriteRoot), includeInactive: true);
		for (int k = 0; k < componentsInChildren.Length; k++)
		{
			GameObject gameObject = componentsInChildren[k].gameObject;
			int hashCode = gameObject.GetHashCode();
			if (!subjects.ContainsKey(hashCode))
			{
				for (int l = 0; l < Transitions.list.Length; l++)
				{
					Transitions.list[l].AddSubSubject(gameObject);
				}
				subjects.Add(hashCode, gameObject);
			}
		}
		Component[] componentsInChildren2 = base.transform.GetComponentsInChildren(typeof(SpriteText), includeInactive: true);
		for (int m = 0; m < componentsInChildren2.Length; m++)
		{
			GameObject gameObject = componentsInChildren2[m].gameObject;
			int hashCode = gameObject.GetHashCode();
			if (!subjects.ContainsKey(hashCode))
			{
				for (int n = 0; n < Transitions.list.Length; n++)
				{
					Transitions.list[n].AddSubSubject(gameObject);
				}
				subjects.Add(hashCode, gameObject);
			}
		}
	}

	public void AddChild(GameObject go)
	{
		IUIObject iUIObject = (IUIObject)go.GetComponent("IUIObject");
		if (iUIObject != null)
		{
			if (iUIObject.Container != this)
			{
				iUIObject.Container = this;
			}
			uiObjs.Add(new EZLinkedListNode<IUIObject>(iUIObject));
		}
		else
		{
			UIPanelBase uIPanelBase = (UIPanelBase)go.GetComponent(typeof(UIPanelBase));
			if (uIPanelBase != null)
			{
				if (uIPanelBase.Container != this)
				{
					uIPanelBase.Container = this;
				}
				childPanels.Add(new EZLinkedListNode<UIPanelBase>(uIPanelBase));
			}
		}
		if (!base.gameObject.active)
		{
			go.SetActiveRecursively(state: false);
		}
		AddSubject(go);
	}

	public void RemoveChild(GameObject go)
	{
		IUIObject iUIObject = (IUIObject)go.GetComponent("IUIObject");
		if (iUIObject != null)
		{
			if (uiObjs.Rewind())
			{
				do
				{
					if (uiObjs.Current.val == iUIObject)
					{
						uiObjs.Remove(uiObjs.Current);
						break;
					}
				}
				while (uiObjs.MoveNext());
			}
			if (iUIObject.Container == this)
			{
				iUIObject.Container = null;
			}
		}
		else
		{
			UIPanelBase uIPanelBase = (UIPanelBase)go.GetComponent(typeof(UIPanelBase));
			if (uIPanelBase != null)
			{
				if (childPanels.Rewind())
				{
					do
					{
						if (childPanels.Current.val == uIPanelBase)
						{
							childPanels.Remove(childPanels.Current);
							break;
						}
					}
					while (childPanels.MoveNext());
				}
				if (uIPanelBase.Container == this)
				{
					uIPanelBase.Container = null;
				}
			}
		}
		RemoveSubject(go);
	}

	public void MakeChild(GameObject go)
	{
		AddChild(go);
		go.transform.parent = base.transform;
	}

	public void AddSubject(GameObject go)
	{
		int hashCode = go.GetHashCode();
		if (!subjects.ContainsKey(hashCode))
		{
			subjects.Add(hashCode, go);
			for (int i = 0; i < Transitions.list.Length; i++)
			{
				Transitions.list[i].AddSubSubject(go);
			}
		}
	}

	public void RemoveSubject(GameObject go)
	{
		int hashCode = go.GetHashCode();
		if (subjects.ContainsKey(hashCode))
		{
			subjects.Remove(hashCode);
			for (int i = 0; i < Transitions.list.Length; i++)
			{
				Transitions.list[i].RemoveSubSubject(go);
			}
		}
	}

	public string[] GetTransitionNames()
	{
		if (Transitions == null)
		{
			return null;
		}
		string[] array = new string[Transitions.list.Length];
		for (int i = 0; i < Transitions.list.Length; i++)
		{
			array[i] = Transitions.list[i].name;
		}
		return array;
	}

	public EZTransition GetTransition(int index)
	{
		if (Transitions == null)
		{
			return null;
		}
		if (Transitions.list == null)
		{
			return null;
		}
		if (Transitions.list.Length <= index || index < 0)
		{
			return null;
		}
		return Transitions.list[index];
	}

	public EZTransition GetTransition(UIPanelManager.SHOW_MODE transition)
	{
		return GetTransition((int)transition);
	}

	public EZTransition GetTransition(string transName)
	{
		if (Transitions == null)
		{
			return null;
		}
		if (Transitions.list == null)
		{
			return null;
		}
		EZTransition[] list = Transitions.list;
		for (int i = 0; i < list.Length; i++)
		{
			if (string.Equals(list[i].name, transName, StringComparison.CurrentCultureIgnoreCase))
			{
				return list[i];
			}
		}
		return null;
	}

	public virtual void StartTransition(UIPanelManager.SHOW_MODE mode)
	{
		if (!m_started)
		{
			Start();
		}
		if (prevTransition != null)
		{
			prevTransition.StopSafe();
		}
		prevTransIndex = (int)mode;
		if (blockInput[prevTransIndex])
		{
			UIManager.instance.LockInput();
		}
		prevTransition = Transitions.list[prevTransIndex];
		if (deactivateAllOnDismiss && (mode == UIPanelManager.SHOW_MODE.BringInBack || mode == UIPanelManager.SHOW_MODE.BringInForward))
		{
			base.gameObject.SetActiveRecursively(state: true);
			Start();
		}
		prevTransition.Start();
	}

	public virtual void StartTransition(string transName)
	{
		if (!m_started)
		{
			Start();
		}
		EZTransition[] list = Transitions.list;
		for (int i = 0; i < list.Length; i++)
		{
			if (string.Equals(list[i].name, transName, StringComparison.CurrentCultureIgnoreCase))
			{
				if (prevTransition != null)
				{
					prevTransition.StopSafe();
				}
				prevTransIndex = i;
				if (blockInput[prevTransIndex])
				{
					UIManager.instance.LockInput();
				}
				prevTransition = list[prevTransIndex];
				if (deactivateAllOnDismiss && (prevTransition == list[1] || prevTransition == list[0]))
				{
					base.gameObject.SetActiveRecursively(state: true);
					Start();
				}
				prevTransition.Start();
			}
		}
	}

	public void TransitionCompleted(EZTransition transition)
	{
		if (deactivateAllOnDismiss && (transition == Transitions.list[2] || transition == Transitions.list[3]))
		{
			base.gameObject.SetActiveRecursively(state: false);
		}
		if (tempTransCompleteDel != null)
		{
			tempTransCompleteDel(this, transition);
		}
		tempTransCompleteDel = null;
		if (blockInput[prevTransIndex] && UIManager.Exists())
		{
			UIManager.instance.UnlockInput();
		}
	}

	public virtual void BringIn()
	{
		StartTransition(UIPanelManager.SHOW_MODE.BringInForward);
	}

	public virtual void Dismiss()
	{
		StartTransition(UIPanelManager.SHOW_MODE.DismissForward);
	}

	public static int CompareIndices(UIPanelBase a, UIPanelBase b)
	{
		return a.index - b.index;
	}

	public void AddTempTransitionDelegate(TransitionCompleteDelegate del)
	{
		tempTransCompleteDel = (TransitionCompleteDelegate)Delegate.Combine(tempTransCompleteDel, del);
	}

	public bool RequestContainership(IUIContainer cont)
	{
		Transform parent = base.transform.parent;
		Transform transform = ((Component)cont).transform;
		while (parent != null)
		{
			if (parent == transform)
			{
				container = cont;
				return true;
			}
			if (parent.gameObject.GetComponent("IUIContainer") != null)
			{
				return false;
			}
			parent = parent.parent;
		}
		return false;
	}

	public bool GotFocus()
	{
		return false;
	}

	public virtual void SetInputDelegate(EZInputDelegate del)
	{
		inputDelegate = del;
	}

	public virtual void AddInputDelegate(EZInputDelegate del)
	{
		inputDelegate = (EZInputDelegate)Delegate.Combine(inputDelegate, del);
	}

	public virtual void RemoveInputDelegate(EZInputDelegate del)
	{
		inputDelegate = (EZInputDelegate)Delegate.Remove(inputDelegate, del);
	}

	public virtual void SetValueChangedDelegate(EZValueChangedDelegate del)
	{
		changeDelegate = del;
	}

	public virtual void AddValueChangedDelegate(EZValueChangedDelegate del)
	{
		changeDelegate = (EZValueChangedDelegate)Delegate.Combine(changeDelegate, del);
	}

	public virtual void RemoveValueChangedDelegate(EZValueChangedDelegate del)
	{
		changeDelegate = (EZValueChangedDelegate)Delegate.Remove(changeDelegate, del);
	}

	public virtual void OnInput(POINTER_INFO ptr)
	{
		if (Container != null)
		{
			ptr.callerIsControl = true;
			Container.OnInput(ptr);
		}
	}

	virtual GameObject IUIObject.get_gameObject()
	{
		return base.gameObject;
	}

	virtual Transform IUIObject.get_transform()
	{
		return base.transform;
	}
}
