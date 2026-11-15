using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectCharacterUIEvent : MonoBehaviour
{
	private enum CharacterSound
	{
		none = 0,
		momo = 30101,
		rk = 30201,
		lele = 30501,
		fred = 30301,
		mmt = 30405,
		kula = 30601,
		puti = 30701,
		yali = 30801
	}

	private const string characterName = "ShowCharacter";

	private const string carName = "ShowCar";

	private bool onEnter;

	private CharacterSound lastPlay;

	private bool changeState;

	private ushort lastID;

	private float newScaleNum;

	private ushort oldID;

	private float currentScale;

	private bool onChange;

	private float switchSpeed = 3f;

	private float startTime;

	private float currentTime;

	private bool bInitDefaultPos;

	private Vector3 beginPos;

	private Vector3 endPos;

	private Vector3 oriPos;

	private Dictionary<int, int> m_CarTypeToIndex = new Dictionary<int, int>();

	private GameObject m_ReleaseBg;

	private GameObject m_ReleaseText;

	private GameObject m_Purchase;

	private GameObject m_Proceeding;

	private GameObject m_ProceedingText;

	private GameObject m_ReleaseTitle;

	private string m_PurchasePrice = string.Empty;

	private GameObject m_GoBtn;

	private int m_BuyID = -1;

	private int m_Index = -1;

	private Dictionary<string, string> m_AlertBoxMap = new Dictionary<string, string>();

	private string m_TextFilePath = "Config/AlertBox/ReleaseCharacterText";

	private Dictionary<int, Vector3> rolePos = new Dictionary<int, Vector3>();

	private Dictionary<int, Quaternion> roleRotation = new Dictionary<int, Quaternion>();

	private Dictionary<int, Vector3> roleScale = new Dictionary<int, Vector3>();

	private Dictionary<int, Vector3> carPos = new Dictionary<int, Vector3>();

	private Dictionary<int, Quaternion> carRotation = new Dictionary<int, Quaternion>();

	private Dictionary<int, Vector3> carScale = new Dictionary<int, Vector3>();

	private bool m_Started;

	private ushort m_CurrentID;

	private ushort m_CurrentUserID;

	public string CharecterName => "ShowCharacter";

	public string CarName => "ShowCar";

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
				if (array != null && array.Length >= 2 && m_AlertBoxMap != null && !m_AlertBoxMap.ContainsKey(array[0]))
				{
					m_AlertBoxMap.Add(array[0], array[1]);
				}
			}
		}
	}

	private void CheckEnable()
	{
		CheckEnable(0, 2001);
		CheckEnable(1, 2002);
		CheckEnable(2, 2101);
		CheckEnable(3, 2003);
		CheckEnable(4, 2201);
		CheckEnable(5, 2004);
		CheckEnable(6, 2005);
		CheckEnable(7, 2006);
	}

	private void CheckEnable(int index, int type)
	{
		GameObject gameObject = GameObject.Find("Panel/GameObject" + index);
		if (CharacterLockManager.Instance.ReleaseCharacter(type) == 0 && null != gameObject)
		{
			Transform transform = gameObject.transform.FindChild("Shelter");
			if (null != transform)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	private bool Init()
	{
		string currentPlatform = AppMain.Instance.GetCurrentPlatform();
		GameObject gameObject = GameObject.Find("Panel/btn_start");
		float x = gameObject.transform.position.x;
		if (currentPlatform == "iphone")
		{
			beginPos = new Vector3(609.8239f, 16.57338f, -15.27289f);
			endPos = new Vector3(285.5035f, 16.57338f, -15.2789f);
			oriPos = new Vector3(2048f, 16.57338f, -15.27289f);
		}
		else
		{
			beginPos = new Vector3(601.8661f, 95.74983f, -20f);
			endPos = new Vector3(x, 78.74983f, -20f);
			oriPos = new Vector3(2048f, 95.74983f, -15.27289f);
		}
		rolePos.Add(3001, new Vector3(-99f, -327f, 752f));
		rolePos.Add(3002, new Vector3(75f, -262f, 643f));
		rolePos.Add(3101, new Vector3(-94.85254f, -303.9784f, 1018.871f));
		rolePos.Add(3003, new Vector3(-26f, -284f, 1051f));
		rolePos.Add(3201, new Vector3(52f, -167f, 1094f));
		rolePos.Add(3004, new Vector3(-98f, -285f, 649f));
		rolePos.Add(3005, new Vector3(-153.2813f, -354.9612f, 873.2233f));
		rolePos.Add(3006, new Vector3(78.76807f, -328.3894f, 1137.462f));
		roleRotation.Add(3001, Quaternion.Euler(351f, 180f, 0f));
		roleRotation.Add(3002, Quaternion.Euler(0f, 189f, 0f));
		roleRotation.Add(3101, Quaternion.Euler(0f, 174f, 0f));
		roleRotation.Add(3003, Quaternion.Euler(0f, 180f, 0f));
		roleRotation.Add(3201, Quaternion.Euler(358f, 191f, 1f));
		roleRotation.Add(3004, Quaternion.Euler(355f, 180f, 0f));
		roleRotation.Add(3005, Quaternion.Euler(355f, 172f, 0f));
		roleRotation.Add(3006, Quaternion.Euler(355f, 180f, 0f));
		roleScale.Add(3001, new Vector3(200f, 200f, 200f));
		roleScale.Add(3002, new Vector3(200f, 200f, 200f));
		roleScale.Add(3101, new Vector3(200f, 200f, 200f));
		roleScale.Add(3003, new Vector3(200f, 200f, 200f));
		roleScale.Add(3201, new Vector3(200f, 200f, 200f));
		roleScale.Add(3004, new Vector3(200f, 200f, 200f));
		roleScale.Add(3005, new Vector3(200f, 200f, 200f));
		roleScale.Add(3006, new Vector3(200f, 200f, 200f));
		carScale.Add(2001, new Vector3(200f, 200f, 200f));
		carScale.Add(2002, new Vector3(200f, 200f, 200f));
		carScale.Add(2101, new Vector3(200f, 200f, 200f));
		carScale.Add(2003, new Vector3(200f, 200f, 200f));
		carScale.Add(2201, new Vector3(200f, 200f, 200f));
		carScale.Add(2004, new Vector3(200f, 200f, 200f));
		carScale.Add(2005, new Vector3(200f, 200f, 200f));
		carScale.Add(2006, new Vector3(200f, 200f, 200f));
		carPos.Add(2001, new Vector3(69f, -380f, 898f));
		carPos.Add(2002, new Vector3(-46f, -274f, 794f));
		carPos.Add(2101, new Vector3(126.1692f, -373.2359f, 824.678f));
		carPos.Add(2003, new Vector3(27f, -331f, 844f));
		carPos.Add(2201, new Vector3(42f, -476f, 1068f));
		carPos.Add(2004, new Vector3(82f, -309f, 820f));
		carPos.Add(2005, new Vector3(40.56104f, -447.7967f, 1055.422f));
		carPos.Add(2006, new Vector3(-57.27661f, -309.8033f, 842.8025f));
		carRotation.Add(2001, Quaternion.Euler(353.3416f, 168.4497f, 1.718451f));
		carRotation.Add(2002, Quaternion.Euler(4f, 193f, 351f));
		carRotation.Add(2101, Quaternion.Euler(270f, 170f, 0f));
		carRotation.Add(2003, Quaternion.Euler(-90f, 211.0926f, 0f));
		carRotation.Add(2201, Quaternion.Euler(275f, 351f, 212f));
		carRotation.Add(2004, Quaternion.Euler(270f, 198f, 0f));
		carRotation.Add(2005, Quaternion.Euler(270f, 165.771f, 0f));
		carRotation.Add(2006, Quaternion.Euler(270f, 185.8392f, 0f));
		m_CarTypeToIndex.Add(2001, 0);
		m_CarTypeToIndex.Add(2002, 1);
		m_CarTypeToIndex.Add(2101, 2);
		m_CarTypeToIndex.Add(2003, 3);
		m_CarTypeToIndex.Add(2201, 4);
		m_CarTypeToIndex.Add(2004, 5);
		m_CarTypeToIndex.Add(2005, 6);
		m_CarTypeToIndex.Add(2006, 7);
		return false;
	}

	private void Uninit()
	{
		rolePos = null;
		roleRotation = null;
		roleScale = null;
		carPos = null;
		carRotation = null;
		carScale = null;
		Object.Destroy(this);
	}

	private void FindSaveObject()
	{
		m_ReleaseBg = GameObject.Find("Panel/Release/Release_Bg");
		if (null != m_ReleaseBg)
		{
			m_ReleaseBg.active = false;
		}
		m_ReleaseText = GameObject.Find("Panel/Release/Release_Bg/ReleaseText");
		if (null != m_ReleaseText)
		{
			m_ReleaseText.active = false;
		}
		m_Purchase = GameObject.Find("Panel/Release/Purchase");
		if (null != m_Purchase)
		{
			m_Purchase.active = false;
		}
		m_Proceeding = GameObject.Find("Panel/Release/Proceeding");
		if (null != m_Proceeding)
		{
			m_Proceeding.active = false;
		}
		m_ProceedingText = GameObject.Find("Panel/Release/Proceeding/ProceedingText");
		if (null != m_ProceedingText)
		{
			m_ProceedingText.active = false;
		}
		m_ReleaseTitle = GameObject.Find("Panel/Release/Release_Title");
		if (null != m_ReleaseTitle)
		{
			m_ReleaseTitle.active = false;
		}
		m_GoBtn = GameObject.Find("Panel/btn_start");
	}

	private void Start()
	{
		CheckEnable();
		FindSaveObject();
		if (SceneUIManager.Instance.m_Loading != null)
		{
			Object.Destroy(SceneUIManager.Instance.m_Loading);
			UtilBinding.ShowLoadingIndicator(bShow: false);
		}
		LoadAlertBoxText();
		Init();
		onEnter = true;
		changeState = false;
		lastPlay = CharacterSound.none;
		lastID = LocalUserInfo.Instance.UserInfo.CarId;
		if (!CheckCarIdIsLegal((CarModeType)lastID))
		{
			lastID = 2003;
		}
		switch ((CarModeType)lastID)
		{
		case CarModeType.CAR_RK:
		{
			GameObject gameObject = GameObject.Find("GameObject1");
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.Value = true;
			CharacterSelectedRk();
			break;
		}
		case CarModeType.CAR_BEAR:
		{
			GameObject gameObject = GameObject.Find("GameObject2");
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.Value = true;
			CharacterSelectedFred();
			break;
		}
		case CarModeType.CAR_MOLELE:
		{
			GameObject gameObject = GameObject.Find("GameObject3");
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.Value = true;
			CharacterSelectedLele();
			break;
		}
		case CarModeType.CAR_RADI:
		{
			GameObject gameObject = GameObject.Find("GameObject4");
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.Value = true;
			CharacterSelectedMmt();
			break;
		}
		case CarModeType.CAR_KULA:
		{
			GameObject gameObject = GameObject.Find("GameObject5");
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.Value = true;
			CharacterSelectedKula();
			break;
		}
		case CarModeType.CAR_PUTI:
		{
			GameObject gameObject = GameObject.Find("GameObject6");
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.Value = true;
			CharacterSelectedPuti();
			break;
		}
		case CarModeType.CAR_YALI:
		{
			GameObject gameObject = GameObject.Find("GameObject7");
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.Value = true;
			CharacterSelectedYali();
			break;
		}
		default:
		{
			GameObject gameObject = GameObject.Find("GameObject0");
			UIRadioBtn component = gameObject.GetComponent<UIRadioBtn>();
			component.Value = true;
			CharacterSelectedMomo();
			break;
		}
		}
		onEnter = false;
	}

	public void OnBack()
	{
		m_Started = true;
		UtilBinding.ShowWaitingView(bShow: true);
		SoundManager.Instance.StopSound((int)lastPlay);
		SceneManager.Instance.RebackToLastLevel();
		SaveSelectedCharacter(3003);
		Uninit();
	}

	public void OnRace()
	{
		m_Started = true;
		UtilBinding.ShowWaitingView(bShow: true);
		onEnter = true;
		SoundManager.Instance.StopSound((int)lastPlay);
		SceneManager.Instance.LoadLevel(1003);
		DeletePreviousSelected();
		TSingleton<CarManagerSelected>.Instance.ClearCachedSelectedCar();
		TSingleton<SelectedRoleFactory>.Instance.ClearCachedSelectedRole();
	}

	public void SaveSelectedCharacter(ushort id)
	{
		UserInfo userInfo = LocalUserInfo.Instance.UserInfo;
		DeletePreviousSelected();
		userInfo.CarId = (ushort)(id - 1000);
		userInfo.UserId = id;
		LocalUserInfo.Instance.UserInfo = userInfo;
	}

	private bool CheckPreviousSelected(RoleType role)
	{
		return (ushort)role == m_CurrentUserID;
	}

	public void CharacterSelectedMomo()
	{
		if (m_Started)
		{
			return;
		}
		bool release = CheckRelease(2001, 0);
		if (!CheckPreviousSelected(RoleType.EMomo) || onEnter)
		{
			if (lastPlay != CharacterSound.momo)
			{
				PlayCharacterSound(CharacterSound.momo);
			}
			ChangeCharacter(3001, release);
		}
	}

	public void CharacterSelectedRk()
	{
		if (m_Started)
		{
			return;
		}
		bool release = CheckRelease(2002, 1);
		if (!CheckPreviousSelected(RoleType.ERk) || onEnter)
		{
			if (lastPlay != CharacterSound.rk)
			{
				PlayCharacterSound(CharacterSound.rk);
			}
			ChangeCharacter(3002, release);
		}
	}

	public void CharacterSelectedLele()
	{
		if (m_Started)
		{
			return;
		}
		bool release = CheckRelease(2003, 3);
		if (!CheckPreviousSelected(RoleType.EMolele) || onEnter)
		{
			if (lastPlay != CharacterSound.lele)
			{
				PlayCharacterSound(CharacterSound.lele);
			}
			ChangeCharacter(3003, release);
		}
	}

	public void CharacterSelectedFred()
	{
		if (m_Started)
		{
			return;
		}
		bool release = CheckRelease(2101, 2);
		if (!CheckPreviousSelected(RoleType.EBear) || onEnter)
		{
			if (lastPlay != CharacterSound.fred)
			{
				PlayCharacterSound(CharacterSound.fred);
			}
			ChangeCharacter(3101, release);
		}
	}

	public void CharacterSelectedMmt()
	{
		if (m_Started)
		{
			return;
		}
		bool release = CheckRelease(2201, 4);
		if (!CheckPreviousSelected(RoleType.EMmt) || onEnter)
		{
			if (lastPlay != CharacterSound.mmt)
			{
				PlayCharacterSound(CharacterSound.mmt);
			}
			ChangeCharacter(3201, release);
		}
	}

	public void CharacterSelectedKula()
	{
		if (m_Started)
		{
			return;
		}
		bool release = CheckRelease(2004, 5);
		if (!CheckPreviousSelected(RoleType.EKuLa) || onEnter)
		{
			if (lastPlay != CharacterSound.kula)
			{
				PlayCharacterSound(CharacterSound.kula);
			}
			ChangeCharacter(3004, release);
		}
	}

	public void CharacterSelectedPuti()
	{
		if (m_Started)
		{
			return;
		}
		bool release = CheckRelease(2005, 6);
		if (!CheckPreviousSelected(RoleType.EPuti) || onEnter)
		{
			if (lastPlay != CharacterSound.puti)
			{
				PlayCharacterSound(CharacterSound.puti);
			}
			ChangeCharacter(3005, release);
		}
	}

	public void CharacterSelectedYali()
	{
		if (m_Started)
		{
			return;
		}
		bool release = CheckRelease(2006, 7);
		if (!CheckPreviousSelected(RoleType.EYali) || onEnter)
		{
			if (lastPlay != CharacterSound.yali)
			{
				PlayCharacterSound(CharacterSound.yali);
			}
			ChangeCharacter(3006, release);
		}
	}

	private bool CheckRelease(int id, int index)
	{
		HidePurchasePanel();
		HideRecordReleasePanel();
		m_BuyID = -1;
		switch (CharacterLockManager.Instance.ReleaseCharacter(id))
		{
		case 0:
			m_GoBtn.active = true;
			return true;
		case 1:
		{
			m_GoBtn.active = false;
			m_BuyID = id;
			m_Index = index;
			string key3 = "ID_" + id + "_" + AndroidLocalizationBinder.GetLanguageIdentifier();
			string text3 = string.Empty;
			if (m_AlertBoxMap != null && m_AlertBoxMap.ContainsKey(key3))
			{
				text3 = m_AlertBoxMap[key3];
			}
			ShowPurchasePanel(text3);
			break;
		}
		case 2:
		{
			string proceeding2 = string.Empty + CharacterLockManager.Instance.TotalMedals() + "/" + CharacterLockManager.Instance.NeedTotalMedals(id);
			string key2 = "ID_" + id + "_" + AndroidLocalizationBinder.GetLanguageIdentifier();
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
			string proceeding = string.Empty + CharacterLockManager.Instance.GetSpecificMedals(id) + "/" + CharacterLockManager.Instance.NeedSpecificMedals(id);
			m_GoBtn.active = false;
			string key = "ID_" + id + "_" + AndroidLocalizationBinder.GetLanguageIdentifier();
			string text = string.Empty;
			if (m_AlertBoxMap != null && m_AlertBoxMap.ContainsKey(key))
			{
				text = m_AlertBoxMap[key];
			}
			string languageIdentifier = AndroidLocalizationBinder.GetLanguageIdentifier();
			if (languageIdentifier.Equals("en") && id == 2004)
			{
				Debug.Log("Use Width Panel for Kula!!!");
				ShowWidthRecordReleasePanel(text, proceeding);
			}
			else
			{
				ShowRecordReleasePanel(text, proceeding);
			}
			break;
		}
		}
		return false;
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
		if (!m_Started)
		{
			Debug.Log(proceeding);
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
	}

	private void ShowWidthRecordReleasePanel(string text, string proceeding)
	{
		if (!m_Started)
		{
			Debug.Log(proceeding);
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

	private void ShowPurchasePanel(string text)
	{
		if (!m_Started)
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
			SpriteText component = m_ReleaseText.GetComponent<SpriteText>();
			component.Text = text;
		}
	}

	public void ChangeCharacter(ushort carId, bool release)
	{
		if (!m_Started)
		{
			startTime = Time.time;
			currentTime = startTime;
			UserInfo userInfo = LocalUserInfo.Instance.UserInfo;
			DeletePreviousSelected();
			if (release)
			{
				userInfo.CarId = (ushort)(carId - 1000);
				userInfo.UserId = carId;
				LocalUserInfo.Instance.UserInfo = userInfo;
			}
			m_CurrentID = (ushort)(carId - 1000);
			m_CurrentUserID = carId;
			ChangeCarModel(carId - 1000, carId);
		}
	}

	public void DeletePreviousSelected()
	{
		if (!onEnter)
		{
			GameObject gameObject = GameObject.Find(CharecterName);
			if ((bool)gameObject)
			{
				Object.DestroyImmediate(gameObject, allowDestroyingAssets: true);
			}
			GameObject gameObject2 = GameObject.Find(CarName);
			if ((bool)gameObject2)
			{
				Object.DestroyImmediate(gameObject2, allowDestroyingAssets: true);
			}
		}
	}

	private void PlayCharacterSound(CharacterSound sound)
	{
		if (lastPlay != CharacterSound.none)
		{
			SoundManager.Instance.StopSound((int)lastPlay);
		}
		SoundManager.Instance.PlaySound((int)sound);
		lastPlay = sound;
	}

	public void ChangeCarModel(int carID, int roleID)
	{
		if (!onEnter)
		{
			changeState = true;
			onChange = true;
		}
		SelectedRole player = TSingleton<SelectedRoleFactory>.Instance.CreateSelectedRole((RoleType)roleID, rolePos[roleID], roleRotation[roleID], roleScale[roleID], CharecterName);
		player.transform.parent = GameObject.Find("Camera").transform;
		player.transform.localPosition = rolePos[roleID];
		if (roleID == 3004)
		{
			PlayKulaFire(ref player);
		}
		if (roleID == 3006)
		{
			PlayYaliFire(ref player);
		}
		PlayCharacterAnimation(player);
		GameObject gameObject = TSingleton<CarManagerSelected>.Instance.CreateCar(0, CarName, carPos[carID], carRotation[carID], carScale[carID], (CarModeType)carID);
		if (carID != 2002)
		{
			gameObject.transform.parent = GameObject.Find("Camera").transform;
			gameObject.transform.localPosition = carPos[carID];
			return;
		}
		GameObject gameObject2 = GameObject.Find("CarObject");
		gameObject.transform.parent = gameObject2.transform;
		gameObject2.transform.localPosition = carPos[carID];
		gameObject2.transform.localScale = carScale[carID];
		gameObject2.transform.localRotation = carRotation[carID];
	}

	public void PlayCharacterAnimation(SelectedRole player)
	{
		player.m_RoleModel.AddComponent<PlayAnimationList>();
		PlayAnimationList component = player.m_RoleModel.GetComponent<PlayAnimationList>();
		component.ObjectToPlay = player.m_RoleModel;
		if ((bool)player.animation.GetClip("stan") && (bool)player.animation.GetClip("sele"))
		{
			component.PlayAnimation("sele", "stan");
		}
		else
		{
			component.PlayAnimation("sele01", "sele02");
		}
	}

	public void FixedUpdate()
	{
		if (!bInitDefaultPos)
		{
			GameObject gameObject = GameObject.Find("page_" + m_CarTypeToIndex[lastID]);
			if (null != gameObject)
			{
				gameObject.transform.position = endPos;
			}
			bInitDefaultPos = true;
		}
		if (changeState)
		{
			ChangeState(lastID, m_CurrentID);
		}
	}

	private void ChangeState(ushort oldID, ushort newID)
	{
		currentTime += Time.deltaTime;
		GameObject gameObject = GameObject.Find("page_" + m_CarTypeToIndex[lastID]);
		GameObject gameObject2 = GameObject.Find("page_" + m_CarTypeToIndex[newID]);
		if (onChange && gameObject.transform.position.x > endPos.x)
		{
			ResetAllToBeginPos(newID);
		}
		onChange = false;
		if (gameObject2.transform.position.x <= endPos.x)
		{
			changeState = false;
			onChange = false;
			lastID = newID;
		}
		else
		{
			gameObject.transform.position = Vector3.Lerp(endPos, new Vector3(696.8239f, 408.4627f, -10f), (currentTime - startTime) * switchSpeed);
			gameObject.transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), Vector3.zero, (currentTime - startTime) * switchSpeed);
			gameObject2.transform.position = Vector3.Lerp(beginPos, endPos, (currentTime - startTime) * switchSpeed);
			gameObject2.transform.localScale = Vector3.Lerp(new Vector3(0.7f, 0.7f, 0.7f), new Vector3(1f, 1f, 1f), (currentTime - startTime) * switchSpeed);
		}
	}

	private void ResetAllToBeginPos()
	{
		for (ushort num = 0; num < 6; num++)
		{
			GameObject gameObject = GameObject.Find("page_" + num);
			gameObject.transform.position = oriPos;
			gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
		}
	}

	private void ResetAllToBeginPos(int except)
	{
		for (ushort num = 0; num < 8; num++)
		{
			if (num != except)
			{
				GameObject gameObject = GameObject.Find("page_" + num);
				gameObject.transform.position = oriPos;
				gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			}
		}
	}

	private void ResetStatePosition(int oldID, int selectedID)
	{
		for (ushort num = 0; num < 8; num++)
		{
			GameObject gameObject = GameObject.Find("page_" + num);
			if (num == selectedID)
			{
				if (onEnter && (bool)gameObject)
				{
					gameObject.transform.position = endPos;
				}
			}
			else if ((bool)gameObject)
			{
				newScaleNum = 0.7f;
				gameObject.transform.localScale = new Vector3(newScaleNum, newScaleNum, newScaleNum);
				gameObject.transform.position = beginPos;
			}
		}
	}

	private void ResetStatePosition(int oldID)
	{
		GameObject gameObject = GameObject.Find("page_" + m_CarTypeToIndex[oldID]);
		newScaleNum = 0.7f;
		gameObject.transform.localScale = new Vector3(newScaleNum, newScaleNum, newScaleNum);
		gameObject.transform.position = beginPos;
	}

	private void PlayKulaFire(ref SelectedRole player)
	{
		GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.KULA_FIRE, isPause: true, isOneShot: false);
		Effect component = gameObject.GetComponent<Effect>();
		component.AttachTo(Utility.FindChildTranform(player.transform, "effect02"), new Vector3(-0.1036772f, -0.04123587f, 0.0795649f), new Vector3(0f, 0f, 0f));
		component.SetLifeTime(3f);
		component.doReset();
		GameObject gameObject2 = EffectManager.Instance.CreateEffect(eEffectType.KULA_FIRE_2, isPause: false, isOneShot: false);
		Effect component2 = gameObject2.GetComponent<Effect>();
		component2.transform.localScale = new Vector3(200f, 200f, 200f);
		component2.AttachTo(Utility.FindChildTranform(player.transform, "effect01"), new Vector3(0.02189802f, 0.2715608f, -0.02124882f), new Vector3(0f, 0f, 0f));
		component2.doReset();
	}

	private void PlayYaliFire(ref SelectedRole player)
	{
		GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.YALI_FIRE, isPause: true, isOneShot: false);
		Effect component = gameObject.GetComponent<Effect>();
		component.AttachTo(player.transform, Vector3.zero, Vector3.zero);
		component.doReset();
	}

	private bool CheckCarIdIsLegal(CarModeType id)
	{
		return id == CarModeType.CAR_MOMO || id == CarModeType.CAR_RK || id == CarModeType.CAR_BEAR || id == CarModeType.CAR_MOLELE || id == CarModeType.CAR_RADI || id == CarModeType.CAR_KULA;
	}

	private void EnablePanel()
	{
	}

	private void DisablePanel()
	{
	}

	private void PurchaseOver()
	{
		EnablePanel();
		if (m_BuyID <= 0)
		{
		}
	}

	public void Buy()
	{
	}
}
