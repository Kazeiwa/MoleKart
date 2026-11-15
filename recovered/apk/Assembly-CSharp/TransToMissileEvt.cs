public class TransToMissileEvt : FEvent
{
	public TransToMissileEvt(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToMissile;
	}
}
