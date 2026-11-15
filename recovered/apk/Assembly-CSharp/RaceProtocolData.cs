public class RaceProtocolData
{
	private static RaceProtocolData instance;

	private Net_Package_PlayerSyncData m_playerSyncData = default(Net_Package_PlayerSyncData);

	private bool m_isArbiterPlayer;

	private static ReputationData m_playerReputationData = default(ReputationData);

	public static RaceProtocolData Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new RaceProtocolData();
				m_playerReputationData.playerId = LocalUserInfo.Instance.UserId;
			}
			return instance;
		}
	}

	public void SetRaceRequestSyncPlayerDataServer(Net_Package_PlayerSyncData playerSyncData)
	{
		m_playerSyncData = playerSyncData;
	}

	public Net_Package_PlayerSyncData GetRaceRequestSyncPlayerData()
	{
		return m_playerSyncData;
	}

	public void SetRaceJudge(bool value)
	{
		m_isArbiterPlayer = value;
	}

	public bool GetRaceJudge()
	{
		return m_isArbiterPlayer;
	}

	public void SetReputationLevel(uint level)
	{
		m_playerReputationData.level = level;
	}

	public void SetReputationCoin(uint coin)
	{
		m_playerReputationData.coin = coin;
	}

	public ReputationData GetReputationData()
	{
		return m_playerReputationData;
	}
}
