using System.Collections.Generic;

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
		Factory = 1112
	}

	private const int NUMBER_IN_ONE_PAGE = 30;

	private Dictionary<string, List<MKScore>> dataIndexDict;

	private Dictionary<string, MKMyScore> myLeadboardData;

	protected void Init()
	{
	}

	public void AddScore(string category, MKScore score)
	{
	}

	public void AddSelfScore(string category, MKMyScore score)
	{
	}

	public List<MKScore> GetScoreList(string category, int location)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public List<MKScore> GetScoreList(string category, int location, int length)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CheckHasScoreList(string category, int location)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CheckHasSelfScore(string category)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public MKMyScore GetMyScore(string category)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
