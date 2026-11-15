using RaceProtocol;
using UnityEngine;

public class HostRoomUI : MonoBehaviour
{
	public const int MaxPlayerCount = 4;

	public Texture2D[] m_PicTimes;

	public GameObject[] m_TimePlanes;

	public Texture2D[] m_TimePic;

	public GameObject[] m_Shelters;

	private bool[] slots;

	private int selectedTrack;

	private RacingType gameMode;

	private bool canGoBack;

	private GameObject startBtn;

	private int m_CurrentLevel;

	public GameObject[] m_Items;

	public static int CURRENT_GAMESERVER_VERSION;

	private int m_TempSelectedTrack;

	private int m_PreSelectTrack;

	private int CheckDiffculty()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SetTrackRecordTime()
	{
	}

	protected void Start()
	{
	}

	protected void OnDestroy()
	{
	}

	private void AckLink(string peerId, MsgCheckLink message)
	{
	}

	private void StartGame()
	{
	}

	public void OnPlayerJoinReq(string peerId, MsgPlayerJoinReq req)
	{
	}

	private int GetOneSlot()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void FreeOneSlot(int i)
	{
	}

	private void RebuildPlayerList()
	{
	}

	private void OnRequestPlayerList(string peerId, MsgRequestPlayerList req)
	{
	}

	private void OnPeerDisconnected(string peerId, MsgOnPeerDisconnect message)
	{
	}

	private void RemovePlayer(string peerId, MsgPlayerLeave msg)
	{
	}

	private void GoBack()
	{
	}

	private void SelectRandom()
	{
	}

	private void SelectPort()
	{
	}

	private void SelectFarm()
	{
	}

	private void SelectMaple()
	{
	}

	private void SelectSky()
	{
	}

	private void SelectVolcano()
	{
	}

	private void SelectIce()
	{
	}

	private void SelectManor()
	{
	}

	private void SelectFactory()
	{
	}

	private void SelectTrack()
	{
	}

	private void SetDefaultSelect()
	{
	}

	private void SelectPropMode()
	{
	}

	private void SelectTimingMode()
	{
	}

	private void ChangeTrackAndModePreview()
	{
	}

	private void UpdatePlayerListView()
	{
	}

	private void TrackAndModeSelected()
	{
	}

	private void StartGameAsServer()
	{
	}

	private int RandomTrack()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void CheckEnable()
	{
	}

	private void CheckEnable(int sceneID)
	{
	}

	protected void Update()
	{
	}

	private void OnClientJoined(string peerId, MsgClientJoined message)
	{
	}
}
