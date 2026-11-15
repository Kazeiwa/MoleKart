using System.Collections.Generic;
using UnityEngine;

namespace FrameWork;

public class CFrameWork : IFrameWork
{
	private Dictionary<enmModuleID, IModule> ModuleMap = new Dictionary<enmModuleID, IModule>();

	private static CFrameWork instance;

	public static IFrameWork GetFrameWork()
	{
		if (instance == null)
		{
			instance = new CFrameWork();
		}
		return instance;
	}

	public IModule GetModule(enmModuleID moduleid)
	{
		if (ModuleMap.ContainsKey(moduleid))
		{
			return ModuleMap[moduleid];
		}
		return null;
	}

	public bool RegisterModule(IModule module)
	{
		enmModuleID iD = module.GetID();
		if (ModuleMap.ContainsKey(iD))
		{
			Debug.Log(string.Concat("RegisterModule", iD, "Already exist!"));
			return false;
		}
		ModuleMap.Add(iD, module);
		return true;
	}

	public void UnregisterModule(IModule module)
	{
		enmModuleID iD = module.GetID();
		if (ModuleMap.ContainsKey(iD))
		{
			ModuleMap.Remove(iD);
			Debug.Log(string.Concat("UnregisterModule", iD, " successfully!"));
		}
		else
		{
			Debug.Log(string.Concat("UnregisterModule", iD, " failed!"));
		}
	}

	public void Update(float fCurTime)
	{
		foreach (KeyValuePair<enmModuleID, IModule> item in ModuleMap)
		{
			item.Value.Update(fCurTime);
		}
	}

	public void FixedUpdate()
	{
		foreach (KeyValuePair<enmModuleID, IModule> item in ModuleMap)
		{
			item.Value.FixedUpdate();
		}
	}

	public void OnGUI()
	{
		foreach (KeyValuePair<enmModuleID, IModule> item in ModuleMap)
		{
			item.Value.OnGUI();
		}
	}
}
