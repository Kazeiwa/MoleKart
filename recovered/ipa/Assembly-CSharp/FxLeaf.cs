using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FxLeaf : MonoBehaviour
{
	public struct LeafFx
	{
		public float age;

		public Vector3 endpos;

		public Vector3 startpos;

		public Vector3 curpos;
	}

	protected SpritStanderd m_SprSta;

	private int m_Num;

	public float m_LeafEffectTime;

	public float m_TransTime;

	private float m_CurrentTime;

	private ArrayList m_Array;

	private Vector3 offset1;

	private Vector3 offset2;

	private Vector3 offset3;

	private Vector3 offset4;

	public Vector2[] speedarry;

	private Vector3[] m_Vec3;

	public bool m_Enable
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public void Init(SpritStanderd mgs)
	{
	}

	public void CreateLeaf(int num)
	{
	}

	public Vector3 Fxlerp(ref Vector3 v1, ref Vector3 v2, float fac)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void UpdateLeaf(float timeDelta)
	{
	}

	private void Update()
	{
	}

	public void ResetLeaf()
	{
	}
}
