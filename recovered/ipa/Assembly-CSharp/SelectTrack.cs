using System.Collections.Generic;
using UnityEngine;
using iap;

public class SelectTrack : MonoBehaviour
{
	private GameObject m_PicPlanePre;

	private GameObject m_PicPlaneBack;

	private GameObject m_GoBtn;

	private float blinkTime;

	public Texture[] m_PicTexs;

	public Texture2D[] m_PicTimes;

	private Texture m_CurTex;

	private RacingType iRacingType;

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

	private GameObject m_Purchase;

	private GameObject m_Proceeding;

	private GameObject m_ReleaseTitle;

	public GameObject[] m_Shelters;

	private string[] m_CueString;

	private int m_BuyID;

	private string m_PurchasePrice;

	private bool m_Started;

	private float fStartCoolTime;

	private Dictionary<string, string> m_AlertBoxMap;

	private string m_TextFilePath;

	private float m_TempTime;

	private float m_DeleteTime;

	private void LoadAlertBoxText()
	{
	}

	private void CheckNewRelease()
	{
	}

	private void CheckNewRelease(int sceneID, ref bool aButton)
	{
	}

	private void Start()
	{
	}

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void HideLoadingUI()
	{
	}

	private void SetDefaultSelect()
	{
	}

	private void FindSaveObject()
	{
	}

	private void HideAllMedal()
	{
	}

	private void ShowMedals(int num)
	{
	}

	private void Update()
	{
	}

	private void SetTrackRecordTime()
	{
	}

	private void StartBtnBlink()
	{
	}

	public void SelectRandom()
	{
	}

	public void SelectPort()
	{
	}

	public void SelectFarm()
	{
	}

	public void SelectMaple()
	{
	}

	public void SelectSky()
	{
	}

	public void SelectIce()
	{
	}

	public void SelectVolcano()
	{
	}

	public void SelectManor()
	{
	}

	public void SelectFactory()
	{
	}

	private bool CheckTrackIdIsLegal(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private int DefaultPic(int scene)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool Select(int sceneId, int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private int RandomTrack()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void StartGame()
	{
	}

	public void Return()
	{
	}

	private bool CheckRelease(int sceneID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void ShowPurchasePanel()
	{
	}

	private void HidePurchasePanel()
	{
	}

	private void ShowRecordReleasePanel(string text, string proceeding)
	{
	}

	private void HideRecordReleasePanel()
	{
	}

	public void Buy()
	{
	}

	private void CheckEnable()
	{
	}

	private void DeletePanelTab()
	{
	}

	private void CheckEnable(int sceneID)
	{
	}

	public void PurchaseCallback(Request.Status current_status)
	{
	}

	private void ReleaseTrack()
	{
	}

	private void PurchaseOver()
	{
	}

	public void ShareWeibo()
	{
	}

	public void ShareFacebook()
	{
	}

	private static string BuyIdToText(int buyId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
