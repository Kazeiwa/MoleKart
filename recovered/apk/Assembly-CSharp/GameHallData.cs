using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHallData
{
	private ArrayList _channelList = new ArrayList();

	private Dictionary<byte, Dictionary<ushort, RoomData>> _hallDictionary = new Dictionary<byte, Dictionary<ushort, RoomData>>();

	private byte m_CurrentChannel;

	private byte m_CurrentRaceType;

	private static GameHallData instance;

	public static GameHallData Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameHallData();
			}
			return instance;
		}
	}

	public byte CurrentChannel
	{
		get
		{
			return m_CurrentChannel;
		}
		set
		{
			m_CurrentChannel = value;
		}
	}

	public byte CurrentRaceType
	{
		get
		{
			return m_CurrentRaceType;
		}
		set
		{
			m_CurrentRaceType = value;
		}
	}

	public ArrayList CreateRoomDataList()
	{
		ArrayList arrayList = new ArrayList();
		RoomData roomData = new RoomData
		{
			RoomId = 1000,
			RoomName = "room name 1000",
			MaxMembers = 8,
			CurMembers = 4,
			Password = "1234",
			RacingType = 1
		};
		arrayList.Add(roomData);
		roomData.RoomId = 1001;
		roomData.RoomName = "room name 1001";
		roomData.MaxMembers = 6;
		roomData.CurMembers = 4;
		roomData.Password = "1234";
		roomData.RacingType = 3;
		arrayList.Add(roomData);
		roomData.RoomId = 1002;
		roomData.RoomName = "room name 1002";
		roomData.MaxMembers = 5;
		roomData.CurMembers = 2;
		roomData.Password = "1234";
		roomData.RacingType = 1;
		arrayList.Add(roomData);
		roomData.RoomId = 1003;
		roomData.RoomName = "room name 1003";
		roomData.MaxMembers = 8;
		roomData.CurMembers = 4;
		roomData.Password = "1234";
		roomData.RacingType = 3;
		arrayList.Add(roomData);
		roomData.RoomId = 1004;
		roomData.RoomName = "room name 1004";
		roomData.MaxMembers = 7;
		roomData.CurMembers = 3;
		roomData.Password = "1234";
		roomData.RacingType = 3;
		arrayList.Add(roomData);
		roomData.RoomId = 1005;
		roomData.RoomName = "room name 1005";
		roomData.MaxMembers = 8;
		roomData.CurMembers = 5;
		roomData.Password = "1234";
		roomData.RacingType = 1;
		arrayList.Add(roomData);
		roomData.RoomId = 1006;
		roomData.RoomName = "room name 1006";
		roomData.MaxMembers = 6;
		roomData.CurMembers = 1;
		roomData.Password = "1234";
		roomData.RacingType = 3;
		arrayList.Add(roomData);
		roomData.RoomId = 1007;
		roomData.RoomName = "room name 1007";
		roomData.MaxMembers = 6;
		roomData.CurMembers = 3;
		roomData.Password = string.Empty;
		roomData.RacingType = 1;
		arrayList.Add(roomData);
		roomData.RoomId = 1008;
		roomData.RoomName = "room name 1008";
		roomData.MaxMembers = 5;
		roomData.CurMembers = 4;
		roomData.Password = "1234";
		roomData.RacingType = 3;
		arrayList.Add(roomData);
		roomData.RoomId = 1009;
		roomData.RoomName = "room name 1009";
		roomData.MaxMembers = 7;
		roomData.CurMembers = 2;
		roomData.Password = string.Empty;
		roomData.RacingType = 3;
		arrayList.Add(roomData);
		roomData.RoomId = 1010;
		roomData.RoomName = "room name 1010";
		roomData.MaxMembers = 6;
		roomData.CurMembers = 3;
		roomData.Password = "1234";
		roomData.RacingType = 3;
		arrayList.Add(roomData);
		roomData.RoomId = 1011;
		roomData.RoomName = "room name 1011";
		roomData.MaxMembers = 5;
		roomData.CurMembers = 3;
		roomData.Password = "1234";
		roomData.RacingType = 1;
		arrayList.Add(roomData);
		roomData.RoomId = 1012;
		roomData.RoomName = "room name 1012";
		roomData.MaxMembers = 4;
		roomData.CurMembers = 4;
		roomData.Password = "1234";
		roomData.RacingType = 1;
		arrayList.Add(roomData);
		roomData.RoomId = 1013;
		roomData.RoomName = "room name 1013";
		roomData.MaxMembers = 5;
		roomData.CurMembers = 2;
		roomData.Password = "1234";
		roomData.RacingType = 1;
		arrayList.Add(roomData);
		return arrayList;
	}

	public ArrayList CreateRoomMemberList()
	{
		ArrayList arrayList = new ArrayList();
		RoomMemberData roomMemberData = new RoomMemberData
		{
			EnterTime = Time.time,
			IsLord = 1,
			IsReady = 0,
			IsLocal = 0,
			PlayerId = 20101201u
		};
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsReady = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.PlayerId = 20101202u;
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsReady = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.PlayerId = 20101203u;
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsReady = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.PlayerId = 20101204u;
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsReady = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.PlayerId = 20101205u;
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.IsReady = 0;
		roomMemberData.PlayerId = 20101207u;
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.IsReady = 0;
		roomMemberData.PlayerId = 20101208u;
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.IsReady = 1;
		roomMemberData.PlayerId = 20101209u;
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.IsReady = 0;
		roomMemberData.PlayerId = 20101210u;
		arrayList.Add(roomMemberData);
		roomMemberData.EnterTime = Time.time;
		roomMemberData.IsLord = 0;
		roomMemberData.IsLocal = 0;
		roomMemberData.IsReady = 1;
		roomMemberData.PlayerId = 20101211u;
		arrayList.Add(roomMemberData);
		return arrayList;
	}

	public ArrayList CreateChannelDataList()
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(0);
		arrayList.Add(1);
		return arrayList;
	}

	public void InitData()
	{
		if (_hallDictionary.Count > 0)
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		arrayList = CreateChannelDataList();
		AddChannelDataList(arrayList);
		ArrayList arrayList2 = new ArrayList();
		arrayList2 = CreateRoomDataList();
		foreach (RoomData item in arrayList2)
		{
			AddRoomData(item);
		}
	}

	public void AddChannelDataList(ArrayList _dataList)
	{
		_channelList.Clear();
		_channelList.AddRange(_dataList);
	}

	public void AddRoomData(RoomData roomData)
	{
		byte currentChannel = m_CurrentChannel;
		if (!_hallDictionary.ContainsKey(currentChannel))
		{
			Dictionary<ushort, RoomData> dictionary = new Dictionary<ushort, RoomData>();
			dictionary.Add(roomData.RoomId, roomData);
			_hallDictionary.Add(currentChannel, dictionary);
			return;
		}
		Dictionary<ushort, RoomData> dictionary2 = _hallDictionary[currentChannel];
		if (dictionary2.ContainsKey(roomData.RoomId))
		{
			dictionary2[roomData.RoomId] = roomData;
		}
		else
		{
			dictionary2.Add(roomData.RoomId, roomData);
		}
	}

	public int GetGameChannels(ArrayList channelList)
	{
		channelList = _channelList;
		return channelList.Count;
	}

	public Dictionary<ushort, RoomData> GetGameRooms(byte level, int page, int count)
	{
		if (_hallDictionary.ContainsKey(level))
		{
			return _hallDictionary[level];
		}
		return new Dictionary<ushort, RoomData>();
	}

	public RoomData GetGameRoomData(byte level, ushort roomId)
	{
		if (_hallDictionary.ContainsKey(level))
		{
			Dictionary<ushort, RoomData> dictionary = _hallDictionary[level];
			if (dictionary.ContainsKey(roomId))
			{
				return dictionary[roomId];
			}
		}
		return default(RoomData);
	}
}
