using System;

public class GameOverService : IGameOverService
{
    private readonly IGameplaySoundContainer _soundContainer;
    private readonly IAmbientSoundContainer _ambientContainer;

    public event Action Won;
    public event Action Lost;

    public GameOverService(IGameplaySoundContainer soundContainer, IAmbientSoundContainer ambientContainer)
    {
        _soundContainer = soundContainer;
        _ambientContainer = ambientContainer;
    }

    public void Win()
    {
        _ambientContainer.Stop();
        _soundContainer.Play(SoundsName.Win);
        Won?.Invoke();
    }

    public void Lose()
    {
        _ambientContainer.Stop();
        _soundContainer.Play(SoundsName.Lose);
        Lost?.Invoke();
    }
}
