using UnityEngine;

[ExecuteInEditMode]
public abstract class SpriteBase : SpriteRoot
{
	public delegate void AnimCompleteDelegate(SpriteBase sprite);

	public delegate void AnimFrameDelegate(SpriteBase sprite, int frame);

	public bool playAnimOnStart;

	public int defaultAnim;

	protected int curAnimIndex;

	protected AnimCompleteDelegate animCompleteDelegate;

	protected AnimFrameDelegate animFrameDelegate;

	protected float timeSinceLastFrame;

	protected float timeBetweenAnimFrames;

	protected int framesToAdvance;

	protected bool animating;

	public bool Animating
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public int CurAnimIndex
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	protected override void Awake()
	{
	}

	public override void Clear()
	{
	}

	public override void Delete()
	{
	}

	protected override void OnDisable()
	{
	}

	protected override void OnEnable()
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Hide(bool tf)
	{
	}

	public void SetAnimCompleteDelegate(AnimCompleteDelegate del)
	{
	}

	public void SetAnimFrameDelegate(AnimFrameDelegate del)
	{
	}

	public void SetSpriteResizedDelegate(SpriteResizedDelegate del)
	{
	}

	public void AddSpriteResizedDelegate(SpriteResizedDelegate del)
	{
	}

	public void RemoveSpriteresizedDelegate(SpriteResizedDelegate del)
	{
	}

	public virtual bool StepAnim(float time)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void PlayAnim(int index)
	{
	}

	public virtual void PlayAnim(string name)
	{
	}

	public virtual void PlayAnimInReverse(int index)
	{
	}

	public virtual void PlayAnimInReverse(string name)
	{
	}

	public void SetFramerate(float fps)
	{
	}

	public void PauseAnim()
	{
	}

	public virtual void StopAnim()
	{
	}

	public void RevertToStatic()
	{
	}

	protected void AddToAnimatedList()
	{
	}

	protected void RemoveFromAnimatedList()
	{
	}

	public bool IsAnimating()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
