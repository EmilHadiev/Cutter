using System;

public interface IGameStarter
{
    void Start();
    event Action Started;
}