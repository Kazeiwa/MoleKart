public class TransToCarDeadEvt : FEvent
{
	public TransToCarDeadEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToDead;
	}
}
