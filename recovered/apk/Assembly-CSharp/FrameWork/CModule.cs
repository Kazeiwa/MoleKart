namespace FrameWork;

public class CModule : IModule
{
	public enmModuleID moduleid;

	public IFrameWork framework;

	protected CModule(enmModuleID moduleid)
	{
		framework = null;
		this.moduleid = moduleid;
	}

	public enmModuleID GetID()
	{
		return moduleid;
	}

	public virtual void Init(IFrameWork iFrameHost)
	{
		framework = iFrameHost;
		framework.RegisterModule(this);
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
		if (framework != null)
		{
			framework.UnregisterModule(this);
		}
	}
}
