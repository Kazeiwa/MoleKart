public class TransToBackFlipEvt : FEvent
{
	public TransToBackFlipEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToBackFlip;
	}
}
