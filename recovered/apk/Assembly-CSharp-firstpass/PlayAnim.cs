using System;
using System.Collections;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
	public string AniName = string.Empty;

	public bool ForceLoop;

	public float StartTime = 1f;

	private bool Played;

	private void Awake()
	{
		Played = false;
		if (AniName.Length <= 0)
		{
			{
				IEnumerator enumerator = base.animation.GetEnumerator();
				try
				{
					if (enumerator.MoveNext())
					{
						AnimationState animationState = (AnimationState)enumerator.Current;
						AniName = animationState.name;
						if (ForceLoop)
						{
							animationState.wrapMode = WrapMode.Loop;
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		if (AniName.Length <= 0)
		{
			Played = true;
		}
	}

	private void Update()
	{
		if (!Played)
		{
			StartTime -= Time.deltaTime;
			if (StartTime <= 0f)
			{
				base.animation.Play(AniName);
				Played = true;
			}
		}
	}
}
