using UnityEngine;

public class MushroomGolden : MonoBehaviour
{
	private float LIFE_TIME = 5f;

	private float m_LifeTime;

	private void Start()
	{
	}

	private void Update()
	{
		m_LifeTime += Time.deltaTime;
		if (m_LifeTime > LIFE_TIME)
		{
			base.gameObject.GetComponent<Car>().DropProp(drop: false);
			Object.Destroy(this);
		}
	}
}
