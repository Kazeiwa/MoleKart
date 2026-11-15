using Misc;
using UnityEngine;

public class ClipDeathCollider : MonoBehaviour
{
	public bool m_ClipDeath;

	private void Start()
	{
		Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
		rigidbody.mass = 1E+09f;
		rigidbody.useGravity = false;
		rigidbody.isKinematic = false;
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		Car car = CarUtil.FindCarInCollision(other);
		if (null != car && other.gameObject.layer != 19 && other.gameObject.layer != 24)
		{
			ABMessageData data = new COM_DATA();
			((COM_DATA)data).iValue = 64;
			((COM_DATA)data).vValue = car.GetPosition();
			if (m_ClipDeath)
			{
				CTMessagePipeline.sendMessage(car, 1003, ref data);
			}
			else
			{
				CTMessagePipeline.sendMessage(car, 1001, ref data);
			}
		}
	}
}
