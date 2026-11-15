namespace TaomeeNetwork;

internal class LocalNetFactory
{
	private static LocalNetFactory instance;

	private ILocalNetInterface localNet;

	private NetworkType _netType;

	public static LocalNetFactory Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new LocalNetFactory();
			}
			return instance;
		}
	}

	public ILocalNetInterface LocalNet => localNet;

	public ILocalNetInterface CreateLocalNet(string gameName, NetworkType netType)
	{
		if (_netType != netType && _netType != NetworkType.enNoNetWork && localNet != null)
		{
			localNet.Uninit();
			localNet = null;
		}
		if (localNet == null)
		{
			_netType = netType;
			localNet = new IpLocalNetwork();
		}
		localNet.Uninit();
		localNet.Init(gameName);
		return localNet;
	}

	public void SetSendHeartbeat(bool isSend)
	{
		if (localNet != null)
		{
			localNet.SetSendHeartbeat(isSend);
		}
	}
}
