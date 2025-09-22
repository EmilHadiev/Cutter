using System;

public class GameStarter : IGameStarter
{
    private readonly IPlayer _player;
    private readonly IAmbientSoundContainer _soundContainer;

    public event Action Started;

    public GameStarter(IPlayer player, IAmbientSoundContainer ambientSoundContainer)
    {
        _player = player;
        _soundContainer = ambientSoundContainer;
    }

    public void Start()
    {
        Started?.Invoke();
        _player.Movable.StartMove();
        _soundContainer.PlayRandomAmbient();
    }
}
