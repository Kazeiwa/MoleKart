using System.IO;
using System.Xml;
using FrameWork;
using UnityEngine;

public class LocalUserConfig
{
	private ConfigClass m_config;

	private static LocalUserConfig instance;

	private static string configFilePath = Application.persistentDataPath + "/LocalUserConfig.xml";

	private string controlType;

	private string propUsedTimes = "0";

	public bool CanShowTutorial
	{
		get
		{
			if (propUsedTimes == "0" || propUsedTimes == "1")
			{
				return true;
			}
			if (propUsedTimes == "2")
			{
				return false;
			}
			return false;
		}
	}

	public string userName { get; set; }

	public CONTROL_TYPE ControlType { get; set; }

	public static LocalUserConfig Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new LocalUserConfig();
			}
			return instance;
		}
	}

	public LocalUserConfig()
	{
		LoadDataFromDisk();
	}

	public bool HasLocalUserConfigFile()
	{
		return File.Exists(configFilePath);
	}

	public void LoadDataFromDisk()
	{
		if (!HasLocalUserConfigFile())
		{
			CreateLocalUserConfigFile();
		}
		else
		{
			XmlDocument xmlDocument = EncryptXMLDocument.Load(configFilePath);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode == null)
			{
				Debug.Log("Current LocalUserConfig is damaged!");
				CreateLocalUserConfigFile();
			}
		}
		userName = LoadLocalPlayerName();
		ControlType = LoadControlType();
		LoadCanUseProp();
	}

	public void SaveDataToDisk()
	{
		if (!HasLocalUserConfigFile())
		{
			CreateLocalUserConfigFile();
		}
		SaveLocalPlayerName(userName);
		SaveControlType(ControlType);
	}

	public void CreateLocalUserConfigFile()
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = new XmlDocument();
		}
		XmlElement newChild = xmlDocument.CreateElement("Config");
		xmlDocument.AppendChild(newChild);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
		XmlElement xmlElement = xmlDocument.CreateElement("ControlType");
		xmlElement.InnerText = "3";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("UserName");
		xmlElement.InnerText = "Mole";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("PropUseTimes");
		xmlElement.InnerText = "0";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("ShowFirstTip");
		xmlElement.InnerText = "1";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("LastRequiredUpdateVersion");
		xmlElement.InnerText = "1.0.0";
		xmlNode.AppendChild(xmlElement);
		AddTracks(xmlDocument, xmlNode);
		AddLockTrackRecord(xmlDocument, xmlNode);
		AddLockCharacterRecord(xmlDocument, xmlNode);
		EncryptXMLDocument.SaveToEncryptXML(xmlDocument, configFilePath);
		Debug.Log("Config File Path:" + configFilePath);
	}

	private void SaveControlType(CONTROL_TYPE type)
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		m_config.strFileName = configFilePath;
		ConfigClass config = m_config;
		int num = (int)type;
		config.SaveConfig("ControlType", num.ToString());
	}

	private CONTROL_TYPE LoadControlType()
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		controlType = m_config.ReadConfig("ControlType");
		switch (controlType)
		{
		case "0":
			ControlType = CONTROL_TYPE.CONTROLTYPE_BABY;
			break;
		case "1":
			ControlType = CONTROL_TYPE.CONTROLTYPE_CHILD;
			break;
		case "2":
			ControlType = CONTROL_TYPE.CONTROLTYPE_DAXIA;
			break;
		case "3":
			ControlType = CONTROL_TYPE.CONTROLTYPE_MASTER;
			break;
		default:
			ControlType = CONTROL_TYPE.CONTROLTYPE_BABY;
			break;
		}
		return ControlType;
	}

	private string LoadLocalPlayerName()
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		userName = m_config.ReadConfig("UserName");
		return userName;
	}

	private void SaveLocalPlayerName(string name)
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		m_config.strFileName = configFilePath;
		m_config.SaveConfig("UserName", name);
		Debug.Log("SaveLocalPlayerName");
	}

	private void LoadCanUseProp()
	{
		XmlDocument xmlDocument = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDocument);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
		XmlNode xmlNode2 = xmlDocument.SelectSingleNode("PropUseTimes");
		string text = m_config.ReadConfig("PropUseTimes");
		if (text == string.Empty)
		{
			Debug.Break();
			XmlElement xmlElement = xmlDocument.CreateElement("PropUseTimes");
			xmlElement.InnerText = "0";
			xmlNode.AppendChild(xmlElement);
			xmlDocument.Save(configFilePath);
			propUsedTimes = "0";
		}
		else
		{
			propUsedTimes = m_config.ReadConfig("PropUseTimes");
		}
	}

	public void SaveCanUseProp()
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		m_config.strFileName = configFilePath;
		int num;
		string configValue = (propUsedTimes = propUsedTimes switch
		{
			"0" => "1", 
			_ => (num != 1) ? "2" : "2", 
		});
		m_config.SaveConfig("PropUseTimes", configValue);
	}

	public bool LoadShowFirstTip()
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		string text = m_config.ReadConfig("ShowFirstTip");
		return text == "1";
	}

	public void SaveShowFirstTip(bool bShow)
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		m_config.strFileName = configFilePath;
		m_config.SaveConfig("ShowFirstTip", (!bShow) ? "0" : "1");
	}

	public string LoadLastRequiredUpdateVersion()
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		return m_config.ReadConfig("LastRequiredUpdateVersion");
	}

	public void SaveLastRequiredUpdateVersion(string version)
	{
		XmlDocument xmlDoc = EncryptXMLDocument.Load(configFilePath);
		m_config = new ConfigClass(xmlDoc);
		m_config.strFileName = configFilePath;
		m_config.SaveConfig("LastRequiredUpdateVersion", version);
	}

	public float Record(int sceneID, int easy)
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			string xpath = "Normal";
			switch (easy)
			{
			case 0:
				xpath = "Easy";
				break;
			case 2:
				xpath = "Diffcult";
				break;
			}
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 == null)
				{
					AddTracks(xmlDocument, xmlNode);
					return 0f;
				}
				xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 != null)
				{
					string xpath2 = "PropMode";
					IFrameWork frameWork = CFrameWork.GetFrameWork();
					if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && (raceLogicModule.GetCurrentRaceType() == RacingType.OnlineTimingMode || raceLogicModule.GetCurrentRaceType() == RacingType.SpeedSingleMode))
					{
						xpath2 = "SpeedMode";
					}
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath2);
					if (xmlNode3 != null)
					{
						XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
						if (xmlNode4 != null)
						{
							XmlNode xmlNode5 = xmlNode4.SelectSingleNode(xpath);
							if (xmlNode5 != null)
							{
								XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Record");
								if (xmlNode6 != null)
								{
									return float.Parse(xmlNode6.InnerText);
								}
							}
						}
					}
				}
			}
		}
		return 0f;
	}

	public float Record(int sceneID, int easy, RacingType raceType)
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			string xpath = "Normal";
			switch (easy)
			{
			case 0:
				xpath = "Easy";
				break;
			case 2:
				xpath = "Diffcult";
				break;
			}
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 == null)
				{
					AddTracks(xmlDocument, xmlNode);
					return 0f;
				}
				xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 != null)
				{
					string xpath2 = "PropMode";
					if (raceType == RacingType.OnlineTimingMode || raceType == RacingType.SpeedSingleMode)
					{
						xpath2 = "SpeedMode";
					}
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath2);
					if (xmlNode3 != null)
					{
						XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
						if (xmlNode4 != null)
						{
							XmlNode xmlNode5 = xmlNode4.SelectSingleNode(xpath);
							if (xmlNode5 != null)
							{
								XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Record");
								if (xmlNode6 != null)
								{
									return float.Parse(xmlNode6.InnerText);
								}
							}
						}
					}
				}
			}
		}
		return 0f;
	}

	public float Record(int sceneID)
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 == null)
				{
					AddTracks(xmlDocument, xmlNode);
					return 0f;
				}
				xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 != null)
				{
					string xpath = "PropMode";
					IFrameWork frameWork = CFrameWork.GetFrameWork();
					if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && (raceLogicModule.GetCurrentRaceType() == RacingType.OnlineTimingMode || raceLogicModule.GetCurrentRaceType() == RacingType.SpeedSingleMode))
					{
						xpath = "SpeedMode";
					}
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath);
					if (xmlNode3 != null)
					{
						XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
						if (xmlNode4 != null)
						{
							float num = 0f;
							XmlNode xmlNode5 = xmlNode4.SelectSingleNode("Normal");
							if (xmlNode5 != null)
							{
								XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Record");
								if (xmlNode6 != null)
								{
									float num2 = float.Parse(xmlNode6.InnerText);
									num = ((num != 0f && (!(num2 > 0f) || !(num > num2))) ? num : num2);
								}
							}
							xmlNode5 = xmlNode4.SelectSingleNode("Easy");
							if (xmlNode5 != null)
							{
								XmlNode xmlNode7 = xmlNode5.SelectSingleNode("Record");
								if (xmlNode7 != null)
								{
									float num3 = float.Parse(xmlNode7.InnerText);
									num = ((num != 0f && (!(num3 > 0f) || !(num > num3))) ? num : num3);
								}
							}
							xmlNode5 = xmlNode4.SelectSingleNode("Diffcult");
							if (xmlNode5 != null)
							{
								XmlNode xmlNode8 = xmlNode5.SelectSingleNode("Record");
								if (xmlNode8 != null)
								{
									float num4 = float.Parse(xmlNode8.InnerText);
									num = ((num != 0f && (!(num4 > 0f) || !(num > num4))) ? num : num4);
								}
							}
							return num;
						}
					}
				}
			}
		}
		return 0f;
	}

	public int Medals(int sceneID)
	{
		int num = 0;
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 == null)
				{
					AddTracks(xmlDocument, xmlNode);
					return 0;
				}
				xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 != null)
				{
					for (int i = 0; i < 2; i++)
					{
						string xpath = "PropMode";
						if (i == 1)
						{
							xpath = "SpeedMode";
						}
						XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath);
						if (xmlNode3 == null)
						{
							continue;
						}
						XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
						if (xmlNode4 == null)
						{
							continue;
						}
						for (int j = 0; j < 3; j++)
						{
							string xpath2 = "Normal";
							switch (j)
							{
							case 1:
								xpath2 = "Easy";
								break;
							case 2:
								xpath2 = "Diffcult";
								break;
							}
							XmlNode xmlNode5 = xmlNode4.SelectSingleNode(xpath2);
							if (xmlNode5 != null)
							{
								XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Medal");
								if (xmlNode6 != null)
								{
									num += int.Parse(xmlNode6.InnerText);
								}
							}
						}
					}
				}
			}
		}
		return num;
	}

	public int MedalsCurrentMode(int sceneID)
	{
		int num = 0;
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 == null)
				{
					AddTracks(xmlDocument, xmlNode);
					return 0;
				}
				xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 != null)
				{
					string xpath = "PropMode";
					IFrameWork frameWork = CFrameWork.GetFrameWork();
					if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && (raceLogicModule.GetCurrentRaceType() == RacingType.OnlineTimingMode || raceLogicModule.GetCurrentRaceType() == RacingType.SpeedSingleMode))
					{
						xpath = "SpeedMode";
					}
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath);
					if (xmlNode3 != null)
					{
						XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
						if (xmlNode4 != null)
						{
							for (int i = 0; i < 3; i++)
							{
								string xpath2 = "Normal";
								switch (i)
								{
								case 1:
									xpath2 = "Easy";
									break;
								case 2:
									xpath2 = "Diffcult";
									break;
								}
								XmlNode xmlNode5 = xmlNode4.SelectSingleNode(xpath2);
								if (xmlNode5 != null)
								{
									XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Medal");
									if (xmlNode6 != null)
									{
										num += int.Parse(xmlNode6.InnerText);
									}
								}
							}
						}
					}
				}
			}
		}
		return num;
	}

	public int Medal(int sceneID, int easy)
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 == null)
				{
					AddTracks(xmlDocument, xmlNode);
					return 0;
				}
				xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 != null)
				{
					string xpath = "PropMode";
					IFrameWork frameWork = CFrameWork.GetFrameWork();
					if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && (raceLogicModule.GetCurrentRaceType() == RacingType.OnlineTimingMode || raceLogicModule.GetCurrentRaceType() == RacingType.SpeedSingleMode))
					{
						xpath = "SpeedMode";
					}
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath);
					if (xmlNode3 != null)
					{
						XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
						if (xmlNode4 != null)
						{
							string xpath2 = "Easy";
							switch (easy)
							{
							case 1:
								xpath2 = "Normal";
								break;
							case 2:
								xpath2 = "Diffcult";
								break;
							}
							XmlNode xmlNode5 = xmlNode4.SelectSingleNode(xpath2);
							if (xmlNode5 != null)
							{
								XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Medal");
								if (xmlNode6 != null)
								{
									return int.Parse(xmlNode6.InnerText);
								}
							}
						}
					}
				}
			}
		}
		return 0;
	}

	public int TotalMedals()
	{
		int num = 0;
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 == null)
				{
					AddTracks(xmlDocument, xmlNode);
					return 0;
				}
				xmlNode2 = xmlNode.SelectSingleNode("Tracks");
				if (xmlNode2 != null)
				{
					for (int i = 0; i < 2; i++)
					{
						string xpath = "PropMode";
						if (i == 1)
						{
							xpath = "SpeedMode";
						}
						XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath);
						if (xmlNode3 == null)
						{
							continue;
						}
						for (int j = 1; j <= 9; j++)
						{
							XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + (1100 + j));
							if (xmlNode4 == null)
							{
								continue;
							}
							for (int k = 0; k < 3; k++)
							{
								string xpath2 = "Normal";
								switch (k)
								{
								case 1:
									xpath2 = "Easy";
									break;
								case 2:
									xpath2 = "Diffcult";
									break;
								}
								XmlNode xmlNode5 = xmlNode4.SelectSingleNode(xpath2);
								if (xmlNode5 != null)
								{
									XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Medal");
									if (xmlNode6 != null)
									{
										num += int.Parse(xmlNode6.InnerText);
									}
								}
							}
						}
					}
				}
			}
		}
		return num;
	}

	public int ReleaseTrack(int sceneID)
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("LockTracks");
				if (xmlNode2 == null)
				{
					AddLockTrackRecord(xmlDocument, xmlNode);
					return 0;
				}
				xmlNode2 = xmlNode.SelectSingleNode("LockTracks");
				if (xmlNode2 != null)
				{
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode("ID_" + sceneID);
					if (xmlNode3 != null)
					{
						return int.Parse(xmlNode3.InnerText);
					}
				}
			}
		}
		return 0;
	}

	public int ReleaseCharacter(int type)
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument != null)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("LockCharacters");
				if (xmlNode2 == null)
				{
					AddLockCharacterRecord(xmlDocument, xmlNode);
					return 0;
				}
				xmlNode2 = xmlNode.SelectSingleNode("LockCharacters");
				if (xmlNode2 != null)
				{
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode("ID_" + type);
					if (xmlNode3 != null)
					{
						return int.Parse(xmlNode3.InnerText);
					}
				}
			}
		}
		return 0;
	}

	public void SaveMedal(int sceneID, int easy)
	{
		if (TrackLockManager.Instance.ReleaseTrack(sceneID) != 0 || AppMain.Instance.IsOnlineMode)
		{
			return;
		}
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument == null)
		{
			return;
		}
		string xpath = "Normal";
		switch (easy)
		{
		case 0:
			xpath = "Easy";
			break;
		case 2:
			xpath = "Diffcult";
			break;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
		if (xmlNode == null)
		{
			return;
		}
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
		if (xmlNode2 == null)
		{
			AddTracks(xmlDocument, xmlNode);
		}
		xmlNode2 = xmlNode.SelectSingleNode("Tracks");
		if (xmlNode2 == null)
		{
			return;
		}
		string xpath2 = "PropMode";
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && (raceLogicModule.GetCurrentRaceType() == RacingType.OnlineTimingMode || raceLogicModule.GetCurrentRaceType() == RacingType.SpeedSingleMode))
		{
			xpath2 = "SpeedMode";
		}
		XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath2);
		if (xmlNode3 == null)
		{
			return;
		}
		XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
		if (xmlNode4 == null)
		{
			AddSceneNode(xmlDocument, xmlNode3, sceneID);
			xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
		}
		if (xmlNode4 == null)
		{
			return;
		}
		XmlNode xmlNode5 = xmlNode4.SelectSingleNode(xpath);
		if (xmlNode5 != null)
		{
			XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Medal");
			if (xmlNode6 != null && int.Parse(xmlNode6.InnerText) == 0)
			{
				xmlNode6.InnerText = "1";
				EncryptXMLDocument.SaveToEncryptXML(xmlDocument, configFilePath);
			}
		}
	}

	public void SaveRecord(int sceneID, int easy, float fTime)
	{
		if (TrackLockManager.Instance.ReleaseTrack(sceneID) != 0)
		{
			return;
		}
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument == null)
		{
			return;
		}
		string xpath = "Normal";
		switch (easy)
		{
		case 0:
			xpath = "Easy";
			break;
		case 2:
			xpath = "Diffcult";
			break;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
		if (xmlNode == null)
		{
			return;
		}
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("Tracks");
		if (xmlNode2 == null)
		{
			AddTracks(xmlDocument, xmlNode);
		}
		xmlNode2 = xmlNode.SelectSingleNode("Tracks");
		if (xmlNode2 == null)
		{
			return;
		}
		string xpath2 = "PropMode";
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && (raceLogicModule.GetCurrentRaceType() == RacingType.OnlineTimingMode || raceLogicModule.GetCurrentRaceType() == RacingType.SpeedSingleMode))
		{
			xpath2 = "SpeedMode";
		}
		XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath2);
		if (xmlNode3 == null)
		{
			return;
		}
		XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
		if (xmlNode4 == null)
		{
			AddSceneNode(xmlDocument, xmlNode3, sceneID);
			xmlNode4 = xmlNode3.SelectSingleNode("ID_" + sceneID);
		}
		if (xmlNode4 == null)
		{
			return;
		}
		XmlNode xmlNode5 = xmlNode4.SelectSingleNode(xpath);
		if (xmlNode5 == null)
		{
			return;
		}
		XmlNode xmlNode6 = xmlNode5.SelectSingleNode("Record");
		if (xmlNode6 != null)
		{
			int num = (int)(fTime * 100f);
			int num2 = int.Parse(xmlNode6.InnerText);
			if (num2 > num || num2 <= 0)
			{
				xmlNode6.InnerText = num.ToString();
				EncryptXMLDocument.SaveToEncryptXML(xmlDocument, configFilePath);
			}
		}
	}

	public void SaveReleaseTrack(int sceneID)
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument == null)
		{
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
		if (xmlNode == null)
		{
			return;
		}
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("LockTracks");
		if (xmlNode2 == null)
		{
			AddLockTrackRecord(xmlDocument, xmlNode);
		}
		xmlNode2 = xmlNode.SelectSingleNode("LockTracks");
		if (xmlNode2 != null)
		{
			XmlNode xmlNode3 = xmlNode2.SelectSingleNode("ID_" + sceneID);
			if (xmlNode3 != null)
			{
				xmlNode3.InnerText = "1";
				EncryptXMLDocument.SaveToEncryptXML(xmlDocument, configFilePath);
			}
		}
	}

	public void SaveReleaseCharacter(int type)
	{
		XmlDocument xmlDocument = null;
		if (xmlDocument == null)
		{
			xmlDocument = EncryptXMLDocument.Load(configFilePath);
		}
		if (xmlDocument == null)
		{
			return;
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
		if (xmlNode == null)
		{
			return;
		}
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("LockCharacters");
		if (xmlNode2 == null)
		{
			AddLockCharacterRecord(xmlDocument, xmlNode);
		}
		xmlNode2 = xmlNode.SelectSingleNode("LockCharacters");
		if (xmlNode2 != null)
		{
			XmlNode xmlNode3 = xmlNode2.SelectSingleNode("ID_" + type);
			if (xmlNode3 != null)
			{
				xmlNode3.InnerText = "1";
				EncryptXMLDocument.SaveToEncryptXML(xmlDocument, configFilePath);
			}
		}
	}

	public void AddTracks(XmlDocument xml, XmlNode root)
	{
		if (root == null || xml == null)
		{
			return;
		}
		XmlNode xmlNode = xml.CreateElement("Tracks");
		root.AppendChild(xmlNode);
		for (int i = 0; i < 2; i++)
		{
			string name = "PropMode";
			if (i == 1)
			{
				name = "SpeedMode";
			}
			XmlNode xmlNode2 = xml.CreateElement(name);
			xmlNode.AppendChild(xmlNode2);
			for (int j = 1; j <= 9; j++)
			{
				XmlNode xmlNode3 = xml.CreateElement("ID_110" + j);
				xmlNode2.AppendChild(xmlNode3);
				XmlNode xmlNode4 = xml.CreateElement("Easy");
				xmlNode3.AppendChild(xmlNode4);
				XmlElement xmlElement = xml.CreateElement("Record");
				xmlElement.InnerText = "0";
				xmlNode4.AppendChild(xmlElement);
				XmlElement xmlElement2 = xml.CreateElement("Medal");
				xmlElement2.InnerText = "0";
				xmlNode4.AppendChild(xmlElement2);
				XmlNode xmlNode5 = xml.CreateElement("Normal");
				xmlNode3.AppendChild(xmlNode5);
				xmlElement = xml.CreateElement("Record");
				xmlElement.InnerText = "0";
				xmlNode5.AppendChild(xmlElement);
				xmlElement2 = xml.CreateElement("Medal");
				xmlElement2.InnerText = "0";
				xmlNode5.AppendChild(xmlElement2);
				XmlNode xmlNode6 = xml.CreateElement("Diffcult");
				xmlNode3.AppendChild(xmlNode6);
				xmlElement = xml.CreateElement("Record");
				xmlElement.InnerText = "0";
				xmlNode6.AppendChild(xmlElement);
				xmlElement2 = xml.CreateElement("Medal");
				xmlElement2.InnerText = "0";
				xmlNode6.AppendChild(xmlElement2);
			}
		}
	}

	private void AddSceneNode(XmlDocument xml, XmlNode mode, int sceneID)
	{
		XmlNode xmlNode = xml.CreateElement("ID_" + sceneID);
		mode.AppendChild(xmlNode);
		XmlNode xmlNode2 = xml.CreateElement("Easy");
		xmlNode.AppendChild(xmlNode2);
		XmlElement xmlElement = xml.CreateElement("Record");
		xmlElement.InnerText = "0";
		xmlNode2.AppendChild(xmlElement);
		XmlElement xmlElement2 = xml.CreateElement("Medal");
		xmlElement2.InnerText = "0";
		xmlNode2.AppendChild(xmlElement2);
		XmlNode xmlNode3 = xml.CreateElement("Normal");
		xmlNode.AppendChild(xmlNode3);
		xmlElement = xml.CreateElement("Record");
		xmlElement.InnerText = "0";
		xmlNode3.AppendChild(xmlElement);
		xmlElement2 = xml.CreateElement("Medal");
		xmlElement2.InnerText = "0";
		xmlNode3.AppendChild(xmlElement2);
		XmlNode xmlNode4 = xml.CreateElement("Diffcult");
		xmlNode.AppendChild(xmlNode4);
		xmlElement = xml.CreateElement("Record");
		xmlElement.InnerText = "0";
		xmlNode4.AppendChild(xmlElement);
		xmlElement2 = xml.CreateElement("Medal");
		xmlElement2.InnerText = "0";
		xmlNode4.AppendChild(xmlElement2);
	}

	public void AddLockTrackRecord(XmlDocument xml, XmlNode root)
	{
		if (root != null && xml != null)
		{
			XmlNode xmlNode = xml.CreateElement("LockTracks");
			root.AppendChild(xmlNode);
			for (int i = 1; i <= 9; i++)
			{
				XmlElement xmlElement = xml.CreateElement("ID_110" + i);
				xmlElement.InnerText = "0";
				xmlNode.AppendChild(xmlElement);
			}
			xml.Save(configFilePath);
		}
	}

	public void AddLockCharacterRecord(XmlDocument xml, XmlNode root)
	{
		if (root != null && xml != null)
		{
			XmlNode xmlNode = xml.CreateElement("LockCharacters");
			root.AppendChild(xmlNode);
			XmlElement xmlElement = xml.CreateElement("ID_" + 2101);
			xmlElement.InnerText = "0";
			xmlNode.AppendChild(xmlElement);
			xmlElement = xml.CreateElement("ID_" + 2004);
			xmlElement.InnerText = "0";
			xmlNode.AppendChild(xmlElement);
			xmlElement = xml.CreateElement("ID_" + 2003);
			xmlElement.InnerText = "0";
			xmlNode.AppendChild(xmlElement);
			xmlElement = xml.CreateElement("ID_" + 2001);
			xmlElement.InnerText = "0";
			xmlNode.AppendChild(xmlElement);
			xmlElement = xml.CreateElement("ID_" + 2201);
			xmlElement.InnerText = "0";
			xmlNode.AppendChild(xmlElement);
			xmlElement = xml.CreateElement("ID_" + 2002);
			xmlElement.InnerText = "0";
			xmlNode.AppendChild(xmlElement);
			EncryptXMLDocument.SaveToEncryptXML(xml, configFilePath);
		}
	}
}
