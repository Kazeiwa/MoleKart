using UnityEngine;

public class SmoothCameraWithBumper : MonoBehaviour
{
	private CameraMode m_CameraMode;

	public CameraData m_CurCameraData;

	public Transform m_ChangeTarget;

	private Car m_HeroCar;

	private iTweenPath m_ObjPath;

	public static SmoothCameraWithBumper Instacne;

	private Camera m_Camera;

	private Transform m_Transform;

	private float m_CullTime;

	private void Awake()
	{
		Instacne = this;
		m_CameraMode = CameraMode.Follow;
		m_CurCameraData = new CameraData();
		m_CurCameraData.Reset();
		m_ObjPath = base.gameObject.GetComponentInChildren<iTweenPath>();
		AutoCamera.Instance.SetItween(m_ObjPath);
		m_Camera = base.gameObject.GetComponent<Camera>();
		m_HeroCar = TSingleton<CarManager>.Instance.GetHeroCar();
		FollowCamera.Instance.m_HeroCar = m_HeroCar;
		m_CurCameraData.m_position = base.transform.position;
		m_CurCameraData.m_FieldView = base.camera.fieldOfView;
		m_CurCameraData.m_Right = base.transform.right;
		if (m_HeroCar != null)
		{
			Transform target = m_HeroCar.transform;
			m_CurCameraData.m_Target = target;
		}
		m_Transform = base.transform;
	}

	public void SetCameraMode(CameraMode mode)
	{
		m_CameraMode = mode;
	}

	public void DeathReset()
	{
		m_CurCameraData.DeathReset();
	}

	public void ResetCamera()
	{
	}

	private void OnPreCull()
	{
		if (!SystemConfig.Instance.SetNpcInvisible)
		{
			SetAllNPCVisible();
		}
	}

	public void UpdateEx()
	{
		if (!(m_HeroCar == null))
		{
			bool flag = false;
			switch (m_CameraMode)
			{
			case CameraMode.Follow:
				flag = FollowCamera.Instance.UpdateCamera(m_CurCameraData);
				break;
			case CameraMode.Auto:
				flag = AutoCamera.Instance.UpdateCamera(m_CurCameraData);
				break;
			}
			if (flag)
			{
				m_Transform.position = m_CurCameraData.m_position;
				m_Transform.LookAt(m_CurCameraData.m_TargetPosition);
			}
		}
	}

	public void FixedUpdateEx()
	{
		if (m_HeroCar == null)
		{
			Debug.LogError("Hero Car is null, exit");
			return;
		}
		bool flag = false;
		switch (m_CameraMode)
		{
		case CameraMode.Follow:
			flag = FollowCamera.Instance.FixUpdateCamera(m_CurCameraData);
			break;
		case CameraMode.Auto:
			flag = AutoCamera.Instance.FixUpdateCamera(m_CurCameraData);
			break;
		}
		if (flag)
		{
			m_Transform.position = m_CurCameraData.m_position;
			m_Transform.LookAt(m_CurCameraData.m_TargetPosition);
			m_CurCameraData.m_Right = base.transform.right;
		}
	}

	public void ForceCameraNormal()
	{
		FollowCamera.Instance.ForceCameraNormal(m_CurCameraData);
	}

	public void EnterCamera()
	{
		switch (m_CameraMode)
		{
		case CameraMode.Follow:
			FollowCamera.Instance.EnterCamera(m_CurCameraData);
			break;
		}
	}

	public void ChangePosition()
	{
		base.gameObject.transform.position = new Vector3(53f, 15.5026f, -2.4f);
	}

	private void OnDestroy()
	{
		if (m_CurCameraData != null)
		{
			m_CurCameraData.UnInit();
			m_CurCameraData = null;
		}
	}

	private void SetAllNPCVisible()
	{
		m_CullTime += Time.deltaTime;
		if (!(m_CullTime > 1f))
		{
			return;
		}
		m_CullTime = 0f;
		foreach (Car car in TSingleton<CarManager>.Instance.Cars)
		{
			if ((car.CarState & 0x20) != 0)
			{
				break;
			}
			if (!car.isHero)
			{
				if ((base.transform.position - car.transform.position).sqrMagnitude > 10000f)
				{
					car.CameraDistanceHide(isNear: false);
					car.DisableAllCarParticles();
				}
				else
				{
					car.CameraDistanceHide(isNear: true);
					car.RestAllParticle();
				}
			}
		}
	}
}
