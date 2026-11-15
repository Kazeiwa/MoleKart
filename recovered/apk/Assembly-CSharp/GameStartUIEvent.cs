using System.Collections;
using FrameWork;
using UnityEngine;

public class GameStartUIEvent : MonoBehaviour
{
	private static bool appInfoReported;

	private bool buttonClicked;

	private float deltaTimer = 1f;

	private void Start()
	{
		MyTcpClient.Instance.CloseClient();
		if (!appInfoReported)
		{
			StartCoroutine("StartLoadResource");
			appInfoReported = true;
		}
		if (!AppMain.Instance.GameCenterTried)
		{
			AppMain.Instance.GameCenterTried = true;
		}
		SoundManager.Instance.PlaySoundEx(60002);
	}

	private IEnumerator StartLoadResource()
	{
		FlurryBinding.ReportAppRun(AppMain.Instance.channel);
		yield return 0;
	}

	public void OnRace()
	{
		UtilBinding.ShowWaitingView(bShow: true);
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule)
		{
			raceLogicModule.SetCurrentRaceType(RacingType.SpeedSingleMode);
		}
		SceneManager.Instance.LoadLevel(1002);
	}

	public void OnProp()
	{
		UtilBinding.ShowWaitingView(bShow: true);
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule)
		{
			raceLogicModule.SetCurrentRaceType(RacingType.PropSingleMode);
		}
		SceneManager.Instance.LoadLevel(1002);
	}

	public void OnNet()
	{
	}

	public void OnConfiguration()
	{
		UtilBinding.ShowWaitingView(bShow: true);
		SceneManager.Instance.LoadLevel(1004);
	}

	public void OnMKLeaderboard()
	{
		UtilBinding.ShowWaitingView(bShow: true);
		SceneManager.Instance.LoadLevel(1008);
	}

	private void GameCenterButtonPressed()
	{
		if (!buttonClicked)
		{
			buttonClicked = true;
		}
	}

	private void OpenFeitButtonPressed()
	{
		if (!buttonClicked)
		{
			buttonClicked = true;
			Debug.Log("Try to Login OpenFeit!");
		}
	}

	private void TaomeeMoreGame()
	{
		Debug.Log("ToameeMoreGame!");
	}

	private void Update()
	{
		if (deltaTimer > 0f)
		{
			deltaTimer -= Time.deltaTime;
			return;
		}
		buttonClicked = false;
		deltaTimer = 1f;
	}
}
