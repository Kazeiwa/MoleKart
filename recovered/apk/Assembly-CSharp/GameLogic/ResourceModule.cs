using FrameWork;
using UnityEngine;

namespace GameLogic;

public class ResourceModule : CModule, IModule, IResourceModule
{
	public ResourceModule()
		: base(enmModuleID.MI_RESOURCE)
	{
	}

	public override void Init(IFrameWork iFrameHost)
	{
		base.Init(iFrameHost);
	}

	public override void Uninit()
	{
		base.Uninit();
	}

	public override void Update(float fCurTime)
	{
	}

	public Object LoadShader(string shaderName)
	{
		return null;
	}

	public Object LoadShanderMaterial(string materailName)
	{
		return null;
	}

	public Object LoadShanderTexture(string textureName)
	{
		return null;
	}

	public Object LoadParticle(string particleName)
	{
		return null;
	}
}
