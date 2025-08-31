using System;

public interface IDefensible
{
    event Action ShieldBroke;

    bool IsShieldExisting { get; }
    bool IsDefending { get; }

    bool TryDefend();
    void Deactivate();
    void Activate();
}