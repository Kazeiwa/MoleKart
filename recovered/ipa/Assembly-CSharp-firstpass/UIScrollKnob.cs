using UnityEngine;

public class UIScrollKnob : UIButton
{
	protected Vector3 origPos;

	protected UISlider slider;

	protected float maxScrollPos;

	protected Plane ctrlPlane;

	protected Vector2 colliderSizeFactor;

	protected float colliderExtent;

	private float dist;

	private Vector3 inputPoint;

	private Vector3 newPos;

	private Vector3 prevPoint;

	protected override void Awake()
	{
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
	}

	public void SetStartPos(Vector3 startPos)
	{
	}

	protected Vector3 GetLocalInputPoint(Ray ray)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	public void SetColliderSizeFactor(Vector2 csf)
	{
	}

	public override void UpdateCollider()
	{
	}

	public float GetScrollPos()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetPosition(float pos)
	{
	}

	public void SetSlider(UISlider s)
	{
	}

	public UISlider GetSlider()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetMaxScroll(float max)
	{
	}

	public void SetupAppearance()
	{
	}

	public new static UIScrollKnob Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public new static UIScrollKnob Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
