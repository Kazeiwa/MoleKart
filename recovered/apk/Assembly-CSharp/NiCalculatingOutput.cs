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
			m_pcName = pcName;
			m_fCurrentSum = 0f;
			m_uiSamples = 0;
			m_fCurrentMin = 0f;
			m_fCurrentMax = 0f;
			m_fStoredMin = 0f;
			m_fStoredMax = 0f;
			m_fStoredSum = 0f;
			m_fStoredMean = 0f;
			m_fStoredCount = 0f;
		}

		public float GetValue(MetricsFunction eFunc)
		{
			return eFunc switch
			{
				MetricsFunction.FUNC_MIN => m_fStoredMin, 
				MetricsFunction.FUNC_MAX => m_fStoredMax, 
				MetricsFunction.FUNC_SUM => m_fStoredSum, 
				MetricsFunction.FUNC_SUM_PER_FRAME => m_fStoredFSum, 
				MetricsFunction.FUNC_MEAN => m_fStoredMean, 
				MetricsFunction.FUNC_COUNT => m_fStoredCount, 
				_ => 0f, 
			};
		}

		public char[] GetName()
		{
			return m_pcName;
		}

		public void AddValue(float fValue)
		{
			if (m_uiSamples == 0)
			{
				m_fCurrentMin = (m_fCurrentMax = (m_fCurrentSum = fValue));
			}
			else
			{
				m_fCurrentSum += fValue;
				m_fCurrentMax = ((!(fValue > m_fCurrentMax)) ? m_fCurrentMax : fValue);
				m_fCurrentMin = ((!(fValue < m_fCurrentMin)) ? m_fCurrentMin : fValue);
			}
			m_uiSamples++;
		}

		public void StoreCurrent(int uiFramePeriod)
		{
			m_fStoredSum = m_fCurrentSum;
			m_fStoredMin = m_fCurrentMin;
			m_fStoredMax = m_fCurrentMax;
			m_fStoredCount = m_uiSamples;
			m_fStoredFSum = m_fCurrentSum / (float)uiFramePeriod;
			m_fStoredMean = ((m_uiSamples <= 0) ? 0f : (m_fStoredSum / m_fStoredCount));
			m_uiSamples = 0;
		}
	}
}
