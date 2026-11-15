using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SceneManager
{
	private struct SceneConfig
	{
		public int parentID;

		public int childID;

		public string name;

		public int loadType;

		public int AddtionID;
	}

	private static string currentLanguage = "_" + AppMain.Instance.GetCurrentSystemLanguage();

	private static string currentPlatform = "_" + AppMain.Instance.GetCurrentPlatform();

	private static string xml_path = "Config/xmlConfig/SceneConfig";

	private static Dictionary<int, SceneConfig> m_SceneMap = new Dictionary<int, SceneConfig>();

	private int m_CurSceneId = -1;

	private static SceneManager instance = null;

	public static SceneManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new SceneManager();
			}
			return instance;
		}
	}

	public int CurGameSceneId => m_CurSceneId;

	public string CurSceneName => GetSceneName(CurGameSceneId);

	private SceneManager()
	{
	}

	public bool Init()
	{
		m_CurSceneId = 1000;
		if (m_SceneMap == null || m_SceneMap.Count <= 0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				xmlDocument.LoadXml(textAsset.text);
				return ParseXml(xmlDocument);
			}
			return false;
		}
		return true;
	}

	private bool ParseXml(XmlDocument xml)
	{
		if (xml == null)
		{
			return false;
		}
		XmlNode xmlNode = xml.SelectSingleNode("Levels");
		if (xmlNode != null)
		{
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("Level");
			if (xmlNodeList != null)
			{
				foreach (XmlNode item in xmlNodeList)
				{
					if (item != null)
					{
						XmlNode xmlNode3 = item.SelectSingleNode("name");
						XmlNode xmlNode4 = item.SelectSingleNode("ID");
						XmlNode xmlNode5 = item.SelectSingleNode("ParentID");
						XmlNode xmlNode6 = item.SelectSingleNode("ChildID");
						XmlNode xmlNode7 = item.SelectSingleNode("LoadType");
						XmlNode xmlNode8 = item.SelectSingleNode("NotMultiPlatform");
						XmlNode xmlNode9 = item.SelectSingleNode("AdditonID");
						XmlNode xmlNode10 = item.SelectSingleNode("IsAddition");
						XmlNode xmlNode11 = item.SelectSingleNode("IsMultiLanguage");
						if (xmlNode3 == null || xmlNode4 == null || xmlNode5 == null || xmlNode6 == null || xmlNode7 == null || xmlNode9 == null || xmlNode10 == null)
						{
							Debug.LogError("SceneConfig is error, please correct");
							return false;
						}
						SceneConfig value = new SceneConfig
						{
							name = xmlNode3.InnerText
						};
						if (xmlNode11 != null && int.Parse(xmlNode11.InnerText) == 1)
						{
							value.name += currentLanguage;
						}
						else if (int.Parse(xmlNode10.InnerText) != 1)
						{
							value.name += "_zh-Hans";
						}
						if (xmlNode8 != null && int.Parse(xmlNode8.InnerText) == 1 && !currentPlatform.Equals("_") && !currentPlatform.Equals("_ipad"))
						{
							value.name += currentPlatform;
						}
						value.parentID = int.Parse(xmlNode5.InnerText);
						value.childID = int.Parse(xmlNode6.InnerText);
						value.loadType = int.Parse(xmlNode7.InnerText);
						value.AddtionID = int.Parse(xmlNode9.InnerText);
						if (!m_SceneMap.ContainsKey(int.Parse(xmlNode4.InnerText)))
						{
							m_SceneMap.Add(int.Parse(xmlNode4.InnerText), value);
						}
					}
				}
				return true;
			}
			Debug.LogWarning("Have no Level node in SceneConfig.xml file");
			return false;
		}
		Debug.LogWarning("Levels node is null in SceneConfig.xml file");
		return false;
	}

	public bool RebackToLastLevel()
	{
		if (m_SceneMap != null && m_SceneMap.ContainsKey(m_CurSceneId))
		{
			int parentID = m_SceneMap[m_CurSceneId].parentID;
			LoadLevel(parentID);
		}
		return false;
	}

	public bool GoToNextLevel()
	{
		if (m_SceneMap != null && m_SceneMap.ContainsKey(m_CurSceneId))
		{
			int childID = m_SceneMap[m_CurSceneId].childID;
			LoadLevel(childID);
		}
		return false;
	}

	public bool LoadLevel(int sceneID)
	{
		if (m_CurSceneId == sceneID)
		{
			return false;
		}
		if (m_SceneMap != null && m_SceneMap.ContainsKey(sceneID))
		{
			SoundManager.Instance.FreeLevelSounds(m_CurSceneId);
			SoundManager.Instance.LoadLevelSounds(sceneID);
			m_CurSceneId = sceneID;
			string addtionLevel = null;
			if (m_SceneMap.ContainsKey(m_SceneMap[sceneID].AddtionID))
			{
				addtionLevel = m_SceneMap[m_SceneMap[sceneID].AddtionID].name;
			}
			if (m_SceneMap[sceneID].loadType == 0)
			{
				return LoadLevelAsyn(m_SceneMap[sceneID].name, addtionLevel);
			}
			return LoadLevel(m_SceneMap[sceneID].name, addtionLevel);
		}
		string message = "LoadLevel: sceneID = " + sceneID + ", but failed!";
		Debug.LogError(message);
		return false;
	}

	public bool LoadLevelAdditive(int sceneID)
	{
		if (m_SceneMap != null && m_SceneMap.ContainsKey(sceneID))
		{
			string text = null;
			text = m_SceneMap[sceneID].name;
			if (m_SceneMap[sceneID].loadType == 0)
			{
				Application.LoadLevelAdditiveAsync(text);
			}
		}
		return false;
	}

	public void LoadEmptyLevel()
	{
		if (RaceModeFactory.CurrentRaceMode != null)
		{
			RaceModeFactory.CurrentRaceMode.Uninit();
		}
		SoundManager.Instance.FreeLevelSounds(m_CurSceneId);
		Application.LoadLevel("emptylevel");
		m_CurSceneId = -1;
	}

	private bool LoadLevel(string name, string addtionLevel)
	{
		if (string.IsNullOrEmpty(name))
		{
			return false;
		}
		if (RaceModeFactory.CurrentRaceMode != null)
		{
			RaceModeFactory.CurrentRaceMode.Uninit();
		}
		Application.LoadLevel(name);
		if (!string.IsNullOrEmpty(addtionLevel))
		{
			Application.LoadLevelAdditive(addtionLevel);
		}
		return true;
	}

	private bool LoadLevelAsyn(string name, string addtionLevel)
	{
		if (string.IsNullOrEmpty(name))
		{
			return false;
		}
		if (RaceModeFactory.CurrentRaceMode != null)
		{
			RaceModeFactory.CurrentRaceMode.Uninit();
		}
		Application.LoadLevelAsync(name);
		if (!string.IsNullOrEmpty(addtionLevel))
		{
			Application.LoadLevelAdditiveAsync(addtionLevel);
		}
		return true;
	}

	public string GetSceneName(int sceneID)
	{
		string result = null;
		if (m_SceneMap.ContainsKey(sceneID))
		{
			result = m_SceneMap[sceneID].name;
		}
		return result;
	}
}
