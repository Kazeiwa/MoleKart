using System.Collections;
using UnityEngine;

public class MeshEmitterNew : MonoBehaviour
{
	public class Petal
	{
		public float age;

		public float life;

		public Vector3 sclBorn;

		public Vector3 scl;

		public Color clr;

		public Vector3 pos;

		public Vector3 rot;

		public Vector3 spd;

		public Vector3 spd2;

		public Vector3 rotSpd;

		public Vector2 uvPos;
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

	public Vector3 _minSclBorn;

	public Vector3 _maxSclBorn;

	public Vector3 _sclStart;

	public Vector3 _sclMiddle;

	public Vector3 _sclEnd;

	public Vector3 _minRot;

	public Vector3 _maxRot;

	public Vector3 _minRotSpeed;

	public Vector3 _maxRotSpeed;

	public Color _clrStart;

	public Color _clrMiddle;

	public Color _clrEnd;

	public Vector3 _Gravity;

	public float _uSpeed;

	public float _vSpeed;

	public float _uRepeat;

	public float _vRepeat;

	private Mesh _Mesh;

	private float _TimeLastCreate;

	private bool _Emit;

	private Vector3[] _vbSrc;

	private Vector2[] _uvSrc;

	private int[] _ibSrc;

	private int nNumPetals;

	private ArrayList _Petals;

	private Vector3[] vb;

	private Vector2[] uvs;

	private Color[] clrs;

	private int[] ib;

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

	public void Awake()
	{
	}

	private Color lerp(ref Color c1, ref Color c2, float fac)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Vector3 lerp(ref Vector3 v1, ref Vector3 v2, float fac)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Vector3 mult(ref Vector3 v1, ref Vector3 v2)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void InitOnePetal(Petal pt)
	{
	}

	private void CreatePetals(int num)
	{
	}

	private void UpdatePetals(float timeDelta)
	{
	}

	public void Update()
	{
	}

	private void UpdateMesh()
	{
	}
}
