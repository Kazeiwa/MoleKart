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
		: base(gameobject, clLayer)
	{
		m_type = CollisionBodyType.CBT_STATIC;
		m_height = 0f;
	}

	public CStaticCollisionBody(GameObject gameobject, EnmCollisionLayer clLayer, float fRadius)
		: base(gameobject, clLayer)
	{
		m_type = CollisionBodyType.CBT_STATIC;
		m_height = 0f;
		m_iObstacle = new Obstacle2D(gameobject.transform, fRadius);
	}

	public override void Uninit()
	{
		base.Uninit();
	}

	public float GetGroundY(float posX, float posZ)
	{
		return (0f - (D + A * posX + C * posZ)) / B;
	}
}
