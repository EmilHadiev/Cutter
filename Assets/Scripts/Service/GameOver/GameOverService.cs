using System;

public class GameOverService : IGameOverService
{
    public event Action Won;
    public event Action Lost;

    public void Win()
    {
        Won?.Invoke();
    }

    public void Lose()
    {
        Lost?.Invoke();
    }
}
