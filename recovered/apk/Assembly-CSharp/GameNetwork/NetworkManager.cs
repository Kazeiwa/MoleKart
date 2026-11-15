using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Google.ProtocolBuffers;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

namespace GameNetwork;

public class NetworkManager
{
	private class Nested
	{
		internal static readonly NetworkManager Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new NetworkManager();
		}
	}

	private const string GameName = "MoleKart";

	public bool notDealPeerDisconnected;

	private bool isNetworkAvaliable = true;

	public static NetworkManager Instance => Nested.Instance;

	public int ConnectionType { get; set; }

	public string ClientPeerId { get; set; }

	public string ClientPeerName { get; set; }

	public string ServerPeerId { get; set; }

	public string ServerPeerName { get; set; }

	public string RecvNowTime { get; set; }

	public Dictionary<string, string> AvailableServers { get; set; }

	public bool IsServer => 1 == ConnectionType;

	public bool IsClient => 2 == ConnectionType;

	public bool IsStandAlone => 0 == ConnectionType;

	public bool IsNetworkAvaliable
	{
		get
		{
			return isNetworkAvaliable;
		}
		set
		{
			isNetworkAvaliable = value;
		}
	}

	private NetworkManager()
	{
		ConnectionType = 0;
		ClientPeerId = string.Empty;
		ClientPeerName = string.Empty;
		ServerPeerId = string.Empty;
		ServerPeerName = string.Empty;
		RecvNowTime = string.Empty;
		AvailableServers = new Dictionary<string, string>();
	}

	public void Init()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			LocalNetFactory.Instance.CreateLocalNet("MoleKart", NetworkType.enWirelessLocalNet);
			string userName = LocalUserConfig.Instance.userName;
			LocalNetFactory.Instance.LocalNet.StartClientAndSearchServer(userName);
			LocalNetFactory.Instance.LocalNet.StopClient();
			isNetworkAvaliable = true;
		}
	}

	public void StartServer()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			LocalNetFactory.Instance.CreateLocalNet("MoleKart", NetworkType.enWirelessLocalNet);
		}
		else
		{
			LocalNetFactory.Instance.CreateLocalNet("MoleKart", NetworkType.enIpLocalNet);
		}
		LocalNetFactory.Instance.LocalNet.StartServer(LocalUserConfig.Instance.userName);
		ConnectionType = 1;
		NetHandlerRegistry.Instance.RegistHandler<MsgOnPeerConnected>(OnPeerConnected);
		NetHandlerRegistry.Instance.RegistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
		NetHandlerRegistry.Instance.RegistHandler<MsgNowTime>(OnRecvTime);
		NetHandlerRegistry.Instance.RegistHandler<MsgIntoBackground>(OnOtherIntoBackground);
	}

	public void StopServer()
	{
		if (ConnectionType == 1)
		{
			NetHandlerRegistry.Instance.UnregistHandler<MsgOnPeerConnected>(OnPeerConnected);
			NetHandlerRegistry.Instance.UnregistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
			NetHandlerRegistry.Instance.UnregistHandler<MsgNowTime>(OnRecvTime);
			NetHandlerRegistry.Instance.UnregistHandler<MsgIntoBackground>(OnOtherIntoBackground);
			AvailableServers.Clear();
			ConnectionType = 0;
			ServerPeerId = string.Empty;
			ServerPeerName = string.Empty;
			ClientPeerId = string.Empty;
			ClientPeerName = string.Empty;
			RecvNowTime = string.Empty;
		}
	}

	public void StartClient()
	{
		ConnectionType = 2;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			LocalNetFactory.Instance.CreateLocalNet("MoleKart", NetworkType.enWirelessLocalNet);
		}
		else
		{
			LocalNetFactory.Instance.CreateLocalNet("MoleKart", NetworkType.enIpLocalNet);
		}
		string userName = LocalUserConfig.Instance.userName;
		NetHandlerRegistry.Instance.RegistHandler<MsgPeerBecameAvailable>(OnPeerBecameAvailable);
		NetHandlerRegistry.Instance.RegistHandler<MsgPeerBecameUnavailable>(OnPeerBecameUnavailable);
		NetHandlerRegistry.Instance.RegistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
		NetHandlerRegistry.Instance.RegistHandler<MsgNowTime>(OnRecvTime);
		NetHandlerRegistry.Instance.UnregistHandler<MsgIntoBackground>(OnOtherIntoBackground);
		LocalNetFactory.Instance.LocalNet.StartClientAndSearchServer(userName);
	}

	public void StopClient()
	{
		if (ConnectionType == 2)
		{
			NetHandlerRegistry.Instance.UnregistHandler<MsgPeerBecameAvailable>(OnPeerBecameAvailable);
			NetHandlerRegistry.Instance.UnregistHandler<MsgPeerBecameUnavailable>(OnPeerBecameUnavailable);
			NetHandlerRegistry.Instance.UnregistHandler<MsgOnPeerDisconnect>(OnPeerDisconnected);
			NetHandlerRegistry.Instance.UnregistHandler<MsgNowTime>(OnRecvTime);
			NetHandlerRegistry.Instance.UnregistHandler<MsgIntoBackground>(OnOtherIntoBackground);
			LocalNetFactory.Instance.LocalNet.StopClient();
			AvailableServers.Clear();
			ConnectionType = 0;
			ServerPeerId = string.Empty;
			ServerPeerName = string.Empty;
			ClientPeerId = string.Empty;
			ClientPeerName = string.Empty;
			RecvNowTime = string.Empty;
		}
	}

	public void ClientJoinServer(string peerId)
	{
		LocalNetFactory.Instance.LocalNet.JoinServer(peerId);
	}

	public bool BroadCastMessage(IMessageLite message, bool reliable)
	{
		return LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable);
	}

	public bool BroadCastMessageExcept(IMessageLite message, string excludedPeerId, bool reliable)
	{
		return LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacketExcept(message, excludedPeerId, reliable);
	}

	public bool SendMessageToPeer(string peerId, IMessageLite message)
	{
		return LocalNetFactory.Instance.LocalNet.SessionManager.FindSessionByPeerId(peerId)?.Send(message, available: true) ?? false;
	}

	private void OnPeerBecameAvailable(string peerId, MsgPeerBecameAvailable message)
	{
		if (!AvailableServers.ContainsKey(peerId))
		{
			AvailableServers.Add(peerId, message.PeerDisplayName);
		}
	}

	private void OnPeerBecameUnavailable(string peerId, MsgPeerBecameUnavailable message)
	{
		AvailableServers.Remove(peerId);
	}

	public void OnPeerConnected(string peerId, MsgOnPeerConnected message)
	{
		ClientPeerId = peerId;
		ClientPeerName = message.PeerDisplayName;
	}

	private void OnPeerDisconnected(string peerId, MsgOnPeerDisconnect message)
	{
		if (notDealPeerDisconnected)
		{
			notDealPeerDisconnected = false;
		}
		else if (SceneManager.Instance.CurGameSceneId != 1006 && SceneManager.Instance.CurGameSceneId != 1007 && GameDataManager.Instance.GetPlayerId(peerId) != uint.MaxValue)
		{
			if (!AppMain.Instance.isIntoBackground)
			{
				AppMain.Instance.QuitOnlineGame(AppMain.Instance.ShowOtherDisconnectedAlert);
			}
			ClientPeerId = string.Empty;
			ClientPeerName = string.Empty;
		}
	}

	public void SetSendHeartbeat(bool isSend)
	{
		LocalNetFactory.Instance.SetSendHeartbeat(isSend);
	}

	private void OnRecvTime(string peerId, MsgNowTime message)
	{
		RecvNowTime = message.NowTime;
	}

	private void OnOtherIntoBackground(string peerId, MsgIntoBackground message)
	{
		AppMain.Instance.QuitOnlineGame(AppMain.Instance.ShowOtherDisconnectedAlert);
	}
}
