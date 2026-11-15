using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FrameWork;
using GameLogic;
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

	private iOSIAPObserver iosIAP;

	public bool Pause;

	private string m_currentPlatform;

	public bool isIntoBackground;

	public bool showAlert;

	public AlertDismissedDelegate onCallBack;

	private DelayDelegateTimer timer;

	public InputManager Input
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public SceneManager SceneMgr
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool GameCenterTried
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public static AppMain Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsSpeedMode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsOnlineMode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsServer
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsClient
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IRaceMode RaceMode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private AppMain()
	{
	}

	private void Start()
	{
	}

	[DebuggerHidden]
	private IEnumerator StartLoadResource()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Init()
	{
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
	}

	private void OnGUI()
	{
	}

	public void Uninit()
	{
	}

	public void OnApplicationQuit()
	{
	}

	public string GetCurrentSystemLanguage()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public string GetCurrentPlatform()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsGameCenterAvailable()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void OnApplicationPause(bool pause)
	{
	}

	public void LoadOnlineGameLevel(AlertDismissedDelegate callBack)
	{
	}

	public void QuitOnlineGame(AlertDismissedDelegate callBack)
	{
	}

	private void DelayLoadingOnlineGameLobbyLevel()
	{
	}

	public void ShowIntoBackgroundAlert()
	{
	}

	public void ShowOtherDisconnectedAlert()
	{
	}

	public void ShowHostLeaveAlert()
	{
	}

	private void OnLevelWasLoaded(int level)
	{
	}
}
