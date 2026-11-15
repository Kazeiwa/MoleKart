using FrameWork;
using UnityEngine;

public class GameRaceModehook : MonoBehaviour
{
	public int iSceneID;

	private IRaceLogicModule pkModule;

	private void Awake()
	{
		Resources.UnloadUnusedAssets();
	}

	private void Start()
	{
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null)
		{
			pkModule = frameWork.GetModule(enmModuleID.MI_RACELOGIC) as IRaceLogicModule;
			if (pkModule != null)
			{
				pkModule.CreateGame(iSceneID);
			}
		}
		else
		{
			Debug.LogWarning("Can't find the Framework, Please Start Our game from Splash.level!");
		}
	}

	private void OnDestroy()
	{
		if (pkModule != null)
		{
			pkModule.DestroyGame();
		}
		pkModule = null;
		Resources.UnloadUnusedAssets();
	}
}
