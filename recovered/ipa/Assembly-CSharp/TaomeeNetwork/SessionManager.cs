using System.Collections.Generic;
using Google.ProtocolBuffers;

namespace TaomeeNetwork;

public class SessionManager : ISessionManager
{
	public Dictionary<string, string> serverDeviceIdToPeers;

	public static long sendedSize;

	public static int recvSize;

	private Dictionary<string, ISession> sessionsList;

	public void NetworkAllDisable()
	{
	}

	public void PeerBecameAvailable(string peerID)
	{
	}

	public void PeerBecameUnavailable(string peerID)
	{
	}

	public ISession OnCreateSession(string peerID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void OnDisconnectSession(string peerID)
	{
	}

	public void OnCannotConnected(string peerID)
	{
	}

	private string GetDisplayName(string peerID, ref string deviceid)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SendMonstersMoveList()
	{
	}

	public void SendCarsMoves()
	{
	}

	public bool BroadCastMsgPacket(IMessageLite msg, bool reliable)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool BroadCastMsgPacketExcept(IMessageLite message, string excludedPeerId, bool reliable)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void EndSend()
	{
	}

	public void EndUnrelieableSend()
	{
	}

	public ISession FindSessionByPeerId(string peerId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void OnReceiveBytes(string peerId, byte[] buff, int index, int recvLen)
	{
	}

	public string[] GetConnectedPeers()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Clear()
	{
	}
}
