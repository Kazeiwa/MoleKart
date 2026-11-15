using System.Collections.Generic;
using System.Xml;

public class SceneManager
{
	private struct SceneConfig
	{
		public int Id;

		public string SimpleName;

		public int parentID;

		public int childID;

		public string name;

		public int loadType;

		public int AddtionID;
	}

	private static string currentLanguage;

	private static string currentPlatform;

	private static string xml_path;

	private Dictionary<int, SceneConfig> sceneConfigMap;

	private Dictionary<string, int> sceneSimpleNameToIdMap;

	private int m_CurSceneId;

	private static SceneManager instance;

	public static SceneManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int CurGameSceneId
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public string CurSceneName
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool Init()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseXml(XmlDocument xml)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool RebackToLastLevel()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GoToNextLevel()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool LoadLevel(int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool LoadLevelAdditive(int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void LoadEmptyLevel()
	{
	}

	private bool LoadLevel(string name, string addtionLevel)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool LoadLevelAsyn(string name, string addtionLevel)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public string GetSceneName(int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetSceneID(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
