using System.Collections;
using System.Collections.Generic;
using RaceProtocol;
using UnityEngine;

public class OnlineRankUIBase : MonoBehaviour
{
	protected enum RANKUI_STATE
	{
		RANKUI_STATE_BEGIN,
		RANKUI_STATE_COUNT_TEN_SEC,
		RANKUI_STATE_WAIT_TEN_SEC,
		RANKUI_STATE_COMPLETE_SCALE,
		RANKUI_STATE_COMPLETE_SHOW,
		RANKUI_STATE_SHOWRANK,
		RANKUI_STATE_FINISH
	}

	protected RANKUI_STATE state = RANKUI_STATE.RANKUI_STATE_COUNT_TEN_SEC;

	protected static string xml_path = "Config/xmlConfig/RankUIConfig";

	protected int playerNum;

	protected int playerRank = -1;

	protected Texture2D finishTex;

	protected Rect finishRect;

	protected Vector2 finishScale = new Vector2(6f, 6f);

	protected float finishDeltaTime1 = 0.5f;

	protected float finishDeltaTime2 = 4f;

	protected Texture2D unFinishTex;

	protected Texture2D rankImgTex;

	protected Rect rankImgRect;

	protected Texture2D timePointTex;

	protected Texture2D timePoint2Tex;

	protected Texture2D nullTimeTex;

	protected Texture2D[] rankBarTex = new Texture2D[2];

	protected Texture2D[] rankNumTex = new Texture2D[6];

	protected Texture2D[] rankTimeNumTex = new Texture2D[10];

	protected Dictionary<string, Texture2D> rankIconTex = new Dictionary<string, Texture2D>();

	protected Rect[] rankBarRect = new Rect[6];

	protected Rect[] rankNumRect = new Rect[6];

	protected Rect[] rankIconRect = new Rect[6];

	protected Rect[] rankTimeNumRect = new Rect[8];

	protected bool touchAgainButton;

	protected bool touchBackButton;

	protected bool ShowUI;

	protected string language;

	protected string plat;

	protected bool[] nameLabelIsSet = new bool[6];

	protected GameObject finish;

	protected GameObject unFinish;

	protected GameObject breakRec;

	protected GameObject medal;

	protected GameObject words;

	protected GameObject easyWord;

	protected GameObject normalWord;

	protected GameObject masterWord;

	protected GameObject background;

	protected GameObject congra;

	protected GameObject aero;

	protected float showRankTime = 5f;

	protected float waitTime = 10f;

	protected Dictionary<int, GameObject> countNumbers = new Dictionary<int, GameObject>();

	protected bool selfIsFinished;

	protected bool isBreakRecord;

	private bool finishWordSet;

	private bool m_isFinished;

	protected ArrayList rankList = new ArrayList();

	private bool effectPlayed;

	private DelayDelegateTimer timer;

	private int lastPlayGameLevel = -1;

	private bool finishSoundPlayed;

	protected float delayTime = 1f;

	protected float m_PreSendTime;

	private bool wordSet;

	private Dictionary<GameObject, float> posAniTimeDic = new Dictionary<GameObject, float>();

	private Dictionary<GameObject, float> scaleAniTimeDic = new Dictionary<GameObject, float>();

	public int PlayerNum
	{
		get
		{
			return playerNum;
		}
		set
		{
			playerNum = value;
		}
	}

	public void InitRankRoot(RankUI root)
	{
		finish = root.finish;
		unFinish = root.unFinish;
		breakRec = root.breakRec;
		medal = root.medal;
		words = root.words;
		easyWord = root.easyWord;
		normalWord = root.normalWord;
		masterWord = root.masterWord;
		background = root.background;
		congra = root.congra;
		aero = root.aero;
	}

