using UnityEngine;

public class CullingCamera_Manual : MonoBehaviour
{
	private CullingArea_Manual[] cullingAreas;

	private CullingAreaGroupSettings[] allCullingGroups;

	private Transform thisTransform;

	public CullingCameraOption_OutsideAllAreas outsideAllAreas;

	public bool disableCullingForTest;

	private void Awake()
	{
	}

	private void OnPreCull()
	{
	}

	private void EnableGroupsFromCollider(CullingArea_Manual area)
	{
	}
}
