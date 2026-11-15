using UnityEngine;

public class FollowCamera
{
	private static FollowCamera instance;

	public Car m_HeroCar;

	private Vector3 m_OffsetTarget;

	public float m_OffsetY;

	public float m_HitWallDamping;

	public float m_fDampingK;

	public float m_fSpringK;

	public bool m_bStay;

	public float m_fStaytime;

	public float m_fZScale;

	public Vector3 m_Assist;

	private bool bEnableShiftScaleAdjust;

	private float fShiftScale;

	private float CameraDisScale;

	private bool m_PreBoost;

	private float m_fCameraShift;

	public bool m_bCameraShiftLook;

	public static FollowCamera Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void ShiftScaleAdjust(bool enable, float fFactor)
	{
	}

	public bool UpdateCamera(CameraData camdata)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetCameraShift(float shift)
	{
	}

	public void LockCameraShift(bool bEnable)
	{
	}

	public bool FixUpdateCamera(CameraData camdata)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ForceCameraNormal(CameraData camdata)
	{
	}

	public void EnterCamera(CameraData camdata)
	{
	}
}
