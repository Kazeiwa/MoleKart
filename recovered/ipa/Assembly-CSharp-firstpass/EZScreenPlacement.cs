using System;
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

	[Serializable]
	public class RelativeTo
	{
		public HORIZONTAL_ALIGN horizontal;

		public VERTICAL_ALIGN vertical;

		public bool Equals(RelativeTo rt)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public void Copy(RelativeTo rt)
		{
		}
	}

	public Camera renderCamera;

	public Vector3 screenPos;

	public RelativeTo relativeTo;

	public Transform relativeObject;

	public bool alwaysRecursive;

	protected Vector2 screenSize;

	protected EZScreenPlacementMirror mirror;

	protected bool m_awake;

	public Camera RenderCamera
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

	public void PositionOnScreenRecursively()
	{
	}

	public void PositionOnScreen()
	{
	}

	public void PositionOnScreen(int x, int y, float depth)
	{
	}

	public void PositionOnScreen(Vector3 pos)
	{
	}

	public void SetCamera()
	{
	}

	public void SetCamera(Camera c)
	{
	}

	public static bool TestDepenency(EZScreenPlacement sp)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void DoMirror()
	{
	}

	public virtual void OnDrawGizmosSelected()
	{
	}

	public virtual void OnDrawGizmos()
	{
	}
}
