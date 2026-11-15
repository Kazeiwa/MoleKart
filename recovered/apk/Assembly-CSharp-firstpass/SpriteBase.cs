using System;
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
			return animating;
		}
		set
		{
			if (value)
			{
				PlayAnim(curAnimIndex);
			}
		}
	}

	public int CurAnimIndex
	{
		get
		{
			return curAnimIndex;
		}
		set
		{
			curAnimIndex = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
	}

	public override void Clear()
	{
		base.Clear();
		animCompleteDelegate = null;
	}

	public override void Delete()
	{
		if (animating)
		{
			RemoveFromAnimatedList();
			animating = true;
		}
		base.Delete();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (animating)
		{
			RemoveFromAnimatedList();
			animating = true;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (Application.isPlaying && animating)
		{
			animating = false;
			AddToAnimatedList();
		}
	}

	public override void Copy(SpriteRoot s)
	{
		base.Copy(s);
		if (s is SpriteBase)
		{
			SpriteBase spriteBase = (SpriteBase)s;
			defaultAnim = spriteBase.defaultAnim;
			playAnimOnStart = spriteBase.playAnimOnStart;
		}
	}

	public override void Hide(bool tf)
	{
		base.Hide(tf);
		if (tf)
		{
			PauseAnim();
		}
	}

	public void SetAnimCompleteDelegate(AnimCompleteDelegate del)
	{
		animCompleteDelegate = del;
	}

	public void SetAnimFrameDelegate(AnimFrameDelegate del)
	{
		animFrameDelegate = del;
	}

	public void SetSpriteResizedDelegate(SpriteResizedDelegate del)
	{
		resizedDelegate = del;
	}

	public void AddSpriteResizedDelegate(SpriteResizedDelegate del)
	{
		resizedDelegate = (SpriteResizedDelegate)Delegate.Combine(resizedDelegate, del);
	}

	public void RemoveSpriteresizedDelegate(SpriteResizedDelegate del)
	{
		resizedDelegate = (SpriteResizedDelegate)Delegate.Remove(resizedDelegate, del);
	}

	public virtual bool StepAnim(float time)
	{
		return false;
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
		timeBetweenAnimFrames = 1f / fps;
	}

	public void PauseAnim()
	{
		if (animating)
		{
			RemoveFromAnimatedList();
		}
	}

	public virtual void StopAnim()
	{
	}

	public void RevertToStatic()
	{
		if (animating)
		{
			StopAnim();
		}
		InitUVs();
		SetBleedCompensation();
		if (autoResize || pixelPerfect)
		{
			CalcSize();
		}
	}

	protected void AddToAnimatedList()
	{
		if (!animating && Application.isPlaying && base.gameObject.active)
		{
			animating = true;
			SpriteAnimationPump.Add(this);
		}
	}

	protected void RemoveFromAnimatedList()
	{
		SpriteAnimationPump.Remove(this);
		animating = false;
	}

	public bool IsAnimating()
	{
		return animating;
	}
}
