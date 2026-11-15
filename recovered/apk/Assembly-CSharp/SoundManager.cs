using System.Collections;
using System.Xml;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	protected static SoundManager instance;

	protected JukeboxController m_jukebox;

	protected ArrayList m_Audios = new ArrayList();

	protected ArrayList m_PauseAudios = new ArrayList();

	public static SoundManager Instance
	{
		get
		{
			if (instance == null)
			{
				string currentPlatform = AppMain.Instance.GetCurrentPlatform();
				if (currentPlatform == "iphone" || iPhoneSettings.generation == iPhoneGeneration.iPad1Gen)
				{
					instance = new GameObject("SoundManager").AddComponent<SoundManagerSimple>();
				}
				else if (currentPlatform == "ipad")
				{
					instance = new GameObject("SoundManager").AddComponent<SoundManager>();
				}
				else
				{
					instance = new GameObject("SoundManager").AddComponent<SoundManager>();
				}
				Object.DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}

	public virtual void Init()
	{
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("Config/xmlConfig/SoundConfig") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		GameObject gameObject = new GameObject("Jukebox");
		gameObject.transform.parent = base.transform;
		m_jukebox = new JukeboxController(gameObject);
		m_jukebox.ReadFile(xmlDocument);
	}

	public virtual void Shutdown()
	{
	}

	public virtual void PlaySound(int id)
	{
		if (m_jukebox != null)
		{
			m_jukebox.PlayClip(id);
		}
	}

	public virtual void PlaySoundEx(int id)
	{
		PlaySound(id);
	}

	public virtual void StopSound(int id)
	{
		if (m_jukebox != null)
		{
			m_jukebox.StopClip(id);
		}
	}

	public virtual void PauseSound(int id)
	{
		if (m_jukebox != null)
		{
			m_jukebox.PauseClip(id);
		}
	}

	public bool IsPlaying(int id)
	{
		if (m_jukebox != null)
		{
			return m_jukebox.IsPlaying(id);
		}
		return false;
	}

	public void PauseAllMonoSound()
	{
		if (m_jukebox != null)
		{
			m_jukebox.PauseAllClip();
		}
	}

	public void ContinueAllMonoSound()
	{
		if (m_jukebox != null)
		{
			m_jukebox.ContinueAllClip();
		}
	}

	public void ClearPauseMonoSound()
	{
		if (m_jukebox != null)
		{
			m_jukebox.ClearPauseMonoSound();
		}
	}

	public virtual void FadeSoundToStop(int id, float deltaTime)
	{
		if (m_jukebox != null)
		{
			m_jukebox.FadeSoundToStop(id, deltaTime);
		}
	}

	public void SetVolumn(int id, float volumn)
	{
		if (m_jukebox != null)
		{
			m_jukebox.SetVolumn(id, volumn);
		}
	}

	protected Sound GetSoundByID(int id)
	{
		if (m_jukebox != null)
		{
			return m_jukebox.GetSoundByID(id);
		}
		return new Sound
		{
			id = -1
		};
	}

	public virtual AudioSource AddAudio(GameObject obj, int id)
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
		audioSource.rolloffMode = soundByID.rolloff;
		audioSource.maxDistance = soundByID.maxDis;
		audioSource.dopplerLevel = 0f;
		m_Audios.Add(audioSource);
		return audioSource;
	}

	public virtual void PlayAudio(GameObject obj, AudioSource source, int priority)
	{
		if (source != null)
		{
			source.Play();
		}
	}

	public virtual void StopAudio(AudioSource source)
	{
		if (source != null)
		{
			source.Stop();
		}
	}

	public virtual void RemoveAudio(AudioSource audio)
	{
		if ((!(null == audio) || m_Audios != null) && m_Audios.Count != 0)
		{
			m_Audios.Remove(audio);
		}
	}

	public virtual void ClearSteroAudios()
	{
		if (m_Audios != null)
		{
			m_Audios.Clear();
		}
	}

	public virtual void PauseSteroSound()
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
	}

	public virtual void ContiueSteroSound()
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
	}

	public void StopAllMonoSound()
	{
		if (m_jukebox != null)
		{
			m_jukebox.StopAllMonoClip();
		}
	}

	public void StopAllSteroSound()
	{
		for (int i = 0; i < m_Audios.Count; i++)
		{
			if (m_Audios[i] != null)
			{
				AudioSource audioSource = (AudioSource)m_Audios[i];
				if (audioSource != null && audioSource.isPlaying)
				{
					audioSource.Stop();
				}
			}
		}
	}

	public void StopAllSound()
	{
		StopAllSteroSound();
		StopAllMonoSound();
	}

	public virtual void StopBackgroundMusic(bool isPause)
	{
		if (m_jukebox != null)
		{
			m_jukebox.StopBackgroundMusic(isPause);
		}
	}

	public virtual void PlayBackgroundMusic()
	{
		if (m_jukebox != null)
		{
			m_jukebox.PlayBackgroundMusic();
		}
	}

	public virtual void PauseBackgroundMusic()
	{
		if (m_jukebox != null)
		{
			m_jukebox.PauseBackgroundMusic();
		}
	}

	public virtual void LoadLevelSounds(int iLevel)
	{
		if (m_jukebox != null)
		{
			m_jukebox.LoadLevelSounds(iLevel);
		}
	}

	public virtual void FreeLevelSounds(int iLevel)
	{
		if (m_jukebox != null)
		{
			m_jukebox.FreeLevelSounds(iLevel);
		}
	}
}
