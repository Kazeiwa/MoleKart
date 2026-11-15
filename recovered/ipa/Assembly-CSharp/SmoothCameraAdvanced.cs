using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/SmoothCameraAdvanced")]
internal class SmoothCameraAdvanced : MonoBehaviour
{
	public enum MovementType
	{
		Instant,
		LinearInterpolation,
		SphericalLinearInterpolation
	}

	private static SmoothCameraAdvanced instance;

	private Transform ourCameraTransform;

	public Transform target;

	[SerializeField]
	public CameraBumper bumper;

	[SerializeField]
	private List<CameraControl> controls;

	[SerializeField]
	private Vector3 lookAtOffset;

	[SerializeField]
	private MovementType rotationType;

	[SerializeField]
	private MovementType translationType;

	[SerializeField]
	private LimitedFloat distance;

	[SerializeField]
	private LimitedFloat height;

	[SerializeField]
	private LimitedFloat panX;

	[SerializeField]
	private LimitedFloat panY;

	[SerializeField]
	private float damping;

	[SerializeField]
	private float rotationDamping;

	public static SmoothCameraAdvanced Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private static Transform CameraTransform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static Transform Target
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static CameraBumper Bumper
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static List<CameraControl> Controls
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static Vector3 LookAtOffset
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	private static Vector3 RuntimeOffset
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	private static MovementType RotationType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static MovementType TranslationType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static LimitedFloat Distance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static LimitedFloat Height
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static LimitedFloat PanX
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static LimitedFloat PanY
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static float Damping
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static float RotationDamping
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	private void Reset()
	{
	}

	private void Awake()
	{
	}

	public void Update()
	{
	}

	private void UpdateControls()
	{
	}

	private void UpdateCameraPosition()
	{
	}

	public static void FocusOn(Transform argTarget)
	{
	}

	public static void ResetPosition()
	{
	}
}
