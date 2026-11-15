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
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool bValidate
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float Height(float x, float z)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 Normal(float x, float y, float z)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Collider Collider(float x, float y, float z)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CollideGardbar(float x, float z)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CollideGardbar(float x1, float z1, float x2, float z2, float step)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UnInit()
	{
	}
}
