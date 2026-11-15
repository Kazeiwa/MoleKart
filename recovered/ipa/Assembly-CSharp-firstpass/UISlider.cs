using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Slider")]
public class UISlider : AutoSpriteControlBase
{
	protected float m_value;

	public MonoBehaviour scriptWithMethodToInvoke;

	public string methodToInvoke;

	public float defaultValue;

	public float stopKnobFromEdge;

	public float knobFloatDistance;

	public Vector2 knobSize;

	public Vector2 knobColliderSizeFactor;

	protected AutoSprite emptySprite;

	protected UIScrollKnob knob;

	[HideInInspector]
	public TextureAnim[] states;

	[HideInInspector]
	public EZTransitionList[] transitions;

	public SpriteRoot[] filledLayers;

	public SpriteRoot[] emptyLayers;

	public SpriteRoot[] knobLayers;

	protected float truncFloor;

	protected float truncRange;

	protected int[] filledIndices;

	protected int[] emptyIndices;

	public override bool controlIsEnabled
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float Value
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override TextureAnim[] States
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

	public override bool Clipped
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override Rect3D ClippingRect
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public override EZTransitionList GetTransitions(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected override void Awake()
	{
	}

	public override void Start()
	{
	}

	public override void SetSize(float width, float height)
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	protected Vector3 CalcKnobStartPos()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void InitUVs()
	{
	}

	protected void UpdateValue()
	{
	}

	public void ScrollKnobMoved(UIScrollKnob knob, float val)
	{
	}

	public override void SetInputDelegate(EZInputDelegate del)
	{
	}

	public override void AddInputDelegate(EZInputDelegate del)
	{
	}

	public override void RemoveInputDelegate(EZInputDelegate del)
	{
	}

	protected void UpdateAppearance(float truncVal)
	{
	}

	public UIScrollKnob GetKnob()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Unclip()
	{
	}

	public static UISlider Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static UISlider Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Hide(bool tf)
	{
	}

	public override void SetColor(Color c)
	{
	}
}
