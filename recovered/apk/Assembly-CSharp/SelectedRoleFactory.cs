using System.Collections.Generic;
using UnityEngine;

public class SelectedRoleFactory : TSingleton<SelectedRoleFactory>
{
	private Dictionary<int, SelectedRole> mySelectedRoles = new Dictionary<int, SelectedRole>();

	public SelectedRole CreateSelectedRole(RoleType id, Vector3 pos, Quaternion rotate, Vector3 scale, string name)
	{
		if (!RoleConfigData.m_RoleMap.ContainsKey((int)id))
		{
			return null;
		}
		Object role = Resources.Load(RoleConfigData.m_RoleMap[(int)id].high_mode);
		return new SelectedRole(name, pos, rotate, scale, role);
	}

	public SelectedRole GetSelectedRole(RoleType id, Vector3 pos, Quaternion rotate, Vector3 scale, string name)
	{
		if (mySelectedRoles.ContainsKey((int)id))
		{
			return mySelectedRoles[(int)id];
		}
		if (!RoleConfigData.m_RoleMap.ContainsKey((int)id))
		{
			return null;
		}
		Object role = Resources.Load(RoleConfigData.m_RoleMap[(int)id].high_mode);
		SelectedRole selectedRole = new SelectedRole(name, pos, rotate, scale, role);
		mySelectedRoles[(int)id] = selectedRole;
		return selectedRole;
	}

	public void ClearCachedSelectedRole()
	{
		foreach (SelectedRole value in mySelectedRoles.Values)
		{
			value.Uninit();
		}
		mySelectedRoles.Clear();
		Resources.UnloadUnusedAssets();
	}
}
