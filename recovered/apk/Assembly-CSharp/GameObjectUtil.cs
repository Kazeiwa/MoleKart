using UnityEngine;

public class GameObjectUtil
{
	public static T[] GetComponentsInChildren<T>(string name) where T : Component
	{
		GameObject gameObject = GameObject.Find(name);
		if (gameObject == null)
		{
			return null;
		}
		return gameObject.GetComponentsInChildren<T>();
	}

	public static GameObject CreateEmptyUnderGameObject(Vector3 position, Quaternion rotation, string name, GameObject parent)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = position;
		gameObject.transform.rotation = rotation;
		if (parent != null)
		{
			gameObject.transform.parent = parent.transform;
		}
		return gameObject;
	}

	public static GameObject CreateEmptyUnderTransform(Vector3 localPosition, Quaternion localRotation, string name, Transform parent)
	{
		GameObject gameObject = new GameObject(name);
		if (parent != null)
		{
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = localPosition;
			gameObject.transform.localRotation = localRotation;
			gameObject.transform.localScale = Vector3.one;
		}
		return gameObject;
	}

	public static GameObject InsertEmtpyParent(GameObject go, string name)
	{
		return InsertEmtpyParent(go.transform, name);
	}

	public static GameObject InsertEmtpyParent(Transform transform, string name)
	{
		Transform parent = transform.parent;
		GameObject gameObject = CreateEmptyUnderTransform(Vector3.zero, Quaternion.identity, name, parent);
		transform.parent = gameObject.transform;
		return gameObject;
	}

	public static T InsertGameObject<T>(Vector3 localPosition, Quaternion localRotation, string name, Transform parent) where T : Component
	{
		GameObject gameObject = CreateEmptyUnderTransform(localPosition, localRotation, name, parent);
		return gameObject.AddComponent<T>();
	}
}
