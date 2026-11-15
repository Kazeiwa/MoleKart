using System.Collections.Generic;

namespace FrameWork;

public class CFrameWork : IFrameWork
{
	private Dictionary<enmModuleID, IModule> ModuleMap;

	private static CFrameWork instance;

	public static IFrameWork GetFrameWork()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IModule GetModule(enmModuleID moduleid)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool RegisterModule(IModule module)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UnregisterModule(IModule module)
	{
	}

	public void Update(float fCurTime)
	{
	}

	public void FixedUpdate()
	{
	}

	public void OnGUI()
	{
	}
}
