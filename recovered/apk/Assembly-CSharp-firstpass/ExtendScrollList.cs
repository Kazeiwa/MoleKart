using System.Collections.Generic;
using UnityEngine;

public class ExtendScrollList : UIScrollList
{
	private float fCount;

	private float fTime;

	private float fFrameRate;

	private void UpdateListItemActtachTextClippingRect(IUIListObject obj)
	{
		if (obj == null)
		{
			return;
		}
		Transform[] componentsInChildren = obj.gameObject.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			SpriteText spriteText = (SpriteText)transform.gameObject.GetComponentInChildren(typeof(SpriteText));
			if (spriteText != null)
			{
				spriteText.ClippingRect = clientClippingRect;
			}
		}
	}

	protected override void ClipItems()
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
		List<IUIListObject> list = visibleItems;
		visibleItems = tempVisItems;
		tempVisItems = list;
		tempVisItems.Clear();
		for (int m = 0; m < visibleItems.Count; m++)
		{
			UpdateListItemActtachTextClippingRect(visibleItems[m]);
		}
	}

	public IUIListObject CreateItem(GameObject prefab, int position, List<string> lstRowElements)
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
				gameObject = (GameObject)Object.Instantiate(prefab);
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
			gameObject = (GameObject)Object.Instantiate(prefab);
		}
		iUIListObject = (IUIListObject)gameObject.GetComponent(typeof(IUIListObject));
		if (iUIListObject == null)
		{
			return null;
		}
		InsertItem(iUIListObject, position, lstRowElements);
		return iUIListObject;
	}

	public void InsertItem(IUIListObject item, int position, List<string> lstRowElements)
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
		item.transform.localPosition = Vector3.zero;
		item.SetList(this);
		setListItemText(item, lstRowElements);
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

	private void setListItemText(IUIListObject listObj, List<string> lstRowElements)
	{
		if (listObj == null)
		{
			return;
		}
		int num = 0;
		Transform[] componentsInChildren = listObj.gameObject.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (num >= 1)
			{
				SpriteText spriteText = (SpriteText)transform.gameObject.GetComponentInChildren(typeof(SpriteText));
				if (lstRowElements.Count >= num)
				{
					spriteText.Text = lstRowElements[num - 1];
				}
				else
				{
					spriteText.Text = string.Empty;
				}
			}
			num++;
		}
	}

	public override void ListDragged(POINTER_INFO ptr)
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

	private void calFps()
	{
		fCount += 1f;
		fTime += Time.deltaTime;
		if (fTime > 0.5f)
		{
			fFrameRate = fCount / fTime;
			fTime = 0f;
			fCount = 0f;
		}
	}
}
