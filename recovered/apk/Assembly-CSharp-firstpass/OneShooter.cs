using UnityEngine;

public class OneShooter : MonoBehaviour
{
	public float _minFreq = 0.5f;

	public float _maxFreq = 1f;

	private ParticleEmitter _Emitter;

	private float _time;

	private void Awake()
	{
		_Emitter = GetComponent<ParticleEmitter>();
		if (_Emitter != null)
		{
			_Emitter.emit = false;
		}
		_time = Mathf.Lerp(_minFreq, _maxFreq, Random.value);
	}

	private void Update()
	{
		if (!(null == _Emitter))
		{
			_time -= Time.deltaTime;
			if (_time < 0f)
			{
				_time = Mathf.Lerp(_minFreq, _maxFreq, Random.value);
				float num = Mathf.Lerp(_Emitter.minEmission, _Emitter.maxEmission, Random.value);
				_Emitter.Emit((int)num);
				_Emitter.emit = false;
			}
		}
	}
}
