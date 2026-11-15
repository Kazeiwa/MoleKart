using Leaderboard;
using Misc;
using RaceProtocol;
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
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class StateConnecting : NetworkState
	{
		public StateConnecting(STATE_NETWORK state, LeaderboardController controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	protected class StateConnected : NetworkState
	{
		public StateConnected(STATE_NETWORK state, LeaderboardController controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	protected class StateDisconnected : NetworkState
	{
		public StateDisconnected(STATE_NETWORK state, LeaderboardController controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	protected class StateConnectionUnavailable : NetworkState
	{
		public StateConnectionUnavailable(STATE_NETWORK state, LeaderboardController controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	protected class StateSending : NetworkState
	{
		public StateSending(STATE_NETWORK state, LeaderboardController controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	protected class StateReadSuccess : NetworkState
	{
		public StateReadSuccess(STATE_NETWORK state, LeaderboardController controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	protected class StateNetworkUnavailable : NetworkState
	{
		public StateNetworkUnavailable(STATE_NETWORK state, LeaderboardController controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	protected class StateSendFailed : NetworkState
	{
		public StateSendFailed(STATE_NETWORK state, LeaderboardController controller)
		{
		}

		public override void EntryAction()
		{
		}
	}

	private const string HOST_IP = "211.151.105.138";

	private const int HOST_PORT = 25103;

	private const float DELTA_TIME = 30f;

	private const int MAX_RETRY_TIMES = 3;

	private const float CONNECTION_TIME_OUT = 10f;

	private const uint MAX_NUM_IN_PAGE = 30u;

	private int currentRetryTimes;

	private SuperFSM<STATE_NETWORK, LeaderboardController> fsm;

	private LeaderboardDataManager dataManager;

	private LeaderboardDataManager.TRACK_NAME selectedTrack;

	private LeaderboardDataManager.GAME_MODE selectedMode;

	private LeaderboardDataManager.TIME_SCOPE selectedTimeScope;

	private int pageInTableView;

	private string category;

	private float startTime;

	private bool tcpConnected;

	private GameObject effectObject;

	private bool noClick;

	private bool canDownload;

	private bool firstClick;

	private bool alertShown;

	private bool SetDefaultButton;

	public bool Connected
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}

	private void MakeCategory()
	{
	}

	public void ShowNetworkUnavailableAlert()
	{
	}

	private void SelectPropMode()
	{
	}

	private void SelectTimeMode()
	{
	}

	private void SelectToday()
	{
	}

	private void SelectWeek()
	{
	}

	private void SelectAllTime()
	{
	}

	private void SelectSky()
	{
	}

	private void SelectIce()
	{
	}

	private void SelectVolcano()
	{
	}

	private void SelectPort()
	{
	}

	private void SelectMaple()
	{
	}

	private void SelectFarm()
	{
	}

	private void SelectManor()
	{
	}

	private void SelectFactory()
	{
	}

	private void PlaySelectedEffect()
	{
	}

	private void BackToMainMenu()
	{
	}

	public void ShareWeibo()
	{
	}

	public void ShareFacebook()
	{
	}

	public void ConnectToHost()
	{
	}

	public void CloseConnection()
	{
	}

	private void RequestSelfScore()
	{
	}

	private void RequestScoreList(bool clearTableView)
	{
	}

	private void OnConnectedToHost(string peerId, HostConnected msg)
	{
	}

	private void OnDisconnectedToHost(string peerId, HostDisconnected msg)
	{
	}

	private void OnReceiveSelfScore(string peerId, MyScorePercentAck msg)
	{
	}

	protected bool CheckScoreListIsValid(RankingListAck msg)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void OnReceiveScoreList(string peerId, RankingListAck msg)
	{
	}

	private void MakeFSM()
	{
	}

	private void GetNewData(string page)
	{
	}

	private void SetCurrentTalbeViewPage(string pageStr)
	{
	}

	public void SendDataToTableView()
	{
	}

	public void SendDataToSelfScoreView()
	{
	}
}
