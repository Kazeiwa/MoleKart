using UnityEngine;

public class Effect : MonoBehaviour
{
	private ParticleEmitter[] pEmitter;

	private MeshEmitter[] pMeshEmitter;

	private ParticleRenderer pRenderer;

	private Animation[] pAnimation;

	private UVAnimator2[] pUVAnimator;

	private bool isOneShot;

	private bool hasEmitted;

	private bool isStopped;

	private float lifeTime;

	private float existTime;

	private void Awake()
	{
		pEmitter = GetComponentsInChildren<ParticleEmitter>();
		pMeshEmitter = GetComponentsInChildren<MeshEmitter>();
		pRenderer = GetComponent<ParticleRenderer>();
		pAnimation = GetComponentsInChildren<Animation>();
		pUVAnimator = GetComponentsInChildren<UVAnimator2>();
		isOneShot = false;
		hasEmitted = false;
		isStopped = false;
		lifeTime = -1f;
		existTime = 0f;
	}

	private void Update()
	{
		if (isOneShot)
		{
			if (!hasEmitted)
			{
				hasEmitted = true;
			}
			else
			{
				Stop();
			}
		}
		else if (lifeTime != -1f)
		{
			existTime += Time.deltaTime;
			if (existTime >= lifeTime)
			{
				Pause();
				Disable();
				existTime = 0f;
			}
		}
	}

	public void SetPosition(Vector3 position)
	{
		base.transform.localPosition = position;
	}

	public void SetRotation(Vector3 rotation)
	{
		base.transform.localRotation = Quaternion.Euler(rotation);
	}

	public void SetLifeTime(float life)
	{
		lifeTime = life;
		existTime = 0f;
	}

	public void SetTexture(Texture tex)
	{
		pRenderer.material.mainTexture = tex;
	}

	public void SetOneShot(bool oneShot)
	{
		isOneShot = oneShot;
		lifeTime = -1f;
	}

	public void AttachTo(Transform parentTrans, Vector3 relativePos, Vector3 rotation)
	{
		base.transform.parent = parentTrans;
		SetPosition(relativePos);
		SetRotation(rotation);
	}

	public void Pause()
	{
		MeshRenderer[] componentsInChildren = base.transform.GetComponentsInChildren<MeshRenderer>();
		if (componentsInChildren != null)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (null != componentsInChildren[i])
				{
					componentsInChildren[i].enabled = false;
				}
			}
		}
		ParticleRenderer[] componentsInChildren2 = base.transform.GetComponentsInChildren<ParticleRenderer>();
		if (componentsInChildren2 != null)
		{
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				if (null != componentsInChildren2[j])
				{
					componentsInChildren2[j].enabled = false;
				}
			}
		}
		ParticleEmitter[] array = pEmitter;
		foreach (ParticleEmitter particleEmitter in array)
		{
			particleEmitter.emit = false;
		}
		MeshEmitter[] array2 = pMeshEmitter;
		foreach (MeshEmitter meshEmitter in array2)
		{
			meshEmitter.emit = false;
		}
	}

	public void Resume()
	{
		MeshRenderer[] componentsInChildren = base.transform.GetComponentsInChildren<MeshRenderer>();
		if (componentsInChildren != null)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (null != componentsInChildren[i])
				{
					componentsInChildren[i].enabled = true;
				}
			}
		}
		ParticleRenderer[] componentsInChildren2 = base.transform.GetComponentsInChildren<ParticleRenderer>();
		if (componentsInChildren2 != null)
		{
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				if (null != componentsInChildren2[j])
				{
					componentsInChildren2[j].enabled = true;
				}
			}
		}
		ParticleEmitter[] array = pEmitter;
		foreach (ParticleEmitter particleEmitter in array)
		{
			particleEmitter.emit = true;
		}
		MeshEmitter[] array2 = pMeshEmitter;
		foreach (MeshEmitter meshEmitter in array2)
		{
			meshEmitter.emit = true;
		}
	}

	public void Reset()
	{
		Resume();
	}

	public void Enable()
	{
		Disable();
		if (!base.gameObject.active)
		{
			Reset();
		}
		base.gameObject.active = true;
		EmitController[] componentsInChildren = GetComponentsInChildren<EmitController>();
		EmitController[] array = componentsInChildren;
		foreach (EmitController emitController in array)
		{
			emitController.enabled = true;
			emitController.doReset();
		}
		MeshEmitter[] array2 = pMeshEmitter;
		foreach (MeshEmitter meshEmitter in array2)
		{
			meshEmitter.enabled = true;
		}
		UVAnimator2[] array3 = pUVAnimator;
		foreach (UVAnimator2 uVAnimator in array3)
		{
			uVAnimator.enabled = true;
		}
		Animation[] array4 = pAnimation;
		foreach (Animation animation in array4)
		{
			animation.enabled = true;
			animation.Play();
			animation.renderer.enabled = true;
		}
	}

	public void Disable()
	{
		EmitController[] componentsInChildren = GetComponentsInChildren<EmitController>();
		EmitController[] array = componentsInChildren;
		foreach (EmitController emitController in array)
		{
			emitController.enabled = false;
		}
		MeshEmitter[] array2 = pMeshEmitter;
		foreach (MeshEmitter meshEmitter in array2)
		{
			meshEmitter.enabled = false;
		}
		UVAnimator2[] array3 = pUVAnimator;
		foreach (UVAnimator2 uVAnimator in array3)
		{
			uVAnimator.enabled = false;
		}
		Animation[] array4 = pAnimation;
		foreach (Animation animation in array4)
		{
			animation.enabled = false;
			animation.Stop();
			animation.renderer.enabled = false;
		}
		if (base.gameObject.active)
		{
			Pause();
		}
		base.gameObject.active = false;
	}

	public void Stop()
	{
		if (!isStopped)
		{
			ParticleEmitter[] array = pEmitter;
			foreach (ParticleEmitter particleEmitter in array)
			{
				particleEmitter.emit = false;
			}
			Disable();
			isStopped = true;
		}
	}

	public void doReset()
	{
		Resume();
		EmitController[] componentsInChildren = GetComponentsInChildren<EmitController>();
		EmitController[] array = componentsInChildren;
		foreach (EmitController emitController in array)
		{
			emitController.doReset();
		}
	}
}
