using System.Collections;
using System.Collections.Generic;
using FrameWork;
using GameNetwork;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class ClientRoomUI : MonoBehaviour
{
	private const string characterName = "ShowCharacter";

	private const string carName = "ShowCar";

	public static string serverPeerId;

	private int selectedTrack = 1100;

	private RacingType gameMode = RacingType.OnlinePropMode;

	private Dictionary<int, Vector3> rolePos = new Dictionary<int, Vector3>();

	private Dictionary<int, Quaternion> roleRotation = new Dictionary<int, Quaternion>();

	private Dictionary<int, Vector3> roleScale = new Dictionary<int, Vector3>();

	private Dictionary<int, Vector3> carPos = new Dictionary<int, Vector3>();

	private Dictionary<int, Quaternion> carRotation = new Dictionary<int, Quaternion>();

	private Dictionary<int, Vector3> carScale = new Dictionary<int, Vector3>();

	private bool canGoBack = true;

	private Handler<MsgOnPeerDisconnect> peerDisconnectedHandler;

	private int roleSound;

	private bool m_SendCheck;

	private float m_SendCheckTime;

	private bool m_ReceivedCheck;

	private float m_DeltaTime;

	private void Start()
	{
		SceneUIManager.Instance.m_CurStrings = null;
		NetworkManager.Instance.SetSendHeartbeat(isSend: true);
		NetworkManager.Instance.notDealPeerDisconnected = true;
		if (!SoundManager.Instance.IsPlaying(60002))
		{
			SoundManager.Instance.PlaySoundEx(60002);
		}
		Debug.LogWarning("is server:" + AppMain.Instance.IsServer);
		Debug.Log("Client On Start");
		canGoBack = true;
		if (null != SceneUIManager.Instance.m_Loading)
		{
			SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: false);
			UtilBinding.ShowLoadingIndicator(bShow: false);
		}
		NetHandlerRegistry.Instance.RegistHandler<MsgStartGame>(OnReceiveStartGame);
		NetHandlerRegistry.Instance.RegistHandler<MsgNotifyPlayerList>(OnNotifyPlayerList);
		NetHandlerRegistry.Instance.RegistHandler<MsgHostLeave>(OnHostLeave);
		NetHandlerRegistry.Instance.RegistHandler<MsgPlayerLeave>(OnOtherClientLeave);
		NetHandlerRegistry.Instance.RegistHandler<MsgTrackAndModeSelected>(OnTrackAndModeSelected);
		NetHandlerRegistry.Instance.RegistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
		NetHandlerRegistry.Instance.RegistHandler<MsgIntoBackground>(OnOtherIntoBackground);
		NetHandlerRegistry.Instance.RegistHandler<MsgCheckLink>(AckLink);
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(new MsgRequestPlayerList(), reliable: true);
		InitCarDisp();
		ChangeCarModel();
		MsgClientJoined.Builder builder = MsgClientJoined.CreateBuilder();
		MsgClientJoined message = builder.Build();
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
		selectedTrack = NetworkGameUI.selectedTrack;
		gameMode = NetworkGameUI.gameMode;
	}

	protected void OnDestroy()
	{
		Debug.Log("Client On Destroy");
		NetHandlerRegistry.Instance.UnregistHandler<MsgStartGame>(OnReceiveStartGame);
		NetHandlerRegistry.Instance.UnregistHandler<MsgNotifyPlayerList>(OnNotifyPlayerList);
		NetHandlerRegistry.Instance.UnregistHandler<MsgHostLeave>(OnHostLeave);
		NetHandlerRegistry.Instance.UnregistHandler<MsgPlayerLeave>(OnOtherClientLeave);
		NetHandlerRegistry.Instance.UnregistHandler<MsgTrackAndModeSelected>(OnTrackAndModeSelected);
		NetHandlerRegistry.Instance.UnregistHandler<MsgIntoBackground>(OnOtherIntoBackground);
		NetHandlerRegistry.Instance.UnregistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
		NetHandlerRegistry.Instance.UnregistHandler<MsgCheckLink>(AckLink);
	}

	private void OnReceiveStartGame(string peerId, MsgStartGame message)
	{
		FlurryBinding.ReportPlayOnlineGame();
		canGoBack = false;
		SoundManager.Instance.StopSound(roleSound);
		gameMode = (RacingType)message.GameMode;
		selectedTrack = message.TrackId;
		StartGame();
	}

	private void StartGame()
	{
		NetworkManager.Instance.SetSendHeartbeat(isSend: false);
		Debug.Log("ClientRoomUi : StartGame");
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule)
		{
			raceLogicModule.SetCurrentRaceType(gameMode);
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

	private void Update()
	{
		m_DeltaTime += Time.deltaTime;
		if (AppMain.Instance.IsClient && !m_SendCheck && m_DeltaTime > 2.5f)
		{
			MsgCheckLink.Builder builder = new MsgCheckLink.Builder();
			builder.Received = 1;
			MsgCheckLink message = builder.Build();
			NetworkManager.Instance.BroadCastMessage(message, reliable: true);
			m_SendCheck = true;
			m_SendCheckTime = Time.time;
		}
		if (AppMain.Instance.IsClient && m_SendCheck && Time.time - m_SendCheckTime > 1.5f && !m_ReceivedCheck)
		{
			GoBack();
		}
	}

	private void OnNotifyPlayerList(string peerId, MsgNotifyPlayerList players)
	{
		Debug.Log("OnReceiveNotifyPlayerList");
		ArrayList playerDataList = GameDataManager.Instance.GetPlayerDataList();
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < players.PlayerListCount; i++)
		{
			MsgPlayer playerList = players.GetPlayerList(i);
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
				PlayerData playerData2 = new PlayerData();
				playerData2.PlayerId = (uint)playerList.PlayerId;
				playerData2.role = (RoleType)playerList.RoleType;
				playerData2.name = playerList.Name;
				playerData2.carType = (CarModeType)playerList.CarType;
				playerData2.StartPositionId = playerList.StartPosition;
				playerData2.peerID = playerList.PeerId;
				arrayList.Add(playerData2);
			}
		}
		GameDataManager.Instance.SetPlayerDataList(arrayList);
		UpdatePlayerListView();
	}

	private void OnPeerDisconnected(string peerId, MsgOnPeerDisconnect message)
	{
		Debug.Log("CLIENT On PEER disconnected serverPeerId:" + serverPeerId);
		Debug.Log("Ondisconnected peerid:" + peerId);
		if (peerId == serverPeerId)
		{
			Debug.Log("SERVER DISCONNECTED");
			GoBack();
		}
		else
		{
			UpdatePlayerListView();
		}
	}

	private void OnOtherClientLeave(string peerId, MsgPlayerLeave msg)
	{
		Debug.Log("Other Client Leave");
		GameDataManager.Instance.RemovePlayer(msg.PeerId);
		UpdatePlayerListView();
	}

	private void OnHostLeave(string peerId, MsgHostLeave msg)
	{
		Debug.Log("Receive Host leave Message");
		AppMain.Instance.onCallBack = AppMain.Instance.ShowHostLeaveAlert;
		AppMain.Instance.showAlert = true;
		GoBack();
	}

	private void OnTrackAndModeSelected(string peerId, MsgTrackAndModeSelected msg)
	{
		selectedTrack = msg.TrackId;
		gameMode = (RacingType)msg.GameMode;
		ChangeTrackAndModePreview();
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
		if (gameMode == RacingType.OnlinePropMode)
		{
			GameObject.Find("Mode").transform.FindChild("propMode").gameObject.active = true;
			GameObject.Find("Mode").transform.FindChild("timingMode").gameObject.active = false;
		}
		else
		{
			GameObject.Find("Mode").transform.FindChild("propMode").gameObject.active = false;
			GameObject.Find("Mode").transform.FindChild("timingMode").gameObject.active = true;
		}
	}

	public void ChangeCarModel()
	{
		int carId = LocalUserInfo.Instance.UserInfo.CarId;
		int userId = (int)LocalUserInfo.Instance.UserInfo.UserId;
		SelectedRole player = TSingleton<SelectedRoleFactory>.Instance.CreateSelectedRole((RoleType)userId, rolePos[userId], roleRotation[userId], roleScale[userId], "ShowCharacter");
		player.transform.parent = GameObject.Find("PlayerAndCar").transform;
		player.transform.localPosition = rolePos[userId];
		if (userId == 3004)
		{
			PlayKulaFire(ref player);
		}
		PlayCharacterAnimation(player);
		switch ((RoleType)userId)
		{
		case RoleType.EMomo:
			roleSound = 30101;
			break;
		case RoleType.ERk:
			roleSound = 30201;
			break;
		case RoleType.EBear:
			roleSound = 30301;
			break;
		case RoleType.EMmt:
			roleSound = 30405;
			break;
		case RoleType.EMolele:
			roleSound = 30501;
			break;
		case RoleType.EKuLa:
			roleSound = 30601;
			break;
		}
		SoundManager.Instance.PlaySound(roleSound);
		GameObject gameObject = TSingleton<CarManagerSelected>.Instance.CreateCar(0, "ShowCar", carPos[carId], carRotation[carId], carScale[carId], (CarModeType)carId);
		if (carId != 2002)
		{
			gameObject.transform.parent = GameObject.Find("PlayerAndCar").transform;
			gameObject.transform.localPosition = carPos[carId];
			return;
		}
		GameObject gameObject2 = GameObject.Find("CarObject");
		gameObject.transform.parent = gameObject2.transform;
		gameObject2.transform.localPosition = carPos[carId];
		gameObject2.transform.localScale = carScale[carId];
		gameObject2.transform.localRotation = carRotation[carId];
	}

	private void InitCarDisp()
	{
		rolePos.Add(3001, new Vector3(-330f, -324f, 741f));
		rolePos.Add(3002, new Vector3(-127f, -255f, 680f));
		rolePos.Add(3101, new Vector3(-336f, -294f, 1138f));
		rolePos.Add(3003, new Vector3(-315f, -284f, 1031f));
		rolePos.Add(3201, new Vector3(-355f, -167f, 1096f));
		rolePos.Add(3004, new Vector3(-293f, -277f, 601f));
		roleRotation.Add(3001, Quaternion.Euler(351f, 172f, 1f));
		roleRotation.Add(3002, Quaternion.Euler(0f, 179f, 0f));
		roleRotation.Add(3101, Quaternion.Euler(0f, 174f, 0f));
		roleRotation.Add(3003, Quaternion.Euler(0f, 164f, 0f));
		roleRotation.Add(3201, Quaternion.Euler(357f, 175f, 2f));
		roleRotation.Add(3004, Quaternion.Euler(355f, 156f, 3f));
		roleScale.Add(3001, new Vector3(200f, 200f, 200f));
		roleScale.Add(3002, new Vector3(200f, 200f, 200f));
		roleScale.Add(3101, new Vector3(200f, 200f, 200f));
		roleScale.Add(3003, new Vector3(200f, 200f, 200f));
		roleScale.Add(3201, new Vector3(200f, 200f, 200f));
		roleScale.Add(3004, new Vector3(200f, 200f, 200f));
		carScale.Add(2001, new Vector3(200f, 200f, 200f));
		carScale.Add(2002, new Vector3(200f, 200f, 200f));
		carScale.Add(2101, new Vector3(200f, 200f, 200f));
		carScale.Add(2003, new Vector3(200f, 200f, 200f));
		carScale.Add(2201, new Vector3(200f, 200f, 200f));
		carScale.Add(2004, new Vector3(200f, 200f, 200f));
		carPos.Add(2001, new Vector3(-181f, -381f, 908f));
		carPos.Add(2002, new Vector3(-272f, -267f, 807f));
		carPos.Add(2101, new Vector3(-114f, -363f, 852f));
		carPos.Add(2003, new Vector3(-209f, -331f, 943f));
		carPos.Add(2201, new Vector3(-367f, -475f, 1066f));
		carPos.Add(2004, new Vector3(-198f, -312f, 830f));
		carRotation.Add(2001, Quaternion.Euler(354.0806f, 157.6994f, 0.8957303f));
		carRotation.Add(2002, Quaternion.Euler(4f, 183f, 351f));
		carRotation.Add(2101, Quaternion.Euler(270f, 170f, 0f));
		carRotation.Add(2003, Quaternion.Euler(270f, 195f, 0f));
		carRotation.Add(2201, Quaternion.Euler(275f, 356f, 190f));
		carRotation.Add(2004, Quaternion.Euler(273f, 66f, 108f));
	}

	private void PlayKulaFire(ref SelectedRole player)
	{
		GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.KULA_FIRE, isPause: true, isOneShot: false);
		Effect component = gameObject.GetComponent<Effect>();
		component.AttachTo(Utility.FindChildTranform(player.transform, "effect02"), new Vector3(-0.1036772f, -0.04123587f, 0.0795649f), new Vector3(0f, 0f, 0f));
		component.SetLifeTime(3f);
		component.doReset();
		GameObject gameObject2 = EffectManager.Instance.CreateEffect(eEffectType.KULA_FIRE_2, isPause: false, isOneShot: false);
		Effect component2 = gameObject2.GetComponent<Effect>();
		component2.transform.localScale = new Vector3(200f, 200f, 200f);
		component2.AttachTo(Utility.FindChildTranform(player.transform, "effect01"), new Vector3(0.02189802f, 0.2715608f, -0.02124882f), new Vector3(0f, 0f, 0f));
		component2.doReset();
	}

	public void PlayCharacterAnimation(SelectedRole player)
	{
		player.m_RoleModel.AddComponent<PlayAnimationList>();
		PlayAnimationList component = player.m_RoleModel.GetComponent<PlayAnimationList>();
		component.ObjectToPlay = player.m_RoleModel;
		if ((bool)player.animation.GetClip("stan") && (bool)player.animation.GetClip("sele"))
		{
			component.PlayAnimation("sele", "stan");
		}
		else
		{
			component.PlayAnimation("sele01", "sele02");
		}
	}

	private void UpdatePlayerListView()
	{
		Debug.Log("UpdateClientPlayerListView!!!");
	}

	private void OnOtherIntoBackground(string peerId, MsgIntoBackground message)
	{
		LocalNetFactory.Instance.LocalNet.StopClient();
		SceneManager.Instance.LoadLevel(1005);
	}

	private void AckLink(string peerId, MsgCheckLink message)
	{
		Debug.Log("----------Clent receive ack link pakage---------");
		m_ReceivedCheck = true;
	}

	private void GoBack()
	{
		if (canGoBack)
		{
			NetworkManager.Instance.notDealPeerDisconnected = true;
			SoundManager.Instance.StopSound(roleSound);
			UtilBinding.ShowWaitingView(bShow: true);
			Debug.Log("Client Back to Lobby");
			NetworkManager.Instance.StopClient();
			SceneManager.Instance.LoadLevel(1005);
		}
	}
}
