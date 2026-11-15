using UnityEngine;

public class HeightMapManager
{
	private static HeightMapManager instance;

	public float[] m_HeightMap;

	public Vector3[] m_NormalMap;

	public Collider[] m_ColliderMap;

	public bool[] m_CollideGardbar;

	public int m_Left;

	public int m_Right;

	public int m_Top;

	public int m_Bottom;

	public bool Inited;

	public static HeightMapManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new HeightMapManager();
			}
			return instance;
		}
	}

	public bool bValidate => Inited;

	public float Height(float x, float z)
	{
		if (Instance.m_HeightMap == null)
		{
			return -1000f;
		}
		int num = (int)(x + 0.5f);
		int num2 = (int)(z + 0.5f);
		int num3 = (num2 - m_Bottom) * (m_Right - m_Left) + (num - m_Left);
		if (num3 < Instance.m_HeightMap.Length && num3 >= 0)
		{
			return Instance.m_HeightMap[num3];
		}
		return -1000f;
	}

	public Vector3 Normal(float x, float y, float z)
	{
		if (Instance.m_NormalMap == null)
		{
			return Vector3.up;
		}
		int num = Mathf.RoundToInt(x);
		int num2 = Mathf.RoundToInt(z);
		Vector3 result = Vector3.up;
		int num3 = (num2 - m_Bottom) * (m_Right - m_Left) + (num - m_Left);
		if (num3 < Instance.m_NormalMap.Length && num3 >= 0)
		{
			result = Instance.m_NormalMap[num3];
		}
		return result;
	}

	public Collider Collider(float x, float y, float z)
	{
		Collider result = null;
		int num = (int)(x + 0.5f);
		int num2 = (int)(z + 0.5f);
		int num3 = (num2 - m_Bottom) * (m_Right - m_Left) + (num - m_Left);
		if (num3 < Instance.m_ColliderMap.Length && num3 >= 0)
		{
			result = Instance.m_ColliderMap[num3];
		}
		return result;
	}

	public bool CollideGardbar(float x, float z)
	{
		if (Instance.m_CollideGardbar == null)
		{
			return true;
		}
		int num = (int)(x + 0.5f);
		int num2 = (int)(z + 0.5f);
		int num3 = (num2 - m_Bottom) * (m_Right - m_Left) + (num - m_Left);
		if (num3 >= 0 && num3 <= Instance.m_CollideGardbar.Length)
		{
			return Instance.m_CollideGardbar[num3];
		}
		return true;
	}

	public bool CollideGardbar(float x1, float z1, float x2, float z2, float step)
	{
		if (Instance.m_CollideGardbar == null)
		{
			return true;
		}
		Vector2 vector = new Vector2(x1, z1);
		Vector2 vector2 = new Vector3(x2, z2);
		Vector2 vector3 = vector2 - vector;
		float magnitude = vector3.magnitude;
		vector3.Normalize();
		bool result = false;
		for (float num = 0f; num < magnitude; num += step)
		{
			float num2 = vector.x + vector3.x * num;
			float num3 = vector.y + vector3.y * num;
			int num4 = (int)(num2 + 0.5f);
			int num5 = (int)(num3 + 0.5f);
			int num6 = (num5 - m_Bottom) * (m_Right - m_Left) + (num4 - m_Left);
			if (num6 >= 0 && num6 < Instance.m_CollideGardbar.Length && Instance.m_CollideGardbar[num6])
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void UnInit()
	{
		Instance.m_HeightMap = null;
		Instance.m_NormalMap = null;
		Instance.m_ColliderMap = null;
		Instance.m_CollideGardbar = null;
		Instance.Inited = false;
	}
}
