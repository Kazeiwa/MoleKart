using FrameWork;
using UnityEngine;

public class SceneIntroUI : ISceneUI
{
	private Transform theTransform;

	public void InitUI(Transform transform)
	{
		theTransform = transform;
	}

	public void ShowIntroUI()
	{
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		IRaceLogicModule raceLogicModule = frameWork.GetModule(enmModuleID.MI_RACELOGIC) as IRaceLogicModule;
		(raceLogicModule.GetCurrentRaceType() switch
		{
			RacingType.SpeedSingleMode => theTransform.FindChild("IntroUI/DashMode").gameObject, 
			RacingType.OnlineTimingMode => theTransform.FindChild("IntroUI/DashMode").gameObject, 
			RacingType.PropSingleMode => theTransform.FindChild("IntroUI/PropMode").gameObject, 
			RacingType.OnlinePropMode => theTransform.FindChild("IntroUI/PropMode").gameObject, 
			_ => theTransform.FindChild("IntroUI/PropMode").gameObject, 
		}).active = true;
		GameObject gameObject;
		if (!AppMain.Instance.IsOnlineMode)
		{
			(LocalUserInfo.Instance.TrackLevel switch
			{
				0 => theTransform.FindChild("IntroUI/easy").gameObject, 
				1 => theTransform.FindChild("IntroUI/normal").gameObject, 
				2 => theTransform.FindChild("IntroUI/master").gameObject, 
				_ => theTransform.FindChild("IntroUI/easy").gameObject, 
			}).active = true;
			gameObject = theTransform.FindChild("IntroUI/DifficultyLabel").gameObject;
			gameObject.active = true;
		}
		gameObject = theTransform.FindChild("IntroUI/Skip").gameObject;
		gameObject.active = true;
		switch (SceneManager.Instance.CurGameSceneId)
		{
		case 1101:
			gameObject = theTransform.FindChild("IntroUI/port").gameObject;
			break;
		case 1102:
			gameObject = theTransform.FindChild("IntroUI/farm").gameObject;
			break;
		case 1103:
			gameObject = theTransform.FindChild("IntroUI/mogu").gameObject;
			break;
		case 1104:
			gameObject = theTransform.FindChild("IntroUI/sky").gameObject;
			break;
		case 1105:
			gameObject = theTransform.FindChild("IntroUI/volcano").gameObject;
			break;
		case 1106:
			gameObject = theTransform.FindChild("IntroUI/ice").gameObject;
			break;
		case 1107:
			gameObject = theTransform.FindChild("IntroUI/manor").gameObject;
			break;
		case 1108:
			gameObject = theTransform.FindChild("IntroUI/maple").gameObject;
			break;
		case 1109:
			gameObject = theTransform.FindChild("IntroUI/factory").gameObject;
			break;
		}
		SetEnglishOnlineTrackNamePos(gameObject);
		gameObject.active = true;
	}

	public void HideIntroUI()
	{
		Transform transform = theTransform.FindChild("IntroUI/DashMode");
		if (transform != null)
		{
			GameObject gameObject = transform.parent.gameObject;
			Object.DestroyImmediate(gameObject, allowDestroyingAssets: true);
		}
	}

	private void SetEnglishOnlineTrackNamePos(GameObject gameobject)
	{
		if (AppMain.Instance.GetCurrentSystemLanguage() == "en" && AppMain.Instance.IsOnlineMode)
		{
			gameobject.transform.position = new Vector3(0f, gameobject.transform.position.y, gameobject.transform.position.z);
		}
	}

	public void UpdateUI()
	{
	}
}
