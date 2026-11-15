namespace FrameWork;

public interface IFrameWork
{
	void Update(float fCurTime);

	IModule GetModule(enmModuleID moduleid);

	bool RegisterModule(IModule module);

	void UnregisterModule(IModule module);
}
