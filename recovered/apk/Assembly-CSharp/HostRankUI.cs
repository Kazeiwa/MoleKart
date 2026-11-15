using System.Collections;
using System.Collections.Generic;
using GameNetwork;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class HostRankUI : OnlineRankUIBase
{
	protected Dictionary<string, MsgRank> rankDict = new Dictionary<string, MsgRank>();

	private bool selfIsAdded;

	private bool rankBrodcasted;

	private new void Awake()
	{
		base.Awake();
		NetHandlerRegistry.Instance.RegistHandler<MsgSelfFinishRace>(OnClientFinishRace);
	}

	protected void OnClientFinishRace(string peerId, MsgSelfFinishRace message)
	{
		Debug.Log("Into OnClientFinishRace");
		if (AppMain.Instance.IsServer)
		{
			Debug.Log("Peer finished the game:" + peerId);
			if (!rankDict.ContainsKey(peerId))
			{
				Debug.Log("Add one client finish race to Dict");
				rankDict.Add(peerId, message.Rank);
				rankList.Add(message.Rank);
				MsgAckSelfFinishRace.Builder builder = new MsgAckSelfFinishRace.Builder();
				builder.Received = 1;
				MsgAckSelfFinishRace message2 = builder.Build();
				NetworkManager.Instance.SendMessageToPeer(peerId, message2);
			}
			if (rankDict.Count == GameDataManager.Instance.GetPlayerDataList().Count)
			{
				Debug.Log("Everyone has finished the race");
				BrodCastRankInfoToClient();
			}
			else if (rankDict.Count == GameDataManager.Instance.GetPlayerDataList().Count - 1 && waitTime <= 0f && !RaceSceneData.IsFinish(TSingleton<CarManager>.Instance.GetHeroCar().ID))
			{
				Debug.Log("All clients finished but host didn't finish and counting has finished");
				AddSelfRank();
				BrodCastRankInfoToClient();
			}
			else
			{
				Debug.Log("unknown condition!");
			}
		}
	}

	private void AddSelfRank()
	{
		if (!selfIsAdded)
		{
			SceneUIManager.Instance.SetRaceStarted(started: false);
			SceneUIManager.Instance.RemoveAllSceneUI();
			ScoreReportManager.Instance.ReportScoreToGameCenter();
			LockMovement();
			DriveManager.Instance.Reset();
			Debug.Log("Add self rank info");
			int iD = TSingleton<CarManager>.Instance.GetHeroCar().ID;
			MsgRank.Builder builder = MsgRank.CreateBuilder();
			builder.Name = LocalUserConfig.Instance.userName;
			builder.CarType = LocalUserInfo.Instance.UserInfo.CarId;
			builder.RoleType = LocalUserInfo.Instance.UserInfo.roleType;
			bool flag = RaceSceneData.IsFinish(iD);
			builder.IsFinish = (flag ? 1 : 0);
			if (flag)
			{
				builder.Time = RaceSceneData.SpendTime(iD);
			}
			else
			{
				builder.Time = 0f;
			}
			builder.Rank = RaceSceneData.Rank(iD);
			builder.ID = iD;
			Debug.Log("my rank:" + builder.Rank);
			MsgRank msgRank = builder.Build();
			rankDict.Add(msgRank.Name, msgRank);
			rankList.Add(msgRank);
			if (rankList.Count == GameDataManager.Instance.GetPlayerDataList().Count)
			{
				BrodCastRankInfoToClient();
			}
			selfIsAdded = true;
		}
	}

	private bool CheckAllIsNotFinishInTime()
	{
		foreach (MsgRank rank in rankList)
		{
			if (rank.Time != 0f)
			{
				return false;
			}
		}
		Debug.Log("No one finish the race in time!!!!");
		return true;
	}

	private bool CheckAllRankDataIsValid()
	{
		bool[] array = new bool[GameDataManager.Instance.GetPlayerDataList().Count];
		foreach (MsgRank rank in rankList)
		{
			if (!array[rank.Rank - 1])
			{
				array[rank.Rank - 1] = true;
				continue;
			}
			Debug.Log("Rank Result is not VALID!!!!!!!!");
			return false;
		}
		return true;
	}

	protected void RebuildRankList()
	{
		Debug.Log("Rebuild rank list!!!!!!!");
		rankList.Clear();
		foreach (MsgRank value2 in rankDict.Values)
		{
			MsgRank.Builder builder = MsgRank.CreateBuilder();
			builder.Name = value2.Name;
			builder.RoleType = value2.RoleType;
			builder.CarType = value2.CarType;
			bool flag = RaceSceneData.IsFinish(value2.ID);
			builder.IsFinish = (flag ? 1 : 0);
			if (flag)
			{
				builder.Time = RaceSceneData.SpendTime(value2.ID);
			}
			else
			{
				builder.Time = 0f;
			}
			builder.Rank = RaceSceneData.Rank(value2.ID);
			builder.ID = value2.ID;
			MsgRank value = builder.Build();
			rankList.Add(value);
			Debug.Log("Rebuild add one :" + value2.ID);
		}
	}

	private void BrodCastRankInfoToClient()
	{
		if (rankBrodcasted)
		{
			return;
		}
		NetworkManager.Instance.SetSendHeartbeat(isSend: false);
		Debug.Log("Server brodcast finish now message to all clients" + rankDict.Count + "----" + GameDataManager.Instance.GetPlayerDataList().Count);
		if (rankDict.Count != GameDataManager.Instance.GetPlayerDataList().Count)
		{
			return;
		}
		HideCountingView();
		if (!CheckAllRankDataIsValid())
		{
			RebuildRankList();
		}
		MsgFinishRaceNow.Builder builder = MsgFinishRaceNow.CreateBuilder();
		foreach (MsgRank rank in rankList)
		{
			builder.AddRankList(rank);
			Debug.Log("BrodCast::::: Finish:" + rank.IsFinish + " Name:" + rank.Name + " Time:" + rank.Time + " cartype:" + rank.CarType + " roletype:" + rank.RoleType + "Rank:" + rank.Rank);
		}
		MsgFinishRaceNow message = builder.Build();
		LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
		LocalNetFactory.Instance.LocalNet.SessionManager.EndSend();
		rankBrodcasted = true;
		Debug.Log("Finish now message did send");
		NetHandlerRegistry.Instance.UnregistHandler<MsgSelfFinishRace>(OnClientFinishRace);
		SoundManager.Instance.StopAllSound();
		DealBreakRecord();
		state = RANKUI_STATE.RANKUI_STATE_COMPLETE_SCALE;
	}

	private void Update()
	{
		SetAllRect();
		switch (state)
		{
		case RANKUI_STATE.RANKUI_STATE_COUNT_TEN_SEC:
			if (CheckSelfNotFinishInTime())
			{
				AddSelfRank();
			}
			waitTime -= Time.deltaTime;
			if (waitTime > 0f)
			{
				for (int num = 9; num > 0; num--)
				{
					if ((bool)countNumbers[num])
					{
						if (num == (int)waitTime)
						{
							countNumbers[num].active = true;
						}
						else
						{
							countNumbers[num].active = false;
						}
					}
				}
				if (SelfIsFinished())
				{
					AddSelfRank();
				}
			}
			else
			{
				HideCountingView();
				AddSelfRank();
				BrodCastRankInfoToClient();
			}
			break;
		case RANKUI_STATE.RANKUI_STATE_COMPLETE_SCALE:
			SetFinishWord();
			PlayFinishSound();
			PlayRankSound();
			delayTime -= Time.deltaTime;
			SetWord();
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
			showRankTime -= Time.deltaTime;
			if (showRankTime <= 0f)
			{
				state = RANKUI_STATE.RANKUI_STATE_FINISH;
			}
			break;
		}
		case RANKUI_STATE.RANKUI_STATE_FINISH:
			QuitNetworkLevel();
			break;
		case RANKUI_STATE.RANKUI_STATE_WAIT_TEN_SEC:
		case RANKUI_STATE.RANKUI_STATE_COMPLETE_SHOW:
			break;
		}
	}

	private void OnDestroy()
	{
		NetHandlerRegistry.Instance.UnregistHandler<MsgSelfFinishRace>(OnClientFinishRace);
	}
}
