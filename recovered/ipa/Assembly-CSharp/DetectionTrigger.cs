using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Triggers/DetectionTrigger")]
public class DetectionTrigger : MonoBehaviour
{
	public enum ColliderEnumType
	{
		Box,
		Capsule,
		Sphere,
		Wheel,
		Mesh
	}

	private GameObject ourGameObject;

	protected Collider ourCollider;

	[SerializeField]
	private ColliderEnumType colliderType;

	private Dictionary<int, Transform> ourColliders;

	private List<Transform> ourIgnores;

	private List<Type> ourIgnoreTypes;

	private GameObject GameObject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	protected Collider Collider
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ColliderEnumType ColliderType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Dictionary<int, Transform> Colliders
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public List<Transform> Ignores
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public List<Type> IgnoreTypes
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool IsTripped
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Awake()
	{
	}

	private void OnTriggerEnter(Collider argCollider)
	{
	}

	private void OnTriggerExit(Collider argCollider)
	{
	}

	private void OnColliderEnter(Collision argCollider)
	{
	}

	private void OnColliderExit(Collision argCollider)
	{
	}

	private Collider GetCollider()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
