public class TransToScaleSmallerEvt : FEvent
{
	public TransToScaleSmallerEvt(FState state)
	{
		m_Sender = state;
		m_EvtType = EventType.ToScaleSmaller;
	}
}
