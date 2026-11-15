using System.Collections.Generic;

internal class LocalUserInfo
{
	private struct Rank
	{
		public int iType;

		public int iRankDifficult;

		public int iRank;

		public float fScore;

		public bool bUnLock;
	}

	private UserInfo m_UserInfo;

	private byte[] m_Session;

	private uint m_RoleCreated;

	public int TrackLevel;

	public int TrackId;

	public int NetWorkTrackID;

	public RacingType NetWorkMode;

	public ushort iCar;

	public ushort iRole;

	public ushort iSpeedCar;

	public ushort iSpeedRole;

	public int iSpeedTrackId;

	public bool isNetWork;

	private static LocalUserInfo instance;

	private List<Rank> m_lsRankRecord;

	private float m_fTotalGameTime;

	public static LocalUserInfo Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public uint UserId
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public byte[] Session
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public uint RoleCreated
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public UserInfo UserInfo
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float TotalGameTime
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void ReadRank()
	{
	}

	public void SaveRank()
	{
	}

	public void AddNewRankRecord(int iType, int iRankDifficult, int iRank, float fScore)
	{
	}

	public void ReadGameTime()
	{
	}

	public void SaveGameTime()
	{
	}
}
