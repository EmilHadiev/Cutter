using System;

public interface IGameOverService
{
    event Action Lost;
    event Action Won;
    event Action Continue;

    void Lose();
    void Win();
    void Continued();
}