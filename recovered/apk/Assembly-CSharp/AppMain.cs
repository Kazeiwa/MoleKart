using System.Collections;
using FrameWork;
using GameLogic;
using GameNetwork;
using TaomeeNetwork;
using UnityEngine;

public class AppMain : MonoBehaviour
{
	public delegate void AlertDismissedDelegate();

	private static AppMain instance;

	private bool m_bInit;

	private CFrameWork framework;

	private RaceLogicModule m_RaceSceneModule;

	private ResourceModule m_ResourceModule;

	private LoginModule m_LoginModule;

	public bool Pause;

	public string channel;

	public string currVersion;

	private string m_currentPlatform = string.Empty;

	public bool isIntoBackground;

	public bool showAlert;

	public AlertDismissedDelegate onCallBack;

	private DelayDelegateTimer timer;

	public InputManager Input => InputManager.Instance;

	public SceneManager SceneMgr => SceneManager.Instance;

	public bool GameCenterTried { get; set; }

	public static AppMain Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("AppMain").AddComponent<AppMain>();
				Object.DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}

	public bool IsSpeedMode
	{
		get
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && (raceLogicModule.GetCurrentRaceType() == RacingType.OnlineTimingMode || raceLogicModule.GetCurrentRaceType() == RacingType.SpeedSingleMode))
			{
				return true;
			}
			return false;
		}
	}

	public bool IsOnlineMode
	{
		get
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode())
			{
				return true;
			}
			return false;
		}
	}

	public bool IsServer
	{
		get
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode() && raceLogicModule.IsYouServer())
			{
				return true;
			}
			return false;
		}
	}

	public bool IsClient
	{
		get
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode() && !raceLogicModule.IsYouServer())
			{
				return true;
			}
			return false;
		}
	}

	public IRaceMode RaceMode
	{
		get
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule)
			{
				return raceLogicModule.GetCurrentRaceMode();
			}
			return null;
		}
	}

	private AppMain()
	{
		framework = (CFrameWork)CFrameWork.GetFrameWork();
		m_RaceSceneModule = new RaceLogicModule();
		m_ResourceModule = new ResourceModule();
		m_LoginModule = new LoginModule();
	}

	private void Start()
	{
		MonoBehaviour.print("Starting " + Time.time);
		GameObject target = GameObject.Find("GUI Text");
		Object.DontDestroyOnLoad(target);
		StartCoroutine("StartLoadResource");
		MonoBehaviour.print("Before WaitAndPrint Finishes " + Time.time);
	}

	private IEnumerator StartLoadResource()
	{
		m_ResourceModule.Init(framework);
		yield return 0;
	}

	public void Init()
	{
		if (!m_bInit)
		{
			SystemConfig.Instance.Load();
			TSingleton<ShopDataManager>.Instance.Init();
			Engine.Init();
			UTResourceManager.Start();
			SceneManager.Instance.Init();
			m_RaceSceneModule.Init(framework);
			m_LoginModule.Init(framework);
			NetworkManager.Instance.Init();
			FlurryBinding.StartSession();
			UtilBinding.SetScreenWake(bOn: true);
			m_bInit = true;
		}
	}

	private void Update()
	{
		if (!Pause)
		{
			if (InputManager.Instance.CheckI())
			{
				SystemConfig.Instance.AutoUseProps = !SystemConfig.Instance.AutoUseProps;
			}
			UTResourceManager.FixedUpdate();
			framework.Update(Time.time);
			Engine.Update();
			if (LocalNetFactory.Instance.LocalNet != null && LocalNetFactory.Instance.LocalNet.SessionManager != null)
			{
				LocalNetFactory.Instance.LocalNet.SessionManager.EndSend();
			}
		}
	}

	private void FixedUpdate()
	{
		if (!Pause)
		{
			framework.FixedUpdate();
		}
	}

	private void OnGUI()
	{
		framework.OnGUI();
	}

	public void Uninit()
	{
		FlurryBinding.StopSession();
		SystemConfig.Instance.Save();
		m_RaceSceneModule.Uninit();
		m_LoginModule.Uninit();
		m_ResourceModule.Uninit();
		Engine.Shutdown();
		m_bInit = false;
	}

	public void OnApplicationQuit()
	{
		Uninit();
		Debug.Log("OnApplicationQuit()!");
		instance = null;
	}

	public string GetCurrentSystemLanguage()
	{
		return "zh-Hans";
	}

	public string GetCurrentPlatform()
	{
		if (m_currentPlatform != string.Empty)
		{
			return m_currentPlatform;
		}
		return m_currentPlatform;
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			isIntoBackground = true;
			Debug.Log("Into Pause");
			if (IsOnlineMode)
			{
				Debug.Log("into background");
				SoundManager.Instance.StopAllSound();
				if (!SoundManager.Instance.IsPlaying(60002))
				{
					SoundManager.Instance.PlaySoundEx(60002);
				}
				RaceModeFactory.CurrentRaceMode.Uninit();
				TSingleton<PropManager>.Instance.Clear();
				SceneUIManager.Instance.HideRankUI();
				SoundManager.Instance.ClearSteroAudios();
			}
			else
			{
				Debug.Log("Not online Mode");
			}
			if (NetworkManager.Instance.IsClient)
			{
				NetworkManager.Instance.StopClient();
			}
			else if (NetworkManager.Instance.IsServer)
			{
				NetworkManager.Instance.StopServer();
			}
		}
		else
		{
			Debug.Log("Out Pause");
			NetworkManager.Instance.IsNetworkAvaliable = true;
			DriveManager.Instance.Reset();
			if (RaceModeFactory.CurrentRaceMode is RaceModeBase raceModeBase && raceModeBase.GetRaceOver())
			{
				DriveManager.Instance.SetCanDrive(drive: false);
			}
			else
			{
				DriveManager.Instance.SetCanDrive(drive: true);
			}
			bool flag = false;
			switch (SceneManager.Instance.CurGameSceneId)
			{
			case 1101:
				flag = true;
				break;
			case 1102:
				flag = true;
				break;
			case 1103:
				flag = true;
				break;
			case 1104:
				flag = true;
				break;
			case 1105:
				flag = true;
				break;
			case 1106:
				flag = true;
				break;
			case 1107:
				flag = true;
				break;
			case 1108:
				flag = true;
				break;
			}
			if (SceneManager.Instance.CurGameSceneId == 1005)
			{
				GameDataManager.Instance.RemoveAllPlayers();
				NetworkManager.Instance.StartClient();
			}
			else if (SceneManager.Instance.CurGameSceneId == 1006 || SceneManager.Instance.CurGameSceneId == 1007)
			{
				onCallBack = ShowIntoBackgroundAlert;
				showAlert = true;
				SceneManager.Instance.LoadLevel(1005);
			}
			else if (IsOnlineMode && flag)
			{
				LoadOnlineGameLevel(ShowIntoBackgroundAlert);
			}
			if (flag && !IsOnlineMode)
			{
				if (SceneUIManager.Instance.m_SceneUIController != null)
				{
					SceneUIManager.Instance.m_SceneUIController.ShowPause();
				}
			}
			else
			{
				Debug.Log("Can't Pause Now");
			}
			isIntoBackground = false;
		}
		UtilBinding.SetScreenWake(!pause);
	}

	public void LoadOnlineGameLevel(AlertDismissedDelegate callBack)
	{
		showAlert = true;
		onCallBack = callBack;
		NetworkManager.Instance.notDealPeerDisconnected = true;
		if (null != SceneUIManager.Instance.m_Loading)
		{
			SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
			SceneUIManager.Instance.m_Loading.GetComponent<LoadingController>().ShowLoadingIndicator();
		}
		SceneManager.Instance.LoadEmptyLevel();
		Resources.UnloadUnusedAssets();
		timer = DelayDelegateTimer.Create();
		timer.InitTimer(3f, DelayLoadingOnlineGameLobbyLevel);
		timer.StartTimer();
		Object.DontDestroyOnLoad(timer.gameObject);
	}

	public void QuitOnlineGame(AlertDismissedDelegate callBack)
	{
		if (NetworkManager.Instance.IsClient)
		{
			NetworkManager.Instance.StopClient();
		}
		else if (NetworkManager.Instance.IsServer)
		{
			NetworkManager.Instance.StopServer();
		}
		showAlert = true;
		onCallBack = callBack;
		bool flag = false;
		switch (SceneManager.Instance.CurGameSceneId)
		{
		case 1101:
			flag = true;
			break;
		case 1102:
			flag = true;
			break;
		case 1103:
			flag = true;
			break;
		case 1104:
			flag = true;
			break;
		case 1105:
			flag = true;
			break;
		case 1106:
			flag = true;
			break;
		case 1107:
			flag = true;
			break;
		case 1108:
			flag = true;
			break;
		}
		if (flag && Instance.IsOnlineMode)
		{
			if (null != SceneUIManager.Instance.m_Loading)
			{
				SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
				SceneUIManager.Instance.m_Loading.GetComponent<LoadingController>().ShowLoadingIndicator();
			}
			SceneManager.Instance.LoadEmptyLevel();
			Debug.Log("One player leave the race, back to lobby!!!");
			SoundManager.Instance.StopAllSound();
			RaceModeFactory.CurrentRaceMode.Uninit();
			TSingleton<PropManager>.Instance.Clear();
			SceneUIManager.Instance.HideRankUI();
			SoundManager.Instance.ClearPauseMonoSound();
			SoundManager.Instance.ClearSteroAudios();
			Resources.UnloadUnusedAssets();
			timer = DelayDelegateTimer.Create();
			timer.InitTimer(3f, DelayLoadingOnlineGameLobbyLevel);
			timer.StartTimer();
			Object.DontDestroyOnLoad(timer.gameObject);
		}
	}

	private void DelayLoadingOnlineGameLobbyLevel()
	{
		Debug.Log("Reload DelayLoadingOnlineGameLobbyLevel ");
		LocalNetFactory.Instance.LocalNet.StopClient();
		SceneManager.Instance.LoadLevel(1005);
		Object.DestroyObject(timer.gameObject);
	}

	public void ShowIntoBackgroundAlert()
	{
		Debug.Log("ShowIntoBackgroundAlert");
		string currentSystemLanguage = Instance.GetCurrentSystemLanguage();
		if (currentSystemLanguage == "zh-Hans")
		{
			string text = "游戏进入后台模式时将断开网络连接";
			string text2 = "好";
		}
		else if (currentSystemLanguage == "zh-Hant")
		{
			string text = "遊戲進入後臺模式時將斷開網絡連接";
			string text2 = "好";
		}
		else
		{
			string text = "You will be disconnected if the App is in background mode.";
			string text2 = "OK";
		}
	}

	public void ShowOtherDisconnectedAlert()
	{
		Debug.Log("ShowOtherDisconnectedAlert");
		string currentSystemLanguage = Instance.GetCurrentSystemLanguage();
		if (currentSystemLanguage == "zh-Hans")
		{
			string text = "网络连接丢失。";
			string text2 = "好";
		}
		else if (currentSystemLanguage == "zh-Hant")
		{
			string text = "網絡連接丟失。";
			string text2 = "好";
		}
		else
		{
			string text = "Connection lost.";
			string text2 = "OK";
		}
	}

	public void ShowHostLeaveAlert()
	{
		Debug.Log("ShowHostLeaveAlert");
		if (GetCurrentSystemLanguage() == "zh-Hans")
		{
			string text = "网络连接已断开";
			string text2 = "好";
		}
		else if (GetCurrentSystemLanguage() == "zh-Hant")
		{
			string text = "網絡連接已斷開";
			string text2 = "好";
		}
		else
		{
			string text = "Network disconnected.";
			string text2 = "OK";
		}
	}

	private void OnLevelWasLoaded(int level)
	{
		Debug.Log("On Level Was Loaded");
		Debug.Log("ShowAlert:" + showAlert);
		UtilBinding.ShowWaitingView(bShow: false);
		if (showAlert && SceneManager.Instance.CurGameSceneId == 1005)
		{
			Debug.Log("Into Show Alert");
			if (onCallBack != null)
			{
				Debug.Log("Call back on lobby level was loaded");
				onCallBack();
			}
			showAlert = false;
		}
		if (Application.loadedLevelName.ToLower() != "emptylevel")
		{
			onCallBack = null;
		}
	}

	private void OnApplicationBack(string arg)
	{
		switch (SceneManager.Instance.CurGameSceneId)
		{
		case 1001:
			Application.Quit();
			break;
		case 1002:
		{
			GameObject gameObject3 = GameObject.Find("Panel/PanelEvent");
			if (gameObject3 != null)
			{
				gameObject3.SendMessage("OnBack");
			}
			break;
		}
		case 1003:
		{
			GameObject gameObject4 = GameObject.Find("Controller");
			if (gameObject4 != null)
			{
				gameObject4.SendMessage("Return");
			}
			break;
		}
		case 1004:
		{
			GameObject gameObject2 = GameObject.Find("SettingsController");
			if (gameObject2 != null)
			{
				gameObject2.SendMessage("BackToMainMenu");
			}
			break;
		}
		case 1008:
		{
			GameObject gameObject = GameObject.Find("LeadboardController");
			if (gameObject != null)
			{
				gameObject.SendMessage("BackToMainMenu");
			}
			break;
		}
		case 1005:
		case 1006:
		case 1007:
			break;
		}
	}
}
