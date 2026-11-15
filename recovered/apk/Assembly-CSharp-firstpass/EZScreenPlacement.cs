using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[AddComponentMenu("EZ GUI/Utility/EZ Screen Placement")]
[ExecuteInEditMode]
public class EZScreenPlacement : MonoBehaviour, IUseCamera
{
	public enum HORIZONTAL_ALIGN
	{
		NONE,
		SCREEN_LEFT,
		SCREEN_RIGHT,
		SCREEN_CENTER,
		OBJECT
	}

	public enum VERTICAL_ALIGN
	{
		NONE,
		SCREEN_TOP,
		SCREEN_BOTTOM,
		SCREEN_CENTER,
		OBJECT
	}

	public enum SRTETCH_TYPE
	{
		STRETCH_NONE,
		STRETCH_HORIZONTAL,
		STRETCH_VERTICAL,
		STRETCH_BOTH
	}

	[Serializable]
	public class RelativeTo
	{
		public HORIZONTAL_ALIGN horizontal = HORIZONTAL_ALIGN.SCREEN_LEFT;

		public VERTICAL_ALIGN vertical = VERTICAL_ALIGN.SCREEN_TOP;

		public bool Equals(RelativeTo rt)
		{
			if (rt == null)
			{
				return false;
			}
			return horizontal == rt.horizontal && vertical == rt.vertical;
		}

		public void Copy(RelativeTo rt)
		{
			if (rt != null)
			{
				horizontal = rt.horizontal;
				vertical = rt.vertical;
			}
		}
	}

	public Camera renderCamera;

	public Vector3 screenPos = Vector3.forward;

	public bool useRelativeX = true;

	public bool useRelativeY = true;

	public SRTETCH_TYPE stretchType;

	public RelativeTo relativeTo = new RelativeTo();

	public Transform relativeObject;

	public bool alwaysRecursive = true;

	protected Vector2 screenSize;

	protected EZScreenPlacementMirror mirror = new EZScreenPlacementMirror();

	protected bool m_awake;

	public Camera RenderCamera
	{
		get
		{
			return renderCamera;
		}
		set
		{
			SetCamera(value);
		}
	}

	private void Awake()
	{
		if (!m_awake)
		{
			m_awake = true;
			IUseCamera useCamera = (IUseCamera)GetComponent("IUseCamera");
			if (useCamera != null)
			{
				renderCamera = useCamera.RenderCamera;
			}
			if (renderCamera == null)
			{
				renderCamera = Camera.main;
			}
			screenSize.x = renderCamera.pixelWidth;
			screenSize.y = renderCamera.pixelHeight;
			PositionOnScreenRecursively();
		}
	}

	public void PositionOnScreenRecursively()
	{
		if (!m_awake)
		{
			Awake();
		}
		if (relativeObject != null)
		{
			EZScreenPlacement component = relativeObject.GetComponent<EZScreenPlacement>();
			if (component != null)
			{
				component.PositionOnScreenRecursively();
			}
		}
		PositionOnScreen();
	}

	public void PositionOnScreen()
	{
		if (!m_awake)
		{
			Awake();
		}
		if (renderCamera == null)
		{
			Debug.LogError("Render camera not yet assigned to EZScreenPlacement component of \"" + base.name + "\" when attempting to call PositionOnScreen()");
			return;
		}
		Vector3 vector = renderCamera.WorldToScreenPoint(base.transform.position);
		Vector3 position = screenPos;
		if (useRelativeX)
		{
			position.x *= screenSize.x;
		}
		if (useRelativeY)
		{
			position.y *= screenSize.y;
		}
		if (stretchType != SRTETCH_TYPE.STRETCH_NONE)
		{
			Vector3 vector2 = renderCamera.ScreenToWorldPoint(Vector3.zero);
			float x = base.transform.localScale.x;
			float y = base.transform.localScale.y;
			if (stretchType == SRTETCH_TYPE.STRETCH_HORIZONTAL || stretchType == SRTETCH_TYPE.STRETCH_BOTH)
			{
				x = Mathf.Abs(vector2.x) * 2f;
			}
			if (stretchType == SRTETCH_TYPE.STRETCH_VERTICAL || stretchType == SRTETCH_TYPE.STRETCH_BOTH)
			{
				y = Mathf.Abs(vector2.y) * 2f;
			}
			base.transform.localScale = new Vector3(x, y, base.transform.localScale.z);
		}
		switch (relativeTo.horizontal)
		{
		case HORIZONTAL_ALIGN.SCREEN_RIGHT:
			position.x = screenSize.x + position.x;
			break;
		case HORIZONTAL_ALIGN.SCREEN_CENTER:
			position.x = screenSize.x * 0.5f + position.x;
			break;
		case HORIZONTAL_ALIGN.OBJECT:
			if (relativeObject != null)
			{
				position.x = renderCamera.WorldToScreenPoint(relativeObject.position).x + position.x;
			}
			else
			{
				position.x = vector.x;
			}
			break;
		case HORIZONTAL_ALIGN.NONE:
			position.x = vector.x;
			break;
		}
		switch (relativeTo.vertical)
		{
		case VERTICAL_ALIGN.SCREEN_TOP:
			position.y = screenSize.y + position.y;
			break;
		case VERTICAL_ALIGN.SCREEN_CENTER:
			position.y = screenSize.y * 0.5f + position.y;
			break;
		case VERTICAL_ALIGN.OBJECT:
			if (relativeObject != null)
			{
				position.y = renderCamera.WorldToScreenPoint(relativeObject.position).y + position.y;
			}
			else
			{
				position.y = vector.y;
			}
			break;
		case VERTICAL_ALIGN.NONE:
			position.y = vector.y;
			break;
		}
		base.transform.position = renderCamera.ScreenToWorldPoint(position);
	}

	public void PositionOnScreen(int x, int y, float depth)
	{
		PositionOnScreen(new Vector3(x, y, depth));
	}

	public void PositionOnScreen(Vector3 pos)
	{
		screenPos = pos;
		PositionOnScreen();
	}

	public void SetCamera()
	{
		SetCamera(renderCamera);
	}

	public void SetCamera(Camera c)
	{
		if (!(c == null))
		{
			renderCamera = c;
			screenSize.x = renderCamera.pixelWidth;
			screenSize.y = renderCamera.pixelHeight;
			if (alwaysRecursive || (Application.isEditor && !Application.isPlaying))
			{
				PositionOnScreenRecursively();
			}
			else
			{
				PositionOnScreen();
			}
		}
	}

	public static bool TestDepenency(EZScreenPlacement sp)
	{
		if (sp.relativeObject == null)
		{
			return true;
		}
		List<EZScreenPlacement> list = new List<EZScreenPlacement>();
		list.Add(sp);
		EZScreenPlacement component = sp.relativeObject.GetComponent<EZScreenPlacement>();
		while (component != null)
		{
			if (list.Contains(component))
			{
				return false;
			}
			list.Add(component);
			if (component.relativeObject == null)
			{
				return true;
			}
			component = component.relativeObject.GetComponent<EZScreenPlacement>();
		}
		return true;
	}

	public virtual void DoMirror()
	{
		if (!Application.isPlaying)
		{
			if (mirror == null)
			{
				mirror = new EZScreenPlacementMirror();
				mirror.Mirror(this);
			}
			mirror.Validate(this);
			if (mirror.DidChange(this))
			{
				SetCamera(renderCamera);
				mirror.Mirror(this);
			}
		}
	}

	public virtual void OnDrawGizmosSelected()
	{
		DoMirror();
	}

	public virtual void OnDrawGizmos()
	{
		DoMirror();
	}
}
