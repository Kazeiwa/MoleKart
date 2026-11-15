using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	public const float ROTATION_LOCK_TIME = 0.5f;

	public const float MAX_BOUCING_VEL = 15f;

	public const float UP_VEL_COEFF = 0.3f;

	public const float BACK_VEL_COEFF = 0.3f;

	public const float MIN_BOUCING_VEL = 25f;

	public const float MIN_BOUCING_SIDEVEL = 15f;

	public const float FORCE_ROT_ANGEL_T = 0.08726646f;

	public bool _EnableHandleColWithNpc;

	private ChassisSimulator m_chassis;

	private Vehicle m_car;

	private Collider[] m_collider;

	private Rigidbody m_rigid;

	private float m_colAccumTime;

	private bool m_lockRotation;

	public readonly float REVERSE_LOCK_OFFSET_TWEAK;

	private ICollisionCallback callback;

	public void Init(Vehicle car, Collider colliderHead, Collider colliderTail, Collider colliderBody, GameObject obj, ICollisionCallback callback)
	{
	}

	public void OnTriggerEnter(Collider c)
	{
	}

	public void OnTriggerStay(Collider c)
	{
	}

	public void OnTriggerExit(Collider c)
	{
	}

	public void OnCollisionEnter(Collision collision)
	{
	}

	public void OnCollisionStay(Collision other)
	{
	}

	public void Update()
	{
	}
}
