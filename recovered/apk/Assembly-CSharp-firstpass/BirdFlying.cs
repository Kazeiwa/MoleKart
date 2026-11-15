using UnityEngine;

public class BirdFlying : MonoBehaviour
{
	private Transform m_FlyTransForm;

	private Vector3 m_position;

	public float m_Radio = 10f;

	public float m_speed = 40f;

	private Vector3 m_temp = Vector3.zero;

	private float m_time;

	private void Start()
	{
		m_FlyTransForm = base.gameObject.transform;
		m_position = new Vector3(m_FlyTransForm.position.x, m_FlyTransForm.position.y, m_FlyTransForm.position.z);
		base.gameObject.AddComponent<AudioSource>();
		AudioClip clip = (AudioClip)Resources.Load("Sound/20102");
		base.audio.clip = clip;
		base.audio.loop = true;
		base.audio.rolloffMode = AudioRolloffMode.Linear;
		base.audio.maxDistance = 70f;
		base.audio.Play();
		m_temp = new Vector3(m_position.x + m_Radio, m_position.y, m_position.z);
	}

	private void Update()
	{
		if (null != m_FlyTransForm)
		{
			m_time += Time.deltaTime;
			if (!(m_time <= 0.1f))
			{
				m_FlyTransForm.RotateAround(m_temp, Vector3.up, m_speed * m_time);
				m_time = 0f;
			}
		}
	}
}
