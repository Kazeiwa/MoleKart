using System.IO;
using UnityEngine;

public class GetPropBoxWaypoints : MonoBehaviour
{
	public GameObject m_WayPoints;

	private void OnGUI()
	{
		if (GUI.Button(new Rect(300f, 100f, 200f, 100f), "Create points file") && null != m_WayPoints)
		{
			WriteWaypoint(m_WayPoints, "propBox.txt");
		}
	}

	private static void WriteWaypoint(GameObject obj, string fileName)
	{
		if (null == obj)
		{
			return;
		}
		Transform[] componentsInChildren = obj.GetComponentsInChildren<Transform>();
		StreamWriter streamWriter = new StreamWriter(fileName);
		if (streamWriter == null)
		{
			return;
		}
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform != obj.transform && null != transform.GetComponent<BoxCollider>())
			{
				Vector3 position = transform.position;
				Vector3 vGroundNormal;
				Vector3 vHitPoint;
				bool flag = CollisionLayer.Instance.VerticalIntersectHitPosition(position, out vGroundNormal, out vHitPoint);
				string value = string.Empty + transform.position.x + '\t' + transform.position.y + '\t' + transform.position.z;
				streamWriter.WriteLine(value);
			}
		}
		streamWriter.Close();
	}
}
