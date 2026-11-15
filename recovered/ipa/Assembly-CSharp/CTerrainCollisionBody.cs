using UnityEngine;

public class CTerrainCollisionBody : CDynamicCollisionBody
{
	private Collider m_kCollider;

	public CTerrainCollisionBody(GameObject colliderObject, EnmCollisionLayer clLayer)
	{
	}

	public override void Uninit()
	{
	}
}
