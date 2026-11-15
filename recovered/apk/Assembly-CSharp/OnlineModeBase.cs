using System.Collections;
using FrameWork;
using GameNetwork;
using RaceProtocol;
using RaceTrack;
using TaomeeNetwork;
using UnityEngine;

internal class OnlineModeBase : RaceModeBase
{
	private float fMoveCameraTime = 20f;

	private bool m_bFade;

	private bool m_bReadyToReceivePkg;

	private bool bLoadingisFinish;

	private Handler<MsgPlayerData2> playerInfoHandler;

	private Handler<MsgStartRace> startRaceHandler;

	private IRaceLogicModule pkModule;

	private float fLastUpdateTime;

	private bool readyMessageSent;

	public override void Init(SceneRace scene)
	{
		base.Init(scene);
		m_SceneRaceComp = scene;
		InitPropWayPoints(scene.iSceneID);
		CreateRemoteCars();
		InitRaceAI();
		InitNetwork();
		StartShowTimeCamera();
		SceneUIManager.Instance.ShowIntroUI();
		m_HeroCar.LockMovement(bLock: true);
		m_HeroCar.EnableKinematic();
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			GameObject gameObject = TSingleton<CarManager>.Instance.GetCarByIndex(i).gameObject;
			if (null != gameObject && null != gameObject.GetComponent<PlayerCar>())
			{
				PlayerCar component = gameObject.GetComponent<PlayerCar>();
				component.LockCarActor();
				component.EnableKinematic();
			}
		}
		m_bReadyToReceivePkg = false;
		m_Init = true;
	}

	private void InitPropWayPoints(int sceneID)
	{
		if (!RaceModeFactory.ScenePointMap.ContainsKey(m_SceneRaceComp.iSceneID))
		{
			Debug.LogError("Do not set the right scene id: " + m_SceneRaceComp.iSceneID);
		}
		else
		{
			WayPoint.Instance.Init(RaceModeFactory.ScenePointMap[m_SceneRaceComp.iSceneID].PropWay);
		}
	}

	private void InitRaceAI()
	{
		if (!RaceModeFactory.ScenePointMap.ContainsKey(m_SceneRaceComp.iSceneID))
		{
			Debug.LogError("Do not set the right scene id");
			return;
		}
		RaceWorld instance = RaceWorld.Instance;
		RaceWorldTextAssetReader raceWorldTextAssetReader = new RaceWorldTextAssetReader(instance, RaceModeFactory.ScenePointMap[m_SceneRaceComp.iSceneID].RaceTrackPath);
		if (!raceWorldTextAssetReader.Read())
		{
			Debug.LogError("AI: RaceWorld read fail!");
		}
	}

	public override void Uninit()
	{
		if (!m_Init)
		{
			return;
		}
		UninitNetwork();
		WayPoint.Instance.Destroy();
		ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
		foreach (PlayerData item in playerDataList)
		{
			if (!item.IsMaster)
			{
				Car car = TSingleton<CarManager>.Instance.GetCar((int)item.PlayerId);
				TSingleton<CarManager>.Instance.DestroyCar(car.gameObject);
			}
		}
		base.Uninit();
	}

	protected override void CreatePlayerCar()
	{
		ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
		foreach (PlayerData item in playerDataList)
		{
			if (item.IsMaster)
			{
				UserInfo userInfo = LocalUserInfo.Instance.UserInfo;
				GameObject gameObject = TSingleton<CarManager>.Instance.CreateCar((int)item.PlayerId, CarLogicType.RT_HERO, "LocalPlayer", StartPosition(item.StartPositionId), base.StartRotation, (CarModeType)userInfo.CarId);
				if (null != gameObject)
				{
					HeroCar component = gameObject.GetComponent<HeroCar>();
					component.Init((int)item.PlayerId, m_CarEffect);
					m_HeroCar = component;
					gameObject.rigidbody.mass = 1000f;
					component.MoveToGround();
					GameObject gameObject2 = TSingleton<CarManager>.Instance.GetCar((int)item.PlayerId).gameObject;
					Role role = RoleFactory.Instance.CreateRole("LocalPlayer", new Vector3(0f, 0f, 0f), default(Quaternion), (RoleType)userInfo.roleType);
					RoleCarCompositerFactory.Instance.CreateCompositer(role, gameObject2, isHero: true);
				}
				else
				{
					Debug.LogError("PlayerCar is null, please check");
				}
				break;
			}
		}
	}

	private void CreateRemoteCars()
	{
		ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
		foreach (PlayerData item in playerDataList)
		{
			if (!item.IsMaster)
			{
				GameObject gameObject = TSingleton<CarManager>.Instance.CreateCar((int)item.PlayerId, CarLogicType.RT_PHYSPLAYER, item.name, StartPosition(item.StartPositionId), base.StartRotation, item.carType);
				Debug.Log("CreateRemoteCars!" + item.PlayerId);
				if (null != gameObject)
				{
					PhysxPlayerCar component = gameObject.GetComponent<PhysxPlayerCar>();
					component.Init((int)item.PlayerId, m_CarEffect);
					gameObject.rigidbody.mass = 1000f;
					component.MoveToGround();
					Role role = RoleFactory.Instance.CreateNpcRole("REMOTEPLAYER", new Vector3(0f, 0f, 0f), default(Quaternion), item.role);
					RoleCarCompositerFactory.Instance.CreateCompositer(role, gameObject, isHero: false);
				}
			}
		}
	}

	public override void UpdateMode()
	{
		if (!m_Init)
		{
			return;
		}
		switch (m_currentRaceState)
		{
		case ERaceState.MOVE_CAMMER_STATE:
			UpdateShowTimeCamera();
			SendLocalPlayerInformationToServer();
			break;
		case ERaceState.START_TIME_COUNT_STATE:
			SoundManager.Instance.StopSound(10001);
			SoundManager.Instance.SetVolumn(10001, 0.5f);
			UpdateGameStartCounter();
			SendLocalPlayerInformationToServer();
			break;
		case ERaceState.END_TIME_COUNT_STATE:
		case ERaceState.START_RACE_STATE:
			if (!m_BgSoundPlayed)
			{
				SoundManager.Instance.PlayBackgroundMusic();
				m_BgSoundPlayed = true;
				RaceOver(over: false);
				SceneUIManager.Instance.SetRaceStarted(started: true);
			}
			UpdateTime();
			UpdateRaceData();
			UpdateUIInRace();
			DriveManager.Instance.Drive();
			SendLocalPlayerInformationToServer();
			break;
		case ERaceState.GAME_OVER_RESULT_STATE:
			SendLocalPlayerInformationToServer();
			break;
		}
		Utility.CPU_Profile_Begin_X();
		TSingleton<CarManager>.Instance.Update();
		if (m_bReadyToReceivePkg)
		{
			RaceWorld.Instance.UpdateMonsters();
		}
		else
		{
			ATrace.Say("Not Ready to UpdateMonster!----------------");
		}
		Utility.CPU_Profile_End_X("CarManager.update = ");
	}

	private void UpdateRaceData()
	{
		RaceSceneData.UpdateRaceData(this);
	}

	public void UpdateTime()
	{
		m_iSpendTime += Time.deltaTime;
		if (m_iSpendTime >= TotalTime())
		{
			m_iSpendTime = TotalTime();
			m_currentRaceState = ERaceState.GAME_OVER_RESULT_STATE;
			RaceOver(over: true);
		}
	}

	public void UpdateResultTime()
	{
		m_fTimer += Time.deltaTime;
		if (m_fTimer >= 5f)
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			IRaceLogicModule raceLogicModule = frameWork.GetModule(enmModuleID.MI_RACELOGIC) as IRaceLogicModule;
			if (raceLogicModule.IsYouServer())
			{
				SceneManager.Instance.LoadLevel(1006);
			}
			else
			{
				SceneManager.Instance.LoadLevel(1007);
			}
		}
	}

	private void UpdateUIInRace()
	{
		PlayerData data = new PlayerData();
		if (RaceSceneData.GetPlayerData((ulong)TSingleton<CarManager>.Instance.GetHeroCar().ID, ref data))
		{
			SceneUIManager.Instance.ShowTime(data.SpendTime);
			SceneUIManager.Instance.ShowLap(data.ShowLaps);
			SceneUIManager.Instance.ShowSmallMap();
			SceneUIManager.Instance.ShowSlider(show: true);
			SceneUIManager.Instance.ShowRealtimeRank();
		}
	}

	private void UpdateGameStartCounter()
	{
		if (m_fTimer < 5.7f && m_fTimer != -1f)
		{
			SoundManager.Instance.PlaySoundEx(60004);
		}
		else
		{
			SoundManager.Instance.StopSound(60004);
		}
		if (m_fTimer == -1f)
		{
			m_currentRaceState = ERaceState.START_RACE_STATE;
			InputManager.Instance.bTouchMode_Break = false;
		}
		m_fTimer += Time.deltaTime;
		if (m_fTimer > 5.5f)
		{
			m_fTimer = -1f;
		}
		if (!(m_fTimer > 4.5f))
		{
			return;
		}
		m_HeroCar.LockMovement(bLock: false);
		m_HeroCar.DisableKinematic();
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			GameObject gameObject = TSingleton<CarManager>.Instance.GetCarByIndex(i).gameObject;
			if (null != gameObject && null != gameObject.GetComponent<NPCCar>())
			{
				NPCCar component = gameObject.GetComponent<NPCCar>();
				component.UnLockCarActor();
				component.DisableKinematic();
			}
		}
		DriveManager.Instance.SetCanDrive(drive: true);
		DriveManager.Instance.Drive();
		if (InputManager.Instance.CheckBoost())
		{
			AccelerateCarWhenGo();
		}
		UpdateTime();
		UpdateRaceData();
		UpdateUIInRace();
	}

	private void StartShowTimeCamera()
	{
		ResetTimer();
		m_currentRaceState = ERaceState.MOVE_CAMMER_STATE;
		if (CameraManager.instance != null)
		{
			CameraManager.instance.Init();
			fMoveCameraTime = CameraManager.instance.GetAllTime();
		}
		m_bFade = false;
	}

	private void UpdateShowTimeCamera()
	{
		if (!bLoadingisFinish && null != SceneUIManager.Instance.m_Loading && !SceneUIManager.Instance.m_Loading.active)
		{
			bLoadingisFinish = true;
			SoundManager.Instance.PlaySound(10001);
		}
		bool flag = false;
		flag = InputManager.Instance.CheckSwap();
		m_fTimer += Time.deltaTime;
		if (m_fTimer > fMoveCameraTime || flag || m_bFade)
		{
			SceneUIManager.Instance.HideIntroUI();
			SoundManager.Instance.FadeSoundToStop(10001, 5f);
			if (!m_bFade)
			{
				m_bFade = true;
			}
			CameraManager.instance.ChangeNormal();
			m_SceneRaceComp.m_bFixUpdate = true;
			SendReadyMessageToServer();
		}
		else
		{
			CameraManager.instance.Update();
		}
	}

	public override void RaceOver(bool over)
	{
		base.RaceOver(over);
		if (over)
		{
			StopAllPlayerCar();
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			IRaceLogicModule raceLogicModule = frameWork.GetModule(enmModuleID.MI_RACELOGIC) as IRaceLogicModule;
			IRaceMode currentRaceMode = raceLogicModule.GetCurrentRaceMode();
			currentRaceMode.FinishRace();
		}
	}

	private void StopAllPlayerCar()
	{
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (null != carByIndex)
			{
				PlayerData data = new PlayerData();
				if (RaceSceneData.GetPlayerData((ulong)carByIndex.ID, ref data) && !data.IsFinish)
				{
					data.SpendTime += (float)(data.Ranking * 4 + data.Ranking * 3 % 4) + (float)data.Ranking * 0.17f;
					data.IsFinish = true;
					RaceSceneData.SetPlayerData((ulong)carByIndex.ID, data);
				}
			}
		}
	}

	public override void FinishRace()
	{
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		IRaceLogicModule raceLogicModule = frameWork.GetModule(enmModuleID.MI_RACELOGIC) as IRaceLogicModule;
		if (raceLogicModule.IsYouServer())
		{
			raceLogicModule.ServerIsFinishRace();
			return;
		}
		MsgFinishRace.Builder builder = new MsgFinishRace.Builder();
		builder.PlayerId = m_HeroCar.ID;
		MsgFinishRace message = builder.Build();
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
	}

	private void InitNetwork()
	{
		playerInfoHandler = OnPlayerInfoChanaged;
		if (SystemConfig.Instance.EnableCarMovePkg)
		{
			NetHandlerRegistry.Instance.RegistHandler(playerInfoHandler);
		}
		startRaceHandler = OnStartRace;
		NetHandlerRegistry.Instance.RegistHandler(startRaceHandler);
		if (NetworkManager.Instance.IsClient)
		{
			RaceWorld.Instance.SetupClient();
		}
		NetHandlerRegistry.Instance.RegistHandler<MsgGameResult>(OnGameResult);
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		pkModule = frameWork.GetModule(enmModuleID.MI_RACELOGIC) as IRaceLogicModule;
	}

	private void UninitNetwork()
	{
		NetHandlerRegistry.Instance.UnregistHandler<MsgGameResult>(OnGameResult);
		NetHandlerRegistry.Instance.UnregistHandler(playerInfoHandler);
		NetHandlerRegistry.Instance.UnregistHandler(startRaceHandler);
		if (NetworkManager.Instance.IsClient)
		{
			RaceWorld.Instance.TeardownClient();
		}
	}

	private void SendLocalPlayerInformationToServer()
	{
		if (SystemConfig.Instance.EnableCarMovePkg && !(Time.time - fLastUpdateTime < 0.1429f))
		{
			if (!m_bReadyToReceivePkg)
			{
				ATrace.Say("Can't Send LocalPlayer Information To Server!");
				return;
			}
			fLastUpdateTime = Time.time;
			LocalNetFactory.Instance.LocalNet.SessionManager.SendCarsMoves();
		}
	}

	private void OnPlayerInfoChanaged(string peerId, MsgPlayerData2 message)
	{
		Car car = TSingleton<CarManager>.Instance.GetCar(message.PlayerId);
		if (car != null && car.ID != m_HeroCar.ID)
		{
			Vector3 position = new Vector3(message.PosX, message.PosY, message.PosZ);
			Vector3 vecGlobal = new Vector3(message.VecX, message.VecY, message.VecZ);
			Vector3 accelate = new Vector3(message.Ax, message.Ay, message.Az);
			car.SetPosition(position);
			car.SetRotation(Quaternion.Euler(message.EulerX, message.EulerY, message.EulerZ));
			car.SetVecGlobal(vecGlobal);
			car.SetAccelate(accelate);
			car.SetCarState((uint)message.CarState);
			if (pkModule.IsYouServer() && !m_bReadyToReceivePkg)
			{
			}
		}
	}

	private void SendReadyMessageToServer()
	{
		if (!readyMessageSent)
		{
			Debug.Log("Into send ready message to server");
			if (pkModule.IsYouServer())
			{
				Debug.Log("server is ready");
				pkModule.ServerIsReady();
			}
			else
			{
				Debug.Log("client is ready");
				MsgFinishLoadingMap.Builder builder = new MsgFinishLoadingMap.Builder();
				builder.SetPlayerId(m_HeroCar.ID);
				MsgFinishLoadingMap message = builder.Build();
				LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
			}
			readyMessageSent = true;
		}
	}

	public void StopSynchronizeMovementPkg()
	{
		m_bReadyToReceivePkg = false;
	}

	private void OnStartRace(string peerId, MsgStartRace message)
	{
		if (NetworkManager.Instance.IsClient)
		{
			m_bReadyToReceivePkg = true;
			RaceWorld.Instance.Init();
		}
		BeginCountTime();
		Debug.Log("Client ---->OnStartRace()!");
	}

	public override void BeginCountTime()
	{
		if (NetworkManager.Instance.IsServer)
		{
			Debug.Log("Server---->BeginCountTime!");
			m_bReadyToReceivePkg = true;
			RaceWorld.Instance.Init();
		}
		m_currentRaceState = ERaceState.START_TIME_COUNT_STATE;
		SceneUIManager.Instance.InitOnlineGameSceneUI();
		NetworkManager.Instance.SetSendHeartbeat(isSend: true);
		ResetTimer();
	}

	private void OnGameResult(string peerId, MsgGameResult msg)
	{
		ATrace.Say("OnGameResult----> Receive Server send finish racing message!");
		SceneUIManager.Instance.ShowRankUI(isNetwork: true, isHost: false);
		ResetTimer();
	}
}
