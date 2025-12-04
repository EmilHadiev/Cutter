using YG;

public class LeaderBoard : ILeaderBoard
{
    public void SetCompletedLevels(int score)
    {
        YG2.SetLeaderboard(LeaderBoardNames.CompletedLevels, score);
    }

    public void SetHardcoreScore(int score)
    {
        YG2.SetLeaderboard(LeaderBoardNames.Hardcore, score);
    }
}
