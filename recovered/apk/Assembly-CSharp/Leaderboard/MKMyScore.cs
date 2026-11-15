using RaceProtocol;

namespace Leaderboard;

public class MKMyScore : MKScore
{
	private float percent;

	private LeaderboardDataManager.TIME_SCOPE timeScope;

	private LeaderboardDataManager.GAME_MODE gameMode;

	private LeaderboardDataManager.TRACK_NAME trackName;

	public MKMyScore(MyScorePercentAck msg)
	{
		name = msg.Score.NickName;
		rank = msg.Score.RankNo;
		mimihao = msg.Score.UserId;
		score = msg.Score.Score;
		percent = msg.RankPercent;
		timeScope = (LeaderboardDataManager.TIME_SCOPE)msg.RankType;
		gameMode = (LeaderboardDataManager.GAME_MODE)msg.Mode;
		trackName = (LeaderboardDataManager.TRACK_NAME)msg.Track;
	}

	public override string ToString()
	{
		return rank.ToString() + " " + mimihao.ToString() + " " + name + " " + FormatScore() + " " + percent + "%";
	}

	public string GetCategory()
	{
		return gameMode.ToString() + "." + trackName.ToString() + "." + timeScope;
	}
}
