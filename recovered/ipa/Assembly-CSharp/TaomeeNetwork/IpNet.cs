using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using RaceProtocol;
using UnityEngine;

namespace TaomeeNetwork;

internal class IpNet : MonoBehaviour
{
	private class PeerInfo
	{
		public string peerId;

		public string peerName;

		public string ip;

		public int port;

		public uint udpRecvTimes;

		public Client client;
	}

	private class Client
	{
		public TcpClient tcpClient;

		public byte[] recvBuff;

		public int recvPos;

		public byte[] writeBuff;

		public int writePos;
	}

	private const uint udpSendInterv = 45u;

	private string _peerId;

	private string selfIp;

	private UdpClient udpClient;

	private IPEndPoint udpReceivePoint;

	private int udpRecvPort;

	private UdpClient udpServer;

	private string udpBroadcastIp;

	private string udpBroadcastIp2;

	private PeerInfo selfClient;

	private string clientPeerName;

	private TcpListener tcplistener;

	private string AnyIp;

	private int tcpListenPort;

	public string _servername;

	private bool youServer;

	private uint circleCount;

	private static IpNet instance;

	private bool _available;

	private HashSet<Client> _tempTcpClients;

	private Dictionary<string, PeerInfo> serversPeersIdToInfo;

	private Dictionary<string, PeerInfo> clientsPeersIdToInfo;

	public static IpNet Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void OnUdpRecv(string peerId, UdpBroadCastServer message)
	{
	}

	private void Update()
	{
	}

	public void StartServer(string serverName)
	{
	}

	public void StopServer()
	{
	}

	public void StartClient(string displayName)
	{
	}

	public void StopClient()
	{
	}

	public void SetServerAvailable(bool available)
	{
	}

	private void PeerBecameAvailable(string peerId)
	{
	}

	private void PeerBecameUnavailable(string peerId)
	{
	}

	public void OnPeerConnected(string peerId)
	{
	}

	public void OnPeerDisconnect(string peerId)
	{
	}

	private void SendAllInBuff()
	{
	}

	public void Send(string peerId, byte[] buff, int lenth)
	{
	}

	public void SendRawBytesToPeers(string[] peerIds, byte[] bytes, int lenth)
	{
	}

	public string GetMyselfPeerId()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ConnectToServer(string serverPeerId)
	{
	}

	public string GetDisplayName(string peerId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
