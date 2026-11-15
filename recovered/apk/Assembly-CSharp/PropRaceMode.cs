using Misc;
using RaceTrack;
using UnityEngine;

internal class PropRaceMode : RaceModeBase
{
	private float fMoveCameraTime = 20f;

	private bool m_bFade;

	private PlayerData data = new PlayerData();

	private bool bLoadingisFinish;

	public override void Init(SceneRace scene)
	{
		base.Init(scene);
		m_SceneRaceComp = scene;
		InitPropWayPoints(scene.iSceneID);
		InitScenePropBox();
		CreateNPCCars();
		PlayerAIProfileBuilder.Build(TSingleton<CarManager>.Instance.Cars, LocalUserInfo.Instance.TrackLevel);
		InitRaceAI();
		StartShowTimeCamera();
		SceneUIManager.Instance.ShowIntroUI();
		m_HeroCar.LockMovement(bLock: true);
		m_HeroCar.EnableKinematic();
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			GameObject gameObject = TSingleton<CarManager>.Instance.GetCarByIndex(i).gameObject;
			if (null != gameObject && null != gameObject.GetComponent<NPCCar>())
			{
				NPCCar component = gameObject.GetComponent<NPCCar>();
				component.LockCarActor();
				component.EnableKinematic();
			}
		}
		RaceWorld.Instance.Init();
		FunMaker.Instance.StartState();
		m_Init = true;
	}

	private void InitScenePropBox()
	{
		string name = "Scene/Item";
		GameObject gameObject = GameObject.Find(name);
		if (null == gameObject)
		{
			gameObject = new GameObject(name);
		}
		if (RaceModeFactory.ScenePointMap.ContainsKey(m_SceneRaceComp.iSceneID))
		{
			TSingleton<PropManager>.Instance.SetScenePropBoxes(RaceModeFactory.ScenePointMap[m_SceneRaceComp.iSceneID].SceneBox, gameObject);
		}
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
		if (m_Init)
		{
			WayPoint.Instance.Destroy();
			for (int i = 1; i <= 5; i++)
			{
				Car car = TSingleton<CarManager>.Instance.GetCar(i);
				TSingleton<CarManager>.Instance.DestroyCar(car.gameObject);
			}
			FunMaker.Instance.UninitState();
			base.Uninit();
		}
	}

	public override void ReStartGame()
	{
		base.ReStartGame();
		for (int i = 1; i <= 5; i++)
		{
			Car car = TSingleton<CarManager>.Instance.GetCar(i);
			car.SetPosition(StartPosition(i));
			car.SetRotation(base.StartRotation);
			car.SetVecGlobal(Vector3.zero);
			car.MoveToGround();
		}
		StartShowTimeCamera();
		m_HeroCar.LockMovement(bLock: true);
		for (int j = 0; j < TSingleton<CarManager>.Instance.GetCarCount(); j++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(j);
			GameObject gameObject = TSingleton<CarManager>.Instance.GetCarByIndex(j).gameObject;
			if (carByIndex != null && carByIndex != m_HeroCar && RaceSceneData.GetPlayerData((ulong)carByIndex.ID, ref data))
			{
				data.SpendTime = 0f;
				data.ShowLaps = 0;
				data.CurLaps = 0;
				data.PreBlockNum = 0;
				data.TotalBlockNum = 0;
				RaceSceneData.SetPlayerData((ulong)carByIndex.ID, data);
			}
			if (null != gameObject && null != gameObject.GetComponent<NPCCar>())
			{
				NPCCar component = gameObject.GetComponent<NPCCar>();
				component.LockCarActor();
			}
		}
	}

	private void CreateRemotePlayerCars()
	{
		int id = 7;
		GameObject gameObject = TSingleton<CarManager>.Instance.CreateCar(id, CarLogicType.RT_PLAYER, "Player", StartPosition(5), base.StartRotation, CarModeType.CAR_RADI);
		PlayerCar component = gameObject.GetComponent<PlayerCar>();
		component.Init(id, m_CarEffect);
		gameObject.rigidbody.mass = 1000f;
		component.MoveToGround();
		Role role = RoleFactory.Instance.CreateRole("RemotePlayer", new Vector3(0f, 0f, 0f), default(Quaternion), RoleType.EMmt);
		RoleCarCompositerFactory.Instance.CreateCompositer(role, gameObject, isHero: false);
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
			break;
		case ERaceState.START_TIME_COUNT_STATE:
			SoundManager.Instance.StopSound(10001);
			SoundManager.Instance.SetVolumn(10001, 0.5f);
			UpdateGameStartCounter();
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
			break;
		}
		base.UpdateMode();
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

	private void UpdateUIInRace()
	{
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
		FunMaker.Instance.Active = true;
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
			m_currentRaceState = ERaceState.START_TIME_COUNT_STATE;
			SceneUIManager.Instance.InitSceneUI();
			ResetTimer();
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
			StopAllNPCCar();
			SceneUIManager.Instance.ShowRankUI();
		}
	}

	private void StopAllNPCCar()
	{
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (null != carByIndex && RaceSceneData.GetPlayerData((ulong)carByIndex.ID, ref data) && !data.IsFinish)
			{
				data.SpendTime += (float)(data.Ranking * 4 + data.Ranking * 3 % 4) + (float)data.Ranking * 0.17f;
				data.IsFinish = true;
				RaceSceneData.SetPlayerData((ulong)carByIndex.ID, data);
			}
		}
	}
}
