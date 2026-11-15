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
		thisTransform = base.transform;
		if (disableCullingForTest)
		{
			Debug.LogWarning("Culling has been disabled!");
			return;
		}
		cullingAreas = (CullingArea_Manual[])Object.FindObjectsOfType(typeof(CullingArea_Manual));
		CullingGroup_Manual[] array = (CullingGroup_Manual[])Object.FindObjectsOfType(typeof(CullingGroup_Manual));
		allCullingGroups = new CullingAreaGroupSettings[array.Length];
		int num = 0;
		CullingGroup_Manual[] array2 = array;
		foreach (CullingGroup_Manual script in array2)
		{
			CullingAreaGroupSettings cullingAreaGroupSettings = new CullingAreaGroupSettings();
			cullingAreaGroupSettings.script = script;
			allCullingGroups[num] = cullingAreaGroupSettings;
			num++;
		}
	}

	private void OnPreCull()
	{
		if (disableCullingForTest)
		{
			return;
		}
		CullingAreaGroupSettings[] array = allCullingGroups;
		foreach (CullingAreaGroupSettings cullingAreaGroupSettings in array)
		{
			cullingAreaGroupSettings.cullingOptions = CullingOptions._;
		}
		Vector3 position = thisTransform.position;
		bool flag = false;
		CullingArea_Manual[] array2 = cullingAreas;
		foreach (CullingArea_Manual cullingArea_Manual in array2)
		{
			Transform transform = cullingArea_Manual.transform;
			Vector3 point = transform.InverseTransformPoint(position);
			if (new Bounds(Vector3.zero, new Vector3(1f, 1f, 1f)).Contains(point))
			{
				flag = true;
				EnableGroupsFromCollider(cullingArea_Manual);
			}
		}
		CullingAreaGroupSettings[] array3 = allCullingGroups;
		foreach (CullingAreaGroupSettings cullingAreaGroupSettings2 in array3)
		{
			if (cullingAreaGroupSettings2.cullingOptions == CullingOptions.Show || (!flag && (outsideAllAreas == CullingCameraOption_OutsideAllAreas.PrintErrorShowAllGroups || outsideAllAreas == CullingCameraOption_OutsideAllAreas.ShowAllGroups)))
			{
				StartCoroutine(cullingAreaGroupSettings2.script.StopCulling());
			}
			else
			{
				StartCoroutine(cullingAreaGroupSettings2.script.StartCulling());
			}
		}
		if (!flag && outsideAllAreas != CullingCameraOption_OutsideAllAreas.PrintErrorHideAllGroups && outsideAllAreas != CullingCameraOption_OutsideAllAreas.PrintErrorShowAllGroups)
		{
		}
	}

	private void EnableGroupsFromCollider(CullingArea_Manual area)
	{
		if (disableCullingForTest)
		{
			return;
		}
		foreach (CullingAreaGroupSettings groups in area.groupsList)
		{
			if (groups.cullingOptions == CullingOptions.Show)
			{
				CullingAreaGroupSettings[] array = allCullingGroups;
				foreach (CullingAreaGroupSettings cullingAreaGroupSettings in array)
				{
					if (cullingAreaGroupSettings.script == groups.script)
					{
						if (cullingAreaGroupSettings.cullingOptions != CullingOptions.AlwaysHide)
						{
							cullingAreaGroupSettings.cullingOptions = CullingOptions.Show;
						}
						break;
					}
				}
			}
			else
			{
				if (groups.cullingOptions != CullingOptions.AlwaysHide)
				{
					continue;
				}
				CullingAreaGroupSettings[] array2 = allCullingGroups;
				foreach (CullingAreaGroupSettings cullingAreaGroupSettings2 in array2)
				{
					if (cullingAreaGroupSettings2.script == groups.script)
					{
						cullingAreaGroupSettings2.cullingOptions = CullingOptions.AlwaysHide;
						break;
					}
				}
			}
		}
	}
}
