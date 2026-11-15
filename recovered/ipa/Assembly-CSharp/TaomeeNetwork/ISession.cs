using Google.ProtocolBuffers;

namespace TaomeeNetwork;

public interface ISession
{
	bool Send(IMessageLite msg, bool available);

	void EndSend();

	void EndUnreliableSend();

	void OnRecv(byte[] package, int nLength);

	void SetPeerID(string peerID);

	string GetPeerID();

	void SetPeerName(string peerName);

	string GetPeerName();
}
