using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/List Item")]
public class UIListItem : UIButton, IUIListObject, IUIObject
{
	[HideInInspector]
	public bool activeOnlyWhenSelected;

	protected int m_index;

	protected bool m_selected;

	protected UIScrollList list;

	protected Vector2 colliderTL;

	protected Vector2 colliderBR;

	protected Vector3 colliderCenter;

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

	public override string Text
	{
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

	public SpriteText TextObj
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	protected override void Awake()
	{
	}

	protected void DoNeccessaryInput(ref POINTER_INFO ptr)
	{
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
	}

	protected override void OnDisable()
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	public override void FindOuterEdges()
	{
	}

	public override void TruncateRight(float pct)
	{
	}

	public override void TruncateLeft(float pct)
	{
	}

	public override void TruncateTop(float pct)
	{
	}

	public override void TruncateBottom(float pct)
	{
	}

	public override void Untruncate()
	{
	}

	public override void Hide(bool tf)
	{
	}

	public void SetList(UIScrollList c)
	{
	}

	public void FindText()
	{
	}

	public bool IsContainer()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public new static UIListItem Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public new static UIListItem Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual Vector2 IUIListObject.get_TopLeftEdge()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual Vector2 IUIListObject.get_BottomRightEdge()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	virtual bool IUIListObject.get_Managed()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
