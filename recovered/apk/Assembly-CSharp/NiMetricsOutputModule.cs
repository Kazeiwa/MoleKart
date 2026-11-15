public abstract class NiMetricsOutputModule
{
	public virtual void AddValue(char[] pcName, float fValue)
	{
	}

	public virtual void StartTimer(NiMetricsTimer kTimer)
	{
	}

	public virtual void EndTimer(NiMetricsTimer kTimer)
	{
	}

	public virtual void AddImmediateEvent(char[] pcName, float fValue)
	{
	}

	public virtual void Update()
	{
	}
}
