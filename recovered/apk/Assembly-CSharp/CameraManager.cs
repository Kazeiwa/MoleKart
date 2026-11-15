using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
	public enum MainCameraState
	{
		StaticState,
		AniState,
		Normal
	}

	public struct CamData
	{
		public GameObject cam;

		public float lasttime;
	}

	public Dictionary<string, CamData> m_Cameras;

	public GameObject m_CurrentCamera;

	private Camera[] m_AllCameras;

	private float m_Time;

	private GameObject m_CameraManager;

	public MainCameraState m_CamState;

	public SmoothCameraWithBumper m_SmoothCam;

	public CullingCamera_Manual m_CullCam;

	private static CameraManager m_Instance;

	public static CameraManager instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = new CameraManager();
			}
			return m_Instance;
		}
	}

	public void Init()
	{
		m_CamState = MainCameraState.StaticState;
		m_Time = 0f;
		m_Cameras = new Dictionary<string, CamData>();
		m_CameraManager = GameObject.Find("CyclicCamAni/CameraManager");
		if (null == m_CameraManager)
		{
			DebugEx.Log("相机管理器(CameraManager)不存在!");
			return;
		}
		m_AllCameras = m_CameraManager.GetComponentsInChildren<Camera>();
		Camera[] allCameras = m_AllCameras;
		foreach (Camera camera in allCameras)
		{
			Camera camera2 = camera;
			if (m_Cameras.ContainsKey(camera2.name))
			{
				Debug.LogWarning("SecurityCamera: Warning! All cameras used with SecurityCamera should have unique names. Conflict has been automatically renamed.");
			}
			CamData value = default(CamData);
			iTweenEvent component = camera2.GetComponent<iTweenEvent>();
			float lasttime = (float)component.Values["time"];
			value.cam = camera2.gameObject;
			value.lasttime = lasttime;
			m_Cameras.Add(camera2.name, value);
			if (value.cam.name == "Camera1")
			{
				m_CurrentCamera = value.cam;
				m_CurrentCamera.active = false;
				m_CurrentCamera.camera.depth = -1f;
			}
			else
			{
				camera2.gameObject.active = false;
			}
		}
		GameObject gameObject = GameObject.Find("MainCamera");
		m_SmoothCam = gameObject.GetComponent<SmoothCameraWithBumper>();
		m_SmoothCam.ResetCamera();
		m_SmoothCam.ChangePosition();
		m_CullCam = m_SmoothCam.GetComponent<CullingCamera_Manual>();
		if ((bool)m_CullCam)
		{
			m_CullCam.disableCullingForTest = true;
		}
	}

	public float GetAllTime()
	{
		float num = 0f;
		foreach (KeyValuePair<string, CamData> camera in m_Cameras)
		{
			num += camera.Value.lasttime;
		}
		return num;
	}

	public void Update()
	{
		if (HeightMapManager.Instance.bValidate)
		{
			m_CurrentCamera.active = true;
		}
		if (m_CurrentCamera != null && m_Cameras != null)
		{
			m_Time += Time.deltaTime;
			float num = m_Time;
			foreach (KeyValuePair<string, CamData> camera in m_Cameras)
			{
				if (camera.Value.lasttime - num > 0f)
				{
					ChangeCamera(camera.Key);
					break;
				}
				num -= camera.Value.lasttime;
			}
		}
		m_SmoothCam.UpdateEx();
	}

	public void ChangeCamera(string newCamera)
	{
		if (m_CurrentCamera.gameObject != m_Cameras[newCamera].cam.gameObject)
		{
			m_CurrentCamera.active = false;
			m_Cameras[newCamera].cam.active = true;
			m_CurrentCamera = m_Cameras[newCamera].cam;
			m_CurrentCamera.camera.depth = -1f;
		}
	}

	public void FadeInAndOut()
	{
		FadeOutAndInCallBack component = m_CameraManager.GetComponent<FadeOutAndInCallBack>();
		component.FadeInAndOut();
	}

	public void ChangeNormal()
	{
		ChangeMainCamera();
		SetMainState(MainCameraState.Normal);
		ChangeCameraMode(CameraMode.Follow);
		if ((bool)m_CullCam)
		{
			m_CullCam.disableCullingForTest = false;
		}
	}

	public void ChangeMainCamera()
	{
		Camera[] allCameras = m_AllCameras;
		foreach (Camera camera in allCameras)
		{
			camera.enabled = false;
		}
		m_SmoothCam.gameObject.active = true;
	}

	public void SetMainState(MainCameraState state)
	{
		m_CamState = state;
	}

	public void EnterCamera()
	{
		if (m_SmoothCam != null)
		{
			m_SmoothCam.EnterCamera();
		}
	}

	public void ForceCameraNormal()
	{
		if (m_SmoothCam != null)
		{
			m_SmoothCam.ForceCameraNormal();
		}
	}

	public void ChangeCameraMode(CameraMode mode)
	{
		m_SmoothCam.SetCameraMode(mode);
	}

	public void DeathReset()
	{
		m_SmoothCam.DeathReset();
	}

	public void UnInit()
	{
		if (m_Cameras != null)
		{
			m_Cameras.Clear();
		}
		m_Cameras = null;
		if (m_CameraManager != null)
		{
			iTweenEvent[] componentsInChildren = m_CameraManager.GetComponentsInChildren<iTweenEvent>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Clear();
			}
		}
	}
}
