using UnityEngine;

public class CameraAspact : MonoBehaviour
{
	private Camera m_camera;

	private void Start()
	{
		m_camera = base.gameObject.GetComponent<Camera>();
		if (!m_camera)
		{
		}
	}
}
