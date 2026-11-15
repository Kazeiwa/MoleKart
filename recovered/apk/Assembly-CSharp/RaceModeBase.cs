using System;
using System.Collections.Generic;
using RaceTrack;
using UnityEngine;

internal abstract class RaceModeBase : IRaceMode
{
	protected bool m_Init;

	public ERaceState m_currentRaceState = ERaceState.INVAID_STATE;

	protected float m_LastGameTime;

	protected HeroCar m_HeroCar;

	protected GameObject m_CarEffect;

	protected GameObject m_MainCamera;

	protected SceneRace m_SceneRaceComp;

	protected bool m_RaceOver;

	protected bool m_BgSoundPlayed;

	protected float m_iSpendTime;

	protected List<Vector3> m_lstStartPoints = new List<Vector3>();

	protected Quaternion m_kStartRotation;

	protected float m_fTimer;

	protected bool m_InitAccelerate;

	public Quaternion StartRotation => m_kStartRotation;

	public int StartPositionCount => m_lstStartPoints.Count;

	public float LastGameTime
	{
		get
		{
			return m_LastGameTime;
		}
		set
		{
			m_LastGameTime = value;
		}
	}

	public bool Inited => m_Init;

	public virtual void Init(SceneRace scene)
	{
		m_SceneRaceComp = scene;
		TSingleton<PropManager>.Instance.Init();
		TSingleton<CarManager>.Instance.Init();
		RoleFactory.Instance.Init();
		RandomPickProp.Instance.Init();
		InitCollidersData();
		InitTriggers();
		InitStartPosition();
		CreatePlayerCar();
		InstallEffects();
		m_MainCamera = GameObject.Find("MainCamera");
		SmoothCameraWithBumper smoothCameraWithBumper = m_MainCamera.AddComponent<SmoothCameraWithBumper>();
		smoothCameraWithBumper.ResetCamera();
		Engine.DisableAutoRotate();
		Resources.UnloadUnusedAssets();
	}

	protected virtual void CreatePlayerCar()
	{
		UserInfo userInfo = LocalUserInfo.Instance.UserInfo;
		GameObject gameObject = TSingleton<CarManager>.Instance.CreateCar(0, CarLogicType.RT_HERO, "LocalPlayer", StartPosition(0), StartRotation, (CarModeType)userInfo.CarId);
		if (null != gameObject)
		{
			HeroCar component = gameObject.GetComponent<HeroCar>();
			component.Init(0, m_CarEffect);
			m_HeroCar = component;
			gameObject.rigidbody.mass = 1000f;
			component.MoveToGround();
			GameObject gameObject2 = TSingleton<CarManager>.Instance.GetCar(0).gameObject;
			Role role = RoleFactory.Instance.CreateRole("LocalPlayer", new Vector3(0f, 0f, 0f), default(Quaternion), (RoleType)userInfo.UserId);
			RoleCarCompositerFactory.Instance.CreateCompositer(role, gameObject2, isHero: true);
		}
		else
		{
			Debug.LogError("PlayerCar is null, please check");
		}
	}

