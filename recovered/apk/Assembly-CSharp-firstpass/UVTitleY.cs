using UnityEngine;

public class UVTitleY : MonoBehaviour
{
	public float m_Speed = 3f;

	private float m_Pos;

	private float m_Time;

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		m_Time += deltaTime;
		Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(0f, 0f - m_Pos, 0f), Quaternion.identity, new Vector3(1f, 60f, 1f));
		base.renderer.material.SetMatrix("_TexMatrix", matrix);
		m_Pos += deltaTime * m_Speed;
	}
}
