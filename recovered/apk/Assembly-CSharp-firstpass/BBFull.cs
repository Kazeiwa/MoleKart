using UnityEngine;

public class BBFull : MonoBehaviour
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
			base.transform.LookAt(_Camera.transform.position);
			base.transform.rotation = base.transform.rotation * quaternion;
		}
	}

	private void Update()
	{
		if (null != _Camera)
		{
			float deltaTime = Time.deltaTime;
			_Rot += deltaTime * _RotSpeed;
			Quaternion quaternion = Quaternion.Euler(0f, 0f, _Rot);
			base.transform.LookAt(_Camera.transform.position);
			base.transform.rotation = base.transform.rotation * quaternion;
		}
	}
}
