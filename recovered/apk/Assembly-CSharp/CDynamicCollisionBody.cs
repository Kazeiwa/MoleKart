using UnityEngine;

public class CDynamicCollisionBody : CCollisionBody
{
	public float A;

	public float B;

	public float C;

	public float D;

	public float m_height;

	public float m_angle;

	public float m_maxZ;

	public CDynamicCollisionBody(GameObject gameobject, EnmCollisionLayer clLayer)
		: base(gameobject, clLayer)
	{
		m_type = CollisionBodyType.CBT_DYNAMIC;
		m_height = 0f;
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
