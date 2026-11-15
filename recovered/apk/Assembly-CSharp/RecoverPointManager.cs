using System.Collections;
using UnityEngine;

public class RecoverPointManager
{
	private static RecoverPointManager instance;

	public ArrayList m_RecoverPoints = new ArrayList();

	public static RecoverPointManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new RecoverPointManager();
			}
			return instance;
		}
	}

	public Vector3 GetRecoverPoint(Vector3 pos)
	{
		Vector2 vector = new Vector2(pos.x, pos.z);
		int num = -1;
		float num2 = float.MaxValue;
		int num3 = 0;
		while (m_RecoverPoints != null && num3 < m_RecoverPoints.Count)
		{
			Vector2 vector2 = (Vector2)m_RecoverPoints[num3];
			if ((vector2 - vector).magnitude < num2)
			{
				num2 = (vector2 - vector).magnitude;
				num = num3;
			}
			num3++;
		}
		if (num < 0)
		{
			return Vector3.zero;
		}
		vector = (Vector2)m_RecoverPoints[num];
		Vector3 result = new Vector3(vector.x, HeightMapManager.Instance.Height(vector.x, vector.y) + 0.3f, vector.y);
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			Vector2 pos2 = TSingleton<CarManager>.Instance.GetCarByIndex(i).Pos;
			if ((pos2 - vector).magnitude < 3f)
			{
				if (i % 2 == 0)
				{
					result.x -= 1.5f;
					result.z -= 1.5f;
				}
				else
				{
					result.x += 1.5f;
					result.z += 1.5f;
				}
				break;
			}
		}
		return result;
	}
}
