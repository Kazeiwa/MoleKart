using System;
using System.Collections;
using FrameWork;
using GameNetwork;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class HostRoomUI : MonoBehaviour
{
	private const int MAX_PALYER_NUMBER = 4;

	public const int CURRENT_GAMESERVER_VERSION = 4;

	private bool[] slots = new bool[4];

	private int selectedTrack = 1100;

	private RacingType gameMode = RacingType.OnlinePropMode;

	public Texture2D[] m_PicTimes;

	public GameObject[] m_TimePlanes;

	public Texture2D[] m_TimePic;

	private bool canGoBack = true;

	private GameObject startBtn;

	private int m_CurrentLevel;

	public GameObject[] m_Items;

	private int m_TempSelectedTrack = 1100;

	private int CheckDiffculty()
	{
		if (TrackLockManager.Instance.Medal(selectedTrack, 1) > 0)
		{
			m_CurrentLevel = 2;
		}
		else if (TrackLockManager.Instance.Medal(selectedTrack, 0) > 0)
		{
			m_CurrentLevel = 1;
		}
		else
		{
			m_CurrentLevel = 0;
		}
		return m_CurrentLevel;
	}

	private void SetTrackRecordTime()
	{
		if (m_TimePlanes == null)
		{
			return;
		}
		for (int i = 0; i < m_TimePlanes.Length; i++)
		{
			if (!(null == m_TimePlanes[i]))
			{
				m_TimePlanes[i].GetComponent<MeshRenderer>().enabled = true;
				float num = LocalUserConfig.Instance.Record(1100 + i + 1, CheckDiffculty(), gameMode);
				if (num > 0f)
				{
					int num2 = (int)num;
					int num3 = num2 / 6000;
					int num4 = num3 / 10;
					int num5 = num3 % 10;
					int num6 = num2 / 100;
					int num7 = num6 % 60 / 10;
					int num8 = num6 % 60 % 10;
					int num9 = num2 % 10;
					int num10 = num2 / 10 % 10;
					m_TimePic[i].SetPixels(5, 4, m_PicTimes[num5].width, m_PicTimes[num5].height, m_PicTimes[num5].GetPixels(0, 0, m_PicTimes[num5].width, m_PicTimes[num5].height));
					m_TimePic[i].SetPixels(43, 4, m_PicTimes[num7].width, m_PicTimes[num7].height, m_PicTimes[num7].GetPixels(0, 0, m_PicTimes[num7].width, m_PicTimes[num7].height));
					m_TimePic[i].SetPixels(67, 4, m_PicTimes[num8].width, m_PicTimes[num8].height, m_PicTimes[num8].GetPixels(0, 0, m_PicTimes[num8].width, m_PicTimes[num8].height));
					m_TimePic[i].SetPixels(115, 4, m_PicTimes[num10].width, m_PicTimes[num10].height, m_PicTimes[num10].GetPixels(0, 0, m_PicTimes[num10].width, m_PicTimes[num10].height));
					m_TimePic[i].SetPixels(139, 4, m_PicTimes[num9].width, m_PicTimes[num9].height, m_PicTimes[num9].GetPixels(0, 0, m_PicTimes[num9].width, m_PicTimes[num9].height));
					m_TimePic[i].Apply();
					m_TimePlanes[i].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_TimePic[i]);
				}
				else
				{
					m_TimePlanes[i].GetComponent<MeshRenderer>().enabled = false;
				}
			}
		}
	}

	protected void Start()
	{
		NetworkManager.Instance.SetSendHeartbeat(isSend: true);
		NetworkManager.Instance.notDealPeerDisconnected = true;
		DestoryShelter();
		if (!SoundManager.Instance.IsPlaying(60002))
		{
			SoundManager.Instance.PlaySoundEx(60002);
		}
		Debug.LogWarning("is server:" + AppMain.Instance.IsServer);
		canGoBack = true;
		startBtn = GameObject.Find("StartGame");
		Debug.Log("Host On Start");
		SetTrackRecordTime();
		SelectRandom();
		SelectPropMode();
		LocalNetFactory.Instance.LocalNet.SetServerAvailable(reliable: true);
		NetHandlerRegistry.Instance.RegistHandler<MsgPlayerJoinReq>(OnPlayerJoinReq);
		NetHandlerRegistry.Instance.RegistHandler<MsgRequestPlayerList>(OnRequestPlayerList);
		NetHandlerRegistry.Instance.RegistHandler<MsgPlayerLeave>(RemovePlayer);
		NetHandlerRegistry.Instance.RegistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
		NetHandlerRegistry.Instance.RegistHandler<MsgClientJoined>(OnClientJoined);
		NetHandlerRegistry.Instance.RegistHandler<MsgCheckLink>(AckLink);
		for (int i = 0; i < 4; i++)
		{
			slots[i] = false;
		}
		slots[0] = true;
		if (null != SceneUIManager.Instance.m_Loading)
		{
			SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: false);
			UtilBinding.ShowLoadingIndicator(bShow: false);
		}
		ChangeTrackAndModePreview();
		GameObject gameObject = GameObject.Find("ScrollItems/Item1");
		if (null != gameObject)
		{
			GameObject gameObject2 = GameObject.Find("MainCamera/UI/ScrollList");
			if (null != gameObject2)
			{
				UIScrollList component = gameObject2.GetComponent<UIScrollList>();
				if (null != component)
				{
					component.DidSelect(gameObject.GetComponent<UIListItem>());
				}
			}
		}
		GameObject gameObject3 = GameObject.Find("propBtn");
		gameObject3.GetComponent<UIRadioBtn>().Value = true;
		UpdatePlayerListView();
	}

	private void DestoryShelter()
	{
		for (int i = 1; i <= 9; i++)
		{
			if (m_Items != null && i - 1 < m_Items.Length && TrackLockManager.Instance.ReleaseTrack(1100 + i) == 0 && null != m_Items[i - 1])
			{
				UnityEngine.Object.Destroy(m_Items[i - 1].transform.FindChild("Shelter").gameObject);
			}
		}
	}

	protected void OnDestroy()
	{
		Debug.Log("Host On Destroy");
		NetHandlerRegistry.Instance.UnregistHandler<MsgPlayerJoinReq>(OnPlayerJoinReq);
		NetHandlerRegistry.Instance.UnregistHandler<MsgRequestPlayerList>(OnRequestPlayerList);
		NetHandlerRegistry.Instance.UnregistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
		NetHandlerRegistry.Instance.UnregistHandler<MsgClientJoined>(OnClientJoined);
		NetHandlerRegistry.Instance.UnregistHandler<MsgPlayerLeave>(RemovePlayer);
		NetHandlerRegistry.Instance.UnregistHandler<MsgCheckLink>(AckLink);
	}

	private void AckLink(string peerId, MsgCheckLink message)
	{
		NetworkManager.Instance.SendMessageToPeer(peerId, message);
	}

	private void StartGame()
	{
		NetworkManager.Instance.SetSendHeartbeat(isSend: false);
		Debug.Log("HostRoomUi : StartGame");
		SceneUIManager.Instance.m_CurStrings = null;
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule)
		{
			raceLogicModule.SetCurrentRaceType(gameMode);
			raceLogicModule.StartServerLoop();
		}
		if (null != SceneUIManager.Instance.m_Loading)
		{
			SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
			SceneUIManager.Instance.m_Loading.GetComponent<LoadingController>().ShowLoadingIndicator();
		}
		NetworkManager.Instance.notDealPeerDisconnected = false;
		SoundManager.Instance.StopSound(60002);
		SceneManager.Instance.LoadLevel(selectedTrack);
	}

	public void OnPlayerJoinReq(string peerId, MsgPlayerJoinReq req)
	{
		Debug.Log("HostRoomUI.OnPlayerJoinReq: peerId=" + peerId);
		if (GameDataManager.Instance.GetPlayerDataList().Count < 4 && req.Version == 4)
		{
			PlayerData newPlayer = new PlayerData();
			newPlayer.name = req.Name;
			newPlayer.role = (RoleType)req.RoleType;
			newPlayer.carType = (CarModeType)req.CarType;
			newPlayer.peerID = peerId;
			newPlayer.StartPositionId = GetOneSlot();
			if (GameDataManager.Instance.AddPlayer(ref newPlayer))
			{
				MsgPlayerJoinResp.Builder builder = MsgPlayerJoinResp.CreateBuilder();
				builder.SetPeerId(peerId);
				builder.SetResult(0);
				builder.SetPlayerId((int)newPlayer.PlayerId);
				builder.SetStartPosition(newPlayer.StartPositionId);
				MsgTrackAndModeSelected.Builder builder2 = MsgTrackAndModeSelected.CreateBuilder();
				builder2.SetGameMode((int)gameMode);
				builder2.SetTrackId(selectedTrack);
				MsgTrackAndModeSelected trackMode = builder2.Build();
				builder.SetTrackMode(trackMode);
				Debug.Log("HostRoomUI.OnPlayerJoinReq: Send MsgPlayerJoinResp to peerId=" + peerId + "start position = " + newPlayer.StartPositionId + "Player id = " + newPlayer.PlayerId);
				ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
				MsgNotifyPlayerList.Builder builder3 = MsgNotifyPlayerList.CreateBuilder();
				foreach (PlayerData item in playerDataList)
				{
					MsgPlayer.Builder builder4 = MsgPlayer.CreateBuilder();
					builder4.SetCarType((int)item.carType);
					builder4.SetName(item.name);
					builder4.SetPeerId(item.peerID);
					builder4.SetPlayerId((int)item.PlayerId);
					builder4.SetRoleType((int)item.role);
					builder4.SetStartPosition(item.StartPositionId);
					MsgPlayer value = builder4.Build();
					builder3.AddPlayerList(value);
				}
				MsgNotifyPlayerList playerlist = builder3.Build();
				builder.SetPlayerlist(playerlist);
				MsgPlayerJoinResp msg = builder.Build();
				ISession session = LocalNetFactory.Instance.LocalNet.SessionManager.FindSessionByPeerId(peerId);
				session.Send(msg, available: true);
				session.EndSend();
				RebuildPlayerList();
				UpdatePlayerListView();
			}
			else
			{
				FreeOneSlot(newPlayer.StartPositionId);
				Debug.Log("HostRoomUI.OnPlayerJoinReq: FreeOneSlotstart position = " + newPlayer.StartPositionId);
			}
		}
		else
		{
			int result = -1;
			if (req.Version != 4)
			{
				result = -2;
			}
			MsgPlayerJoinResp.Builder builder5 = MsgPlayerJoinResp.CreateBuilder();
			builder5.SetPeerId(peerId);
			builder5.SetResult(result);
			builder5.SetPlayerId(-1);
			builder5.SetStartPosition(-1);
			MsgPlayerJoinResp msg2 = builder5.Build();
			ISession session2 = LocalNetFactory.Instance.LocalNet.SessionManager.FindSessionByPeerId(peerId);
			session2.Send(msg2, available: true);
		}
	}

	private int GetOneSlot()
	{
		ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
		Debug.Log("Current Players num = " + playerDataList.Count);
		foreach (PlayerData item in playerDataList)
		{
			int startPositionId = item.StartPositionId;
			if (!slots[startPositionId])
			{
				Debug.Log("GetOneSlot Error! slots[" + startPositionId + "] is not empty! peer id = " + item.peerID);
				slots[startPositionId] = true;
			}
		}
		for (int i = 0; i < 4; i++)
		{
			if (!slots[i])
			{
				slots[i] = true;
				return i;
			}
		}
		Debug.Log("GetOneSlot Error!");
		return -1;
	}

	private void FreeOneSlot(int i)
	{
		if (i < 4 && i >= 0)
		{
			slots[i] = false;
		}
	}

	private void RebuildPlayerList()
	{
		ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
		MsgNotifyPlayerList.Builder builder = MsgNotifyPlayerList.CreateBuilder();
		foreach (PlayerData item in playerDataList)
		{
			MsgPlayer.Builder builder2 = MsgPlayer.CreateBuilder();
			builder2.SetCarType((int)item.carType);
			builder2.SetName(item.name);
			builder2.SetPeerId(item.peerID);
			builder2.SetPlayerId((int)item.PlayerId);
			builder2.SetRoleType((int)item.role);
			builder2.SetStartPosition(item.StartPositionId);
			MsgPlayer value = builder2.Build();
			builder.AddPlayerList(value);
		}
		MsgNotifyPlayerList message = builder.Build();
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
	}

	private void OnRequestPlayerList(string peerId, MsgRequestPlayerList req)
	{
		ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
		MsgNotifyPlayerList.Builder builder = MsgNotifyPlayerList.CreateBuilder();
		foreach (PlayerData item in playerDataList)
		{
			MsgPlayer.Builder builder2 = MsgPlayer.CreateBuilder();
			builder2.SetCarType((int)item.carType);
			builder2.SetName(item.name);
			builder2.SetPeerId(item.peerID);
			builder2.SetPlayerId((int)item.PlayerId);
			builder2.SetRoleType((int)item.role);
			builder2.SetStartPosition(item.StartPositionId);
			MsgPlayer value = builder2.Build();
			builder.AddPlayerList(value);
		}
		MsgNotifyPlayerList msg = builder.Build();
		ISession session = LocalNetFactory.Instance.LocalNet.SessionManager.FindSessionByPeerId(peerId);
		session.Send(msg, available: true);
	}

	private void OnPeerDisconnected(string peerId, MsgOnPeerDisconnect message)
	{
		Debug.LogWarning("HostRoomUI: A client disconnected!");
		PlayerData data = new PlayerData();
		if (GameDataManager.Instance.GetPlayerData(peerId, ref data))
		{
			FreeOneSlot(data.StartPositionId);
			Debug.Log("OnPeerDisconnected, remove FreeOneSlot!!= " + data.StartPositionId);
		}
		GameDataManager.Instance.RemovePlayer(peerId);
		RebuildPlayerList();
		UpdatePlayerListView();
	}

	private void RemovePlayer(string peerId, MsgPlayerLeave msg)
	{
		Debug.Log("Client leave, remove client!!");
		PlayerData data = new PlayerData();
		if (GameDataManager.Instance.GetPlayerData(msg.PeerId, ref data))
		{
			FreeOneSlot(data.StartPositionId);
			Debug.Log("Client leave, remove FreeOneSlot!!= " + data.StartPositionId);
		}
		GameDataManager.Instance.RemovePlayer(msg.PeerId);
		RebuildPlayerList();
		UpdatePlayerListView();
	}

	private void GoBack()
	{
		if (canGoBack)
		{
			LocalNetFactory.Instance.LocalNet.SetServerAvailable(reliable: false);
			NetworkManager.Instance.notDealPeerDisconnected = true;
			UtilBinding.ShowWaitingView(bShow: true);
			Debug.LogWarning("host leave");
			MsgHostLeave.Builder builder = MsgHostLeave.CreateBuilder();
			MsgHostLeave message = builder.Build();
			LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
			LocalNetFactory.Instance.LocalNet.SessionManager.EndSend();
			NetworkManager.Instance.StopServer();
			SceneManager.Instance.LoadLevel(1005);
		}
	}

	private void SelectRandom()
	{
		if (canGoBack)
		{
			selectedTrack = 1100;
			m_TempSelectedTrack = 1100;
			TrackAndModeSelected();
		}
	}

	private void SelectPort()
	{
		if (canGoBack)
		{
			if (TrackLockManager.Instance.ReleaseTrack(1101) == 0)
			{
				selectedTrack = 1101;
			}
			m_TempSelectedTrack = 1101;
			TrackAndModeSelected();
		}
	}

	private void SelectFarm()
	{
		if (canGoBack)
		{
			if (TrackLockManager.Instance.ReleaseTrack(1102) == 0)
			{
				selectedTrack = 1102;
			}
			m_TempSelectedTrack = 1102;
			TrackAndModeSelected();
		}
	}

	private void SelectMogu()
	{
		if (canGoBack)
		{
			if (TrackLockManager.Instance.ReleaseTrack(1103) == 0)
			{
				selectedTrack = 1103;
			}
			m_TempSelectedTrack = 1103;
			TrackAndModeSelected();
		}
	}

	private void SelectSky()
	{
		if (canGoBack)
		{
			if (TrackLockManager.Instance.ReleaseTrack(1104) == 0)
			{
				selectedTrack = 1104;
			}
			m_TempSelectedTrack = 1104;
			TrackAndModeSelected();
		}
	}

	private void SelectVolcano()
	{
		if (canGoBack)
		{
			if (TrackLockManager.Instance.ReleaseTrack(1105) == 0)
			{
				selectedTrack = 1105;
			}
			m_TempSelectedTrack = 1105;
			TrackAndModeSelected();
		}
	}

	private void SelectIce()
	{
		if (canGoBack)
		{
			if (TrackLockManager.Instance.ReleaseTrack(1106) == 0)
			{
				selectedTrack = 1106;
			}
			m_TempSelectedTrack = 1106;
			TrackAndModeSelected();
		}
	}

	private void SelectManor()
	{
		if (canGoBack)
		{
			if (TrackLockManager.Instance.ReleaseTrack(1107) == 0)
			{
				selectedTrack = 1107;
			}
			m_TempSelectedTrack = 1107;
			TrackAndModeSelected();
		}
	}

	private void SelectPropMode()
	{
		if (canGoBack)
		{
			gameMode = RacingType.OnlinePropMode;
			SetTrackRecordTime();
			TrackAndModeSelected();
		}
	}

	private void SelectTimingMode()
	{
		if (canGoBack)
		{
			gameMode = RacingType.OnlineTimingMode;
			SetTrackRecordTime();
			TrackAndModeSelected();
		}
	}

	private void ChangeTrackAndModePreview()
	{
		Transform transform = GameObject.Find("TrackPreview").transform;
		for (int i = 0; i < transform.GetChildCount(); i++)
		{
			if (transform.GetChild(i).name == selectedTrack.ToString())
			{
				transform.GetChild(i).gameObject.active = true;
			}
			else
			{
				transform.GetChild(i).gameObject.active = false;
			}
		}
	}

	private void UpdatePlayerListView()
	{
		Debug.Log("UpdateHostPlayerListView!!!!");
	}

	private void TrackAndModeSelected()
	{
		MsgTrackAndModeSelected.Builder builder = MsgTrackAndModeSelected.CreateBuilder();
		builder.SetGameMode((int)gameMode);
		builder.SetTrackId(selectedTrack);
		MsgTrackAndModeSelected message = builder.Build();
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
		LocalNetFactory.Instance.LocalNet.SessionManager.EndSend();
		ChangeTrackAndModePreview();
	}

	private void StartGameAsServer()
	{
		if (GameDataManager.Instance.GetPlayerDataList().Count != 1)
		{
			FlurryBinding.ReportPlayOnlineGame();
			LocalNetFactory.Instance.LocalNet.SetServerAvailable(reliable: false);
			canGoBack = false;
			MsgStartGame.Builder builder = MsgStartGame.CreateBuilder();
			builder.SetGameMode((int)gameMode);
			if (selectedTrack == 1100)
			{
				selectedTrack = RandomTrack();
			}
			builder.SetTrackId(selectedTrack);
			MsgStartGame message = builder.Build();
			LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
			StartGame();
		}
	}

	private int RandomTrack()
	{
		System.Random random = new System.Random((int)Time.time);
		int num = random.Next() % 450;
		for (int num2 = 9; num2 > 0; num2--)
		{
			if (num >= (num2 - 1) * 50 && TrackLockManager.Instance.ReleaseTrack(1100 + num2) == 0)
			{
				return 1100 + num2;
			}
		}
		return 1102;
	}

	private void Update()
	{
		if (GameDataManager.Instance.GetPlayerDataList().Count == 1)
		{
			UIButton component = startBtn.GetComponent<UIButton>();
			component.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		}
		else if (TrackLockManager.Instance.ReleaseTrack(m_TempSelectedTrack) == 0)
		{
			UIButton component2 = startBtn.GetComponent<UIButton>();
			component2.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		}
		else
		{
			UIButton component3 = startBtn.GetComponent<UIButton>();
			component3.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		}
	}

	private void OnClientJoined(string peerId, MsgClientJoined message)
	{
		MsgTrackAndModeSelected.Builder builder = MsgTrackAndModeSelected.CreateBuilder();
		builder.SetGameMode((int)gameMode);
		builder.SetTrackId(selectedTrack);
		MsgTrackAndModeSelected message2 = builder.Build();
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message2, reliable: true);
		LocalNetFactory.Instance.LocalNet.SessionManager.EndSend();
	}
}
