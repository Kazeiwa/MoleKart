namespace TaomeeNetwork;

internal class WirelessLocalNetwork : ILocalNetInterface
{
	private ISessionManager sessionMgr;

	private NetMod netmod;

	private string _gameName;

	public ISessionManager SessionManager
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public NetMod NetMode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Init(string gameName)
	{
	}

	public void Uninit()
	{
	}

	public bool StartServer(string serverName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void StopServer()
	{
	}

	public bool StartClientAndSearchServer(string myselfPeerName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void StopClient()
	{
	}

	public void SetServerAvailable(bool available)
	{
	}

	public void JoinServer(string serverPeerId)
	{
	}

	public void SendRawBytesToPeers(string[] peerIds, byte[] bytes, int lenth, bool realiable)
	{
	}

	public string GetMyselfPeerId()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetSendHeartbeat(bool isSend)
	{
	}

	public string GetDisplayName(string peerId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
