using UnityEngine;

[ExecuteInEditMode]
public class SimpleSprite : SpriteRoot
{
	public Vector2 lowerLeftPixel;

	public Vector2 pixelDimensions;

	public override Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected override void Awake()
	{
	}

	protected override void Init()
	{
	}

	public override void Start()
	{
	}

	public override void Clear()
	{
	}

	public void Setup(float w, float h, Vector2 lowerleftPixel, Vector2 pixeldimensions)
	{
	}

	public void Setup(float w, float h, Vector2 lowerleftPixel, Vector2 pixeldimensions, Material material)
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void InitUVs()
	{
	}

	public void SetLowerLeftPixel(Vector2 lowerLeft)
	{
	}

	public void SetLowerLeftPixel(int x, int y)
	{
	}

	public void SetPixelDimensions(Vector2 size)
	{
	}

	public void SetPixelDimensions(int x, int y)
	{
	}

	public override int GetStateIndex(string stateName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void SetState(int index)
	{
	}

	public static SimpleSprite Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static SimpleSprite Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void DoMirror()
	{
	}
}
