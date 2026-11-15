using RaceTrack;
using UnityEngine;

public class QuakeCarState : FState
{
	public static float QUAKE_TIME = 6.5f;

	private float m_QuakeTime;

	private bool m_EftPlayed;

	private bool m_SoundPlayed;

	public QuakeCarState(CarActor actor)
		: base(ChildMode.ExclusiveStates, null)
	{
		m_PlayerCar = actor.GetPlayerCar();
		m_Type = EventType.ToQuake;
		m_HighPriority.Add(EventType.ToDead);
		m_HighPriority.Add(EventType.ToMissile);
	}

	public override void Entered()
	{
		if (m_CanRepeat || m_StateFlag != StateFlag.UPDATING)
		{
			m_StateFlag = StateFlag.UPDATING;
			m_EftPlayed = false;
			m_QuakeTime = 0f;
			m_SoundPlayed = false;
			if (m_PlayerCar.isHero)
			{
				m_PlayerCar.PlayEarthQuakeEffect();
			}
		}
	}

	public override void Update()
	{
		m_QuakeTime += Time.deltaTime;
		if (!m_SoundPlayed && m_QuakeTime >= 1f)
		{
			SoundManager.Instance.PlaySound(52801);
			m_SoundPlayed = true;
		}
		if (!m_EftPlayed && m_QuakeTime >= 4.8f)
		{
			m_PlayerCar.PlayQuakRotEft();
			m_EftPlayed = true;
			if ((m_PlayerCar.carState & 0x40) == 0)
			{
				m_PlayerCar.transform.position += new Vector3(0f, 1f, 0f);
			}
			foreach (IMonster monster in RaceWorld.Instance.Monsters)
			{
				monster.KnockedOut = true;
			}
		}
		if (m_EftPlayed && m_PlayerCar.transform.position.y <= HeightMapManager.Instance.Height(m_PlayerCar.transform.position.x, m_PlayerCar.transform.position.z) + 0.2f)
		{
			m_PlayerCar.DropProp(drop: true);
			FEvent evt = new TransToRotateEvt(this);
			m_Machine.PostEvent(evt, FStateMachine.EventPriority.HighPriority);
			m_StateFlag = StateFlag.OVER;
		}
	}

	public override void Exited()
	{
		m_StateFlag = StateFlag.STOP;
		m_PlayerCar.StopScreenEft(eEffectType.EARTH_QUAKE.ToString());
	}
}
