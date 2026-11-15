using UnityEngine;

public class FollowCamera
{
	private static FollowCamera instance;

	public Car m_HeroCar;

	private Vector3 m_OffsetTarget = Vector3.zero;

	public float m_OffsetY = 2f;

	public float m_HitWallDamping = 3f;

	public float m_fDampingK = 1f;

	public float m_fSpringK = 150f;

	public bool m_bStay;

	public float m_fStaytime;

	public float m_fZScale = 1f;

	public Vector3 m_Assist = Vector3.zero;

	private bool bEnableShiftScaleAdjust;

	private float fShiftScale = 1f;

	private bool m_PreBoost;

	private float m_fCameraShift;

	public bool m_bCameraShiftLook;

	public static FollowCamera Instance
	{
		get
		{
			if (instance == null)
			{
				new FollowCamera();
			}
			return instance;
		}
	}

	public FollowCamera()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void ShiftScaleAdjust(bool enable, float fFactor)
	{
		bEnableShiftScaleAdjust = enable;
		fShiftScale = fFactor;
	}

	public bool UpdateCamera(CameraData camdata)
	{
		if (camdata.m_CameraStateSelf == CameraState.CarDeath)
		{
			return false;
		}
		if (camdata.m_CameraStateSelf == CameraState.CarLeftRightBound)
		{
			if (camdata.m_CameraStateSelf == CameraState.CarDeath)
			{
				camdata.m_Sourcey = Mathf.Lerp(camdata.m_Sourcey, camdata.m_LerpSourcey, Time.deltaTime);
				camdata.m_Sourcez = Mathf.Lerp(camdata.m_Sourcez, camdata.m_LerpSourcez, Time.deltaTime);
			}
			float num = 1f;
			if (bEnableShiftScaleAdjust)
			{
				num = fShiftScale;
			}
			if (m_HeroCar.vehicle.GetBoostMode())
			{
				m_PreBoost = true;
				m_fZScale += Time.deltaTime * num;
			}
			else
			{
				if (camdata.m_CameraStateSelf != CameraState.CamBecomeMissile)
				{
					m_PreBoost = false;
				}
				m_fZScale -= Time.deltaTime * 0.75f * num;
			}
			if (m_fZScale > 1f + 0.5f * num)
			{
				m_fZScale = 1f + 0.5f * num;
			}
			else if (m_fZScale < 1f)
			{
				m_fZScale = 1f;
			}
			Vector3 vector = camdata.m_Target.TransformPoint(camdata.m_Sourcex, 0f, camdata.m_Sourcez * m_fZScale);
			vector -= camdata.m_Target.position;
			vector.y = 0f;
			vector.Normalize();
			vector *= Mathf.Abs(camdata.m_Sourcez * m_fZScale);
			vector += camdata.m_Target.position;
			vector.y += camdata.m_Sourcey;
			m_OffsetTarget.x = camdata.m_Tarx;
			m_OffsetTarget.y = camdata.m_Tary;
			m_OffsetTarget.z = camdata.m_Tarz;
			Vector3 vector2 = camdata.m_Target.position + m_OffsetTarget;
			Vector3 vDir = vector - vector2;
			float magnitude = vDir.magnitude;
			vDir.Normalize();
			Vector3 position = vector;
			if (CollisionLayer.Instance.RayCastHitToGuardbar(vector2, vDir, magnitude, out var vPos, out var _))
			{
				position = vPos;
				position.y = vector.y;
				camdata.m_near = 0.08f;
			}
			else
			{
				camdata.m_near = 0.08f;
			}
			camdata.m_position = position;
			camdata.m_TargetPosition = camdata.m_Target.position + m_OffsetTarget;
			return true;
		}
		if (camdata.m_CameraStateSelf == CameraState.CamCarBounce)
		{
			Vector3 position2 = camdata.m_Target.TransformPoint(camdata.m_Sourcex, camdata.m_Sourcey, camdata.m_Sourcez);
			m_OffsetTarget.x = camdata.m_Tarx;
			m_OffsetTarget.y = camdata.m_Tary;
			m_OffsetTarget.z = camdata.m_Tarz;
			camdata.m_position = position2;
			camdata.m_TargetPosition = camdata.m_Target.position + m_OffsetTarget;
			return true;
		}
		return false;
	}

	public void SetCameraShift(float shift)
	{
		m_fCameraShift = shift;
	}

	public void LockCameraShift(bool bEnable)
	{
		m_bCameraShiftLook = bEnable;
	}

