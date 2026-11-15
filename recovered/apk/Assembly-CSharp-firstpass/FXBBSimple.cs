using UnityEngine;

public class FXBBSimple : MonoBehaviour
{
	public float _MinScale = 1f;

	public float _MaxScale = 1f;

	public float _SpdScale = 5f;

	public string _ClrName = string.Empty;

	public float _SpdBlink = 5f;

	public float _MinAlpha = 0.5f;

	public float _MaxAlpha = 1f;

	private Camera _Camera;

	private float _Time;

	private void Start()
	{
		if (!(null == _Camera))
		{
			return;
		}
		Object[] array = Object.FindObjectsOfType(typeof(Camera));
		Object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Camera camera = (Camera)array2[i];
			if (camera.name.Contains("Main"))
			{
				_Camera = camera;
				break;
			}
		}
	}

	private void Update()
	{
		if (null != _Camera)
		{
			_Time += Time.deltaTime;
			float t = (Mathf.Sin(_Time * _SpdScale) + 1f) * 0.5f;
			t = Mathf.Lerp(_MinScale, _MaxScale, t);
			base.transform.localScale = Vector3.one * t;
			base.transform.LookAt(_Camera.transform.position);
			if (_ClrName.Length > 0 && _SpdBlink > 0f)
			{
				Color color = base.renderer.material.GetColor(_ClrName);
				float t2 = (Mathf.Sin(_Time * _SpdBlink) + 1f) * 0.5f;
				color.a = Mathf.Lerp(_MinAlpha, _MaxAlpha, t2);
				base.renderer.material.SetColor(_ClrName, color);
			}
		}
	}
}
