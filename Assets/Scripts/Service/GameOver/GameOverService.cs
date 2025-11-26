using System;

public class GameOverService : IGameOverService
{
    private readonly IGameplaySoundContainer _soundContainer;
    private readonly IAmbientSoundContainer _ambientContainer;
    private readonly ISavable _saver;
    private readonly PlayerProgress _progress;
    private readonly PlayerData _data;

    public event Action Won;
    public event Action Lost;
    public event Action Continue;

    public GameOverService(IGameplaySoundContainer soundContainer, IAmbientSoundContainer ambientContainer, ISavable saver, 
        PlayerProgress playerProgress, PlayerData data)
    {
        _soundContainer = soundContainer;
        _ambientContainer = ambientContainer;
        _saver = saver;
        _progress = playerProgress;
        _data = data;
    }

    public void Win()
    {
        _ambientContainer.Stop();
        _soundContainer.Play(SoundsName.Win);
        AddLevel();
        _saver.Save();
        Won?.Invoke();
    }

    private void AddLevel()
    {
        if (_progress.IsHardcoreMode)
            _progress.CurrentHardcoreLevel += 1;
        else
            _progress.CurrentLevel += 1;
    }

    public void Lose()
    {
        _ambientContainer.Stop();
        _soundContainer.Play(SoundsName.Lose);
        TryResetHardcore();
        _saver.Save();
        Lost?.Invoke();
    }

    public void Continued()
    {
        _ambientContainer.PlayRandomAmbient();
        Continue?.Invoke();
    }

    private void TryResetHardcore()
    {
        if (_progress.IsHardcoreMode)
        {
            _progress.CurrentHardcoreLevel = 0;
            _data.HardcoreHealth = 1;
            _data.HardcoreEnergy = 1;
        }
    }
}
