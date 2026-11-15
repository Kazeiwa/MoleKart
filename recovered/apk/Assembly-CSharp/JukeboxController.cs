using System.Collections;
using System.Xml;
using UnityEngine;

public class JukeboxController
{
	private Hashtable jukebox;

	private GameObject root;

	private Hashtable playingSounds = new Hashtable();

	private ArrayList common = new ArrayList();

	private Hashtable soundTablet = new Hashtable();

	private Hashtable levelTablet = new Hashtable();

	private Sound exclusiveSound = default(Sound);

	public int m_bPerformanceLevel = 1;

	private char[] splits = new char[1] { '/' };

	public JukeboxController(GameObject node)
	{
		jukebox = new Hashtable();
		root = node;
	}

	private void LoadMusicTablet(XmlDocument levelxml)
	{
		XmlNode xmlNode = levelxml.SelectSingleNode("Levels/LevelConfig");
		if (xmlNode != null)
		{
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("Common");
			XmlNodeList xmlNodeList = xmlNode2.SelectNodes("id");
			foreach (XmlNode item in xmlNodeList)
			{
				int num = int.Parse(item.InnerText);
				if (common.Contains(num))
				{
					Debug.LogWarning("JukeboxController--->common error!" + num + "duplicated key");
				}
				else
				{
					common.Add(num);
				}
			}
			xmlNodeList = xmlNode.SelectNodes("Level");
			foreach (XmlNode item2 in xmlNodeList)
			{
				int num2 = int.Parse(item2.Attributes["id"].Value);
				ArrayList arrayList = new ArrayList();
				XmlNodeList xmlNodeList2 = item2.SelectNodes("id");
				foreach (XmlNode item3 in xmlNodeList2)
				{
					int num3 = int.Parse(item3.InnerText);
					if (arrayList.Contains(num3))
					{
						Debug.LogWarning("JukeboxController--->Level " + num2 + " error!duplicated key " + num3);
					}
					else
					{
						arrayList.Add(num3);
					}
				}
				levelTablet.Add(num2, arrayList);
			}
		}
		foreach (int item4 in common)
		{
			if (soundTablet.Contains(item4))
			{
				Sound sound = (Sound)soundTablet[item4];
				LoadSoundResource(sound);
			}
			else
			{
				Debug.LogError("Couldn't find sound " + item4 + "Sound Tablet!");
			}
		}
		Resources.UnloadUnusedAssets();
	}

	public void LoadLevelSounds(int iLevel)
	{
		if (levelTablet.Contains(iLevel))
		{
			ArrayList arrayList = levelTablet[iLevel] as ArrayList;
			foreach (int item in arrayList)
			{
				if (soundTablet.Contains(item))
				{
					Sound sound = (Sound)soundTablet[item];
					LoadSoundResource(sound);
				}
				else
				{
					Debug.LogError("Couldn't find sound " + item + "Sound Tablet!");
				}
			}
		}
		Resources.UnloadUnusedAssets();
	}

	public void FreeLevelSounds(int iLevel)
	{
		if (levelTablet.Contains(iLevel))
		{
			ArrayList arrayList = levelTablet[iLevel] as ArrayList;
			foreach (int item in arrayList)
			{
				RemoveClip(item);
			}
		}
		Resources.UnloadUnusedAssets();
	}

	public void AddClip(int id, int priority, int type, string path, int mode, AudioClip clip, float maxDis, float volumn, float bTime, float sTime)
	{
		Debug.LogWarning("id = " + id);
		Sound sound = new Sound
		{
			id = id,
			priority = priority,
			path = path,
			loop = (mode == 1),
			fLength = clip.length,
			type = type,
			clip = clip,
			rolloff = AudioRolloffMode.Linear,
			maxDis = maxDis,
			volumn = volumn
		};
		if (type == 0)
		{
			sound.audioObj = new GameObject(string.Empty + id);
			sound.audioObj.transform.parent = root.transform;
			sound.audio = sound.audioObj.AddComponent<AudioSource>();
			sound.audio.clip = sound.clip;
			sound.audio.rolloffMode = sound.rolloff;
			sound.audio.loop = sound.loop;
			sound.audio.playOnAwake = false;
			sound.audio.volume = sound.volumn;
			sound.audio.time = bTime;
			sound.audio.ignoreListenerVolume = true;
		}
		jukebox.Add(id, sound);
	}

	public void AddClip(int id, int priority, int type, string path, int mode, AudioClip clip, float maxDis, float volumn, float bTime, float sTime, int plevel)
	{
		if (plevel != 1)
		{
			AddClip(id, priority, type, path, mode, clip, maxDis, volumn, bTime, sTime);
		}
	}

