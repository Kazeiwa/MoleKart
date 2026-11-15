using System.Collections.Generic;

public class GameRoomData
{
	private RoomData m_RoomData;

	private Dictionary<uint, RoomMemberData> m_MemberList;

	private static GameRoomData instance;

	public static GameRoomData Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RoomData RoomData
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public ushort CurrentRoomId
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public void AddMember(RoomMemberData _MemberInfo)
	{
	}

	public void RemoveAllMembers()
	{
	}

	public void UpdateMemberInfo(RoomMemberData _MemberInfo)
	{
	}

	public void RemoveMember(uint userId)
	{
	}

	public Dictionary<uint, RoomMemberData> GetMemberList()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
