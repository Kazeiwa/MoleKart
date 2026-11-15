using RaceProtocol;
using TaomeeNetwork;

internal class OnlineModeBase : RaceModeBase
{
	private float fMoveCameraTime;

	private bool m_bFade;

	private bool m_bReadyToReceivePkg;

	private bool bLoadingisFinish;

	private Handler<MsgPlayerData2> playerInfoHandler;

	private Handler<MsgStartRace> startRaceHandler;

	private IRaceLogicModule pkModule;

	private float fLastUpdateTime;

	private bool readyMessageSent;

	public override void Init(SceneRace scene)
	{
	}

	private void InitPropWayPoints(int sceneID)
	{
	}

	private void InitRaceAI()
	{
	}

	public override void Uninit()
	{
	}

	protected override void CreatePlayerCar()
	{
	}

	private void CreateRemoteCars()
	{
	}

	public override void UpdateMode()
	{
	}

	private void UpdateRaceData()
	{
	}

	public void UpdateTime()
	{
	}

	public void UpdateResultTime()
	{
	}

	private void UpdateUIInRace()
	{
	}

	private void UpdateGameStartCounter()
	{
	}

	private void StartShowTimeCamera()
	{
	}

	private void UpdateShowTimeCamera()
	{
	}

	public override void RaceOver(bool over)
	{
	}

	private void StopAllPlayerCar()
	{
	}

	public override void FinishRace()
	{
	}

	private void InitNetwork()
	{
	}

	private void UninitNetwork()
	{
	}

	private void SendLocalPlayerInformationToServer()
	{
	}

	private void OnPlayerInfoChanaged(string peerId, MsgPlayerData2 message)
	{
	}

	private void SendReadyMessageToServer()
	{
	}

	public void StopSynchronizeMovementPkg()
	{
	}

	private void OnStartRace(string peerId, MsgStartRace message)
	{
	}

	public override void BeginCountTime()
	{
	}

	private void OnGameResult(string peerId, MsgGameResult msg)
	{
	}
}
