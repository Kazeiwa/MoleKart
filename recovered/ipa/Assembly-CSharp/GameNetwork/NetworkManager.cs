using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Google.ProtocolBuffers;
using RaceProtocol;

namespace GameNetwork;

public class NetworkManager
{
	private class Nested
	{
		internal static readonly NetworkManager Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
		}
	}

	private const string GameName = "MoleKart";

	public bool notDealPeerDisconnected;

	private bool isNetworkAvaliable;

	public static NetworkManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int ConnectionType
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public string ClientPeerId
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public string ClientPeerName
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public string ServerPeerId
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public string ServerPeerName
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public string RecvNowTime
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public Dictionary<string, string> AvailableServers
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public bool IsServer
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsClient
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsStandAlone
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsNetworkAvaliable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	private NetworkManager()
	{
	}

	public void Init()
	{
	}

	public void StartServer()
	{
	}

	public void StopServer()
	{
	}

	public void StartClient()
	{
	}

	public void StopClient()
	{
	}

	public void ClientJoinServer(string peerId)
	{
	}

	public bool BroadCastMessage(IMessageLite message, bool reliable)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool BroadCastMessageExcept(IMessageLite message, string excludedPeerId, bool reliable)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool SendMessageToPeer(string peerId, IMessageLite message)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void OnPeerBecameAvailable(string peerId, MsgPeerBecameAvailable message)
	{
	}

	private void OnPeerBecameUnavailable(string peerId, MsgPeerBecameUnavailable message)
	{
	}

	public void OnPeerConnected(string peerId, MsgOnPeerConnected message)
	{
	}

	private void OnPeerDisconnected(string peerId, MsgOnPeerDisconnect message)
	{
	}

	public void SetSendHeartbeat(bool isSend)
	{
	}

	private void OnRecvTime(string peerId, MsgNowTime message)
	{
	}

	private void OnOtherIntoBackground(string peerId, MsgIntoBackground message)
	{
	}
}
