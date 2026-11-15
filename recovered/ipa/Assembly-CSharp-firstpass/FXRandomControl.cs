using UnityEngine;

public class FXRandomControl : MonoBehaviour
{
	public bool _Enable;

	public float _EnableTimeMin;

	public float _EnableTimeMax;

	public float _DisableTimeMin;

	public float _DisableTimeMax;

	private float _RandomEnableTime;

	private float _RandomDisableTime;

	private ParticleEmitter emitter;

	private int runningMode;

	private void Start()
	{
	}

	private void Update()
	{
	}
}
