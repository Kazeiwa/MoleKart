using System.Collections;
using UnityEngine;

public class FXFlower2 : MonoBehaviour
{
	public struct Petal
	{
		public float age;

		public float life;

		public Color clr;

		public Vector3 pos;

		public Vector3 rot;

		public Vector3 spd;

		public Vector3 spd2;
	}

	public FXMgrShadow _MgrShadow;

	public GameObject _PetalObj;

	public float _Pps;

	public float _Radius;

	public float _minSpeed;

	public float _maxSpeed;

	public float _minLife;

	public float _maxLife;

	public float _midLife;

	public Vector3 _Gravity;

	public Vector3 _RotSpeed;

	public Vector4 _rectShadow;

	private Mesh _Mesh;

	private float _TimeLastCreate;

	private bool _Enabled;

	private Vector3[] _vbSrc;

	private Vector2[] _uvSrc;

	private int[] _ibSrc;

	private ArrayList _Petals;

	private void Awake()
	{
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

	private void LateUpdate()
	{
	}
}
