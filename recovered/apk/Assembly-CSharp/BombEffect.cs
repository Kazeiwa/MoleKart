using System.Collections;
using UnityEngine;

public class BombEffect : Prop
{
	private ArrayList m_Cars = new ArrayList();

	private void Start()
	{
		Renderer componentInChildren = base.gameObject.GetComponentInChildren<Renderer>();
		if (null != componentInChildren)
		{
			componentInChildren.receiveShadows = false;
			componentInChildren.castShadows = false;
		}
		InitDistanceToCamera();
	}

	private void Update()
	{
		UpdateDistanceToCamera();
		UpdateVisible();
		CheckCollidWithMonster(8f);
		CheckCollideWithOtherProps(8f);
		if (m_CreatedTime < 0.6f)
		{
			for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
			{
				Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
				if (null != carByIndex && !m_Cars.Contains(carByIndex) && (carByIndex.transform.position - base.transform.position).magnitude < 10f)
				{
					carByIndex.HitByBomb();
					m_Cars.Add(carByIndex);
				}
			}
		}
		m_CreatedTime += Time.deltaTime;
		if (m_CreatedTime > 1f)
		{
			TSingleton<PropManager>.Instance.Release(ePropType.BOMB, this);
			SoundManager.Instance.RemoveAudio(m_Audio);
			Object.Destroy(base.gameObject);
		}
	}

	public void DropAtPosition(Vector3 vPosd)
	{
		base.transform.position = vPosd;
		Start();
		m_bitUpdateFlag = 16;
		UpdateDistanceToCamera();
		UpdateVisible();
		PlaySound(51602, 1);
	}
}
