using System;

public class GameOverService : IGameOverService
{
    private readonly IGameplaySoundContainer _soundContainer;
    private readonly IAmbientSoundContainer _ambientContainer;
    private readonly ISavable _saver;
    private readonly PlayerProgress _progress;

    public event Action Won;
    public event Action Lost;
    public event Action Continue;

    public GameOverService(IGameplaySoundContainer soundContainer, IAmbientSoundContainer ambientContainer, ISavable saver, PlayerProgress playerProgress)
    {
        _soundContainer = soundContainer;
        _ambientContainer = ambientContainer;
        _saver = saver;
        _progress = playerProgress;
    }

    public void Win()
    {
        _ambientContainer.Stop();
        _soundContainer.Play(SoundsName.Win);
        _progress.CurrentLevel += 1;
        _saver.Save();
        Won?.Invoke();
    }

    public void Lose()
    {
        _ambientContainer.Stop();
        _soundContainer.Play(SoundsName.Lose);
        _saver.Save();
        Lost?.Invoke();
    }

    public void Continued()
    {
        _ambientContainer.PlayRandomAmbient();
        Continue?.Invoke();
    }
}
