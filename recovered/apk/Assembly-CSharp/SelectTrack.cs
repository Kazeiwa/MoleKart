using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectTrack : MonoBehaviour
{
	private GameObject m_PicPlanePre;

	private GameObject m_PicPlaneBack;

	private GameObject m_GoBtn;

	private float blinkTime;

	public Texture[] m_PicTexs;

	public Texture2D[] m_PicTimes;

	private Texture m_CurTex;

	public int m_TrackID;

	public int m_Complecity;

	public GameObject[] m_TimePlanes;

	public Texture2D[] m_TimePic;

	public GameObject m_Loading;

	private GameObject m_Return;

	private GameObject m_MedalTitle;

	private GameObject m_MedalPre1;

	private GameObject m_MedalPre2;

	private GameObject m_MedalPre3;

	private GameObject m_MedalBg1;

	private GameObject m_MedalBg2;

	private GameObject m_MedalBg3;

	private GameObject m_ReleaseBg;

	private GameObject m_ReleaseText;

	private GameObject m_Purchase;

	private GameObject m_Proceeding;

	private GameObject m_ProceedingText;

	private GameObject m_ReleaseTitle;

	public GameObject[] m_Shelters;

	public string[] m_CueString = new string[9]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	private int m_BuyID = -1;

	private string m_PurchasePrice = string.Empty;

	private bool m_Started;

	private float fStartCoolTime = 1f;

	private Dictionary<string, string> m_AlertBoxMap = new Dictionary<string, string>();

	private string m_TextFilePath = "Config/AlertBox/ReleaseTrackText";

	private float m_TempTime;

	private float m_DeleteTime = 1f;

	private void LoadAlertBoxText()
	{
		TextAsset textAsset = Resources.Load(m_TextFilePath) as TextAsset;
		if (null == textAsset)
		{
			Debug.Log("-------is null -----------");
			return;
		}
		Stream stream = new MemoryStream(textAsset.bytes);
		StreamReader streamReader = new StreamReader(stream);
		if (streamReader == null)
		{
			return;
		}
		string text;
		while ((text = streamReader.ReadLine()) != null)
		{
			if (!string.IsNullOrEmpty(text))
			{
				char[] separator = new char[1] { '*' };
				string[] array = text.Split(separator);
				Debug.Log(text);
				if (array != null && array.Length >= 2 && m_AlertBoxMap != null && !m_AlertBoxMap.ContainsKey(array[0]))
				{
					m_AlertBoxMap.Add(array[0], array[1]);
				}
			}
		}
	}

	private void Start()
	{
		if (!SoundManager.Instance.IsPlaying(60002))
		{
			SoundManager.Instance.PlaySoundEx(60002);
		}
		m_TrackID = 1100;
		LoadAlertBoxText();
		FindSaveObject();
		SetDefaultSelect();
		HideLoadingUI();
		blinkTime = 0f;
		if (m_PicTexs != null)
		{
			m_CurTex = m_PicTexs[0];
		}
		SetTrackRecordTime();
		CheckEnable();
		TSingleton<ShopDataManager>.Instance.Init();
		Resources.UnloadUnusedAssets();
		SceneUIManager.Instance.m_CurStrings = m_CueString;
	}

	private void Awake()
	{
		Resources.UnloadUnusedAssets();
	}

	private void OnDestroy()
	{
		Debug.Log("Select Track : OnDestroy!");
		Resources.UnloadUnusedAssets();
	}

	private void HideLoadingUI()
	{
		if (SceneUIManager.Instance.m_Loading != null)
		{
			UnityEngine.Object.Destroy(SceneUIManager.Instance.m_Loading);
			UtilBinding.ShowLoadingIndicator(bShow: false);
		}
		if (null == m_Loading)
		{
			m_Loading = GameObject.Find("LoadingUI");
		}
		if (null != m_Loading)
		{
			m_Loading.SetActiveRecursively(state: false);
			UnityEngine.Object.DontDestroyOnLoad(m_Loading);
			SceneUIManager.Instance.m_Loading = m_Loading;
		}
	}

	private void SetDefaultSelect()
	{
		GameObject gameObject = GameObject.Find("ScrollItems/Item1");
		if (null == gameObject)
		{
			gameObject = GameObject.Find("UI/ScrollList/Mover/Item1");
		}
		if (null != gameObject)
		{
			GameObject gameObject2 = GameObject.Find("UI/ScrollList");
			if (null != gameObject2)
			{
				UIScrollList component = gameObject2.GetComponent<UIScrollList>();
				if (null != component)
				{
					component.DidSelect(gameObject.GetComponent<UIListItem>());
				}
			}
		}
		HideAllMedal();
	}

	private void FindSaveObject()
	{
		m_PicPlanePre = GameObject.Find("UI/Show_Panel/Pic_1");
		m_PicPlanePre.active = false;
		m_PicPlaneBack = GameObject.Find("UI/Show_Panel/Pic_2");
		m_GoBtn = GameObject.Find("UI/StartGame");
		m_Return = GameObject.Find("UI/Return");
		m_MedalTitle = GameObject.Find("UI/Show_Panel/Medal_title");
		m_MedalPre1 = GameObject.Find("UI/Show_Panel/Medal_pre1");
		m_MedalPre2 = GameObject.Find("UI/Show_Panel/Medal_pre2");
		m_MedalPre3 = GameObject.Find("UI/Show_Panel/Medal_pre3");
		m_MedalBg1 = GameObject.Find("UI/Show_Panel/Medal_bg1");
		m_MedalBg2 = GameObject.Find("UI/Show_Panel/Medal_bg2");
		m_MedalBg3 = GameObject.Find("UI/Show_Panel/Medal_bg3");
		m_ReleaseBg = GameObject.Find("UI/Release/Release_Bg");
		if (null != m_ReleaseBg)
		{
			m_ReleaseBg.active = false;
		}
		m_ReleaseText = GameObject.Find("UI/Release/Release_Bg/ReleaseText");
		if (null != m_ReleaseText)
		{
			m_ReleaseText.active = false;
		}
		m_Purchase = GameObject.Find("UI/Release/Purchase");
		if (null != m_Purchase)
		{
			m_Purchase.active = false;
		}
		m_Proceeding = GameObject.Find("UI/Release/Proceeding");
		if (null != m_Proceeding)
		{
			m_Proceeding.active = false;
		}
		m_ProceedingText = GameObject.Find("UI/Release/Proceeding/ProceedingText");
		if (null != m_ProceedingText)
		{
			m_ProceedingText.active = false;
		}
		m_ReleaseTitle = GameObject.Find("UI/Release/Release_Title");
		if (null != m_ReleaseTitle)
		{
			m_ReleaseTitle.active = false;
		}
	}

	private void HideAllMedal()
	{
		if (null != m_MedalTitle)
		{
			m_MedalTitle.active = false;
		}
		if (null != m_MedalPre1)
		{
			m_MedalPre1.active = false;
		}
		if (null != m_MedalPre2)
		{
			m_MedalPre2.active = false;
		}
		if (null != m_MedalPre3)
		{
			m_MedalPre3.active = false;
		}
		if (null != m_MedalBg1)
		{
			m_MedalBg1.active = false;
		}
		if (null != m_MedalBg2)
		{
			m_MedalBg2.active = false;
		}
		if (null != m_MedalBg3)
		{
			m_MedalBg3.active = false;
		}
	}

	private void ShowMedals(int num)
	{
		if (null != m_MedalTitle)
		{
			m_MedalTitle.active = true;
		}
		if (null != m_MedalBg1)
		{
			m_MedalBg1.active = true;
		}
		if (null != m_MedalBg2)
		{
			m_MedalBg2.active = true;
		}
		if (null != m_MedalBg3)
		{
			m_MedalBg3.active = true;
		}
		if (null != m_MedalPre1)
		{
			if (num > 0)
			{
				m_MedalPre1.active = true;
			}
			else
			{
				m_MedalPre1.active = false;
			}
		}
		if (null != m_MedalPre2)
		{
			if (num > 1)
			{
				m_MedalPre2.active = true;
			}
			else
			{
				m_MedalPre2.active = false;
			}
		}
		if (null != m_MedalPre3)
		{
			if (num > 2)
			{
				m_MedalPre3.active = true;
			}
			else
			{
				m_MedalPre3.active = false;
			}
		}
	}

	private void Update()
	{
		fStartCoolTime -= Time.deltaTime;
		if (m_DeleteTime > 0f)
		{
			m_DeleteTime += Time.deltaTime;
		}
		if (m_DeleteTime > 10f)
		{
			m_DeleteTime = -1f;
		}
		m_TempTime -= Time.deltaTime;
		StartBtnBlink();
		if (m_BuyID > 0 && m_TempTime > 0f && m_TempTime < 2f)
		{
			PurchaseOver();
			m_TempTime = 0f;
		}
	}

	private void SetTrackRecordTime()
	{
		if (m_TimePlanes == null)
		{
			return;
		}
		for (int i = 0; i < m_TimePlanes.Length; i++)
		{
			if (!(null == m_TimePlanes[i]))
			{
				m_TimePlanes[i].GetComponent<MeshRenderer>().enabled = true;
				float num = LocalUserConfig.Instance.Record(1100 + i + 1);
				if (num > 0f)
				{
					int num2 = (int)num;
					int num3 = num2 / 6000;
					int num4 = num3 / 10;
					int num5 = num3 % 10;
					int num6 = num2 / 100;
					int num7 = num6 % 60 / 10;
					int num8 = num6 % 60 % 10;
					int num9 = num2 % 10;
					int num10 = num2 / 10 % 10;
					m_TimePic[i].SetPixels(5, 4, m_PicTimes[num5].width, m_PicTimes[num5].height, m_PicTimes[num5].GetPixels(0, 0, m_PicTimes[num5].width, m_PicTimes[num5].height));
					m_TimePic[i].SetPixels(43, 4, m_PicTimes[num7].width, m_PicTimes[num7].height, m_PicTimes[num7].GetPixels(0, 0, m_PicTimes[num7].width, m_PicTimes[num7].height));
					m_TimePic[i].SetPixels(67, 4, m_PicTimes[num8].width, m_PicTimes[num8].height, m_PicTimes[num8].GetPixels(0, 0, m_PicTimes[num8].width, m_PicTimes[num8].height));
					m_TimePic[i].SetPixels(115, 4, m_PicTimes[num10].width, m_PicTimes[num10].height, m_PicTimes[num10].GetPixels(0, 0, m_PicTimes[num10].width, m_PicTimes[num10].height));
					m_TimePic[i].SetPixels(139, 4, m_PicTimes[num9].width, m_PicTimes[num9].height, m_PicTimes[num9].GetPixels(0, 0, m_PicTimes[num9].width, m_PicTimes[num9].height));
					m_TimePic[i].Apply();
					m_TimePlanes[i].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_TimePic[i]);
				}
				else
				{
					m_TimePlanes[i].GetComponent<MeshRenderer>().enabled = false;
				}
			}
		}
	}

	private void StartBtnBlink()
	{
		if (blinkTime >= 0.25f)
		{
			if (m_GoBtn.transform.localScale.x == 1f)
			{
				m_GoBtn.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
			}
			else
			{
				m_GoBtn.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			blinkTime = 0f;
		}
		else
		{
			blinkTime += Time.deltaTime;
		}
	}

	public void SelectItem0()
	{
		if (!m_Started)
		{
			Select(RandomTrack(), 0);
		}
	}

	public void SelectItem1()
	{
		if (!m_Started && Select(1101, 1))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1101));
		}
	}

	public void SelectItem2()
	{
		if (!m_Started && Select(1102, 2))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1102));
		}
	}

	public void SelectItem3()
	{
		if (!m_Started && Select(1103, 3))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1103));
		}
	}

	public void SelectItem4()
	{
		if (!m_Started && Select(1104, 4))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1104));
		}
	}

	public void SelectItem5()
	{
		if (!m_Started && Select(1105, 5))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1105));
		}
	}

	public void SelectItem6()
	{
		if (!m_Started && Select(1106, 6))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1106));
		}
	}

	public void SelectItem7()
	{
		if (!m_Started && Select(1107, 7))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1107));
		}
	}

	public void SelectItem8()
	{
		if (!m_Started && Select(1108, 8))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1108));
		}
	}

	public void SelectItem9()
	{
		if (!m_Started && Select(1109, 9))
		{
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(1109));
		}
	}

	private bool Select(int scene, int index)
	{
		HideAllMedal();
		bool flag = true;
		if (!CheckRelease(scene))
		{
			flag = false;
		}
		if (flag)
		{
			m_TrackID = scene;
		}
		m_PicPlanePre.active = true;
		if (null != m_PicPlanePre && null != m_PicPlaneBack && m_PicTexs != null && index < m_PicTexs.Length && m_CurTex != m_PicTexs[index])
		{
			m_TrackID = scene;
			m_PicPlaneBack.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_CurTex);
			m_PicPlanePre.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_PicTexs[index]);
			m_CurTex = m_PicTexs[index];
			Animation component = m_PicPlanePre.GetComponent<Animation>();
			if (null != component)
			{
				component.Play();
			}
		}
		return flag;
	}

	private int RandomTrack()
	{
		System.Random random = new System.Random((int)Time.time);
		int num = random.Next() % 450;
		for (int num2 = 9; num2 > 0; num2--)
		{
			if (num >= (num2 - 1) * 50 && TrackLockManager.Instance.ReleaseTrack(1100 + num2) == 0)
			{
				return 1100 + num2;
			}
		}
		return 1102;
	}

	public void StartGame()
	{
		if (fStartCoolTime > 0f || m_Started || !CheckRelease(m_TrackID))
		{
			return;
		}
		m_Started = true;
		GameObject gameObject = GameObject.Find("UIManager");
		if (null != gameObject)
		{
			gameObject.active = false;
		}
		if (m_TrackID == 1100)
		{
			m_TrackID = RandomTrack();
		}
		LocalUserInfo.Instance.TrackLevel = TrackLockManager.Instance.CheckDiffculty(m_TrackID);
		if (null != m_Loading)
		{
			m_Loading.SetActiveRecursively(state: true);
			m_Loading.GetComponent<LoadingController>().ShowLoadingIndicator();
			if (null != m_Return)
			{
				m_Return.SetActiveRecursively(state: false);
			}
		}
		SceneManager.Instance.LoadLevel(m_TrackID);
		SoundManager.Instance.StopSound(60002);
	}

	public void Return()
	{
		m_Started = true;
		UtilBinding.ShowWaitingView(bShow: true);
		SceneManager.Instance.RebackToLastLevel();
	}

	private bool CheckRelease(int sceneID)
	{
		m_TempTime = 0f;
		HidePurchasePanel();
		HideRecordReleasePanel();
		m_BuyID = -1;
		switch (TrackLockManager.Instance.ReleaseTrack(sceneID))
		{
		case 0:
			m_GoBtn.active = true;
			return true;
		case 1:
			m_GoBtn.active = false;
			m_BuyID = sceneID;
			ShowPurchasePanel();
			break;
		case 2:
		{
			string proceeding2 = string.Empty + TrackLockManager.Instance.TotalMedals() + "/" + TrackLockManager.Instance.NeedTotalMedals(sceneID);
			string key2 = "ID_" + sceneID + "_" + AndroidLocalizationBinder.GetLanguageIdentifier();
			string text2 = string.Empty;
			if (m_AlertBoxMap != null && m_AlertBoxMap.ContainsKey(key2))
			{
				text2 = m_AlertBoxMap[key2];
			}
			ShowRecordReleasePanel(text2, proceeding2);
			m_GoBtn.active = false;
			break;
		}
		case 3:
		{
			string proceeding = string.Empty + TrackLockManager.Instance.GetSpecificMedals(sceneID) + "/" + TrackLockManager.Instance.NeedSpecificMedals(sceneID);
			string key = "ID_" + sceneID + "_" + AndroidLocalizationBinder.GetLanguageIdentifier();
			string text = string.Empty;
			if (m_AlertBoxMap != null && m_AlertBoxMap.ContainsKey(key))
			{
				text = m_AlertBoxMap[key];
			}
			ShowRecordReleasePanel(text, proceeding);
			m_GoBtn.active = false;
			break;
		}
		}
		return false;
	}

	private void ShowPurchasePanel()
	{
		if (null != m_ReleaseBg)
		{
			m_ReleaseBg.active = true;
		}
		if (null != m_ReleaseText)
		{
			m_ReleaseText.active = true;
		}
		if (null != m_Purchase)
		{
			m_Purchase.active = true;
		}
		if (null != m_ReleaseTitle)
		{
			m_ReleaseTitle.active = true;
		}
	}

	private void HidePurchasePanel()
	{
		if (null != m_ReleaseBg)
		{
			m_ReleaseBg.active = false;
		}
		if (null != m_ReleaseText)
		{
			m_ReleaseText.active = false;
		}
		if (null != m_Purchase)
		{
			m_Purchase.active = false;
		}
		if (null != m_ReleaseTitle)
		{
			m_ReleaseTitle.active = false;
		}
	}

	private void ShowRecordReleasePanel(string text, string proceeding)
	{
		if (null != m_ReleaseBg)
		{
			m_ReleaseBg.active = true;
		}
		if (null != m_ReleaseText)
		{
			m_ReleaseText.active = true;
		}
		if (null != m_Proceeding)
		{
			m_Proceeding.active = true;
		}
		if (null != m_ProceedingText)
		{
			m_ProceedingText.active = true;
		}
		if (null != m_ReleaseTitle)
		{
			m_ReleaseTitle.active = true;
		}
		SpriteText component = m_ReleaseText.GetComponent<SpriteText>();
		component.Text = text;
		SpriteText component2 = m_ProceedingText.GetComponent<SpriteText>();
		component2.Text = proceeding;
	}

	private void HideRecordReleasePanel()
	{
		if (null != m_ReleaseBg)
		{
			m_ReleaseBg.active = false;
		}
		if (null != m_ReleaseText)
		{
			m_ReleaseText.active = false;
		}
		if (null != m_Proceeding)
		{
			m_Proceeding.active = false;
		}
		if (null != m_ProceedingText)
		{
			m_ProceedingText.active = false;
		}
		if (null != m_ReleaseTitle)
		{
			m_ReleaseTitle.active = false;
		}
	}

	public void Buy()
	{
	}

	private void CheckEnable()
	{
		for (int i = 1; i <= 9; i++)
		{
			CheckEnable(1100 + i);
		}
	}

	private void DeletePanelTab()
	{
		if (m_TimePlanes != null)
		{
			for (int i = 0; i < m_TimePlanes.Length; i++)
			{
				m_TimePlanes[i].transform.parent.Find("Pic").GetComponent<UIButton>().enabled = false;
				m_TimePlanes[i].transform.parent.Find("Title").GetComponent<UIButton>().enabled = false;
			}
		}
	}

	private void CheckEnable(int sceneID)
	{
		if (m_Shelters == null)
		{
			return;
		}
		for (int i = 0; i < m_Shelters.Length; i++)
		{
			if (null != m_Shelters[i] && m_Shelters[i].transform.parent.name.Equals("Item" + (sceneID - 1100 + 1)) && TrackLockManager.Instance.ReleaseTrack(sceneID) == 0)
			{
				UnityEngine.Object.Destroy(m_Shelters[i]);
			}
		}
	}

	private void DisablePanel()
	{
	}

	private void EnablePanel()
	{
	}

	private void ReleaseTrack()
	{
		EnablePanel();
		if (m_BuyID > 0)
		{
			string text = null;
			switch (m_BuyID)
			{
			case 1104:
				text = "com.taomee.molekart.sky";
				break;
			case 1106:
				text = "com.taomee.molekart.ice";
				break;
			case 1107:
				text = "com.taomee.molekart.garden";
				break;
			}
			if (text != null)
			{
				FlurryBinding.ReportBuyTrack(text);
			}
			TrackLockManager.Instance.SaveReleaseTrack(m_BuyID);
			m_TrackID = m_BuyID;
			HidePurchasePanel();
			m_GoBtn.active = true;
			CheckEnable(m_BuyID);
			ShowMedals(LocalUserConfig.Instance.MedalsCurrentMode(m_BuyID));
			m_BuyID = -1;
		}
	}

	private void PurchaseOver()
	{
		EnablePanel();
		if (m_BuyID <= 0)
		{
		}
	}
}
