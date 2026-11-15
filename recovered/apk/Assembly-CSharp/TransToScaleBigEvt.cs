public class TransToScaleBigEvt : FEvent
{
	public TransToScaleBigEvt(FState state)
	{
		m_Sender = state;
		m_EvtType = EventType.ToScaleBigger;
	}
}
