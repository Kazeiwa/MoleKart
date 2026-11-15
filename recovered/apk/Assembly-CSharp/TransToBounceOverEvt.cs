public class TransToBounceOverEvt : FEvent
{
	public TransToBounceOverEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToBounceOver;
	}
}
