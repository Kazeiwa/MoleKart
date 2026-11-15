using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIController : MonoBehaviour
{
	private TimeUI m_TimeUI;

	private LoadingUI m_LoadingUI;

	private PropUI m_PropUI;

	private LapUI m_LapUI;

	private CountDownUI m_CountDownUI;

	private SmallMapUI m_SmallMapUI;

	private ArrayList m_UIGroup;

	private FadeInAndOut m_FadeInAndOut;

	private ReverseUI m_ReverseUI;

	private RealtimeRankUIController m_RealtimeRankUI;

	private PauseUI m_PauseUI;

	private ControlPanelUI m_ControlPanel;

	private SceneIntroUI m_SceneIntroUI;

	private TurorialUI m_TutorialUI;

	public bool canShowSlider;

	private Dictionary<int, GameObject> m_RankNumber;

	private bool raceIsStarted;

	private RankUI m_RankUI;

	private bool onlineRankUIAdded;

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

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	public void SetRaceIsStarted(bool started)
	{
	}

	public bool RaceIsStarted()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ShowPauseUI()
	{
	}

	public void ShowPause()
	{
	}

	public void PauseBtnPressed()
	{
	}

	public void ResetHandleBreakStateToNormal()
	{
	}

	public void BackToGameBtnPressed()
	{
	}

	public void RestartBtnPressed()
	{
	}

	public void ControlBtnPressed()
	{
	}

	public void FinishBtnPressed()
	{
	}

	public void BackBtnPressed()
	{
	}

	public void RightArrowBtnPressed()
	{
	}

	public void LeftArrowBtnPressed()
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

	public void SetCanShowSlider(bool canShow)
	{
	}

	public void SetCanShowDrift(bool canShow)
	{
	}

	public void ResetDriftStateToNormal()
	{
	}

	public void SetCanShowControlBtn(bool canShow)
	{
	}

	public void UpdateShowControlBtn()
	{
	}

	public void ShowSlider(bool show)
	{
	}

	public void ShowHandbreak(bool show)
	{
	}

	private void Cancel()
	{
	}

	private void Confirm()
	{
	}

	public void ShowRankUI()
	{
	}

	public void ShowRankUI(bool isNetwork, bool isHost)
	{
	}

	public void RemoveAllSceneUI()
	{
	}

	public void RemoveAllSceneUIExceptCount(bool finish)
	{
	}

	public void RemoveAllSceneUIExceptCount()
	{
	}

	public void HideRankUI()
	{
	}

	public void InitSceneUI()
	{
	}

	public void InitOnlineGameSceneUI()
	{
	}

	public void RotateCar(Car car)
	{
	}

	public void ShowLap(int lap)
	{
	}

	public void HideLap()
	{
	}

	public void ShowSmallMap()
	{
	}

	public void HideSmallMap()
	{
	}

	public void ShowTime(float fTime)
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

	public void HideReverseUI()
	{
	}

	public void ShowReverseUI()
	{
	}

	private void Update()
	{
	}

	public void HalfUnInit()
	{
	}

	public void BANANA()
	{
	}

	public void BANANA_THREE()
	{
	}

	public void BOMB()
	{
	}

	public void CHEAT_BOX()
	{
	}

	public void CUTTLE_FISH()
	{
	}

	public void LIGHTNING()
	{
	}

	public void LUCKY_STAR()
	{
	}

	public void MISSILE()
	{
	}

	public void MUSHROOM_BIG()
	{
	}

	public void MUSHROOM_GOLDERN()
	{
	}

	public void MUSHROOM_ONE()
	{
	}

	public void MUSHROOM_THREE()
	{
	}

	public void QUAKE()
	{
	}

	public void THUNDER_CLOUD()
	{
	}

	public void TORTOISE_BLUE()
	{
	}

	public void TORTOISE_GREEN()
	{
	}

	public void TORTOISE_GREEN_THREE()
	{
	}

	public void TORTOISE_RED()
	{
	}

	public void TORTOISE_RED_THREE()
	{
	}
}
