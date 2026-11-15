using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml;
using UnityEngine;

public class SystemConfig
{
	private class Nested
	{
		internal static readonly SystemConfig Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new SystemConfig();
		}
	}

	private const string VERSION_NAME = "version";

	private ConfigClass m_config;

	public static string configFilePath = Application.dataPath + "/Resources/Config/xmlConfig/Config.xml";

	public LocalUserConfig localUserConfig = LocalUserConfig.Instance;

	private int versionNumber;

	private string uuid;

	private readonly string[] booleanOptionNames = new string[25]
	{
		"standalone", "AutoUseProps", "StuckCheat", "FallenCheat", "GizmosMinimalCurveRadius", "GizmosPathFollowing", "GizmosWallAvoidance", "GizmosCollisionAvoidance", "GizmosMonsterAvoidance", "GizmosFrontVehicleAvoidance",
		"GizmosBackVehicleAvoidance", "GizmosPropAcquiring", "GizmosVehicleName", "GizmosVehicleSteeringState", "GizmosMonsterName", "GizmosSeparation", "GizmosTotalForce", "SetNpcInvisible", "GizmosWorldPartition", "GizmosRockMonster",
		"HasSimpleNetUI", "SendPropPackage", "EnableCarMovePkg", "DisableMonsterMovementMessageSending", "DisableMonsterMovementMessageReceiving"
	};

	public int VersionNumber => versionNumber;

	public string UUID
	{
		get
		{
			if (uuid == null && uuid == null)
			{
				uuid = SystemInfo.deviceUniqueIdentifier;
			}
			return uuid;
		}
		set
		{
			uuid = value;
		}
	}

	public int DebugLevel { get; set; }

	public bool Standalone { get; set; }

	public uint EnterLevelID { get; set; }

	public bool AutoUseProps { get; set; }

	public bool StuckCheat { get; set; }

	public bool FallenCheat { get; set; }

	public bool SendPropPackage { get; set; }

	public bool GizmosMinimalCurveRadius { get; set; }

	public bool GizmosPathFollowing { get; set; }

	public bool GizmosWallAvoidance { get; set; }

	public bool GizmosCollisionAvoidance { get; set; }

	public bool GizmosMonsterAvoidance { get; set; }

	public bool GizmosFrontVehicleAvoidance { get; set; }

	public bool GizmosBackVehicleAvoidance { get; set; }

	public bool GizmosPropAcquiring { get; set; }

	public bool GizmosVehicleName { get; set; }

	public bool GizmosVehicleSteeringState { get; set; }

	public bool GizmosMonsterName { get; set; }

	public bool GizmosSeparation { get; set; }

	public bool GizmosTotalForce { get; set; }

	public bool SetNpcInvisible { get; set; }

	public bool HasSimpleNetUI { get; set; }

	public bool EnableCarMovePkg { get; set; }

	public bool DisableMonsterMovementMessageSending { get; set; }

	public bool DisableMonsterMovementMessageReceiving { get; set; }

	public bool GizmosWorldPartition { get; set; }

	public bool GizmosRockMonster { get; set; }

	public static SystemConfig Instance => Nested.Instance;

	private SystemConfig()
	{
		DebugLevel = 0;
		EnterLevelID = 1101u;
		GizmosRockMonster = true;
	}

	private void LoadControlSetting()
	{
		if (!localUserConfig.HasLocalUserConfigFile())
		{
			localUserConfig.CreateLocalUserConfigFile();
		}
		localUserConfig.LoadDataFromDisk();
	}

	public void Load()
	{
		LoadControlSetting();
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("Config/xmlConfig/Config") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		m_config = new ConfigClass(xmlDocument);
		string floatString = m_config.ReadConfig("standalone");
		Standalone = Utility.StringToFloat(floatString) > 0f;
		floatString = m_config.ReadConfig("EnterLevel");
		EnterLevelID = (uint)Utility.StringToFloat(floatString);
		floatString = m_config.ReadConfig("debug");
		DebugLevel = (int)Utility.StringToFloat(floatString);
		NiLog.iDebug = DebugLevel;
		versionNumber = Convert.ToInt32(m_config.ReadConfig("version"));
		string[] array = booleanOptionNames;
		foreach (string name in array)
		{
			ReadBooleanOption(name);
		}
		AchievementManager.Instance.LoadAchievement();
	}

	public void Save()
	{
		localUserConfig.SaveDataToDisk();
	}

	public void CreateConfigFile()
	{
		XmlDocument xmlDocument = new XmlDocument();
		XmlElement newChild = xmlDocument.CreateElement("Config");
		xmlDocument.AppendChild(newChild);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
		XmlElement xmlElement = xmlDocument.CreateElement("standalone");
		xmlElement.InnerText = "0";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("EnterLevel");
		xmlElement.InnerText = "1000";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("debug");
		xmlElement.InnerText = "0";
		xmlNode.AppendChild(xmlElement);
		xmlElement = xmlDocument.CreateElement("ControlType");
		xmlElement.InnerText = "0";
		xmlNode.AppendChild(xmlElement);
		string[] array = booleanOptionNames;
		foreach (string name in array)
		{
			xmlElement = xmlDocument.CreateElement(name);
			xmlElement.InnerText = "0";
			xmlNode.AppendChild(xmlElement);
		}
		xmlDocument.Save(configFilePath);
	}

	private void ReadBooleanOption(string name)
	{
		bool flag = m_config.ReadBool(name);
		Type type = GetType();
		PropertyInfo property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
		if (property != null && property.PropertyType == typeof(bool))
		{
			property.SetValue(this, flag, null);
		}
	}
}
