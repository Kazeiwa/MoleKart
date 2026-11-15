using UnityEngine;

public class SettingsController : MonoBehaviour
{
	private string language;

	private string platform;

	private CONTROL_TYPE controlType = CONTROL_TYPE.CONTROLTYPE_MASTER;

	private bool settingControlType = true;

	private Rect controlRect;

	private Rect modeRect;

	private GameObject support;

	private GameObject goMode;

	private Vector3 backgroundPos;

	private GameObject AutoDriftA;

	private GameObject AutoDriftB;

	private GameObject ManualDriftA;

	private GameObject ManualDriftB;

	private void Start()
	{
		platform = AppMain.Instance.GetCurrentPlatform();
		if (platform != string.Empty)
		{
			platform += "/";
		}
		else
		{
			platform = "ipad/";
		}
		if (platform == "iphone/")
		{
			controlRect = new Rect(270f, 182f, 670f, 455f);
			modeRect = new Rect(282f, 88f, 635f, 545f);
		}
		else
		{
			controlRect = new Rect(301f, 193f, 700f, 540f);
			modeRect = new Rect(301f, 143f, 675f, 575f);
		}
		AutoDriftA = GameObject.Find("UI/GameObject/controls/A-a");
		AutoDriftB = GameObject.Find("UI/GameObject/controls/M-a");
		ManualDriftA = GameObject.Find("UI/GameObject/controls/A-b");
		ManualDriftB = GameObject.Find("UI/GameObject/controls/M-b");
		goMode = GameObject.Find("UI/GameObject/Mode");
		goMode.active = false;
		CONTROL_TYPE cONTROL_TYPE = (controlType = InputManager.Instance.LoadControlType());
		SetControlModeTex();
		switch (cONTROL_TYPE)
		{
		case CONTROL_TYPE.CONTROLTYPE_BABY:
			GameObject.Find("baby").GetComponent<UIRadioBtn>().Value = true;
			break;
		case CONTROL_TYPE.CONTROLTYPE_CHILD:
			GameObject.Find("child").GetComponent<UIRadioBtn>().Value = true;
			break;
		case CONTROL_TYPE.CONTROLTYPE_DAXIA:
			GameObject.Find("daxia").GetComponent<UIRadioBtn>().Value = true;
			break;
		case CONTROL_TYPE.CONTROLTYPE_MASTER:
			GameObject.Find("master").GetComponent<UIRadioBtn>().Value = true;
			break;
		}
		GameObject.Find("controlBtn").GetComponent<UIRadioBtn>().Value = true;
		support = GameObject.Find("Support");
		GameObject gameObject = GameObject.Find("UI/GameObject/gray_background");
		backgroundPos = gameObject.transform.position;
		ShowProps(show: false);
		ControlBtnSelected();
	}

	private void OnDestroy()
	{
		Debug.Log("Settings Destroy()!!");
		GameObject gameObject = GameObject.Find("Props");
		if (gameObject != null)
		{
			Object.DestroyImmediate(gameObject);
		}
		gameObject = GameObject.Find("UI");
		if (gameObject != null)
		{
			Object.DestroyImmediate(gameObject);
		}
		Resources.UnloadUnusedAssets();
	}

	private void BackToMainMenu()
	{
		UtilBinding.ShowWaitingView(bShow: true);
		SceneManager.Instance.LoadLevel(1001);
	}

	private void ShowControlSettingButtons(bool show)
	{
		GameObject gameObject = GameObject.Find("baby");
		gameObject.GetComponent<UIRadioBtn>().Hide(!show);
		gameObject = GameObject.Find("baby/text");
		gameObject.GetComponent<UIPictureLabel>().Hide(!show);
		gameObject = GameObject.Find("child");
		gameObject.GetComponent<UIRadioBtn>().Hide(!show);
		gameObject = GameObject.Find("child/text");
		gameObject.GetComponent<UIPictureLabel>().Hide(!show);
		gameObject = GameObject.Find("daxia");
		gameObject.GetComponent<UIRadioBtn>().Hide(!show);
		gameObject = GameObject.Find("daxia/text");
		gameObject.GetComponent<UIPictureLabel>().Hide(!show);
		gameObject = GameObject.Find("master");
		gameObject.GetComponent<UIRadioBtn>().Hide(!show);
		gameObject = GameObject.Find("master/text");
		gameObject.GetComponent<UIPictureLabel>().Hide(!show);
	}

	private void ControlBtnSelected()
	{
		ShowProps(show: false);
		settingControlType = true;
		SetControlModeTex();
		ShowControlSettingButtons(show: true);
		ShowMode(bShow: false);
		if ((bool)support)
		{
			support.SetActiveRecursively(state: false);
		}
	}

	private void ModeBtnSelected()
	{
		ShowProps(show: false);
		settingControlType = false;
		SetControlModeTex();
		ShowControlSettingButtons(show: false);
		ShowMode(bShow: true);
		if ((bool)support)
		{
			support.SetActiveRecursively(state: false);
		}
	}

	private void PropBtnSelected()
	{
		ShowProps(show: true);
		settingControlType = false;
		SetControlModeTex();
		ShowControlSettingButtons(show: false);
		ShowMode(bShow: false);
		if ((bool)support)
		{
			support.SetActiveRecursively(state: false);
		}
	}

	private void SupportBtnSelected()
	{
		ShowProps(show: false);
		settingControlType = false;
		SetControlModeTex();
		ShowControlSettingButtons(show: false);
		ShowMode(bShow: false);
		if ((bool)support)
		{
			support.SetActiveRecursively(state: true);
		}
	}

	private void BabyModeSelected()
	{
		controlType = CONTROL_TYPE.CONTROLTYPE_BABY;
		SetControlModeTex();
	}

	private void ChildModeSelected()
	{
		controlType = CONTROL_TYPE.CONTROLTYPE_CHILD;
		SetControlModeTex();
	}

	private void DaxiaModeSelected()
	{
		controlType = CONTROL_TYPE.CONTROLTYPE_DAXIA;
		SetControlModeTex();
	}

	private void MasterModeSelected()
	{
		controlType = CONTROL_TYPE.CONTROLTYPE_MASTER;
		SetControlModeTex();
	}

	private void SetControlModeTex()
	{
		AutoDriftA.active = false;
		AutoDriftB.active = false;
		ManualDriftB.active = false;
		ManualDriftA.active = false;
		if (settingControlType)
		{
			switch (controlType)
			{
			case CONTROL_TYPE.CONTROLTYPE_BABY:
				AutoDriftA.active = true;
				break;
			case CONTROL_TYPE.CONTROLTYPE_CHILD:
				AutoDriftB.active = true;
				break;
			case CONTROL_TYPE.CONTROLTYPE_DAXIA:
				ManualDriftA.active = true;
				break;
			case CONTROL_TYPE.CONTROLTYPE_MASTER:
				ManualDriftB.active = true;
				break;
			}
		}
		SaveControlMode();
	}

	private void SaveControlMode()
	{
		InputManager.Instance.SaveControlType(controlType);
	}

	private void ShowMode(bool bShow)
	{
		if (goMode != null)
		{
			goMode.active = bShow;
		}
	}

	private void ShowProps(bool show)
	{
		UIScrollList component = GameObject.Find("Prop_Scroll").GetComponent<UIScrollList>();
		if (show)
		{
			component.transform.position = new Vector3(backgroundPos.x, backgroundPos.y, backgroundPos.z - 3f);
		}
		else
		{
			component.transform.position = new Vector3(2009.151f, -76.49939f, -146.375f);
		}
		component.touchScroll = show;
	}
}
