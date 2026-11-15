using System.Collections.Generic;
using UnityEngine;

public class RoleFactory
{
	private static RoleFactory instance = null;

	protected static Dictionary<RoleType, GameObject> m_PlayerMode = new Dictionary<RoleType, GameObject>();

	protected static Dictionary<RoleType, GameObject> m_NpcMode = new Dictionary<RoleType, GameObject>();

	public static RoleFactory Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new RoleFactory();
			}
			return instance;
		}
	}

	public bool Init()
	{
		Debug.Log("$RoleFactory:Init()");
		if (!TSingleton<RoleConfigData>.Instance.bInited)
		{
			return false;
		}
		foreach (RoleInf value in RoleConfigData.m_RoleMap.Values)
		{
			GameObject gameObject = Resources.Load(value.low_mode) as GameObject;
			if (null != gameObject && !m_PlayerMode.ContainsKey((RoleType)value.ID))
			{
				m_PlayerMode.Add((RoleType)value.ID, gameObject);
			}
			GameObject gameObject2 = Resources.Load(value.npc_mode) as GameObject;
			if (null != gameObject2 && !m_NpcMode.ContainsKey((RoleType)value.ID))
			{
				m_NpcMode.Add((RoleType)value.ID, gameObject2);
			}
		}
		return true;
	}

	public void UnInit()
	{
		Debug.Log("RoleFactory:UnInit()!");
		m_PlayerMode.Clear();
		m_NpcMode.Clear();
	}

	public Role CreateRole(string name, Vector3 pos, Quaternion rotation, RoleType roletype)
	{
		if (!m_PlayerMode.ContainsKey(roletype) && RoleConfigData.m_RoleMap.ContainsKey((int)roletype))
		{
			GameObject gameObject = Resources.Load(RoleConfigData.m_RoleMap[(int)roletype].low_mode) as GameObject;
			if (null != gameObject)
			{
				m_PlayerMode.Add(roletype, gameObject);
			}
		}
		if (RoleConfigData.m_RoleMap.ContainsKey((int)roletype))
		{
			if (RoleConfigData.m_RoleMap[(int)roletype].type == 0)
			{
				if (!m_PlayerMode.ContainsKey(RoleType.EMole) && RoleConfigData.m_RoleMap.ContainsKey(3000))
				{
					GameObject gameObject2 = Resources.Load(RoleConfigData.m_RoleMap[3000].low_mode) as GameObject;
					if (null != gameObject2)
					{
						m_PlayerMode.Add(RoleType.EMole, gameObject2);
					}
				}
				if (m_PlayerMode.ContainsKey(roletype) && m_PlayerMode.ContainsKey(RoleType.EMole))
				{
					return new Role(name, pos, rotation, m_PlayerMode[RoleType.EMole], m_PlayerMode[roletype], roletype, isNpc: false);
				}
			}
			else if (m_PlayerMode.ContainsKey(roletype))
			{
				return new Role(name, pos, rotation, m_PlayerMode[roletype], roletype, isNpc: false);
			}
		}
		return null;
	}

	public Role CreateNpcRole(string name, Vector3 pos, Quaternion rotation, RoleType roletype)
	{
		if (!m_NpcMode.ContainsKey(roletype) && RoleConfigData.m_RoleMap.ContainsKey((int)roletype))
		{
			GameObject gameObject = Resources.Load(RoleConfigData.m_RoleMap[(int)roletype].npc_mode) as GameObject;
			if (null != gameObject)
			{
				m_NpcMode.Add(roletype, gameObject);
			}
		}
		if (RoleConfigData.m_RoleMap.ContainsKey((int)roletype) && m_NpcMode.ContainsKey(roletype))
		{
			return new Role(name, pos, rotation, m_NpcMode[roletype], roletype, isNpc: true);
		}
		return null;
	}
}
