using System.Collections;
using System.Xml;
using UnityEngine;

public class SoundManagerSimple : SoundManager
{
	private float MAX_DIS_SQR = 2500f;

	private float MAX_DIS = 50f;

	protected ArrayList m_LoopAudios = new ArrayList();

	public bool m_bPauseSound;

	public override void Init()
	{
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("Config/xmlConfig/SoundConfig") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		GameObject gameObject = new GameObject("Jukebox");
		gameObject.transform.parent = base.transform;
		m_jukebox = new JukeboxController(gameObject);
		m_jukebox.m_bPerformanceLevel = 1;
		m_jukebox.ReadFile(xmlDocument);
	}

	public override void Shutdown()
	{
	}

	public override void PlaySound(int id)
	{
		if (m_jukebox != null)
		{
			m_jukebox.PlayClip(id);
		}
	}

	public override void StopSound(int id)
	{
		if (m_jukebox != null)
		{
			m_jukebox.StopClip(id);
		}
	}

	public override void PauseSound(int id)
	{
		if (m_jukebox != null)
		{
			m_jukebox.PauseClip(id);
		}
	}

	private AudioListener GetListener()
	{
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject != null)
		{
			return gameObject.GetComponent<AudioListener>();
		}
		return null;
	}

	private float GetDistanceSqr(Transform from, Transform to)
	{
		return (from.position - to.position).sqrMagnitude;
	}

	public override AudioSource AddAudio(GameObject obj, int id)
	{
		if (null == obj)
		{
			return null;
		}
		Sound soundByID = GetSoundByID(id);
		if (soundByID.id == -1)
		{
			return null;
		}
		AudioSource audioSource = obj.AddComponent<AudioSource>();
		audioSource.clip = soundByID.clip;
		audioSource.loop = soundByID.loop;
		audioSource.maxDistance = MAX_DIS;
		audioSource.playOnAwake = false;
		m_Audios.Add(audioSource);
		return audioSource;
	}

	public override void PlayAudio(GameObject obj, AudioSource source, int priority)
	{
		if (!(source != null))
		{
			return;
		}
		if (!source.loop)
		{
			AudioListener listener = GetListener();
			if (!(listener != null))
			{
				return;
			}
			float distanceSqr = GetDistanceSqr(listener.transform, obj.transform);
			if (!(distanceSqr >= MAX_DIS_SQR))
			{
				float num = Mathf.Clamp01(1f - distanceSqr / MAX_DIS_SQR);
				if (num > 0.1f)
				{
					source.volume = num;
					source.PlayOneShot(source.clip);
				}
			}
		}
		else if (source.loop)
		{
			m_LoopAudios.Add(source);
		}
	}

	public void Update()
	{
		if (m_bPauseSound || m_LoopAudios.Count <= 0)
		{
			return;
		}
		AudioListener listener = GetListener();
		if (!(listener != null))
		{
			return;
		}
		foreach (AudioSource loopAudio in m_LoopAudios)
		{
			float distanceSqr = GetDistanceSqr(listener.transform, loopAudio.transform);
			if (distanceSqr >= MAX_DIS_SQR)
			{
				if (loopAudio.isPlaying)
				{
					loopAudio.Stop();
				}
				continue;
			}
			float num = Mathf.Clamp01(1f - distanceSqr / MAX_DIS_SQR);
			if (num > 0.1f)
			{
				loopAudio.volume = num;
			}
			if (!loopAudio.isPlaying)
			{
				loopAudio.Play();
			}
		}
	}

	public override void StopAudio(AudioSource source)
	{
		if (source != null)
		{
			if (!source.loop)
			{
				source.Stop();
				return;
			}
			source.Stop();
			m_LoopAudios.Remove(source);
		}
	}

	public override void FadeSoundToStop(int id, float deltaTime)
	{
		if (m_jukebox != null)
		{
			m_jukebox.FadeSoundToStop(id, deltaTime);
		}
	}

	public override void RemoveAudio(AudioSource audio)
	{
		if ((!(null == audio) || m_Audios != null) && m_Audios.Count != 0)
		{
			m_Audios.Remove(audio);
			m_LoopAudios.Remove(audio);
		}
	}

	public override void ClearSteroAudios()
	{
		if (m_Audios != null)
		{
			m_Audios.Clear();
		}
		m_LoopAudios.Clear();
	}

	public override void PauseSteroSound()
	{
		for (int i = 0; i < m_Audios.Count; i++)
		{
			if (m_Audios[i] != null)
			{
				AudioSource audioSource = (AudioSource)m_Audios[i];
				if (null != audioSource && audioSource.isPlaying)
				{
					audioSource.Pause();
					m_PauseAudios.Add(audioSource);
				}
			}
		}
		m_bPauseSound = true;
	}

	public override void ContiueSteroSound()
	{
		for (int i = 0; i < m_PauseAudios.Count; i++)
		{
			if (m_PauseAudios[i] != null)
			{
				AudioSource audioSource = (AudioSource)m_PauseAudios[i];
				if (null != audioSource)
				{
					audioSource.Play();
				}
			}
		}
		m_PauseAudios.Clear();
		m_bPauseSound = false;
	}

	public override void StopBackgroundMusic(bool isPause)
	{
		if (m_jukebox != null)
		{
			m_jukebox.StopBackgroundMusic(isPause);
		}
	}

	public override void PlayBackgroundMusic()
	{
		if (m_jukebox != null)
		{
			m_jukebox.PlayBackgroundMusic();
		}
	}

	public override void PauseBackgroundMusic()
	{
		if (m_jukebox != null)
		{
			m_jukebox.PauseBackgroundMusic();
		}
	}

	public override void LoadLevelSounds(int iLevel)
	{
		base.LoadLevelSounds(iLevel);
		m_bPauseSound = false;
	}

	public override void FreeLevelSounds(int iLevel)
	{
		base.FreeLevelSounds(iLevel);
	}
}
