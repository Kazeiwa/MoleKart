using UnityEngine;

[RequireComponent(typeof(SplineInterpolator))]
[AddComponentMenu("Splines/Spline Controller")]
public class SplineController : MonoBehaviour
{
	public GameObject SplineRoot;

	public float Duration;

	public eOrientationMode OrientationMode;

	public eWrapMode WrapMode;

	public bool AutoStart;

	public bool AutoClose;

	public bool HideOnExecute;

	private SplineInterpolator mSplineInterp;

	private Transform[] mTransforms;

	private void OnDrawGizmos()
	{
	}

	private void Start()
	{
	}

	private void SetupSplineInterpolator(SplineInterpolator interp, Transform[] trans)
	{
	}

	private Transform[] GetTransforms()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void DisableTransforms()
	{
	}

	private void FollowSpline()
	{
	}
}
