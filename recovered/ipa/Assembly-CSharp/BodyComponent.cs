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

	private ArrayList m_arrBoxcomponents;

	private ArrayList m_arrOthercomponents;

	private int m_TotalNumber;

	private btVehicleBase chassis;

	private btVehicleBase m_car;

	private float m_fOrigioBoundRadius;

	private bool m_bIsSimulate;

	private Vector3 m_vOriginLocalPosition;

	public int TotalNumber
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsSimulateMode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public BoxComponent this[int i]
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public BodyComponent(GameObject gameobject, btVehicleBase chassis, EnmCollisionLayer clLayer)
	{
	}

	public BodyComponent(GameObject gameobject, btVehicleBase chassis, EnmCollisionLayer clLayer, ICollisionCallback callback)
	{
	}

	public void Uninit()
	{
	}

	private void f(Vector3 pt0, Vector3 pt1, ref float xMin, ref float yMin, ref float xMax, ref float yMax)
	{
	}

	private void CreateSphereBuddy()
	{
	}

	public void SynchronousSpherebuddy(Vector3 vPos)
	{
	}

	public void BeginSimulate(btVehicleBase car)
	{
	}

	public void EndSimulate()
	{
	}

	public void Simulate(float fTickTime)
	{
	}

	public void Scale(float fScale)
	{
	}
}
