public class RaceProtocolData
{
	private static RaceProtocolData instance;

	private Net_Package_PlayerSyncData m_playerSyncData;

	private bool m_isArbiterPlayer;

	private static ReputationData m_playerReputationData;

	public static RaceProtocolData Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void SetRaceRequestSyncPlayerDataServer(Net_Package_PlayerSyncData playerSyncData)
	{
	}

	public Net_Package_PlayerSyncData GetRaceRequestSyncPlayerData()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetRaceJudge(bool value)
	{
	}

	public bool GetRaceJudge()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetReputationLevel(uint level)
	{
	}

	public void SetReputationCoin(uint coin)
	{
	}

	public ReputationData GetReputationData()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
