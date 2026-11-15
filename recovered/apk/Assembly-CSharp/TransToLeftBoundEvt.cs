public class TransToLeftBoundEvt : FEvent
{
	public TransToLeftBoundEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToLeftBound;
	}
}
