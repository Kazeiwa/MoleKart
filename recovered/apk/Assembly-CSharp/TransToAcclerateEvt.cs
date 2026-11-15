public class TransToAcclerateEvt : FEvent
{
	public TransToAcclerateEvt(FState state)
	{
		m_Sender = state;
		m_EvtType = EventType.ToAccelerate;
	}
}
