using UnityEngine;

public class FadeInAndOut : ISceneUI
{
	private Transform m_Transform;

	private Color m_Color;

	private bool m_BeginFadeIn;

	private bool m_BeginFadeOut;

	private bool m_FadeInAndOutAuto;

	private float m_FadeInTime;

	private float m_FadeOutTime;

	private float m_FadeInAndOutTime;

	private GameObject m_obj;

	public void InitUI(Transform transform)
	{
		if (!(null == transform))
		{
			m_Transform = transform;
			m_obj = transform.FindChild("Plane").gameObject;
			m_obj.active = true;
			m_Color = new Color(0f, 0f, 0f, 0f);
			m_BeginFadeIn = false;
			m_BeginFadeOut = false;
			m_obj.renderer.material.color = m_Color;
		}
	}

	public void UpdateUI()
	{
		if (m_FadeInAndOutAuto)
		{
			m_FadeInAndOutTime += Time.deltaTime;
			if (m_FadeInAndOutTime < 0.1f)
			{
				m_Color.a += m_FadeInAndOutTime * 10f;
				if (m_Color.a >= 1f)
				{
					m_Color.a = 1f;
				}
			}
			else if (m_FadeInAndOutTime >= 0.1f && m_FadeInAndOutTime < 1.5f)
			{
				m_Color.a = 1f;
			}
			else if (m_FadeInAndOutTime >= 1.5f && m_FadeInAndOutTime < 2.5f)
			{
				float num = m_FadeInAndOutTime - 1.5f;
				m_Color.a -= num;
				if (m_Color.a <= 0f)
				{
					m_Color.a = 0f;
					m_FadeInAndOutAuto = false;
				}
			}
			else
			{
				m_FadeInAndOutAuto = false;
			}
			m_obj.renderer.material.color = m_Color;
		}
		else if (m_BeginFadeIn)
		{
			m_FadeInTime += Time.deltaTime;
			m_Color.a += m_FadeInTime;
			if (m_Color.a >= 1f)
			{
				m_Color.a = 1f;
				m_BeginFadeIn = false;
			}
			m_obj.renderer.material.color = m_Color;
		}
		else if (m_BeginFadeOut)
		{
			m_FadeOutTime += Time.deltaTime;
			m_Color.a -= m_FadeOutTime;
			if (m_Color.a <= 0f)
			{
				m_Color.a = 0f;
				m_BeginFadeOut = false;
			}
			m_obj.renderer.material.color = m_Color;
		}
	}

	public void ShowTime(float fTime)
	{
	}

	public void HideTime()
	{
	}

	public void FadeIn(float fTime)
	{
		m_BeginFadeIn = true;
		m_FadeInTime = 0f;
	}

	public void FadeOut(float fTime)
	{
		m_BeginFadeOut = true;
		m_FadeOutTime = 0f;
	}

	public void AutoFadeInAndOut(float fTime)
	{
		m_FadeInAndOutAuto = true;
		m_FadeInAndOutTime = 0f;
	}
}
