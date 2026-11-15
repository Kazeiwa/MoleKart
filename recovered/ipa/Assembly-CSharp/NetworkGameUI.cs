using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class NetworkGameUI : MonoBehaviour
{
	private bool showRadar;

	private GameObject radar;

	private GameObject radarBackground;

	private GameObject radarTitle;

	private GameObject tip;

	private GameObject cantFind;

	private float rotateAngle;

	private float speed;

	private Hashtable remoteTable;

	private Dictionary<string, string> localDict;

	private bool reloadTable;

	private bool canClick;

	private float watiTime;

	private bool joinBtnPressed;

	private bool nowQuit;

	private bool canShowRooms;

	private float startWaitTime;

	private float searchWaitTime;

	private bool isNetworkAvaliable;

	private ushort selectedrole;

	public GameObject gLoading;

	private Handler<MsgPlayerJoinResp> playerjoinResp;

	public static int selectedTrack;

	public static RacingType gameMode;

	private string connectingToPeer;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	[DebuggerHidden]
	private IEnumerator NowScanServer()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void OnGUI()
	{
	}

	public void StartClient()
	{
	}

	public void StopClient()
	{
	}

	public void StartServer()
	{
	}

	public void StopServer()
	{
	}

	private void CharacterSelectedKula()
	{
	}

	private void CharacterSelectedMomo()
	{
	}

	private void CharacterSelectedRk()
	{
	}

	private void CharacterSelectedLele()
	{
	}

	private void CharacterSelectedMmt()
	{
	}

	private void CharacterSelectedFred()
	{
	}

	private void CharacterSelectedYali()
	{
	}

	private void CharacterSelectedPuti()
	{
	}

	private void CharacterSelected()
	{
	}

	private void ChangeCharacter(ushort roleId)
	{
	}

	private void SendJoinMsg2Server()
	{
	}

	public void ShowGameVersionError()
	{
	}

	public void OnPlayerJoinResp(string peerId, MsgPlayerJoinResp msg)
	{
	}

	public void Update()
	{
	}

	private void JoinButtonPressed(string peerId)
	{
	}

	public void OnPeerConnected(string peerId, MsgOnPeerConnected message)
	{
	}

	public void OnPeerDisconnected(string peerId, MsgOnPeerDisconnect message)
	{
	}

	public void OnCannotConnected(string peerId, MsgCannotConnected msg)
	{
	}

	public void OnPeerUnavailable(string peerId, MsgPeerBecameUnavailable msg)
	{
	}

	private void StartAsServer()
	{
	}

	public void GoBack()
	{
	}

	private void UpdateHostListView()
	{
	}

	private void OnNetworkDisabled(string peerId, MsgNetworkAllDisable msg)
	{
	}

	private void ShowNetworkDisableAlert()
	{
	}

	private void ShowConnectFailAlert()
	{
	}

	private void CheckEnable()
	{
	}

	private void CheckEnable(int index, int type)
	{
	}

	private void OnDestroy()
	{
	}
}
