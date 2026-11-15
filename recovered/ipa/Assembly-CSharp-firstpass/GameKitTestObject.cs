using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class GameKitTestObject : MonoBehaviour
{
	public GUIText guiTextField;

	public GUIText guiTextFieldTime;

	public Transform someTransform;

	private static GameKitTestObject _instance;

	private void Awake()
	{
	}

	public void Start()
	{
	}

	public void OnDisable()
	{
	}

	private void receivedConnectionRequestFromPeer(string peerId)
	{
	}

	private void peerBecameAvailable(string peerId)
	{
	}

	private void peerBecameUnavailable(string peerId)
	{
	}

	public void onPeerConnected(string peerId)
	{
	}

	public void onPeerDisconnect(string peerId)
	{
	}

	public void onPeerPickerCanceled()
	{
	}

	public void onPeerConnectedForVoiceChat(string peerId)
	{
	}

	public void onPeerDisconnectedFromVoiceChat(string peerId)
	{
	}

	[DebuggerHidden]
	public IEnumerator sendTouchesToPeer()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void gameKitTestMethod(string param)
	{
	}

	public void touchMovedToLocation(string param)
	{
	}

	public void sendRawBytes()
	{
	}

	public static void receivedRawBytes(byte[] bytes)
	{
	}
}
