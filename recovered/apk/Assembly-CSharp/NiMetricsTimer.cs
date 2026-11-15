public class NiMetricsTimer
{
	protected char[] m_pcName;

	protected float m_fElapsed;

	public NiMetricsTimer(char[] pcName)
	{
		m_pcName = pcName;
		m_fElapsed = 0f;
	}

	public char[] GetName()
	{
		return m_pcName;
	}

	public float GetElapsed()
	{
		return m_fElapsed;
	}
}
