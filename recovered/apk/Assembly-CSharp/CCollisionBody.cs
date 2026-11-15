using RaceTrack;
using UnityEngine;

public class CCollisionBody
{
	public Vector3 m_center;

	public Vector3 m_forward;

	public Vector3 m_right;

	public int m_forwardRadius;

	public int m_rightRadius;

	public int m_upRadius;

	public int m_boundingRadius;

	public int m_inactiveRadius;

	public CollisionBodyType m_type;

	public GameObject m_GameObject;

	public Transform m_Transform;

	public EnmCollisionLayer m_clLayer;

	public Vector3 CollisionNormal;

	public Collider m_Collider;

	protected IObstacle m_iObstacle;

	public CCollisionBody(GameObject gameobject, EnmCollisionLayer clLayer)
	{
		m_GameObject = gameobject;
		m_Transform = gameobject.transform;
		m_clLayer = clLayer;
		m_Collider = m_Transform.GetComponentInChildren<Collider>();
		CollisionNormal = Vector3.zero;
		CollisionLayer.Instance.AddCollider(this);
	}

	~CCollisionBody()
	{
		Uninit();
	}

	public virtual void Uninit()
	{
		CollisionLayer.Instance.RemoveCollider(this);
	}

	public Transform GetPosition()
	{
		return m_Transform;
	}

	public GameObject GetGameObject()
	{
		return m_GameObject;
	}

	public Collider GetCollider()
	{
		return m_Collider;
	}

	public IObstacle GetIObstacle()
	{
		return m_iObstacle;
	}
}
