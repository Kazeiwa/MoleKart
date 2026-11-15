using Google.ProtocolBuffers;

namespace TaomeeNetwork;

public class Session : ISession
{
	private SendBuf availableBuf;

	private SendBuf unavailableBuf;

	private string[] peers;

	private string m_peerId;

	private string m_peerName;

	public bool Send(IMessageLite msg, bool reliable)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void EndSend()
	{
	}

	public void EndUnreliableSend()
	{
	}

	public void OnRecv(byte[] buff, int lenth)
	{
	}

	public void SetPeerID(string peerId)
	{
	}

	public string GetPeerID()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetPeerName(string peerName)
	{
	}

	public string GetPeerName()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
