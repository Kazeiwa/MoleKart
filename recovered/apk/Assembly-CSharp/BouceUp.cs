using UnityEngine;

public class BouceUp : MonoBehaviour
{
	public float m_HorizonSpeed;

	public float m_VeticalSpeed;

	public float m_HorizonDampingCoefficient = 2f;

	public float m_VeticalDampingCoefficient = 2f;

	public float m_VeticalDampingCoefficient_Down = 1.8f;

	private void Start()
	{
		Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
		rigidbody.mass = 1E+09f;
		rigidbody.useGravity = false;
		rigidbody.isKinematic = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		Car car = null;
		if (null != other.transform.parent && null != other.transform.parent.parent && null != (car = other.transform.parent.parent.gameObject.GetComponent<Car>()))
		{
			car.BounceCar(m_HorizonSpeed, m_HorizonDampingCoefficient, m_VeticalSpeed, m_VeticalDampingCoefficient, m_VeticalDampingCoefficient_Down);
		}
	}
}
