using System;
using System.Collections;
using UnityEngine;

public class PlayAnimationList : MonoBehaviour
{
	private Hashtable animationList;

	private GameObject objectToPlay;

	private string currentClip;

	private float currentTime = 100f;

	private string[] clipNameList = new string[2];

	private bool beginPlay;

	public string[] ClipNameList
	{
		get
		{
			return clipNameList;
		}
		set
		{
			clipNameList = value;
		}
	}

	public Hashtable AnimationList
	{
		get
		{
			return animationList;
		}
		set
		{
			animationList = value;
		}
	}

	public GameObject ObjectToPlay
	{
		get
		{
			return objectToPlay;
		}
		set
		{
			objectToPlay = value;
		}
	}

	public void GetAllClips()
	{
		animationList = new Hashtable();
		foreach (int value in Enum.GetValues(typeof(RoleAnim3D.RoleAnimClipState)))
		{
			animationList.Add((RoleAnim3D.RoleAnimClipState)value, ((RoleAnim3D.RoleAnimClipState)value).ToString());
		}
	}

	public void PlayAnimation(string name, bool crossFade, bool loop)
	{
		foreach (DictionaryEntry animation in animationList)
		{
			if ((string)animation.Value == name)
			{
				currentClip = (string)animation.Value;
				break;
			}
		}
		if (crossFade)
		{
			objectToPlay.animation.CrossFadeQueued(currentClip);
		}
		else
		{
			objectToPlay.animation.PlayQueued(currentClip);
		}
	}

	public void PlayAnimation(string name1, string name2)
	{
		clipNameList[0] = name1;
		clipNameList[1] = name2;
		beginPlay = true;
	}

	public void StopAllAnimation()
	{
		if (objectToPlay.animation.isPlaying)
		{
			objectToPlay.animation.Stop();
		}
	}

	public void Update()
	{
		if (beginPlay && currentTime > 0.3f)
		{
			objectToPlay.animation.CrossFade(clipNameList[0]);
			if (currentTime == 100f)
			{
				currentTime = objectToPlay.animation.GetClip(clipNameList[0]).length;
			}
			currentTime -= Time.deltaTime;
		}
		else
		{
			objectToPlay.animation.CrossFade(clipNameList[1]);
			objectToPlay.animation.wrapMode = WrapMode.Loop;
		}
	}
}
