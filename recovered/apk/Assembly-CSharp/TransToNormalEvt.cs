public class TransToNormalEvt : FEvent
{
	public TransToNormalEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToNormal;
	}
}
