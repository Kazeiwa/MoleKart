using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameKitManager : MonoBehaviour
{
	public delegate void PeerChangeEventHandler(string peerId);

	public delegate void PeerPickerCancelledEventHandler();

	public delegate void NetworkNotAvailableEventHandler();

	public static List<string> connectedPeers;

	public static List<string> availablePeers;

	public static event PeerChangeEventHandler peerConnected
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event PeerChangeEventHandler peerDisconnected
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event PeerChangeEventHandler peerBecameAvailable
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event PeerChangeEventHandler peerBecameUnavailable
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event PeerChangeEventHandler receivedConnectionRequestFromPeer
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event PeerChangeEventHandler acceptConnectionWithPeerFailed
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event PeerChangeEventHandler peerConnectedForVoiceChat
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event PeerChangeEventHandler peerDisconnectedFromVoiceChat
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event PeerPickerCancelledEventHandler peerPickerCancelled
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event NetworkNotAvailableEventHandler networkNotAavilable
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	public static bool isConnected()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void didConnectToPeer(string peerId)
	{
	}

	public void peerPickerDidCancel(string emptyString)
	{
	}

	public void peerDidDisconnect(string peerId)
	{
	}

	public void networkNotAvailable(string emptyString)
	{
	}

	public void peerDidBecameAvailable(string peerId)
	{
	}

	public void peerDidBecameUnavailable(string peerId)
	{
	}

	public void didReceiveConnectionRequestFromPeer(string peerId)
	{
	}

	public void acceptConnectionWithPeerDidFail(string peerId)
	{
	}

	public void voiceChatServiceDidStart(string peerId)
	{
	}

	public void voiceChatServiceDidStop(string peerId)
	{
	}
}
