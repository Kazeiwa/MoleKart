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
		AUTO_A_PANEL,
		AUTO_B_PANEL,
		AUTO_C_PANEL,
		MANUAL_A_PANEL,
		MANUAL_B_PANEL,
		MANUAL_C_PANEL,
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

	private CONFIRM_TYPE m_confirmType;

	private Dictionary<PAUSE_ELEMENTS, GameObject> backgroundObjects;

	private Dictionary<PAUSE_ELEMENTS, GameObject> firstLevelObjects;

	private Dictionary<PAUSE_ELEMENTS, GameObject> secondLevelObjects;

	private Dictionary<PAUSE_ELEMENTS, GameObject> confirmLevelObjects;

	private int controlIndex;

	private GameObject[] controls;

	private Dictionary<int, Vector3> modeShowPosDic;

	private int canClickBtn;

	private bool isShowingConfirmView;

	private int lastPlayGameLevel;

	private DelayDelegateTimer timer;

	public void InitUI(Transform transform)
	{
	}

	private void SetStateToNormal(GameObject button)
	{
	}

	private CONTROL_TYPE LoadControlType()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SaveControlType(CONTROL_TYPE controlType)
	{
	}

	public void ShowPauseBtn()
	{
	}

	public void HidePauseBtn()
	{
	}

	public void HideAllObjects()
	{
	}

	public void ShowBackground()
	{
	}

	public void ShowPauseMark(bool show)
	{
	}

	public void ShowFirstLevel()
	{
	}

	public void HideFirstLevel()
	{
	}

	public void ShowSecondLevel()
	{
	}

	public void HideSecondLevel()
	{
	}

	public void RightArrowClicked()
	{
	}

	private void ShowMode(int index)
	{
	}

	private void HideMode()
	{
	}

	public void LevtArrowClicked()
	{
	}

	public void BackBtnPressed()
	{
	}

	public void BackToGame()
	{
	}

	public void Restart()
	{
	}

	public void FinishGame()
	{
	}

	private void ShowRestartConfirmView()
	{
	}

	private void ShowExitConfirmView()
	{
	}

	private void ConfirmExit()
	{
	}

	private void ConfirmRestart()
	{
	}

	public void Confirm()
	{
	}

	public void Cancel()
	{
	}

	private void HideConfirmLevel()
	{
	}

	public void QuitLevel(bool restart)
	{
	}

	private void DelayLoadingCharacterAndSelectTrackLevel()
	{
	}

	private void DelayLoadingCurrentGameLevel()
	{
	}

	public void UpdateUI()
	{
	}
}
