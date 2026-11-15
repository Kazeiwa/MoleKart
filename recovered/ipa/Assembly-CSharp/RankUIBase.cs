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

	protected RANKUI_STATE state;

	protected int playerNum;

	protected int playerRank;

	protected Texture2D finishTex;

	protected Rect finishRect;

	protected Vector2 finishScale;

	protected float finishDeltaTime1;

	protected float finishDeltaTime2;

	protected Texture2D unFinishTex;

	protected Texture2D againNoremal;

	protected Texture2D againSelected;

	protected Texture2D againButton;

	protected Rect againRect;

	protected Texture2D backButtonNormal;

	protected Texture2D backButtonSelected;

	protected Texture2D backButton;

	protected Rect backButtonRect;

	private bool _enableWeibo;

	protected Texture2D weiboButtonNormal;

	protected Texture2D weiboButtonSelected;

	protected Texture2D weiboButton;

	protected Rect weiboButtonRect;

	protected Texture2D facebookButtonNormal;

	protected Texture2D facebookButtonSelected;

	protected Texture2D facebookButton;

	protected Rect facebookButtonRect;

	protected Texture2D rankImgTex;

	protected Rect rankImgRect;

	protected Texture2D keepHurryTex;

	protected Rect keepHurryRect;

	protected Texture2D timePointTex;

	protected Texture2D timePoint2Tex;

	protected Texture2D nullTimeTex;

	protected Texture2D[] rankBarTex;

	protected Texture2D[] rankNumTex;

	protected Texture2D[] rankTimeNumTex;

	protected Rect[] rankBarRect;

	protected Rect[] rankNumRect;

	protected Rect[] rankIconRect;

	protected Rect[] rankTimeNumRect;

	protected Dictionary<int, string> names;

	protected bool touchAgainButton;

	protected bool touchBackButton;

	protected bool touchWeiboButton;

	protected bool touchFacebookButton;

	protected bool ShowUI;

	protected string language;

	protected string plat;

	protected bool[] nameLabelIsSet;

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

	protected Dictionary<int, GameObject> countNumbers;

	protected bool selfIsFinished;

	protected Vector2 resolutionScale;

	protected bool isBreakRecord;

	private bool finishWordSet;

	private bool m_isFinished;

	private int currentMedal;

	private bool effectPlayed;

	private DelayDelegateTimer timer;

	private int lastPlayGameLevel;

	private bool finishSoundPlayed;

	private float delayTime;

	private bool wordSet;

	private Dictionary<GameObject, float> posAniTimeDic;

	private Dictionary<GameObject, float> scaleAniTimeDic;

	public int PlayerNum
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public void InitRankRoot(RankUI root)
	{
	}

	private void Awake()
	{
	}

	public virtual void Init()
	{
	}

	private void SetGameObject()
	{
	}

	private void SetNames()
	{
	}

	private void SetRankNumTex()
	{
	}

	private void SetTimeNumTex()
	{
	}

	private void SetBarTex()
	{
	}

	protected void InitUIConfiguration()
	{
	}

	protected void Reset()
	{
	}

	protected void SetTimeRect()
	{
	}

	protected void SetAllRect()
	{
	}

	protected void DealBreakRecord()
	{
	}

	protected void SetFinishWord()
	{
	}

	protected void SetSelfIfFinished(bool isFinish)
	{
	}

	protected bool SelfIfFinished()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void DrawRankBar()
	{
	}

	protected bool ShowingFinishUI()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void DrawRankDetail()
	{
	}

	protected void CheckTouch()
	{
	}

	protected void PlayBreakRecordEffect()
	{
	}

	protected void LockMovement()
	{
	}

	private void QuitLevel(bool restart)
	{
	}

	private void DelayLoadingCharacterAndSelectTrackLevel()
	{
	}

	private void DelayLoadingCurrentGameLevel()
	{
	}

	private void Update()
	{
	}

	private bool SetWord()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void AnimatePosition(GameObject theObj, Vector3 startPos, Vector3 finalPos, float deltaTime)
	{
	}

	private void AnimateScale(GameObject theObj, float fromScale, float toScale, float deltaTime)
	{
	}

	private void OnGUI()
	{
	}
}
