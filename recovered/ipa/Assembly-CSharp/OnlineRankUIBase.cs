using System.Collections;
using System.Collections.Generic;
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

	protected RANKUI_STATE state;

	protected int playerNum;

	protected int playerRank;

	protected Texture2D finishTex;

	protected Rect finishRect;

	protected Vector2 finishScale;

	protected float finishDeltaTime1;

	protected float finishDeltaTime2;

	protected Texture2D unFinishTex;

	protected Texture2D rankImgTex;

	protected Rect rankImgRect;

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

	protected bool touchAgainButton;

	protected bool touchBackButton;

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

	protected float showRankTime;

	protected float waitTime;

	protected Dictionary<int, GameObject> countNumbers;

	protected bool selfIsFinished;

	protected Vector2 resolutionScale;

	protected bool isBreakRecord;

	private bool finishWordSet;

	private bool m_isFinished;

	protected ArrayList rankList;

	private bool effectPlayed;

	private DelayDelegateTimer timer;

	private int lastPlayGameLevel;

	private bool finishSoundPlayed;

	protected float delayTime;

	protected float m_PreSendTime;

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

	protected void Awake()
	{
	}

	public virtual void Init()
	{
	}

	private void SetGameObject()
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

	protected bool SelfIsFinished()
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

	protected void DrawNetworkRankDetail()
	{
	}

	protected void PlayBreakRecordEffect()
	{
	}

	protected void LockMovement()
	{
	}

	protected void QuitNetworkLevel()
	{
	}

	private void DelayLoadingCharacterAndSelectTrackLevel()
	{
	}

	private void DelayLoadingCurrentGameLevel()
	{
	}

	protected void PlayFinishSound()
	{
	}

	protected void PlayRankSound()
	{
	}

	protected bool SetWord()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void AnimatePosition(GameObject theObj, Vector3 startPos, Vector3 finalPos, float deltaTime)
	{
	}

	protected void AnimateScale(GameObject theObj, float fromScale, float toScale, float deltaTime)
	{
	}

	protected void HideCountingView()
	{
	}

	protected bool CheckSelfNotFinishInTime()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected bool SelfIfFinished()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void OnGUI()
	{
	}
}
