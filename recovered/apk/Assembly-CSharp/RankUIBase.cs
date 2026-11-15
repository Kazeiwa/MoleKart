using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankUIBase : MonoBehaviour
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

	protected RANKUI_STATE state = RANKUI_STATE.RANKUI_STATE_COMPLETE_SCALE;

	protected static string xml_path = "Config/xmlConfig/RankUIConfig";

	protected int playerNum;

	protected int playerRank = -1;

	protected Texture2D finishTex;

	protected Rect finishRect;

	protected Vector2 finishScale = new Vector2(6f, 6f);

	protected float finishDeltaTime1 = 0.5f;

	protected float finishDeltaTime2 = 4f;

	protected Texture2D unFinishTex;

	protected Texture2D againNoremal;

	protected Texture2D againSelected;

	protected Texture2D againButton;

	protected Rect againRect;

	protected Texture2D backButtonNormal;

	protected Texture2D backButtonSelected;

	protected Texture2D backButton;

	protected Rect backButtonRect;

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

	protected Rect[] rankNameRect = new Rect[6];

	protected Rect[] rankTimeNumRect = new Rect[8];

	protected Dictionary<int, string> names = new Dictionary<int, string>();

	protected bool touchAgainButton;

	protected bool touchBackButton;

	protected bool ShowUI;

	protected string language;

	protected string plat;

	protected bool[] nameLabelIsSet = new bool[6];

	protected TextAsset fontTextAsset;

	protected Texture fontTexture;

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

	protected Dictionary<int, GameObject> countNumbers = new Dictionary<int, GameObject>();

	protected bool selfIsFinished;

	protected Vector2 resolutionScale = new Vector2(1f, 1f);

	protected bool isBreakRecord;

	private bool finishWordSet;

	private bool m_isFinished;

	private bool effectPlayed;

	private DelayDelegateTimer timer;

	private int lastPlayGameLevel = -1;

	private bool finishSoundPlayed;

	private float delayTime = 1f;

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
		fontTextAsset = root.fontTextAsset;
		fontTexture = root.fontTexture;
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

	private void Awake()
	{
		SceneUIManager.Instance.SetRaceStarted(started: false);
		language = AppMain.Instance.GetCurrentSystemLanguage();
		plat = AppMain.Instance.GetCurrentPlatform();
		plat = "ipad";
		resolutionScale.x = (float)Screen.width / 1024f;
		resolutionScale.y = (float)Screen.height / 768f;
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
		againNoremal = SceneUIManager.Instance.againNoremal;
		againSelected = SceneUIManager.Instance.againSelected;
		backButtonNormal = SceneUIManager.Instance.backButtonNormal;
		backButtonSelected = SceneUIManager.Instance.backButtonSelected;
		rankImgTex = SceneUIManager.Instance.rankImgTex;
		timePointTex = SceneUIManager.Instance.timePointTex;
		timePoint2Tex = SceneUIManager.Instance.timePoint2Tex;
		unFinishTex = SceneUIManager.Instance.unFinishedTimeTex;
		againButton = againNoremal;
		backButton = backButtonNormal;
		if (plat == "iphone")
		{
			againRect = new Rect(12f, 514f, againNoremal.width, againNoremal.height);
			backButtonRect = new Rect(616f, 514f, backButtonNormal.width, backButtonNormal.height);
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
		else
		{
			againRect = new Rect(0f * resolutionScale.x, 665f * resolutionScale.y, (float)againNoremal.width * resolutionScale.x, (float)againNoremal.height * resolutionScale.y);
			backButtonRect = new Rect(694f * resolutionScale.x, 665f * resolutionScale.y, (float)backButtonNormal.width * resolutionScale.x, (float)backButtonNormal.height * resolutionScale.y);
			if (language == "zh-Hans")
			{
				rankImgRect = new Rect(449f * resolutionScale.x, 10f * resolutionScale.y, (float)rankImgTex.width * resolutionScale.x, (float)rankImgTex.height * resolutionScale.y);
			}
			else if (language == "en")
			{
				rankImgRect = new Rect(362f * resolutionScale.x, 10f * resolutionScale.y, (float)rankImgTex.width * resolutionScale.x, (float)rankImgTex.height * resolutionScale.y);
			}
			else
			{
				rankImgRect = new Rect(449f * resolutionScale.x, 10f * resolutionScale.y, (float)rankImgTex.width * resolutionScale.x, (float)rankImgTex.height * resolutionScale.y);
			}
		}
		Init();
	}

	public virtual void Init()
	{
		SetNames();
		SetRankNumTex();
		SetTimeNumTex();
		SetBarTex();
		ParseRankUIConfig();
		SetTimeRect();
		ShowUI = true;
		SceneUIManager.Instance.RemoveAllSceneUI();
		InitUIConfiguration();
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

	private void SetNames()
	{
		switch (language)
		{
		case "zh-Hans":
			names.Add(3001, "么么公主");
			names.Add(3002, "RK");
			names.Add(3003, "乐乐侠");
			names.Add(3101, "大白熊");
			names.Add(3201, "毛毛头");
			names.Add(3004, "库拉");
			names.Add(3005, "菩提大伯");
			names.Add(3006, "丫丽");
			break;
		case "en":
			names.Add(3001, "Princess Momo");
			names.Add(3002, "RK");
			names.Add(3003, "Mole Man");
			names.Add(3101, "Big White Bear");
			names.Add(3201, "Hairy Head");
			names.Add(3004, "Kura");
			names.Add(3005, "Uncle Bodhi");
			names.Add(3006, "Ally");
			break;
		case "zh-Hant":
			names.Add(3001, "麼麼公主");
			names.Add(3002, "RK");
			names.Add(3003, "樂樂俠");
			names.Add(3101, "大白熊");
			names.Add(3201, "毛毛頭");
			names.Add(3004, "庫拉");
			names.Add(3005, "菩提大伯");
			names.Add(3006, "丫麗");
			break;
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
				reference2 = new Rect((float)(700 + j * 25) * resolutionScale.x, 135f * resolutionScale.y, 25f * resolutionScale.x, 30f * resolutionScale.y);
			}
		}
	}

	protected void SetAllRect()
	{
		if (plat == "iphone")
		{
			for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
			{
				ref Rect reference = ref rankBarRect[i];
				reference = new Rect(124f, 95 + i * 68, rankBarTex[0].width, rankBarTex[0].height);
				ref Rect reference2 = ref rankNumRect[i];
				reference2 = new Rect(161f, 74 + i * 70, rankNumTex[i].width, rankNumTex[i].height);
				ref Rect reference3 = ref rankIconRect[i];
				reference3 = new Rect(308f, 84 + i * 70, 70f, 70f);
				ref Rect reference4 = ref rankNameRect[i];
				reference4 = new Rect(200f * resolutionScale.x, (float)(89 + i * 34) * resolutionScale.y, 120f * resolutionScale.x, 50f * resolutionScale.y);
			}
			return;
		}
		float num = Mathf.Min(resolutionScale.x, resolutionScale.y);
		for (int j = 0; j < TSingleton<CarManager>.Instance.GetCarCount(); j++)
		{
			ref Rect reference5 = ref rankBarRect[j];
			reference5 = new Rect(128f * resolutionScale.x, (float)(111 + j * 87) * resolutionScale.y, (float)rankBarTex[0].width * resolutionScale.x, (float)rankBarTex[0].height * resolutionScale.y);
			ref Rect reference6 = ref rankNumRect[j];
			reference6 = new Rect(150f * resolutionScale.x, (float)(80 + j * 90) * resolutionScale.y, (float)rankNumTex[j].width * resolutionScale.x, (float)rankNumTex[j].height * resolutionScale.y);
			ref Rect reference7 = ref rankIconRect[j];
			reference7 = new Rect(300f * resolutionScale.x, (float)(100 + j * 87) * resolutionScale.y, 90f * num, 90f * num);
			ref Rect reference8 = ref rankNameRect[j];
			reference8 = new Rect(437f * resolutionScale.x, (float)(135 + j * 87) * resolutionScale.y, 240f * resolutionScale.x, 50f * resolutionScale.y);
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

	protected bool SelfIfFinished()
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

	protected void DrawRankDetail()
	{
		int num = 0;
		num = ((!(plat == "iphone")) ? ((int)(87f * resolutionScale.y)) : 68);
		GUI.DrawTexture(backButtonRect, backButton);
		GUI.DrawTexture(againRect, againButton);
		GUI.DrawTexture(rankImgRect, rankImgTex);
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (!(carByIndex != null))
			{
				continue;
			}
			PlayerData data = new PlayerData();
			if (!RaceSceneData.GetPlayerData((ulong)carByIndex.ID, ref data))
			{
				continue;
			}
			int num2 = data.Ranking - 1;
			if (num2 == TSingleton<CarManager>.Instance.GetCarCount() || num2 < 0)
			{
				continue;
			}
			if (carByIndex.isHero)
			{
				playerRank = num2 + 1;
				GUI.DrawTexture(rankBarRect[num2], rankBarTex[1]);
			}
			else
			{
				GUI.DrawTexture(rankBarRect[num2], rankBarTex[0]);
			}
			GUI.DrawTexture(rankNumRect[num2], rankNumTex[num2]);
			if (carByIndex.isHero)
			{
				if (carByIndex.GetRole(0) != null)
				{
					GUI.DrawTexture(rankIconRect[num2], rankIconTex[carByIndex.GetRole(0).RoleID.ToString()]);
				}
			}
			else if (carByIndex.GetRole(0) != null)
			{
				GUI.DrawTexture(rankIconRect[num2], rankIconTex[carByIndex.GetRole(0).RoleID.ToString()]);
			}
			string value = string.Empty;
			if (carByIndex.isHero)
			{
				value = LocalUserConfig.Instance.userName;
			}
			else
			{
				names.TryGetValue(carByIndex.GetRole(0).RoleID, out value);
			}
			if (Event.current.type.Equals(UnityEngine.EventType.Repaint) && value.Length > 0)
			{
				SpriteFont font = FontStore.GetFont(fontTextAsset);
				float num3 = (float)fontTexture.width * resolutionScale.x;
				float num4 = (float)fontTexture.height * resolutionScale.y;
				float num5 = 0f;
				string text = value;
				foreach (char ch in text)
				{
					SpriteChar spriteChar = font.GetSpriteChar(ch);
					float num6 = (spriteChar.UVs.xMax - spriteChar.UVs.x) * num3 * 1.8f;
					float height = (spriteChar.UVs.yMax - spriteChar.UVs.y) * num4 * 1.8f;
					Rect screenRect = new Rect(rankNameRect[num2].left + num5, rankNameRect[num2].top - spriteChar.yOffset * resolutionScale.y, num6, height);
					Graphics.DrawTexture(screenRect, fontTexture, spriteChar.UVs, 0, 0, 0, 0, Color.black);
					num5 += num6 * 1.1f;
				}
			}
			if (data.SpendTime > RaceModeFactory.CurrentRaceMode.TotalTime())
			{
				if (plat == "ipad")
				{
					GUI.DrawTexture(new Rect(710f * resolutionScale.x, (float)(132 + num2 * 86) * resolutionScale.y, (float)unFinishTex.width * resolutionScale.x, (float)unFinishTex.height * resolutionScale.y), unFinishTex);
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
			float spendTime = data.SpendTime;
			int num7 = (int)(spendTime / 60f);
			int num8 = (int)(spendTime - (float)(num7 * 60));
			int num9 = (int)(spendTime * 100f - (float)(num8 * 100) - (float)(num7 * 60 * 100));
			if (num7 / 10 > 0)
			{
				GUI.DrawTexture(new Rect(rankTimeNumRect[0].xMin, rankTimeNumRect[0].yMin + (float)(num2 * num), rankTimeNumRect[0].width, rankTimeNumRect[0].height), rankTimeNumTex[num7 / 10]);
			}
			GUI.DrawTexture(new Rect(rankTimeNumRect[1].xMin, rankTimeNumRect[1].yMin + (float)(num2 * num), rankTimeNumRect[1].width, rankTimeNumRect[1].height), rankTimeNumTex[num7 - num7 / 10]);
			GUI.DrawTexture(new Rect(rankTimeNumRect[2].xMin, rankTimeNumRect[2].yMin + (float)(num2 * num), (float)timePointTex.width * resolutionScale.x, (float)timePointTex.height * resolutionScale.y), timePointTex);
			int num10 = num8 / 10;
			int num11 = num8 - num10 * 10;
			if (num10 >= 6)
			{
				num10 = 0;
			}
			if (num11 > 9)
			{
				num11 = 0;
			}
			GUI.DrawTexture(new Rect(rankTimeNumRect[3].xMin, rankTimeNumRect[3].yMin + (float)(num2 * num), rankTimeNumRect[3].width, rankTimeNumRect[3].height), rankTimeNumTex[num10]);
			GUI.DrawTexture(new Rect(rankTimeNumRect[4].xMin, rankTimeNumRect[4].yMin + (float)(num2 * num), rankTimeNumRect[4].width, rankTimeNumRect[4].height), rankTimeNumTex[num11]);
			GUI.DrawTexture(new Rect(rankTimeNumRect[5].xMin, rankTimeNumRect[5].yMin + (float)(num2 * num), (float)timePoint2Tex.width * resolutionScale.x, (float)timePoint2Tex.height * resolutionScale.y), timePoint2Tex);
			int num12 = num9 / 10;
			int num13 = num9 - num12 * 10;
			if (num12 > 9)
			{
				num12 = 0;
			}
			if (num13 > 9)
			{
				num13 = 0;
			}
			GUI.DrawTexture(new Rect(rankTimeNumRect[6].xMin, rankTimeNumRect[6].yMin + (float)(num2 * num), rankTimeNumRect[6].width, rankTimeNumRect[6].height), rankTimeNumTex[num12]);
			GUI.DrawTexture(new Rect(rankTimeNumRect[7].xMin, rankTimeNumRect[7].yMin + (float)(num2 * num), rankTimeNumRect[7].width, rankTimeNumRect[7].height), rankTimeNumTex[num13]);
		}
	}

	protected void CheckTouch()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 point = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
			if (againRect.Contains(point))
			{
				againButton = againSelected;
				touchAgainButton = true;
			}
			else if (backButtonRect.Contains(point))
			{
				backButton = backButtonSelected;
				touchBackButton = true;
			}
			else
			{
				backButton = backButtonNormal;
				againButton = againNoremal;
				touchAgainButton = false;
				touchBackButton = false;
			}
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		Vector2 point2 = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		if (againRect.Contains(point2) && touchAgainButton)
		{
			againButton = againNoremal;
			if (RaceModeFactory.CurrentRaceMode != null)
			{
				backButton = backButtonNormal;
				ShowUI = false;
				int curGameSceneId = SceneManager.Instance.CurGameSceneId;
				QuitLevel(restart: true);
				SoundManager.Instance.StopSound(60002);
				SceneManager.Instance.LoadLevel(curGameSceneId);
			}
		}
		else if (backButtonRect.Contains(point2) && touchBackButton)
		{
			QuitLevel(restart: false);
		}
		else
		{
			backButton = backButtonNormal;
			againButton = againNoremal;
			touchAgainButton = false;
			touchBackButton = false;
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

	private void QuitLevel(bool restart)
	{
		Debug.Log("Quit not network level");
		ShowUI = false;
		if (!restart)
		{
			lastPlayGameLevel = SceneManager.Instance.CurGameSceneId;
		}
		LocalUserInfo.Instance.TrackLevel = TrackLockManager.Instance.CheckDiffculty(SceneManager.Instance.CurGameSceneId);
		SceneManager.Instance.LoadEmptyLevel();
		SoundManager.Instance.StopAllSound();
		RaceModeFactory.CurrentRaceMode.Uninit();
		TSingleton<PropManager>.Instance.Clear();
		SceneUIManager.Instance.HideRankUI();
		SoundManager.Instance.ClearSteroAudios();
		if (null != SceneUIManager.Instance.m_Loading)
		{
			SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
			SceneUIManager.Instance.m_Loading.GetComponent<LoadingController>().ShowLoadingIndicator();
		}
		AppMain.Instance.Pause = false;
		DriveManager.Instance.SetCanDrive(drive: true);
		Resources.UnloadUnusedAssets();
		timer = DelayDelegateTimer.Create();
		if (!restart)
		{
			timer.InitTimer(3f, DelayLoadingCharacterAndSelectTrackLevel);
		}
		else
		{
			timer.InitTimer(3f, DelayLoadingCurrentGameLevel);
		}
		timer.StartTimer();
		Object.DontDestroyOnLoad(timer.gameObject);
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

	private void Update()
	{
		SetAllRect();
		switch (state)
		{
		case RANKUI_STATE.RANKUI_STATE_COMPLETE_SCALE:
			SetFinishWord();
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
			if (!SoundManager.Instance.IsPlaying(60015) && !SoundManager.Instance.IsPlaying(60014))
			{
				SoundManager.Instance.PlaySound(60016);
			}
			delayTime -= Time.deltaTime;
			SetWord();
			switch (AppMain.Instance.GetCurrentPlatform())
			{
			default:
			{
				int num;
				if (num == 1)
				{
					if (delayTime >= 0f)
					{
						AnimatePosition(words, words.transform.localPosition, new Vector3(0f, -372f, 148f), 1f);
					}
					AnimateScale(medal, 0f, 1f, 1f);
					AnimateScale(aero, 0f, 768f, 1f);
					if ((bool)aero)
					{
						aero.transform.RotateAroundLocal(new Vector3(0f, 0f, 1f), Time.deltaTime);
					}
				}
				else
				{
					if (delayTime >= 0f)
					{
						AnimatePosition(words, words.transform.localPosition, new Vector3(0f, -372f, 148f), 1f);
					}
					AnimateScale(medal, 0f, 1f, 1f);
					AnimateScale(aero, 0f, 768f, 1f);
					if ((bool)aero)
					{
						aero.transform.RotateAroundLocal(new Vector3(0f, 0f, 1f), Time.deltaTime);
					}
				}
				break;
			}
			case "ipad":
				if (delayTime >= 0f)
				{
					AnimatePosition(words, words.transform.localPosition, new Vector3(0f, -372f, 148f), 1f);
				}
				AnimateScale(medal, 0f, 1f, 1f);
				AnimateScale(aero, 0f, 768f, 1f);
				if ((bool)aero)
				{
					aero.transform.RotateAroundLocal(new Vector3(0f, 0f, 1f), Time.deltaTime);
				}
				break;
			}
			AnimateScale(finish, 4f, 1f, 0.2f);
			finishDeltaTime2 -= Time.deltaTime;
			if (finishDeltaTime2 <= 0f)
			{
				state = RANKUI_STATE.RANKUI_STATE_SHOWRANK;
				SceneUIManager.Instance.RemoveAllSceneUI();
			}
			break;
		case RANKUI_STATE.RANKUI_STATE_SHOWRANK:
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < base.transform.GetChildCount(); i++)
			{
				arrayList.Add(base.transform.GetChild(i));
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				Object.Destroy(((Transform)arrayList[j]).gameObject);
			}
			CheckTouch();
			break;
		}
		case RANKUI_STATE.RANKUI_STATE_COMPLETE_SHOW:
			break;
		}
	}

	private bool SetWord()
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

	private void AnimatePosition(GameObject theObj, Vector3 startPos, Vector3 finalPos, float deltaTime)
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

	private void AnimateScale(GameObject theObj, float fromScale, float toScale, float deltaTime)
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

	private void OnGUI()
	{
		if (ShowUI)
		{
			RANKUI_STATE rANKUI_STATE = state;
			if (rANKUI_STATE == RANKUI_STATE.RANKUI_STATE_SHOWRANK)
			{
				DrawRankDetail();
			}
		}
	}
}
