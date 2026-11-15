using FrameWork;
using UnityEngine;

internal class OnlinePropMode : OnlineModeBase
{
	public override void Init(SceneRace scene)
	{
		base.Init(scene);
		InitScenePropBox();
		RegisterPropHandler();
	}

	private void RegisterPropHandler()
	{
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode())
		{
			ReceiveMessageHandler.Instance.Init();
		}
	}

	private void UnRegisterPropHandler()
	{
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode())
		{
			ReceiveMessageHandler.Instance.UnInit();
		}
	}

	private void InitScenePropBox()
	{
		string name = "Scene/Item";
		GameObject gameObject = GameObject.Find(name);
		if (null == gameObject)
		{
			gameObject = new GameObject(name);
		}
		if (RaceModeFactory.ScenePointMap.ContainsKey(m_SceneRaceComp.iSceneID))
		{
			TSingleton<PropManager>.Instance.SetScenePropBoxes(RaceModeFactory.ScenePointMap[m_SceneRaceComp.iSceneID].SceneBox, gameObject);
		}
	}

	public override void Uninit()
	{
		Debug.Log("OnlinePropMode--->Uninit()");
		UnRegisterPropHandler();
		base.Uninit();
	}
}
