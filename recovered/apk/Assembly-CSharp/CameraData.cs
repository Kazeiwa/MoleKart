using UnityEngine;

public class CameraData
{
	public Transform m_Target;

	public Transform m_TargetLogic;

	public Vector3 m_position;

	public Vector3 m_TargetPosition;

	public float m_Sourcex;

	public float m_Sourcey = 3.8f;

	public float m_Sourcez = -4.64f;

	public float m_LerpSourcey = 3.8f;

	public float m_LerpSourcez = -4.64f;

	public float m_Tarx;

	public float m_Tary = 2.4f;

	public float m_Tarz;

	public float m_near;

	public float m_FieldView;

	public CameraState m_CameraStateSelf;

	public bool m_bFollowCharacter;

	public bool m_FlagHitWall;

	public Vector3 m_Right;

	public void Reset()
	{
		m_Target = null;
		m_TargetLogic = null;
		m_TargetPosition = Vector3.zero;
		m_position = Vector3.zero;
		m_Sourcex = 0f;
		m_Sourcey = 3.8f;
		m_Sourcez = -4.64f;
		m_Tarx = 0f;
		m_Tary = 2.4f;
		m_Tarz = 0f;
		m_CameraStateSelf = CameraState.CamNormal;
		m_bFollowCharacter = false;
		m_FlagHitWall = false;
		m_near = 0.5f;
	}

	public CameraState GetState()
	{
		if (m_FlagHitWall)
		{
			return CameraState.CamHitWall;
		}
		if (CameraManager.instance.m_CamState == CameraManager.MainCameraState.StaticState)
		{
			return CameraState.CamStatic;
		}
		if (CameraManager.instance.m_CamState == CameraManager.MainCameraState.AniState)
		{
			return CameraState.CamAni;
		}
		return CameraState.CamNormal;
	}

	public void DeathReset()
	{
		m_Sourcey = 3.8f;
		m_Sourcez = -4.64f;
		m_LerpSourcey = 3.8f;
		m_LerpSourcez = -4.64f;
	}

	public void UnInit()
	{
		m_Target = null;
		m_TargetLogic = null;
	}
}
