using UnityEngine;

public class AutoCamera
{
	private static AutoCamera instance;

	private float m_TimeAdd;

	private float m_pretime;

	private iTweenPath m_ObjPath;

	private Vector3 m_OffsetTarget = Vector3.zero;

	public static AutoCamera Instance
	{
		get
		{
			if (instance == null)
			{
				new AutoCamera();
			}
			return instance;
		}
	}

	public AutoCamera()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public bool UpdateCamera(CameraData camdata)
	{
		return false;
	}

	public bool FixUpdateCamera(CameraData camdata)
	{
		switch (camdata.GetState())
		{
		case CameraState.CamStatic:
			m_TimeAdd = 0f;
			m_pretime = 0f;
			camdata.m_position = new Vector3(53f, 15.5026f, -2.4f);
			break;
		case CameraState.CamAni:
			m_TimeAdd += Time.deltaTime;
			m_pretime = m_TimeAdd / 2f;
			SmoothAni(m_pretime, camdata);
			break;
		}
		m_OffsetTarget.x = camdata.m_Tarx;
		m_OffsetTarget.y = camdata.m_Tary;
		m_OffsetTarget.z = camdata.m_Tarz;
		camdata.m_TargetPosition = camdata.m_Target.position + m_OffsetTarget;
		return true;
	}

	public void SmoothAni(float t, CameraData camdata)
	{
		if (m_TimeAdd > 2f)
		{
			CameraManager.instance.SetMainState(CameraManager.MainCameraState.Normal);
			CameraManager.instance.ChangeCameraMode(CameraMode.Follow);
			return;
		}
		int num = m_ObjPath.nodeCount - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		Vector3 vector = m_ObjPath.nodes[num2];
		Vector3 vector2 = m_ObjPath.nodes[num2 + 1];
		Vector3 vector3 = m_ObjPath.nodes[num2 + 2];
		Vector3 vector4 = m_ObjPath.nodes[num2 + 3];
		camdata.m_position = 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
	}

	public void SetItween(iTweenPath path)
	{
		m_ObjPath = path;
	}
}
