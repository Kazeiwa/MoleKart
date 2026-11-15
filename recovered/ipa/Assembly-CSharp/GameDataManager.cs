using System.Collections.Generic;

public class GameDataManager
{
	private List<PlayerData> m_PlayerDataList;

	private static GameDataManager instance;

	private List<uint> PlayerIndexPool;

	public static GameDataManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private uint GenerateNewPlayerID()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetPlayerDataList(List<PlayerData> dataList)
	{
	}

	public List<PlayerData> GetPlayerDataList()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool AddPlayer(ref PlayerData newPlayer)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool AddPlayer(PlayerData newPlayer, uint playerId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool AddPlayer(PlayerData newPlayer, uint playerId, bool isLocalUser)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RemovePlayer(string peerId)
	{
	}

	public bool RemovePlayer(ref PlayerData newPlayer)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GetPlayerData(int playerId, out PlayerData data)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GetPlayerData(string peerID, out PlayerData data)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public uint GetPlayerId(string peerID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RemoveAllPlayers()
	{
	}
}
