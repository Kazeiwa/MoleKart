using System;
using UnityEngine;

public class FXSpeedUp : MonoBehaviour
{
	public float m_Speed = 3f;

	private float m_Pos;

	private float m_Time;

	private Color temp = new Color(1f, 1f, 1f, 1f);

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		m_Time += deltaTime;
		Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(0f, 0f - m_Pos, 0f), Quaternion.identity, new Vector3(1f, 1f, 1f));
		base.renderer.material.SetMatrix("_TexMatrix", matrix);
		float g = Mathf.Abs(Mathf.Sin(m_Time * (float)Math.PI / 2f));
		float b = Mathf.Abs(Mathf.Cos(m_Time * (float)Math.PI / 2f));
		temp.r = 1f;
		temp.g = g;
		temp.b = b;
		base.renderer.material.SetColor("_ColorMove", temp);
		m_Pos += deltaTime * m_Speed;
	}
}
