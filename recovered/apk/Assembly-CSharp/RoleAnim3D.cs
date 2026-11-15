using System.Collections.Generic;
using UnityEngine;

public class RoleAnim3D
{
	public enum RoleAnimClipState
	{
		win_mo,
		lose_mo,
		win_RK,
		lose_RK,
		stan,
		turl01,
		turl02,
		turl03,
		turr01,
		turr02,
		turr03,
		drfl01,
		drfl02,
		drfl03,
		drfr01,
		drfr02,
		drfr03,
		hitd,
		beyl,
		beyr,
		reve01,
		reve02,
		reve03,
		stun01,
		stun02,
		stun03,
		thrf,
		thrb,
		land,
		lean01,
		lean02,
		lean03,
		win_molele,
		lose_molele,
		MaxAni
	}

	private Dictionary<int, Animation> animationList = new Dictionary<int, Animation>();

	private string m_lastClipName;

	public static string[] clipNameList = new string[34]
	{
		"win_mo", "lose_mo", "win_RK", "lose_RK", "stan", "turl01", "turl02", "turl03", "turr01", "turr02",
		"turr03", "drfl01", "drfl02", "drfl03", "drfr01", "drfr02", "drfr03", "hitd", "beyl", "beyr",
		"reve01", "reve02", "reve03", "stun01", "stun02", "stun03", "thrf", "thrb", "land", "lean01",
		"lean02", "lean03", "win_molele", "lose_molele"
	};

	public int Count => animationList.Count;

	public string LastClipName
	{
		get
		{
			return m_lastClipName;
		}
		set
		{
			m_lastClipName = value;
		}
	}

	public RoleAnim3D(Animation animation)
	{
		if (null != animation)
		{
			Init(animation);
		}
	}

	public void Init(Animation aN)
	{
		int count = animationList.Count;
		aN.wrapMode = WrapMode.Once;
		aN.Stop();
		animationList.Add(count, aN);
		aN["turl01"].layer = -1;
		aN["turr01"].layer = -1;
		aN["stan"].layer = -1;
		aN["reve01"].layer = -1;
	}

	public Animation GetAnimation(int index)
	{
		if (index < 0 || index >= animationList.Count)
		{
			return null;
		}
		return animationList[index];
	}

	public Animation GetAnimByName(string name)
	{
		if (animationList == null)
		{
			return null;
		}
		for (int i = 0; i < animationList.Count; i++)
		{
			if (animationList[i].name.Equals(name))
			{
				return animationList[i];
			}
		}
		return null;
	}

	public void PlayAnimation(Animation myAnimation, string animationName, WrapMode w)
	{
		PlayAnimation(myAnimation, animationName, 1f, revert: false, w);
	}

	public void PlayRevertAnimation(Animation myAnimation, string animationName)
	{
		PlayAnimation(myAnimation, animationName, 1f, revert: true, WrapMode.Once);
	}

	public void PlayAnimation(Animation myAnimation, string animationName)
	{
		PlayAnimation(myAnimation, animationName, 1f, revert: false, WrapMode.Once);
	}

	private void PlayAnimation(Animation myAnimation, string animationName, float speed, bool revert, WrapMode w)
	{
		if (animationName != null && !(myAnimation == null))
		{
			AnimationState animationState = myAnimation[animationName];
			if (revert)
			{
				animationState.speed = 0f - speed;
				animationState.time = animationState.length;
			}
			else
			{
				animationState.speed = speed;
				animationState.time = 0f;
			}
			animationState.wrapMode = w;
			m_lastClipName = animationName;
			myAnimation.Play(animationName);
		}
	}

	public void RewindAnimation(Animation myAnimation, string animationName)
	{
		m_lastClipName = null;
		myAnimation.Rewind(animationName);
	}

	public bool IsAnyAnimationPlay(int index)
	{
		bool result = false;
		if (index >= animationList.Count)
		{
			Debug.Log("There is no this Animation: " + index);
			return false;
		}
		Animation animation = animationList[index];
		if (animation.GetClipCount() > 0)
		{
			foreach (AnimationState item in animation)
			{
				if (animation.IsPlaying(item.clip.name))
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public bool IsAnyAnimationPlay(Animation aN)
	{
		bool result = false;
		if (aN == null)
		{
			Debug.Log("There is no Animation: ");
			return false;
		}
		if (aN.GetClipCount() > 0)
		{
			foreach (AnimationState item in aN)
			{
				if (aN.IsPlaying(item.clip.name))
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public static string GetClipName(RoleAnimClipState ec)
	{
		return clipNameList[(int)ec];
	}
}
