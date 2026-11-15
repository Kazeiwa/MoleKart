public class TransToQuakeEvt : FEvent
{
	public TransToQuakeEvt(FState state)
	{
		m_Sender = state;
		m_EvtType = EventType.ToQuake;
	}
}
