public class TransToBounceEvt : FEvent
{
	public TransToBounceEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToBounce;
	}
}
