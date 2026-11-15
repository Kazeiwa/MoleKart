using UnityEngine;

[ExecuteInEditMode]
public abstract class SpriteRoot : MonoBehaviour, IEZLinkedListItem<SpriteRoot>, IUseCamera
{
	public enum SPRITE_PLANE
	{
		XY,
		XZ,
		YZ
	}

	public enum ANCHOR_METHOD
	{
		UPPER_LEFT,
		UPPER_CENTER,
		UPPER_RIGHT,
		MIDDLE_LEFT,
		MIDDLE_CENTER,
		MIDDLE_RIGHT,
		BOTTOM_LEFT,
		BOTTOM_CENTER,
		BOTTOM_RIGHT,
		TEXTURE_OFFSET
	}

	public enum WINDING_ORDER
	{
		CCW,
		CW
	}

	public delegate void SpriteResizedDelegate(float newWidth, float newHeight, SpriteRoot sprite);

	public bool managed;

	public SpriteManager manager;

	protected bool addedToManager;

	public int drawLayer;

	public bool persistent;

	public SPRITE_PLANE plane;

	public WINDING_ORDER winding;

	public float width;

	public float height;

	public Vector2 bleedCompensation;

	public ANCHOR_METHOD anchor;

	public bool pixelPerfect;

	public bool autoResize;

	protected Vector2 bleedCompensationUV;

	protected Vector2 bleedCompensationUVMax;

	protected SPRITE_FRAME frameInfo;

	protected Rect uvRect;

	protected Vector2 scaleFactor;

	protected Vector2 topLeftOffset;

	protected Vector2 bottomRightOffset;

	protected Vector3 topLeft;

	protected Vector3 bottomRight;

	protected Vector3 unclippedTopLeft;

	protected Vector3 unclippedBottomRight;

	protected Vector2 tlTruncate;

	protected Vector2 brTruncate;

	protected bool truncated;

	protected Rect3D clippingRect;

	protected Rect localClipRect;

	protected float leftClipPct;

	protected float rightClipPct;

	protected float topClipPct;

	protected float bottomClipPct;

	protected bool clipped;

	[HideInInspector]
	public bool billboarded;

	[HideInInspector]
	public bool isClone;

	protected bool m_started;

	protected bool deleted;

	public Vector3 offset;

	public Color color;

	protected ISpriteMesh m_spriteMesh;

	protected SpriteRoot m_prev;

	protected SpriteRoot m_next;

	protected Vector2 screenSize;

	public Camera renderCamera;

	protected Vector2 sizeUnitsPerUV;

	[HideInInspector]
	public Vector2 pixelsPerUV;

	protected float worldUnitsPerScreenPixel;

	protected SpriteResizedDelegate resizedDelegate;

	protected EZScreenPlacement screenPlacer;

	public bool hideAtStart;

	protected bool m_hidden;

	public bool ignoreClipping;

	protected SpriteRootMirror mirror;

	protected Vector2 tempUV;

	protected Mesh oldMesh;

	protected SpriteManager savedManager;

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

	public bool Managed
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool Started
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual Rect3D ClippingRect
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual bool Clipped
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Vector3 UnclippedTopLeft
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 UnclippedBottomRight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 TopLeft
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 BottomRight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ISpriteMesh spriteMesh
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool AddedToManager
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public SpriteRoot prev
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public SpriteRoot next
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	protected virtual void Awake()
	{
	}

	public virtual void Start()
	{
	}

	protected void CalcSizeUnitsPerUV()
	{
	}

	protected virtual void Init()
	{
	}

	public virtual void Clear()
	{
	}

	public virtual void Copy(SpriteRoot s)
	{
	}

	public virtual void InitUVs()
	{
	}

	public virtual void Delete()
	{
	}

	protected virtual void OnEnable()
	{
	}

	protected virtual void OnDisable()
	{
	}

	public virtual void OnDestroy()
	{
	}

	public void CalcEdges()
	{
	}

	public void CalcSize()
	{
	}

	public virtual void SetSize(float width, float height)
	{
	}

	protected void SetSizeXY(float w, float h)
	{
	}

	protected void SetSizeXZ(float w, float h)
	{
	}

	protected void SetSizeYZ(float w, float h)
	{
	}

	public virtual void TruncateRight(float pct)
	{
	}

	public virtual void TruncateLeft(float pct)
	{
	}

	public virtual void TruncateTop(float pct)
	{
	}

	public virtual void TruncateBottom(float pct)
	{
	}

	public virtual void Untruncate()
	{
	}

	public virtual void Unclip()
	{
	}

	public virtual void UpdateUVs()
	{
	}

	public void TransformBillboarded(Transform t)
	{
	}

	public virtual void SetColor(Color c)
	{
	}

	public void SetPixelToUV(int texWidth, int texHeight)
	{
	}

	public void SetPixelToUV(Texture tex)
	{
	}

	public void SetCamera()
	{
	}

	public virtual void SetCamera(Camera c)
	{
	}

	public virtual void Hide(bool tf)
	{
	}

	public bool IsHidden()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void DestroyMesh()
	{
	}

	protected void AddMesh()
	{
	}

	public void SetBleedCompensation()
	{
	}

	public void SetBleedCompensation(float x, float y)
	{
	}

	public void SetBleedCompensation(Vector2 xy)
	{
	}

	public void SetPlane(SPRITE_PLANE p)
	{
	}

	public void SetWindingOrder(WINDING_ORDER order)
	{
	}

	public void SetDrawLayer(int layer)
	{
	}

	public void SetFrameInfo(SPRITE_FRAME fInfo)
	{
	}

	public void SetUVs(Rect uv)
	{
	}

	public void SetUVsFromPixelCoords(Rect pxCoords)
	{
	}

	public Rect GetUVs()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3[] GetVertices()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetCenterPoint()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetAnchor(ANCHOR_METHOD a)
	{
	}

	public void SetOffset(Vector3 o)
	{
	}

	public abstract Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader);

	public Vector2 PixelSpaceToUVSpace(Vector2 xy)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PixelSpaceToUVSpace(int x, int y)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PixelCoordToUVCoord(Vector2 xy)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PixelCoordToUVCoord(int x, int y)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public abstract int GetStateIndex(string stateName);

	public abstract void SetState(int index);

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
