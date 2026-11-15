using UnityEngine;

internal class OnlineTimingMode : OnlineModeBase
{
	public override void Init(SceneRace scene)
	{
		base.Init(scene);
		InitScenePropBox();
	}

	private void InitScenePropBox()
	{
		string name = "Scene/Item";
		GameObject gameObject = GameObject.Find(name);
		gameObject.active = false;
	}
}
