using System;
using System.Collections.Generic;
using FrameWork;
using Leaderboard;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class ScoreReportManager
{
	private const string HOST_IP = "211.151.105.138";

	private const int HOST_PORT = 25113;

	private const int MAX_RETRY = 3;

	private const float TIME_OUT = 5f;

	private int currentRetry;

	private static ScoreReportManager instance;

	private Dictionary<string, string> OFIDDict = new Dictionary<string, string>();

	private bool tcpConnected;

	private Dictionary<string, SaveScoreReq> scoreToSend = new Dictionary<string, SaveScoreReq>();

	private List<SaveScoreReq> scoreToSendList = new List<SaveScoreReq>();

	private float sendScoreTime;

	public static ScoreReportManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new ScoreReportManager();
				instance.Init();
				instance.SetOFIDDict();
			}
			return instance;
		}
	}

	private void Init()
	{
		NetHandlerRegistry.Instance.RegistHandler<HostConnected>(OnConnectedToHost);
		NetHandlerRegistry.Instance.RegistHandler<HostDisconnected>(OnDisconnectedToHost);
		NetHandlerRegistry.Instance.RegistHandler<SaveScoreAck>(OnMyScoreReported);
	}

	private void Update()
	{
		if (sendScoreTime != 0f && Time.time - sendScoreTime > 5f)
		{
			MyTcpClient.Instance.CloseClient();
		}
	}

	private void OnMyScoreReported(string peerId, SaveScoreAck msg)
	{
		sendScoreTime = 0f;
		if (msg.Succeed)
		{
			if (scoreToSendList.Count > 0 && scoreToSendList[0] != null)
			{
				scoreToSendList.RemoveAt(0);
			}
			if (scoreToSendList.Count == 0)
			{
				MyTcpClient.Instance.CloseClient();
			}
		}
	}

	private void OnConnectedToHost(string peerId, HostConnected msg)
	{
		currentRetry = 0;
		ReportScoreToTaomee();
	}

	private void OnDisconnectedToHost(string peerId, HostDisconnected msg)
	{
		if (scoreToSendList.Count > 0)
		{
			ConnectToHost();
		}
	}

	private void ConnectToHost()
	{
		if (currentRetry < 3 && !tcpConnected)
		{
			MyTcpClient.Instance.ConnectToHost("211.151.105.138", 25113);
			currentRetry++;
		}
	}

	private void SetOFIDDict()
	{
		OFIDDict.Add("Time.Sky", "1053807");
		OFIDDict.Add("Prop.Sky", "1053797");
		OFIDDict.Add("Time.Volcano", "1053757");
		OFIDDict.Add("Prop.Volcano", "1053767");
		OFIDDict.Add("Time.Ice", "1053777");
		OFIDDict.Add("Prop.Ice", "1053787");
		OFIDDict.Add("Time.Port", "1082127");
		OFIDDict.Add("Prop.Port", "1082117");
		OFIDDict.Add("Time.Maple", "1090467");
		OFIDDict.Add("Prop.Maple", "1090457");
		OFIDDict.Add("Time.Farm", "1106547");
		OFIDDict.Add("Prop.Farm", "1106537");
		OFIDDict.Add("Prop.Manor", "1106557");
		OFIDDict.Add("Time.Manor", "1106567");
		OFIDDict.Add("Prop.Mushroom", "1106657");
		OFIDDict.Add("Time.Mushroom", "1106667");
	}

	private string GetRaceModeString()
	{
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		IRaceLogicModule raceLogicModule = frameWork.GetModule(enmModuleID.MI_RACELOGIC) as IRaceLogicModule;
		switch (raceLogicModule.GetCurrentRaceType())
		{
		case RacingType.SpeedSingleMode:
			return "Time";
		case RacingType.OnlineTimingMode:
			return "Time";
		case RacingType.PropSingleMode:
			return "Prop";
		case RacingType.OnlinePropMode:
			return "Prop";
		default:
			Debug.Log("Error to get race mode.");
			return null;
		}
	}

	private string GetTrackNameString()
	{
		switch (SceneManager.Instance.CurGameSceneId)
		{
		case 1101:
			return "Port";
		case 1102:
			return "Farm";
		case 1103:
			return "Mushroom";
		case 1104:
			return "Sky";
		case 1105:
			return "Volcano";
		case 1106:
			return "Ice";
		case 1107:
			return "Manor";
		case 1108:
			return "Maple";
		case 1109:
			return "Factory";
		default:
			Debug.Log("Error to get track name");
			return null;
		}
	}

	public void ReportScoreToGameCenter()
	{
		if (!RaceSceneData.IsFinish(TSingleton<CarManager>.Instance.GetHeroCar().ID))
		{
			Debug.Log("Not report score");
			return;
		}
		currentRetry = 0;
		string raceModeString = GetRaceModeString();
		string trackNameString = GetTrackNameString();
		string text = raceModeString + "." + trackNameString;
		long num = (long)(RaceSceneData.SpendTime(TSingleton<CarManager>.Instance.GetHeroCar().ID) * 100f);
		if (raceModeString != null && trackNameString != null && num > 0)
		{
			Debug.Log("Report to game center!!!  Time =" + num + "ID = " + text);
			int num2 = (int)(num / 6000);
			int num3 = (int)((num - num2 * 6000) / 100);
			int num4 = (int)(num - num2 * 6000 - num3 * 100);
			string text2 = "0:" + num2 + ":" + num3 + "." + num4;
		}
		BuildScore(raceModeString, (uint)num);
		ConnectToHost();
	}

	private void BuildScore(string mode, uint score)
	{
		uint mode2 = ((mode == "Prop") ? 1u : 2u);
		SavedScore.Builder builder = new SavedScore.Builder();
		builder.Mode = mode2;
		builder.Track = (uint)LeaderboardDataManager.ConvertTrackID(SceneManager.Instance.CurGameSceneId);
		Debug.Log("track old " + SceneManager.Instance.CurGameSceneId);
		Debug.Log("track new " + builder.Track);
		builder.Score = score;
		builder.Time = (uint)ToUnixTime(TimeZoneInfo.ConvertTimeToUtc(DateTime.Now));
		SavedScore savedScore = builder.Build();
		SaveScoreReq.Builder builder2 = new SaveScoreReq.Builder();
		builder2.DeviceId = SystemConfig.Instance.UUID;
		builder2.NickName = LocalUserConfig.Instance.userName;
		builder2.SavedScore = savedScore;
		SaveScoreReq item = builder2.Build();
		scoreToSendList.Add(item);
	}

	private void ReportScoreToTaomee()
	{
		foreach (SaveScoreReq scoreToSend in scoreToSendList)
		{
			Debug.Log("Report a score to Taomee");
			sendScoreTime = Time.time;
			MyTcpClient.Instance.SendMsg(scoreToSend);
		}
	}

	private DateTime FromUnixTime(long unixTime)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
	}

	private long ToUnixTime(DateTime date)
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return Convert.ToInt64((date - dateTime).TotalSeconds);
	}
}
