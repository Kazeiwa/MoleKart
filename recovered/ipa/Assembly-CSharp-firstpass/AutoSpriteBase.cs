using UnityEngine;

public abstract class AutoSpriteBase : SpriteBase, ISpriteAggregator
{
	protected Texture2D[] sourceTextures;

	protected CSpriteFrame[] spriteFrames;

	[HideInInspector]
	public UVAnimation[] animations;

	protected UVAnimation curAnim;

	public abstract TextureAnim[] States { get; set; }

	public virtual CSpriteFrame DefaultFrame
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual TextureAnim DefaultState
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Texture2D[] SourceTextures
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public CSpriteFrame[] SpriteFrames
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected override void Awake()
	{
	}

	public override void Clear()
	{
	}

	public void Setup(float w, float h)
	{
	}

	public void Setup(float w, float h, Material material)
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public virtual void CopyAll(SpriteRoot s)
	{
	}

	public override bool StepAnim(float time)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PlayAnim(UVAnimation anim, int frame)
	{
	}

	public void PlayAnim(UVAnimation anim)
	{
	}

	public void PlayAnim(int index, int frame)
	{
	}

	public override void PlayAnim(int index)
	{
	}

	public void PlayAnim(string name, int frame)
	{
	}

	public override void PlayAnim(string name)
	{
	}

	public void PlayAnimInReverse(UVAnimation anim)
	{
	}

	public void PlayAnimInReverse(UVAnimation anim, int frame)
	{
	}

	public override void PlayAnimInReverse(int index)
	{
	}

	public void PlayAnimInReverse(int index, int frame)
	{
	}

	public override void PlayAnimInReverse(string name)
	{
	}

	public void PlayAnimInReverse(string name, int frame)
	{
	}

	public void DoAnim(int index)
	{
	}

	public void DoAnim(string name)
	{
	}

	public void DoAnim(UVAnimation anim)
	{
	}

	public void SetCurFrame(int index)
	{
	}

	public override void StopAnim()
	{
	}

	public void UnpauseAnim()
	{
	}

	public UVAnimation GetCurAnim()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public UVAnimation GetAnim(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override int GetStateIndex(string stateName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void SetState(int index)
	{
	}

	public virtual void Aggregate(PathFromGUIDDelegate guid2Path, LoadAssetDelegate load, GUIDFromPathDelegate path2Guid)
	{
	}
}
