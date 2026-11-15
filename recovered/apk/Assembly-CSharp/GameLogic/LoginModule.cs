using System.Linq;
using FrameWork;
using UnityEngine;

namespace GameLogic;

public class LoginModule : CModule, IModule, ILoginModule
{
	private float m_fLoadingTime;

	private GameObject m_GameLook;

	private GameObject m_LogoBackground;

	private GameObject m_LogoMask;

	private GameObject m_Logo;

	private GameObject PressButton;

	private GUIText m_txtLoading;

	private bool m_bFirstPlay = true;

	public string m_strPlatformName = string.Empty;

	private bool m_bDeviceToggle = true;

	private bool m_bActive = true;

	private bool bStartLoad;

	private bool bLogoHide;

	private bool bBKShow;

	public LoginModule()
		: base(enmModuleID.MI_LOGIN)
	{
	}

	public override void Init(IFrameWork iFrameHost)
	{
		base.Init(iFrameHost);
		m_strPlatformName = AppMain.Instance.GetCurrentPlatform();
		if (m_strPlatformName == "ipad")
		{
			m_bDeviceToggle = true;
		}
		else if (m_strPlatformName == "iphone")
		{
			m_bDeviceToggle = false;
		}
		string currentSystemLanguage = AppMain.Instance.GetCurrentSystemLanguage();
		string empty = string.Empty;
		empty = ((!m_bDeviceToggle) ? "GameLookSmall" : "GameLook");
		m_GameLook = GameObject.Find(currentSystemLanguage switch
		{
			"en" => empty + "-en", 
			"zh-Hans" => empty + "-zh-Hans", 
			"zh-Hant" => empty + "-zh-Hant", 
			_ => empty + "-zh-Hans", 
		});
		GameObject.Find("GameLook-zh-Hans").SetActiveRecursively(state: false);
		m_Logo = GameObject.Find("Logo");
		if (m_bDeviceToggle)
		{
			m_LogoBackground = GameObject.Find("LogoBig");
			PressButton = GameObject.Find("Press-zh-Hans");
			PressButton.active = false;
		}
		else
		{
			m_LogoBackground = GameObject.Find("LogoSmall");
			GameObject gameObject = GameObject.Find("LogoBig");
			if (null != gameObject)
			{
				gameObject.active = false;
			}
		}
		m_LogoMask = GameObject.Find("LogoMask");
		if (!m_bDeviceToggle)
		{
			GameObject gameObject2 = GameObject.Find("Main Camera");
			if (gameObject2 != null)
			{
				Camera component = gameObject2.GetComponent<Camera>();
				component.aspect = 1.5f;
				component.orthographicSize = 320f;
				component.rect = new Rect(0f, 0f, 480f, 320f);
				component.pixelRect = new Rect(0f, 0f, 960f, 640f);
				component.backgroundColor = new Color(0f, 1f, 0f, 1f);
			}
		}
		LocalUserInfo.Instance.UserInfo = LoadLocalUserInfo();
		if (SystemConfig.Instance.Standalone)
		{
			m_bActive = false;
			SceneManager.Instance.Init();
			SceneManager.Instance.LoadLevel((int)SystemConfig.Instance.EnterLevelID);
			Debug.LogWarning("Application run in standalone mode");
		}
	}

	public override void Uninit()
	{
		SaveLocalUserInfo();
		DebugEx.Log("LoginModule:Uninit()");
		base.Uninit();
	}

	private UserInfo LoadLocalUserInfo()
	{
		UserInfo result = new UserInfo
		{
			roleType = 0,
			Level = 0u,
			UserName = "Mole",
			UserId = 0u
		};
		try
		{
			m_bFirstPlay = !PlayerPrefs.HasKey("bFirstPlayTheGame");
			if (!m_bFirstPlay)
			{
				result.UserId = (uint)PlayerPrefs.GetInt("UserId");
				result.UserName = PlayerPrefs.GetString("UserName");
				result.roleType = (byte)PlayerPrefs.GetInt("Species");
				result.CarId = (ushort)PlayerPrefs.GetInt("CarId");
				LocalUserInfo.Instance.ReadGameTime();
				LocalUserInfo.Instance.ReadRank();
			}
		}
		catch (PlayerPrefsException ex)
		{
			Debug.LogError("PlayerPrefsException =" + ex);
		}
		return result;
	}

	private void SaveLocalUserInfo()
	{
		try
		{
			PlayerPrefs.SetInt("bFirstPlayTheGame", 0);
			UserInfo userInfo = LocalUserInfo.Instance.UserInfo;
			PlayerPrefs.SetInt("UserId", (int)userInfo.UserId);
			PlayerPrefs.SetString("UserName", userInfo.UserName);
			PlayerPrefs.SetInt("Species", userInfo.roleType);
			PlayerPrefs.SetInt("CarId", userInfo.CarId);
			LocalUserInfo.Instance.SaveGameTime();
			LocalUserInfo.Instance.SaveRank();
			PlayerPrefs.Save();
		}
		catch (PlayerPrefsException ex)
		{
			Debug.LogError("PlayerPrefsException =" + ex);
		}
	}

	public override void Update(float fCurTime)
	{
		if (!m_bActive)
		{
			return;
		}
		m_fLoadingTime += Time.deltaTime;
		if (m_fLoadingTime > 4f && !bLogoHide)
		{
			m_LogoBackground.active = false;
			if (m_LogoMask != null)
			{
				m_LogoMask.active = false;
			}
			m_Logo.active = false;
			bLogoHide = true;
		}
		if (m_fLoadingTime > 4f && !bBKShow)
		{
			SoundManager.Instance.PlaySoundEx(60002);
			m_GameLook.SetActiveRecursively(state: true);
			PressButton.active = true;
			bBKShow = true;
		}
		if (m_fLoadingTime > 4f && bBKShow && (InputManager.Instance.GetTouchPoints().Count() > 0 || Input.GetMouseButtonDown(0)))
		{
			SoundManager.Instance.PlaySound(60003);
			LoadGamingLook();
		}
	}

	public void LoadGamingLook()
	{
		if (!bStartLoad)
		{
			SceneManager.Instance.GoToNextLevel();
			bStartLoad = true;
		}
		m_bActive = false;
	}

	private void LoadLevelCallback()
	{
		m_bActive = false;
	}

	public bool Login(uint userid, string password)
	{
		return false;
	}

	public bool Login(string email, string password)
	{
		return false;
	}

	public bool LoginSession(byte[] session)
	{
		return false;
	}

	public void Exit()
	{
	}
}
