using System;
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

	public bool touchScroll = true;

	public UISlider slider;

	public ORIENTATION orientation;

	public DIRECTION direction;

	public ALIGNMENT alignment = ALIGNMENT.CENTER;

	public Vector2 viewableArea;

	protected Rect3D clientClippingRect;

	public float itemSpacing;

	public bool spacingAtEnds = true;

	public bool activateWhenAdding = true;

	public bool clipContents = true;

	public bool clipWhenMoving;

	public float dragThreshold = float.NaN;

	public GameObject[] sceneItems = new GameObject[0];

	public PrefabListItem[] prefabItems = new PrefabListItem[0];

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvokeOnSelect;

	public SpriteManager manager;

	public bool detargetOnDisable;

	protected float contentExtents;

	protected IUIListObject selectedItem;

	protected float scrollPos;

	protected GameObject mover;

	protected List<IUIListObject> items = new List<IUIListObject>();

	protected List<IUIListObject> visibleItems = new List<IUIListObject>();

	protected List<IUIListObject> tempVisItems = new List<IUIListObject>();

	protected bool m_controlIsEnabled = true;

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

	public float scrollDelta;

	private float lastTime;

	private float timeDelta;

	public float ScrollPosition
	{
		get
		{
			return scrollPos;
		}
		set
		{
			ScrollListTo(value);
		}
	}

	public float ContentExtents => contentExtents;

	public IUIListObject SelectedItem
	{
		get
		{
			return selectedItem;
		}
		set
		{
			if (selectedItem != null)
			{
				selectedItem.selected = false;
			}
			if (value == null)
			{
				selectedItem = null;
				return;
			}
			selectedItem = value;
			selectedItem.selected = true;
		}
	}

	public int Count => items.Count;

	public bool controlIsEnabled
	{
		get
		{
			return m_controlIsEnabled;
		}
		set
		{
			m_controlIsEnabled = value;
			for (int i = 0; i < items.Count; i++)
			{
				items[i].controlIsEnabled = value;
			}
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
			if (value != container)
			{
				if (container != null)
				{
					RemoveItemsFromContainer();
				}
				container = value;
				AddItemsToContainer();
			}
			else
			{
				container = value;
			}
		}
	}

	public void Awake()
	{
		if (!m_awake)
		{
			m_awake = true;
			mover = new GameObject();
			mover.name = "Mover";
			mover.transform.parent = base.transform;
			mover.transform.localPosition = Vector3.zero;
			mover.transform.localRotation = Quaternion.identity;
			mover.transform.localScale = Vector3.one;
			if (direction == DIRECTION.BtoT_RtoL)
			{
				scrollPos = 1f;
			}
		}
	}

	public void Start()
	{
		if (m_started)
		{
			return;
		}
		m_started = true;
		lastTime = Time.realtimeSinceStartup;
		cachedPos = base.transform.position;
		cachedRot = base.transform.rotation;
		cachedScale = base.transform.lossyScale;
		CalcClippingRect();
		if (slider != null)
		{
			slider.AddValueChangedDelegate(SliderMoved);
		}
		if (base.collider == null && touchScroll)
		{
			BoxCollider boxCollider = (BoxCollider)base.gameObject.AddComponent(typeof(BoxCollider));
			boxCollider.size = new Vector3(viewableArea.x, viewableArea.y, 0.001f);
			boxCollider.center = Vector3.forward * 0.01f;
			boxCollider.isTrigger = true;
		}
		for (int i = 0; i < sceneItems.Length; i++)
		{
			if (sceneItems[i] != null)
			{
				AddItem(sceneItems[i]);
			}
		}
		for (int j = 0; j < prefabItems.Length; j++)
		{
			if (prefabItems[j] == null)
			{
				continue;
			}
			if (prefabItems[j].item == null)
			{
				if (prefabItems[0].item != null)
				{
					CreateItem(prefabItems[0].item, (!(prefabItems[j].itemText == string.Empty)) ? prefabItems[j].itemText : null);
				}
			}
			else
			{
				CreateItem(prefabItems[j].item, (!(prefabItems[j].itemText == string.Empty)) ? prefabItems[j].itemText : null);
			}
		}
		if (float.IsNaN(dragThreshold))
		{
			dragThreshold = UIManager.instance.dragThreshold;
		}
	}

	protected void CalcClippingRect()
	{
		clientClippingRect.FromPoints(new Vector3((0f - viewableArea.x) * 0.5f, viewableArea.y * 0.5f), new Vector3(viewableArea.x * 0.5f, viewableArea.y * 0.5f), new Vector3((0f - viewableArea.x) * 0.5f, (0f - viewableArea.y) * 0.5f));
		clientClippingRect.MultFast(base.transform.localToWorldMatrix);
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].TextObj != null)
			{
				items[i].TextObj.ClippingRect = clientClippingRect;
			}
		}
	}

	public void SliderMoved(IUIObject slider)
	{
		ScrollListTo_Internal(((UISlider)slider).Value);
	}

	protected void ScrollListTo_Internal(float pos)
	{
		float num = ((!spacingAtEnds) ? 0f : itemSpacing);
		if (!float.IsNaN(pos))
		{
			if (orientation == ORIENTATION.VERTICAL)
			{
				float num2 = contentExtents + num - viewableArea.y;
				float num3 = ((direction != DIRECTION.TtoB_LtoR) ? (-1f) : 1f);
				mover.transform.localPosition = Vector3.up * num3 * Mathf.Clamp(num2, 0f, num2) * pos;
			}
			else
			{
				float num2 = contentExtents + num - viewableArea.x;
				float num4 = ((direction != DIRECTION.TtoB_LtoR) ? 1f : (-1f));
				mover.transform.localPosition = Vector3.right * num4 * Mathf.Clamp(num2, 0f, num2) * pos;
			}
			scrollPos = pos;
			ClipItems();
			if (slider != null)
			{
				slider.Value = scrollPos;
			}
		}
	}

	public void ScrollListTo(float pos)
	{
		scrollInertia = 0f;
		scrollDelta = 0f;
		ScrollListTo_Internal(pos);
	}

	public void SetViewableAreaPixelDimensions(Camera cam, int width, int height)
	{
		float distanceToPoint = new Plane(cam.transform.forward, cam.transform.position).GetDistanceToPoint(base.transform.position);
		float num = Vector3.Distance(cam.ScreenToWorldPoint(new Vector3(0f, 1f, distanceToPoint)), cam.ScreenToWorldPoint(new Vector3(0f, 0f, distanceToPoint)));
		viewableArea = new Vector2((float)width * num, (float)height * num);
		RepositionItems();
	}

	public void InsertItem(IUIListObject item, int position)
	{
		InsertItem(item, position, null);
	}

	public void InsertItem(IUIListObject item, int position, string text)
	{
		if (!m_awake)
		{
			Awake();
		}
		if (!m_started)
		{
			Start();
		}
		if (activateWhenAdding && !((Component)item).gameObject.active)
		{
			((Component)item).gameObject.SetActiveRecursively(state: true);
		}
		if (!base.gameObject.active)
		{
			((Component)item).gameObject.SetActiveRecursively(state: false);
		}
		item.gameObject.layer = base.gameObject.layer;
		if (container != null)
		{
			container.AddChild(item.gameObject);
		}
		item.transform.parent = mover.transform;
		item.transform.localRotation = Quaternion.identity;
		item.transform.localScale = Vector3.one;
		item.transform.localPosition = Vector3.zero;
		item.SetList(this);
		if (text != null)
		{
			item.Text = text;
		}
		item.FindOuterEdges();
		item.UpdateCollider();
		position = Mathf.Clamp(position, 0, items.Count);
		if (clipContents)
		{
			item.Hide(tf: true);
			if (!item.Managed)
			{
				item.gameObject.SetActiveRecursively(state: false);
			}
		}
		if (position == items.Count)
		{
			float x = 0f;
			float y = 0f;
			bool flag = false;
			float change;
			if (orientation == ORIENTATION.HORIZONTAL)
			{
				if (items.Count > 0)
				{
					flag = true;
					IUIListObject iUIListObject = items[items.Count - 1];
					x = ((direction != DIRECTION.TtoB_LtoR) ? (iUIListObject.transform.localPosition.x - iUIListObject.BottomRightEdge.x - itemSpacing + item.TopLeftEdge.x) : (iUIListObject.transform.localPosition.x + iUIListObject.BottomRightEdge.x + itemSpacing - item.TopLeftEdge.x));
				}
				else
				{
					if (spacingAtEnds)
					{
						flag = true;
					}
					x = ((direction != DIRECTION.TtoB_LtoR) ? (viewableArea.x * 0.5f - item.BottomRightEdge.x - ((!spacingAtEnds) ? 0f : itemSpacing)) : (viewableArea.x * -0.5f - item.TopLeftEdge.x + ((!spacingAtEnds) ? 0f : itemSpacing)));
				}
				switch (alignment)
				{
				case ALIGNMENT.CENTER:
					y = 0f;
					break;
				case ALIGNMENT.LEFT_TOP:
					y = viewableArea.y * 0.5f - item.TopLeftEdge.y;
					break;
				case ALIGNMENT.RIGHT_BOTTOM:
					y = viewableArea.y * -0.5f - item.BottomRightEdge.y;
					break;
				}
				change = item.BottomRightEdge.x - item.TopLeftEdge.x + ((!flag) ? 0f : itemSpacing);
			}
			else
			{
				if (items.Count > 0)
				{
					flag = true;
					IUIListObject iUIListObject = items[items.Count - 1];
					y = ((direction != DIRECTION.TtoB_LtoR) ? (iUIListObject.transform.localPosition.y - iUIListObject.BottomRightEdge.y + itemSpacing + item.TopLeftEdge.y) : (iUIListObject.transform.localPosition.y + iUIListObject.BottomRightEdge.y - itemSpacing - item.TopLeftEdge.y));
				}
				else
				{
					if (spacingAtEnds)
					{
						flag = true;
					}
					y = ((direction != DIRECTION.TtoB_LtoR) ? (viewableArea.y * -0.5f - item.BottomRightEdge.y + ((!spacingAtEnds) ? 0f : itemSpacing)) : (viewableArea.y * 0.5f - item.TopLeftEdge.y - ((!spacingAtEnds) ? 0f : itemSpacing)));
				}
				switch (alignment)
				{
				case ALIGNMENT.CENTER:
					x = 0f;
					break;
				case ALIGNMENT.LEFT_TOP:
					x = viewableArea.x * -0.5f - item.TopLeftEdge.x;
					break;
				case ALIGNMENT.RIGHT_BOTTOM:
					x = viewableArea.x * 0.5f - item.BottomRightEdge.x;
					break;
				}
				change = item.TopLeftEdge.y - item.BottomRightEdge.y + ((!flag) ? 0f : itemSpacing);
			}
			item.transform.localPosition = new Vector3(x, y);
			item.Index = items.Count;
			items.Add(item);
			UpdateContentExtents(change);
			ClipItems();
		}
		else
		{
			items.Insert(position, item);
			PositionItems();
		}
	}

	public void AddItem(GameObject itemGO)
	{
		IUIListObject iUIListObject = (IUIListObject)itemGO.GetComponent(typeof(IUIListObject));
		if (iUIListObject == null)
		{
			Debug.LogWarning("GameObject \"" + itemGO.name + "\" does not contain any list item component suitable to be added to scroll list \"" + base.name + "\".");
		}
		else
		{
			AddItem(iUIListObject, null);
		}
	}

	public void AddItem(IUIListObject item)
	{
		AddItem(item, null);
	}

	public void AddItem(IUIListObject item, string text)
	{
		if (!m_awake)
		{
			Awake();
		}
		if (!m_started)
		{
			Start();
		}
		InsertItem(item, items.Count, text);
	}

	public IUIListObject CreateItem(GameObject prefab)
	{
		if (!m_awake)
		{
			Awake();
		}
		if (!m_started)
		{
			Start();
		}
		return CreateItem(prefab, items.Count, null);
	}

	public IUIListObject CreateItem(GameObject prefab, string text)
	{
		if (!m_awake)
		{
			Awake();
		}
		if (!m_started)
		{
			Start();
		}
		return CreateItem(prefab, items.Count, text);
	}

	public IUIListObject CreateItem(GameObject prefab, int position)
	{
		return CreateItem(prefab, position, null);
	}

	public IUIListObject CreateItem(GameObject prefab, int position, string text)
	{
		IUIListObject iUIListObject = (IUIListObject)prefab.GetComponent(typeof(IUIListObject));
		if (iUIListObject == null)
		{
			return null;
		}
		GameObject gameObject;
		if (manager != null)
		{
			if (iUIListObject.IsContainer())
			{
				gameObject = (GameObject)UnityEngine.Object.Instantiate(prefab);
				Component[] componentsInChildren = gameObject.GetComponentsInChildren(typeof(SpriteRoot));
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					manager.AddSprite((SpriteRoot)componentsInChildren[i]);
				}
			}
			else
			{
				SpriteRoot spriteRoot = manager.CreateSprite(prefab);
				if (spriteRoot == null)
				{
					return null;
				}
				gameObject = spriteRoot.gameObject;
			}
		}
		else
		{
			gameObject = (GameObject)UnityEngine.Object.Instantiate(prefab);
		}
		iUIListObject = (IUIListObject)gameObject.GetComponent(typeof(IUIListObject));
		if (iUIListObject == null)
		{
			return null;
		}
		InsertItem(iUIListObject, position, text);
		return iUIListObject;
	}

	protected void UpdateContentExtents(float change)
	{
		float num = ((!spacingAtEnds) ? 0f : itemSpacing);
		contentExtents += change;
		float num2;
		float num3;
		if (orientation == ORIENTATION.HORIZONTAL)
		{
			num2 = contentExtents - change + num - viewableArea.x;
			num3 = contentExtents + num - viewableArea.x;
			scrollMax = viewableArea.x / (contentExtents + num - viewableArea.x) * 0.5f;
		}
		else
		{
			num2 = contentExtents - change + num - viewableArea.y;
			num3 = contentExtents + num - viewableArea.y;
			scrollMax = viewableArea.y / (contentExtents + num - viewableArea.y) * 0.5f;
		}
		ScrollListTo_Internal(Mathf.Clamp01(num2 * scrollPos / num3));
	}

	protected float GetYCentered(IUIListObject item)
	{
		return 0f;
	}

	protected float GetYAlignTop(IUIListObject item)
	{
		return viewableArea.y * 0.5f - item.TopLeftEdge.y;
	}

	protected float GetYAlignBottom(IUIListObject item)
	{
		return viewableArea.y * -0.5f - item.BottomRightEdge.y;
	}

	protected float GetXCentered(IUIListObject item)
	{
		return 0f;
	}

	protected float GetXAlignLeft(IUIListObject item)
	{
		return viewableArea.x * -0.5f - item.TopLeftEdge.x;
	}

	protected float GetXAlignRight(IUIListObject item)
	{
		return viewableArea.x * 0.5f - item.BottomRightEdge.x;
	}

	public void PositionItems()
	{
		if (orientation == ORIENTATION.HORIZONTAL)
		{
			PositionHorizontally(updateExtents: false);
		}
		else
		{
			PositionVertically(updateExtents: false);
		}
		UpdateContentExtents(0f);
		ClipItems();
	}

	public void RepositionItems()
	{
		if (orientation == ORIENTATION.HORIZONTAL)
		{
			PositionHorizontally(updateExtents: true);
		}
		else
		{
			PositionVertically(updateExtents: true);
		}
		UpdateContentExtents(0f);
		ClipItems();
	}

	protected void PositionHorizontally(bool updateExtents)
	{
		contentExtents = 0f;
		ItemAlignmentDel itemAlignmentDel = alignment switch
		{
			ALIGNMENT.CENTER => GetYCentered, 
			ALIGNMENT.LEFT_TOP => GetYAlignTop, 
			ALIGNMENT.RIGHT_BOTTOM => GetYAlignBottom, 
			_ => GetYCentered, 
		};
		float num;
		if (direction == DIRECTION.TtoB_LtoR)
		{
			num = viewableArea.x * -0.5f + ((!spacingAtEnds) ? 0f : itemSpacing);
			for (int i = 0; i < items.Count; i++)
			{
				if (updateExtents)
				{
					items[i].FindOuterEdges();
				}
				items[i].transform.localPosition = new Vector3(num - items[i].TopLeftEdge.x, itemAlignmentDel(items[i]));
				contentExtents += items[i].BottomRightEdge.x - items[i].TopLeftEdge.x + itemSpacing;
				num += items[i].BottomRightEdge.x - items[i].TopLeftEdge.x + itemSpacing;
				items[i].Index = i;
			}
			if (!spacingAtEnds)
			{
				contentExtents -= itemSpacing;
			}
			return;
		}
		num = viewableArea.x * 0.5f - ((!spacingAtEnds) ? 0f : itemSpacing);
		for (int j = 0; j < items.Count; j++)
		{
			if (updateExtents)
			{
				items[j].FindOuterEdges();
			}
			items[j].transform.localPosition = new Vector3(num - items[j].BottomRightEdge.x, itemAlignmentDel(items[j]));
			contentExtents += items[j].BottomRightEdge.x - items[j].TopLeftEdge.x + itemSpacing;
			num -= items[j].BottomRightEdge.x - items[j].TopLeftEdge.x + itemSpacing;
			items[j].Index = j;
		}
		if (!spacingAtEnds)
		{
			contentExtents -= itemSpacing;
		}
	}

	protected void PositionVertically(bool updateExtents)
	{
		contentExtents = 0f;
		ItemAlignmentDel itemAlignmentDel = alignment switch
		{
			ALIGNMENT.CENTER => GetXCentered, 
			ALIGNMENT.LEFT_TOP => GetXAlignLeft, 
			ALIGNMENT.RIGHT_BOTTOM => GetXAlignRight, 
			_ => GetXCentered, 
		};
		float num;
		if (direction == DIRECTION.TtoB_LtoR)
		{
			num = viewableArea.y * 0.5f - ((!spacingAtEnds) ? 0f : itemSpacing);
			for (int i = 0; i < items.Count; i++)
			{
				if (updateExtents)
				{
					items[i].FindOuterEdges();
				}
				items[i].transform.localPosition = new Vector3(itemAlignmentDel(items[i]), num - items[i].TopLeftEdge.y);
				contentExtents += items[i].TopLeftEdge.y - items[i].BottomRightEdge.y + itemSpacing;
				num -= items[i].TopLeftEdge.y - items[i].BottomRightEdge.y + itemSpacing;
				items[i].Index = i;
			}
			if (!spacingAtEnds)
			{
				contentExtents -= itemSpacing;
			}
			return;
		}
		num = viewableArea.y * -0.5f + ((!spacingAtEnds) ? 0f : itemSpacing);
		for (int j = 0; j < items.Count; j++)
		{
			if (updateExtents)
			{
				items[j].FindOuterEdges();
			}
			items[j].transform.localPosition = new Vector3(itemAlignmentDel(items[j]), num - items[j].BottomRightEdge.y);
			contentExtents += items[j].TopLeftEdge.y - items[j].BottomRightEdge.y + itemSpacing;
			num += items[j].TopLeftEdge.y - items[j].BottomRightEdge.y + itemSpacing;
			items[j].Index = j;
		}
		if (!spacingAtEnds)
		{
			contentExtents -= itemSpacing;
		}
	}

	protected virtual void ClipItems()
	{
		if (mover == null || items.Count < 1 || !clipContents || !base.gameObject.active)
		{
			return;
		}
		IUIListObject iUIListObject = null;
		IUIListObject iUIListObject2 = null;
		if (orientation == ORIENTATION.HORIZONTAL)
		{
			float x = mover.transform.localPosition.x;
			float num = viewableArea.x * -0.5f - x;
			float num2 = viewableArea.x * 0.5f - x;
			int i = (int)((float)(items.Count - 1) * Mathf.Clamp01(scrollPos));
			if (direction == DIRECTION.TtoB_LtoR)
			{
				float x2 = items[i].transform.localPosition.x;
				if (items[i].BottomRightEdge.x + x2 >= num)
				{
					for (i--; i > -1; i--)
					{
						x2 = items[i].transform.localPosition.x;
						if (items[i].BottomRightEdge.x + x2 < num)
						{
							break;
						}
					}
					iUIListObject = items[i + 1];
				}
				else
				{
					for (; i < items.Count; i++)
					{
						x2 = items[i].transform.localPosition.x;
						if (items[i].BottomRightEdge.x + x2 >= num)
						{
							iUIListObject = items[i];
							break;
						}
					}
				}
				if (iUIListObject != null)
				{
					tempVisItems.Add(iUIListObject);
					if (!iUIListObject.gameObject.active)
					{
						iUIListObject.gameObject.SetActiveRecursively(state: true);
					}
					iUIListObject.Hide(tf: false);
					iUIListObject.ClippingRect = clientClippingRect;
					x2 = iUIListObject.transform.localPosition.x;
					if (iUIListObject.BottomRightEdge.x + x2 < num2)
					{
						for (i = iUIListObject.Index + 1; i < items.Count; i++)
						{
							x2 = items[i].transform.localPosition.x;
							if (items[i].BottomRightEdge.x + x2 >= num2)
							{
								if (!items[i].gameObject.active)
								{
									items[i].gameObject.SetActiveRecursively(state: true);
								}
								items[i].Hide(tf: false);
								items[i].ClippingRect = clientClippingRect;
								tempVisItems.Add(items[i]);
								break;
							}
							if (!items[i].gameObject.active)
							{
								items[i].gameObject.SetActiveRecursively(state: true);
							}
							items[i].Hide(tf: false);
							items[i].Clipped = false;
							tempVisItems.Add(items[i]);
						}
					}
				}
			}
			else
			{
				float x2 = items[i].transform.localPosition.x;
				if (items[i].TopLeftEdge.x - x2 <= num2)
				{
					for (i--; i > -1; i--)
					{
						x2 = items[i].transform.localPosition.x;
						if (items[i].TopLeftEdge.x - x2 > num2)
						{
							break;
						}
					}
					iUIListObject = items[i + 1];
				}
				else
				{
					for (; i < items.Count; i++)
					{
						x2 = items[i].transform.localPosition.x;
						if (items[i].TopLeftEdge.x - x2 <= num2)
						{
							iUIListObject = items[i];
							break;
						}
					}
				}
				if (iUIListObject != null)
				{
					tempVisItems.Add(iUIListObject);
					if (!iUIListObject.gameObject.active)
					{
						iUIListObject.gameObject.SetActiveRecursively(state: true);
					}
					iUIListObject.Hide(tf: false);
					iUIListObject.ClippingRect = clientClippingRect;
					x2 = iUIListObject.transform.localPosition.x;
					if (iUIListObject.TopLeftEdge.x - x2 > num)
					{
						for (i = iUIListObject.Index + 1; i < items.Count; i++)
						{
							x2 = items[i].transform.localPosition.x;
							if (items[i].TopLeftEdge.x - x2 <= num)
							{
								if (!items[i].gameObject.active)
								{
									items[i].gameObject.SetActiveRecursively(state: true);
								}
								items[i].Hide(tf: false);
								items[i].ClippingRect = clientClippingRect;
								tempVisItems.Add(items[i]);
								break;
							}
							if (!items[i].gameObject.active)
							{
								items[i].gameObject.SetActiveRecursively(state: true);
							}
							items[i].Hide(tf: false);
							items[i].Clipped = false;
							tempVisItems.Add(items[i]);
						}
					}
				}
			}
		}
		else
		{
			float y = mover.transform.localPosition.y;
			float num3 = viewableArea.y * 0.5f - y;
			float num4 = viewableArea.y * -0.5f - y;
			int j = (int)((float)(items.Count - 1) * Mathf.Clamp01(scrollPos));
			if (direction == DIRECTION.TtoB_LtoR)
			{
				float y2 = items[j].transform.localPosition.y;
				if (items[j].BottomRightEdge.y + y2 <= num3)
				{
					for (j--; j > -1; j--)
					{
						y2 = items[j].transform.localPosition.y;
						if (items[j].BottomRightEdge.y + y2 > num3)
						{
							break;
						}
					}
					iUIListObject = items[j + 1];
				}
				else
				{
					for (; j < items.Count; j++)
					{
						y2 = items[j].transform.localPosition.y;
						if (items[j].BottomRightEdge.y + y2 <= num3)
						{
							iUIListObject = items[j];
							break;
						}
					}
				}
				if (iUIListObject != null)
				{
					tempVisItems.Add(iUIListObject);
					if (!iUIListObject.gameObject.active)
					{
						iUIListObject.gameObject.SetActiveRecursively(state: true);
					}
					iUIListObject.Hide(tf: false);
					iUIListObject.ClippingRect = clientClippingRect;
					y2 = iUIListObject.transform.localPosition.y;
					if (iUIListObject.BottomRightEdge.y + y2 > num4)
					{
						for (j = iUIListObject.Index + 1; j < items.Count; j++)
						{
							y2 = items[j].transform.localPosition.y;
							if (items[j].BottomRightEdge.y + y2 <= num4)
							{
								if (!items[j].gameObject.active)
								{
									items[j].gameObject.SetActiveRecursively(state: true);
								}
								items[j].Hide(tf: false);
								items[j].ClippingRect = clientClippingRect;
								tempVisItems.Add(items[j]);
								break;
							}
							if (!items[j].gameObject.active)
							{
								items[j].gameObject.SetActiveRecursively(state: true);
							}
							items[j].Hide(tf: false);
							items[j].Clipped = false;
							tempVisItems.Add(items[j]);
						}
					}
				}
			}
			else
			{
				float y2 = items[j].transform.localPosition.y;
				if (items[j].TopLeftEdge.y + y2 >= num4)
				{
					for (j--; j > -1; j--)
					{
						y2 = items[j].transform.localPosition.y;
						if (items[j].TopLeftEdge.y + y2 < num4)
						{
							break;
						}
					}
					iUIListObject = items[j + 1];
				}
				else
				{
					for (; j < items.Count; j++)
					{
						y2 = items[j].transform.localPosition.y;
						if (items[j].TopLeftEdge.y + y2 >= num4)
						{
							iUIListObject = items[j];
							break;
						}
					}
				}
				if (iUIListObject != null)
				{
					tempVisItems.Add(iUIListObject);
					if (!iUIListObject.gameObject.active)
					{
						iUIListObject.gameObject.SetActiveRecursively(state: true);
					}
					iUIListObject.Hide(tf: false);
					iUIListObject.ClippingRect = clientClippingRect;
					y2 = iUIListObject.transform.localPosition.y;
					if (iUIListObject.TopLeftEdge.y + y2 < num3)
					{
						for (j = iUIListObject.Index + 1; j < items.Count; j++)
						{
							y2 = items[j].transform.localPosition.y;
							if (items[j].TopLeftEdge.y + y2 >= num3)
							{
								if (!items[j].gameObject.active)
								{
									items[j].gameObject.SetActiveRecursively(state: true);
								}
								items[j].Hide(tf: false);
								items[j].ClippingRect = clientClippingRect;
								tempVisItems.Add(items[j]);
								break;
							}
							if (!items[j].gameObject.active)
							{
								items[j].gameObject.SetActiveRecursively(state: true);
							}
							items[j].Hide(tf: false);
							items[j].Clipped = false;
							tempVisItems.Add(items[j]);
						}
					}
				}
			}
		}
		if (iUIListObject == null)
		{
			return;
		}
		iUIListObject2 = tempVisItems[tempVisItems.Count - 1];
		if (visibleItems.Count > 0)
		{
			if (visibleItems[0].Index > iUIListObject2.Index || visibleItems[visibleItems.Count - 1].Index < iUIListObject.Index)
			{
				for (int k = 0; k < visibleItems.Count; k++)
				{
					visibleItems[k].Hide(tf: true);
					if (!visibleItems[k].Managed)
					{
						visibleItems[k].gameObject.SetActiveRecursively(state: false);
					}
				}
			}
			else
			{
				for (int l = 0; l < visibleItems.Count && visibleItems[l].Index < iUIListObject.Index; l++)
				{
					visibleItems[l].Hide(tf: true);
					if (!visibleItems[l].Managed)
					{
						visibleItems[l].gameObject.SetActiveRecursively(state: false);
					}
				}
				int num5 = visibleItems.Count - 1;
				while (num5 > -1 && visibleItems[num5].Index > iUIListObject2.Index)
				{
					visibleItems[num5].Hide(tf: true);
					if (!visibleItems[num5].Managed)
					{
						visibleItems[num5].gameObject.SetActiveRecursively(state: false);
					}
					num5--;
				}
			}
		}
		for (int m = 0; m < items.Count; m++)
		{
			if (selectedItem != items[m])
			{
				(items[m] as UIListItem).ForceNormal();
			}
		}
		List<IUIListObject> list = visibleItems;
		visibleItems = tempVisItems;
		tempVisItems = list;
		tempVisItems.Clear();
	}

	public void DidSelect(IUIListObject item)
	{
		if (selectedItem != null)
		{
			selectedItem.selected = false;
		}
		selectedItem = item;
		item.selected = true;
		DidClick(item);
	}

	public void DidClick(IUIListObject item)
	{
		if (scriptWithMethodToInvoke != null)
		{
			scriptWithMethodToInvoke.Invoke(methodToInvokeOnSelect, 0f);
		}
		if (changeDelegate != null)
		{
			changeDelegate(this);
		}
	}

	public virtual void ListDragged(POINTER_INFO ptr)
	{
		if (!touchScroll || !controlIsEnabled)
		{
			return;
		}
		Plane plane = default(Plane);
		if (ptr.inputDelta.sqrMagnitude == 0f)
		{
			scrollDelta = 0f;
			return;
		}
		plane.SetNormalAndPosition(mover.transform.forward * -1f, mover.transform.position);
		plane.Raycast(ptr.ray, out var enter);
		Vector3 position = ptr.ray.origin + ptr.ray.direction * enter;
		plane.Raycast(ptr.prevRay, out enter);
		Vector3 position2 = ptr.prevRay.origin + ptr.prevRay.direction * enter;
		position = base.transform.InverseTransformPoint(position);
		position2 = base.transform.InverseTransformPoint(position2);
		Vector3 vector = position - position2;
		float num = ((!spacingAtEnds) ? 0f : itemSpacing);
		if (orientation == ORIENTATION.HORIZONTAL)
		{
			scrollDelta = (0f - vector.x) / (contentExtents + num - viewableArea.x);
			scrollDelta *= base.transform.localScale.x;
		}
		else
		{
			scrollDelta = vector.y / (contentExtents + num - viewableArea.y);
			scrollDelta *= base.transform.localScale.y;
		}
		float num2 = scrollPos + scrollDelta;
		if (num2 > 1f)
		{
			scrollDelta *= Mathf.Clamp01(1f - (num2 - 1f) / scrollMax);
		}
		else if (num2 < 0f)
		{
			scrollDelta *= Mathf.Clamp01(1f + num2 / scrollMax);
		}
		if (direction == DIRECTION.BtoT_RtoL)
		{
			scrollDelta *= -1f;
		}
		ScrollListTo_Internal(scrollPos + scrollDelta);
		noTouch = false;
		isScrolling = true;
	}

	public void PointerReleased()
	{
		noTouch = true;
		if (scrollInertia != 0f)
		{
			scrollDelta = scrollInertia;
		}
		scrollInertia = 0f;
	}

	public void OnEnable()
	{
		base.gameObject.SetActiveRecursively(state: true);
		RepositionItems();
		ClipItems();
	}

	protected virtual void OnDisable()
	{
		if (Application.isPlaying)
		{
			if (EZAnimator.Exists())
			{
				EZAnimator.instance.Stop(base.gameObject);
				EZAnimator.instance.Stop(this);
			}
			if (detargetOnDisable && UIManager.Exists())
			{
				UIManager.instance.Detarget(this);
			}
		}
	}

	public void SetSelectedItem(int index)
	{
		if (index < 0 || index >= items.Count)
		{
			if (selectedItem != null)
			{
				selectedItem.selected = false;
			}
			selectedItem = null;
			return;
		}
		IUIListObject iUIListObject = items[index];
		if (selectedItem != null)
		{
			selectedItem.selected = false;
		}
		selectedItem = iUIListObject;
		iUIListObject.selected = true;
	}

	public IUIListObject GetItem(int index)
	{
		if (index < 0 || index >= items.Count)
		{
			return null;
		}
		return items[index];
	}

	public void RemoveItem(int index, bool destroy)
	{
		if (index >= 0 && index < items.Count)
		{
			if (container != null)
			{
				container.RemoveChild(items[index].gameObject);
			}
			if (selectedItem == items[index])
			{
				selectedItem = null;
				items[index].selected = false;
			}
			visibleItems.Remove(items[index]);
			if (destroy)
			{
				items[index].Delete();
				UnityEngine.Object.Destroy(items[index].gameObject);
			}
			else
			{
				items[index].transform.parent = null;
				items[index].gameObject.SetActiveRecursively(state: false);
			}
			items.RemoveAt(index);
			PositionItems();
		}
	}

	public void RemoveItem(IUIListObject item, bool destroy)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i] == item)
			{
				RemoveItem(i, destroy);
				break;
			}
		}
	}

	public void ClearList(bool destroy)
	{
		RemoveItemsFromContainer();
		selectedItem = null;
		for (int i = 0; i < items.Count; i++)
		{
			items[i].transform.parent = null;
			if (destroy)
			{
				UnityEngine.Object.Destroy(items[i].gameObject);
			}
			else
			{
				items[i].gameObject.SetActiveRecursively(state: false);
			}
		}
		visibleItems.Clear();
		items.Clear();
		PositionItems();
	}

	public void OnInput(POINTER_INFO ptr)
	{
		if (!m_controlIsEnabled)
		{
			if (Container != null)
			{
				ptr.callerIsControl = true;
				Container.OnInput(ptr);
			}
			return;
		}
		if (Vector3.SqrMagnitude(ptr.origPos - ptr.devicePos) > dragThreshold * dragThreshold)
		{
			ptr.isTap = false;
			if (ptr.evt == POINTER_INFO.INPUT_EVENT.TAP)
			{
				ptr.evt = POINTER_INFO.INPUT_EVENT.RELEASE;
			}
		}
		else
		{
			ptr.isTap = true;
		}
		if (inputDelegate != null)
		{
			inputDelegate(ref ptr);
		}
		switch (ptr.evt)
		{
		case POINTER_INFO.INPUT_EVENT.NO_CHANGE:
			if (ptr.active)
			{
				ListDragged(ptr);
			}
			break;
		case POINTER_INFO.INPUT_EVENT.DRAG:
			if (!ptr.isTap)
			{
				ListDragged(ptr);
			}
			break;
		case POINTER_INFO.INPUT_EVENT.RELEASE:
		case POINTER_INFO.INPUT_EVENT.TAP:
		case POINTER_INFO.INPUT_EVENT.RELEASE_OFF:
			PointerReleased();
			break;
		}
		if (Container != null)
		{
			ptr.callerIsControl = true;
			Container.OnInput(ptr);
		}
	}

	private void Update()
	{
		timeDelta = Time.realtimeSinceStartup - lastTime;
		lastTime = Time.realtimeSinceStartup;
		if (cachedPos != base.transform.position || cachedRot != base.transform.rotation || cachedScale != base.transform.lossyScale)
		{
			cachedPos = base.transform.position;
			cachedRot = base.transform.rotation;
			cachedScale = base.transform.lossyScale;
			CalcClippingRect();
			if (clipWhenMoving)
			{
				ClipItems();
			}
		}
		if (isScrolling && noTouch)
		{
			scrollDelta -= scrollDelta * 0.4f * (timeDelta / 0.166f);
			if (scrollPos < 0f)
			{
				scrollPos -= scrollPos * 1f * (timeDelta / 0.166f);
				scrollDelta *= Mathf.Clamp01(1f + scrollPos / scrollMax);
			}
			else if (scrollPos > 1f)
			{
				scrollPos -= (scrollPos - 1f) * 1f * (timeDelta / 0.166f);
				scrollDelta *= Mathf.Clamp01(1f - (scrollPos - 1f) / scrollMax);
			}
			if (Mathf.Abs(scrollDelta) < 0.0001f)
			{
				scrollDelta = 0f;
				if (scrollPos > -0.0001f && scrollPos < 0.0001f)
				{
					scrollPos = Mathf.Clamp01(scrollPos);
				}
			}
			ScrollListTo_Internal(scrollPos + scrollDelta);
			if (slider != null)
			{
				slider.Value = scrollPos;
			}
			if (scrollPos >= 0f && scrollPos <= 1.001f && scrollDelta == 0f)
			{
				isScrolling = false;
			}
		}
		else
		{
			scrollInertia = Mathf.Lerp(scrollInertia, scrollDelta, 83f / 150f);
		}
	}

	protected void AddItemsToContainer()
	{
		if (container != null)
		{
			for (int i = 0; i < items.Count; i++)
			{
				container.AddChild(items[i].gameObject);
			}
		}
	}

	protected void RemoveItemsFromContainer()
	{
		if (container != null)
		{
			for (int i = 0; i < items.Count; i++)
			{
				container.RemoveChild(items[i].gameObject);
			}
		}
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

	public void SetInputDelegate(EZInputDelegate del)
	{
		inputDelegate = del;
	}

	public void AddInputDelegate(EZInputDelegate del)
	{
		inputDelegate = (EZInputDelegate)Delegate.Combine(inputDelegate, del);
	}

	public void RemoveInputDelegate(EZInputDelegate del)
	{
		inputDelegate = (EZInputDelegate)Delegate.Remove(inputDelegate, del);
	}

	public void SetValueChangedDelegate(EZValueChangedDelegate del)
	{
		changeDelegate = del;
	}

	public void AddValueChangedDelegate(EZValueChangedDelegate del)
	{
		changeDelegate = (EZValueChangedDelegate)Delegate.Combine(changeDelegate, del);
	}

	public void RemoveValueChangedDelegate(EZValueChangedDelegate del)
	{
		changeDelegate = (EZValueChangedDelegate)Delegate.Remove(changeDelegate, del);
	}

	private void OnDrawGizmosSelected()
	{
		Vector3 vector = base.transform.position - base.transform.TransformDirection(Vector3.right * viewableArea.x * 0.5f * base.transform.lossyScale.x) + base.transform.TransformDirection(Vector3.up * viewableArea.y * 0.5f * base.transform.lossyScale.y);
		Vector3 vector2 = base.transform.position - base.transform.TransformDirection(Vector3.right * viewableArea.x * 0.5f * base.transform.lossyScale.x) - base.transform.TransformDirection(Vector3.up * viewableArea.y * 0.5f * base.transform.lossyScale.y);
		Vector3 vector3 = base.transform.position + base.transform.TransformDirection(Vector3.right * viewableArea.x * 0.5f * base.transform.lossyScale.x) - base.transform.TransformDirection(Vector3.up * viewableArea.y * 0.5f * base.transform.lossyScale.y);
		Vector3 vector4 = base.transform.position + base.transform.TransformDirection(Vector3.right * viewableArea.x * 0.5f * base.transform.lossyScale.x) + base.transform.TransformDirection(Vector3.up * viewableArea.y * 0.5f * base.transform.lossyScale.y);
		Gizmos.color = new Color(1f, 0f, 0.5f, 1f);
		Gizmos.DrawLine(vector, vector2);
		Gizmos.DrawLine(vector2, vector3);
		Gizmos.DrawLine(vector3, vector4);
		Gizmos.DrawLine(vector4, vector);
	}

	public static UIScrollList Create(string name, Vector3 pos)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		return (UIScrollList)gameObject.AddComponent(typeof(UIScrollList));
	}

	public static UIScrollList Create(string name, Vector3 pos, Quaternion rotation)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		gameObject.transform.rotation = rotation;
		return (UIScrollList)gameObject.AddComponent(typeof(UIScrollList));
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
