using System.Collections.Generic;
using UnityEngine;

namespace Leaderboard;

public class LeaderboardDataManager
{
	public enum TIME_SCOPE
	{
		TIME_SCOPE_TODAY = 1,
		TIME_SCOPE_WEEK,
		TIME_SCOPE_ALL_TIME
	}

	public enum GAME_MODE
	{
		Prop = 1,
		Time
	}

	public enum TRACK_NAME
	{
		Sky = 1104,
		Ice = 1106,
		Volcano = 1105,
		Port = 1108,
		Maple = 1109,
		Farm = 1110,
		Manor = 1111,
		Mushroom = 1112,
		Factory = 1113
	}

	public const int SKY = 1104;

	public const int VOLCALO = 1105;

	public const int ICE = 1106;

	public const int PORT = 1108;

	public const int MAPLE = 1109;

	public const int FARM = 1110;

	public const int MANOR = 1111;

	public const int MOGU = 1112;

	public const int FACTORY = 1113;

	private const int NUMBER_IN_ONE_PAGE = 30;

	private Dictionary<string, List<MKScore>> dataIndexDict = new Dictionary<string, List<MKScore>>();

	private Dictionary<string, MKMyScore> myLeadboardData = new Dictionary<string, MKMyScore>();

	public LeaderboardDataManager()
	{
		Init();
	}

	public static int ConvertTrackID(int trackID)
	{
		int result = 0;
		switch (trackID)
		{
		case 1104:
			result = 1104;
			break;
		case 1105:
			result = 1105;
			break;
		case 1106:
			result = 1106;
			break;
		case 1101:
			result = 1108;
			break;
		case 1108:
			result = 1109;
			break;
		case 1102:
			result = 1110;
			break;
		case 1107:
			result = 1111;
			break;
		case 1103:
			result = 1112;
			break;
		case 1109:
			result = 1113;
			break;
		default:
			Debug.Log("track unknown");
			break;
		}
		return result;
	}

	protected void Init()
	{
	}

	public void AddScore(string category, MKScore score)
	{
		if (!dataIndexDict.TryGetValue(category, out var value))
		{
			value = new List<MKScore>();
			dataIndexDict.Add(category, value);
		}
		if (value != null)
		{
			if (value.Count > score.Rank)
			{
				value[score.Rank] = score;
			}
			else
			{
				value.Add(score);
			}
		}
	}

	public void AddSelfScore(string category, MKMyScore score)
	{
		if (myLeadboardData.ContainsKey(category))
		{
			myLeadboardData[category] = score;
		}
		else
		{
			myLeadboardData.Add(category, score);
		}
	}

	public List<MKScore> GetScoreList(string category, int location)
	{
		List<MKScore> value = new List<MKScore>();
		if (dataIndexDict.TryGetValue(category, out value) && value != null)
		{
			if (value.Count >= (location + 1) * 30)
			{
				return value.GetRange(location * 30, 30);
			}
			return value.GetRange(location * 30, value.Count - location * 30);
		}
		return null;
	}

	public List<MKScore> GetScoreList(string category, int location, int length)
	{
		List<MKScore> value = new List<MKScore>();
		if (dataIndexDict.TryGetValue(category, out value))
		{
			value.GetRange(location, length);
			return value;
		}
		return null;
	}

	public bool CheckHasScoreList(string category, int location)
	{
		if (dataIndexDict.ContainsKey(category) && dataIndexDict[category].Count >= location * 30 + 1)
		{
			return true;
		}
		return false;
	}

	public bool CheckHasSelfScore(string category)
	{
		return myLeadboardData.ContainsKey(category);
	}

	public MKMyScore GetMyScore(string category)
	{
		MKMyScore value = null;
		if (myLeadboardData.TryGetValue(category, out value))
		{
			return value;
		}
		return null;
	}
}
