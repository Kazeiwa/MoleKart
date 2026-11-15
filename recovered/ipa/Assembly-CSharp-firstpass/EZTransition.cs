using System;
using UnityEngine;

[Serializable]
public class EZTransition
{
	public delegate void OnTransitionEndDelegate(EZTransition transition);

	public string name;

	public EZAnimation.ANIM_TYPE[] animationTypes;

	public AnimParams[] animParams;

	[NonSerialized]
	protected EZLinkedList<EZLinkedListNode<EZAnimation>> runningAnims;

	protected EZLinkedList<EZLinkedListNode<EZAnimation>> idleAnims;

	[NonSerialized]
	protected GameObject mainSubject;

	[NonSerialized]
	protected EZLinkedList<EZLinkedListNode<GameObject>> subSubjects;

	[NonSerialized]
	protected OnTransitionEndDelegate onEndDelegates;

	public bool initialized;

	protected bool forcedStop;

	public EZLinkedList<EZLinkedListNode<GameObject>> SubSubjects
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public GameObject MainSubject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public EZTransition(string n)
	{
	}

	public void AddTransitionEndDelegate(OnTransitionEndDelegate del)
	{
	}

	public void RemoveTransitionEndDelegate(OnTransitionEndDelegate del)
	{
	}

	public void Copy(EZTransition src)
	{
	}

	public void AddSubSubject(GameObject go)
	{
	}

	public void RemoveSubSubject(GameObject go)
	{
	}

	public void OnAnimEnd(EZAnimation anim)
	{
	}

	public EZLinkedListNode<EZAnimation> AddRunningAnim()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Start()
	{
	}

	public void End()
	{
	}

	public void StopSafe()
	{
	}

	public void Pause()
	{
	}

	public void Unpause()
	{
	}

	public bool IsRunning()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsRunningAtAll()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void CallEndDelegates()
	{
	}

	public int Add()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public AnimParams AddElement(EZAnimation.ANIM_TYPE type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Remove(int index)
	{
	}

	public void SetElementType(int index, EZAnimation.ANIM_TYPE type)
	{
	}

	public string[] GetNames()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
