using System;

public interface IDefensible
{
    bool IsCanDefend { get; }

    bool TryDefend();
    void Deactivate();
    void Activate();
}