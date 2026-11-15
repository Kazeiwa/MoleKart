using UnityEngine;

public class FXRandomControl : MonoBehaviour
{
	public bool _Enable = true;

	public float _EnableTimeMin = 10f;

	public float _EnableTimeMax = 30f;

	public float _DisableTimeMin = 10f;

	public float _DisableTimeMax = 30f;

	private float _RandomEnableTime;

	private float _RandomDisableTime;

	private ParticleEmitter emitter;

	private int runningMode;

	private void Start()
	{
		_RandomDisableTime = Random.Range(_DisableTimeMin, _DisableTimeMax);
		emitter = base.particleEmitter;
		if (emitter != null)
		{
			emitter.emit = false;
		}
	}

	private void Update()
	{
		if (emitter == null)
		{
			return;
		}
		if (runningMode == 0)
		{
			_RandomDisableTime -= Time.deltaTime;
			if (_RandomDisableTime < 0f)
			{
				emitter.emit = true;
				_RandomEnableTime = Random.Range(_EnableTimeMin, _EnableTimeMax);
				runningMode = 1;
			}
		}
		else
		{
			_RandomEnableTime -= Time.deltaTime;
			if (_RandomEnableTime < 0f)
			{
				emitter.emit = false;
				_RandomDisableTime = Random.Range(_DisableTimeMin, _DisableTimeMax);
				runningMode = 0;
			}
		}
	}
}