	public void RemoveClip(int id)
	{
		if (jukebox.Contains(id))
		{
			Sound sound = (Sound)jukebox[id];
			Debug.Log("Remove Clip = " + sound.path);
			Object.DestroyImmediate(sound.audioObj, allowDestroyingAssets: true);
			jukebox.Remove(id);
		}
	}

	public void PlayClip(int id)
	{
		if (jukebox.Contains(id) && jukebox[id] != null && ((Sound)jukebox[id]).type == 0 && !((Sound)jukebox[id]).audio.isPlaying)
		{
			((Sound)jukebox[id]).audio.Play();
		}
	}

	public void PlayExclusiveClip(int id)
	{
		if (jukebox.Contains(id) && jukebox[id] != null)
		{
			Sound sound = (Sound)jukebox[id];
			int priority = sound.priority;
			if (exclusiveSound.audio == null)
			{
				exclusiveSound = sound;
				exclusiveSound.priority = priority;
				exclusiveSound.audio.Play();
			}
			else if (!exclusiveSound.audio.isPlaying)
			{
				exclusiveSound = sound;
				exclusiveSound.priority = priority;
				exclusiveSound.audio.Play();
			}
			else if (exclusiveSound.priority < priority)
			{
				exclusiveSound.audio.Stop();
				exclusiveSound = sound;
				exclusiveSound.priority = priority;
				exclusiveSound.audio.Play();
			}
		}
	}

	public void PlayExclusiveClip(AudioSource source, int priority)
	{
		if (exclusiveSound.audio == null)
		{
			exclusiveSound.audio = source;
			exclusiveSound.priority = priority;
			exclusiveSound.audio.Play();
		}
		else if (!exclusiveSound.audio.isPlaying)
		{
			exclusiveSound.audio = source;
			exclusiveSound.priority = priority;
			exclusiveSound.audio.Play();
		}
		else if (exclusiveSound.priority < priority)
		{
			exclusiveSound.audio.Stop();
			exclusiveSound.audio = source;
			exclusiveSound.priority = priority;
			exclusiveSound.audio.Play();
		}
	}

	public void FadeSoundToStop(int id, float deltaTime)
	{
		if (jukebox.Contains(id) && jukebox[id] != null && ((Sound)jukebox[id]).type == 0)
		{
			if (((Sound)jukebox[id]).audio.isPlaying)
			{
				((Sound)jukebox[id]).audio.volume -= 1f * Time.deltaTime / deltaTime;
			}
			if (((Sound)jukebox[id]).audio.volume <= 0f)
			{
				StopClip(id);
			}
		}
	}

	public void StopClip(int id)
	{
		if (jukebox.Contains(id) && jukebox[id] != null && ((Sound)jukebox[id]).type == 0 && ((Sound)jukebox[id]).audio.isPlaying)
		{
			((Sound)jukebox[id]).audio.Stop();
		}
	}

	public void PauseClip(int id)
	{
		if (jukebox.Contains(id) && jukebox[id] != null && ((Sound)jukebox[id]).type == 0 && ((Sound)jukebox[id]).audio.isPlaying)
		{
			((Sound)jukebox[id]).audio.Pause();
		}
	}

	public void StopAllMonoClip()
	{
		foreach (DictionaryEntry item in jukebox)
		{
			if (((Sound)item.Value).audio != null)
			{
				((Sound)item.Value).audio.Stop();
			}
		}
		playingSounds.Clear();
	}

	public void PauseAllClip()
	{
		foreach (DictionaryEntry item in jukebox)
		{
			if (IsPlaying((int)item.Key))
			{
				((Sound)item.Value).audio.Pause();
				playingSounds.Add(item.Key, item.Value);
			}
		}
	}

	public void ContinueAllClip()
	{
		if (jukebox == null)
		{
			return;
		}
		foreach (DictionaryEntry playingSound in playingSounds)
		{
			if (((Sound)playingSound.Value).audio != null)
			{
				((Sound)playingSound.Value).audio.Play();
			}
		}
		playingSounds.Clear();
	}

	public void PauseBackgroundMusic()
	{
		PauseClip(SceneManager.Instance.CurGameSceneId switch
		{
			1101 => 10101, 
			1102 => 10201, 
			1103 => 10301, 
			1104 => 10401, 
			1105 => 10501, 
			1106 => 10601, 
			1107 => 10701, 
			1108 => 10801, 
			1109 => 10901, 
			_ => 10101, 
		});
	}

	public void PlayBackgroundMusic()
	{
		PlayClip(SceneManager.Instance.CurGameSceneId switch
		{
			1101 => 10101, 
			1102 => 10201, 
			1103 => 10301, 
			1104 => 10401, 
			1105 => 10501, 
			1106 => 10601, 
			1107 => 10701, 
			1108 => 10801, 
			1109 => 10901, 
			_ => 10101, 
		});
	}

