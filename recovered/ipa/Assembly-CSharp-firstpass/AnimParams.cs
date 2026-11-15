using System;
using UnityEngine;

[Serializable]
public class AnimParams
{
	public EZAnimation.ANIM_MODE mode;

	public float delay;

	public float duration;

	public bool pingPong;

	public bool restartOnRepeat;

	public bool repeatDelay;

	public EZAnimation.EASING_TYPE easing;

	[NonSerialized]
	protected EZTransition m_transition;

	public Color color;

	public Color color2;

	public Vector3 vec;

	public Vector3 vec2;

	public Vector3 axis;

	public float floatVal;

	public float floatVal2;

	public string strVal;

	public EZTransition transition
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public AnimParams(EZTransition trans)
	{
	}

	public void Copy(AnimParams src)
	{
	}

	public virtual void DrawGUI(EZAnimation.ANIM_TYPE type, GameObject go, IGUIHelper gui, bool inspector)
	{
	}
}
