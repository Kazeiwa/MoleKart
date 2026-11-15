using System.Collections.Generic;
using UnityEngine;

public class PauseUI : ISceneUI
{
	private enum PAUSE_ELEMENTS
	{
		BACKGROUND,
		PAUSE_BTN,
		PAUSE_MARK,
		BACK_TO_GAME_BTN,
		RESTART_BTN,
		CONTROL_SETTING_BTN,
		FINISH_GAME_BTN,
		BACK_BTN,
		RIGHT_ARROW_BTN,
		LEFT_ARROW_BTN,
		BABY_PANEL,
		CHILD_PANEL,
		DAXIA_PANEL,
		MASTER_PANEL,
		OK_BTN,
		CANCEL_BTN,
		EXIT_TEXT,
		EXIT_TITLE,
		RESTART_TEXT,
		RESTART_TITLE,
		BACK_BAR
	}

	private enum CONFIRM_TYPE
	{
		CONFIRM_TYPE_NONE,
		CONFIRM_TYPE_RESTART,
		CONFIRM_TYPE_EXIT
	}

	private bool canShowSlider;

	private bool showFirstLevel;

	private bool showSecondLevel;

	private bool canShowTutorialUI;

	private CONFIRM_TYPE m_confirmType;

	private Dictionary<PAUSE_ELEMENTS, GameObject> backgroundObjects = new Dictionary<PAUSE_ELEMENTS, GameObject>();

	private Dictionary<PAUSE_ELEMENTS, GameObject> firstLevelObjects = new Dictionary<PAUSE_ELEMENTS, GameObject>();

	private Dictionary<PAUSE_ELEMENTS, GameObject> secondLevelObjects = new Dictionary<PAUSE_ELEMENTS, GameObject>();

	private Dictionary<PAUSE_ELEMENTS, GameObject> confirmLevelObjects = new Dictionary<PAUSE_ELEMENTS, GameObject>();

	private int controlIndex;

	private GameObject[] controls = new GameObject[4];

	private Dictionary<int, Vector3> modeShowPosDic = new Dictionary<int, Vector3>();

	private int canClickBtn;

	private bool isShowingConfirmView;

	private int lastPlayGameLevel = -1;

	private DelayDelegateTimer timer;

