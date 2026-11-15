using UnityEngine;

public class HorizontallyLocalPlayerFacingBehaviour : MonoBehaviour
{
	private Transform cachedTransform;

	private Transform cachedlocalPlayerTransform;

	private Renderer cachedRenderer;

	public Transform LocalPlayerTransform
	{
		get
		{
			if (null == cachedlocalPlayerTransform)
			{
				HeroCar heroCar = Object.FindObjectOfType(typeof(HeroCar)) as HeroCar;
				if (heroCar != null)
				{
					cachedlocalPlayerTransform = heroCar.transform;
				}
			}
			return cachedlocalPlayerTransform;
		}
	}

	private Renderer Renderer
	{
		get
		{
			if (null == cachedRenderer)
			{
				cachedRenderer = GetComponentInChildren<Renderer>();
			}
			return cachedRenderer;
		}
	}

	protected void Start()
	{
		cachedTransform = base.transform;
	}

	protected void Update()
	{
		if (LocalPlayerTransform != null && Renderer.enabled)
		{
			Vector3 worldPosition = new Vector3(cachedlocalPlayerTransform.position.x, cachedTransform.position.y, cachedlocalPlayerTransform.position.z);
			cachedTransform.LookAt(worldPosition, Vector3.up);
		}
	}
}
