using UnityEngine;

public class BirdSalute : MonoBehaviour
{
	private Transform m_car;

	private Transform m_bird;

	private VertexAniSet m_verAniSet;

	private void Start()
	{
		m_bird = base.transform;
		m_verAniSet = GetComponent<VertexAniSet>();
	}

	public void Update()
	{
		if (null == m_car)
		{
			m_car = TSingleton<CarManager>.Instance.GetCar(0).transform;
		}
		if (!(null == m_car))
		{
			if ((m_car.position - m_bird.position).sqrMagnitude < 600f)
			{
				m_verAniSet.PlayAni("jingli");
			}
			else
			{
				m_verAniSet.PlayAni("libi");
			}
		}
	}
}
