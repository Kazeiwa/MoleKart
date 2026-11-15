public class TransToThunderCloudEvt : FEvent
{
	public TransToThunderCloudEvt(FState state)
	{
		m_Sender = state;
		m_EvtType = EventType.ToThunderCloud;
	}
}
