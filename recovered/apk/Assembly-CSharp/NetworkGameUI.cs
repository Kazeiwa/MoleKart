using System.Collections;
using System.Collections.Generic;
using GameNetwork;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class NetworkGameUI : MonoBehaviour
{
	private bool showRadar = true;

	private GameObject radar;

	private GameObject radarBackground;

	private GameObject radarTitle;

	private GameObject tip;

	private GameObject cantFind;

	private float rotateAngle;

	private float speed = 100f;

	private Hashtable remoteTable = new Hashtable();

	private Dictionary<string, string> localDict = new Dictionary<string, string>();

	private bool reloadTable;

	private bool canClick = true;

	private float watiTime = 13.5f;

	private bool joinBtnPressed;

	private bool nowQuit;

	private bool canShowRooms;

	private float startWaitTime = 2f;

	private float searchWaitTime = 5f;

	private bool isNetworkAvaliable = true;

	public GameObject gLoading;

	private Handler<MsgPlayerJoinResp> playerjoinResp;

	public static int selectedTrack = -1;

	public static RacingType gameMode = RacingType.INVAID_MODE;

	private string connectingToPeer;

	private void Awake()
	{
		UtilBinding.ShowLoadingIndicator(bShow: false);
		NetHandlerRegistry.Instance.RegistHandler<MsgNetworkAllDisable>(OnNetworkDisabled);
	}

	private void Start()
	{
		if (!SoundManager.Instance.IsPlaying(60002))
		{
			SoundManager.Instance.PlaySoundEx(60002);
		}
		CheckEnable();
		canClick = true;
		GameDataManager.Instance.RemoveAllPlayers();
		showRadar = true;
		radar = GameObject.Find("radar_active");
		radarBackground = GameObject.Find("radar");
		radarTitle = GameObject.Find("searchGame");
		cantFind = GameObject.Find("cantFind");
		if ((bool)cantFind)
		{
			cantFind.active = false;
		}
		tip = GameObject.Find("Tip");
		UIRadioBtn component = GameObject.Find("GameObject3").GetComponent<UIRadioBtn>();
		component.Value = true;
		CharacterSelectedLele();
		if (LocalNetFactory.Instance.LocalNet != null)
		{
			LocalNetFactory.Instance.LocalNet.SetServerAvailable(reliable: false);
			Debug.LogWarning("stopserver");
			StopServer();
		}
		connectingToPeer = null;
		StartClient();
		if (SceneUIManager.Instance.m_Loading != null)
		{
			Object.Destroy(SceneUIManager.Instance.m_Loading);
		}
		if (null == gLoading)
		{
			gLoading = GameObject.Find("LoadingUI");
		}
		if (null != gLoading)
		{
			UtilBinding.ShowLoadingIndicator(bShow: false);
			gLoading.SetActiveRecursively(state: false);
			Object.DontDestroyOnLoad(gLoading);
			SceneUIManager.Instance.m_Loading = gLoading;
		}
	}

	private IEnumerator NowScanServer()
	{
		yield return new WaitForSeconds(2f);
		StartClient();
	}

	protected void OnGUI()
	{
		float num = 5f;
		float num2 = 10f;
		float height = 30f;
		float num3 = 35f;
		float num4 = 160f;
		if (!(AppMain.Instance.GetCurrentPlatform() == string.Empty))
		{
			return;
		}
		GUI.Label(new Rect(num2, num += num3, num4 + 30f, height), "available ServersList:");
		foreach (KeyValuePair<string, string> availableServer in NetworkManager.Instance.AvailableServers)
		{
			GUI.Label(new Rect(num2, num += num3, num4 + 260f, height), "peerId:" + availableServer.Key + " peerName:" + availableServer.Value);
			if (GUI.Button(new Rect(num2 + 260f, num, num4, height), "Connect To Server"))
			{
				if (NetworkManager.Instance.ConnectionType == 1)
				{
					StopServer();
				}
				if (NetworkManager.Instance.ConnectionType != 2)
				{
					StartClient();
				}
				connectingToPeer = availableServer.Key;
				LocalNetFactory.Instance.LocalNet.JoinServer(availableServer.Key);
			}
		}
		GUI.Label(new Rect(num2, num += num3, num4 + 260f, height), "connected Server:peerId:" + NetworkManager.Instance.ClientPeerId + " peerName:" + NetworkManager.Instance.ClientPeerName);
	}

	public void StartClient()
	{
		Debug.Log("NetworkGameUI StartClient");
		NetworkManager.Instance.StartClient();
		playerjoinResp = OnPlayerJoinResp;
		NetHandlerRegistry.Instance.RegistHandler(playerjoinResp);
		NetHandlerRegistry.Instance.RegistHandler<MsgOnPeerConnected>(OnPeerConnected);
		NetHandlerRegistry.Instance.RegistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
		NetHandlerRegistry.Instance.RegistHandler<MsgCannotConnected>(OnCannotConnected);
		NetHandlerRegistry.Instance.RegistHandler<MsgPeerBecameUnavailable>(OnPeerUnavailable);
	}

	public void StopClient()
	{
		Debug.Log("NetworkGameUI StopClient");
		NetworkManager.Instance.StopClient();
		NetHandlerRegistry.Instance.UnregistHandler(playerjoinResp);
	}

	public void StartServer()
	{
		Debug.Log("NetworkGameUI Start Server");
		NetworkManager.Instance.StartServer();
		PlayerData newPlayer = new PlayerData();
		newPlayer.name = LocalUserConfig.Instance.userName;
		newPlayer.role = (RoleType)LocalUserInfo.Instance.UserInfo.roleType;
		newPlayer.carType = (CarModeType)LocalUserInfo.Instance.UserInfo.CarId;
		newPlayer.peerID = LocalNetFactory.Instance.LocalNet.GetMyselfPeerId();
		NetworkManager.Instance.ServerPeerId = LocalNetFactory.Instance.LocalNet.GetMyselfPeerId();
		newPlayer.IsMaster = true;
		newPlayer.StartPositionId = 0;
		GameDataManager.Instance.AddPlayer(ref newPlayer);
		SceneManager.Instance.LoadLevel(1006);
	}

	public void StopServer()
	{
		Debug.Log("NetworkGameUI Stop Server");
		PlayerData newPlayer = new PlayerData();
		newPlayer.name = LocalUserConfig.Instance.userName;
		newPlayer.role = (RoleType)LocalUserInfo.Instance.UserInfo.roleType;
		newPlayer.carType = (CarModeType)LocalUserInfo.Instance.UserInfo.CarId;
		newPlayer.peerID = NetworkManager.Instance.ServerPeerId;
		newPlayer.IsMaster = true;
		GameDataManager.Instance.RemovePlayer(ref newPlayer);
		NetworkManager.Instance.StopServer();
	}

	private void CharacterSelectedKula()
	{
		if (canClick)
		{
			ChangeCharacter(3004);
		}
	}

	private void CharacterSelectedMomo()
	{
		if (canClick)
		{
			ChangeCharacter(3001);
		}
	}

	private void CharacterSelectedRk()
	{
		if (canClick)
		{
			ChangeCharacter(3002);
		}
	}

	private void CharacterSelectedLele()
	{
		if (canClick)
		{
			ChangeCharacter(3003);
		}
	}

	private void CharacterSelectedMmt()
	{
		if (canClick)
		{
			ChangeCharacter(3201);
		}
	}

	private void CharacterSelectedFred()
	{
		if (canClick)
		{
			ChangeCharacter(3101);
		}
	}

	private void ChangeCharacter(ushort roleId)
	{
		UserInfo userInfo = LocalUserInfo.Instance.UserInfo;
		if (userInfo.CarId != roleId)
		{
			userInfo.CarId = (ushort)(roleId - 1000);
			userInfo.UserId = roleId;
			userInfo.roleType = roleId;
			LocalUserInfo.Instance.UserInfo = userInfo;
		}
	}

	private void SendJoinMsg2Server()
	{
		Debug.Log("NetworkGameUI.SendJoinMsg2Server");
		MsgPlayerJoinReq.Builder builder = MsgPlayerJoinReq.CreateBuilder();
		builder.SetCarType(LocalUserInfo.Instance.UserInfo.CarId);
		builder.SetName(LocalUserConfig.Instance.userName);
		builder.SetPeerId(NetworkManager.Instance.ClientPeerId);
		builder.SetRoleType(LocalUserInfo.Instance.UserInfo.roleType);
		builder.Version = 4;
		MsgPlayerJoinReq message = builder.Build();
		NetworkManager.Instance.BroadCastMessage(message, reliable: true);
	}

	public void ShowGameVersionError()
	{
		string text = "Join game failed";
		string text2 = "Your game version is not as same as the host, can't join the game!";
		string text3 = "OK";
		if (AppMain.Instance.GetCurrentSystemLanguage().Equals("zh-Hant"))
		{
			text = "加入遊戲失敗";
			text2 = "您的遊戲版本與主機不一致，加入遊戲失敗！";
			text3 = "確定";
		}
		if (AppMain.Instance.GetCurrentSystemLanguage().Equals("zh-Hans"))
		{
			text = "加入游戏失败";
			text2 = "您的游戏版本与主机不一致，加入游戏失败！";
			text3 = "确定";
		}
	}

	public void OnPlayerJoinResp(string peerId, MsgPlayerJoinResp msg)
	{
		Debug.Log("OnPlayerJoinResp [" + msg.PeerId + "|" + msg.Result + "]");
		ClientRoomUI.serverPeerId = peerId;
		if (msg.Result == 0)
		{
			Debug.Log("Join the server success!");
			PlayerData playerData = new PlayerData();
			playerData.name = LocalUserConfig.Instance.userName;
			playerData.role = (RoleType)LocalUserInfo.Instance.UserInfo.roleType;
			playerData.carType = (CarModeType)LocalUserInfo.Instance.UserInfo.CarId;
			playerData.peerID = msg.PeerId;
			playerData.PlayerId = (uint)msg.PlayerId;
			playerData.IsMaster = true;
			playerData.StartPositionId = msg.StartPosition;
			GameDataManager.Instance.AddPlayer(playerData, (uint)msg.PlayerId);
			selectedTrack = msg.TrackMode.TrackId;
			gameMode = (RacingType)msg.TrackMode.GameMode;
			Debug.Log("process player list");
			MsgNotifyPlayerList playerlist = msg.Playerlist;
			ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < playerlist.PlayerListCount; i++)
			{
				MsgPlayer playerList = playerlist.GetPlayerList(i);
				bool flag = true;
				foreach (PlayerData item in playerDataList)
				{
					if (item.peerID == playerList.PeerId)
					{
						arrayList.Add(item);
						flag = false;
						break;
					}
				}
				if (flag)
				{
					PlayerData playerData3 = new PlayerData();
					playerData3.PlayerId = (uint)playerList.PlayerId;
					playerData3.role = (RoleType)playerList.RoleType;
					playerData3.name = playerList.Name;
					playerData3.carType = (CarModeType)playerList.CarType;
					playerData3.StartPositionId = playerList.StartPosition;
					playerData3.peerID = playerList.PeerId;
					arrayList.Add(playerData3);
				}
			}
			GameDataManager.Instance.SetPlayerDataList(arrayList);
			NetHandlerRegistry.Instance.UnregistHandler(playerjoinResp);
			SceneManager.Instance.LoadLevel(1007);
		}
		else
		{
			UtilBinding.ShowWaitingView(bShow: false);
			Debug.LogWarning("Join the server failed!");
			if (msg.Result == -2)
			{
				ShowGameVersionError();
			}
			if (msg.Result == -1)
			{
				ShowConnectFailAlert();
			}
		}
	}

	public void Update()
	{
		if (startWaitTime > 0f)
		{
			startWaitTime -= Time.deltaTime;
			showRadar = true;
		}
		else
		{
			canShowRooms = true;
		}
		if (nowQuit)
		{
			return;
		}
		reloadTable = false;
		if (joinBtnPressed)
		{
			if (watiTime > 0f)
			{
				watiTime -= Time.deltaTime;
			}
			else
			{
				joinBtnPressed = false;
			}
		}
		else
		{
			if (connectingToPeer != null)
			{
				connectingToPeer = null;
				NetworkManager.Instance.StopClient();
				UtilBinding.ShowWaitingView(bShow: false);
				NetworkManager.Instance.StartClient();
			}
			watiTime = 13.5f;
		}
		if (showRadar)
		{
			if (searchWaitTime > 0f)
			{
				radarTitle.active = true;
				searchWaitTime -= Time.deltaTime;
			}
			else
			{
				radarTitle.active = false;
				if ((bool)cantFind)
				{
					cantFind.active = true;
				}
			}
			radar.active = true;
			radarBackground.active = true;
			tip.active = true;
			rotateAngle += Time.deltaTime * speed;
			if (rotateAngle >= 360f)
			{
				rotateAngle = 0f;
			}
			radar.transform.localEulerAngles = new Vector3(0f, 180f, rotateAngle);
		}
		else
		{
			if ((bool)cantFind)
			{
				cantFind.active = false;
			}
			radar.active = false;
			radarTitle.active = false;
			radarBackground.active = false;
			tip.active = false;
		}
		if (NetworkManager.Instance.AvailableServers.Count > 0 && canShowRooms)
		{
			if (NetworkManager.Instance.AvailableServers.Count != remoteTable.Count)
			{
				reloadTable = true;
			}
			if (!reloadTable)
			{
				foreach (KeyValuePair<string, string> availableServer in NetworkManager.Instance.AvailableServers)
				{
					if (!remoteTable.ContainsKey(availableServer.Key))
					{
						reloadTable = true;
						break;
					}
				}
			}
			searchWaitTime = 5f;
			showRadar = false;
			if ((bool)cantFind)
			{
				cantFind.active = false;
			}
			if (reloadTable)
			{
				Debug.LogWarning("RRRRRRRRReload table");
				UpdateHostListView();
			}
		}
		else
		{
			showRadar = true;
		}
		remoteTable.Clear();
		if (!canShowRooms)
		{
			return;
		}
		foreach (KeyValuePair<string, string> availableServer2 in NetworkManager.Instance.AvailableServers)
		{
			remoteTable.Add(availableServer2.Key, availableServer2.Value);
		}
	}

	private void JoinButtonPressed(string peerId)
	{
		Debug.Log("JoinButtonPressed join to server:" + peerId);
		joinBtnPressed = true;
		UtilBinding.ShowWaitingView(bShow: true);
		if (NetworkManager.Instance.ConnectionType == 1)
		{
			StopServer();
		}
		if (NetworkManager.Instance.ConnectionType != 2)
		{
			StartClient();
		}
		connectingToPeer = peerId;
		LocalNetFactory.Instance.LocalNet.JoinServer(peerId);
	}

	public void OnPeerConnected(string peerId, MsgOnPeerConnected message)
	{
		Debug.Log("NetworkGameUI.OnPeerConnected: peerId=[" + peerId + "]");
		if (connectingToPeer == peerId)
		{
			SendJoinMsg2Server();
			connectingToPeer = null;
		}
	}

	public void OnPeerDisconnected(string peerId, MsgOnPeerDisconnect message)
	{
		Debug.Log("NetworkGameUI.OnPeerDisconnected: peerId=[" + peerId + "]");
		if (connectingToPeer == peerId)
		{
			UtilBinding.ShowWaitingView(bShow: false);
			ShowConnectFailAlert();
			connectingToPeer = null;
		}
	}

	public void OnCannotConnected(string peerId, MsgCannotConnected msg)
	{
		Debug.Log("NetworkGameUI.OnCannotConnected: peerId=[" + peerId + "]");
		if (connectingToPeer == peerId)
		{
			NetworkManager.Instance.StopClient();
			UtilBinding.ShowWaitingView(bShow: false);
			ShowConnectFailAlert();
			connectingToPeer = null;
			NetworkManager.Instance.StartClient();
		}
	}

	public void OnPeerUnavailable(string peerId, MsgPeerBecameUnavailable msg)
	{
		if (connectingToPeer == peerId)
		{
			Debug.Log("NetworkGameUI.OnPeerUnavailable: peerId=[" + peerId + "]");
			UtilBinding.ShowWaitingView(bShow: false);
			ShowConnectFailAlert();
			connectingToPeer = null;
		}
	}

	private void StartAsServer()
	{
		if (!NetworkManager.Instance.IsNetworkAvaliable)
		{
			ShowNetworkDisableAlert();
			return;
		}
		UtilBinding.ShowWaitingView(bShow: true);
		if (NetworkManager.Instance.ConnectionType == 2)
		{
			StopClient();
		}
		if (NetworkManager.Instance.ConnectionType != 1)
		{
			StartServer();
		}
	}

	public void GoBack()
	{
		if (canClick)
		{
			NetworkManager.Instance.IsNetworkAvaliable = true;
			nowQuit = true;
			UtilBinding.ShowWaitingView(bShow: true);
			StopServer();
			StopClient();
			SceneManager.Instance.LoadLevel(1001);
		}
	}

	private void UpdateHostListView()
	{
		string[] array = new string[NetworkManager.Instance.AvailableServers.Count];
		string[] array2 = new string[NetworkManager.Instance.AvailableServers.Count];
		int num = 0;
		foreach (KeyValuePair<string, string> availableServer in NetworkManager.Instance.AvailableServers)
		{
			array[num] = availableServer.Value;
			array2[num] = availableServer.Key;
			num++;
		}
	}

	private void OnNetworkDisabled(string peerId, MsgNetworkAllDisable msg)
	{
		NetworkManager.Instance.IsNetworkAvaliable = false;
		ShowNetworkDisableAlert();
		NetworkManager.Instance.StopClient();
	}

	private void ShowNetworkDisableAlert()
	{
		string empty = string.Empty;
		if (AppMain.Instance.GetCurrentSystemLanguage() == "zh-Hans")
		{
			string text = "好";
			string text2 = "检测到设备没有开启任何网络连接。进行多人对战时，请开启蓝牙。（设置->通用->蓝牙）";
		}
		else if (AppMain.Instance.GetCurrentSystemLanguage() == "zh-Hant")
		{
			string text = "好";
			string text2 = "檢測到設備沒有開啟任何網絡連接。進行多人對戰時，請開啟藍牙。（設備->通用->藍牙）;";
		}
		else
		{
			string text = "OK";
			string text2 = "Detection indicated the device didn't access to any internet connection. For multiplayer game, please turn on your Bluetooth.(Settings -> General -> Bluetooth)";
		}
	}

	private void ShowConnectFailAlert()
	{
		Debug.Log("Connect to host failed");
		string empty = string.Empty;
		if (AppMain.Instance.GetCurrentSystemLanguage() == "zh-Hans")
		{
			string text = "好";
			string text2 = "无法加入房间，请选择其他房间。";
		}
		else if (AppMain.Instance.GetCurrentSystemLanguage() == "zh-Hant")
		{
			string text = "好";
			string text2 = "無法加入房間，請選擇其他房間。";
		}
		else
		{
			string text = "OK";
			string text2 = "You cannot enter this room, please select another room.";
		}
	}

	private void CheckEnable()
	{
		CheckEnable(0, 2001);
		CheckEnable(1, 2002);
		CheckEnable(2, 2101);
		CheckEnable(3, 2003);
		CheckEnable(4, 2201);
		CheckEnable(5, 2004);
	}

	private void CheckEnable(int index, int type)
	{
		GameObject gameObject = GameObject.Find("Panel/GameObject" + index);
		if (CharacterLockManager.Instance.ReleaseCharacter(type) != 0 && null != gameObject)
		{
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.controlIsEnabled = false;
		}
	}

	private void OnDestroy()
	{
		NetHandlerRegistry.Instance.UnregistHandler<MsgPeerBecameUnavailable>(OnPeerUnavailable);
		NetHandlerRegistry.Instance.UnregistHandler<MsgNetworkAllDisable>(OnNetworkDisabled);
		NetHandlerRegistry.Instance.UnregistHandler<MsgOnPeerConnected>(OnPeerConnected);
		NetHandlerRegistry.Instance.UnregistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
		NetHandlerRegistry.Instance.UnregistHandler<MsgCannotConnected>(OnCannotConnected);
	}
}