	protected void CreateNPCCars()
	{
		Dictionary<int, RoleType> dictionary = new Dictionary<int, RoleType>();
		UserInfo userInfo = LocalUserInfo.Instance.UserInfo;
		System.Random random = new System.Random((int)Time.time);
		int num = random.Next() % 8;
		for (int i = 1; i < 6; i++)
		{
			if (userInfo.CarId != 2002 && !dictionary.ContainsValue(RoleType.ERk) && (num > 1 || num < 0))
			{
				TSingleton<CarManager>.Instance.CreateCar(i, CarLogicType.RT_NPC, "NPC_RK", StartPosition(i), StartRotation, CarModeType.CAR_RK);
				dictionary.Add(i, RoleType.ERk);
			}
			else if (userInfo.CarId != 2101 && !dictionary.ContainsValue(RoleType.EBear) && (num > 2 || num < 1))
			{
				TSingleton<CarManager>.Instance.CreateCar(i, CarLogicType.RT_NPC, "NPC_FRED", StartPosition(i), StartRotation, CarModeType.CAR_BEAR);
				dictionary.Add(i, RoleType.EBear);
			}
			else if (userInfo.CarId != 2003 && !dictionary.ContainsValue(RoleType.EMolele) && (num > 3 || num < 2))
			{
				TSingleton<CarManager>.Instance.CreateCar(i, CarLogicType.RT_NPC, "NPC_LELE", StartPosition(i), StartRotation, CarModeType.CAR_MOLELE);
				dictionary.Add(i, RoleType.EMolele);
			}
			else if (userInfo.CarId != 2201 && !dictionary.ContainsValue(RoleType.EMmt) && (num > 4 || num < 3))
			{
				TSingleton<CarManager>.Instance.CreateCar(i, CarLogicType.RT_NPC, "NPC_MMT", StartPosition(i), StartRotation, CarModeType.CAR_RADI);
				dictionary.Add(i, RoleType.EMmt);
			}
			else if (userInfo.CarId != 2004 && !dictionary.ContainsValue(RoleType.EKuLa) && (num > 5 || num < 4))
			{
				TSingleton<CarManager>.Instance.CreateCar(i, CarLogicType.RT_NPC, "NPC_KULA", StartPosition(i), StartRotation, CarModeType.CAR_KULA);
				dictionary.Add(i, RoleType.EKuLa);
			}
			else if (userInfo.CarId != 2001 && !dictionary.ContainsValue(RoleType.EMomo) && (num > 6 || num < 5))
			{
				TSingleton<CarManager>.Instance.CreateCar(i, CarLogicType.RT_NPC, "NPC_MOMO", StartPosition(i), StartRotation, CarModeType.CAR_MOMO);
				dictionary.Add(i, RoleType.EMomo);
			}
			else if (userInfo.CarId != 2005 && !dictionary.ContainsValue(RoleType.EPuti) && (num > 7 || num < 6))
			{
				TSingleton<CarManager>.Instance.CreateCar(i, CarLogicType.RT_NPC, "NPC_PUTI", StartPosition(i), StartRotation, CarModeType.CAR_PUTI);
				dictionary.Add(i, RoleType.EPuti);
			}
			else if (userInfo.CarId != 2006 && !dictionary.ContainsValue(RoleType.EYali) && num % 7 != 0)
			{
				TSingleton<CarManager>.Instance.CreateCar(i, CarLogicType.RT_NPC, "NPC_YALI", StartPosition(i), StartRotation, CarModeType.CAR_YALI);
				dictionary.Add(i, RoleType.EYali);
			}
		}
		for (int j = 1; j < TSingleton<CarManager>.Instance.GetCarCount(); j++)
		{
			GameObject gameObject = TSingleton<CarManager>.Instance.GetCar(j).gameObject;
			if (null != gameObject)
			{
				NPCCar component = gameObject.GetComponent<NPCCar>();
				component.Init(j, m_CarEffect);
				gameObject.rigidbody.mass = 1000f;
				component.MoveToGround();
				Role role = RoleFactory.Instance.CreateNpcRole("NPC", new Vector3(0f, 0f, 0f), default(Quaternion), dictionary[j]);
				RoleCarCompositerFactory.Instance.CreateCompositer(role, gameObject, isHero: false);
			}
		}
	}

	private void InitCollidersData()
	{
		GameObject gameObject = GameObject.Find("Colliders/Road");
		if (gameObject != null)
		{
			MeshCollider[] componentsInChildren = gameObject.transform.GetComponentsInChildren<MeshCollider>();
			if (componentsInChildren != null)
			{
				MeshCollider[] array = componentsInChildren;
				foreach (MeshCollider meshCollider in array)
				{
					CStaticCollisionBody collider = new CStaticCollisionBody(meshCollider.gameObject, EnmCollisionLayer.CL_GROUND);
					CollisionLayer.Instance.AddCollider(collider);
				}
			}
		}
		GameObject gameObject2 = GameObject.Find("Scene/speed");
		if (gameObject2 != null)
		{
			MeshCollider[] componentsInChildren2 = gameObject2.transform.GetComponentsInChildren<MeshCollider>();
			if (componentsInChildren2 != null)
			{
				MeshCollider[] array2 = componentsInChildren2;
				foreach (MeshCollider meshCollider2 in array2)
				{
					CStaticCollisionBody collider = new CStaticCollisionBody(meshCollider2.gameObject, EnmCollisionLayer.CL_GROUND);
					CollisionLayer.Instance.AddCollider(collider);
				}
			}
		}
		GameObject gameObject3 = GameObject.Find("Colliders/Gardbar");
		if (gameObject3 != null)
		{
			Collider[] componentsInChildren3 = gameObject3.transform.GetComponentsInChildren<Collider>();
			if (componentsInChildren3 != null)
			{
				Collider[] array3 = componentsInChildren3;
				foreach (Collider collider2 in array3)
				{
					CStaticCollisionBody collider3 = new CStaticCollisionBody(collider2.gameObject, EnmCollisionLayer.CL_GARDBAR);
					CollisionLayer.Instance.AddCollider(collider3);
					collider2.tag = "COL_GARDBAR";
				}
			}
		}
		GameObject gameObject4 = GameObject.Find("Colliders/tree");
		if (gameObject4 != null)
		{
			Collider[] componentsInChildren4 = gameObject4.transform.GetComponentsInChildren<Collider>();
			if (componentsInChildren4 != null)
			{
				Collider[] array4 = componentsInChildren4;
				foreach (Collider collider4 in array4)
				{
					CStaticCollisionBody collider5 = new CStaticCollisionBody(collider4.gameObject, EnmCollisionLayer.CL_TREE, 2f);
					CollisionLayer.Instance.AddCollider(collider5);
					collider4.tag = "Tree";
				}
			}
		}
		GameObject gameObject5 = GameObject.Find("Colliders/Props");
		if (gameObject5 != null)
		{
			MeshCollider[] componentsInChildren5 = gameObject5.transform.GetComponentsInChildren<MeshCollider>();
			if (componentsInChildren5 != null)
			{
				MeshCollider[] array5 = componentsInChildren5;
				foreach (MeshCollider meshCollider3 in array5)
				{
					CStaticCollisionBody collider6 = new CStaticCollisionBody(meshCollider3.gameObject, EnmCollisionLayer.CL_OBSTACLE);
					CollisionLayer.Instance.AddCollider(collider6);
				}
			}
		}
		GameObject gameObject6 = GameObject.Find("water/waterblack");
		if (gameObject6 != null)
		{
			Collider component = gameObject6.transform.GetComponent<Collider>();
			if (null != component)
			{
				CDynamicCollisionBody collider7 = new CTerrainCollisionBody(component.gameObject, EnmCollisionLayer.CL_DEEPWATER);
				CollisionLayer.Instance.AddCollider(collider7);
			}
		}
		GameObject gameObject7 = GameObject.Find("water/waterblue");
		if (gameObject7 != null)
		{
			MeshCollider component2 = gameObject7.transform.GetComponent<MeshCollider>();
			if (null != component2)
			{
				CDynamicCollisionBody collider8 = new CTerrainCollisionBody(component2.gameObject, EnmCollisionLayer.CL_SHALLOWWATER);
				CollisionLayer.Instance.AddCollider(collider8);
			}
		}
	}

