using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Achievement;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	private const string configPath = "Config/xmlConfig/ArchievementConfig.xml";

	private string userAchievementPath = "Achievements.xml";

	private static AchievementManager instance;

	private Dictionary<ACHIEVEMENT, bool> achievementsDict = new Dictionary<ACHIEVEMENT, bool>();

	private Dictionary<ACHIEVEMENT, bool> showAchievement = new Dictionary<ACHIEVEMENT, bool>();

	private int masterGolds;

	private int anyGolds;

	private DateTime totalTime = DateTime.Now;

	private DateTime beginTime = DateTime.Now;

	private int onlinePlayed;

	private int version;

	public static AchievementManager Instance => instance;

	private void Awake()
	{
		instance = GetComponent<AchievementManager>();
		Init();
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void Init()
	{
		achievementsDict.Add(ACHIEVEMENT.ACHIEVEMENT_COMMENT, value: false);
		showAchievement.Add(ACHIEVEMENT.ACHIEVEMENT_COMMENT, value: true);
		userAchievementPath = Application.persistentDataPath + "/" + userAchievementPath;
	}

	public void AddPlayOnlineGame()
	{
		onlinePlayed++;
		CheckAllAchievements();
	}

	public void AddMasterGold()
	{
		masterGolds++;
		CheckAllAchievements();
	}

	public void AddAnyGold()
	{
		anyGolds++;
		CheckAllAchievements();
	}

	public void LoadAchievement()
	{
		if (!File.Exists(userAchievementPath))
		{
			CreateUserFile();
		}
		else
		{
			XmlDocument xmlDocument = EncryptXMLDocument.Load(userAchievementPath);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("version");
			int num = Convert.ToInt32(xmlNode2.InnerText);
			if (num != SystemConfig.Instance.VersionNumber)
			{
				File.Delete(userAchievementPath);
				CreateUserFile();
			}
		}
		XmlDocument xmlDocument2 = EncryptXMLDocument.Load(userAchievementPath);
		XmlNode xmlNode3 = xmlDocument2.SelectSingleNode("root");
		XmlNode xmlNode4 = xmlNode3.SelectSingleNode("masterGolds");
		masterGolds = Convert.ToInt32(xmlNode4.InnerText);
		xmlNode4 = xmlNode3.SelectSingleNode("golds");
		anyGolds = Convert.ToInt32(xmlNode4.InnerText);
		xmlNode4 = xmlNode3.SelectSingleNode("totalTime");
		totalTime = Convert.ToDateTime(xmlNode4.InnerText);
		xmlNode4 = xmlNode3.SelectSingleNode("onlinePlayed");
		onlinePlayed = Convert.ToInt32(xmlNode4.InnerText);
		XmlNode xmlNode5 = xmlNode3.SelectSingleNode("achievements");
		foreach (XmlElement childNode in xmlNode5.ChildNodes)
		{
			ACHIEVEMENT key = (ACHIEVEMENT)(int)Enum.Parse(typeof(ACHIEVEMENT), childNode.Name);
			achievementsDict[key] = childNode.InnerText == "1";
			string attribute = childNode.GetAttribute("show");
			if (attribute == "1")
			{
				showAchievement[key] = true;
			}
			else
			{
				showAchievement[key] = false;
			}
		}
	}

	public void SaveAchievement()
	{
		XmlDocument xmlDocument = EncryptXMLDocument.Load(userAchievementPath);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlElement xmlElement = xmlNode.SelectSingleNode("masterGolds") as XmlElement;
		xmlElement.InnerText = masterGolds.ToString();
		xmlElement = xmlNode.SelectSingleNode("golds") as XmlElement;
		xmlElement.InnerText = anyGolds.ToString();
		xmlElement = xmlNode.SelectSingleNode("totalTime") as XmlElement;
		xmlElement.InnerText = totalTime.ToString();
		xmlElement = xmlNode.SelectSingleNode("onlinePlayed") as XmlElement;
		xmlElement.InnerText = onlinePlayed.ToString();
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("achievements");
		foreach (ACHIEVEMENT key in achievementsDict.Keys)
		{
			if (achievementsDict[key])
			{
				XmlElement xmlElement2 = xmlNode2.SelectSingleNode(key.ToString()) as XmlElement;
				xmlElement2.InnerText = "1";
				string value = ((!showAchievement[key]) ? "0" : "1");
				xmlElement2.SetAttribute("show", value);
			}
		}
		EncryptXMLDocument.SaveToEncryptXML(xmlDocument, userAchievementPath);
	}

	public bool CheckAchievement(ACHIEVEMENT achievement)
	{
		return achievementsDict[achievement];
	}

	private void CreateUserFile()
	{
		XmlDocument xmlDocument = new XmlDocument();
		XmlElement newChild = xmlDocument.CreateElement("root");
		xmlDocument.AppendChild(newChild);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlElement xmlElement = xmlDocument.CreateElement("totalTime");
		xmlElement.InnerText = DateTime.Now.ToString();
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("masterGolds");
		xmlElement.InnerText = "0";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("golds");
		xmlElement.InnerText = "0";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("onlinePlayed");
		xmlElement.InnerText = "0";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("version");
		xmlElement.InnerText = SystemConfig.Instance.VersionNumber.ToString();
		xmlNode.AppendChild(xmlElement);
		XmlElement newChild2 = xmlDocument.CreateElement("achievements");
		xmlNode.AppendChild(newChild2);
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("achievements");
		foreach (ACHIEVEMENT key in achievementsDict.Keys)
		{
			XmlElement xmlElement2 = xmlDocument.CreateElement(key.ToString());
			xmlElement2.InnerText = "0";
			xmlElement2.SetAttribute("show", "1");
			xmlNode2.AppendChild(xmlElement2);
		}
		EncryptXMLDocument.SaveToEncryptXML(xmlDocument, userAchievementPath);
	}

	public void ShowCommentAlert()
	{
		if (CheckAchievement(ACHIEVEMENT.ACHIEVEMENT_COMMENT) && !showAchievement[ACHIEVEMENT.ACHIEVEMENT_COMMENT])
		{
		}
	}

	private void CheckAllAchievements()
	{
		if (CheckComment())
		{
			achievementsDict[ACHIEVEMENT.ACHIEVEMENT_COMMENT] = true;
		}
	}

	private bool CheckComment()
	{
		if (onlinePlayed >= 3)
		{
			return true;
		}
		if (masterGolds >= 2)
		{
			return true;
		}
		if (anyGolds >= 4)
		{
			return true;
		}
		DateTime dateTime = beginTime;
		if (dateTime.AddMinutes(20.0).CompareTo(DateTime.Now) <= 0)
		{
			return true;
		}
		return false;
	}

	private void NeverShowComment()
	{
		Debug.Log("Never show comment");
		showAchievement[ACHIEVEMENT.ACHIEVEMENT_COMMENT] = false;
	}

	private void ClearCommentData()
	{
		Debug.Log("Show comment later");
		masterGolds = 0;
		anyGolds = 0;
		beginTime = DateTime.Now;
		onlinePlayed = 0;
		achievementsDict[ACHIEVEMENT.ACHIEVEMENT_COMMENT] = false;
		showAchievement[ACHIEVEMENT.ACHIEVEMENT_COMMENT] = true;
	}

	private void ShowCommentLater()
	{
		ClearCommentData();
	}
}
