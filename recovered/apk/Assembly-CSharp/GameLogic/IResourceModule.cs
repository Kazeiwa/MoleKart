using FrameWork;
using UnityEngine;

namespace GameLogic;

public interface IResourceModule : IModule
{
	Object LoadShader(string shaderName);

	Object LoadShanderMaterial(string materailName);

	Object LoadShanderTexture(string textureName);

	Object LoadParticle(string particleName);
}
