public class TransToRightBoundEvt : FEvent
{
	public TransToRightBoundEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToRightBound;
	}
}
