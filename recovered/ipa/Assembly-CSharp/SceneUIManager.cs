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

	public Texture2D weiboButtonNormal;

	public Texture2D weiboButtonSelected;

	public Texture2D facebookButtonNormal;

	public Texture2D facebookButtonSelected;

	public Texture2D rankImgTex;

	public Texture2D timePointTex;

	public Texture2D timePoint2Tex;

	public Texture2D unFinishedTimeTex;

	public Texture2D unfinishedTex;

	public Texture2D breakRecordTex;

	public Texture2D keepHurryTex;

	public Texture2D[] rankBarTex;

	public Texture2D[] rankNumTex;

	public Texture2D[] rankTimeNumTex;

	public string[] m_CurStrings;

	public RankUIData rankUIData;

	private TurorialUI turorial;

	public static SceneUIManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public PauseUI PauseUIController
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RealtimeRankUIController RealtimeUI
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Init(string language, string plat)
	{
	}

	public void UnInit()
	{
	}

	private void SetRankNumTex(string plat)
	{
	}

	private void SetTimeNumTex(string plat)
	{
	}

	private void SetBarTex(string plat)
	{
	}

	public void RotateCar(Car car)
	{
	}

	public void InitSceneUI()
	{
	}

	public void InitOnlineGameSceneUI()
	{
	}

	public void ShowTime(float fTime)
	{
	}

	public void ShowLap(int lap)
	{
	}

	public void ShowSmallMap()
	{
	}

	public void DisableLoadingUI()
	{
	}

	public void DisableCountDown()
	{
	}

	public void CountDown(int index)
	{
	}

	public void ShowProp(ePropType type)
	{
	}

	public void RollPropUI(ePropType type)
	{
	}

	public void HideProp()
	{
	}

	public void ShowProp()
	{
	}

	public void HideSmallMap()
	{
	}

	public void HideLap()
	{
	}

	public void HideTime()
	{
	}

	public void FadeIn(float fTime)
	{
	}

	public void AutoFadeInAndOut(float fTime)
	{
	}

	public void HideReverseUI()
	{
	}

	public void ShowReverseUI()
	{
	}

	public void SetRaceStarted(bool started)
	{
	}

	public bool RaceIsStarted()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ShowPauseUI()
	{
	}

	public void HidePauseUI()
	{
	}

	public void ShowRealtimeRank()
	{
	}

	public void HideRealtimeRank()
	{
	}

	public void HideIntroUI()
	{
	}

	public void ShowIntroUI()
	{
	}

	public void ShowPropTutorialUI(bool show)
	{
	}

	public void DestroyPropTutorialUI()
	{
	}

	public void SetCanShowSlider(bool canShow)
	{
	}

	public void SetCanShowDrift(bool canShow)
	{
	}

	public void ResetDriftStateToNormal()
	{
	}

	public void ShowSlider(bool show)
	{
	}

	public void SetCanShowControlBtn(bool canShow)
	{
	}

	public void UpdateShowControlBtn()
	{
	}

	public void ShowRankUI()
	{
	}

	public void ShowRankUI(bool isNetwork, bool isHost)
	{
	}

	public void HideRankUI()
	{
	}

	public void ShowHandbreak(bool show)
	{
	}

	public void ResetHandleBreakStateToNormal()
	{
	}

	public void RemoveAllSceneUIExceptCount()
	{
	}

	public void RemoveAllSceneUI()
	{
	}
}
