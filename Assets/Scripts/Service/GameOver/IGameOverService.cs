using System;

public interface IGameOverService
{
    event Action Lost;
    event Action Won;

    void Lose();
    void Win();
}