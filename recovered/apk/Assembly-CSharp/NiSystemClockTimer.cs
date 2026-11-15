using UnityEngine;

public class NiSystemClockTimer
{
	protected float m_fStart;

	protected float m_fElapsed;

	public float GetElapsed()
	{
		return m_fElapsed;
	}

	public void Start()
	{
		m_fStart = Time.time;
	}

	public void Stop()
	{
		m_fElapsed = Time.time - m_fStart;
	}
}
