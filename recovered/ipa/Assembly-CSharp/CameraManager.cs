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
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Init()
	{
	}

	public float GetAllTime()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Update()
	{
	}

	public void ChangeCamera(string newCamera)
	{
	}

	public void FadeInAndOut()
	{
	}

	public void ChangeNormal()
	{
	}

	public void ChangeMainCamera()
	{
	}

	public void SetMainState(MainCameraState state)
	{
	}

	public void EnterCamera()
	{
	}

	public void ForceCameraNormal()
	{
	}

	public void ChangeCameraMode(CameraMode mode)
	{
	}

	public void DeathReset()
	{
	}

	public void UnInit()
	{
	}
}
