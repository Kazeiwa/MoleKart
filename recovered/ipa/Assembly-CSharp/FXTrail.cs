using System.Collections;
using UnityEngine;

public class FXTrail : MonoBehaviour
{
	public struct TrailNode
	{
		public float age;

		public float dis;

		public Vector3 leftL;

		public Vector3 rightL;

		public Vector3 leftR;

		public Vector3 rightR;

		public Vector3 center;
	}

	private static float NODE_AGE;

	public float _TrailWidth;

	public GameObject _TyreLeft;

	public GameObject _TyreRight;

	private bool _Enabled;

	private ArrayList _Trail;

	private Mesh _Mesh;

	private float _Distance;

	public void EnableTrail(bool bEnabled)
	{
	}

	public bool IsTrailEnabled()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Init(Material mtl, GameObject root)
	{
	}

	private bool GetProjected(Vector3 pt, out Vector3 ptHit)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void RecordTrail()
	{
	}

	private void UpdateTrail(float timeDelta)
	{
	}

	private void Update()
	{
	}

	private Color getColor(float age)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void LateUpdate()
	{
	}
}
