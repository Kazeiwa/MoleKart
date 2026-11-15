using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Scroll List")]
public class UIScrollList : MonoBehaviour, IUIObject
{
	public enum ORIENTATION
	{
		HORIZONTAL,
		VERTICAL
	}

	public enum DIRECTION
	{
		TtoB_LtoR,
		BtoT_RtoL
	}

	public enum ALIGNMENT
	{
		LEFT_TOP,
		CENTER,
		RIGHT_BOTTOM
	}

	protected delegate float ItemAlignmentDel(IUIListObject item);

	protected const float reboundSpeed = 1f;

	protected const float overscrollAllowance = 0.5f;

	protected const float scrollDecelCoef = 0.4f;

	protected const float lowPassKernelWidthInSeconds = 0.03f;

	protected const float scrollDeltaUpdateInterval = 0.0166f;

	protected const float lowPassFilterFactor = 83f / 150f;

	protected const float backgroundColliderOffset = 0.01f;

	public bool touchScroll;

	public UISlider slider;

	public ORIENTATION orientation;

	public DIRECTION direction;

	public ALIGNMENT alignment;

	public Vector2 viewableArea;

	protected Rect3D clientClippingRect;

	public float itemSpacing;

	public bool spacingAtEnds;

	public bool activateWhenAdding;

	public bool clipContents;

	public bool clipWhenMoving;

	public float dragThreshold;

	public GameObject[] sceneItems;

	public PrefabListItem[] prefabItems;

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvokeOnSelect;

	public SpriteManager manager;

	public bool detargetOnDisable;

	protected float contentExtents;

	protected IUIListObject selectedItem;

	protected float scrollPos;

	protected GameObject mover;

	protected List<IUIListObject> items;

	protected List<IUIListObject> visibleItems;

	protected List<IUIListObject> tempVisItems;

	protected bool m_controlIsEnabled;

	protected IUIContainer container;

	protected EZInputDelegate inputDelegate;

	protected EZValueChangedDelegate changeDelegate;

	protected Vector3 cachedPos;

	protected Quaternion cachedRot;

	protected Vector3 cachedScale;

	protected bool m_started;

	protected bool m_awake;

	protected bool isScrolling;

	protected bool noTouch;

	private float scrollInertia;

	protected float scrollMax;

	private float scrollDelta;

	private float lastTime;

	private float timeDelta;

	public float ScrollPosition
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float ContentExtents
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IUIListObject SelectedItem
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public int Count
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool controlIsEnabled
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

	private void Awake()
	{
	}

	private void Start()
	{
	}

	protected void CalcClippingRect()
	{
	}

	public void SliderMoved(IUIObject slider)
	{
	}

	protected void ScrollListTo_Internal(float pos)
	{
	}

	public void ScrollListTo(float pos)
	{
	}

	public void SetViewableAreaPixelDimensions(Camera cam, int width, int height)
	{
	}

	public void InsertItem(IUIListObject item, int position)
	{
	}

	public void InsertItem(IUIListObject item, int position, string text)
	{
	}

	public void AddItem(GameObject itemGO)
	{
	}

	public void AddItem(IUIListObject item)
	{
	}

	public void AddItem(IUIListObject item, string text)
	{
	}

	public IUIListObject CreateItem(GameObject prefab)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IUIListObject CreateItem(GameObject prefab, string text)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IUIListObject CreateItem(GameObject prefab, int position)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IUIListObject CreateItem(GameObject prefab, int position, string text)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void UpdateContentExtents(float change)
	{
	}

	protected float GetYCentered(IUIListObject item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected float GetYAlignTop(IUIListObject item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected float GetYAlignBottom(IUIListObject item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected float GetXCentered(IUIListObject item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected float GetXAlignLeft(IUIListObject item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected float GetXAlignRight(IUIListObject item)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PositionItems()
	{
	}

	public void RepositionItems()
	{
	}

	protected void PositionHorizontally(bool updateExtents)
	{
	}

	protected void PositionVertically(bool updateExtents)
	{
	}

	protected void ClipItems()
	{
	}

	public void DidSelect(IUIListObject item)
	{
	}

	public void DidClick(IUIListObject item)
	{
	}

	public void ListDragged(POINTER_INFO ptr)
	{
	}

	public void PointerReleased()
	{
	}

	public void OnEnable()
	{
	}

	protected virtual void OnDisable()
	{
	}

	public void SetSelectedItem(int index)
	{
	}

	public IUIListObject GetItem(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RemoveItem(int index, bool destroy)
	{
	}

	public void RemoveItem(IUIListObject item, bool destroy)
	{
	}

	public void ClearList(bool destroy)
	{
	}

	public void OnInput(POINTER_INFO ptr)
	{
	}

	private void Update()
	{
	}

	protected void AddItemsToContainer()
	{
	}

	protected void RemoveItemsFromContainer()
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

	public void SetInputDelegate(EZInputDelegate del)
	{
	}

	public void AddInputDelegate(EZInputDelegate del)
	{
	}

	public void RemoveInputDelegate(EZInputDelegate del)
	{
	}

	public void SetValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	public void AddValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	public void RemoveValueChangedDelegate(EZValueChangedDelegate del)
	{
	}

	private void OnDrawGizmosSelected()
	{
	}

	public static UIScrollList Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UIScrollList Create(string name, Vector3 pos, Quaternion rotation)
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
