using UnityEngine;

namespace Misc;

public struct BareTransform
{
	public Vector3 Position;

	public Quaternion Rotation;

	public BareTransform(Vector3 position, Quaternion rotation)
	{
		Position = position;
		Rotation = rotation;
	}

	public static BareTransform FromTransform(Transform transform)
	{
		return new BareTransform(transform.position, transform.rotation);
	}

	public static BareTransform FromGameObject(GameObject gameObject)
	{
		return FromTransform(gameObject.transform);
	}
}
