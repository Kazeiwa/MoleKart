using UnityEngine;

public class CStaticCollisionBody : CCollisionBody
{
	public float A;

	public float B;

	public float C;

	public float D;

	public float m_height;

	public float m_angle;

	public float m_maxZ;

	public CStaticCollisionBody(GameObject gameobject, EnmCollisionLayer clLayer)
	{
	}

	public CStaticCollisionBody(GameObject gameobject, EnmCollisionLayer clLayer, float fRadius)
	{
	}

	public override void Uninit()
	{
	}

	public float GetGroundY(float posX, float posZ)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
