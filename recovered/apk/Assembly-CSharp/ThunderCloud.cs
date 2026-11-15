using UnityEngine;

public class ThunderCloud : Prop
{
	private Car m_PlayerCar;

	public static float LIFE_TIME = 10f;

	private Transform m_Pro01;

	private void Start()
	{
		Renderer componentInChildren = base.gameObject.GetComponentInChildren<Renderer>();
		if (null != componentInChildren)
		{
			componentInChildren.enabled = true;
			componentInChildren.receiveShadows = false;
			componentInChildren.castShadows = false;
		}
		PlaySound(52901, 1);
	}

	private void Update()
	{
		if (null != m_PlayerCar && null != m_PlayerCar.m_CollideCar)
		{
			if (AppMain.Instance.IsOnlineMode)
			{
				if (m_PlayerCar.isHero)
				{
					if (m_CreatedTime > 1f)
					{
						SendMessageHandler.Instance.BroadCastThunderCloundTransfer(m_PlayerCar, m_PlayerCar.m_CollideCar);
						m_PlayerCar.DisappearThunderCloud();
						Car collideCar = m_PlayerCar.m_CollideCar;
						m_PlayerCar = collideCar;
						m_PlayerCar.m_Thunder = this;
						m_PlayerCar.UseThunderCloud();
						m_CreatedTime = 0f;
						m_Pro01 = m_PlayerCar.transform.Find("car/root/pro01");
					}
					m_PlayerCar.m_CollideCar = null;
				}
			}
			else if (m_CreatedTime > 1f)
			{
				m_PlayerCar.DisappearThunderCloud();
				Car collideCar2 = m_PlayerCar.m_CollideCar;
				m_PlayerCar.m_CollideCar = null;
				m_PlayerCar = collideCar2;
				m_PlayerCar.UseThunderCloud();
				m_CreatedTime = 0f;
				m_Pro01 = m_PlayerCar.transform.Find("car/root/pro01");
			}
		}
		if (null != m_PlayerCar)
		{
			if (null == m_Pro01)
			{
				m_Pro01 = m_PlayerCar.transform.Find("car/root/pro01");
			}
			if (null != m_Pro01)
			{
				Vector3 vector = new Vector3(m_PlayerCar.transform.position.x, m_Pro01.position.y + (m_Pro01.parent.parent.transform.localScale.y - 1f) * 0.8f, m_PlayerCar.transform.position.z);
				base.gameObject.transform.position = vector + new Vector3(0f, 2f, 0f);
			}
		}
		if ((m_PlayerCar.carState & 0x8000) != 0)
		{
			if (null != m_Audio)
			{
				m_Audio.Stop();
				SoundManager.Instance.RemoveAudio(m_Audio);
			}
			if (null != m_PlayerCar)
			{
				m_PlayerCar.DisappearThunderCloud();
			}
			TSingleton<PropManager>.Instance.Release(ePropType.THUNDER_CLOUD, this);
			Object.Destroy(base.gameObject);
			return;
		}
		m_CreatedTime += Time.deltaTime;
		if (m_CreatedTime >= LIFE_TIME)
		{
			if (null != m_Audio)
			{
				m_Audio.Stop();
				SoundManager.Instance.RemoveAudio(m_Audio);
			}
			if (null != m_PlayerCar)
			{
				m_PlayerCar.DisappearThunderCloud();
			}
			SkillManager.Instance.LightningSelf(m_PlayerCar);
			TSingleton<PropManager>.Instance.Release(ePropType.THUNDER_CLOUD, this);
			Object.Destroy(base.gameObject);
		}
	}

	public void CreateThunderCloud(Car car)
	{
		if (!(null == car))
		{
			m_PlayerCar = car;
			m_PlayerCar.m_Thunder = this;
			m_Pro01 = m_PlayerCar.transform.Find("car/root/pro01");
			car.UseThunderCloud();
		}
	}

	public void Transfer(Car destCar)
	{
		if (!(null == destCar))
		{
			m_PlayerCar.DisappearThunderCloud();
			m_PlayerCar = destCar;
			m_PlayerCar.m_Thunder = this;
			m_PlayerCar.m_CollideCar = null;
			m_PlayerCar.UseThunderCloud();
			m_CreatedTime = 0f;
			m_Pro01 = m_PlayerCar.transform.Find("car/root/pro01");
		}
	}
}
