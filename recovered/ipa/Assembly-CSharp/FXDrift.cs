using System.Collections;
using UnityEngine;

public class FXDrift : MonoBehaviour
{
	public struct DriftNode
	{
		public float age;

		public Vector3 downL;

		public Vector3 upL;

		public Vector3 downR;

		public Vector3 upR;

		public float distance;

		public Vector3 center;
	}

	public GameObject m_TyreLeft;

	public GameObject m_TyreRight;

	public float m_DriftWidth;

	private static float NODE_AGE;

	private Mesh m_Mesh;

	private ArrayList m_DriftList;

	private bool m_Enabled;

	private float m_Distance;

	public void EnableDirft(bool bEnabled)
	{
	}

	public void Init(Material mtl, GameObject root)
	{
	}

	public void Update()
	{
	}

	public void LateUpdate()
	{
	}

	private Color GetColor(float age)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool GetProjected(Vector3 pt, float offset, out Vector3 recordpt)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void RecordDrift()
	{
	}

	private void UpdateDrift(float timeDelta)
	{
	}
}
