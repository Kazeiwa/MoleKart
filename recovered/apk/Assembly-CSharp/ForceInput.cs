using UnityEngine;

public class ForceInput : MonoBehaviour
{
	private void FixedUpdate()
	{
		float num = 10f;
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			base.transform.rigidbody.AddForce(-Vector3.left * num);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			base.transform.rigidbody.AddForce(-Vector3.right * num);
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			base.transform.rigidbody.AddForce(-Vector3.forward * num);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			base.transform.rigidbody.AddForce(-Vector3.back * num);
		}
	}
}
