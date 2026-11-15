using System.Collections;
using UnityEngine;

public class BodyComponent
{
	public const int FRONT_INDEX = 0;

	public const int RIGHT_INDEX = 1;

	public const int BACK_INDEX = 2;

	public const int LEFT_INDEX = 3;

	public Vector3 CollisionNormal;

	public SphereBuddy m_BuddySphere;

	public float BoundRadius;

	private GameObject m_Node;

	private Transform m_ColliderRoot;

	private Transform m_tsCollider;

	private ArrayList m_arrBoxcomponents = new ArrayList();

	private ArrayList m_arrOthercomponents = new ArrayList();

	private int m_TotalNumber;

	private btVehicleBase chassis;

	private btVehicleBase m_car;

	private float m_fOrigioBoundRadius;

	private bool m_bIsSimulate;

	private Vector3 m_vOriginLocalPosition = Vector3.zero;

	public int TotalNumber => m_TotalNumber;

	public bool IsSimulateMode => m_bIsSimulate;

	public BoxComponent this[int i]
	{
		get
		{
			i = ((i <= m_arrBoxcomponents.Count) ? i : (m_arrBoxcomponents.Count - 1));
			return m_arrBoxcomponents[i] as BoxComponent;
		}
	}

	public BodyComponent(GameObject gameobject, btVehicleBase chassis, EnmCollisionLayer clLayer)
	{
		m_Node = gameobject;
		this.chassis = chassis;
		m_ColliderRoot = m_Node.transform.FindChild("collision");
		m_vOriginLocalPosition = m_ColliderRoot.localPosition;
		PhysicMaterial physicMaterial = null;
		physicMaterial = Resources.Load("carbody_material") as PhysicMaterial;
		Transform transform = m_Node.transform.FindChild("collision/bodybox");
		if (transform != null)
		{
			m_TotalNumber++;
			BoxComponent value = new BoxComponent(transform.gameObject, clLayer)
			{
				m_Collider = 
				{
					material = physicMaterial
				}
			};
			m_arrBoxcomponents.Add(value);
		}
		if (transform != null)
		{
			m_tsCollider = transform.parent;
		}
		CreateSphereBuddy();
	}

	public BodyComponent(GameObject gameobject, btVehicleBase chassis, EnmCollisionLayer clLayer, CollisionCallback callback)
	{
		m_Node = gameobject;
		this.chassis = chassis;
		m_ColliderRoot = m_Node.transform.FindChild("collision");
		m_vOriginLocalPosition = m_ColliderRoot.localPosition;
		PhysicMaterial physicMaterial = null;
		physicMaterial = Resources.Load("carbody_material") as PhysicMaterial;
		Transform transform = m_Node.transform.FindChild("collision/bodybox");
		if (transform != null)
		{
			m_TotalNumber++;
			BoxComponent value = new BoxComponent(transform.gameObject, clLayer)
			{
				m_Collider = 
				{
					material = physicMaterial
				}
			};
			m_arrBoxcomponents.Add(value);
		}
		if (transform != null)
		{
			m_tsCollider = transform.parent;
		}
		CreateSphereBuddy();
		if (!(chassis is Vehicle))
		{
			return;
		}
		Vehicle vehicle = chassis as Vehicle;
		Transform transform2 = m_Node.transform.FindChild("collision/bodyhead");
		Transform transform3 = m_Node.transform.FindChild("collision/bodytail");
		if (transform2 != null && transform3 != null)
		{
			transform2.collider.material = physicMaterial;
			transform2.gameObject.layer = 19;
			transform3.collider.material = physicMaterial;
			transform3.gameObject.layer = 19;
			CollisionHandler component = m_Node.GetComponent<CollisionHandler>();
			if (component == null)
			{
				vehicle.m_colHandler = m_Node.AddComponent<CollisionHandler>();
				vehicle.m_colHandler.Init(vehicle, transform2.collider, transform3.collider, m_Node, callback);
			}
			m_arrOthercomponents.Add(transform2);
			m_arrOthercomponents.Add(transform3);
		}
	}

