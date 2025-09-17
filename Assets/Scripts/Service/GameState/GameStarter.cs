using System;

public class GameStarter : IGameStarter
{
    private readonly IPause _pause;

    public event Action Started;

    public GameStarter(IPause pause)
    {
        _pause = pause;
        pause.Stop();
    }

    public void Start()
    {
        _pause.Continue();
        Started?.Invoke();
    }
}
