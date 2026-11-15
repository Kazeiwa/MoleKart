using UnityEngine;

public struct POINTER_INFO
{
	public enum INPUT_EVENT
	{
		NO_CHANGE,
		PRESS,
		RELEASE,
		TAP,
		MOVE,
		MOVE_OFF,
		RELEASE_OFF,
		DRAG
	}

	public enum POINTER_TYPE
	{
		MOUSE = 1,
		TOUCHPAD,
		MOUSE_TOUCHPAD,
		RAY
	}

	public POINTER_TYPE type;

	public Camera camera;

	public int id;

	public int actionID;

	public INPUT_EVENT evt;

	public RaycastHit hitInfo;

	public bool active;

	public Vector3 devicePos;

	public Vector3 origPos;

	public Vector3 inputDelta;

	public bool isTap;

	public Ray ray;

	public Ray prevRay;

	public float rayDepth;

	public IUIObject targetObj;

	public int layerMask;

	public bool callerIsControl;

	public void Copy(POINTER_INFO ptr)
	{
	}

	public void Reuse(POINTER_INFO ptr)
	{
	}

	public void Reset(int actID)
	{
	}
}