	public void InitUI(Transform transform)
	{
		GameObject gameObject = transform.FindChild("Pause/pauseBtn").gameObject;
		backgroundObjects.Add(PAUSE_ELEMENTS.PAUSE_BTN, gameObject);
		gameObject = transform.FindChild("Pause/background").gameObject;
		backgroundObjects.Add(PAUSE_ELEMENTS.BACKGROUND, gameObject);
		gameObject = transform.FindChild("Pause/pauseMark").gameObject;
		backgroundObjects.Add(PAUSE_ELEMENTS.PAUSE_MARK, gameObject);
		gameObject = transform.FindChild("Pause/FirstLevel/backToGame").gameObject;
		firstLevelObjects.Add(PAUSE_ELEMENTS.BACK_TO_GAME_BTN, gameObject);
		gameObject = transform.FindChild("Pause/FirstLevel/restart").gameObject;
		firstLevelObjects.Add(PAUSE_ELEMENTS.RESTART_BTN, gameObject);
		gameObject = transform.FindChild("Pause/FirstLevel/controlSetting").gameObject;
		firstLevelObjects.Add(PAUSE_ELEMENTS.CONTROL_SETTING_BTN, gameObject);
		gameObject = transform.FindChild("Pause/FirstLevel/finishGame").gameObject;
		firstLevelObjects.Add(PAUSE_ELEMENTS.FINISH_GAME_BTN, gameObject);
		gameObject = transform.FindChild("Pause/SecondLevel/back").gameObject;
		secondLevelObjects.Add(PAUSE_ELEMENTS.BACK_BTN, gameObject);
		gameObject = transform.FindChild("Pause/SecondLevel/rightArrow").gameObject;
		secondLevelObjects.Add(PAUSE_ELEMENTS.RIGHT_ARROW_BTN, gameObject);
		gameObject = transform.FindChild("Pause/SecondLevel/leftArrow").gameObject;
		secondLevelObjects.Add(PAUSE_ELEMENTS.LEFT_ARROW_BTN, gameObject);
		gameObject = transform.FindChild("Pause/SecondLevel/baby").gameObject;
		secondLevelObjects.Add(PAUSE_ELEMENTS.BABY_PANEL, gameObject);
		controls[0] = gameObject;
		gameObject = transform.FindChild("Pause/SecondLevel/child").gameObject;
		secondLevelObjects.Add(PAUSE_ELEMENTS.CHILD_PANEL, gameObject);
		controls[1] = gameObject;
		gameObject = transform.FindChild("Pause/SecondLevel/daxia").gameObject;
		secondLevelObjects.Add(PAUSE_ELEMENTS.DAXIA_PANEL, gameObject);
		controls[2] = gameObject;
		gameObject = transform.FindChild("Pause/SecondLevel/master").gameObject;
		secondLevelObjects.Add(PAUSE_ELEMENTS.MASTER_PANEL, gameObject);
		controls[3] = gameObject;
		gameObject = transform.FindChild("Pause/ConfirmLevel/backBar").gameObject;
		confirmLevelObjects.Add(PAUSE_ELEMENTS.BACK_BAR, gameObject);
		gameObject = transform.FindChild("Pause/ConfirmLevel/backBar/exit_text").gameObject;
		confirmLevelObjects.Add(PAUSE_ELEMENTS.EXIT_TEXT, gameObject);
		gameObject = transform.FindChild("Pause/ConfirmLevel/backBar/exit_title").gameObject;
		confirmLevelObjects.Add(PAUSE_ELEMENTS.EXIT_TITLE, gameObject);
		gameObject = transform.FindChild("Pause/ConfirmLevel/backBar/restart_text").gameObject;
		confirmLevelObjects.Add(PAUSE_ELEMENTS.RESTART_TEXT, gameObject);
		gameObject = transform.FindChild("Pause/ConfirmLevel/backBar/restart_title").gameObject;
		confirmLevelObjects.Add(PAUSE_ELEMENTS.RESTART_TITLE, gameObject);
		gameObject = transform.FindChild("Pause/ConfirmLevel/okBtn").gameObject;
		confirmLevelObjects.Add(PAUSE_ELEMENTS.OK_BTN, gameObject);
		gameObject = transform.FindChild("Pause/ConfirmLevel/cancelBtn").gameObject;
		confirmLevelObjects.Add(PAUSE_ELEMENTS.CANCEL_BTN, gameObject);
		if (AppMain.Instance.GetCurrentPlatform() == "iphone")
		{
			modeShowPosDic.Add(0, new Vector3(-0.6532135f, -69.2854f, 0f));
			modeShowPosDic.Add(1, new Vector3(-29.85599f, -69.2854f, 0f));
			modeShowPosDic.Add(2, new Vector3(-32.51961f, -69.28534f, 0f));
			modeShowPosDic.Add(3, new Vector3(-29.85091f, -69.2854f, 0f));
		}
		else
		{
			modeShowPosDic.Add(0, new Vector3(-20.82462f, -77.30441f, 0f));
			modeShowPosDic.Add(1, new Vector3(-49.92397f, -75.15741f, 0f));
			modeShowPosDic.Add(2, new Vector3(-47.7756f, -73.01074f, 0f));
			modeShowPosDic.Add(3, new Vector3(-47.77559f, -75.15729f, 0f));
		}
		HideAllObjects();
	}

