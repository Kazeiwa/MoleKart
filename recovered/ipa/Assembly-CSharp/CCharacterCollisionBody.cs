using UnityEngine;

public class CCharacterCollisionBody : CDynamicCollisionBody
{
	private Rigidbody m_kRigidBody;

	public CCharacterCollisionBody(GameObject colliderObject, EnmCollisionLayer clLayer)
	{
	}

	public override void Uninit()
	{
	}
}
