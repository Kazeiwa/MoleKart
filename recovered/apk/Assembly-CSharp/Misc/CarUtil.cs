using UnityEngine;

namespace Misc;

public class CarUtil
{
	public static Car FindCarInCollision(Collider other)
	{
		Transform transform = FindTransformOfCarInCollision(other);
		return (!(transform == null)) ? transform.GetComponent<Car>() : null;
	}

	public static NPCCar FindNpcCarInCollision(Collider other)
	{
		Transform transform = FindTransformOfCarInCollision(other);
		return (!(transform == null)) ? transform.GetComponent<NPCCar>() : null;
	}

	private static Transform FindTransformOfCarInCollision(Collider other)
	{
		Transform parent = other.transform.parent;
		if (parent == null || parent.name != "collision")
		{
			return null;
		}
		return parent.parent;
	}
}
