public class TransToRotateEvt : FEvent
{
	public TransToRotateEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToRotate;
	}
}
