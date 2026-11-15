using System.Runtime.InteropServices;

public class GameKitBinding
{
	[DllImport("__Internal")]
	private static extern void _gameKitShowPeerPicker(bool includeOnlineChooser);

	public static void showPeerPicker(bool includeOnlineChooser)
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitInitializeSessionForOnlineOnly();

	public static void initializeSessionForOnlineOnly()
	{
	}

	[DllImport("__Internal")]
	private static extern bool _gameKitInitializeSessionWithDisplayName(string gameName, string displayName);

	public static bool initializeSessionWithDisplayName(string gameName, string displayName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern bool _setAvailable(bool available);

	public static bool setAvailable(bool available)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern void _gameKitSetAutoConnectPeers(bool shouldAutoConnect);

	public static void setAutoConnectPeers(bool shouldAutoConnect)
	{
	}

	[DllImport("__Internal")]
	private static extern string _gameKitGetConnectedPeers();

	public static string[] getConnectedPeers()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern string _gameKitSendMessage(bool reliably, string gameObject, string method, string param);

	public static string sendData(string gameObject, string method, string param, bool reliably)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern string _gameKitSendMessageToPeers(bool reliably, string peerIds, string gameObject, string method, string param);

	public static string sendMessageToPeers(string[] peerIds, string gameObject, string method, string param, bool reliably)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern string _gameKitSendRawMessageToAllPeers(string gameObject, string method, byte[] param, int length, bool reliably);

	public static string sendRawMessageToAllPeers(string gameObject, string method, byte[] param, bool reliably)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern string _gameKitSendRawMessageToPeers(string playerIds, string gameObject, string method, byte[] param, int length, bool reliably);

	public static string sendRawMessageToPeers(string[] playerIds, string gameObject, string method, byte[] param, int lenth, bool reliably)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern void _gameKitSetSessionMode(int sessionMode);

	public static void setSessionMode(GameKitSessionMode sessionMode)
	{
	}

	[DllImport("__Internal")]
	private static extern int _gameKitCurrentSessionMode();

	public static GameKitSessionMode currentSessionMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern string _gameKitDisplayNameForPeer(string peerId);

	public static string displayNameForPeer(string peerId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern string _gameKitGetPeerId();

	public static string getPeerId()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern void _gameKitInvalidateSession();

	public static void invalidateSession()
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitConnectToPeer(string peerId);

	public static void connectToPeer(string peerId)
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitAcceptConnectionFromPeer(string peerId);

	public static void acceptConnectionFromPeer(string peerId)
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitDenyConnectionFromPeer(string peerId);

	public static void denyConnectionFromPeer(string peerId)
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitDisconnectFromPeer(string peerId);

	public static void DisconnectFromPeer(string peerId)
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitSetVoiceChatEnabled(bool voiceChatEnabled);

	public static void setVoiceChatEnabled(bool voiceChatEnabled)
	{
	}

	[DllImport("__Internal")]
	private static extern float _gameKitGetRemoteParticipantVolume();

	public static void getRemoteParticipantVolume()
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitSetRemoteParticipantVolume(float volume);

	public static void setRemoteParticipantVolume(float volume)
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitEnableInputMetering(bool shouldEnable);

	public static void enableInputMetering(bool shouldEnable)
	{
	}

	[DllImport("__Internal")]
	private static extern void _gameKitEnableOutputMetering(bool shouldEnable);

	public static void enableOutputMetering(bool shouldEnable)
	{
	}

	[DllImport("__Internal")]
	private static extern float _gameKitGetInputMeterLevel();

	public static float getInputMeterLevel()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DllImport("__Internal")]
	private static extern float _gameKitGetOutputMeterLevel();

	public static float getOutputMeterLevel()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
