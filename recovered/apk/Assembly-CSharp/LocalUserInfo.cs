using System;
using System.Collections.Generic;
using UnityEngine;

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

	private UserInfo m_UserInfo = default(UserInfo);

	private byte[] m_Session;

	private uint m_RoleCreated;

	public int TrackLevel;

	private static LocalUserInfo instance;

	private List<Rank> m_lsRankRecord = new List<Rank>();

	private float m_fTotalGameTime;

	public static LocalUserInfo Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new LocalUserInfo();
			}
			return instance;
		}
	}

	public uint UserId
	{
		get
		{
			return m_UserInfo.UserId;
		}
		set
		{
			m_UserInfo.UserId = value;
		}
	}

	public byte[] Session
	{
		get
		{
			return m_Session;
		}
		set
		{
			m_Session = new byte[value.Length];
			Buffer.BlockCopy(value, 0, m_Session, 0, value.Length);
		}
	}

	public uint RoleCreated
	{
		get
		{
			return m_RoleCreated;
		}
		set
		{
			m_RoleCreated = value;
		}
	}

	public UserInfo UserInfo
	{
		get
		{
			return m_UserInfo;
		}
		set
		{
			m_UserInfo = value;
		}
	}

	public float TotalGameTime => m_fTotalGameTime;

	public void ReadRank()
	{
		int num = 0;
		if (PlayerPrefs.HasKey("RankCount"))
		{
			num = PlayerPrefs.GetInt("RankCount");
		}
		for (int i = 0; i < num; i++)
		{
			Rank item = new Rank
			{
				iType = 0,
				iRankDifficult = 0,
				iRank = 0,
				fScore = 0f,
				bUnLock = true
			};
			string key = "Rank" + i + "iType";
			if (PlayerPrefs.HasKey(key))
			{
				item.iType = PlayerPrefs.GetInt(key);
			}
			key = "Rank" + i + "iRankDifficult";
			if (PlayerPrefs.HasKey(key))
			{
				item.iRankDifficult = PlayerPrefs.GetInt(key);
			}
			key = "Rank" + i + "iRank";
			if (PlayerPrefs.HasKey(key))
			{
				item.iRank = PlayerPrefs.GetInt(key);
			}
			key = "Rank" + i + "fScore";
			if (PlayerPrefs.HasKey(key))
			{
				item.fScore = PlayerPrefs.GetFloat(key);
			}
			m_lsRankRecord.Add(item);
		}
	}

	public void SaveRank()
	{
		int count = m_lsRankRecord.Count;
		count = PlayerPrefs.GetInt("RankCount", count);
		for (int i = 0; i < count; i++)
		{
			Rank rank = m_lsRankRecord[i];
			string key = "Rank" + i + "iType";
			PlayerPrefs.SetInt(key, rank.iType);
			key = "Rank" + i + "iRankDifficult";
			PlayerPrefs.SetInt(key, rank.iRankDifficult);
			key = "Rank" + i + "iRank";
			PlayerPrefs.SetInt(key, rank.iRank);
			key = "Rank" + i + "fScore";
			PlayerPrefs.SetFloat(key, rank.fScore);
		}
	}

	public void AddNewRankRecord(int iType, int iRankDifficult, int iRank, float fScore)
	{
		bool flag = false;
		for (int i = 0; i < m_lsRankRecord.Count; i++)
		{
			Rank rank = m_lsRankRecord[i];
			if (rank.iRank == iRank && rank.iType == iType && rank.iRankDifficult == iRankDifficult)
			{
				flag = true;
				if (rank.fScore > fScore)
				{
					rank.fScore = fScore;
				}
			}
		}
		if (!flag)
		{
			Rank item = new Rank
			{
				iType = iType,
				iRank = iRank,
				iRankDifficult = iRankDifficult,
				fScore = fScore,
				bUnLock = true
			};
			m_lsRankRecord.Add(item);
		}
	}

	public void ReadGameTime()
	{
		if (PlayerPrefs.HasKey("TotalGameTime"))
		{
			m_fTotalGameTime = PlayerPrefs.GetFloat("TotalGameTime");
		}
		else
		{
			m_fTotalGameTime = 0f;
		}
	}

	public void SaveGameTime()
	{
		m_fTotalGameTime += Time.time;
		PlayerPrefs.SetFloat("TotalGameTime", m_fTotalGameTime);
	}
}
