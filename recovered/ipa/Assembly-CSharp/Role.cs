using System.Collections.Generic;
using UnityEngine;

public class Role
{
	private Dictionary<string, int> m_SoundMap;

	private string m_PreAnimClipName;

	public GameObject m_RoleModel;

	private SkinnedMeshRenderer[] m_SkinMeshRenders;

	private MeshRenderer[] m_MehsRenders;

	public Transform m_Parent;

	public bool m_CurrentFlag;

	public string m_CurrentName;

	private RoleType m_RoleType;

	public static string THROW_PROP;

	public static string SCREAM_LOW;

	public static string SCREAM_HIGHT;

	public static string SUCCESS;

	public static string FAIL;

	public static string SELECT;

	public static string HAPPY_ONE;

	public static string HAPPY_TWO;

	public static string HAPPY_THREE;

	public static string SAY_HI;

	private Dictionary<int, AudioSource> m_Audios;

	private static float m_LastPlaySound;

	public Car m_Car;

	public Transform transform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int RoleID
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 Position
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Quaternion Rotation
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Transform Parent
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Animation animation
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool Enabled
	{
		set
		{
		}
	}

	public Role(string name, Vector3 pos, Quaternion rotation, Object role, RoleType type, bool isNpc)
	{
	}

	public Role(string name, Vector3 pos, Quaternion rotation, Object skeleton, Object skin, RoleType type, bool isNpc)
	{
	}

	private void InitSoundMap(RoleType type)
	{
	}

	private void ReplaceRolePart(Object obj)
	{
	}

	public void SetAniState(string name)
	{
	}

	public void Play(string name)
	{
	}

	public void PlayOther(string name)
	{
	}

	public void PlayAni(string name, bool flag)
	{
	}

	public void PlayOnly(string name)
	{
	}

	public void StopAni()
	{
	}

	public void PlayNext(string name)
	{
	}

	public bool PlaySound(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool PlaySound(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool StopSound(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetPreAniName(string name)
	{
	}
}
