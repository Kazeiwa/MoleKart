using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[AddComponentMenu("EZ GUI/Controls/Label")]
[ExecuteInEditMode]
public class SpriteText : MonoBehaviour, IUseCamera
{
	public enum Anchor_Pos
	{
		Upper_Left,
		Upper_Center,
		Upper_Right,
		Middle_Left,
		Middle_Center,
		Middle_Right,
		Lower_Left,
		Lower_Center,
		Lower_Right
	}

	public enum Alignment_Type
	{
		Left,
		Center,
		Right
	}

	protected struct NewlineInsertInfo
	{
		public int index;

		public int charDelta;

		public NewlineInsertInfo(int idx, int delta)
		{
		}
	}

	[HideInInspector]
	public const string colorTag = "[#";

	public string text;

	public float offsetZ;

	public float characterSize;

	public float characterSpacing;

	public float lineSpacing;

	protected float lineSpaceSize;

	public Anchor_Pos anchor;

	public Alignment_Type alignment;

	public int tabSize;

	protected string tabSpaces;

	public TextAsset font;

	public Color color;

	public bool pixelPerfect;

	public float maxWidth;

	public bool dynamicLength;

	public bool removeUnsupportedCharacters;

	public bool parseColorTags;

	public bool password;

	public string maskingCharacter;

	protected EZScreenPlacement screenPlacer;

	private IControl parentControl;

	protected bool clipped;

	protected bool updateClipping;

	protected Rect3D clippingRect;

	protected Rect localClipRect;

	protected Vector3 topLeft;

	protected Vector3 bottomRight;

	protected Vector3 unclippedTL;

	protected Vector3 unclippedBR;

	protected Color[] colors;

	protected bool updateColors;

	protected string[] colDel;

	[HideInInspector]
	public bool isClone;

	protected bool m_started;

	protected bool stringContentChanged;

	protected Vector2 screenSize;

	public Camera renderCamera;

	[HideInInspector]
	public Vector2 pixelsPerUV;

	protected float worldUnitsPerScreenPixel;

	protected float worldUnitsPerTexel;

	protected Vector2 worldUnitsPerUV;

	public bool hideAtStart;

	protected bool m_hidden;

	public bool persistent;

	public bool ignoreClipping;

	protected int capacity;

	protected string meshString;

	protected string plainText;

	protected string displayString;

	protected List<NewlineInsertInfo> newLineInserts;

	protected float totalWidth;

	protected SpriteFont spriteFont;

	protected SpriteTextMirror mirror;

	protected Mesh oldMesh;

	protected Mesh mesh;

	protected MeshRenderer meshRenderer;

	protected MeshFilter meshFilter;

	protected Texture texture;

	protected Vector3[] vertices;

	protected int[] faces;

	protected Vector2[] UVs;

	protected Color[] meshColors;

	private StringBuilder displaySB;

	private StringBuilder plainSB;

	private List<int> colorInserts;

	private List<int> colorTags;

	private List<Color> cols;

	private string[] lines;

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

	public bool Persistent
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public string Text
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public string PlainText
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public string DisplayString
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

	public float BaseHeight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float LineSpan
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

	public float CharacterSpacing
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public IControl Parent
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

	protected virtual void Init()
	{
	}

	protected void ProcessString(string str)
	{
	}

	protected Color ParseColor(string str)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected Color ParseHexColor(string str)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void EnlargeMesh()
	{
	}

	public void UpdateMesh()
	{
	}

	protected Vector3 GetStartPos_SingleLine(float baseHeight, float width)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetDisplayLineCount(int charIndex, out int charLine, out int lineStart, out int lineEnd)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetDisplayLineCount()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int PlainIndexToDisplayIndex(int plainCharIndex)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected float GetLineBaseline(int numLines, int lineNum)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void Layout_Single_Line()
	{
	}

	protected void Layout_Multiline(string[] lines)
	{
	}

	protected void ZeroQuad(int i)
	{
	}

	protected void BuildCharacter(int vertNum, int charNum, Vector3 upperLeft, ref SpriteChar ch)
	{
	}

	protected void Layout_Line(Vector3 startPos, string txt, int charIdx)
	{
	}

	protected void ClearMesh()
	{
	}

	public void Unclip()
	{
	}

	public void Delete()
	{
	}

	private void OnEnable()
	{
	}

	protected virtual void OnDisable()
	{
	}

	public virtual void OnDestroy()
	{
	}

	public virtual void Copy(SpriteText s)
	{
	}

	public void CalcSize()
	{
	}

	protected void LayoutText()
	{
	}

	public void SetColor(Color c)
	{
	}

	public void SetCharacterSize(float size)
	{
	}

	public void SetLineSpacing(float spacing)
	{
	}

	public void SetFont(TextAsset newFont, Material fontMaterial)
	{
	}

	public void SetFont(SpriteFont newFont, Material fontMaterial)
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

	public void SetCamera(Camera c)
	{
	}

	public virtual void Hide(bool tf)
	{
	}

	public bool IsHidden()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetInsertionPointPos(int charIndex)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetNearestInsertionPoint(Vector3 point, ref int insertionPt)
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

	public void SetAnchor(Anchor_Pos a)
	{
	}

	public void SetAlignment(Alignment_Type a)
	{
	}

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
