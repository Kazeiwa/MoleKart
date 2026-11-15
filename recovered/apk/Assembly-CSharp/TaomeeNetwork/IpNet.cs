using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Google.ProtocolBuffers;
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

		public byte[] recvBuff = new byte[6144];

		public int recvPos;

		public byte[] writeBuff = new byte[61440];

		public int writePos;
	}

	private const uint udpSendInterv = 45u;

	private string _peerId;

	private string selfIp = string.Empty;

	private UdpClient udpClient;

	private IPEndPoint udpReceivePoint;

	private int udpRecvPort = 6768;

	private UdpClient udpServer;

	private string udpBroadcastIp = "255.255.255.255";

	private string udpBroadcastIp2;

	private PeerInfo selfClient;

	private string clientPeerName;

	private TcpListener tcplistener;

	private string AnyIp = "0.0.0.0";

	private int tcpListenPort = 25000;

	public string _servername;

	private bool youServer;

	private uint circleCount;

	private static IpNet instance;

	private bool _available;

	private HashSet<Client> _tempTcpClients = new HashSet<Client>();

	private Dictionary<string, PeerInfo> serversPeersIdToInfo;

	private Dictionary<string, PeerInfo> clientsPeersIdToInfo;

	public static IpNet Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("IpNet").AddComponent<IpNet>();
				UnityEngine.Object.DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}

	public void OnUdpRecv(string peerId, UdpBroadCastServer message)
	{
	}

	private void Update()
	{
		SendAllInBuff();
		circleCount++;
		if (youServer && _peerId != null && tcplistener != null)
		{
			if (_available && circleCount % 45 == 0)
			{
				UdpBroadCastServer udpBroadCastServer = UdpBroadCastServer.CreateBuilder().SetPeerDisplayName(_servername).SetServerIp(selfIp)
					.SetServerport(tcpListenPort)
					.Build();
				byte[] array = new byte[udpBroadCastServer.SerializedSize + 70];
				int bytes = ProtoCodec.Encode(udpBroadCastServer, array, 0);
				udpServer.Send(array, bytes, udpBroadcastIp, udpRecvPort);
				if (udpBroadcastIp2 != null)
				{
					udpServer.Send(array, bytes, udpBroadcastIp2, udpRecvPort);
				}
			}
			try
			{
				TcpClient tcpClient = tcplistener.AcceptTcpClient();
				if (tcpClient != null)
				{
					Client client = new Client();
					tcpClient.Client.Blocking = false;
					client.tcpClient = tcpClient;
					_tempTcpClients.Add(client);
				}
			}
			catch (Exception)
			{
			}
			List<Client> list = new List<Client>();
			foreach (Client tempTcpClient in _tempTcpClients)
			{
				while (true)
				{
					try
					{
						int num = tempTcpClient.tcpClient.Client.Receive(tempTcpClient.recvBuff, tempTcpClient.recvPos, tempTcpClient.recvBuff.Length - tempTcpClient.recvPos, SocketFlags.None);
						if (num <= 0)
						{
							break;
						}
						tempTcpClient.recvPos += num;
						while (tempTcpClient.recvPos >= 2)
						{
							short network = BitConverter.ToInt16(tempTcpClient.recvBuff, 0);
							network = IPAddress.NetworkToHostOrder(network);
							if (network < 6 || network > 6144)
							{
								tempTcpClient.tcpClient.Close();
								break;
							}
							if (tempTcpClient.recvPos >= network)
							{
								MsgHead msgHead = null;
								IMessageLite msg = null;
								if (0 < ProtoCodec.Decode(tempTcpClient.recvBuff, 0, network, ref msgHead, ref msg) && msg.GetType().Name == "MsgOnPeerConnected")
								{
									MsgOnPeerConnected msgOnPeerConnected = msg as MsgOnPeerConnected;
									PeerInfo peerInfo = new PeerInfo();
									peerInfo.client = tempTcpClient;
									IPEndPoint iPEndPoint = tempTcpClient.tcpClient.Client.RemoteEndPoint as IPEndPoint;
									peerInfo.peerId = tempTcpClient.tcpClient.Client.RemoteEndPoint.ToString();
									peerInfo.peerName = msgOnPeerConnected.PeerDisplayName;
									peerInfo.ip = iPEndPoint.Address.ToString();
									peerInfo.port = iPEndPoint.Port;
									clientsPeersIdToInfo[peerInfo.peerId] = peerInfo;
									list.Add(tempTcpClient);
									OnPeerConnected(peerInfo.peerId);
								}
								tempTcpClient.recvPos -= network;
								Array.Copy(tempTcpClient.recvBuff, network, tempTcpClient.recvBuff, 0, tempTcpClient.recvPos);
								continue;
							}
							break;
						}
						continue;
					}
					catch (SocketException ex2)
					{
						if (ex2.SocketErrorCode != SocketError.WouldBlock)
						{
							tempTcpClient.tcpClient.Close();
							list.Add(tempTcpClient);
						}
					}
					catch (IOException)
					{
						tempTcpClient.tcpClient.Close();
						list.Add(tempTcpClient);
					}
					catch (ObjectDisposedException)
					{
						tempTcpClient.tcpClient.Close();
						list.Add(tempTcpClient);
					}
					catch
					{
					}
					break;
				}
			}
			foreach (Client item in list)
			{
				_tempTcpClients.Remove(item);
			}
			list = null;
			List<string> list2 = new List<string>();
			foreach (KeyValuePair<string, PeerInfo> item2 in clientsPeersIdToInfo)
			{
				PeerInfo value = item2.Value;
				if (value.client == null)
				{
					OnPeerDisconnect(item2.Key);
					list2.Add(item2.Key);
					continue;
				}
				Client client2 = value.client;
				try
				{
					int num2;
					while (true)
					{
						num2 = client2.tcpClient.Client.Receive(client2.recvBuff, client2.recvPos, client2.recvBuff.Length - client2.recvPos, SocketFlags.None);
						if (num2 <= 0)
						{
							break;
						}
						client2.recvPos += num2;
						while (client2.recvPos >= 2)
						{
							short network2 = BitConverter.ToInt16(client2.recvBuff, 0);
							network2 = IPAddress.NetworkToHostOrder(network2);
							if (network2 < 6 || network2 > 6144)
							{
								client2.tcpClient.Close();
								break;
							}
							if (client2.recvPos >= network2)
							{
								LocalNetFactory.Instance.LocalNet.SessionManager.OnReceiveBytes(value.peerId, client2.recvBuff, 0, network2);
								client2.recvPos -= network2;
								Array.Copy(client2.recvBuff, network2, client2.recvBuff, 0, client2.recvPos);
								continue;
							}
							break;
						}
					}
					if (num2 == 0)
					{
						OnPeerDisconnect(item2.Key);
						client2.tcpClient.Close();
						list2.Add(item2.Key);
					}
				}
				catch (SocketException ex5)
				{
					if (ex5.SocketErrorCode != SocketError.WouldBlock)
					{
						OnPeerDisconnect(item2.Key);
						client2.tcpClient.Close();
						list2.Add(item2.Key);
					}
				}
				catch (IOException)
				{
					OnPeerDisconnect(item2.Key);
					client2.tcpClient.Close();
					list2.Add(item2.Key);
				}
				catch (ObjectDisposedException)
				{
					OnPeerDisconnect(item2.Key);
					client2.tcpClient.Close();
					list2.Add(item2.Key);
				}
				catch
				{
				}
			}
			foreach (string item3 in list2)
			{
				clientsPeersIdToInfo.Remove(item3);
			}
			list2 = null;
		}
		else
		{
			if (udpClient == null)
			{
				return;
			}
			while (true)
			{
				IL_069f:
				try
				{
					while (true)
					{
						udpReceivePoint.Address = IPAddress.Any;
						udpReceivePoint.Port = 0;
						byte[] array2 = udpClient.Receive(ref udpReceivePoint);
						if (array2 == null || array2.Length <= 0)
						{
							break;
						}
						MsgHead msgHead2 = null;
						IMessageLite msg2 = null;
						if (0 >= ProtoCodec.Decode(array2, 0, array2.Length, ref msgHead2, ref msg2))
						{
							continue;
						}
						UdpBroadCastServer udpBroadCastServer2 = msg2 as UdpBroadCastServer;
						string text = udpReceivePoint.Address.ToString() + ":" + udpBroadCastServer2.Serverport;
						if (serversPeersIdToInfo.ContainsKey(text))
						{
							serversPeersIdToInfo[text].peerName = udpBroadCastServer2.PeerDisplayName;
							serversPeersIdToInfo[text].udpRecvTimes = circleCount;
						}
						else
						{
							PeerInfo peerInfo2 = new PeerInfo();
							peerInfo2.peerId = text;
							peerInfo2.peerName = udpBroadCastServer2.PeerDisplayName;
							peerInfo2.ip = udpReceivePoint.Address.ToString();
							peerInfo2.port = udpBroadCastServer2.Serverport;
							peerInfo2.udpRecvTimes = circleCount;
							serversPeersIdToInfo.Add(text, peerInfo2);
						}
						PeerBecameAvailable(text);
						goto IL_069f;
					}
				}
				catch (SocketException)
				{
				}
				catch (IOException)
				{
				}
				catch (ObjectDisposedException)
				{
				}
				catch (Exception ex11)
				{
					throw ex11;
				}
				break;
			}
			List<string> list3 = new List<string>();
			foreach (KeyValuePair<string, PeerInfo> item4 in serversPeersIdToInfo)
			{
				PeerInfo value2 = item4.Value;
				uint num3 = 0u;
				num3 = ((circleCount <= value2.udpRecvTimes) ? (value2.udpRecvTimes - circleCount) : (circleCount - value2.udpRecvTimes));
				uint num4 = 3u;
				if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
				{
					num4 = 80u;
				}
				if (num3 > 45 * num4)
				{
					value2.udpRecvTimes = circleCount;
					if (selfClient == null || item4.Key != selfClient.peerId)
					{
						PeerBecameUnavailable(value2.peerId);
						list3.Add(item4.Key);
					}
					break;
				}
			}
			foreach (string item5 in list3)
			{
				serversPeersIdToInfo.Remove(item5);
			}
			list3 = null;
			try
			{
				while (selfClient != null)
				{
					Client client3 = selfClient.client;
					if (client3.tcpClient.Client == null)
					{
						OnPeerDisconnect(selfClient.peerId);
						selfClient.client.tcpClient.Close();
						serversPeersIdToInfo.Remove(selfClient.peerId);
						break;
					}
					int num5 = client3.tcpClient.Client.Receive(client3.recvBuff, client3.recvPos, client3.recvBuff.Length - client3.recvPos, SocketFlags.None);
					if (num5 <= 0)
					{
						break;
					}
					client3.recvPos += num5;
					while (client3.recvPos >= 2)
					{
						short network3 = BitConverter.ToInt16(client3.recvBuff, 0);
						network3 = IPAddress.NetworkToHostOrder(network3);
						if (network3 < 6 || network3 > 6144)
						{
							selfClient.client.tcpClient.Close();
							break;
						}
						if (client3.recvPos >= network3)
						{
							LocalNetFactory.Instance.LocalNet.SessionManager.OnReceiveBytes(selfClient.peerId, client3.recvBuff, 0, network3);
							client3.recvPos -= network3;
							Array.Copy(client3.recvBuff, network3, client3.recvBuff, 0, client3.recvPos);
							continue;
						}
						break;
					}
				}
			}
			catch (SocketException)
			{
			}
			catch (IOException)
			{
				OnPeerDisconnect(selfClient.peerId);
				selfClient.client.tcpClient.Close();
				serversPeersIdToInfo.Remove(selfClient.peerId);
			}
			catch (ObjectDisposedException)
			{
				OnPeerDisconnect(selfClient.peerId);
				selfClient.client.tcpClient.Close();
				serversPeersIdToInfo.Remove(selfClient.peerId);
			}
			catch (Exception ex15)
			{
				Debug.Log(ex15);
				throw ex15;
			}
		}
	}

	public void StartServer(string serverName)
	{
		StopServer();
		StopClient();
		clientsPeersIdToInfo = new Dictionary<string, PeerInfo>();
		_peerId = serverName + ":" + tcpListenPort;
		tcplistener = new TcpListener(IPAddress.Parse(AnyIp), tcpListenPort);
		tcplistener.Server.Blocking = false;
		tcplistener.Start();
		_servername = serverName;
		udpServer = new UdpClient();
		udpServer.Client.Blocking = false;
		udpServer.EnableBroadcast = true;
		int num = 0;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		NetworkInterface[] array = allNetworkInterfaces;
		foreach (NetworkInterface networkInterface in array)
		{
			if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
			{
				continue;
			}
			IPInterfaceProperties iPProperties = networkInterface.GetIPProperties();
			foreach (UnicastIPAddressInformation unicastAddress in iPProperties.UnicastAddresses)
			{
				if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
				{
					IPAddress iPAddress = IPAddress.Parse("255.255.255.0");
					long addr = (iPAddress.Address & unicastAddress.Address.Address) | ~iPAddress.Address;
					IPAddress iPAddress2 = new IPAddress(addr);
					switch (num)
					{
					case 0:
						udpBroadcastIp = iPAddress2.ToString();
						break;
					case 1:
						udpBroadcastIp2 = iPAddress2.ToString();
						break;
					}
					num++;
				}
			}
		}
		UdpBroadCastServer udpBroadCastServer = UdpBroadCastServer.CreateBuilder().SetPeerDisplayName(_servername).SetServerIp(selfIp)
			.SetServerport(tcpListenPort)
			.Build();
		byte[] array2 = new byte[udpBroadCastServer.SerializedSize + 70];
		int num2 = ProtoCodec.Encode(udpBroadCastServer, array2, 0);
		if (num2 > 0)
		{
			udpServer.Send(array2, num2, udpBroadcastIp, udpRecvPort);
			if (udpBroadcastIp2 != null)
			{
				udpServer.Send(array2, num2, udpBroadcastIp2, udpRecvPort);
			}
		}
		_available = true;
		youServer = true;
	}

	public void StopServer()
	{
		_available = false;
		udpBroadcastIp2 = null;
		if (udpServer != null)
		{
			udpServer.Close();
			udpServer = null;
		}
		_peerId = null;
		_servername = null;
		youServer = false;
		if (tcplistener != null)
		{
			tcplistener.Stop();
			tcplistener = null;
		}
		if (clientsPeersIdToInfo != null)
		{
			clientsPeersIdToInfo.Clear();
			clientsPeersIdToInfo = null;
		}
		_tempTcpClients.Clear();
		clientPeerName = null;
	}

	public void StartClient(string displayName)
	{
		StopServer();
		StopClient();
		serversPeersIdToInfo = new Dictionary<string, PeerInfo>();
		clientPeerName = displayName;
		udpClient = new UdpClient(udpRecvPort);
		udpClient.EnableBroadcast = true;
		udpClient.Client.Blocking = false;
		udpReceivePoint = new IPEndPoint(IPAddress.Parse(AnyIp), 0);
		NetHandlerRegistry.Instance.RegistHandler<UdpBroadCastServer>(OnUdpRecv);
	}

	public void StopClient()
	{
		NetHandlerRegistry.Instance.UnregistHandler<UdpBroadCastServer>(OnUdpRecv);
		if (serversPeersIdToInfo != null)
		{
			serversPeersIdToInfo.Clear();
			serversPeersIdToInfo = null;
		}
		_peerId = null;
		if (udpClient != null)
		{
			udpClient.Close();
			udpClient = null;
		}
		if (selfClient != null)
		{
			selfClient.client.tcpClient.Close();
			selfClient = null;
		}
		udpReceivePoint = null;
	}

	public void SetServerAvailable(bool available)
	{
		_available = available;
	}

	private void PeerBecameAvailable(string peerId)
	{
		LocalNetFactory.Instance.LocalNet.SessionManager.PeerBecameAvailable(peerId);
	}

	private void PeerBecameUnavailable(string peerId)
	{
		LocalNetFactory.Instance.LocalNet.SessionManager.PeerBecameUnavailable(peerId);
	}

	public void OnPeerConnected(string peerId)
	{
		LocalNetFactory.Instance.LocalNet.SessionManager.OnCreateSession(peerId);
	}

	public void OnPeerDisconnect(string peerId)
	{
		LocalNetFactory.Instance.LocalNet.SessionManager.OnDisconnectSession(peerId);
	}

	private void SendAllInBuff()
	{
		if (youServer)
		{
			foreach (KeyValuePair<string, PeerInfo> item in clientsPeersIdToInfo)
			{
				PeerInfo value = item.Value;
				if (value.client != null)
				{
					while (value.client.writePos > 0)
					{
						try
						{
							int num = 0;
							num = value.client.tcpClient.Client.Send(value.client.writeBuff, 0, value.client.writePos, SocketFlags.None);
							if (num > 0)
							{
								value.client.writePos -= num;
								Array.Copy(value.client.writeBuff, num, value.client.writeBuff, 0, value.client.writePos);
							}
						}
						catch (SocketException ex)
						{
							if (ex.SocketErrorCode != SocketError.WouldBlock)
							{
								value.client = null;
								Debug.Log(ex);
							}
							break;
						}
						catch (IOException message)
						{
							value.client = null;
							Debug.Log(message);
							break;
						}
						catch (ObjectDisposedException message2)
						{
							value.client = null;
							Debug.Log(message2);
							break;
						}
					}
				}
			}
			return;
		}
		if (selfClient == null)
		{
			return;
		}
		PeerInfo peerInfo = selfClient;
		if (peerInfo.client == null)
		{
			return;
		}
		while (peerInfo.client.writePos > 0)
		{
			try
			{
				int num2 = 0;
				num2 = peerInfo.client.tcpClient.Client.Send(peerInfo.client.writeBuff, 0, peerInfo.client.writePos, SocketFlags.None);
				if (num2 > 0)
				{
					peerInfo.client.writePos -= num2;
					Array.Copy(peerInfo.client.writeBuff, num2, peerInfo.client.writeBuff, 0, peerInfo.client.writePos);
				}
			}
			catch (SocketException ex2)
			{
				if (ex2.SocketErrorCode != SocketError.WouldBlock)
				{
					peerInfo.client = null;
					Debug.Log(ex2);
				}
				break;
			}
			catch (IOException message3)
			{
				peerInfo.client = null;
				Debug.Log(message3);
				break;
			}
			catch (ObjectDisposedException message4)
			{
				peerInfo.client = null;
				Debug.Log(message4);
				break;
			}
		}
	}

	public void Send(string peerId, byte[] buff, int lenth)
	{
		if (youServer)
		{
			PeerInfo peerInfo = clientsPeersIdToInfo[peerId];
			if (peerInfo == null || peerInfo.client == null)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			while (num < lenth)
			{
				try
				{
					num2 = 0;
					num2 = peerInfo.client.tcpClient.Client.Send(buff, 0, lenth, SocketFlags.None);
					if (num2 > 0)
					{
						num += num2;
					}
				}
				catch (SocketException ex)
				{
					if (ex.SocketErrorCode != SocketError.WouldBlock)
					{
						Debug.Log(ex);
					}
					else if (peerInfo.client.writeBuff.Length - peerInfo.client.writePos >= lenth - num)
					{
						Array.Copy(buff, num, peerInfo.client.writeBuff, peerInfo.client.writePos, lenth - num);
						peerInfo.client.writePos += lenth - num;
					}
					else
					{
						Debug.LogError("!!! wouldBlock writenLen =" + num);
						peerInfo.client.writePos = 0;
					}
					break;
				}
				catch (IOException message)
				{
					Debug.Log(message);
					break;
				}
				catch (ObjectDisposedException message2)
				{
					Debug.Log(message2);
					break;
				}
			}
			return;
		}
		PeerInfo peerInfo2 = selfClient;
		if (peerInfo2 == null || peerInfo2.client == null || peerInfo2.client.tcpClient == null)
		{
			return;
		}
		int num3 = 0;
		int num4 = 0;
		while (num3 < lenth)
		{
			try
			{
				num4 = 0;
				num4 = peerInfo2.client.tcpClient.Client.Send(buff, 0, lenth, SocketFlags.None);
				if (num4 > 0)
				{
					num3 += num4;
				}
			}
			catch (SocketException ex2)
			{
				if (ex2.SocketErrorCode != SocketError.WouldBlock)
				{
					Debug.Log(ex2);
				}
				else if (peerInfo2.client.writeBuff.Length - peerInfo2.client.writePos >= lenth - num3)
				{
					Array.Copy(buff, num3, peerInfo2.client.writeBuff, peerInfo2.client.writePos, lenth - num3);
					peerInfo2.client.writePos += lenth - num3;
				}
				else
				{
					Debug.LogError("!!! wouldBlock writenLen =" + num3);
					peerInfo2.client.writePos = 0;
				}
				break;
			}
			catch (IOException message3)
			{
				Debug.Log(message3);
				break;
			}
			catch (ObjectDisposedException message4)
			{
				Debug.Log(message4);
				break;
			}
		}
	}

	public void SendRawBytesToPeers(string[] peerIds, byte[] bytes, int lenth)
	{
		if (youServer)
		{
			for (int i = 0; i < peerIds.Length; i++)
			{
				PeerInfo peerInfo = clientsPeersIdToInfo[peerIds[i]];
				if (peerInfo == null || peerInfo.client == null)
				{
					continue;
				}
				int num = 0;
				int num2 = 0;
				while (num < lenth)
				{
					try
					{
						num2 = 0;
						num2 = peerInfo.client.tcpClient.Client.Send(bytes, 0, lenth, SocketFlags.None);
						if (num2 > 0)
						{
							num += num2;
						}
					}
					catch (SocketException ex)
					{
						if (ex.SocketErrorCode != SocketError.WouldBlock)
						{
							Debug.Log(ex);
						}
						else if (peerInfo.client.writeBuff.Length - peerInfo.client.writePos >= lenth - num)
						{
							Array.Copy(bytes, num, peerInfo.client.writeBuff, peerInfo.client.writePos, lenth - num);
							peerInfo.client.writePos += lenth - num;
						}
						else
						{
							Debug.LogError("!!! wouldBlock writenLen =" + num);
							peerInfo.client.writePos = 0;
						}
						break;
					}
					catch (IOException message)
					{
						Debug.Log(message);
						break;
					}
					catch (ObjectDisposedException message2)
					{
						Debug.Log(message2);
						break;
					}
				}
			}
			return;
		}
		PeerInfo peerInfo2 = selfClient;
		if (peerInfo2 == null || peerInfo2.client == null || peerInfo2.client.tcpClient == null)
		{
			return;
		}
		int num3 = 0;
		int num4 = 0;
		while (num3 < lenth)
		{
			try
			{
				num4 = 0;
				num4 = peerInfo2.client.tcpClient.Client.Send(bytes, 0, lenth, SocketFlags.None);
				if (num4 > 0)
				{
					num3 += num4;
				}
			}
			catch (SocketException ex2)
			{
				if (ex2.SocketErrorCode != SocketError.WouldBlock)
				{
					Debug.Log(ex2);
				}
				else if (peerInfo2.client.writeBuff.Length - peerInfo2.client.writePos >= lenth - num3)
				{
					Array.Copy(bytes, num3, peerInfo2.client.writeBuff, peerInfo2.client.writePos, lenth - num3);
					peerInfo2.client.writePos += lenth - num3;
				}
				else
				{
					Debug.LogError("!!! wouldBlock writenLen =" + num3);
					peerInfo2.client.writePos = 0;
				}
				break;
			}
			catch (IOException message3)
			{
				Debug.Log(message3);
				break;
			}
			catch (ObjectDisposedException message4)
			{
				Debug.Log(message4);
				break;
			}
		}
	}

	public string GetMyselfPeerId()
	{
		return _peerId;
	}

	public void ConnectToServer(string serverPeerId)
	{
		PeerInfo peerInfo = serversPeersIdToInfo[serverPeerId];
		if (peerInfo != null && (selfClient == null || selfClient.client == null || selfClient.client.tcpClient == null))
		{
			selfClient = peerInfo;
			selfClient.client = new Client();
			selfClient.client.tcpClient = new TcpClient();
			try
			{
				selfClient.client.tcpClient.Connect(IPAddress.Parse(peerInfo.ip), peerInfo.port);
			}
			catch
			{
				selfClient.client.tcpClient = null;
				selfClient.client = null;
				selfClient = null;
			}
			if (selfClient.client.tcpClient != null)
			{
				_peerId = selfClient.client.tcpClient.Client.LocalEndPoint.ToString();
				Debug.Log("myPeerId:[" + _peerId + "]");
				MsgOnPeerConnected msgOnPeerConnected = MsgOnPeerConnected.CreateBuilder().SetPeerDisplayName(clientPeerName).Build();
				byte[] array = new byte[msgOnPeerConnected.SerializedSize + 70];
				int size = ProtoCodec.Encode(msgOnPeerConnected, array, 0);
				int num = selfClient.client.tcpClient.Client.Send(array, size, SocketFlags.None);
				selfClient.client.tcpClient.Client.Blocking = false;
				OnPeerConnected(selfClient.peerId);
			}
		}
	}

	public string GetDisplayName(string peerId)
	{
		if (youServer)
		{
			return clientsPeersIdToInfo[peerId].peerName;
		}
		return serversPeersIdToInfo[peerId].peerName;
	}
}
