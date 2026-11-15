using RaceProtocol;
using TaomeeNetwork;

public class ReceiveMessageHandler
{
	private static ReceiveMessageHandler instance;

	private Handler<MsgPickProp> m_PickPropHandler;

	private Handler<MsgUseProp> m_UsePropHandler;

	private Handler<MsgCarState> m_CarStateHandler;

	private Handler<MsgFireSphere> m_FireSphereHandler;

	private Handler<MsgTransferThunderCloud> m_TransferThunderHandler;

	private Handler<MsgDestroyProp> m_DestroyPropHandler;

	public static ReceiveMessageHandler Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private ReceiveMessageHandler()
	{
	}

	public void Init()
	{
	}

	public void UnInit()
	{
	}

	private void OnRecvPickPropMsg(string peerId, MsgPickProp msg)
	{
	}

	private void OnRecvUsePropMsg(string peerId, MsgUseProp msg)
	{
	}

	private void OnRecvCarState(string peerId, MsgCarState msg)
	{
	}

	private void OnRecvFireSphere(string peerID, MsgFireSphere msg)
	{
	}

	private void OnRecvTransferThunder(string peerID, MsgTransferThunderCloud msg)
	{
	}

	private void OnRecvDestroyProp(string peerID, MsgDestroyProp msg)
	{
	}

	private void OnRequestID(string peerID, MsgRequsetID msg)
	{
	}

	private void ReceiveID(string peerID, MsgSendID msg)
	{
	}
}
