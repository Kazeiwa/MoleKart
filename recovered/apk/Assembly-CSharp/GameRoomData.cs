using System.Collections.Generic;

public class GameRoomData
{
	private RoomData m_RoomData = default(RoomData);

	private Dictionary<uint, RoomMemberData> m_MemberList = new Dictionary<uint, RoomMemberData>();

	private static GameRoomData instance;

	public static GameRoomData Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameRoomData();
			}
			return instance;
		}
	}

	public RoomData RoomData
	{
		get
		{
			return m_RoomData;
		}
		set
		{
			m_RoomData = value;
		}
	}

	public ushort CurrentRoomId
	{
		get
		{
			return m_RoomData.RoomId;
		}
		set
		{
			m_RoomData.RoomId = value;
		}
	}

	public void AddMember(RoomMemberData _MemberInfo)
	{
		if (m_MemberList.ContainsKey(_MemberInfo.PlayerId))
		{
			m_MemberList[_MemberInfo.PlayerId] = _MemberInfo;
		}
		else
		{
			m_MemberList.Add(_MemberInfo.PlayerId, _MemberInfo);
		}
	}

	public void RemoveAllMembers()
	{
		m_MemberList.Clear();
	}

	public void UpdateMemberInfo(RoomMemberData _MemberInfo)
	{
		if (m_MemberList.ContainsKey(_MemberInfo.PlayerId))
		{
			m_MemberList[_MemberInfo.PlayerId] = _MemberInfo;
		}
	}

	public void RemoveMember(uint userId)
	{
		if (m_MemberList.ContainsKey(userId))
		{
			m_MemberList.Remove(userId);
		}
	}

	public Dictionary<uint, RoomMemberData> GetMemberList()
	{
		return m_MemberList;
	}
}
