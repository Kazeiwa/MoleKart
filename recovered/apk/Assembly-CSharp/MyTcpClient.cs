using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Google.ProtocolBuffers;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

internal class MyTcpClient : MonoBehaviour
{
	public enum connectStatus
	{
		noConnect = 0,
		tryConnecting = 1,
		connected = 2,
		disConnected = 3,
		connectedWorking = 5
	}

	private class Client
	{
		public TcpClient tcpClient;

		public byte[] recvBuff = new byte[61440];

		public int recvPos;

		public byte[] writeBuff = new byte[61440];

		public int writePos;
	}

	private static MyTcpClient instance;

	public static connectStatus connectState;

	private Client client;

	private float connectTime;

	public static MyTcpClient Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("MyTcpClient").AddComponent<MyTcpClient>();
				UnityEngine.Object.DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}

	public bool SendMsg(IMessageLite msg)
	{
		if (client == null)
		{
			return false;
		}
		if (client.writePos > 0)
		{
			int num = Send(client.writeBuff, client.writePos);
			if (num > 0)
			{
				client.writePos -= num;
				Array.Copy(client.writeBuff, num, client.writeBuff, 0, client.writePos);
			}
		}
		int num2 = MyTcpClientProtoCodec.Encode(msg, client.writeBuff, client.writePos);
		if (num2 > 0)
		{
			if (client.writePos <= 0)
			{
				client.writePos += num2;
				int num3 = Send(client.writeBuff, client.writePos);
				if (num3 > 0)
				{
					client.writePos -= num3;
					Array.Copy(client.writeBuff, num3, client.writeBuff, 0, client.writePos);
				}
			}
			else
			{
				client.writePos += num2;
			}
		}
		return true;
	}

	private int Send(byte[] buff, int lenth)
	{
		int num = 0;
		while (num < lenth)
		{
			try
			{
				int num2 = client.tcpClient.Client.Send(buff, num, lenth - num, SocketFlags.None);
				if (num2 > 0)
				{
					num += num2;
				}
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode != SocketError.WouldBlock)
				{
					CloseClient();
				}
				break;
			}
			catch (IOException)
			{
				CloseClient();
				break;
			}
			catch (ObjectDisposedException)
			{
				CloseClient();
				break;
			}
		}
		return num;
	}

	public void ConnectToHost(string hostIp, int hostPort)
	{
		if (connectState != connectStatus.noConnect)
		{
			return;
		}
		client = new Client();
		client.tcpClient = new TcpClient();
		try
		{
			connectState = connectStatus.tryConnecting;
			client.tcpClient.BeginConnect(hostIp, hostPort, CallBackMethod, client.tcpClient);
		}
		catch (Exception)
		{
		}
	}

	private static void CallBackMethod(IAsyncResult result)
	{
		TcpClient tcpClient = (TcpClient)result.AsyncState;
		if (tcpClient.Client != null)
		{
			tcpClient.EndConnect(result);
			tcpClient.Client.Blocking = false;
			if (connectState == connectStatus.tryConnecting)
			{
				connectState = connectStatus.connected;
			}
		}
		else if (connectState == connectStatus.tryConnecting)
		{
			connectState = connectStatus.disConnected;
		}
	}

	public void CloseClient()
	{
		connectState = connectStatus.noConnect;
		if (client != null)
		{
			if (client.tcpClient.Client != null)
			{
				client.tcpClient.Close();
			}
			client = null;
		}
	}

	private void Update()
	{
		if (connectState == connectStatus.connected)
		{
			OnConnected();
		}
		else if (connectState == connectStatus.disConnected)
		{
			OnDisconnect();
		}
		try
		{
			while (client != null && connectState == connectStatus.connectedWorking && client.tcpClient.Client != null)
			{
				int num = client.tcpClient.Client.Receive(client.recvBuff, client.recvPos, client.recvBuff.Length - client.recvPos, SocketFlags.None);
				if (num <= 0)
				{
					break;
				}
				client.recvPos += num;
				while (client.recvPos >= 4)
				{
					int network = BitConverter.ToInt32(client.recvBuff, 0);
					network = IPAddress.NetworkToHostOrder(network);
					if (network < 6 || network > 61440)
					{
						CloseClient();
						break;
					}
					if (client.recvPos >= network)
					{
						OnRecv(client.recvBuff, network);
						if (client == null)
						{
							break;
						}
						client.recvPos -= network;
						Array.Copy(client.recvBuff, network, client.recvBuff, 0, client.recvPos);
						continue;
					}
					break;
				}
			}
			if (connectState == connectStatus.connectedWorking && client != null && client.writePos > 0)
			{
				int num2 = Send(client.writeBuff, client.writePos);
				if (num2 > 0)
				{
					client.writePos -= num2;
					Array.Copy(client.writeBuff, num2, client.writeBuff, 0, client.writePos);
				}
			}
		}
		catch (SocketException ex)
		{
			if (ex.SocketErrorCode != SocketError.WouldBlock)
			{
				OnDisconnect();
			}
		}
		catch (IOException)
		{
			OnDisconnect();
		}
		catch (ObjectDisposedException)
		{
			OnDisconnect();
		}
		catch (Exception ex4)
		{
			Debug.Log(ex4);
			throw ex4;
		}
	}

	private void OnConnected()
	{
		Debug.Log("tcp connected!");
		connectState = connectStatus.connectedWorking;
		HostConnected msg = HostConnected.CreateBuilder().Build();
		NetHandlerRegistry.Instance.ProcessMsg(null, msg);
	}

	private void OnDisconnect()
	{
		Debug.Log("tcp disconnected!");
		CloseClient();
		HostDisconnected msg = HostDisconnected.CreateBuilder().Build();
		NetHandlerRegistry.Instance.ProcessMsg(null, msg);
	}

	private void OnRecv(byte[] buff, int lenth)
	{
		MsgHead msgHead = null;
		IMessageLite msg = null;
		try
		{
			MyTcpClientProtoCodec.Decode(buff, 0, lenth, ref msgHead, ref msg);
		}
		catch (Exception)
		{
		}
		if (msgHead != null && msg != null)
		{
			NetHandlerRegistry.Instance.ProcessMsg(null, msg);
		}
	}
}
