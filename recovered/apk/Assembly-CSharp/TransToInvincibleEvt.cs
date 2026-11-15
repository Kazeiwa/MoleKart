public class TransToInvincibleEvt : FEvent
{
	public TransToInvincibleEvt(FState state)
	{
		m_Sender = state;
		m_EvtType = EventType.ToInvincible;
	}
}
