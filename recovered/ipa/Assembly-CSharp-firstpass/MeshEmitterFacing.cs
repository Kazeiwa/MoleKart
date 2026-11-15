using System.Collections;
using UnityEngine;

public class MeshEmitterFacing : MonoBehaviour
{
	public struct Petal
	{
		public float age;

		public float life;

		public float scl;

		public Color clr;

		public Vector3 pos;

		public float rot;

		public Vector3 spd;

		public Vector3 spd2;

		public float rotSpd;
	}

	public GameObject _MeshObj;

	public Material _Mat;

	public bool _localSpace;

	public float _Pps;

	public float _minLife;

	public float _maxLife;

	public float _midLife;

	public float _minRadius;

	public float _maxRadius;

	public float _minTheta;

	public float _maxTheta;

	public float _minPhi;

	public float _maxPhi;

	public float _spdMin;

	public float _spdMax;

	public float _sclStart;

	public float _sclMiddle;

	public float _sclEnd;

	public float _minRot;

	public float _maxRot;

	public float _minRotSpeed;

	public float _maxRotSpeed;

	public Color _clrStart;

	public Color _clrMiddle;

	public Color _clrEnd;

	public Vector3 _Gravity;

	private Mesh _Mesh;

	private float _TimeLastCreate;

	private bool _Emit;

	private Vector3[] _vbSrc;

	private Vector2[] _uvSrc;

	private int[] _ibSrc;

	private ArrayList _Petals;

	public bool emit
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	private void Awake()
	{
	}

	private Color lerp(ref Color c1, ref Color c2, float fac)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void CreatePetals(int num)
	{
	}

	private void UpdatePetals(float timeDelta)
	{
	}

	private void Update()
	{
	}

	private Matrix4x4 buildRotFromTo(Vector3 from, Vector3 to)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void LateUpdate()
	{
	}
}
