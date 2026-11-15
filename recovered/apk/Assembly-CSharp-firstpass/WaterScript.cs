using UnityEngine;

[ExecuteInEditMode]
public class WaterScript : MonoBehaviour
{
	private void Update()
	{
		if ((bool)base.renderer)
		{
			Material sharedMaterial = base.renderer.sharedMaterial;
			if ((bool)sharedMaterial)
			{
				Vector4 vector = new Vector4(1f, 1f, 1f, 1f);
				float num = 0.2f;
				float num2 = Time.time / 20f;
				Vector4 vector2 = vector * (num2 * num);
				Vector4 vector3 = new Vector4(Mathf.Repeat(vector2.x, 1f), Mathf.Repeat(vector2.y, 1f), Mathf.Repeat(vector2.z, 1f), Mathf.Repeat(vector2.w, 1f));
				Matrix4x4 matrix = Matrix4x4.TRS(s: new Vector3(1f / num, 1f / num, 1f), pos: new Vector3(vector3.x, vector3.y, 0f), q: Quaternion.identity);
				sharedMaterial.SetMatrix("_WaveMatrix", matrix);
			}
		}
	}
}