	private void SetStateToNormal(GameObject button)
	{
		if (button.GetComponent<UIButton>() != null)
		{
			button.GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.ACTIVE);
			button.GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.NORMAL);
		}
	}

	private CONTROL_TYPE LoadControlType()
	{
		return InputManager.Instance.LoadControlType();
	}

	private void SaveControlType(CONTROL_TYPE controlType)
	{
		InputManager.Instance.SaveControlType(controlType);
	}

	public void ShowPauseBtn()
	{
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> backgroundObject in backgroundObjects)
		{
			if (backgroundObject.Key == PAUSE_ELEMENTS.PAUSE_BTN)
			{
				backgroundObject.Value.SetActiveRecursively(state: true);
				SetStateToNormal(backgroundObject.Value);
				break;
			}
		}
	}

	public void HidePauseBtn()
	{
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> backgroundObject in backgroundObjects)
		{
			if (backgroundObject.Key == PAUSE_ELEMENTS.PAUSE_BTN)
			{
				backgroundObject.Value.SetActiveRecursively(state: false);
				break;
			}
		}
	}

	public void HideAllObjects()
	{
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> firstLevelObject in firstLevelObjects)
		{
			if (firstLevelObject.Value.active)
			{
				firstLevelObject.Value.SetActiveRecursively(state: false);
			}
			SetStateToNormal(firstLevelObject.Value);
		}
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> secondLevelObject in secondLevelObjects)
		{
			if (secondLevelObject.Value.active)
			{
				secondLevelObject.Value.SetActiveRecursively(state: false);
			}
			SetStateToNormal(secondLevelObject.Value);
		}
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> backgroundObject in backgroundObjects)
		{
			if (backgroundObject.Value.active)
			{
				backgroundObject.Value.SetActiveRecursively(state: false);
			}
			SetStateToNormal(backgroundObject.Value);
		}
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> confirmLevelObject in confirmLevelObjects)
		{
			if (confirmLevelObject.Value.active)
			{
				confirmLevelObject.Value.SetActiveRecursively(state: false);
			}
			SetStateToNormal(confirmLevelObject.Value);
		}
	}

	public void ShowBackground()
	{
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> backgroundObject in backgroundObjects)
		{
			if (backgroundObject.Key == PAUSE_ELEMENTS.PAUSE_BTN)
			{
				backgroundObject.Value.SetActiveRecursively(state: false);
			}
			else
			{
				backgroundObject.Value.SetActiveRecursively(state: true);
			}
		}
	}

	public void ShowPauseMark(bool show)
	{
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> backgroundObject in backgroundObjects)
		{
			if (backgroundObject.Key == PAUSE_ELEMENTS.PAUSE_MARK)
			{
				backgroundObject.Value.SetActiveRecursively(show);
				break;
			}
		}
	}

	public void ShowFirstLevel()
	{
		if (showSecondLevel || isShowingConfirmView)
		{
			return;
		}
		canClickBtn = 0;
		SceneUIManager.Instance.SetCanShowSlider(canShow: false);
		SceneUIManager.Instance.SetCanShowDrift(canShow: false);
		SceneUIManager.Instance.ShowSlider(show: false);
		if (SceneUIManager.Instance.isShowingTutorialUI)
		{
			canShowTutorialUI = true;
			SceneUIManager.Instance.ShowTutorialUI(show: false);
		}
		else
		{
			canShowTutorialUI = false;
		}
		SceneUIManager.Instance.HideRealtimeRank();
		Time.timeScale = 0f;
		AppMain.Instance.Pause = true;
		ShowBackground();
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> firstLevelObject in firstLevelObjects)
		{
			firstLevelObject.Value.SetActiveRecursively(state: true);
			SetStateToNormal(firstLevelObject.Value);
		}
		SceneUIManager.Instance.HideProp();
		SceneUIManager.Instance.HideSmallMap();
		SceneUIManager.Instance.HideTime();
		SceneUIManager.Instance.HideLap();
		SceneUIManager.Instance.HideReverseUI();
		SoundManager.Instance.PauseSteroSound();
		SoundManager.Instance.PauseAllMonoSound();
		controlIndex = (int)LoadControlType();
		if (controlIndex == 0 || controlIndex == 1)
		{
			canShowSlider = false;
		}
		else
		{
			canShowSlider = true;
		}
	}

	public void HideFirstLevel()
	{
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> firstLevelObject in firstLevelObjects)
		{
			if (firstLevelObject.Value.active)
			{
				firstLevelObject.Value.SetActiveRecursively(state: false);
			}
			SetStateToNormal(firstLevelObject.Value);
		}
	}

	public void ShowSecondLevel()
	{
		if (canClickBtn != 0)
		{
			return;
		}
		canClickBtn = 2;
		showSecondLevel = true;
		HideFirstLevel();
		ShowPauseMark(show: false);
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> secondLevelObject in secondLevelObjects)
		{
			if (secondLevelObject.Key == PAUSE_ELEMENTS.BACKGROUND)
			{
				secondLevelObject.Value.SetActiveRecursively(state: true);
			}
			else
			{
				secondLevelObject.Value.SetActiveRecursively(state: false);
			}
		}
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> secondLevelObject2 in secondLevelObjects)
		{
			secondLevelObject2.Value.SetActiveRecursively(state: true);
			SetStateToNormal(secondLevelObject2.Value);
		}
		controls[controlIndex].transform.localPosition = modeShowPosDic[controlIndex];
		ShowMode(controlIndex);
	}

	public void HideSecondLevel()
	{
		showSecondLevel = false;
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> secondLevelObject in secondLevelObjects)
		{
			if (secondLevelObject.Value.active)
			{
				secondLevelObject.Value.SetActiveRecursively(state: false);
			}
			SetStateToNormal(secondLevelObject.Value);
		}
	}

	public void RightArrowClicked()
	{
		controlIndex++;
		if (controlIndex > 3)
		{
			controlIndex = 0;
		}
		for (int i = 0; i <= 3; i++)
		{
			if (i == controlIndex)
			{
				controls[i].transform.localPosition = modeShowPosDic[controlIndex];
				ShowMode(controlIndex);
			}
			else
			{
				controls[i].transform.localPosition = new Vector3(-1000f, 0f, 0f);
			}
		}
		if (controlIndex == 0 || controlIndex == 1)
		{
			canShowSlider = false;
		}
		else
		{
			canShowSlider = true;
		}
	}

	private void ShowMode(int index)
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < controls[i].transform.GetChildCount(); j++)
			{
				if (i == index)
				{
					controls[i].transform.GetChild(j).gameObject.SetActiveRecursively(state: true);
				}
				else
				{
					controls[i].transform.GetChild(j).gameObject.SetActiveRecursively(state: false);
				}
			}
		}
	}

	private void HideMode()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < controls[i].transform.GetChildCount(); j++)
			{
				controls[i].transform.GetChild(j).gameObject.SetActiveRecursively(state: false);
			}
		}
	}

	public void LevtArrowClicked()
	{
		controlIndex--;
		Debug.Log("Index:" + controlIndex);
		if (controlIndex < 0)
		{
			controlIndex = 3;
		}
		for (int i = 0; i <= 3; i++)
		{
			if (i == controlIndex)
			{
				controls[i].transform.localPosition = modeShowPosDic[controlIndex];
				ShowMode(controlIndex);
			}
			else
			{
				controls[i].transform.localPosition = new Vector3(-1000f, 0f, 0f);
			}
		}
		if (controlIndex == 0 || controlIndex == 1)
		{
			canShowSlider = false;
		}
		else
		{
			canShowSlider = true;
		}
	}

	public void BackBtnPressed()
	{
		canClickBtn = 0;
		HideMode();
		SaveControlType((CONTROL_TYPE)controlIndex);
		HideSecondLevel();
		ShowFirstLevel();
		ShowPauseMark(show: true);
	}

	public void BackToGame()
	{
		canClickBtn = 1;
		isShowingConfirmView = false;
		showSecondLevel = false;
		DriveManager.Instance.Reset();
		DriveManager.Instance.SetCanDrive(drive: true);
		AppMain.Instance.Pause = false;
		HideAllObjects();
		ShowPauseBtn();
		SceneUIManager.Instance.SetCanShowSlider(canShowSlider);
		SceneUIManager.Instance.SetCanShowDrift(canShow: true);
		SceneUIManager.Instance.ShowSlider(canShowSlider);
		SceneUIManager.Instance.ShowHandbreak(show: true);
		if (canShowTutorialUI)
		{
			SceneUIManager.Instance.ShowTutorialUI(show: true);
		}
		SceneUIManager.Instance.ShowRealtimeRank();
		Time.timeScale = 1f;
		PlayerData data = new PlayerData();
		if (RaceSceneData.GetPlayerData((ulong)TSingleton<CarManager>.Instance.GetHeroCar().ID, ref data))
		{
			SceneUIManager.Instance.ShowTime(data.SpendTime);
			SceneUIManager.Instance.ShowLap(data.ShowLaps);
			SceneUIManager.Instance.ShowSmallMap();
			SceneUIManager.Instance.ShowProp();
			SceneUIManager.Instance.ShowReverseUI();
		}
		SoundManager.Instance.ContiueSteroSound();
		SoundManager.Instance.ContinueAllMonoSound();
	}

	public void Restart()
	{
		if (canClickBtn == 0)
		{
			canClickBtn = 3;
			m_confirmType = CONFIRM_TYPE.CONFIRM_TYPE_RESTART;
			ShowRestartConfirmView();
		}
	}

	public void FinishGame()
	{
		if (canClickBtn == 0)
		{
			canClickBtn = 4;
			m_confirmType = CONFIRM_TYPE.CONFIRM_TYPE_EXIT;
			ShowExitConfirmView();
		}
	}

	private void ShowRestartConfirmView()
	{
		HideFirstLevel();
		isShowingConfirmView = true;
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.BACK_BAR])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.BACK_BAR].active = true;
		}
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.OK_BTN])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.OK_BTN].active = true;
		}
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.CANCEL_BTN])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.CANCEL_BTN].active = true;
		}
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.RESTART_TEXT])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.RESTART_TEXT].active = true;
		}
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.RESTART_TITLE])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.RESTART_TITLE].active = true;
		}
	}

	private void ShowExitConfirmView()
	{
		HideFirstLevel();
		isShowingConfirmView = true;
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.BACK_BAR])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.BACK_BAR].active = true;
		}
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.OK_BTN])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.OK_BTN].active = true;
		}
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.CANCEL_BTN])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.CANCEL_BTN].active = true;
		}
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.EXIT_TEXT])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.EXIT_TEXT].active = true;
		}
		if ((bool)confirmLevelObjects[PAUSE_ELEMENTS.EXIT_TITLE])
		{
			confirmLevelObjects[PAUSE_ELEMENTS.EXIT_TITLE].active = true;
		}
	}

	private void ConfirmExit()
	{
		Time.timeScale = 1f;
		QuitLevel(restart: false);
		SceneManager.Instance.LoadEmptyLevel();
		DriveManager.Instance.Reset();
		DriveManager.Instance.SetCanDrive(drive: true);
		InputManager.Instance.bTouchMode_Break = false;
		Resources.UnloadUnusedAssets();
		timer = DelayDelegateTimer.Create();
		timer.InitTimer(3f, DelayLoadingCharacterAndSelectTrackLevel);
		timer.StartTimer();
		Object.DontDestroyOnLoad(timer.gameObject);
	}

	private void ConfirmRestart()
	{
		Time.timeScale = 1f;
		lastPlayGameLevel = SceneManager.Instance.CurGameSceneId;
		LocalUserInfo.Instance.TrackLevel = TrackLockManager.Instance.CheckDiffculty(SceneManager.Instance.CurGameSceneId);
		QuitLevel(restart: true);
		SceneManager.Instance.LoadEmptyLevel();
		DriveManager.Instance.Reset();
		DriveManager.Instance.SetCanDrive(drive: true);
		InputManager.Instance.bTouchMode_Break = false;
		Resources.UnloadUnusedAssets();
		timer = DelayDelegateTimer.Create();
		timer.InitTimer(3f, DelayLoadingCurrentGameLevel);
		timer.StartTimer();
		Object.DontDestroyOnLoad(timer.gameObject);
	}

	public void Confirm()
	{
		canClickBtn = 0;
		switch (m_confirmType)
		{
		case CONFIRM_TYPE.CONFIRM_TYPE_EXIT:
			ConfirmExit();
			break;
		case CONFIRM_TYPE.CONFIRM_TYPE_RESTART:
			ConfirmRestart();
			break;
		}
	}

	public void Cancel()
	{
		canClickBtn = 0;
		isShowingConfirmView = false;
		m_confirmType = CONFIRM_TYPE.CONFIRM_TYPE_NONE;
		HideConfirmLevel();
		ShowFirstLevel();
	}

	private void HideConfirmLevel()
	{
		foreach (KeyValuePair<PAUSE_ELEMENTS, GameObject> confirmLevelObject in confirmLevelObjects)
		{
			confirmLevelObject.Value.active = false;
		}
	}

	public void QuitLevel(bool restart)
	{
		SoundManager.Instance.StopAllSound();
		RaceModeFactory.CurrentRaceMode.Uninit();
		TSingleton<PropManager>.Instance.Clear();
		SoundManager.Instance.ClearSteroAudios();
		if (null != SceneUIManager.Instance.m_Loading)
		{
			SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
			SceneUIManager.Instance.m_Loading.GetComponent<LoadingController>().ShowLoadingIndicator();
		}
		AppMain.Instance.Pause = false;
	}

	private void DelayLoadingCharacterAndSelectTrackLevel()
	{
		Debug.Log("Reload DelayLoadingCharacterAndSelectTrackLevel ");
		SceneManager.Instance.LoadLevel(1003);
		Object.DestroyObject(timer.gameObject);
	}

	private void DelayLoadingCurrentGameLevel()
	{
		Debug.Log("Reload level " + lastPlayGameLevel);
		SceneManager.Instance.LoadLevel(lastPlayGameLevel);
		Object.DestroyObject(timer.gameObject);
	}

	public void UpdateUI()
	{
	}
}
