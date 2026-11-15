using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class RaceModeFactory
{
	private static IRaceMode m_curRaceMode;

	private static Dictionary<int, ScenePointConfig> m_ScenePointMap = new Dictionary<int, ScenePointConfig>();

	private static string Xml_Path = "Config/xmlConfig/ScenePointConfig";

	public static IRaceMode CurrentRaceMode => m_curRaceMode;

	public static Dictionary<int, ScenePointConfig> ScenePointMap => m_ScenePointMap;

	public static IRaceMode CreateCurrentGameMode(RacingType eMode)
	{
		switch (eMode)
		{
		case RacingType.PropSingleMode:
			m_curRaceMode = new PropRaceMode();
			return m_curRaceMode;
		case RacingType.SpeedSingleMode:
			m_curRaceMode = new TimingRaceMode();
			return m_curRaceMode;
		case RacingType.OnlinePropMode:
			m_curRaceMode = new OnlinePropMode();
			return m_curRaceMode;
		case RacingType.OnlineTimingMode:
			m_curRaceMode = new OnlineTimingMode();
			return m_curRaceMode;
		default:
			return null;
		}
	}

	public static void Init()
	{
		if (!InitSceneConfig())
		{
			Debug.LogError("RaceModeFactory.Init is error, please check");
			Debug.Break();
		}
	}

	private static bool InitSceneConfig()
	{
		if (m_ScenePointMap != null && m_ScenePointMap.Count > 0)
		{
			return true;
		}
		XmlDocument xmlDocument = new XmlDocument();
		Object obj = Resources.Load(Xml_Path);
		if (null != obj)
		{
			TextAsset textAsset = (TextAsset)obj;
			xmlDocument.LoadXml(textAsset.text);
			return ParseXml(xmlDocument);
		}
		return false;
	}

	private static bool ParseXml(XmlDocument xml)
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
						XmlNode xmlNode5 = item.SelectSingleNode("SceneBox");
						XmlNode xmlNode6 = item.SelectSingleNode("PropWay");
						XmlNode xmlNode7 = item.SelectSingleNode("RaceTrackPath");
						XmlNode xmlNode8 = item.SelectSingleNode("TotalLaps");
						XmlNode xmlNode9 = item.SelectSingleNode("TotalTime");
						if (xmlNode3 == null || xmlNode4 == null || xmlNode5 == null || xmlNode6 == null || xmlNode7 == null || xmlNode8 == null || xmlNode9 == null)
						{
							Debug.LogError("ScenePointConfig is error, please correct");
							return false;
						}
						ScenePointConfig scenePointConfig = new ScenePointConfig();
						scenePointConfig.PropWay = xmlNode6.InnerText;
						scenePointConfig.SceneBox = xmlNode5.InnerText;
						scenePointConfig.RaceTrackPath = xmlNode7.InnerText;
						scenePointConfig.Total_Laps = int.Parse(xmlNode8.InnerText);
						scenePointConfig.Total_Time = float.Parse(xmlNode9.InnerText);
						if (!m_ScenePointMap.ContainsKey(int.Parse(xmlNode4.InnerText)))
						{
							m_ScenePointMap.Add(int.Parse(xmlNode4.InnerText), scenePointConfig);
						}
					}
				}
				return true;
			}
			Debug.LogWarning("Have no Level node in ScenePointConfig.xml file");
			return false;
		}
		Debug.LogWarning("Levels node is null in ScenePointConfig.xml file");
		return false;
	}
}
