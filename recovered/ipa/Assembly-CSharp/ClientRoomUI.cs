using System.Collections.Generic;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class ClientRoomUI : MonoBehaviour
{
	private const string characterName = "ShowCharacter";

	private const string carName = "ShowCar";

	public static string serverPeerId;

	private int selectedTrack;

	private RacingType gameMode;

	private Dictionary<int, Vector3> rolePos;

	private Dictionary<int, Quaternion> roleRotation;

	private Dictionary<int, Vector3> roleScale;

	private Dictionary<int, Vector3> carPos;

	private Dictionary<int, Quaternion> carRotation;

	private Dictionary<int, Vector3> carScale;

	private bool canGoBack;

	private Handler<MsgOnPeerDisconnect> peerDisconnectedHandler;

	private int roleSound;

	private bool canShowPlayerList;

	private bool m_SendCheck;

	private float m_SendCheckTime;

	private bool m_ReceivedCheck;

	private float m_DeltaTime;

	private void Start()
	{
	}

	protected void OnDestroy()
	{
	}

	private void OnReceiveStartGame(string peerId, MsgStartGame message)
	{
	}

	private void StartGame()
	{
	}

	private void Update()
	{
	}

	private void OnNotifyPlayerList(string peerId, MsgNotifyPlayerList players)
	{
	}

	private void OnPeerDisconnected(string peerId, MsgOnPeerDisconnect message)
	{
	}

	private void OnOtherClientLeave(string peerId, MsgPlayerLeave msg)
	{
	}

	private void OnHostLeave(string peerId, MsgHostLeave msg)
	{
	}

	private void OnTrackAndModeSelected(string peerId, MsgTrackAndModeSelected msg)
	{
	}

	private void ChangeTrackAndModePreview()
	{
	}

	public void ChangeCarModel()
	{
	}

	private void InitCarDisp()
	{
	}

	private void PlayKulaFire(ref SelectedRole player)
	{
	}

	private void PlayYaliFire(ref SelectedRole player)
	{
	}

	public void PlayCharacterAnimation(SelectedRole player)
	{
	}

	private void UpdatePlayerListView()
	{
	}

	private void OnOtherIntoBackground(string peerId, MsgIntoBackground message)
	{
	}

	private void AckLink(string peerId, MsgCheckLink message)
	{
	}

	private void GoBack()
	{
	}
}
