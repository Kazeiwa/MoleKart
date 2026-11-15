using System.Collections.Generic;
using UnityEngine;

public class SplineInterpolator : MonoBehaviour
{
	internal class SplineNode
	{
		internal Vector3 Point;

		internal Quaternion Rot;

		internal float Time;

		internal Vector2 EaseIO;

		internal SplineNode(Vector3 p, Quaternion q, float t, Vector2 io)
		{
		}

		internal SplineNode(SplineNode o)
		{
		}
	}

	private eEndPointsMode mEndPointsMode;

	private List<SplineNode> mNodes;

	private string mState;

	private bool mRotations;

	private OnEndCallback mOnEndCallback;

	private float mCurrentTime;

	private int mCurrentIdx;

	private void Awake()
	{
	}

	public void StartInterpolation(OnEndCallback endCallback, bool bRotations, eWrapMode mode)
	{
	}

	public void Reset()
	{
	}

	public void AddPoint(Vector3 pos, Quaternion quat, float timeInSeconds, Vector2 easeInOut)
	{
	}

	private void SetInput()
	{
	}

	private void SetExplicitMode()
	{
	}

	public void SetAutoCloseMode(float joiningPointTime)
	{
	}

	private void Update()
	{
	}

	private Quaternion GetSquad(int idxFirstPoint, float t)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetHermiteInternal(int idxFirstPoint, float t)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector3 GetHermiteAtTime(float timeParam)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
