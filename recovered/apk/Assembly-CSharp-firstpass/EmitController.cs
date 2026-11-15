using UnityEngine;

public class EmitController : MonoBehaviour
{
	public float _timeToShot;

	public float _timeToStopEmit = 1f;

	private bool _shot;

	private float _time;

	private ParticleEmitter _Emitter;

	private MeshEmitter _MeshEmitter;

	private MeshEmitterFacing _MeshEmitterFacing;

	private void Emit(bool bEmit)
	{
		if (null != _Emitter)
		{
			_Emitter.emit = bEmit;
		}
		else if (null != _MeshEmitter)
		{
			_MeshEmitter.emit = bEmit;
		}
		else if (null != _MeshEmitterFacing)
		{
			_MeshEmitterFacing.emit = bEmit;
		}
	}

	private bool isEmit()
	{
		if (null != _Emitter)
		{
			return _Emitter.emit;
		}
		if (null != _MeshEmitter)
		{
			return _MeshEmitter.emit;
		}
		if (null != _MeshEmitterFacing)
		{
			return _MeshEmitterFacing.emit;
		}
		return false;
	}

	private void Awake()
	{
		doReset();
	}

	public void doReset()
	{
		_Emitter = GetComponent<ParticleEmitter>();
		if (null == _Emitter)
		{
			_MeshEmitter = GetComponent<MeshEmitter>();
		}
		if (null == _Emitter && null == _MeshEmitter)
		{
			_MeshEmitterFacing = GetComponent<MeshEmitterFacing>();
		}
		_shot = false;
		_time = 0f;
		Emit(bEmit: false);
	}

	public void Stop()
	{
		Emit(bEmit: false);
	}

	private void Update()
	{
		if (!(null == _Emitter) || !(null == _MeshEmitter) || !(null == _MeshEmitterFacing))
		{
			_time += Time.deltaTime;
			if (_timeToStopEmit != -1f && _shot && isEmit() && _time > _timeToShot + _timeToStopEmit)
			{
				Emit(bEmit: false);
			}
			if (!_shot && _time > _timeToShot)
			{
				_shot = true;
				Emit(bEmit: true);
			}
		}
	}
}
