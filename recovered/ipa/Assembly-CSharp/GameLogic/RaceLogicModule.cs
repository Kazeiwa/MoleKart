using System.Collections;
using FrameWork;
using RaceProtocol;
using TaomeeNetwork;

namespace GameLogic;

public class RaceLogicModule : CModule, IRaceLogicModule
{
	private RacingType m_eGameType;

	private SceneRace m_RaceScene;

	public uint m_uiPeerID;

	private Handler<MsgFinishLoadingMap> loadingMapHandler;

	private ArrayList PeersReadyArray;

	private bool m_bYouServer;

	public override void Init(IFrameWork iFrameHost)
	{
	}

	public override void Uninit()
	{
	}

	public RacingType GetCurrentRaceType()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetCurrentRaceType(RacingType eType)
	{
	}

	public IRaceMode GetCurrentRaceMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void CreateGame(int iSceneID)
	{
	}

	public void DestroyGame()
	{
	}

	public override void Update(float fCurTime)
	{
	}

	public override void FixedUpdate()
	{
	}

	public void StartServerLoop()
	{
	}

	public void StopServerLoop()
	{
	}

	public bool IsOnlineMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ServerIsReady()
	{
	}

	public bool YouAreServer()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void OnFinishLoadingMap(string peerId, MsgFinishLoadingMap msg)
	{
	}

	public void ServerIsFinishRace()
	{
	}

	private void OnFinishRace(string peerId, MsgFinishRace msg)
	{
	}
}
