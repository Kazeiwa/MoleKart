using UnityEngine;

public class SpriteTextMirror
{
	public string text;

	public TextAsset font;

	public float offsetZ;

	public float characterSize;

	public float characterSpacing;

	public float lineSpacing;

	public SpriteText.Anchor_Pos anchor;

	public SpriteText.Alignment_Type alignment;

	public int tabSize;

	public Color color;

	public float maxWidth;

	public bool pixelPerfect;

	public Camera renderCamera;

	public bool hideAtStart;

	public virtual void Mirror(SpriteText s)
	{
	}

	public virtual bool Validate(SpriteText s)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual bool DidChange(SpriteText s)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
