using System.Runtime.CompilerServices;
using System.Xml;

public class LocalUserConfig
{
	private const string KEY_CONFIG = "Config";

	private const string KEY_TIMESTAMP_OF_SHOW_TM_ADVIEW = "TimestampOfShowTmAdView";

	private const string KeyDoneAdOther = "DoneAdOther";

	private const string KeyRaceCount = "RaceCount";

	private const string KEY_USER_NAME = "UserName";

	private const string KEY_CONTROL_TYPE = "ControlType";

	private const string KEY_PROP_USE_TIMES = "PropUseTimes";

	private ConfigClass m_config;

	private static LocalUserConfig instance;

	private static string configFilePath;

	private string controlType;

	private string propUsedTimes;

	private int timestampOfShowTmAdView;

	private bool doneAdOther;

	public int TimestampOfShowTmAdView
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool DoneAdOther
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool CanShowTutorial
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public string userName
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public CONTROL_TYPE ControlType
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public static LocalUserConfig Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool HasLocalUserConfigFile()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void LoadDataFromDisk()
	{
	}

	public void SaveDataToDisk()
	{
	}

	public void CreateLocalUserConfigFile()
	{
	}

	public bool UpdateTimestampOfShowTmAdView(int newSeconds, int threasholdSeconds)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CheckDoneAdOther(int medalCount)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SaveControlType(CONTROL_TYPE type)
	{
	}

	private CONTROL_TYPE LoadControlType()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private string LoadLocalPlayerName()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SaveLocalPlayerName(string name)
	{
	}

	private int LoadTimestampOfShowTmAdView()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SaveTimestampOfShowTmAdView(int unixSeconds)
	{
	}

	private bool LoadDoneAdOther()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SaveDoneAdOther(bool done)
	{
	}

	private void LoadCanUseProp()
	{
	}

	public void SaveCanUseProp()
	{
	}

	public void FlushGameDataToDisk()
	{
	}

	public float Record(int sceneID, int easy)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float Record(int sceneID, int easy, RacingType raceType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float Record(int sceneID, RacingType raceType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float Record(int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int Medals(int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int MedalsCurrentMode(int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int Medal(int sceneID, int easy)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int TotalMedals()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int ReleaseTrack(int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int ReleaseCharacter(int type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SaveMedal(int sceneID, int easy)
	{
	}

	public void SaveRecord(int sceneID, int easy, float fTime)
	{
	}

	public void SaveReleaseTrack(int sceneID)
	{
	}

	public void SaveReleaseCharacter(int type)
	{
	}

	public XmlNode AddTrack(int sceneID, XmlNode mode, XmlDocument xml)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AddTracks(XmlDocument xml, XmlNode root)
	{
	}

	public void AddLockTrackRecord(XmlDocument xml, XmlNode root)
	{
	}

	public XmlElement AddLockTrackRecord(XmlDocument xml, XmlNode root, int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public XmlElement AddLockCharacterRecord(XmlDocument xml, XmlNode root, int type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AddLockCharacterRecord(XmlDocument xml, XmlNode root)
	{
	}

	private static string DifficultyName(int difficulty)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static string GetCurrentModeName()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
