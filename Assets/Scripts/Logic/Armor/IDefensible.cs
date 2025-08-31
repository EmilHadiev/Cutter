using System;

public interface IDefensible
{
    bool IsShieldExisting { get; }
    bool IsDefending { get; }

    bool TryDefend();
    void Deactivate();
    void Activate();
}