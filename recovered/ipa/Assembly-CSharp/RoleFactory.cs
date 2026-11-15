using System.Collections.Generic;
using UnityEngine;

public class RoleFactory
{
	private static RoleFactory instance;

	protected static Dictionary<RoleType, GameObject> m_PlayerMode;

	protected static Dictionary<RoleType, GameObject> m_NpcMode;

	public static RoleFactory Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool Init()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UnInit()
	{
	}

	public Role CreateRole(string name, Vector3 pos, Quaternion rotation, RoleType roletype)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Role CreateNpcRole(string name, Vector3 pos, Quaternion rotation, RoleType roletype)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
