using System.Collections;
using System.Collections.Generic;

public class GameHallData
{
	private ArrayList _channelList;

	private Dictionary<byte, Dictionary<ushort, RoomData>> _hallDictionary;

	private byte m_CurrentChannel;

	private byte m_CurrentRaceType;

	private static GameHallData instance;

	public static GameHallData Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public byte CurrentChannel
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public byte CurrentRaceType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public ArrayList CreateRoomDataList()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public ArrayList CreateRoomMemberList()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public ArrayList CreateChannelDataList()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void InitData()
	{
	}

	public void AddChannelDataList(ArrayList _dataList)
	{
	}

	public void AddRoomData(RoomData roomData)
	{
	}

	public int GetGameChannels(ArrayList channelList)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Dictionary<ushort, RoomData> GetGameRooms(byte level, int page, int count)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public RoomData GetGameRoomData(byte level, ushort roomId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
