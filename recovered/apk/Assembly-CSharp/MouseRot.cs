using UnityEngine;

public class MouseRot : MonoBehaviour
{
	private GameObject _Target;

	private Vector3 _rotBorn;

	private float _rotPressed;

	private Vector3 _posPressed;

	private bool _Pressed;

	private void Start()
	{
	}

	private void Update()
	{
		if (null == _Target)
		{
			_Target = GameObject.Find("/mole(Clone)");
			if (null != _Target)
			{
				_rotBorn = _Target.transform.rotation.eulerAngles;
				_rotBorn *= 57.29578f;
			}
			_Pressed = false;
		}
		if (!(null != _Target))
		{
			return;
		}
		if (!_Pressed && Input.GetMouseButton(0))
		{
			_Pressed = true;
			_rotPressed = _Target.transform.rotation.eulerAngles.y * 57.29578f;
			_posPressed = Input.mousePosition;
			return;
		}
		if (_Pressed && !Input.GetMouseButton(0))
		{
			_Pressed = false;
			return;
		}
		Vector3 mousePosition = Input.mousePosition;
		if (_Pressed && mousePosition != _posPressed)
		{
			Vector3 vector = mousePosition - _posPressed;
			float y = _rotPressed - vector.x * 1f;
			_Target.transform.rotation = Quaternion.Euler(_rotBorn.x, y, _rotBorn.z);
		}
	}
}
