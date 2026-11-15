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
	}

	~CCollisionBody()
	{
	}

	public virtual void Uninit()
	{
	}

	public Transform GetPosition()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public GameObject GetGameObject()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Collider GetCollider()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IObstacle GetIObstacle()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
