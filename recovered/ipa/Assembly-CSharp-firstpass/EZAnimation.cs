using UnityEngine;

public abstract class EZAnimation : IEZLinkedListItem<EZAnimation>
{
	public enum ANIM_TYPE
	{
		AnimClip,
		FadeSprite,
		FadeMaterial,
		FadeText,
		Translate,
		PunchPosition,
		Crash,
		SmoothCrash,
		Shake,
		Scale,
		PunchScale,
		Rotate,
		PunchRotation,
		ShakeRotation,
		CrashRotation,
		FadeAudio,
		TuneAudio
	}

	public enum ANIM_MODE
	{
		By,
		To,
		FromTo
	}

	public enum EASING_TYPE
	{
		Linear,
		BackIn,
		BackOut,
		BackInOut,
		BackOutIn,
		BounceIn,
		BounceOut,
		BounceInOut,
		BounceOutIn,
		CircularIn,
		CircularOut,
		CircularInOut,
		CircularOutIn,
		CubicIn,
		CubicOut,
		CubicInOut,
		CubicOutIn,
		ElasticIn,
		ElasticOut,
		ElasticInOut,
		ElasticOutIn,
		ExponentialIn,
		ExponentialOut,
		ExponentialInOut,
		ExponentialOutIn,
		QuadraticIn,
		QuadraticOut,
		QuadraticInOut,
		QuadraticOutIn,
		QuarticIn,
		QuarticOut,
		QuarticInOut,
		QuarticOutIn,
		QuinticIn,
		QuinticOut,
		QuinticInOut,
		QuinticOutIn,
		SinusoidalIn,
		SinusoidalOut,
		SinusoidalInOut,
		SinusoidalOutIn,
		Spring
	}

	public delegate void CompletionDelegate(EZAnimation anim);

	public delegate float Interpolator(float time, float start, float delta, float duration);

	public ANIM_TYPE type;

	public bool pingPong;

	public bool repeatDelay;

	public bool restartOnRepeat;

	public bool running;

	protected bool m_paused;

	protected object data;

	protected ANIM_MODE m_mode;

	protected float direction;

	protected float timeElapsed;

	protected float wait;

	protected float m_wait;

	protected float duration;

	protected float interval;

	protected CompletionDelegate completedDelegate;

	protected CompletionDelegate startDelegate;

	protected Interpolator interpolator;

	protected EZAnimation m_prev;

	protected EZAnimation m_next;

	public object Data
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float Duration
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float Wait
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool Paused
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public ANIM_MODE Mode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public CompletionDelegate CompletedDelegate
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public CompletionDelegate StartDelegate
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public EZAnimation prev
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public EZAnimation next
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public void Clear()
	{
	}

	public abstract bool Start(GameObject sub, AnimParams parms);

	protected abstract void DoAnim();

	public virtual bool Step(float timeDelta)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void Stop()
	{
	}

	public void _stop()
	{
	}

	public void End()
	{
	}

	public void _cancel()
	{
	}

	public virtual void _end()
	{
	}

	protected virtual void LoopReset()
	{
	}

	public abstract object GetSubject();

	protected virtual void WaitDone()
	{
	}

	protected void StartCommon()
	{
	}

	public static float linear(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quadraticIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quadraticOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quadraticInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quadraticOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float cubicIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float cubicOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float cubicInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float cubicOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quarticIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quarticOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quarticInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quarticOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quinticIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quinticOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quinticInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float quinticOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float sinusIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float sinusOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float sinusInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float sinusOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float expIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float expOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float expInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float expOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float circIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float circOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float circInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float circOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float punch(float amplitude, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float spring(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float elasticIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float elasticIn(float time, float start, float delta, float duration, float amplitude, float period)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float elasticOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float elasticOut(float time, float start, float delta, float duration, float amplitude, float period)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float elasticInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float elasticInOut(float time, float start, float delta, float duration, float amplitude, float period)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float elasticOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float elasticOutIn(float time, float start, float delta, float duration, float amplitude, float period)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float backIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float backIn(float time, float start, float delta, float duration, float overshootAmt)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float backOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float backOut(float time, float start, float delta, float duration, float overshootAmt)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float backInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float backInOut(float time, float start, float delta, float duration, float overshootAmt)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float backOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float backOutIn(float time, float start, float delta, float duration, float overshootAmt)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float bounceIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float bounceOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float bounceInOut(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float bounceOutIn(float time, float start, float delta, float duration)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static Interpolator GetInterpolator(EASING_TYPE type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
