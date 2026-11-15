using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
	private string language;

	private string platform;

	private CONTROL_TYPE controlType;

	private bool settingControlType;

	private Rect controlRect;

	private Rect modeRect;

	private GameObject support;

	private GameObject goMode;

	public GameObject background;

	public GameObject modeBtnList;

	private GameObject AutoDriftA;

	private GameObject AutoDriftB;

	private GameObject AutoDriftC;

	private GameObject ManualDriftA;

	private GameObject ManualDriftB;

	private GameObject ManualDriftC;

	private Dictionary<CONTROL_TYPE, float> scrollFloatDic;

	private bool scrollDicSet;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void BackToMainMenu()
	{
	}

	private void SetScrollFloatDic()
	{
	}

	private void ScrollToSelected()
	{
	}

	private void ShowControlSettingButtons(bool show)
	{
	}

	private void ControlBtnSelected()
	{
	}

	private void ModeBtnSelected()
	{
	}

	private void PropBtnSelected()
	{
	}

	private void SupportBtnSelected()
	{
	}

	private void AutoAModeSelected()
	{
	}

	private void AutoBModeSelected()
	{
	}

	private void AutoCModeSelected()
	{
	}

	private void ManualAModeSelected()
	{
	}

	private void ManualBModeSelected()
	{
	}

	private void ManualCModeSelected()
	{
	}

	private void SetControlModeTex()
	{
	}

	private void SaveControlMode()
	{
	}

	private void ShowMode(bool bShow)
	{
	}

	private void ShowProps(bool show)
	{
	}
}
