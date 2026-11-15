public class TransToBigBoundEvt : FEvent
{
	public TransToBigBoundEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToBigBound;
	}
}
