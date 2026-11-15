using UnityEngine;

public class BBLockY : MonoBehaviour
{
	public Camera _Camera;

	public float _Rot;

	public float _RotSpeed;

	private void Start()
	{
		if (null == _Camera)
		{
			Camera[] array = Object.FindObjectsOfType(typeof(Camera)) as Camera[];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].name.Equals("MainCamera"))
				{
					_Camera = array[i];
				}
			}
		}
		if (_Camera != null)
		{
			Quaternion quaternion = Quaternion.Euler(0f, _Rot, 0f);
			Vector3 position = _Camera.transform.position;
			position.y = base.transform.position.y;
			base.transform.LookAt(position);
			base.transform.rotation = base.transform.rotation * quaternion;
		}
	}

	private void Update()
	{
		if (null != _Camera)
		{
			float deltaTime = Time.deltaTime;
			_Rot += deltaTime * _RotSpeed;
			Quaternion quaternion = Quaternion.Euler(0f, _Rot, 0f);
			Vector3 position = _Camera.transform.position;
			position.y = base.transform.position.y;
			base.transform.LookAt(position);
			base.transform.rotation = base.transform.rotation * quaternion;
		}
	}
}
