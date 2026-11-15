using UnityEngine;

public class CTerrainCollisionBody : CDynamicCollisionBody
{
	private Collider m_kCollider;

	public CTerrainCollisionBody(GameObject colliderObject, EnmCollisionLayer clLayer)
		: base(colliderObject, clLayer)
	{
		m_kCollider = colliderObject.GetComponent<Collider>();
		m_kCollider.isTrigger = true;
	}

	public override void Uninit()
	{
		base.Uninit();
	}
}