	protected void Awake()
	{
		Debug.Log("Register Handler on RankUI awake");
		language = AppMain.Instance.GetCurrentSystemLanguage();
		plat = AppMain.Instance.GetCurrentPlatform();
		if (plat == string.Empty)
		{
			plat = "ipad";
		}
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (carByIndex != null)
			{
				PlayerData data = new PlayerData();
				if (RaceSceneData.GetPlayerData((ulong)carByIndex.ID, ref data) && carByIndex.isHero && data.Ranking > 0)
				{
					playerRank = data.Ranking;
					break;
				}
			}
		}
		for (int j = 0; j < 10; j++)
		{
			countNumbers.Add(j, base.transform.FindChild(j.ToString()).gameObject);
		}
		rankImgTex = SceneUIManager.Instance.rankImgTex;
		timePointTex = SceneUIManager.Instance.timePointTex;
		timePoint2Tex = SceneUIManager.Instance.timePoint2Tex;
		unFinishTex = SceneUIManager.Instance.unFinishedTimeTex;
		if (plat == "iphone")
		{
			if (language == "zh-Hans")
			{
				rankImgRect = new Rect(380f, 0f, rankImgTex.width, rankImgTex.height);
			}
			else if (language == "en")
			{
				rankImgRect = new Rect(313f, 0f, rankImgTex.width, rankImgTex.height);
			}
			else
			{
				rankImgRect = new Rect(380f, 0f, rankImgTex.width, rankImgTex.height);
			}
		}
		else if (language == "zh-Hans")
		{
			rankImgRect = new Rect(449f, 10f, rankImgTex.width, rankImgTex.height);
		}
		else if (language == "en")
		{
			rankImgRect = new Rect(362f, 10f, rankImgTex.width, rankImgTex.height);
		}
		else
		{
			rankImgRect = new Rect(449f, 10f, rankImgTex.width, rankImgTex.height);
		}
		Init();
	}

	public virtual void Init()
	{
		SetRankNumTex();
		SetTimeNumTex();
		SetBarTex();
		ParseRankUIConfig();
		SetTimeRect();
		ShowUI = true;
	}

	private void SetGameObject()
	{
		if (!RaceSceneData.IsFinish(TSingleton<CarManager>.Instance.GetHeroCar().ID))
		{
			finish.active = false;
			breakRec.active = false;
			unFinish.active = true;
			finish = unFinish;
		}
		else if (isBreakRecord)
		{
			finish.active = false;
			unFinish.active = false;
			breakRec.active = true;
			finish = breakRec;
		}
		else
		{
			finish.active = true;
			unFinish.active = false;
			breakRec.active = false;
		}
	}

	private void SetRankNumTex()
	{
		for (int i = 1; i <= 6; i++)
		{
			rankNumTex[i - 1] = SceneUIManager.Instance.rankNumTex[i - 1];
		}
	}

	private void SetTimeNumTex()
	{
		for (int i = 0; i < 10; i++)
		{
			rankTimeNumTex[i] = SceneUIManager.Instance.rankTimeNumTex[i];
		}
	}

	private void SetBarTex()
	{
		rankBarTex[0] = SceneUIManager.Instance.rankBarTex[0];
		rankBarTex[1] = SceneUIManager.Instance.rankBarTex[1];
	}

	protected void InitUIConfiguration()
	{
		SceneUIManager.Instance.RemoveAllSceneUI();
		ScoreReportManager.Instance.ReportScoreToGameCenter();
		LockMovement();
		DriveManager.Instance.Reset();
		DealBreakRecord();
		SoundManager.Instance.StopAllSound();
	}

	protected void Reset()
	{
		finishDeltaTime1 = 0.5f;
		finishDeltaTime2 = 5f;
	}

	protected void SetTimeRect()
	{
		if (plat == "iphone")
		{
			for (int i = 0; i < 8; i++)
			{
				ref Rect reference = ref rankTimeNumRect[i];
				reference = new Rect(620 + i * 25, 110f, 25f, 30f);
			}
		}
		else
		{
			for (int j = 0; j < 8; j++)
			{
				ref Rect reference2 = ref rankTimeNumRect[j];
				reference2 = new Rect(700 + j * 25, 135f, 25f, 30f);
			}
		}
	}

	protected void SetAllRect()
	{
		float num = (Screen.width - rankBarTex[0].width) / 2;
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			ref Rect reference = ref rankBarRect[i];
			reference = new Rect(num, 111 + i * 87, rankBarTex[0].width, rankBarTex[0].height);
			ref Rect reference2 = ref rankNumRect[i];
			reference2 = new Rect(num + 22f, 80 + i * 90, rankNumTex[i].width, rankNumTex[i].height);
			ref Rect reference3 = ref rankIconRect[i];
			reference3 = new Rect(num + 172f, 100 + i * 87, 90f, 90f);
		}
	}

	protected void DealBreakRecord()
	{
		if (SelfIfFinished())
		{
			int iD = TSingleton<CarManager>.Instance.GetHeroCar().ID;
			float num = LocalUserConfig.Instance.Record(SceneManager.Instance.CurGameSceneId);
			if (RaceModeFactory.CurrentRaceMode.TotalTime() >= RaceSceneData.SpendTime(iD))
			{
				selfIsFinished = true;
			}
			if ((RaceSceneData.SpendTime(iD) < num / 100f || num == 0f) && selfIsFinished)
			{
				isBreakRecord = true;
				PlayBreakRecordEffect();
			}
			PlayerData data = new PlayerData();
			if (RaceSceneData.GetPlayerData((ulong)iD, ref data) && data.SpendTime < RaceModeFactory.CurrentRaceMode.TotalTime())
			{
				LocalUserConfig.Instance.SaveRecord(SceneManager.Instance.CurGameSceneId, LocalUserInfo.Instance.TrackLevel, data.SpendTime);
			}
		}
	}

	protected void SetFinishWord()
	{
		if (finishWordSet)
		{
			return;
		}
		if (SelfIfFinished())
		{
			if (isBreakRecord)
			{
				breakRec.active = true;
				finish.active = false;
				unFinish.active = false;
				finish = breakRec;
				Debug.LogWarning("break");
			}
			else
			{
				breakRec.active = false;
				finish.active = true;
				unFinish.active = false;
				Debug.LogWarning("finish");
			}
		}
		else
		{
			breakRec.active = false;
			finish.active = false;
			unFinish.active = true;
			finish = unFinish;
			Debug.LogWarning("unfinish");
		}
		finishWordSet = true;
	}

	protected bool ParseRankUIConfig()
	{
		rankIconTex = SceneUIManager.Instance.rankIconTex;
		return true;
	}

	protected void SetSelfIfFinished(bool isFinish)
	{
		m_isFinished = isFinish;
	}

	protected bool SelfIsFinished()
	{
		return RaceSceneData.IsFinish(TSingleton<CarManager>.Instance.GetHeroCar().ID);
	}

	protected void DrawRankBar()
	{
		for (int i = 0; i < playerNum; i++)
		{
			if (i != playerRank)
			{
				GUI.DrawTexture(rankBarRect[i], rankBarTex[0]);
			}
			else
			{
				GUI.DrawTexture(rankBarRect[i], rankBarTex[1]);
			}
		}
	}

	protected bool ShowingFinishUI()
	{
		Matrix4x4 matrix = GUI.matrix;
		if (finishDeltaTime1 > 0f)
		{
			if (playerRank <= 3 && playerRank >= 1)
			{
				SoundManager.Instance.PlaySound(60014);
			}
			else if (playerRank > 3 && playerRank <= 6)
			{
				SoundManager.Instance.PlaySound(60015);
			}
			Vector2 pivotPoint = new Vector2(finishRect.xMin + finishRect.width / 2f, finishRect.yMin + finishRect.height / 2f);
			if (finishScale.x > 1f && finishScale.y > 1f)
			{
				Vector2 vector = new Vector2(finishScale.x - Time.deltaTime, finishScale.y - Time.deltaTime);
				finishScale = vector;
			}
			else
			{
				finishScale = new Vector2(1f, 1f);
			}
			GUIUtility.ScaleAroundPivot(finishScale, pivotPoint);
			GUI.DrawTexture(finishRect, finishTex);
			finishDeltaTime1 -= Time.deltaTime;
			GUI.matrix = matrix;
			return true;
		}
		if (finishDeltaTime2 > 0f)
		{
			finishDeltaTime2 -= Time.deltaTime;
			GUI.DrawTexture(finishRect, finishTex);
			GUI.matrix = matrix;
			return true;
		}
		return false;
	}

	protected void DrawNetworkRankDetail()
	{
		int num = 0;
		num = ((!(plat == "iphone")) ? 87 : 68);
		GUI.DrawTexture(rankImgRect, rankImgTex);
		foreach (MsgRank rank in rankList)
		{
			int num2 = rank.Rank - 1;
			if (num2 == RaceSceneData.Rank(TSingleton<CarManager>.Instance.GetHeroCar().ID) - 1)
			{
				GUI.DrawTexture(rankBarRect[num2], rankBarTex[1]);
			}
			else
			{
				GUI.DrawTexture(rankBarRect[num2], rankBarTex[0]);
			}
			GUI.DrawTexture(rankNumRect[num2], rankNumTex[num2]);
			GUI.DrawTexture(rankIconRect[num2], rankIconTex[rank.RoleType.ToString()]);
			if (rank.IsFinish == 0)
			{
				if (plat == "ipad")
				{
					GUI.DrawTexture(new Rect(710f, 130 + num2 * 85, unFinishTex.width, unFinishTex.height), unFinishTex);
				}
				else if (plat == "iphone")
				{
					GUI.DrawTexture(new Rect(622f, 100 + num2 * 70, unFinishTex.width, unFinishTex.height), unFinishTex);
				}
				else
				{
					GUI.DrawTexture(new Rect(710f, 130 + num2 * 85, unFinishTex.width, unFinishTex.height), unFinishTex);
				}
				continue;
			}
			float time = rank.Time;
			int num3 = (int)(time / 60f);
			int num4 = (int)(time - (float)(num3 * 60));
			int num5 = (int)(time * 100f - (float)(num4 * 100) - (float)(num3 * 60 * 100));
			if (num3 / 10 > 0)
			{
				GUI.DrawTexture(new Rect(rankTimeNumRect[0].xMin, rankTimeNumRect[0].yMin + (float)(num2 * num), rankTimeNumRect[0].width, rankTimeNumRect[0].height), rankTimeNumTex[num3 / 10]);
			}
			GUI.DrawTexture(new Rect(rankTimeNumRect[1].xMin, rankTimeNumRect[1].yMin + (float)(num2 * num), rankTimeNumRect[1].width, rankTimeNumRect[1].height), rankTimeNumTex[num3 - num3 / 10]);
			GUI.DrawTexture(new Rect(rankTimeNumRect[2].xMin, rankTimeNumRect[2].yMin + (float)(num2 * num), timePointTex.width, timePointTex.height), timePointTex);
			int num6 = num4 / 10;
			int num7 = num4 - num6 * 10;
			if (num6 >= 6)
			{
				num6 = 0;
			}
			if (num7 > 9)
			{
				num7 = 0;
			}
			GUI.DrawTexture(new Rect(rankTimeNumRect[3].xMin, rankTimeNumRect[3].yMin + (float)(num2 * num), rankTimeNumRect[3].width, rankTimeNumRect[3].height), rankTimeNumTex[num6]);
			GUI.DrawTexture(new Rect(rankTimeNumRect[4].xMin, rankTimeNumRect[4].yMin + (float)(num2 * num), rankTimeNumRect[4].width, rankTimeNumRect[4].height), rankTimeNumTex[num7]);
			GUI.DrawTexture(new Rect(rankTimeNumRect[5].xMin, rankTimeNumRect[5].yMin + (float)(num2 * num), timePoint2Tex.width, timePoint2Tex.height), timePoint2Tex);
			int num8 = num5 / 10;
			int num9 = num5 - num8 * 10;
			if (num8 > 9)
			{
				num8 = 0;
			}
			if (num9 > 9)
			{
				num9 = 0;
			}
			GUI.DrawTexture(new Rect(rankTimeNumRect[6].xMin, rankTimeNumRect[6].yMin + (float)(num2 * num), rankTimeNumRect[6].width, rankTimeNumRect[6].height), rankTimeNumTex[num8]);
			GUI.DrawTexture(new Rect(rankTimeNumRect[7].xMin, rankTimeNumRect[7].yMin + (float)(num2 * num), rankTimeNumRect[7].width, rankTimeNumRect[7].height), rankTimeNumTex[num9]);
		}
	}

	protected void PlayBreakRecordEffect()
	{
		if (!effectPlayed)
		{
			GameObject gameObject = EffectManager.Instance.CreateEffect(eEffectType.WIN_BREAK_RECORD, isPause: true, isOneShot: false);
			Effect component = gameObject.GetComponent<Effect>();
			component.AttachTo(GameObject.Find("LocalPlayer").transform, new Vector3(0f, -8.2f, 0f), Vector3.zero);
			component.SetLifeTime(100f);
			component.doReset();
			effectPlayed = true;
		}
	}

	protected void LockMovement()
	{
		Debug.Log("Lock movement!!!!!!!");
		HeroCar heroCar = (HeroCar)TSingleton<CarManager>.Instance.GetHeroCar();
		heroCar.BreakVehicle(1.5f);
		heroCar.LockMovement(bLock: true);
		SceneUIManager.Instance.RemoveAllSceneUIExceptCount();
	}

	protected void QuitNetworkLevel()
	{
		Debug.Log("Quit network level");
		ShowUI = false;
		SoundManager.Instance.StopAllSound();
		RaceModeFactory.CurrentRaceMode.Uninit();
		TSingleton<PropManager>.Instance.Clear();
		SceneUIManager.Instance.HideRankUI();
		SoundManager.Instance.ClearSteroAudios();
		DriveManager.Instance.SetCanDrive(drive: true);
		if (null != SceneUIManager.Instance.m_Loading)
		{
			SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
			SceneUIManager.Instance.m_Loading.GetComponent<LoadingController>().ShowLoadingIndicator();
		}
		if (null != SceneUIManager.Instance.m_Loading)
		{
			SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
		}
		if (AppMain.Instance.IsServer)
		{
			SceneManager.Instance.LoadLevel(1006);
		}
		else
		{
			SceneManager.Instance.LoadLevel(1007);
		}
	}

	private void DelayLoadingCharacterAndSelectTrackLevel()
	{
		Debug.Log("Reload DelayLoadingCharacterAndSelectTrackLevel ");
		SceneManager.Instance.LoadLevel(1003);
		Object.DestroyObject(timer.gameObject);
	}

	private void DelayLoadingCurrentGameLevel()
	{
		Debug.Log("Reload level " + lastPlayGameLevel);
		SceneManager.Instance.LoadLevel(lastPlayGameLevel);
		Object.DestroyObject(timer.gameObject);
	}

	protected void PlayFinishSound()
	{
		if (!finishSoundPlayed)
		{
			if (playerRank <= 3 && playerRank >= 1)
			{
				SoundManager.Instance.PlaySound(60014);
			}
			else if (playerRank > 3 && playerRank <= 6)
			{
				SoundManager.Instance.PlaySound(60015);
			}
			finishSoundPlayed = true;
		}
	}

	protected void PlayRankSound()
	{
		if (!SoundManager.Instance.IsPlaying(60015) && !SoundManager.Instance.IsPlaying(60014))
		{
			SoundManager.Instance.PlaySound(60016);
		}
	}

	protected bool SetWord()
	{
		if ((bool)words && !wordSet)
		{
			int trackLevel = LocalUserInfo.Instance.TrackLevel;
			if (SelfIfFinished() && RaceSceneData.Rank(TSingleton<CarManager>.Instance.GetHeroCar().ID) == 1 && LocalUserConfig.Instance.MedalsCurrentMode(SceneManager.Instance.CurGameSceneId) < 3)
			{
				LocalUserConfig.Instance.SaveMedal(SceneManager.Instance.CurGameSceneId, LocalUserInfo.Instance.TrackLevel);
				background.active = true;
				congra.active = true;
				medal.active = true;
				aero.active = true;
				switch (trackLevel)
				{
				case 0:
					easyWord.active = true;
					break;
				case 1:
					normalWord.active = true;
					break;
				case 2:
					masterWord.active = true;
					break;
				default:
					background.active = false;
					congra.active = false;
					medal.active = false;
					aero.active = false;
					break;
				}
			}
			else
			{
				background.active = false;
				congra.active = false;
				medal.active = false;
				aero.active = false;
			}
			wordSet = true;
			return true;
		}
		return false;
	}

	protected void AnimatePosition(GameObject theObj, Vector3 startPos, Vector3 finalPos, float deltaTime)
	{
		if (!(theObj == null))
		{
			if (!posAniTimeDic.ContainsKey(theObj))
			{
				posAniTimeDic.Add(theObj, Time.time);
			}
			theObj.transform.localPosition = Vector3.Lerp(startPos, finalPos, (Time.time - posAniTimeDic[theObj]) / deltaTime);
		}
	}

	protected void AnimateScale(GameObject theObj, float fromScale, float toScale, float deltaTime)
	{
		if (!(theObj == null))
		{
			if (!scaleAniTimeDic.ContainsKey(theObj))
			{
				scaleAniTimeDic.Add(theObj, Time.time);
			}
			float num = Mathf.Lerp(fromScale, toScale, (Time.time - scaleAniTimeDic[theObj]) / deltaTime);
			theObj.transform.localScale = new Vector3(num, num, num);
		}
	}

	protected void HideCountingView()
	{
		for (int i = 0; i < 10; i++)
		{
			if (countNumbers != null && (bool)countNumbers[i])
			{
				Object.DestroyImmediate(countNumbers[i]);
			}
		}
		if (countNumbers != null)
		{
			countNumbers.Clear();
			countNumbers = null;
		}
	}

	protected bool CheckSelfNotFinishInTime()
	{
		if (RaceSceneData.SpendTime(TSingleton<CarManager>.Instance.GetHeroCar().ID) > RaceModeFactory.CurrentRaceMode.TotalTime())
		{
			return true;
		}
		return false;
	}

	protected bool SelfIfFinished()
	{
		return RaceSceneData.IsFinish(TSingleton<CarManager>.Instance.GetHeroCar().ID);
	}

	private void OnGUI()
	{
		if (ShowUI)
		{
			RANKUI_STATE rANKUI_STATE = state;
			if (rANKUI_STATE == RANKUI_STATE.RANKUI_STATE_SHOWRANK)
			{
				DrawNetworkRankDetail();
			}
		}
	}
}
