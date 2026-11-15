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
		lose_kula,
		win_kula,
		brake,
		win_yali,
		lose_yali,
		win_puti,
		lose_puti,
		MaxAni
	}

	private Dictionary<int, Animation> animationList;

	private string m_lastClipName;

	public static string[] clipNameList;

	public int Count
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public string LastClipName
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public RoleAnim3D(Animation animation)
	{
	}

	public void Init(Animation aN)
	{
	}

	public Animation GetAnimation(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Animation GetAnimByName(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PlayAnimation(Animation myAnimation, string animationName, WrapMode w)
	{
	}

	public void PlayRevertAnimation(Animation myAnimation, string animationName)
	{
	}

	public void PlayAnimation(Animation myAnimation, string animationName)
	{
	}

	private void PlayAnimation(Animation myAnimation, string animationName, float speed, bool revert, WrapMode w)
	{
	}

	public void RewindAnimation(Animation myAnimation, string animationName)
	{
	}

	public bool IsAnyAnimationPlay(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsAnyAnimationPlay(Animation aN)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static string GetClipName(RoleAnimClipState ec)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
