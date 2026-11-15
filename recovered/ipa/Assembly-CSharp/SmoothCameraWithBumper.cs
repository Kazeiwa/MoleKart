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

	private float CameraFovMod;

	private void Awake()
	{
	}

	public void SetCameraMode(CameraMode mode)
	{
	}

	public void DeathReset()
	{
	}

	public void ResetCamera()
	{
	}

	private void OnPreCull()
	{
	}

	public void UpdateEx()
	{
	}

	public void FixedUpdateEx()
	{
	}

	public void ForceCameraNormal()
	{
	}

	public void EnterCamera()
	{
	}

	public void ChangePosition()
	{
	}

	private void OnDestroy()
	{
	}

	private void SetAllNPCVisible()
	{
	}
}
