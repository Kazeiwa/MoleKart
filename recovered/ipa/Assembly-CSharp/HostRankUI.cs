using System.Collections.Generic;
using RaceProtocol;

public class HostRankUI : OnlineRankUIBase
{
	protected Dictionary<string, MsgRank> rankDict;

	private bool selfIsAdded;

	private bool rankBrodcasted;

	private new void Awake()
	{
	}

	protected void OnClientFinishRace(string peerId, MsgSelfFinishRace message)
	{
	}

	private void AddSelfRank()
	{
	}

	private bool CheckAllIsNotFinishInTime()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool CheckAllRankDataIsValid()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void RebuildRankList()
	{
	}

	private void BrodCastRankInfoToClient()
	{
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}
}
