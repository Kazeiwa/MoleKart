public class TransToLeafShelter : FEvent
{
	public TransToLeafShelter(FState sender)
	{
		m_Sender = sender;
		m_EvtType = EventType.ToLeafShelter;
	}
}
