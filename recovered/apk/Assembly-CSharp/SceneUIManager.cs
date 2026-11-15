using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SceneUIManager
{
	private static SceneUIManager instance;

	public SceneUIController m_SceneUIController;

	public GameObject m_Loading;

	public Texture2D finishTex;

	public Texture2D againNoremal;

	public Texture2D againSelected;

	public Texture2D backButtonNormal;

	public Texture2D backButtonSelected;

	public Texture2D rankImgTex;

	public Texture2D timePointTex;

	public Texture2D timePoint2Tex;

	public Texture2D unFinishedTimeTex;

	public Texture2D unfinishedTex;

	public Texture2D breakRecordTex;

	public Texture2D[] rankBarTex = new Texture2D[2];

	public Texture2D[] rankNumTex = new Texture2D[6];

	public Texture2D[] rankTimeNumTex = new Texture2D[10];

	public Dictionary<string, Texture2D> rankIconTex = new Dictionary<string, Texture2D>();

	public string[] m_CurStrings;

	private static string xml_path = "Config/xmlConfig/RankUIConfig";

	public bool isShowingTutorialUI;

	public static SceneUIManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new SceneUIManager();
			}
			return instance;
		}
	}

	public PauseUI PauseUIController
	{
		get
		{
			if (null != m_SceneUIController)
			{
				return m_SceneUIController.PauseUIController;
			}
			return null;
		}
	}

	public RealtimeRankUIController RealtimeUI
	{
		get
		{
			if (null != m_SceneUIController)
			{
				return m_SceneUIController.RealtimeUI;
			}
			return null;
		}
	}

	public void Init(string language, string plat)
	{
		if (!(null != finishTex))
		{
			rankBarTex = new Texture2D[2];
			rankNumTex = new Texture2D[6];
			rankTimeNumTex = new Texture2D[10];
			rankIconTex = new Dictionary<string, Texture2D>();
			againNoremal = Resources.Load("UI/Rank/" + language + "/playAgain") as Texture2D;
			againSelected = Resources.Load("UI/Rank/" + language + "/playAgain_2") as Texture2D;
			backButtonNormal = Resources.Load("UI/Rank/" + language + "/changeTrack") as Texture2D;
			backButtonSelected = Resources.Load("UI/Rank/" + language + "/changeTrack_2") as Texture2D;
			rankImgTex = Resources.Load("UI/Rank/" + language + "/Rank") as Texture2D;
			timePointTex = Resources.Load("UI/time_point") as Texture2D;
			timePoint2Tex = Resources.Load("UI/time_point2") as Texture2D;
			unFinishedTimeTex = Resources.Load("UI/Rank/" + language + "/unfinished_bar") as Texture2D;
			if (plat == string.Empty)
			{
				plat = "ipad";
			}
			SetRankNumTex(plat);
			SetTimeNumTex(plat);
			SetBarTex(plat);
			ParseRankUIConfig(plat);
		}
	}

	public void UnInit()
	{
		finishTex = null;
		againNoremal = null;
		againSelected = null;
		backButtonNormal = null;
		backButtonSelected = null;
		rankImgTex = null;
		timePointTex = null;
		timePoint2Tex = null;
		unFinishedTimeTex = null;
		unfinishedTex = null;
		breakRecordTex = null;
		rankBarTex = null;
		rankNumTex = null;
		rankTimeNumTex = null;
		rankIconTex.Clear();
		rankIconTex = null;
		m_SceneUIController = null;
		Resources.UnloadUnusedAssets();
	}

	private void SetRankNumTex(string plat)
	{
		for (int i = 1; i <= 6; i++)
		{
			rankNumTex[i - 1] = Resources.Load("UI/Rank/" + plat + "/" + i + "th") as Texture2D;
		}
	}

	private void SetTimeNumTex(string plat)
	{
		for (int i = 0; i < 10; i++)
		{
			rankTimeNumTex[i] = Resources.Load("UI/Rank/time_num" + i) as Texture2D;
		}
	}

	private void SetBarTex(string plat)
	{
		rankBarTex[0] = Resources.Load("UI/Rank/" + plat + "/back") as Texture2D;
		rankBarTex[1] = Resources.Load("UI/Rank/" + plat + "/back_1") as Texture2D;
	}

	private bool ParseRankUIConfig(string plat)
	{
		XmlDocument xmlDocument = new XmlDocument();
		Object obj = Resources.Load(xml_path);
		if (null != obj)
		{
			TextAsset textAsset = (TextAsset)obj;
			xmlDocument.LoadXml(textAsset.text);
			if (xmlDocument == null)
			{
				return false;
			}
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Root");
			if (xmlNode != null)
			{
				XmlNodeList xmlNodeList = xmlNode.SelectNodes("Role");
				if (xmlNodeList != null)
				{
					foreach (XmlNode item in xmlNodeList)
					{
						if (item != null)
						{
							XmlNode xmlNode3 = item.SelectSingleNode("Path");
							XmlNode xmlNode4 = item.SelectSingleNode("ID");
							if (xmlNode3 == null || xmlNode4 == null)
							{
								Debug.LogError("RankUIConfig is error, please correct");
								return false;
							}
							if (!rankIconTex.ContainsKey(xmlNode4.InnerText))
							{
								rankIconTex.Add(xmlNode4.InnerText, Resources.Load("UI/Rank/" + plat + "/" + xmlNode3.InnerText) as Texture2D);
							}
						}
					}
					return true;
				}
				Debug.LogWarning("Have no Role node in RankUIConfig.xml file");
				return false;
			}
			Debug.LogWarning("Root node is null in RankUIConfig.xml file");
			return false;
		}
		return false;
	}

	public void RotateCar(Car car)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.RotateCar(car);
		}
	}

	public void InitSceneUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.InitSceneUI();
		}
	}

	public void InitOnlineGameSceneUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.InitOnlineGameSceneUI();
		}
	}

	public void ShowTime(float fTime)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowTime(fTime);
		}
	}

	public void ShowLap(int lap)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowLap(lap);
		}
	}

	public void ShowSmallMap()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowSmallMap();
		}
	}

	public void DisableLoadingUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.DisableLoadingUI();
		}
	}

	public void DisableCountDown()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.DisableCountDown();
		}
	}

	public void CountDown(int index)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.CountDown(index);
		}
	}

	public void ShowProp(ePropType type)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowProp(type);
		}
	}

	public void RollPropUI(ePropType type)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.RollPropUI(type);
		}
	}

	public void HideProp()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HideProp();
		}
	}

	public void ShowProp()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowProp();
		}
	}

	public void HideSmallMap()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HideSmallMap();
		}
	}

	public void HideLap()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HideLap();
		}
	}

	public void HideTime()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HideTime();
		}
	}

	public void FadeIn(float fTime)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.FadeIn(fTime);
		}
	}

	public void AutoFadeInAndOut(float fTime)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.AutoFadeInAndOut(fTime);
		}
	}

	public void HideReverseUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HideReverseUI();
		}
	}

	public void ShowReverseUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowReverseUI();
		}
	}

	public void SetRaceStarted(bool started)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.SetRaceIsStarted(started);
		}
	}

	public bool RaceIsStarted()
	{
		if (null != m_SceneUIController)
		{
			return m_SceneUIController.RaceIsStarted();
		}
		return false;
	}

	public void ShowPauseUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowPauseUI();
		}
	}

	public void HidePauseUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HidePauseUI();
		}
	}

	public void ShowRealtimeRank()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowRealtimeRank();
		}
	}

	public void HideRealtimeRank()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HideRealtimeRank();
		}
	}

	public void HideIntroUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HideIntroUI();
		}
	}

	public void ShowIntroUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowIntroUI();
		}
	}

	public void ShowTutorialUI(bool show)
	{
		if (null != m_SceneUIController)
		{
			isShowingTutorialUI = show;
			m_SceneUIController.ShowTutorial(show);
		}
	}

	public void DestroyTutorialUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.DestroyTutorialUI();
		}
	}

	public void SetCanShowSlider(bool canShow)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.SetCanShowSlider(canShow);
		}
	}

	public void SetCanShowDrift(bool canShow)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.SetCanShowDrift(canShow);
		}
	}

	public void ShowSlider(bool show)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowSlider(show);
		}
	}

	public void ShowRankUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowRankUI();
		}
	}

	public void ShowRankUI(bool isNetwork, bool isHost)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowRankUI(isNetwork, isHost);
		}
	}

	public void HideRankUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.HideRankUI();
		}
	}

	public void ShowHandbreak(bool show)
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.ShowHandbreak(show);
		}
	}

	public void RemoveAllSceneUIExceptCount()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.RemoveAllSceneUIExceptCount();
		}
	}

	public void RemoveAllSceneUI()
	{
		if (null != m_SceneUIController)
		{
			m_SceneUIController.RemoveAllSceneUI();
		}
	}
}