	public void StopBackgroundMusic(bool isPause)
	{
		int id = SceneManager.Instance.CurGameSceneId switch
		{
			1101 => 10101, 
			1102 => 10201, 
			1103 => 10301, 
			1104 => 10401, 
			1105 => 10501, 
			1106 => 10601, 
			1107 => 10701, 
			1108 => 10801, 
			1109 => 10901, 
			_ => 10101, 
		};
		if (isPause)
		{
			PauseClip(id);
		}
		else
		{
			StopClip(id);
		}
	}

	public void ClearPauseMonoSound()
	{
		playingSounds.Clear();
	}

	public bool IsPlaying(int id)
	{
		if (jukebox.Contains(id) && ((Sound)jukebox[id]).type == 0)
		{
			return ((Sound)jukebox[id]).audio.isPlaying;
		}
		return false;
	}

	public void SetVolumn(int id, float volumn)
	{
		if (jukebox.Contains(id) && jukebox[id] != null && ((Sound)jukebox[id]).type == 0)
		{
			((Sound)jukebox[id]).audio.volume = volumn;
		}
	}

	public bool ReadFile(XmlDocument levelxml)
	{
		bool result = false;
		XmlNode xmlNode = levelxml.SelectSingleNode("Levels/AudioTable");
		if (xmlNode != null)
		{
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("AudioClip");
			foreach (XmlNode item in xmlNodeList)
			{
				ParseSound(item);
			}
			result = true;
		}
		LoadMusicTablet(levelxml);
		return result;
	}

	private void ParseSound(XmlNode node)
	{
		XmlNode xmlNode = node.SelectSingleNode("id");
		XmlNode xmlNode2 = node.SelectSingleNode("priority");
		XmlNode xmlNode3 = node.SelectSingleNode("path");
		XmlNode xmlNode4 = node.SelectSingleNode("mode");
		XmlNode xmlNode5 = node.SelectSingleNode("type");
		XmlNode xmlNode6 = node.SelectSingleNode("maxDis");
		XmlNode xmlNode7 = node.SelectSingleNode("volumn");
		XmlNode xmlNode8 = node.SelectSingleNode("beginTime");
		XmlNode xmlNode9 = node.SelectSingleNode("stopTime");
		XmlNode xmlNode10 = node.SelectSingleNode("perfLevel");
		int num = int.Parse(xmlNode.InnerText);
		int priority = 0;
		if (xmlNode2 != null)
		{
			priority = int.Parse(xmlNode2.InnerText);
		}
		int num2 = int.Parse(xmlNode4.InnerText);
		int type = int.Parse(xmlNode5.InnerText);
		int num3 = int.Parse(xmlNode6.InnerText);
		float volumn = float.Parse(xmlNode7.InnerText);
		float num4 = float.Parse(xmlNode8.InnerText);
		float num5 = float.Parse(xmlNode9.InnerText);
		int iPerformance = 0;
		if (xmlNode10 != null)
		{
			iPerformance = int.Parse(xmlNode10.InnerText);
		}
		Sound sound = new Sound
		{
			id = num,
			priority = priority,
			path = xmlNode3.InnerText,
			type = type,
			volumn = volumn,
			maxDis = num3,
			loop = (num2 == 1),
			iPerformance = iPerformance
		};
		soundTablet.Add(num, sound);
	}

	private void LoadSoundResource(Sound sound)
	{
		AudioClip clip = null;
		if (m_bPerformanceLevel == 1)
		{
			clip = Resources.Load(sound.path) as AudioClip;
		}
		else if (m_bPerformanceLevel == 0)
		{
			string[] array = sound.path.Split(splits);
			int num = int.Parse(array[1]) / 10000;
			if (num == 1 || num == 6)
			{
				clip = Resources.Load(sound.path) as AudioClip;
			}
			else
			{
				string path = array[0] + "2D/" + array[1];
				clip = Resources.Load(path) as AudioClip;
			}
		}
		if (m_bPerformanceLevel == 1)
		{
			AddClip(sound.id, sound.priority, sound.type, sound.path, sound.loop ? 1 : 0, clip, sound.maxDis, sound.volumn, sound.fbTime, sound.fsTime);
		}
		else if (m_bPerformanceLevel == 0)
		{
			AddClip(sound.id, sound.priority, sound.type, sound.path, sound.loop ? 1 : 0, clip, sound.maxDis, sound.volumn, sound.fbTime, sound.fsTime, sound.iPerformance);
		}
	}

	public Sound GetSoundByID(int id)
	{
		Sound result = new Sound
		{
			id = -1
		};
		if (jukebox.Contains(id) && jukebox[id] != null)
		{
			return (Sound)jukebox[id];
		}
		return result;
	}
}
