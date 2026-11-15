using UnityEngine;

public class BouceUp : MonoBehaviour
{
	public float m_HorizonSpeed;

	public float m_VeticalSpeed;

	public float m_HorizonDampingCoefficient;

	public float m_VeticalDampingCoefficient;

	public float m_VeticalDampingCoefficient_Down;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
