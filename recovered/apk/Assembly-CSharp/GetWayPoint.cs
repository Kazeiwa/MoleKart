using System.IO;
using UnityEngine;

public class GetWayPoint : MonoBehaviour
{
	public GameObject m_WayPoints;

	private void OnGUI()
	{
		if (GUI.Button(new Rect(100f, 100f, 200f, 100f), "Create way points") && null != m_WayPoints)
		{
			WriteWaypoint(m_WayPoints, "hello.txt");
		}
	}

	private static void WriteWaypoint(GameObject obj, string fileName)
	{
		if (null == obj)
		{
			return;
		}
		StreamWriter streamWriter = new StreamWriter(fileName);
		if (streamWriter == null)
		{
			return;
		}
		Transform transform = obj.transform.FindChild("MainWay");
		if (null != transform)
		{
			int childCount = transform.GetChildCount();
			if (childCount > 0)
			{
				streamWriter.WriteLine(transform.name);
				for (int i = 0; i < childCount; i++)
				{
					Transform transform2 = transform.FindChild(string.Empty + i);
					if (null != transform2)
					{
						Vector3 position = transform2.position;
						position.y = 1000f;
						if (CollisionLayer.Instance.VerticalIntersectHitPosition(position, out var _, out var vHitPoint))
						{
							string value = string.Empty + transform2.position.x + '\t' + (vHitPoint.y + 0.5f) + '\t' + transform2.position.z;
							streamWriter.WriteLine(value);
						}
						else
						{
							Debug.Log(transform2.gameObject.name + "---------" + transform2.name);
						}
					}
				}
			}
		}
		for (int j = 1; j < 20; j++)
		{
			Transform transform3 = obj.transform.FindChild("Branch_" + j);
			if (!(null != transform3))
			{
				continue;
			}
			int childCount2 = transform3.GetChildCount();
			if (childCount2 <= 0)
			{
				continue;
			}
			streamWriter.WriteLine(transform3.name);
			for (int k = 0; k < childCount2; k++)
			{
				Transform transform4 = transform3.FindChild(string.Empty + k);
				if (null != transform4)
				{
					Vector3 position2 = transform4.position;
					position2.y = 1000f;
					if (CollisionLayer.Instance.VerticalIntersectHitPosition(position2, out var _, out var vHitPoint2))
					{
						string value2 = string.Empty + transform4.position.x + '\t' + (vHitPoint2.y + 0.5f) + '\t' + transform4.position.z;
						streamWriter.WriteLine(value2);
					}
					else
					{
						Debug.Log(transform4.gameObject.name + "---------" + transform4.name);
					}
				}
			}
		}
		streamWriter.Close();
	}
}
