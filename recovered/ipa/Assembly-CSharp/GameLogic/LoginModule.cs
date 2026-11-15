using FrameWork;
using UnityEngine;

namespace GameLogic;

public class LoginModule : CModule, IModule, ILoginModule
{
	private float m_fLoadingTime;

	private GameObject m_GameLook;

	private GameObject m_Logo;

	private GameObject m_LogoMask;

	private GUIText m_txtLoading;

	private bool m_bFirstPlay;

	public string m_strPlatformName;

	private bool m_bDeviceToggle;

	private bool m_bActive;

	private bool bStartLoad;

	public override void Init(IFrameWork iFrameHost)
	{
	}

	public override void Uninit()
	{
	}

	private UserInfo LoadLocalUserInfo()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SaveLocalUserInfo()
	{
	}

	public override void Update(float fCurTime)
	{
	}

	public void LoadGamingLook()
	{
	}

	private void LoadLevelCallback()
	{
	}

	public bool Login(uint userid, string password)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool Login(string email, string password)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool LoginSession(byte[] session)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Exit()
	{
	}
}
