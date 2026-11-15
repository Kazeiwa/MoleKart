using System;
using System.Collections.Generic;
using System.Text;
using Leaderboard;
using Misc;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
	public enum STATE_NETWORK
	{
		Connecting,
		Disconnected,
		Connected,
		NetworkUnavailable,
		Sending,
		SendFailed,
		ReadSuccess
	}

	public class NetworkState : SuperFSM<STATE_NETWORK, LeaderboardController>.State
	{
		public NetworkState(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class StateConnecting : NetworkState
	{
		public StateConnecting(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
			Debug.Log("StateConnecting");
			base.Behaviour.currentRetryTimes++;
			base.Behaviour.ConnectToHost();
		}
	}

	protected class StateConnected : NetworkState
	{
		public StateConnected(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.currentRetryTimes = 0;
			base.Behaviour.startTime = 0f;
			Debug.Log("StateConnected");
		}
	}

	protected class StateDisconnected : NetworkState
	{
		public StateDisconnected(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
			Debug.Log("StateDisconnected");
		}
	}

	protected class StateConnectionUnavailable : NetworkState
	{
		public StateConnectionUnavailable(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
			Debug.Log("StateConnectionUnavailable");
		}
	}

	protected class StateSending : NetworkState
	{
		public StateSending(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
			Debug.Log("StateSending");
		}
	}

	protected class StateReadSuccess : NetworkState
	{
		public StateReadSuccess(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
			Debug.Log("StateReadSuccess");
		}
	}

	protected class StateNetworkUnavailable : NetworkState
	{
		public StateNetworkUnavailable(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
			UtilBinding.ShowWaitingView(bShow: false);
			MyTcpClient.Instance.CloseClient();
			base.Behaviour.ShowNetworkUnavailableAlert();
			base.Behaviour.SendDataToSelfScoreView();
			base.Behaviour.SendDataToTableView();
			Debug.Log("StateNetworkUnavailable");
		}
	}

	protected class StateSendFailed : NetworkState
	{
		public StateSendFailed(STATE_NETWORK state, LeaderboardController controller)
			: base(state, controller)
		{
		}

		public override void EntryAction()
		{
			Debug.Log("StateSendFailed");
		}
	}

	private const string HOST_IP = "211.151.105.138";

	private const int HOST_PORT = 25113;

	private const float DELTA_TIME = 30f;

	private const int MAX_RETRY_TIMES = 3;

	private const float CONNECTION_TIME_OUT = 10f;

	private const uint MAX_NUM_IN_PAGE = 30u;

	private int currentRetryTimes;

	private SuperFSM<STATE_NETWORK, LeaderboardController> fsm;

	private LeaderboardDataManager dataManager;

	private LeaderboardDataManager.TRACK_NAME selectedTrack = LeaderboardDataManager.TRACK_NAME.Sky;

	private LeaderboardDataManager.GAME_MODE selectedMode = LeaderboardDataManager.GAME_MODE.Prop;

	private LeaderboardDataManager.TIME_SCOPE selectedTimeScope = LeaderboardDataManager.TIME_SCOPE.TIME_SCOPE_ALL_TIME;

	private int pageInTableView;

	private string category = string.Empty;

	private float startTime;

	private bool tcpConnected;

	private GameObject effectObject;

	private bool noClick = true;

	private bool canDownload = true;

	private bool firstClick;

	private bool alertShown;

	public static LeaderboardController instance;

	private float mToastSumShowTime = 2f;

	private bool mBShowToast;

	private int mPreHour;

	private int mPreMinute;

	private int mPreSecond;

	public bool Connected => tcpConnected;

	private void Awake()
	{
		NetHandlerRegistry.Instance.RegistHandler<HostConnected>(OnConnectedToHost);
		NetHandlerRegistry.Instance.RegistHandler<HostDisconnected>(OnDisconnectedToHost);
		NetHandlerRegistry.Instance.RegistHandler<RankingListAck>(OnReceiveScoreList);
		NetHandlerRegistry.Instance.RegistHandler<MyScorePercentAck>(OnReceiveSelfScore);
	}

	private void Start()
	{
		dataManager = new LeaderboardDataManager();
		MakeFSM();
		fsm.GotoState(STATE_NETWORK.Disconnected);
		selectedMode = LeaderboardDataManager.GAME_MODE.Prop;
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Farm;
		selectedTimeScope = LeaderboardDataManager.TIME_SCOPE.TIME_SCOPE_TODAY;
		PlaySelectedEffect();
		instance = this;
	}

	private void Update()
	{
		if (noClick)
		{
			canDownload = true;
		}
		else if (Time.time - startTime < 30f)
		{
			canDownload = false;
		}
		else
		{
			UtilBinding.ShowWaitingView(bShow: false);
			noClick = true;
			firstClick = false;
		}
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	private void OnDestroy()
	{
		NetHandlerRegistry.Instance.UnregistHandler<HostConnected>(OnConnectedToHost);
		NetHandlerRegistry.Instance.UnregistHandler<HostDisconnected>(OnDisconnectedToHost);
		NetHandlerRegistry.Instance.UnregistHandler<RankingListAck>(OnReceiveScoreList);
		NetHandlerRegistry.Instance.UnregistHandler<MyScorePercentAck>(OnReceiveSelfScore);
	}

	private void MakeCategory()
	{
		alertShown = false;
		if (!firstClick)
		{
			noClick = false;
			startTime = Time.time;
			firstClick = true;
		}
		string text = selectedMode.ToString() + "." + selectedTrack.ToString() + "." + selectedTimeScope;
		if (category != null && text != category)
		{
			pageInTableView = 0;
			category = text;
			RequestScoreList(clearTableView: false);
			RequestSelfScore();
		}
	}

	public void ShowNetworkUnavailableAlert()
	{
		if (alertShown)
		{
			return;
		}
		Debug.Log("Network unavailable!!!!!");
		string currentSystemLanguage = AppMain.Instance.GetCurrentSystemLanguage();
		string msg;
		if (currentSystemLanguage == "zh-Hans")
		{
			string text = null;
			msg = "无法获得数据，请确认已经连接网络，或稍后再试。";
			string text2 = "好";
		}
		else if (currentSystemLanguage == "zh-Hant")
		{
			string text = null;
			msg = "無法獲得數據，請確認已經連接網絡，或稍後再試。";
			string text2 = "好";
		}
		else
		{
			string text = null;
			msg = "Unable to get data, please make sure to connect to internet or try again later.";
			string text2 = "OK";
		}
		if (!mBShowToast)
		{
			DateTime now = DateTime.Now;
			mPreHour = now.Hour;
			mPreMinute = now.Minute;
			mPreSecond = now.Second;
			UtilBinding.ShowToast(msg);
			mBShowToast = true;
		}
		else
		{
			DateTime now2 = DateTime.Now;
			int hour = now2.Hour;
			int minute = now2.Minute;
			int second = now2.Second;
			if (hour != mPreHour || minute != mPreMinute || (float)Mathf.Abs(second - mPreSecond) > mToastSumShowTime)
			{
				UtilBinding.ShowToast(msg);
				mPreHour = hour;
				mPreMinute = minute;
				mPreSecond = second;
			}
		}
		alertShown = true;
	}

	private void SelectPropMode()
	{
		selectedMode = LeaderboardDataManager.GAME_MODE.Prop;
		MakeCategory();
	}

	private void SelectTimeMode()
	{
		selectedMode = LeaderboardDataManager.GAME_MODE.Time;
		MakeCategory();
	}

	private void SelectToday()
	{
		selectedTimeScope = LeaderboardDataManager.TIME_SCOPE.TIME_SCOPE_TODAY;
		MakeCategory();
	}

	private void SelectWeek()
	{
		selectedTimeScope = LeaderboardDataManager.TIME_SCOPE.TIME_SCOPE_WEEK;
		MakeCategory();
	}

	private void SelectAllTime()
	{
		selectedTimeScope = LeaderboardDataManager.TIME_SCOPE.TIME_SCOPE_ALL_TIME;
		MakeCategory();
	}

	private void SelectSky()
	{
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Sky;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void SelectIce()
	{
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Ice;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void SelectVolcano()
	{
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Volcano;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void SelectFactory()
	{
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Factory;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void SelectPort()
	{
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Port;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void SelectMaple()
	{
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Maple;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void SelectFarm()
	{
		Debug.Log("farm");
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Farm;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void SelectManor()
	{
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Manor;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void SelectMushRoom()
	{
		selectedTrack = LeaderboardDataManager.TRACK_NAME.Mushroom;
		PlaySelectedEffect();
		MakeCategory();
	}

	private void PlaySelectedEffect()
	{
		Debug.Log(selectedTrack.ToString());
		GameObject gameObject = GameObject.Find(selectedTrack.ToString());
		if (gameObject == null)
		{
			Debug.Log("btn null");
		}
		if (effectObject != null)
		{
			UnityEngine.Object.DestroyImmediate(effectObject);
			effectObject = null;
		}
		GameObject gameObject2 = EffectManager.Instance.CreateEffect(eEffectType.SELECT_TRACK, isPause: false, isOneShot: false);
		Effect component = gameObject2.GetComponent<Effect>();
		effectObject = gameObject2;
		component.AttachTo(gameObject.transform, new Vector3(0f, 0f, -1f), Vector3.zero);
		component.doReset();
	}

	private void BackToMainMenu()
	{
		CloseConnection();
		UtilBinding.ShowWaitingView(bShow: true);
		SceneManager.Instance.LoadLevel(1001);
	}

	public void ConnectToHost()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			fsm.GotoState(STATE_NETWORK.NetworkUnavailable);
		}
		else if (!tcpConnected && currentRetryTimes <= 3)
		{
			Debug.Log("Connecting........");
			UtilBinding.ShowWaitingView(bShow: true);
			MyTcpClient.Instance.ConnectToHost("211.151.105.138", 25113);
		}
	}

	public void CloseConnection()
	{
		MyTcpClient.Instance.CloseClient();
	}

	private void RequestSelfScore()
	{
		if (!dataManager.CheckHasSelfScore(category) || canDownload)
		{
			ConnectToHost();
			Debug.Log("RequestSelfScore()");
			MyScorePercentReq.Builder builder = new MyScorePercentReq.Builder();
			builder.Mode = (uint)selectedMode;
			builder.Track = (uint)selectedTrack;
			builder.RankType = (uint)selectedTimeScope;
			builder.DeviceId = SystemConfig.Instance.UUID;
			MyScorePercentReq msg = builder.Build();
			MyTcpClient.Instance.SendMsg(msg);
			fsm.GotoState(STATE_NETWORK.Sending);
		}
		else if (dataManager.CheckHasSelfScore(category))
		{
			SendDataToSelfScoreView();
		}
	}

	private void RequestScoreList(bool clearTableView)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			Debug.Log("Network unavailable by Unity");
			fsm.GotoState(STATE_NETWORK.NetworkUnavailable);
			startTime = 0f;
			currentRetryTimes = 0;
			tcpConnected = false;
			return;
		}
		UtilBinding.ShowWaitingView(bShow: true);
		if (!dataManager.CheckHasScoreList(category, pageInTableView) || canDownload)
		{
			Debug.Log("connet to host");
			ConnectToHost();
			RankingListReq.Builder builder = new RankingListReq.Builder();
			builder.Mode = (uint)selectedMode;
			builder.Track = (uint)selectedTrack;
			builder.RankType = (uint)selectedTimeScope;
			builder.PageNo = (uint)pageInTableView;
			builder.PageMax = 30u;
			RankingListReq msg = builder.Build();
			MyTcpClient.Instance.SendMsg(msg);
			fsm.GotoState(STATE_NETWORK.Sending);
		}
		else if (dataManager.CheckHasScoreList(category, pageInTableView))
		{
			SendDataToTableView();
		}
	}

	private void OnConnectedToHost(string peerId, HostConnected msg)
	{
		UtilBinding.ShowWaitingView(bShow: false);
		Debug.Log("OnConnectedToHost(string peerId, HostConnected msg)");
		tcpConnected = true;
		startTime = 0f;
		currentRetryTimes = 0;
		fsm.GotoState(STATE_NETWORK.Connected);
		MakeCategory();
	}

	private void OnDisconnectedToHost(string peerId, HostDisconnected msg)
	{
		Debug.Log("OnDisconnectedToHost(string peerId, HostDisconnected msg)");
		tcpConnected = false;
		fsm.GotoState(STATE_NETWORK.Disconnected);
	}

	private void OnReceiveSelfScore(string peerId, MyScorePercentAck msg)
	{
		if (msg.Score.RankNo != 0)
		{
			MKMyScore mKMyScore = new MKMyScore(msg);
			dataManager.AddSelfScore(mKMyScore.GetCategory(), mKMyScore);
			SendDataToSelfScoreView();
		}
	}

	protected bool CheckScoreListIsValid(RankingListAck msg)
	{
		if (msg.Mode != 1 && msg.Mode != 2)
		{
			return false;
		}
		if (msg.RankType > 3 || msg.RankType < 1)
		{
			return false;
		}
		return true;
	}

	private void OnReceiveScoreList(string peerId, RankingListAck msg)
	{
		if (!CheckScoreListIsValid(msg))
		{
			return;
		}
		Debug.Log("OnReceiveScoreList(string peerId, RankingListAck msg)");
		string text = string.Concat((LeaderboardDataManager.GAME_MODE)msg.Mode, ".", (LeaderboardDataManager.TRACK_NAME)msg.Track, ".", (LeaderboardDataManager.TIME_SCOPE)msg.RankType);
		foreach (MsgScore rankList in msg.RankListList)
		{
			MKScore score = new MKScore(rankList);
			dataManager.AddScore(text, score);
		}
		SendDataToTableView();
	}

	private void MakeFSM()
	{
		fsm = new SuperFSM<STATE_NETWORK, LeaderboardController>(this);
		fsm.CreateState<StateDisconnected>(STATE_NETWORK.Disconnected).CreateTransition(STATE_NETWORK.Connecting, (LeaderboardController b) => b.currentRetryTimes < 3 && Application.internetReachability != NetworkReachability.NotReachable).CreateTransition(STATE_NETWORK.NetworkUnavailable, (LeaderboardController b) => b.currentRetryTimes >= 3 || Application.internetReachability == NetworkReachability.NotReachable);
		fsm.CreateState<StateConnecting>(STATE_NETWORK.Connecting).CreateTransition(STATE_NETWORK.Connected, (LeaderboardController b) => b.tcpConnected).CreateTimedTransition(10f, STATE_NETWORK.Disconnected);
		fsm.CreateState<StateNetworkUnavailable>(STATE_NETWORK.NetworkUnavailable);
		fsm.CreateState<StateSending>(STATE_NETWORK.Sending).CreateTransition(STATE_NETWORK.Disconnected, (LeaderboardController b) => !b.tcpConnected).CreateTimedTransition(10f, STATE_NETWORK.SendFailed);
		fsm.CreateState<StateSendFailed>(STATE_NETWORK.SendFailed).CreateTransition(STATE_NETWORK.Disconnected, (LeaderboardController b) => !b.tcpConnected);
		fsm.CreateState(STATE_NETWORK.Connected);
	}

	public void GetNewData(string page)
	{
		pageInTableView = Convert.ToInt32(page);
		RequestScoreList(clearTableView: false);
	}

	private void SetCurrentTalbeViewPage(string pageStr)
	{
		pageInTableView = Convert.ToInt32(pageStr);
	}

	public void SendDataToTableView()
	{
		if (pageInTableView == 0)
		{
			rankController.instance.destroyListItems();
		}
		StringBuilder stringBuilder = new StringBuilder();
		List<MKScore> scoreList = dataManager.GetScoreList(category, pageInTableView);
		if (scoreList != null)
		{
			foreach (MKScore item in scoreList)
			{
				stringBuilder.Append(item.ToString());
				stringBuilder.Append("\n");
			}
			string text = stringBuilder.ToString();
			if (text != null)
			{
			}
			rankController.instance.insertDataToRankList(scoreList);
		}
		UtilBinding.ShowWaitingView(bShow: false);
	}

	public void SendDataToSelfScoreView()
	{
		MKMyScore myScore = dataManager.GetMyScore(category);
		if (myScore != null)
		{
			string txt = myScore.ToString();
			rankController.instance.displayPlayerScoreInfo(txt);
		}
	}

	public uint getMaxNumInOnePage()
	{
		return 30u;
	}
}