	public bool FixUpdateCamera(CameraData camdata)
	{
		if (camdata.m_CameraStateSelf == CameraState.CarDeath)
		{
			camdata.m_position.y += 3f * Time.fixedDeltaTime;
			return true;
		}
		if (camdata.m_CameraStateSelf == CameraState.CarLeftRightBound || camdata.m_CameraStateSelf == CameraState.CarDeath || camdata.m_CameraStateSelf == CameraState.CamCarBounce)
		{
			return false;
		}
		float num = 1f;
		if (bEnableShiftScaleAdjust)
		{
			num = fShiftScale;
		}
		if (m_HeroCar.vehicle.GetBoostMode())
		{
			m_PreBoost = true;
			m_fZScale += Time.deltaTime * num;
		}
		else
		{
			if (camdata.m_CameraStateSelf != CameraState.CamBecomeMissile)
			{
				m_PreBoost = false;
			}
			m_fZScale -= Time.deltaTime * 0.75f * num;
		}
		if (m_fZScale > 1f + 0.5f * num)
		{
			m_fZScale = 1f + 0.5f * num;
		}
		else if (m_fZScale < 1f)
		{
			m_fZScale = 1f;
		}
		Vector3 vecGlobal = m_HeroCar.GetVecGlobal();
		Vector3 vector = camdata.m_Target.TransformPoint(camdata.m_Sourcex + m_fCameraShift * 1f, 0f, camdata.m_Sourcez * m_fZScale);
		vector -= camdata.m_Target.position;
		vector.y = 0f;
		vector.Normalize();
		vector *= Mathf.Abs(camdata.m_Sourcez * m_fZScale);
		vector += camdata.m_Target.position;
		vector.y += camdata.m_Sourcey;
		m_OffsetTarget.x = camdata.m_Tarx;
		m_OffsetTarget.y = camdata.m_Tary;
		m_OffsetTarget.z = camdata.m_Tarz;
		Vector3 vector2 = camdata.m_Target.position + m_OffsetTarget;
		Vector3 vDir = vector - vector2;
		float magnitude = vDir.magnitude;
		vDir.Normalize();
		Vector3 vector3 = vector;
		if (CollisionLayer.Instance.RayCastHitToGuardbar(vector2, vDir, magnitude, out var vPos, out var vNormal))
		{
			Vector3 vector4 = vNormal * (camdata.m_near + 0.5f) * Mathf.Tan(camdata.m_FieldView / 2f);
			vector3 = vPos + vector4;
			vector3.y = vector.y + camdata.m_Tary;
			camdata.m_FlagHitWall = true;
			camdata.m_near = 0.08f;
		}
		else
		{
			camdata.m_near = 0.08f;
			camdata.m_FlagHitWall = false;
		}
		if (camdata.m_CameraStateSelf == CameraState.CamBound)
		{
			camdata.m_position = vector3;
			m_Assist.x = camdata.m_TargetLogic.position.x;
			m_Assist.y = camdata.m_TargetLogic.position.y;
			m_Assist.z = camdata.m_TargetLogic.position.z;
			camdata.m_TargetPosition = m_Assist + m_OffsetTarget;
			return true;
		}
		if (camdata.m_CameraStateSelf == CameraState.CamBecomeMissile)
		{
			if (m_HeroCar.vehicle.RunningMode == RUNNING_MDOE.RM_SIDESLIP)
			{
				camdata.m_position = vector3;
				camdata.m_TargetPosition = camdata.m_Target.position + m_OffsetTarget;
				return true;
			}
			camdata.m_position = Vector3.Lerp(camdata.m_position, vector3, Time.deltaTime * 5f);
			camdata.m_TargetPosition = camdata.m_Target.position + m_OffsetTarget;
			if (m_PreBoost)
			{
				camdata.m_position.y = camdata.m_TargetPosition.y + 3.5f;
			}
			return true;
		}
		if (m_bStay)
		{
			m_fStaytime -= Time.deltaTime;
			if (m_fStaytime <= 0f)
			{
				m_bStay = false;
				m_fStaytime = 0f;
			}
		}
		else
		{
			CameraState state = camdata.GetState();
			if (state == CameraState.CamHitWall)
			{
				camdata.m_position = Vector3.Lerp(camdata.m_position, vector3, Time.deltaTime * m_HitWallDamping);
			}
			else if (!camdata.m_bFollowCharacter)
			{
				if (m_HeroCar.vehicle.RunningMode == RUNNING_MDOE.RM_SIDESLIP)
				{
					camdata.m_position = vector3;
					Vector3 vector5 = camdata.m_position - vector3;
					Vector3 vector6 = (0f - m_fSpringK) * vector5 - m_fDampingK * vecGlobal;
					vecGlobal += vector6 * Time.deltaTime;
					camdata.m_position += vecGlobal * Time.deltaTime;
				}
				else
				{
					Vector3 vector7 = camdata.m_position - vector3;
					Vector3 vector8 = (0f - m_fSpringK) * vector7 - m_fDampingK * vecGlobal;
					vecGlobal += vector8 * Time.deltaTime;
					camdata.m_position += vecGlobal * Time.deltaTime;
				}
			}
			else
			{
				camdata.m_position = vector3;
			}
			camdata.m_TargetPosition = camdata.m_Target.position + m_OffsetTarget;
		}
		return true;
	}

