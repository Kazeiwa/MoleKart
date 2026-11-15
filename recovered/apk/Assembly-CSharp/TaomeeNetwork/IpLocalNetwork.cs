namespace TaomeeNetwork;

internal class IpLocalNetwork : ILocalNetInterface
{
	private ISessionManager sessionMgr;

	private NetMod netmod;

	private string _gameName;

	public ISessionManager SessionManager => sessionMgr;

	public NetMod NetMode => netmod;

	public void Init(string gameName)
	{
		_gameName = gameName;
		sessionMgr = new SessionManager();
		IpNet instance = IpNet.Instance;
		netmod = NetMod.noNet;
	}

	public void Uninit()
	{
		IpNet.Instance.StopServer();
		IpNet.Instance.StopClient();
		sessionMgr = null;
		netmod = NetMod.noNet;
	}

	public bool StartServer(string serverName)
	{
		string serverName2 = _gameName + serverName;
		IpNet.Instance.StartServer(serverName2);
		netmod = NetMod.NetServer;
		return true;
	}

	public void StopServer()
	{
		IpNet.Instance.StopServer();
		sessionMgr.Clear();
		netmod = NetMod.noNet;
	}

	public bool StartClientAndSearchServer(string myselfPeerName)
	{
		string displayName = _gameName + myselfPeerName;
		IpNet.Instance.StartClient(displayName);
		netmod = NetMod.NetClient;
		return true;
	}

	public void StopClient()
	{
		IpNet.Instance.StopClient();
		sessionMgr.Clear();
		netmod = NetMod.noNet;
	}

	public void SetServerAvailable(bool available)
	{
		IpNet.Instance.SetServerAvailable(available);
	}

	public void JoinServer(string serverPeerId)
	{
		IpNet.Instance.ConnectToServer(serverPeerId);
	}

	public void Send(string peerId, byte[] bytes, int lenth)
	{
		IpNet.Instance.Send(peerId, bytes, lenth);
	}

	public void SendRawBytesToPeers(string[] peerIds, byte[] bytes, int lenth, bool realiable)
	{
		IpNet.Instance.SendRawBytesToPeers(peerIds, bytes, lenth);
	}

	public string GetMyselfPeerId()
	{
		return IpNet.Instance.GetMyselfPeerId();
	}

	public void SetSendHeartbeat(bool isSend)
	{
	}

	public string GetDisplayName(string peerId)
	{
		string displayName = IpNet.Instance.GetDisplayName(peerId);
		int num = displayName.IndexOf(_gameName);
		if (num < 0)
		{
			return null;
		}
		int length = _gameName.Length;
		return displayName.Substring(num + length);
	}
}
