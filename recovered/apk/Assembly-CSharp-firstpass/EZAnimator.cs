using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZAnimator : MonoBehaviour
{
	private static EZAnimator s_Instance = null;

	protected static Dictionary<EZAnimation.ANIM_TYPE, EZLinkedList<EZAnimation>> freeAnimPool = new Dictionary<EZAnimation.ANIM_TYPE, EZLinkedList<EZAnimation>>();

	protected static EZLinkedList<EZAnimation> animations = new EZLinkedList<EZAnimation>();

	protected static bool pumpIsRunning = false;

	protected static bool pumpIsDone = true;

	protected static float startTime;

	protected static float time;

	protected static float elapsed;

	protected static EZAnimation anim;

	private int i;

	public static EZAnimator instance
	{
		get
		{
			if (s_Instance == null)
			{
				GameObject gameObject = new GameObject("EZAnimator");
				s_Instance = (EZAnimator)gameObject.AddComponent(typeof(EZAnimator));
			}
			return s_Instance;
		}
	}

	public static bool Exists()
	{
		return s_Instance != null;
	}

	private void Awake()
	{
		Object.DontDestroyOnLoad(this);
	}

	private void OnLevelWasLoaded(int level)
	{
		if (animations != null)
		{
			EZLinkedListIterator<EZAnimation> eZLinkedListIterator = animations.Begin();
			while (!eZLinkedListIterator.Done)
			{
				EZAnimation current = eZLinkedListIterator.Current;
				current._cancel();
				animations.Remove(current);
				ReturnAnimToPool(current);
				eZLinkedListIterator.Next();
			}
		}
	}

	protected static IEnumerator AnimPump()
	{
		EZLinkedListIterator<EZAnimation> i = animations.Begin();
		startTime = Time.realtimeSinceStartup;
		pumpIsDone = false;
		while (pumpIsRunning)
		{
			time = Time.realtimeSinceStartup;
			elapsed = time - startTime;
			startTime = time;
			i.Begin(animations);
			while (!i.Done)
			{
				if (!i.Current.Step(elapsed))
				{
					anim = i.Current;
					animations.Remove(anim);
					ReturnAnimToPool(anim);
				}
				i.NextNoRemove();
			}
			yield return null;
		}
		pumpIsDone = true;
	}

	public void StartAnimationPump()
	{
		if (!pumpIsRunning && base.gameObject.active)
		{
			pumpIsRunning = true;
			StartCoroutine(PumpStarter());
		}
	}

	protected IEnumerator PumpStarter()
	{
		while (!pumpIsDone)
		{
			yield return null;
		}
		StartCoroutine(AnimPump());
	}

	public static void StopAnimationPump()
	{
	}

	protected EZAnimation CreateNewAnimation(EZAnimation.ANIM_TYPE type)
	{
		return type switch
		{
			EZAnimation.ANIM_TYPE.AnimClip => new RunAnimClip(), 
			EZAnimation.ANIM_TYPE.Crash => new Crash(), 
			EZAnimation.ANIM_TYPE.CrashRotation => new CrashRotation(), 
			EZAnimation.ANIM_TYPE.FadeAudio => new FadeAudio(), 
			EZAnimation.ANIM_TYPE.FadeSprite => new FadeSprite(), 
			EZAnimation.ANIM_TYPE.FadeMaterial => new FadeMaterial(), 
			EZAnimation.ANIM_TYPE.FadeText => new FadeText(), 
			EZAnimation.ANIM_TYPE.PunchPosition => new PunchPosition(), 
			EZAnimation.ANIM_TYPE.PunchRotation => new PunchRotation(), 
			EZAnimation.ANIM_TYPE.PunchScale => new PunchScale(), 
			EZAnimation.ANIM_TYPE.Rotate => new AnimateRotation(), 
			EZAnimation.ANIM_TYPE.Scale => new AnimateScale(), 
			EZAnimation.ANIM_TYPE.Shake => new Shake(), 
			EZAnimation.ANIM_TYPE.ShakeRotation => new ShakeRotation(), 
			EZAnimation.ANIM_TYPE.SmoothCrash => new SmoothCrash(), 
			EZAnimation.ANIM_TYPE.Translate => new AnimatePosition(), 
			EZAnimation.ANIM_TYPE.TuneAudio => new TuneAudio(), 
			_ => null, 
		};
	}

	public EZAnimation GetAnimation(EZAnimation.ANIM_TYPE type)
	{
		if (freeAnimPool.TryGetValue(type, out var value) && !value.Empty)
		{
			EZAnimation head = value.Head;
			value.Remove(head);
			return head;
		}
		return CreateNewAnimation(type);
	}

	protected static void ReturnAnimToPool(EZAnimation anim)
	{
		anim.Clear();
		if (!freeAnimPool.TryGetValue(anim.type, out var value))
		{
			value = new EZLinkedList<EZAnimation>();
			freeAnimPool.Add(anim.type, value);
		}
		value.Add(anim);
	}

	public void AddAnimation(EZAnimation a)
	{
		if (!a.running)
		{
			animations.Add(a);
			a.running = true;
		}
		StartAnimationPump();
	}

	public void AddTransition(EZTransition t)
	{
		if (t.animationTypes == null)
		{
			return;
		}
		for (int i = 0; i < t.animationTypes.Length; i++)
		{
			EZAnimation.ANIM_TYPE aNIM_TYPE = t.animationTypes[i];
			if (aNIM_TYPE == EZAnimation.ANIM_TYPE.FadeSprite || aNIM_TYPE == EZAnimation.ANIM_TYPE.FadeText || aNIM_TYPE == EZAnimation.ANIM_TYPE.FadeMaterial)
			{
				EZLinkedList<EZLinkedListNode<GameObject>> subSubjects = t.SubSubjects;
				if (subSubjects.Rewind())
				{
					do
					{
						EZAnimation eZAnimation = GetAnimation(aNIM_TYPE);
						t.animParams[i].transition = t;
						if (!eZAnimation.Start(subSubjects.Current.val, t.animParams[i]))
						{
							ReturnAnimToPool(eZAnimation);
						}
						else if (eZAnimation.running)
						{
							EZLinkedListNode<EZAnimation> eZLinkedListNode = t.AddRunningAnim();
							eZLinkedListNode.val = eZAnimation;
							eZAnimation.Data = eZLinkedListNode;
						}
					}
					while (subSubjects.MoveNext());
				}
			}
			if (!(t.MainSubject == null))
			{
				EZAnimation eZAnimation = GetAnimation(aNIM_TYPE);
				t.animParams[i].transition = t;
				if (!eZAnimation.Start(t.MainSubject, t.animParams[i]))
				{
					ReturnAnimToPool(eZAnimation);
				}
				else if (eZAnimation.running)
				{
					EZLinkedListNode<EZAnimation> eZLinkedListNode = t.AddRunningAnim();
					eZLinkedListNode.val = eZAnimation;
					eZAnimation.Data = eZLinkedListNode;
				}
			}
		}
	}

	public void StopAnimation(EZAnimation a)
	{
		StopAnimation(a, end: false);
	}

	public void StopAnimation(EZAnimation a, bool end)
	{
		if (a.running)
		{
			if (end)
			{
				a._end();
			}
			else
			{
				a._stop();
			}
			animations.Remove(a);
			ReturnAnimToPool(a);
			if (animations.Empty)
			{
				StopAnimationPump();
			}
		}
	}

	public void Stop(object obj)
	{
		Stop(obj, end: false);
	}

	public void Stop(object obj, bool end)
	{
		EZLinkedListIterator<EZAnimation> eZLinkedListIterator = animations.Begin();
		while (!eZLinkedListIterator.Done)
		{
			if (eZLinkedListIterator.Current.GetSubject() == obj)
			{
				EZAnimation current = eZLinkedListIterator.Current;
				if (current.running)
				{
					if (end)
					{
						current._end();
					}
					else
					{
						current._stop();
					}
					animations.Remove(current);
					ReturnAnimToPool(current);
				}
			}
			eZLinkedListIterator.Next();
		}
	}

	public void Stop(object obj, EZAnimation.ANIM_TYPE type, bool end)
	{
		EZLinkedListIterator<EZAnimation> eZLinkedListIterator = animations.Begin();
		while (!eZLinkedListIterator.Done)
		{
			if (eZLinkedListIterator.Current.GetSubject() == obj && eZLinkedListIterator.Current.type == type)
			{
				EZAnimation current = eZLinkedListIterator.Current;
				if (current.running)
				{
					if (end)
					{
						current._end();
					}
					else
					{
						current._stop();
					}
					animations.Remove(current);
					ReturnAnimToPool(current);
				}
			}
			eZLinkedListIterator.Next();
		}
	}

	public void End(object obj)
	{
		Stop(obj, end: true);
	}

	public void EndAll()
	{
		EZLinkedListIterator<EZAnimation> eZLinkedListIterator = animations.Begin();
		while (!eZLinkedListIterator.Done)
		{
			eZLinkedListIterator.Current.End();
			eZLinkedListIterator.Next();
		}
	}

	public void StopAll()
	{
		EZLinkedListIterator<EZAnimation> eZLinkedListIterator = animations.Begin();
		while (!eZLinkedListIterator.Done)
		{
			eZLinkedListIterator.Current.Stop();
			eZLinkedListIterator.Next();
		}
	}

	public void PauseAll()
	{
		EZLinkedListIterator<EZAnimation> eZLinkedListIterator = animations.Begin();
		while (!eZLinkedListIterator.Done)
		{
			eZLinkedListIterator.Current.Paused = true;
			eZLinkedListIterator.Next();
		}
	}

	public void UnpauseAll()
	{
		EZLinkedListIterator<EZAnimation> eZLinkedListIterator = animations.Begin();
		while (!eZLinkedListIterator.Done)
		{
			eZLinkedListIterator.Current.Paused = false;
			eZLinkedListIterator.Next();
		}
	}

	public static int GetNumAnimations()
	{
		return animations.Count;
	}
}
