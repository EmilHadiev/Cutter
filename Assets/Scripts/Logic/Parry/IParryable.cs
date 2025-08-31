using System;

public interface IParryable
{
    bool IsParryTime { get; }

    void Activate();
    void Deactivate();
}