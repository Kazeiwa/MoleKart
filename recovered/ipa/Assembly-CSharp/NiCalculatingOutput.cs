public class NiCalculatingOutput : NiMetricsOutputModule
{
	public enum MetricsFunction
	{
		FUNC_MIN,
		FUNC_MAX,
		FUNC_SUM,
		FUNC_SUM_PER_FRAME,
		FUNC_MEAN,
		FUNC_COUNT,
		NUM_FUNCS
	}

	private class MetricContainer
	{
		private char[] m_pcName;

		private float m_fCurrentSum;

		private int m_uiSamples;

		private float m_fCurrentMin;

		private float m_fCurrentMax;

		private float m_fStoredMin;

		private float m_fStoredMax;

		private float m_fStoredSum;

		private float m_fStoredFSum;

		private float m_fStoredMean;

		private float m_fStoredCount;

		public MetricContainer(char[] pcName)
		{
		}

		public float GetValue(MetricsFunction eFunc)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public char[] GetName()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public void AddValue(float fValue)
		{
		}

		public void StoreCurrent(int uiFramePeriod)
		{
		}
	}
}
