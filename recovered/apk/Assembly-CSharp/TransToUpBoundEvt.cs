public class TransToUpBoundEvt : FEvent
{
	public TransToUpBoundEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToUpBound;
	}
}
