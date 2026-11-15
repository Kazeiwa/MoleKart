using UnityEngine;

public class SpeedUp : MonoBehaviour
{
	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (null != other && null != other.transform.parent)
		{
			Car component = other.transform.parent.gameObject.GetComponent<Car>();
			if (null != component)
			{
				component.SpeedUp();
			}
		}
	}
}