	public void Uninit()
	{
		foreach (BoxComponent arrBoxcomponent in m_arrBoxcomponents)
		{
			arrBoxcomponent.Uninit();
		}
		m_arrBoxcomponents.Clear();
	}

	private void f(Vector3 pt0, Vector3 pt1, ref float xMin, ref float yMin, ref float xMax, ref float yMax)
	{
		if (xMin > pt0.x)
		{
			xMin = pt0.x;
		}
		if (yMin > pt0.z)
		{
			yMin = pt0.z;
		}
		if (xMax < pt0.x)
		{
			xMax = pt0.x;
		}
		if (yMax < pt0.z)
		{
			yMax = pt0.z;
		}
		if (xMin > pt1.x)
		{
			xMin = pt1.x;
		}
		if (yMin > pt1.z)
		{
			yMin = pt1.z;
		}
		if (xMax < pt1.x)
		{
			xMax = pt1.x;
		}
		if (yMax < pt1.z)
		{
			yMax = pt1.z;
		}
	}

	private void CreateSphereBuddy()
	{
		float num = 0f;
		float num2 = float.PositiveInfinity;
		float num3 = float.PositiveInfinity;
		float num4 = float.NegativeInfinity;
		float num5 = float.NegativeInfinity;
		Quaternion rotation = m_tsCollider.rotation;
		m_tsCollider.rotation = Quaternion.identity;
		for (int i = 0; i < m_arrBoxcomponents.Count; i++)
		{
			BoxComponent boxComponent = m_arrBoxcomponents[i] as BoxComponent;
			Vector3 min = boxComponent.GetCollider().bounds.min;
			Vector3 max = boxComponent.GetCollider().bounds.max;
			if (num2 > min.x)
			{
				num2 = min.x;
			}
			if (num3 > min.z)
			{
				num3 = min.z;
			}
			if (num4 < max.x)
			{
				num4 = max.x;
			}
			if (num5 < max.z)
			{
				num5 = max.z;
			}
		}
		m_tsCollider.rotation = rotation;
		BoundRadius = Mathf.Sqrt((num4 - num2) * (num4 - num2) + (num5 - num3) * (num5 - num3)) / 2f;
		Vector3 position = chassis.GetPosition();
		position.y += num;
		m_fOrigioBoundRadius = BoundRadius;
		m_BuddySphere = new SphereBuddy(position, BoundRadius);
		m_BuddySphere.Disable();
	}

	public void SynchronousSpherebuddy(Vector3 vPos)
	{
		if (!m_bIsSimulate)
		{
			m_BuddySphere.SetPosition(vPos);
		}
		else
		{
			m_car.MoveToGround(m_BuddySphere.GetPosition());
		}
	}

	public void BeginSimulate(btVehicleBase car)
	{
		if (!m_bIsSimulate)
		{
			m_bIsSimulate = true;
			m_car = car;
			m_BuddySphere.Enable();
			m_BuddySphere.SetPosition(m_car.GetPosition());
			m_car.EnableKinematic();
		}
	}

	public void EndSimulate()
	{
		if (m_bIsSimulate)
		{
			m_bIsSimulate = false;
			m_car.DisableKinematic();
			m_car = null;
			m_BuddySphere.Disable();
		}
	}

	public void Simulate(float fTickTime)
	{
		if (m_bIsSimulate)
		{
			if (!Physics.CheckSphere(m_BuddySphere.GetPosition(), BoundRadius, 512))
			{
				EndSimulate();
				return;
			}
			Vector3 vForce = m_car.GetForwardGlobal() * 5f;
			vForce.y = 0f;
			m_BuddySphere.AddForce(vForce);
		}
	}

	public void Scale(float fScale)
	{
		BoundRadius = m_fOrigioBoundRadius * fScale;
		m_ColliderRoot.localScale = new Vector3(fScale, 1f, fScale);
	}
}
