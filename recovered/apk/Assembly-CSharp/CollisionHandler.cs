using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	public const float ROTATION_LOCK_TIME = 0.5f;

	public const float MAX_BOUCING_VEL = 15f;

	public const float UP_VEL_COEFF = 0.3f;

	public const float BACK_VEL_COEFF = 0.3f;

	public const float MIN_BOUCING_VEL = 25f;

	public const float FORCE_ROT_ANGEL_T = 0.08726646f;

	private ChassisSimulator m_chassis;

	private Vehicle m_car;

	private Collider[] m_collider = new Collider[2];

	private Rigidbody m_rigid;

	private float m_colAccumTime;

	private bool m_lockRotation;

	public readonly float REVERSE_LOCK_OFFSET_TWEAK = 0.01f;

	private CollisionCallback callback;

	public void Init(Vehicle car, Collider colliderHead, Collider colliderTail, GameObject obj, CollisionCallback callback)
	{
		m_car = car;
		m_chassis = car.chassis;
		m_collider[0] = colliderHead;
		m_collider[1] = colliderTail;
		m_rigid = obj.GetComponent<Rigidbody>();
		this.callback = callback;
	}

	public void OnTriggerEnter(Collider c)
	{
		string text = c.transform.name;
		if (text == "VStop")
		{
			if (!m_rigid.isKinematic)
			{
				Vector3 velocity = m_rigid.velocity;
				velocity.x = 0f - velocity.x;
				velocity.z = 0f - velocity.z;
				m_rigid.velocity = velocity;
			}
		}
		else if (text == "RoadBounce" && !m_rigid.isKinematic)
		{
			m_car.MoveToGround(m_car.Position);
		}
	}

	public void OnTriggerStay(Collider c)
	{
		string text = c.transform.name;
		if (text == "PushTrigger")
		{
			Vector3 vPreVelGlobal = m_car.m_vPreVelGlobal;
			if (vPreVelGlobal.magnitude < 5f)
			{
				vPreVelGlobal.Normalize();
				vPreVelGlobal *= 5f;
				if (!m_rigid.isKinematic)
				{
					m_rigid.velocity = vPreVelGlobal;
				}
			}
			m_collider[0].enabled = false;
			m_collider[1].enabled = false;
		}
		else
		{
			if (!(text == "VT"))
			{
				return;
			}
			if (!m_car.m_kBoostVar.m_bBoost)
			{
				bool flag = true;
				for (int i = 0; i < 4; i++)
				{
					if (m_chassis.Nxwheels[i].bGrounded)
					{
						flag = false;
					}
				}
				if (flag)
				{
					m_rigid.AddForce(Vector3.down * 5f, ForceMode.VelocityChange);
				}
			}
			else if (!m_rigid.isKinematic)
			{
				Vector3 velocity = m_rigid.velocity;
				velocity.y = 0f;
				m_rigid.velocity = velocity;
			}
		}
	}

	public void OnTriggerExit(Collider c)
	{
		if (c.transform.name.Contains("PushTrigger"))
		{
			m_collider[0].enabled = true;
			m_collider[1].enabled = true;
			m_chassis.ResetMassCenter();
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.contacts.GetLength(0) <= 0)
		{
			return;
		}
		ContactPoint contactPoint = collision.contacts[0];
		if (!(contactPoint.thisCollider == m_collider[0]) && !(contactPoint.thisCollider == m_collider[1]))
		{
			return;
		}
		int layer = contactPoint.otherCollider.gameObject.layer;
		int num = 0;
		switch (layer)
		{
		case 9:
			num = 1;
			break;
		case 14:
			num = 2;
			break;
		case 12:
			num = 3;
			break;
		}
		if (num != 0 && m_car.m_kShift.IsShiftMode())
		{
			m_car.m_kShift.BreakShift();
		}
		if (num != 1 && num != 2)
		{
			return;
		}
		m_rigid.angularVelocity = Vector3.zero;
		Vector3 vPreVelGlobal = m_car.m_vPreVelGlobal;
		float magnitude = vPreVelGlobal.magnitude;
		float f = Vector3.Dot(vPreVelGlobal, contactPoint.normal);
		f = Mathf.Abs(f);
		if (magnitude < 25f)
		{
			return;
		}
		magnitude = Mathf.Min(magnitude, 15f);
		Vector3 velocity = contactPoint.normal * f * 0.3f;
		if (layer == 14)
		{
			velocity = Vector3.zero;
		}
		velocity.y = magnitude * 0.3f;
		velocity.y = Mathf.Min(5f, velocity.y);
		if (m_car.GetBoostMode())
		{
			m_car.m_kBoostVar.m_bPauseBoost = true;
			velocity = Vector3.zero;
			if (contactPoint.thisCollider.transform.name == "bodytail")
			{
				return;
			}
		}
		Vector3 vector = Vector3.Cross(Vector3.up, contactPoint.normal);
		float num2 = Vector3.Dot(vector, vPreVelGlobal) / magnitude;
		if (!(num2 > 0.25f))
		{
			if (num2 < -0.25f)
			{
				vector *= -1f;
			}
			else
			{
				vector = Vector3.zero;
			}
		}
		if (layer != 14)
		{
			velocity += vector * magnitude;
		}
		m_rigid.velocity = velocity;
		m_colAccumTime = 0f;
		m_lockRotation = true;
		m_car.LockRotation(bLockX: true, bLockY: true, bLockZ: true);
		callback.OnHitFence(contactPoint.thisCollider, contactPoint.otherCollider);
	}

	public void OnCollisionStay(Collision collision)
	{
		if (collision.contacts.GetLength(0) <= 0)
		{
			return;
		}
		ContactPoint contactPoint = collision.contacts[0];
		if (!(contactPoint.thisCollider == m_collider[0]) && !(contactPoint.thisCollider == m_collider[1]))
		{
			return;
		}
		int layer = contactPoint.otherCollider.gameObject.layer;
		int num = 0;
		switch (layer)
		{
		case 9:
			num = 1;
			break;
		case 14:
			num = 2;
			break;
		case 12:
			num = 3;
			break;
		}
		if (num != 0 && m_car.m_kShift.IsShiftMode())
		{
			m_car.m_kShift.BreakShift();
		}
		if (num != 1 && num != 2)
		{
			return;
		}
		if (num == 1 && contactPoint.thisCollider == m_collider[0])
		{
			Vector3 normal = contactPoint.normal;
			normal.Normalize();
			m_car.TranslateCarWorld(normal * REVERSE_LOCK_OFFSET_TWEAK);
		}
		if (m_car.m_kShift.IsShiftMode())
		{
			m_car.m_kShift.BreakShift();
		}
		if (m_car.GetBoostMode())
		{
			m_car.m_kBoostVar.m_bPauseBoost = true;
			return;
		}
		Vector3 vPreVelGlobal = m_car.m_vPreVelGlobal;
		float magnitude = vPreVelGlobal.magnitude;
		if (!(magnitude < 25f))
		{
			return;
		}
		Vector3 rightGlobal = m_chassis.GetRightGlobal();
		float num2 = Vector3.Dot(rightGlobal, contactPoint.normal);
		if (Mathf.Abs(num2) < 0.25f)
		{
			int a = (int)(1f / (Mathf.Abs(num2) + 0.01f));
			a = Mathf.Max(a, 1);
			if (num2 > 0.08726646f)
			{
				m_car.m_bEnableForceRot = a;
			}
			else if (num2 < -0.08726646f)
			{
				m_car.m_bEnableForceRot = -a;
			}
			else if (InputManager.Instance.CurrentSteering < 0f)
			{
				m_car.m_bEnableForceRot = -a;
			}
			else if (InputManager.Instance.CurrentSteering > 0f)
			{
				m_car.m_bEnableForceRot = a;
			}
		}
	}

	public void Update()
	{
		if (!m_lockRotation)
		{
			return;
		}
		m_colAccumTime += Time.deltaTime;
		if (m_colAccumTime > 0.5f)
		{
			m_lockRotation = false;
			if (m_rigid != null)
			{
				m_car.LockRotation(bLockX: false, bLockY: false, bLockZ: false);
			}
		}
	}

	public void OnCollisionExit(Collision collision)
	{
	}
}
