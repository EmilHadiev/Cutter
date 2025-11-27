using System;
using YG;
using YG.Utils.LB;
using Zenject;

public class LeaderBoard : IInitializable, IDisposable, ILeaderBoard
{
    private LBData _hardcoreData;

    public LeaderBoard()
    {
        YG2.GetLeaderboard(LeaderBoardNames.Hardcore);
    }

    public void Initialize()
    {
        YG2.onGetLeaderboard += GetLeaderBoard;
    }

    public void Dispose()
    {
        YG2.onGetLeaderboard -= GetLeaderBoard;
    }

    private void GetLeaderBoard(LBData data)
    {
        _hardcoreData = data;
    }

    public void SetCompletedLevels(int score)
    {
        YG2.SetLeaderboard(LeaderBoardNames.CompletedLevels, score);
    }

    public void TrySetHardcoreScore(int score)
    {
        if (_hardcoreData.currentPlayer.score < score)
            YG2.SetLeaderboard(LeaderBoardNames.Hardcore, score);
    }
}
