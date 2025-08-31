using System;

public interface IParryable
{
    event Action ParryStarted;
    event Action ParryStopped;

    bool TryActivate();
}