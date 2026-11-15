using RaceProtocol;

namespace Leaderboard;

public class MKScore
{
	protected string name;

	protected uint score;

	protected int rank;

	protected uint mimihao;

	public int Rank => rank;

	public MKScore()
	{
		name = string.Empty;
		score = 0u;
		rank = -1;
		mimihao = 0u;
	}

	public MKScore(MsgScore msg)
	{
		name = msg.NickName;
		score = msg.Score;
		rank = msg.RankNo - 1;
		mimihao = msg.UserId;
	}

	public int getRank()
	{
		return rank + 1;
	}

	public uint getMimihao()
	{
		return mimihao;
	}

	public string getName()
	{
		return name;
	}

	public string getScore()
	{
		return FormatScore();
	}

	public override string ToString()
	{
		return rank + 1 + " " + mimihao + " " + name + " " + FormatScore();
	}

	protected string FormatScore()
	{
		string empty = string.Empty;
		int num = (int)(score / 6000);
		int num2 = (int)((score - num * 6000) / 100);
		int num3 = (int)(score - num * 6000 - num2 * 100);
		return $"{num:00}'{num2:00}\"{num3:00}";
	}
}
