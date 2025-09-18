using System;

public class GameStarter : IGameStarter
{
    public event Action Started;

    public GameStarter()
    {

    }

    public void Start()
    {
        Started?.Invoke();
    }
}
