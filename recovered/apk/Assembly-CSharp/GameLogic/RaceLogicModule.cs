using System.Collections;
using FrameWork;
using GameNetwork;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

namespace GameLogic;

public class RaceLogicModule : CModule, IRaceLogicModule
{
	private RacingType m_eGameType;

	private SceneRace m_RaceScene;

	public uint m_uiPeerID;

	private Handler<MsgFinishLoadingMap> loadingMapHandler;

	private ArrayList PeersReadyArray = new ArrayList();

	private bool m_bYouServer;

	public RaceLogicModule()
		: base(enmModuleID.MI_RACELOGIC)
	{
	}

	public override void Init(IFrameWork iFrameHost)
	{
		base.Init(iFrameHost);
		TSingleton<RoleConfigData>.Instance.Init();
		TSingleton<CarConfigData>.Instance.Init();
		RaceModeFactory.Init();
		EffectManager.Instance.Init();
	}

	public override void Uninit()
	{
		Debug.Log("RaceSceneModule:Uninit()");
		DestroyGame();
		base.Uninit();
	}

	public RacingType GetCurrentRaceType()
	{
		return m_eGameType;
	}

	public void SetCurrentRaceType(RacingType eType)
	{
		m_eGameType = eType;
	}

	public IRaceMode GetCurrentRaceMode()
	{
		if (m_RaceScene != null)
		{
			return m_RaceScene.GetIRaceMode();
		}
		return null;
	}

	public void CreateGame(int iSceneID)
	{
		if (m_RaceScene != null)
		{
			Debug.LogWarning("You must uninit the previous race mode");
			DestroyGame();
		}
		m_RaceScene = new SceneRace(m_eGameType, iSceneID);
		Debug.Log("Create New Game Mode : " + m_eGameType);
	}

	public void DestroyGame()
	{
		if (m_RaceScene != null)
		{
			m_RaceScene.Uninit();
			m_RaceScene = null;
			if (IsOnlineMode() && IsYouServer())
			{
				StopServerLoop();
			}
		}
	}

	public override void Update(float fCurTime)
	{
		if (m_RaceScene != null)
		{
			m_RaceScene.Update();
		}
	}

	public override void FixedUpdate()
	{
		if (m_RaceScene != null)
		{
			m_RaceScene.FixedUpdate();
		}
	}

	public override void OnGUI()
	{
		if (m_RaceScene != null)
		{
			m_RaceScene.OnGUI();
		}
	}

	public void StartServerLoop()
	{
		loadingMapHandler = OnFinishLoadingMap;
		NetHandlerRegistry.Instance.RegistHandler(loadingMapHandler);
		PeersReadyArray.Clear();
		m_bYouServer = true;
		NetHandlerRegistry.Instance.RegistHandler<MsgFinishRace>(OnFinishRace);
	}

	public void StopServerLoop()
	{
		m_bYouServer = false;
		NetHandlerRegistry.Instance.UnregistHandler(loadingMapHandler);
		NetHandlerRegistry.Instance.UnregistHandler<MsgFinishRace>(OnFinishRace);
	}

	public bool IsOnlineMode()
	{
		if (m_eGameType == RacingType.OnlinePropMode || m_eGameType == RacingType.OnlineTimingMode)
		{
			return true;
		}
		return false;
	}

	public void ServerIsReady()
	{
		int iD = TSingleton<CarManager>.Instance.GetHeroCar().ID;
		if (!PeersReadyArray.Contains(iD))
		{
			PeersReadyArray.Add(iD);
		}
		if (PeersReadyArray.Count == GameDataManager.Instance.GetPlayerDataList().Count)
		{
			MsgStartRace message = new MsgStartRace.Builder().Build();
			LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
			IRaceMode iRaceMode = m_RaceScene.GetIRaceMode();
			iRaceMode.BeginCountTime();
			Debug.Log("Server Is Ready!");
		}
	}

	public bool IsYouServer()
	{
		if (m_bYouServer != NetworkManager.Instance.IsServer)
		{
			Debug.Log("m_bYouServer = " + m_bYouServer);
			Debug.Log("NetworkManager.Instance.IsServer = " + NetworkManager.Instance.IsServer);
		}
		return NetworkManager.Instance.IsServer;
	}

	private void OnFinishLoadingMap(string peerId, MsgFinishLoadingMap msg)
	{
		Debug.Log("OnFinishLoadingMap = " + peerId);
		if (!PeersReadyArray.Contains(msg.PlayerId))
		{
			PeersReadyArray.Add(msg.PlayerId);
		}
		if (PeersReadyArray.Count == GameDataManager.Instance.GetPlayerDataList().Count)
		{
			MsgStartRace message = new MsgStartRace.Builder().Build();
			LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
			IRaceMode iRaceMode = m_RaceScene.GetIRaceMode();
			iRaceMode.BeginCountTime();
			PeersReadyArray.Clear();
			Debug.Log("All member is Ready!");
		}
	}

	public void ServerIsFinishRace()
	{
		Debug.Log("ServerIsFinishRace Now!!!");
		MsgGameResult.Builder builder = new MsgGameResult.Builder();
		MsgGameResult message = builder.Build();
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
		SceneUIManager.Instance.ShowRankUI(isNetwork: true, isHost: true);
	}

	private void OnFinishRace(string peerId, MsgFinishRace msg)
	{
		MsgGameResult.Builder builder = new MsgGameResult.Builder();
		MsgGameResult message = builder.Build();
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
		SceneUIManager.Instance.ShowRankUI(isNetwork: true, isHost: true);
	}
}
