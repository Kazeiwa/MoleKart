public class TransToScaleFlatEvt : FEvent
{
	public TransToScaleFlatEvt(FState state)
	{
		m_Sender = state;
		m_EvtType = EventType.ToScaleFlat;
	}
}
