using System.Collections.Generic;

public class NiMetricsLayer
{
	public static List<NiMetricsOutputModule> ms_kOutputs;

	public static void _SDMInit()
	{
		ms_kOutputs = new List<NiMetricsOutputModule>();
	}

	public static void _SDMShutdown()
	{
	}

	public void AddOutputModule(NiMetricsOutputModule pkMod)
	{
		ms_kOutputs.Add(pkMod);
	}

	public void RemoveOutputModule(NiMetricsOutputModule pkMod)
	{
		ms_kOutputs.Remove(pkMod);
	}

	public void AddValue(char[] pcName, float fValue)
	{
		for (int i = 0; i < ms_kOutputs.Count; i++)
		{
			ms_kOutputs[i].AddValue(pcName, fValue);
		}
	}

	public void StartTimer(NiMetricsTimer kTimer)
	{
		for (int i = 0; i < ms_kOutputs.Count; i++)
		{
			ms_kOutputs[i].StartTimer(kTimer);
		}
	}

	public void EndTimer(NiMetricsTimer kTimer)
	{
		for (int i = 0; i < ms_kOutputs.Count; i++)
		{
			ms_kOutputs[i].EndTimer(kTimer);
		}
	}

	public void Update()
	{
		for (int i = 0; i < ms_kOutputs.Count; i++)
		{
			ms_kOutputs[i].Update();
		}
	}
}
