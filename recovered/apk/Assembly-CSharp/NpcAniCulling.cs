using UnityEngine;

public class NpcAniCulling : MonoBehaviour
{
	private Transform m_cur;

	private SkinnedMeshRenderer render;

	private void Start()
	{
		m_cur = base.transform;
		render = base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
	}

	private void Update()
	{
		if (render == null || m_cur == null || !CameraManager.instance.m_SmoothCam)
		{
			return;
		}
		if ((CameraManager.instance.m_SmoothCam.transform.position - m_cur.position).sqrMagnitude > 4900f)
		{
			render.enabled = false;
		}
		else
		{
			render.enabled = true;
		}
		if ((bool)base.gameObject.animation)
		{
			if (!render.isVisible)
			{
				base.gameObject.animation.Stop();
			}
			else
			{
				base.gameObject.animation.Play();
			}
		}
	}
}
