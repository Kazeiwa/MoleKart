using System.Collections;
using GameNetwork;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class ClientRankUI : OnlineRankUIBase
{
	protected bool m_ReceivedServerAckPackage;

	protected bool finishMessageSent;

	private new void Awake()
	{
		base.Awake();
		NetHandlerRegistry.Instance.RegistHandler<MsgFinishRaceNow>(OnFinishRaceNow);
		NetHandlerRegistry.Instance.RegistHandler<MsgAckSelfFinishRace>(ReceiveServerAck);
	}

	protected void ReceiveServerAck(string peerId, MsgAckSelfFinishRace message)
	{
		if (message != null)
		{
			m_ReceivedServerAckPackage = true;
		}
	}

	protected void SendFinishMessage2Server()
	{
		if (!AppMain.Instance.IsServer)
		{
			SceneUIManager.Instance.SetRaceStarted(started: false);
			SceneUIManager.Instance.RemoveAllSceneUI();
			ScoreReportManager.Instance.ReportScoreToGameCenter();
			DriveManager.Instance.Reset();
			LockMovement();
			Debug.Log("Send Client Finish message to server !!");
			int iD = TSingleton<CarManager>.Instance.GetHeroCar().ID;
			MsgSelfFinishRace.Builder builder = MsgSelfFinishRace.CreateBuilder();
			MsgRank.Builder builder2 = MsgRank.CreateBuilder();
			builder2.Name = LocalUserConfig.Instance.userName;
			builder2.RoleType = (int)LocalUserInfo.Instance.UserInfo.UserId;
			builder2.CarType = LocalUserInfo.Instance.UserInfo.CarId;
			builder2.Rank = RaceSceneData.Rank(iD);
			if (SelfIfFinished())
			{
				builder2.Time = RaceSceneData.SpendTime(iD);
			}
			else
			{
				builder2.Time = 0f;
			}
			builder2.IsFinish = (RaceSceneData.IsFinish(iD) ? 1 : 0);
			builder2.ID = iD;
			MsgRank rank = builder2.Build();
			builder.Rank = rank;
			MsgSelfFinishRace message = builder.Build();
			LocalNetFactory.Instance.LocalNet.SessionManager.BroadCastMsgPacket(message, reliable: true);
			LocalNetFactory.Instance.LocalNet.SessionManager.EndSend();
		}
	}

	protected void OnFinishRaceNow(string peerId, MsgFinishRaceNow message)
	{
		HideCountingView();
		NetworkManager.Instance.SetSendHeartbeat(isSend: false);
		Debug.Log("Received FINISH NOW command!");
		SoundManager.Instance.StopAllSound();
		for (int i = 0; i < message.RankListCount; i++)
		{
			rankList.Add(message.GetRankList(i));
			MsgRank msgRank = message.GetRankList(i);
			Debug.Log("Finish:" + msgRank.IsFinish + " Name:" + msgRank.Name + " Time:" + msgRank.Time + " cartype:" + msgRank.CarType + " roletype:" + msgRank.RoleType + "Rank:" + msgRank.Rank);
		}
		DealBreakRecord();
		SoundManager.Instance.StopAllSound();
		state = RANKUI_STATE.RANKUI_STATE_COMPLETE_SCALE;
		OnlineModeBase onlineModeBase = RaceModeFactory.CurrentRaceMode as OnlineModeBase;
		onlineModeBase.StopSynchronizeMovementPkg();
	}

	private void Update()
	{
		SetAllRect();
		switch (state)
		{
		case RANKUI_STATE.RANKUI_STATE_COUNT_TEN_SEC:
			if (CheckSelfNotFinishInTime() && Time.time - m_PreSendTime > 0.5f)
			{
				m_PreSendTime = Time.time;
				SendFinishMessage2Server();
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
				if (SelfIsFinished() && !finishMessageSent)
				{
					Debug.Log("finish in time, should  send finish message to server");
					SendFinishMessage2Server();
					m_PreSendTime = Time.time;
					finishMessageSent = true;
				}
			}
			else if (!finishMessageSent)
			{
				Debug.Log("NOT finish in time, should send finish message to server");
				SendFinishMessage2Server();
				finishMessageSent = true;
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
		NetHandlerRegistry.Instance.UnregistHandler<MsgFinishRaceNow>(OnFinishRaceNow);
		NetHandlerRegistry.Instance.UnregistHandler<MsgAckSelfFinishRace>(ReceiveServerAck);
	}
}
