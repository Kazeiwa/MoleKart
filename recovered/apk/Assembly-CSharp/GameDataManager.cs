using System.Collections;
using System.Collections.Generic;

public class GameDataManager
{
	private static RoomData m_RoomData = default(RoomData);

	private static ArrayList m_PlayerDataList = new ArrayList();

	private static GameDataManager instance;

	private List<uint> PlayerIndexPool = new List<uint>();

	public static GameDataManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameDataManager();
			}
			return instance;
		}
	}

	public void SetRoomData(RoomData _data)
	{
		m_RoomData.CurMembers = _data.CurMembers;
		m_RoomData.MapId = _data.MapId;
		m_RoomData.MaxMembers = _data.MaxMembers;
		m_RoomData.Password = _data.Password;
		m_RoomData.RacingType = _data.RacingType;
		m_RoomData.RoomId = _data.RoomId;
		m_RoomData.RoomName = _data.RoomName;
		if (m_RoomData.MemberList == null)
		{
			m_RoomData.MemberList = new ArrayList();
		}
		else
		{
			m_RoomData.MemberList.Clear();
		}
		foreach (RoomMemberData member in _data.MemberList)
		{
			m_RoomData.MemberList.Add(member);
		}
	}

	public RoomData GetRoomData()
	{
		return m_RoomData;
	}

	private uint GenerateNewPlayerID()
	{
		uint num;
		for (num = (uint)PlayerIndexPool.Count; PlayerIndexPool.Contains(num); num++)
		{
		}
		PlayerIndexPool.Add(num);
		return num;
	}

	public void SetPlayerDataList(ArrayList _dataList)
	{
		m_PlayerDataList.Clear();
		foreach (PlayerData _data in _dataList)
		{
			m_PlayerDataList.Add(_data);
		}
	}

	public ArrayList GetPlayerDataList()
	{
		return m_PlayerDataList;
	}

	public bool AddPlayer(ref PlayerData newPlayer)
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.peerID == newPlayer.peerID)
			{
				return false;
			}
		}
		newPlayer.PlayerId = GenerateNewPlayerID();
		m_PlayerDataList.Add(newPlayer);
		return true;
	}

	public bool AddPlayer(PlayerData newPlayer, uint playerId)
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.peerID == newPlayer.peerID)
			{
				return false;
			}
		}
		newPlayer.PlayerId = playerId;
		m_PlayerDataList.Add(newPlayer);
		return true;
	}

	public PlayerData GetLocalPlayerData()
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.IsMaster)
			{
				return playerData;
			}
		}
		return new PlayerData();
	}

	public bool AddPlayer(PlayerData newPlayer, uint playerId, bool isLocalUser)
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.peerID == newPlayer.peerID)
			{
				return false;
			}
		}
		newPlayer.PlayerId = playerId;
		newPlayer.IsMaster = isLocalUser;
		m_PlayerDataList.Add(newPlayer);
		return true;
	}

	public void RemovePlayer(string peerId)
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.peerID == peerId)
			{
				m_PlayerDataList.Remove(playerData);
				break;
			}
		}
	}

	public string GetPeerId()
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.IsMaster)
			{
				return playerData.peerID;
			}
		}
		return string.Empty;
	}

	public bool RemovePlayer(ref PlayerData newPlayer)
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.peerID == newPlayer.peerID)
			{
				m_PlayerDataList.Remove(playerData);
				return true;
			}
		}
		return false;
	}

	public bool GetPlayerData(ulong playerId, ref PlayerData data)
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.PlayerId == playerId)
			{
				data = playerData;
				return true;
			}
		}
		return false;
	}

	public bool GetPlayerData(string peerID, ref PlayerData data)
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.peerID == peerID)
			{
				data = playerData;
				return true;
			}
		}
		return false;
	}

	public uint GetPlayerId(string peerID)
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.peerID == peerID)
			{
				return playerData.PlayerId;
			}
		}
		return uint.MaxValue;
	}

	public void RemoveSelf()
	{
		foreach (PlayerData playerData in m_PlayerDataList)
		{
			if (playerData.IsMaster)
			{
				m_PlayerDataList.Remove(playerData);
				break;
			}
		}
	}

	public void RemoveAllPlayers()
	{
		if (m_PlayerDataList != null)
		{
			m_PlayerDataList.Clear();
		}
	}
}