	public void ForceCameraNormal(CameraData camdata)
	{
		camdata.m_Sourcex = 0f;
		camdata.m_Sourcey = 3.8f;
		camdata.m_Sourcez = -4.64f;
		camdata.m_Tarx = 0f;
		camdata.m_Tary = 2.4f;
		camdata.m_Tarz = 0f;
		camdata.m_CameraStateSelf = CameraState.CamNormal;
		camdata.m_bFollowCharacter = false;
		camdata.m_TargetLogic = (camdata.m_Target = m_HeroCar.transform);
	}

	public void EnterCamera(CameraData camdata)
	{
		if ((m_HeroCar.CarState & 0x8000) != 0)
		{
			camdata.m_LerpSourcey = 5.8f;
			camdata.m_LerpSourcez = -5.64f;
			camdata.m_CameraStateSelf = CameraState.CarDeath;
			return;
		}
		if ((m_HeroCar.CarState & 0x10) != 0)
		{
			camdata.m_CameraStateSelf = CameraState.CarLeftRightBound;
			return;
		}
		if ((m_HeroCar.CarState & 0x10000) != 0)
		{
			camdata.m_CameraStateSelf = CameraState.CarLeftRightBound;
			return;
		}
		if ((m_HeroCar.CarState & 0x3000) != 0)
		{
			camdata.m_CameraStateSelf = CameraState.CarLeftRightBound;
			return;
		}
		if ((m_HeroCar.CarState & 0x60000) != 0)
		{
			camdata.m_CameraStateSelf = CameraState.CamCarBounce;
			return;
		}
		if ((m_HeroCar.CarState & 0x200) != 0)
		{
			camdata.m_Sourcex = 0f;
			camdata.m_Sourcey = 3.8f;
			camdata.m_Sourcez = -4.64f;
			camdata.m_Tarx = 0f;
			camdata.m_Tary = 2.4f;
			camdata.m_Tarz = 0f;
			camdata.m_CameraStateSelf = CameraState.CamBound;
			camdata.m_bFollowCharacter = true;
			Transform targetLogic = Utility.FindChildTranform(m_HeroCar.transform, "car");
			camdata.m_TargetLogic = targetLogic;
			return;
		}
		if ((m_HeroCar.CarState & 0x400) != 0)
		{
			camdata.m_CameraStateSelf = CameraState.CamAccelerate;
			return;
		}
		if (m_HeroCar.CarState == 32)
		{
			camdata.m_Sourcez = 0f;
			camdata.m_CameraStateSelf = CameraState.CamBecomeMissile;
			return;
		}
		if ((m_HeroCar.CarState & 0x80) != 0)
		{
			camdata.m_Sourcez = -6f;
			camdata.m_Sourcey = 4f;
			camdata.m_CameraStateSelf = CameraState.CamScaleBig;
			return;
		}
		if ((m_HeroCar.CarState & 0x4800) != 0)
		{
			camdata.m_CameraStateSelf = CameraState.CamThunderCloud;
			Transform transform = m_HeroCar.transform;
			camdata.m_Target = transform;
			return;
		}
		camdata.m_Sourcex = 0f;
		camdata.m_Sourcey = 3.8f;
		camdata.m_Sourcez = -4.64f;
		camdata.m_Tarx = 0f;
		camdata.m_Tary = 2.4f;
		camdata.m_Tarz = 0f;
		if (camdata.m_CameraStateSelf == CameraState.CamBound || camdata.m_CameraStateSelf == CameraState.CarLeftRightBound || camdata.m_CameraStateSelf == CameraState.CarDeath)
		{
			m_bStay = true;
			m_fStaytime = 0.1f;
		}
		camdata.m_CameraStateSelf = CameraState.CamNormal;
		camdata.m_bFollowCharacter = false;
		camdata.m_TargetLogic = (camdata.m_Target = m_HeroCar.transform);
	}
}
