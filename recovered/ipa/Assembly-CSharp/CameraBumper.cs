using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
internal class CameraBumper
{
	public enum CollisionType
	{
		None,
		Raycast,
		Collider
	}

	private RaycastHit hit;

	private GameObject ourBumper;

	private DetectionTrigger ourDetectionTrigger;

	[SerializeField]
	private CollisionType collisionType;

	[SerializeField]
	private float distanceCheck;

	[SerializeField]
	private float newCameraHeight;

	[SerializeField]
	private Vector3 offset;

	private List<Transform> ourIgnores;

	private List<Type> ourIgnoreTypes;

	private GameObject Bumper
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	private DetectionTrigger DetectionTrigger
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public CollisionType Collision
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float DistanceCheck
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float NewCameraHeight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public Vector3 Offset
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

	private bool IsBumperHit(Transform argTarget, Transform argCamera)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 UpdatePosition(Transform argTarget, Transform argCamera, Vector3 argWantedPosition, float argT)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