	private void InitTriggers()
	{
		GameObject gameObject = GameObject.Find("Colliders/DeathCollide");
		if (gameObject != null)
		{
			MeshCollider component = gameObject.transform.GetComponent<MeshCollider>();
			if (null != component)
			{
				component.gameObject.AddComponent<ClipDeathCollider>();
			}
		}
	}

	private void InstallEffects()
	{
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(Camera));
		UnityEngine.Object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Camera camera = (Camera)array2[i];
			if (camera.depth <= -1f)
			{
				EffectManager.sceneCamera = camera;
				break;
			}
		}
		m_CarEffect = new GameObject("FX_Root");
		GameObject gameObject = new GameObject("FX_MgrShadow");
		gameObject.transform.parent = m_CarEffect.transform;
		gameObject = new GameObject("FX_MgrSprite");
		gameObject.transform.parent = m_CarEffect.transform;
		Shader shader = Shader.Find(".Taomee/Sprite");
		if (null == shader)
		{
			shader = Resources.Load("Shader/Sprite.shader") as Shader;
		}
		Material material = new Material(shader);
		Texture2D texture = Resources.Load("Effects/texture/Speedy") as Texture2D;
		material.SetTexture("_MainTex", texture);
		MgrSprite mgrSprite = gameObject.AddComponent<MgrSprite>();
		mgrSprite.Init(material, 32, CameraClearFlags.Nothing);
		gameObject = new GameObject("FX_Speedy");
		gameObject.transform.parent = m_CarEffect.transform;
		FXSpeedy fXSpeedy = gameObject.AddComponent<FXSpeedy>();
		fXSpeedy.Init(mgrSprite);
	}

	public bool GetRaceOver()
	{
		return m_RaceOver;
	}

	private void InitStartPosition()
	{
		m_lstStartPoints.Clear();
		Transform transform = GameObject.Find("StartLine").transform;
		m_kStartRotation = transform.rotation;
		for (int i = 0; i < transform.GetChildCount(); i++)
		{
			m_lstStartPoints.Add(transform.GetChild(i).position);
		}
	}

	public Vector3 StartPosition(int idx)
	{
		if (idx < 0)
		{
			idx = 0;
		}
		if (idx >= m_lstStartPoints.Count)
		{
			idx = m_lstStartPoints.Count - 1;
		}
		return m_lstStartPoints[idx];
	}

	public virtual void Uninit()
	{
		CameraManager.instance.UnInit();
		CollisionLayer.Instance.RemoveAllColliders();
		TSingleton<CarManager>.Instance.DestroyCar(m_HeroCar.gameObject);
		TSingleton<PropManager>.Instance.Clear();
		RaceWorld.Instance.Uninit();
		HeightMapManager.Instance.UnInit();
		Engine.EnableAutoRotate();
		TSingleton<PropManager>.Instance.UnInit();
		TSingleton<CarManager>.Instance.UnInit();
		RoleFactory.Instance.UnInit();
		m_Init = false;
		Resources.UnloadUnusedAssets();
	}

	public virtual void UpdateMode()
	{
		Utility.CPU_Profile_Begin_X();
		TSingleton<CarManager>.Instance.Update();
		RaceWorld.Instance.UpdateMonsters();
		Utility.CPU_Profile_End_X("CarManager.update = ");
	}

	public float TotalTime()
	{
		if (RaceModeFactory.ScenePointMap != null && RaceModeFactory.ScenePointMap.ContainsKey(m_SceneRaceComp.iSceneID))
		{
			return RaceModeFactory.ScenePointMap[m_SceneRaceComp.iSceneID].Total_Time;
		}
		return 1000f;
	}

	public int TotalLap()
	{
		if (RaceModeFactory.ScenePointMap != null && RaceModeFactory.ScenePointMap.ContainsKey(m_SceneRaceComp.iSceneID))
		{
			return RaceModeFactory.ScenePointMap[m_SceneRaceComp.iSceneID].Total_Laps;
		}
		return 3;
	}

	public virtual void RaceOver(bool over)
	{
		if (over && !m_RaceOver)
		{
			PlayerData data = new PlayerData();
			if (RaceSceneData.GetPlayerData((ulong)m_HeroCar.ID, ref data))
			{
				string text = string.Empty;
				if (m_HeroCar.GetRole(0).RoleID == 3001)
				{
					text = "_mo";
				}
				else if (m_HeroCar.GetRole(0).RoleID == 3002)
				{
					text = "_RK";
				}
				else if (m_HeroCar.GetRole(0).RoleID == 3004)
				{
					text = "_kula";
				}
				else if (m_HeroCar.GetRole(0).RoleID == 3003)
				{
					text = "_molele";
				}
				else if (m_HeroCar.GetRole(0).RoleID == 3006)
				{
					text = "_yali";
				}
				else if (m_HeroCar.GetRole(0).RoleID == 3005)
				{
					text = "_puti";
				}
				SoundManager.Instance.StopAllSound();
				if (data.Ranking <= 1 && data.IsFinish)
				{
					m_HeroCar.PlayAniOnly("win" + text);
					m_HeroCar.GetRole(0).PlaySound(Role.SUCCESS);
				}
				else
				{
					m_HeroCar.PlayAniOnly("lose" + text);
					m_HeroCar.GetRole(0).PlaySound(Role.FAIL);
				}
				if (!data.IsFinish)
				{
					data.SpendTime += (float)(data.Ranking * 4 + data.Ranking * 3 % 4) + (float)data.Ranking * 0.17f;
					data.IsFinish = true;
					RaceSceneData.SetPlayerData((ulong)m_HeroCar.ID, data);
				}
				TSingleton<PropManager>.Instance.DestrotyPropBox();
			}
			m_HeroCar.LockMovement(bLock: true);
		}
		m_RaceOver = over;
	}

	public virtual void ReStartGame()
	{
		m_RaceOver = false;
		m_HeroCar.SetPosition(StartPosition(0));
		m_HeroCar.SetRotation(StartRotation);
		m_HeroCar.SetVecGlobal(Vector3.zero);
		m_HeroCar.MoveToGround();
		m_MainCamera = GameObject.Find("MainCamera");
		SmoothCameraWithBumper component = m_MainCamera.GetComponent<SmoothCameraWithBumper>();
		component.ResetCamera();
		if (m_HeroCar != null)
		{
			PlayerData data = new PlayerData();
			if (RaceSceneData.GetPlayerData((ulong)m_HeroCar.ID, ref data))
			{
				data.SpendTime = 0f;
				data.ShowLaps = 0;
				data.CurLaps = 0;
				data.PreBlockNum = 0;
			}
		}
		ResetTimer();
	}

	protected void ResetTimer()
	{
		m_fTimer = 0f;
	}

	public virtual void UpdateUI()
	{
		ERaceState currentRaceState = m_currentRaceState;
		if (currentRaceState == ERaceState.START_TIME_COUNT_STATE)
		{
			Car heroCar = TSingleton<CarManager>.Instance.GetHeroCar();
			if (m_fTimer > 0f)
			{
				int index = 3 - (int)(m_fTimer / 1.5f);
				SceneUIManager.Instance.CountDown(index);
			}
			else if (m_fTimer < 0f)
			{
				SceneUIManager.Instance.DisableCountDown();
			}
		}
	}

	public virtual void BeginCountTime()
	{
	}

	public virtual void FinishRace()
	{
		m_currentRaceState = ERaceState.END_RACE_STATE;
	}

	protected void AccelerateCarWhenGo()
	{
		if (!m_InitAccelerate)
		{
			m_InitAccelerate = true;
			Car heroCar = TSingleton<CarManager>.Instance.GetHeroCar();
			if (null != heroCar)
			{
				heroCar.AccelerateCar(0.5f);
			}
		}
	}
}
