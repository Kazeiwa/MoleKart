using Google.ProtocolBuffers;

namespace TaomeeNetwork;

public interface ISessionManager
{
	void NetworkAllDisable();

	void PeerBecameAvailable(string peerID);

	void PeerBecameUnavailable(string peerID);

	void OnCannotConnected(string peerID);

	ISession OnCreateSession(string peerID);

	void OnDisconnectSession(string peerID);

	void OnReceiveBytes(string peerId, byte[] buff, int index, int recvLen);

	bool BroadCastMsgPacket(IMessageLite message, bool reliable);

	bool BroadCastMsgPacketExcept(IMessageLite message, string excludedPeerId, bool reliable);

	void EndSend();

	void EndUnrelieableSend();

	ISession FindSessionByPeerId(string peerId);

	string[] GetConnectedPeers();

	void Clear();

	void SendMonstersMoveList();

	void SendCarsMoves();
}
