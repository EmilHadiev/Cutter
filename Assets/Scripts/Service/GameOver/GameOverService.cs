using System;

public class GameOverService : IGameOverService
{
    private readonly IGameplaySoundContainer _soundContainer;
    private readonly IAmbientSoundContainer _ambientContainer;
    private readonly ISavable _saver;
    private readonly ILeaderBoard _leaderBoard;
    private readonly PlayerProgress _progress;
    private readonly PlayerData _data;
    private readonly IMetricService _metricService;

    public event Action Won;
    public event Action Lost;
    public event Action Continue;

    public GameOverService(IGameplaySoundContainer soundContainer, IAmbientSoundContainer ambientContainer, ISavable saver, 
        PlayerProgress playerProgress, PlayerData data, ILeaderBoard leaderBoard, IMetricService metricService)
    {
        _soundContainer = soundContainer;
        _ambientContainer = ambientContainer;
        _saver = saver;
        _progress = playerProgress;
        _data = data;
        _leaderBoard = leaderBoard;
        _metricService = metricService;
    }

    public void Win()
    {
        _ambientContainer.Stop();
        _soundContainer.Play(SoundsName.Win);
        AddLevel();
        SetLeaderBoardScore();
        SendLevelMetric(true);
        Save();
        Won?.Invoke();
    }


    public void Lose()
    {
        _ambientContainer.Stop();
        _soundContainer.Play(SoundsName.Lose);
        TryResetHardcore();
        SetLeaderBoardScore();
        SendLevelMetric(false);
        Save();
        Lost?.Invoke();
    }

    public void Continued()
    {
        _ambientContainer.PlayRandomAmbient();
        Continue?.Invoke();
    }

    private void SendLevelMetric(bool isComplete)
    {
        string subMetricName = "";
        int currentLevel = 0;

        if (_progress.IsHardcoreMode)
        {
            subMetricName = "hardcore";
            currentLevel = _progress.CurrentHardcoreLevel;
        }
        else
        {
            subMetricName = "normal";
            currentLevel = _progress.CurrentLevel;
        }

        string currentEvent = "";

        if (isComplete)
            currentEvent = MetricsName.LevelComplete;
        else
            currentEvent = MetricsName.LevelFailed;

        _metricService.SendMetric(currentEvent, subMetricName, currentLevel.ToString());
    }

    private void AddLevel()
    {
        if (_progress.IsHardcoreMode)
            _progress.CurrentHardcoreLevel += 1;
        else
            _progress.CurrentLevel += 1;
    }
    
    private void Save() => _saver.Save();

    private void TryResetHardcore()
    {
        if (_progress.IsHardcoreMode)
        {
            _progress.CurrentHardcoreLevel = 0;
            _data.HardcoreHealth = 1;
            _data.HardcoreEnergy = 1;
        }
    }

    private void SetLeaderBoardScore()
    {
        if (_progress.IsHardcoreMode)
            _leaderBoard.SetHardcoreScore(_progress.CurrentHardcoreLevel);
        else
            _leaderBoard.SetCompletedLevels(_progress.CurrentLevel);
    }
}
