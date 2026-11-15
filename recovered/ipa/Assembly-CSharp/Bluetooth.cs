using System.Collections.Generic;
using TaomeeNetwork;
using UnityEngine;

internal class Bluetooth : MonoBehaviour
{
	private class SendAndRecv
	{
		public uint sendCount;

		public uint recvCount;
	}

	private static Bluetooth instance;

	private NetMod netMod;

	private uint circleCount;

	private uint sendCount;

	private byte[] heartSendBuff;

	private string[] updatePeers;

	private uint recvCircle;

	private List<string> listPeers;

	private List<string> disconnectListPeers;

	private bool connected;

	private bool serverAvailable;

	private bool isConnecting;

	private static string serverPeerId;

	private static Dictionary<string, SendAndRecv> peersRecvCounts;

	public static Bluetooth Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	~Bluetooth()
	{
	}

	public void Init()
	{
	}

	public void Uninit()
	{
	}

	public void NetworkNotAvailable()
	{
	}

	public bool StartClient(string gameName, string displayName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void StopClient()
	{
	}

	public bool StartServer(string gameName, string displayName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void StopServer()
	{
	}

	private void Update()
	{
	}

	public void SetServerAvailable(bool available)
	{
	}

	private void ReceivedConnectionRequestFromPeer(string peerId)
	{
	}

	public void OnAcceptConnectionWithPeerFailed(string peerId)
	{
	}

	private void PeerBecameAvailable(string peerId)
	{
	}

	private void PeerBecameUnavailable(string peerId)
	{
	}

	private void InvalidateSession()
	{
	}

	public string GetPeerDisplayName(string peerId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public string GetMyselfPeerId()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void OnPeerConnected(string peerId)
	{
	}

	public void OnPeerDisconnect(string peerId)
	{
	}

	public void ConnectToPeer(string peerId)
	{
	}

	public void SetSendHeartbeat(bool isSend)
	{
	}

	public void SendRawBytesToPeers(string[] peerIds, byte[] bytes, int lenth, bool realiable)
	{
	}

	public static void Receiv(string peerId, byte[] bytes)
	{
	}
}
