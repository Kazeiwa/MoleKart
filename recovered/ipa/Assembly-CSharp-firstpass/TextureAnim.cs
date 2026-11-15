using System;
using UnityEngine;

[Serializable]
public class TextureAnim
{
	public string name;

	public int loopCycles;

	public bool loopReverse;

	public float framerate;

	public UVAnimation.ANIM_END_ACTION onAnimEnd;

	[HideInInspector]
	public string[] framePaths;

	[HideInInspector]
	public string[] frameGUIDs;

	[HideInInspector]
	public CSpriteFrame[] spriteFrames;

	public TextureAnim()
	{
	}

	public TextureAnim(string n)
	{
	}

	public void Allocate()
	{
	}

	public void Copy(TextureAnim a)
	{
	}
}
