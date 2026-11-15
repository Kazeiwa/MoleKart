using UnityEngine;

public class Effect : MonoBehaviour
{
	private ParticleEmitter[] pEmitter;

	private MeshEmitter[] pMeshEmitter;

	private ParticleRenderer pRenderer;

	private Animation[] pAnimation;

	private UVAnimator2[] pUVAnimator;

	private EmitController[] pEctrol;

	private MeshRenderer[] pMeshRenders;

	private ParticleRenderer[] pParticeRenders;

	private bool isOneShot;

	private bool hasEmitted;

	private bool isStopped;

	private float lifeTime;

	private float existTime;

	private int iPreloadTick;

	private void Awake()
	{
	}

	public void Preload()
	{
	}

	private void Update()
	{
	}

	public void SetPosition(Vector3 position)
	{
	}

	public void SetRotation(Vector3 rotation)
	{
	}

	public void SetLifeTime(float life)
	{
	}

	public void SetTexture(Texture tex)
	{
	}

	public void SetOneShot(bool oneShot)
	{
	}

	public void AttachTo(Transform parentTrans, Vector3 relativePos, Vector3 rotation)
	{
	}

	public void Pause()
	{
	}

	public void Resume()
	{
	}

	public void Reset()
	{
	}

	public void Enable()
	{
	}

	public void Disable()
	{
	}

	public void Stop()
	{
	}

	public void doReset()
	{
	}
}
