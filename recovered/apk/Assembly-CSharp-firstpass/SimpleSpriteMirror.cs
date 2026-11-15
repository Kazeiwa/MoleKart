using UnityEngine;

public class SimpleSpriteMirror : SpriteRootMirror
{
	public Vector2 lowerLeftPixel;

	public Vector2 pixelDimensions;

	public override void Mirror(SpriteRoot s)
	{
		base.Mirror(s);
		lowerLeftPixel = ((SimpleSprite)s).lowerLeftPixel;
		pixelDimensions = ((SimpleSprite)s).pixelDimensions;
	}

	public override bool DidChange(SpriteRoot s)
	{
		if (base.DidChange(s))
		{
			return true;
		}
		if (((SimpleSprite)s).lowerLeftPixel != lowerLeftPixel)
		{
			return true;
		}
		if (((SimpleSprite)s).pixelDimensions != pixelDimensions)
		{
			return true;
		}
		return false;
	}
}
