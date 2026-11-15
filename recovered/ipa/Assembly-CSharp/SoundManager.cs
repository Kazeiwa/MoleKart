using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	protected static SoundManager instance;

	protected JukeboxController m_jukebox;

	protected ArrayList m_Audios;

	protected ArrayList m_PauseAudios;

	public static SoundManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual void Init()
	{
	}

	public virtual void Shutdown()
	{
	}

	public virtual void PlaySound(int id)
	{
	}

	public virtual void StopSound(int id)
	{
	}

	public virtual void PauseSound(int id)
	{
	}

	public bool IsPlaying(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PauseAllMonoSound()
	{
	}

	public void ContinueAllMonoSound()
	{
	}

	public void ClearPauseMonoSound()
	{
	}

	public virtual void FadeSoundToStop(int id, float deltaTime)
	{
	}

	public void SetVolumn(int id, float volumn)
	{
	}

	public void SetPitch(int id, float pitch)
	{
	}

	protected Sound GetSoundByID(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual AudioSource AddAudio(GameObject obj, int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void PlayAudio(GameObject obj, AudioSource source)
	{
	}

	public virtual void StopAudio(AudioSource source)
	{
	}

	public virtual void RemoveAudio(AudioSource audio)
	{
	}

	public virtual void ClearSteroAudios()
	{
	}

	public virtual void PauseSteroSound()
	{
	}

	public virtual void ContiueSteroSound()
	{
	}

	public void StopAllMonoSound()
	{
	}

	public void StopAllSteroSound()
	{
	}

	public void StopAllSound()
	{
	}

	public virtual void StopBackgroundMusic(bool isPause)
	{
	}

	public virtual void PlayBackgroundMusic()
	{
	}

	public virtual void PauseBackgroundMusic()
	{
	}

	public virtual void LoadLevelSounds(int iLevel)
	{
	}

	public virtual void FreeLevelSounds(int iLevel)
	{
	}
}
