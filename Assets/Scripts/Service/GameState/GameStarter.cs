using System;

public class GameStarter : IGameStarter
{
    private readonly IPlayer _player;

    public event Action Started;

    public GameStarter(IPlayer player)
    {
        _player = player;
    }

    public void Start()
    {
        Started?.Invoke();
        _player.Movable.StartMove();
    }
}
