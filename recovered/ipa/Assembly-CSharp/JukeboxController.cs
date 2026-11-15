using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class JukeboxController
{
	private IDictionary<int, Sound> jukebox;

	private GameObject root;

	private IDictionary<int, Sound> playingSounds;

	private IList<int> common;

	private IDictionary<int, Sound> soundTablet;

	private IDictionary<int, IList<int>> levelTablet;

	private int performanceLevel;

	private char[] splits;

	public int PerformanceLevel
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public JukeboxController(GameObject node)
	{
	}

	private void LoadMusicTablet(XmlDocument levelxml)
	{
	}

	public void LoadLevelSounds(int iLevel)
	{
	}

	public void FreeLevelSounds(int iLevel)
	{
	}

	public void AddClip(int id, int type, string path, int mode, AudioClip clip, float maxDis, float volumn, float bTime, float sTime)
	{
	}

	public void AddClip(int id, int type, string path, int mode, AudioClip clip, float maxDis, float volumn, float bTime, float sTime, int plevel)
	{
	}

	public void RemoveClip(int id)
	{
	}

	public void PlayClip(int id)
	{
	}

	public void FadeSoundToStop(int id, float deltaTime)
	{
	}

	public void StopClip(int id)
	{
	}

	public void PauseClip(int id)
	{
	}

	public void StopAllMonoClip()
	{
	}

	public void PauseAllClip()
	{
	}

	public void ContinueAllClip()
	{
	}

	public void PauseBackgroundMusic()
	{
	}

	public void PlayBackgroundMusic()
	{
	}

	public void StopBackgroundMusic(bool isPause)
	{
	}

	public void ClearPauseMonoSound()
	{
	}

	public bool IsPlaying(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetVolumn(int id, float volumn)
	{
	}

	public void SetPitch(int id, float pitch)
	{
	}

	public bool ReadFile(XmlDocument levelxml)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void ParseSound(XmlNode node)
	{
	}

	private void LoadSoundResource(Sound sound)
	{
	}

	public Sound GetSoundByID(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static int GetBackgroundSoudId()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
