using UnityEngine;

namespace Misc;

public class ColliderUtil
{
	public static float Radius(Collider collider)
	{
		if (collider is BoxCollider)
		{
			BoxCollider boxCollider = collider as BoxCollider;
			return boxCollider.extents.magnitude;
		}
		if (collider is CapsuleCollider)
		{
			CapsuleCollider capsuleCollider = collider as CapsuleCollider;
			return capsuleCollider.radius + 0.2f;
		}
		Debug.LogError("ColliderUtil.Radius: Unknown collider type.");
		return 0f;
	}

	public static Collider GetNonTriggerColliderInChildren(GameObject root)
	{
		return GetNonTriggerColliderInChildren(root.transform);
	}

	public static Collider GetNonTriggerColliderInChildren(Transform root)
	{
		Collider[] componentsInChildren = root.GetComponentsInChildren<Collider>();
		if (componentsInChildren == null)
		{
			return null;
		}
		Collider[] array = componentsInChildren;
		foreach (Collider collider in array)
		{
			if (!collider.isTrigger)
			{
				return collider;
			}
		}
		return null;
	}
}
