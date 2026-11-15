using UnityEngine;

public class CameraData
{
	public Transform m_Target;

	public Transform m_TargetLogic;

	public Vector3 m_position;

	public Vector3 m_TargetPosition;

	public float m_Sourcex;

	public float m_Sourcey;

	public float m_Sourcez;

	public float m_LerpSourcey;

	public float m_LerpSourcez;

	public float m_Tarx;

	public float m_Tary;

	public float m_Tarz;

	public float m_near;

	public float m_FieldView;

	public CameraState m_CameraStateSelf;

	public bool m_bFollowCharacter;

	public bool m_FlagHitWall;

	public Vector3 m_Right;

	public float fovScale;

	public void Reset()
	{
	}

	public CameraState GetState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void DeathReset()
	{
	}

	public void UnInit()
	{
	}
}
