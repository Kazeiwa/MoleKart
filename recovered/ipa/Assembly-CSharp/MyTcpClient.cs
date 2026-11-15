using System;
using System.Net.Sockets;
using Google.ProtocolBuffers;
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

		public byte[] recvBuff;

		public int recvPos;

		public byte[] writeBuff;

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
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool SendMsg(IMessageLite msg)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private int Send(byte[] buff, int lenth)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ConnectToHost(string hostIp, int hostPort)
	{
	}

	private static void CallBackMethod(IAsyncResult result)
	{
	}

	public void CloseClient()
	{
	}

	private void Update()
	{
	}

	private void OnConnected()
	{
	}

	private void OnDisconnect()
	{
	}

	private void OnRecv(byte[] buff, int lenth)
	{
	}
}
