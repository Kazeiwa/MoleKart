public class FEvent
{
	public FState m_Sender;

	protected EventType m_EvtType;

	public FEvent()
	{
		m_EvtType = EventType.None;
	}

	public EventType type()
	{
		return m_EvtType;
	}
}
