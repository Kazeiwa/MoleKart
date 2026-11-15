using RaceProtocol;

public class ClientRankUI : OnlineRankUIBase
{
	protected bool m_ReceivedServerAckPackage;

	protected bool finishMessageSent;

	private new void Awake()
	{
	}

	protected void ReceiveServerAck(string peerId, MsgAckSelfFinishRace message)
	{
	}

	protected void SendFinishMessage2Server()
	{
	}

	protected void OnFinishRaceNow(string peerId, MsgFinishRaceNow message)
	{
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}
}
