using UnityEngine;

public class CameraDistanceHide : MonoBehaviour
{
	public bool _enable;

	public float _distanceMinSqr;

	public float _distanceMaxSqr;

	public bool _InvalidateOnHighHardwareLevel;

	private Renderer[] m_Render;

	private bool m_curShow;

	private Camera m_refMainCamera;

	private int m_bitUpdateFlag;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetVisible(bool isActive)
	{
	}
}
