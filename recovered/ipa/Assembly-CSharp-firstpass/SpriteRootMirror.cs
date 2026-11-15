using UnityEngine;

public class SpriteRootMirror
{
	public bool managed;

	public SpriteManager manager;

	public int drawLayer;

	public SpriteRoot.SPRITE_PLANE plane;

	public SpriteRoot.WINDING_ORDER winding;

	public float width;

	public float height;

	public Vector2 bleedCompensation;

	public SpriteRoot.ANCHOR_METHOD anchor;

	public Vector3 offset;

	public Color color;

	public bool pixelPerfect;

	public bool autoResize;

	public Camera renderCamera;

	public bool hideAtStart;

	public virtual void Mirror(SpriteRoot s)
	{
	}

	public virtual bool Validate(SpriteRoot s)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual bool DidChange(SpriteRoot s)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected virtual void HandleManageState(SpriteRoot s)
	{
	}

	public virtual void UpdateManager(SpriteRoot s)
	{
	}

	protected virtual void HandleDrawLayerChange(SpriteRoot s)
	{
	}
}
