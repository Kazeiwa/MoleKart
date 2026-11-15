public class NiTMetricsTimer<T> : NiMetricsTimer where T : NiSystemClockTimer
{
	protected T m_kTimer;

	public NiTMetricsTimer(char[] pcName)
		: base(pcName)
	{
	}

	public void Start()
	{
		m_kTimer.Start();
	}

	public void Stop()
	{
		m_kTimer.Stop();
		m_fElapsed = m_kTimer.GetElapsed();
	}
}
