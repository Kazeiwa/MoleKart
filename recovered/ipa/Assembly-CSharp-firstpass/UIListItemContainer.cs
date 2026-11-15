using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[AddComponentMenu("EZ GUI/Controls/List Item Container")]
public class UIListItemContainer : ControlBase, IUIListObject, IUIContainer, IUIObject
{
	protected List<SpriteRoot> uiObjs;

	protected List<SpriteText> textObjs;

	protected bool m_started;

	private Vector2 topLeftEdge;

	private Vector2 bottomRightEdge;

	private Rect3D clippingRect;

	private bool clipped;

	private UIScrollList list;

	protected int index;

	private bool m_selected;

	public override IUIContainer Container
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

	public override string[] States
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 TopLeftEdge
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 BottomRightEdge
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool Managed
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Rect3D ClippingRect
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool Clipped
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public int Index
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override string Text
	{
		set
		{
		}
	}

	public SpriteText TextObj
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool selected
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override void Start()
	{
	}

	public void ScanChildren()
	{
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

	public SpriteRoot GetElement(string elementName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SpriteText GetTextElement(string elementName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void OnInput(POINTER_INFO ptr)
	{
	}

	public override EZTransitionList GetTransitions(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsContainer()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void FindOuterEdges()
	{
	}

	public void Hide(bool tf)
	{
	}

	public void Unclip()
	{
	}

	public override void UpdateCollider()
	{
	}

	public void SetList(UIScrollList c)
	{
	}

	public void Delete()
	{
	}
}
