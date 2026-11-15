using System;
using System.Collections.Generic;
using RaceProtocol;

public class ScoreReportManager
{
	private const string HOST_IP = "211.151.105.138";

	private const int HOST_PORT = 25103;

	private const int MAX_RETRY = 3;

	private const float TIME_OUT = 5f;

	private int currentRetry;

	private static ScoreReportManager instance;

	private Dictionary<string, string> OFIDDict;

	private bool tcpConnected;

	private Dictionary<string, SaveScoreReq> scoreToSend;

	private List<SaveScoreReq> scoreToSendList;

	private float sendScoreTime;

	public static ScoreReportManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private void RegisterScoreReportHandler()
	{
	}

	public void UnRegisterScoreReportHandler()
	{
	}

	private void Init()
	{
	}

	private void Update()
	{
	}

	private void OnMyScoreReported(string peerId, SaveScoreAck msg)
	{
	}

	private void OnConnectedToHost(string peerId, HostConnected msg)
	{
	}

	private void OnDisconnectedToHost(string peerId, HostDisconnected msg)
	{
	}

	private void ConnectToHost()
	{
	}

	private void SetOFIDDict()
	{
	}

	private string GetRaceModeString()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private string GetTrackNameString()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ReportScoreToGameCenter()
	{
	}

	private void BuildScore(string mode, uint score)
	{
	}

	private void ReportScoreToTaomee()
	{
	}

	private DateTime FromUnixTime(long unixTime)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private long ToUnixTime(DateTime date)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
