using UnityEngine;

public class PerformanceHide : MonoBehaviour
{
	public bool _enable = true;

	public int _performanceLvl = 1;

	private Renderer[] m_Render;

	private void Start()
	{
		if (_enable)
		{
			m_Render = base.transform.GetComponentsInChildren<Renderer>();
			if (iPhoneSettings.generation < iPhoneGeneration.iPad2Gen)
			{
				_performanceLvl = 0;
			}
			if (_performanceLvl == 0)
			{
				base.gameObject.SetActiveRecursively(state: false);
			}
		}
	}
}
