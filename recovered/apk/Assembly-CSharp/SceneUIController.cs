using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIController : MonoBehaviour
{
	private TimeUI m_TimeUI;

	private LoadingUI m_LoadingUI;

	private PropUI m_PropUI;

	private LapUI m_LapUI;

	private CountDownUI m_CountDownUI;

	private SmallMapUI m_SmallMapUI;

	private ArrayList m_UIGroup = new ArrayList();

	private FadeInAndOut m_FadeInAndOut;

	private ReverseUI m_ReverseUI;

	private RealtimeRankUIController m_RealtimeRankUI;

	private PauseUI m_PauseUI;

	private ControlPanelUI m_ControlPanel;

	private SceneIntroUI m_SceneIntroUI;

	private TurorialUI m_TutorialUI;

	public bool canShowSlider = true;

	private Dictionary<int, GameObject> m_RankNumber = new Dictionary<int, GameObject>();

	private bool raceIsStarted;

	private Object tutorial;

	private RankUI m_RankUI;

	public PauseUI PauseUIController => m_PauseUI;

	public RealtimeRankUIController RealtimeUI => m_RealtimeRankUI;

	private void Start()
	{
		SceneUIManager.Instance.Init(AppMain.Instance.GetCurrentSystemLanguage(), AppMain.Instance.GetCurrentPlatform());
		if (m_SceneIntroUI == null)
		{
			m_SceneIntroUI = new SceneIntroUI();
		}
		m_SceneIntroUI.InitUI(base.transform);
		m_UIGroup.Add(m_SceneIntroUI);
		if (m_ControlPanel == null)
		{
			m_ControlPanel = new ControlPanelUI();
		}
		m_ControlPanel.InitUI(base.transform);
		m_UIGroup.Add(m_ControlPanel);
		CONTROL_TYPE cONTROL_TYPE = InputManager.Instance.LoadControlType();
		if (cONTROL_TYPE == CONTROL_TYPE.CONTROLTYPE_DAXIA || cONTROL_TYPE == CONTROL_TYPE.CONTROLTYPE_MASTER)
		{
			m_ControlPanel.CanShowSlider = true;
		}
		else
		{
			m_ControlPanel.CanShowSlider = false;
		}
		ShowIntroUI();
		GameObject gameObject = base.transform.FindChild("ControlPanel/drift").gameObject;
		GameObject gameObject2 = base.transform.FindChild("ControlPanel/background/slider").gameObject;
		GameObject gameObject3 = base.transform.FindChild("ControlPanel/handbreak").gameObject;
		if (InputManager.Instance.gestureChecker != null)
		{
			if (gameObject != null)
			{
				InputManager.Instance.gestureChecker.driftUI = gameObject;
			}
			if (gameObject3 != null)
			{
				InputManager.Instance.gestureChecker.breakUI = gameObject3;
			}
			if (gameObject2 != null)
			{
				InputManager.Instance.gestureChecker.sliderUI = gameObject2;
			}
		}
		SceneUIManager.Instance.m_SceneUIController = this;
	}

	private void OnDestroy()
	{
		HalfUnInit();
		m_UIGroup = null;
		m_FadeInAndOut = null;
		m_ReverseUI = null;
		Debug.Log("SceneUIController: OnDestroy()!");
		SceneUIManager.Instance.UnInit();
	}

	public void SetRaceIsStarted(bool started)
	{
		if (raceIsStarted != started)
		{
			raceIsStarted = started;
		}
	}

	public bool RaceIsStarted()
	{
		return raceIsStarted;
	}

	public void ShowPauseUI()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.ShowPauseBtn();
		}
	}

	public void ShowPause()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.ShowFirstLevel();
		}
	}

	public void PauseBtnPressed()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.ShowFirstLevel();
		}
	}

	public void BackToGameBtnPressed()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.BackToGame();
		}
	}

	public void RestartBtnPressed()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.Restart();
		}
	}

	public void ControlBtnPressed()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.ShowSecondLevel();
		}
	}

	public void FinishBtnPressed()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.FinishGame();
		}
	}

	public void BackBtnPressed()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.BackBtnPressed();
		}
	}

	public void RightArrowBtnPressed()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.RightArrowClicked();
		}
	}

	public void LeftArrowBtnPressed()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.LevtArrowClicked();
		}
	}

	public void HidePauseUI()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.HideAllObjects();
		}
	}

	public void ShowRealtimeRank()
	{
		if (m_RealtimeRankUI != null)
		{
			m_RealtimeRankUI.showUI(show: true);
		}
	}

	public void HideRealtimeRank()
	{
		if (m_RealtimeRankUI != null)
		{
			m_RealtimeRankUI.showUI(show: false);
		}
	}

	public void HideIntroUI()
	{
		if (m_SceneIntroUI != null)
		{
			m_SceneIntroUI.HideIntroUI();
		}
	}

	public void ShowIntroUI()
	{
		if (m_SceneIntroUI != null)
		{
			m_SceneIntroUI.ShowIntroUI();
		}
	}

	public void SetCanShowSlider(bool canShow)
	{
		if (m_ControlPanel != null)
		{
			m_ControlPanel.CanShowSlider = canShow;
		}
	}

	public void SetCanShowDrift(bool canShow)
	{
		if (m_ControlPanel != null)
		{
			m_ControlPanel.CanShowDrift = canShow;
		}
	}

	public void ShowSlider(bool show)
	{
		if (m_ControlPanel != null)
		{
			m_ControlPanel.ShowSlider(show);
		}
	}

	public void ShowHandbreak(bool show)
	{
		GameObject gameObject = base.transform.FindChild("ControlPanel/handbreak").gameObject;
		gameObject.active = show;
		if (show)
		{
			GameObject gameObject2 = base.transform.FindChild("ControlPanel/drift").gameObject;
			UIButton component = gameObject2.GetComponent<UIButton>();
			component.ForceNormal();
			UIButton component2 = gameObject.GetComponent<UIButton>();
			component2.ForceNormal();
		}
	}

	public void ShowTutorial(bool show)
	{
		if (show)
		{
			Vector3 position = new Vector3(119.6136f, -20.17294f, 40.68945f);
			GameObject original = Resources.Load("Turorial/" + AppMain.Instance.GetCurrentSystemLanguage() + "/Tutorial") as GameObject;
			tutorial = Object.Instantiate(original, position, default(Quaternion));
		}
		else
		{
			DestroyTutorialUI();
		}
	}

	public void DestroyTutorialUI()
	{
		if ((bool)tutorial)
		{
			Object.DestroyImmediate(tutorial);
			tutorial = null;
		}
	}

	private void Cancel()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.Cancel();
		}
	}

	private void Confirm()
	{
		if (m_PauseUI != null)
		{
			m_PauseUI.Confirm();
		}
	}

	public void ShowRankUI()
	{
		DriveManager.Instance.Reset();
		if (m_RealtimeRankUI != null)
		{
			m_RealtimeRankUI.CanShowUI = false;
		}
		if (!GameObject.Find("FinishUI"))
		{
			SceneUIManager.Instance.HidePauseUI();
			SceneManager.Instance.LoadLevelAdditive(101);
		}
	}

	public void ShowRankUI(bool isNetwork, bool isHost)
	{
		if (!GameObject.Find("FinishUI"))
		{
			SceneUIManager.Instance.HidePauseUI();
			SceneManager.Instance.LoadLevelAdditive(101);
			if (m_RealtimeRankUI != null)
			{
				m_RealtimeRankUI.CanShowUI = false;
			}
		}
	}

	public void RemoveAllSceneUI()
	{
		HalfUnInit();
		m_UIGroup.Clear();
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < base.transform.GetChildCount(); i++)
		{
			arrayList.Add(base.transform.GetChild(i));
		}
		for (int j = 0; j < arrayList.Count; j++)
		{
			if (!(((Transform)arrayList[j]).name == "UICamera") && !(((Transform)arrayList[j]).name == "UIManager"))
			{
				Object.Destroy(((Transform)arrayList[j]).gameObject);
			}
		}
		if ((bool)GameObject.Find("Tutorial(Clone)"))
		{
			GameObject obj = GameObject.Find("Tutorial(Clone)");
			Object.DestroyImmediate(obj);
		}
	}

	public void RemoveAllSceneUIExceptCount(bool finish)
	{
		if (!finish)
		{
			return;
		}
		m_UIGroup.Clear();
		m_UIGroup.Add(m_CountDownUI);
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < base.transform.GetChildCount(); i++)
		{
			arrayList.Add(base.transform.GetChild(i));
		}
		for (int j = 0; j < arrayList.Count; j++)
		{
			if (!(((Transform)arrayList[j]).parent.name == "FinishCount") && !(((Transform)arrayList[j]).name == "FinishCount") && !(((Transform)arrayList[j]).name == "UICamera") && !(((Transform)arrayList[j]).name == "UIManager"))
			{
				Object.Destroy(((Transform)arrayList[j]).gameObject);
			}
		}
	}

	public void RemoveAllSceneUIExceptCount()
	{
		HalfUnInit();
		Debug.LogWarning("Remove all sceneui except count");
		m_UIGroup.Clear();
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < base.transform.GetChildCount(); i++)
		{
			arrayList.Add(base.transform.GetChild(i));
		}
		for (int j = 0; j < arrayList.Count; j++)
		{
			if (!(((Transform)arrayList[j]).parent.name == "FinishCount") && !(((Transform)arrayList[j]).name == "FinishCount") && !(((Transform)arrayList[j]).name == "UICamera") && !(((Transform)arrayList[j]).name == "UIManager"))
			{
				Object.Destroy(((Transform)arrayList[j]).gameObject);
			}
		}
	}

	public void HideRankUI()
	{
		if (m_RankUI != null)
		{
			Object.Destroy(m_RankUI);
		}
		m_RankUI = null;
	}

	public void InitSceneUI()
	{
		Debug.Log("INTO INIT ONLINE GAME UI");
		if (m_SmallMapUI == null)
		{
			m_SmallMapUI = new SmallMapUI();
		}
		m_SmallMapUI.InitUI(base.transform);
		m_UIGroup.Add(m_SmallMapUI);
		if (m_CountDownUI == null)
		{
			m_CountDownUI = new CountDownUI();
		}
		m_CountDownUI.InitUI(base.transform);
		m_UIGroup.Add(m_CountDownUI);
		if (m_PropUI == null)
		{
			m_PropUI = new PropUI();
		}
		m_PropUI.InitUI(base.transform);
		m_UIGroup.Add(m_PropUI);
		if (m_TimeUI == null)
		{
			m_TimeUI = new TimeUI();
		}
		m_TimeUI.InitUI(base.transform);
		m_UIGroup.Add(m_TimeUI);
		if (m_LapUI == null)
		{
			m_LapUI = new LapUI();
		}
		m_LapUI.InitUI(base.transform);
		m_UIGroup.Add(m_LapUI);
		if (m_FadeInAndOut == null)
		{
			m_FadeInAndOut = new FadeInAndOut();
		}
		m_FadeInAndOut.InitUI(base.transform);
		m_UIGroup.Add(m_FadeInAndOut);
		if (m_ReverseUI == null)
		{
			m_ReverseUI = new ReverseUI();
		}
		m_ReverseUI.InitUI(base.transform);
		m_UIGroup.Add(m_ReverseUI);
		if (m_RealtimeRankUI == null)
		{
			m_RealtimeRankUI = new RealtimeRankUIController();
		}
		m_RealtimeRankUI.InitUI(base.transform);
		m_UIGroup.Add(m_RealtimeRankUI);
		if (m_PauseUI == null)
		{
			m_PauseUI = new PauseUI();
		}
		m_PauseUI.InitUI(base.transform);
		m_UIGroup.Add(m_PauseUI);
		HideIntroUI();
		ShowRealtimeRank();
		ShowSlider(show: true);
		SetCanShowDrift(canShow: true);
		ShowPauseUI();
		if (AppMain.Instance.IsSpeedMode)
		{
			Transform transform = base.transform.FindChild("Tutorial");
			if (null != transform)
			{
				Object.DestroyImmediate(transform.gameObject);
				Resources.UnloadUnusedAssets();
			}
		}
	}

	public void InitOnlineGameSceneUI()
	{
		Debug.Log("INTO INIT ONLINE GAME UI");
		if (m_SmallMapUI == null)
		{
			m_SmallMapUI = new SmallMapUI();
		}
		m_SmallMapUI.InitUI(base.transform);
		m_UIGroup.Add(m_SmallMapUI);
		if (m_CountDownUI == null)
		{
			m_CountDownUI = new CountDownUI();
		}
		m_CountDownUI.InitUI(base.transform);
		m_UIGroup.Add(m_CountDownUI);
		if (m_PropUI == null)
		{
			m_PropUI = new PropUI();
		}
		m_PropUI.InitUI(base.transform);
		m_UIGroup.Add(m_PropUI);
		if (m_TimeUI == null)
		{
			m_TimeUI = new TimeUI();
		}
		m_TimeUI.InitUI(base.transform);
		m_UIGroup.Add(m_TimeUI);
		if (m_LapUI == null)
		{
			m_LapUI = new LapUI();
		}
		m_LapUI.InitUI(base.transform);
		m_UIGroup.Add(m_LapUI);
		if (m_FadeInAndOut == null)
		{
			m_FadeInAndOut = new FadeInAndOut();
		}
		m_FadeInAndOut.InitUI(base.transform);
		m_UIGroup.Add(m_FadeInAndOut);
		if (m_ReverseUI == null)
		{
			m_ReverseUI = new ReverseUI();
		}
		m_ReverseUI.InitUI(base.transform);
		m_UIGroup.Add(m_ReverseUI);
		if (m_RealtimeRankUI == null)
		{
			m_RealtimeRankUI = new RealtimeRankUIController();
		}
		m_RealtimeRankUI.InitUI(base.transform);
		m_UIGroup.Add(m_RealtimeRankUI);
		if (m_PauseUI == null)
		{
			m_PauseUI = new PauseUI();
		}
		m_PauseUI.InitUI(base.transform);
		m_UIGroup.Add(m_PauseUI);
		HideIntroUI();
		ShowRealtimeRank();
		ShowSlider(show: true);
		SetCanShowDrift(canShow: true);
		if (AppMain.Instance.IsSpeedMode)
		{
			Transform transform = base.transform.FindChild("Tutorial");
			if (null != transform)
			{
				Object.DestroyImmediate(transform.gameObject);
				Resources.UnloadUnusedAssets();
			}
		}
		if (AppMain.Instance.IsOnlineMode)
		{
			Transform transform2 = base.transform.FindChild("Pause");
			if (null != transform2)
			{
				Object.DestroyImmediate(transform2.gameObject);
				Resources.UnloadUnusedAssets();
			}
			m_PauseUI = null;
		}
	}

	public void RotateCar(Car car)
	{
		if (m_SmallMapUI != null)
		{
			m_SmallMapUI.RotateCar(car);
		}
	}

	public void ShowLap(int lap)
	{
		if (m_LapUI != null)
		{
			m_LapUI.ShowLap(lap);
		}
	}

	public void HideLap()
	{
		if (m_LapUI != null)
		{
			m_LapUI.HideLap();
		}
	}

	public void ShowSmallMap()
	{
		if (m_SmallMapUI != null)
		{
			m_SmallMapUI.ShowSmallMap();
		}
	}

	public void HideSmallMap()
	{
		if (m_SmallMapUI != null)
		{
			m_SmallMapUI.HideSmallMap();
		}
	}

	public void ShowTime(float fTime)
	{
		if (m_TimeUI != null)
		{
			m_TimeUI.ShowTime(fTime);
		}
	}

	public void HideTime()
	{
		if (m_TimeUI != null)
		{
			m_TimeUI.HideTime();
		}
	}

	public void FadeIn(float fTime)
	{
		if (m_FadeInAndOut != null)
		{
			m_FadeInAndOut.FadeIn(fTime);
		}
	}

	public void AutoFadeInAndOut(float fTime)
	{
		if (m_FadeInAndOut != null)
		{
			m_FadeInAndOut.AutoFadeInAndOut(fTime);
		}
	}

	public void DisableLoadingUI()
	{
		if (m_LoadingUI != null)
		{
			m_LoadingUI.DisableLoadingUI();
		}
	}

	public void DisableCountDown()
	{
		if (m_CountDownUI != null)
		{
			m_CountDownUI.DisableCountDown();
			Transform transform = base.transform.FindChild("CountDown");
			if (null != transform)
			{
				Object.DestroyImmediate(transform.gameObject);
			}
			m_CountDownUI = null;
		}
	}

	public void CountDown(int index)
	{
		if (m_CountDownUI != null)
		{
			m_CountDownUI.CountDown(index);
		}
	}

	public void ShowProp(ePropType type)
	{
		if (m_PropUI != null)
		{
			m_PropUI.ShowProp(type);
		}
	}

	public void RollPropUI(ePropType type)
	{
		if (m_PropUI != null)
		{
			m_PropUI.RollPropUI(type);
		}
	}

	public void HideProp()
	{
		if (m_PropUI != null)
		{
			m_PropUI.HideProp();
		}
	}

	public void ShowProp()
	{
		if (m_PropUI != null)
		{
			m_PropUI.ShowProp();
		}
	}

	public void HideReverseUI()
	{
		if (m_ReverseUI != null)
		{
			m_ReverseUI.HideReverseUI();
		}
	}

	public void ShowReverseUI()
	{
		if (m_ReverseUI != null)
		{
			m_ReverseUI.ShowReverseUI();
		}
	}

	private void Update()
	{
		if (m_UIGroup == null)
		{
			return;
		}
		for (int i = 0; i < m_UIGroup.Count; i++)
		{
			if (m_UIGroup[i] != null)
			{
				((ISceneUI)m_UIGroup[i]).UpdateUI();
			}
		}
	}

	public void HalfUnInit()
	{
		if (InputManager.Instance.gestureChecker != null)
		{
			InputManager.Instance.gestureChecker.Reset();
		}
		m_TimeUI = null;
		m_LoadingUI = null;
		m_PropUI = null;
		m_LapUI = null;
		m_CountDownUI = null;
		m_SmallMapUI = null;
		m_FadeInAndOut = null;
		m_ReverseUI = null;
		m_ControlPanel = null;
		m_PauseUI = null;
		m_RealtimeRankUI = null;
	}

	public void BANANA()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.BANANA);
	}

	public void BANANA_THREE()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.BANANA_THREE);
	}

	public void BOMB()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.BOMB);
	}

	public void CHEAT_BOX()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.CHEAT_BOX);
	}

	public void CUTTLE_FISH()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.CUTTLE_FISH);
	}

	public void LIGHTNING()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.LIGHTNING);
	}

	public void LUCKY_STAR()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.LUCKY_STAR);
	}

	public void MISSILE()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.MISSILE);
	}

	public void MUSHROOM_BIG()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.MUSHROOM_BIG);
	}

	public void MUSHROOM_GOLDERN()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.MUSHROOM_GOLDERN);
	}

	public void MUSHROOM_ONE()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.MUSHROOM_ONE);
	}

	public void MUSHROOM_THREE()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.MUSHROOM_THREE);
	}

	public void QUAKE()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.QUAKE);
	}

	public void THUNDER_CLOUD()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.THUNDER_CLOUD);
	}

	public void TORTOISE_BLUE()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.TORTOISE_BLUE);
	}

	public void TORTOISE_GREEN()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.TORTOISE_GREEN);
	}

	public void TORTOISE_GREEN_THREE()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.TORTOISE_GREEN_THREE);
	}

	public void TORTOISE_RED()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.TORTOISE_RED);
	}

	public void TORTOISE_RED_THREE()
	{
		PickPropManager.Instance.PickProp(TSingleton<CarManager>.Instance.GetHeroCar(), ePropType.TORTOISE_RED_THREE);
	}
}
