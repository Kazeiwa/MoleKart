namespace FrameWork;

public class CModule : IModule
{
	public enmModuleID moduleid;

	public IFrameWork framework;

	protected CModule(enmModuleID moduleid)
	{
	}

	public enmModuleID GetID()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void Init(IFrameWork iFrameHost)
	{
	}

	public virtual void Update(float fCurTime)
	{
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void OnGUI()
	{
	}

	public virtual void Uninit()
	{
	}
}
