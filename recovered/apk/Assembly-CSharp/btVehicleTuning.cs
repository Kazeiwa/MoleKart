public class btVehicleTuning
{
	public float m_suspensionStiffness;

	public float m_suspensionCompression;

	public float m_suspensionDamping;

	public float m_maxSuspensionTravel;

	public float m_frictionSlip;

	public float m_maxSuspensionForce;

	public btVehicleTuning()
	{
		m_suspensionStiffness = 30f;
		m_suspensionCompression = 0.8f;
		m_suspensionDamping = 0.83f;
		m_maxSuspensionTravel = 0.4f;
		m_frictionSlip = 10.5f;
		m_maxSuspensionForce = 6000f;
	}
}
