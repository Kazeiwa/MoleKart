using UnityEngine;

public class CameraDistanceHide : MonoBehaviour
{
	public bool _enable = true;

	public float _distanceMinSqr = 10000f;

	public float _distanceMaxSqr = 14400f;

	private Renderer[] m_Render;

	private bool m_curShow = true;

	private Camera m_refMainCamera;

	private int m_bitUpdateFlag;

	private void Start()
	{
		if (_enable)
		{
			m_Render = base.transform.GetComponentsInChildren<Renderer>();
			GameObject gameObject = GameObject.Find("MainCamera");
			if (gameObject != null)
			{
				m_refMainCamera = gameObject.GetComponent<Camera>();
			}
			SetVisible(isActive: false);
			m_bitUpdateFlag = Random.Range(0, 15);
		}
	}

	private void Update()
	{
		if (!_enable)
		{
			return;
		}
		m_bitUpdateFlag++;
		if (m_bitUpdateFlag < 16)
		{
			return;
		}
		if (m_refMainCamera != null)
		{
			Vector3 position = base.transform.position;
			position -= m_refMainCamera.transform.position;
			if (position.sqrMagnitude > _distanceMaxSqr)
			{
				SetVisible(isActive: false);
			}
			else if (position.sqrMagnitude < _distanceMinSqr)
			{
				SetVisible(isActive: true);
			}
		}
		m_bitUpdateFlag = 0;
	}

	public void SetVisible(bool isActive)
	{
		if (m_curShow == isActive)
		{
			return;
		}
		m_curShow = isActive;
		if (m_Render != null)
		{
			for (int i = 0; i < m_Render.Length; i++)
			{
				m_Render[i].enabled = isActive;
			}
		}
	}
}
