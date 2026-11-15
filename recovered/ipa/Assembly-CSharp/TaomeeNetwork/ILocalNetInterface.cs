namespace TaomeeNetwork;

public interface ILocalNetInterface
{
	ISessionManager SessionManager { get; }

	NetMod NetMode { get; }

	void Init(string gameName);

	void Uninit();

	bool StartServer(string serverName);

	void StopServer();

	bool StartClientAndSearchServer(string myselfPeerName);

	void StopClient();

	void SetServerAvailable(bool reliable);

	void JoinServer(string serverPeerId);

	void SendRawBytesToPeers(string[] peerIds, byte[] bytes, int lenth, bool reliable);

	string GetMyselfPeerId();

	void SetSendHeartbeat(bool isSend);

	string GetDisplayName(string peerId);
}
