using UnityEngine;

public class CCharacterCollisionBody : CDynamicCollisionBody
{
	private Rigidbody m_kRigidBody;

	public CCharacterCollisionBody(GameObject colliderObject, EnmCollisionLayer clLayer)
		: base(colliderObject, clLayer)
	{
		Debug.Log("CCharacterCollisionBody: Init = " + colliderObject.name);
		m_kRigidBody = colliderObject.AddComponent<Rigidbody>();
		m_kRigidBody.useGravity = false;
		m_kRigidBody.mass = float.PositiveInfinity;
	}

	public override void Uninit()
	{
		base.Uninit();
	}
}
