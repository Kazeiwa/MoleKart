using System.Collections.Generic;
using UnityEngine;

public class SelectedRoleFactory : TSingleton<SelectedRoleFactory>
{
	private Dictionary<int, SelectedRole> mySelectedRoles;

	public SelectedRole CreateSelectedRole(RoleType id, Vector3 pos, Quaternion rotate, Vector3 scale, string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SelectedRole GetSelectedRole(RoleType id, Vector3 pos, Quaternion rotate, Vector3 scale, string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ClearCachedSelectedRole()
	{
	}
}
